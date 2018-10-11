using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Threading;
using Microsoft.Win32;


namespace TTSTest
{
    public partial class Form1 : Form
    {
        private static bool _speeking=false;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifier, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hwnd, int id);

        private const string RegLoc=@"SOFTWARE\LackTTS";
        private RegistryKey _key;

        private Config _prevConfigFile;
        
        private static SpeechSynthesizer _synth;

        public Form1() => InitializeComponent();

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Icon = Properties.Resources.pixel;
            InitializeComp();


            
            RegisterHotKey(Handle, 13, (uint) FsModifier.Control, (uint) Keys.E);
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            UnregisterHotKey(this.Handle, 13);
            if (!IsConfigChange()) return;
            RemoveConfigKey();
            AddConfigKey();
        }
        private enum FsModifier
        {
            Alt=0x0002,
            Control=0x0002,
            Shift=0x0004,
            Windows=0x0008
        }

        
        private string GetMainText()
        {
            if (Focused)
                return Clipboard.ContainsText(TextDataFormat.Text) ? Clipboard.GetText(TextDataFormat.Text) : null;
            try
            {
                SendKeys.SendWait("^c");
            }
            catch
            {
                throw new InvalidOperationException("not selected any application");
            }

            return Clipboard.ContainsText(TextDataFormat.Text) ? Clipboard.GetText(TextDataFormat.Text) : null;
        }

        #region Configuration file

        private bool IsConfigChange()
        {
            if (_prevConfigFile == null) return false;
            var current = GetCurrentConfig();
            return !(_prevConfigFile.Voice == current.Voice & _prevConfigFile.Rate == current.Rate &
                     _prevConfigFile.Volume == current.Volume);
        }

        private void AddConfigKey()
        {
            _key = Registry.CurrentUser.OpenSubKey(RegLoc,true);

            
            using (var ms = new MemoryStream())
            {
                try
                {

                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, GetCurrentConfig());
                    Console.WriteLine("come on");
                    var data = ms.ToArray();
                    _key?.SetValue("config", data, RegistryValueKind.Binary);

                }catch(System.Runtime.Serialization.SerializationException e)
                {
                    Console.WriteLine("fuckkkkadfaslfasd");
                    Console.WriteLine(e.Message);
                }catch(System.UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("problem in adding config key");
                }
                
            }

            _key?.Close();
        }
        
        private void RemoveConfigKey()
        {
            _key = Registry.CurrentUser.OpenSubKey(RegLoc, true);
            if (_key == null) return;
            try
            {
                _key.DeleteValue("LackTTS");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        private void InitConfig(Config config)
        {
            try
            {
                VoiceList_Combo.SelectedIndex = config.Voice;
            }catch(NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
            rateTrackBar.Value = config.Rate;
            volumeTrackBar.Value = config.Volume;
        }
        
        private Config GetConfigFromReg()
        {
            _key = Registry.CurrentUser.OpenSubKey(RegLoc);
            if (_key == null) return new Config();
            try
            {
                using (var ms = new MemoryStream((byte[]) _key.GetValue("config")))
                {
                    var formatter = new BinaryFormatter();
                    return (Config) formatter.Deserialize(ms);

                }

            }
            catch (System.Runtime.Serialization.SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (System.ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            return new Config();
        }

        private Config GetCurrentConfig() => new Config{Rate = rateTrackBar.Value,Voice = VoiceList_Combo.SelectedIndex,Volume = volumeTrackBar.Value};

        private void CreateConfig()
        {
            CreateConfig(0,20,0);
        }
        
        private void CreateConfig(int rate, int volume, int voice)
        {
            _key = Registry.CurrentUser.CreateSubKey(RegLoc);
            _prevConfigFile = new Config{Rate = rate,Voice = voice,Volume = volume};
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(ms, _prevConfigFile);
                }catch(System.Runtime.Serialization.SerializationException e)
                {
                    Console.WriteLine(e.Message);
                }
                var data = ms.ToArray();
                _key?.SetValue("config", data, RegistryValueKind.Binary);
            }

            _key?.Close();
        }

        private bool IsExistConfig()
        {
            using (_key = Registry.CurrentUser.OpenSubKey(RegLoc))
                return _key != null;
            Console.WriteLine("yes");
        }
        
        #endregion
        
        
        #region Initializing sample

        private void InitializeComp()
        {
            _synth = new SpeechSynthesizer();
            foreach (var voice in _synth.GetInstalledVoices())
            {
                VoiceList_Combo.Items.Add(voice.VoiceInfo.Name);
            }

            if (IsExistConfig())
            {
                _prevConfigFile = GetConfigFromReg();
                InitConfig(_prevConfigFile);
            }
            else
            {
                CreateConfig();
                VoiceList_Combo.SelectedIndex = 0;
            }
        }

        private void TextToSpeech()
        {
            
            if (!_speeking)
            {
                var stringToPlay = GetMainText();
                if (stringToPlay == null) return;
                var t1 = new Thread(() =>
                {
                    _speeking = true;
                    try
                    {
                        _synth.Speak(stringToPlay);
                    }
                    catch(OperationCanceledException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                    _speeking = false;
                }) {IsBackground = true};
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            else
            {
                
                _synth.Dispose();
                _synth=new SpeechSynthesizer();
                _synth.SelectVoice(VoiceList_Combo.SelectedItem.ToString());
                _speeking = false;
            }
        }
        #endregion

        #region Minimize to tray code

        private void OnMinimize()
        {
            notifyIcon1.BalloonTipText="Select the text and press ctrl+E to speak";

            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(2000);
            this.Hide();
        }

        protected override void WndProc(ref Message m)
        {
            // Trap WM_SYSCOMMAND, SC_MINIMIZE
            if (m.Msg == 0x112 && m.WParam.ToInt32() == 0xf020)
            {
                OnMinimize();
                return; // NOTE: delete if you still want the default behavior
            }
            base.WndProc(ref m);

            if (m.Msg == 0x0312 & m.WParam.ToInt32() == 13)
            {
                TextToSpeech();
            }
            ;
        }

        #endregion

        #region notity icon code

        //on double click the notify icon
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        //on notify icon exit menu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //on notify icon show menu
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        #endregion

        #region Handlers

        //Rate handler
        private void RateTrackBarHandler(object sender, EventArgs e)
        {
            if (rateTrackBar.Value >= -5 & rateTrackBar.Value <= 5)
                _synth.Rate = rateTrackBar.Value;
        }

        //volume handler
        private void Volume_TrackBar(object sender, EventArgs e)
        {
            if (volumeTrackBar.Value >= 0 & volumeTrackBar.Value <= 100)
            {
                _synth.Volume = volumeTrackBar.Value;
            }
        }

        //play button handler
        private void Play_Button_click(object sender, MouseEventArgs e)
        {
            TextToSpeech();
        }

        //voice list combo box handler
        private void Voice_List_changed(object sender, EventArgs e)
        {
            
            
                if(!VoiceList_Combo.SelectedItem.Equals(null))
                    _synth.SelectVoice(VoiceList_Combo.SelectedItem.ToString());
            
        }

        #endregion
    }
    
    [Serializable]
    public class Config
    {
        public int Voice { get; set; } = 0;
        public int Volume { get; set; } = 20;
        public int Rate { get; set; } = 0;
    }
}