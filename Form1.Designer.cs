using System;
using System.Windows.Forms;

namespace TTSTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Play = new System.Windows.Forms.Button();
            this.rateTrackBar = new System.Windows.Forms.TrackBar();
            this.VoiceList_Combo = new System.Windows.Forms.ComboBox();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.Voices_Label = new System.Windows.Forms.Label();
            this.Rate_Label = new System.Windows.Forms.Label();
            this.Volume_Label = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.rateTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(252, 200);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 0;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Play_Button_click);
            // 
            // rateTrackBar
            // 
            this.rateTrackBar.Location = new System.Drawing.Point(103, 86);
            this.rateTrackBar.Maximum = 6;
            this.rateTrackBar.Minimum = -6;
            this.rateTrackBar.Name = "rateTrackBar";
            this.rateTrackBar.Size = new System.Drawing.Size(224, 45);
            this.rateTrackBar.TabIndex = 1;
            this.rateTrackBar.Scroll += new System.EventHandler(this.RateTrackBarHandler);
            // 
            // VoiceList_Combo
            // 
            this.VoiceList_Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VoiceList_Combo.FormattingEnabled = true;
            this.VoiceList_Combo.Location = new System.Drawing.Point(103, 35);
            this.VoiceList_Combo.Name = "VoiceList_Combo";
            this.VoiceList_Combo.Size = new System.Drawing.Size(224, 21);
            this.VoiceList_Combo.TabIndex = 2;
            this.VoiceList_Combo.SelectedIndexChanged += new System.EventHandler(this.Voice_List_changed);
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.Location = new System.Drawing.Point(103, 137);
            this.volumeTrackBar.Maximum = 60;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(224, 45);
            this.volumeTrackBar.TabIndex = 3;
            this.volumeTrackBar.TickFrequency = 4;
            this.volumeTrackBar.Value = 10;
            this.volumeTrackBar.Scroll += new System.EventHandler(this.Volume_TrackBar);
            // 
            // Voices_Label
            // 
            this.Voices_Label.AutoSize = true;
            this.Voices_Label.Location = new System.Drawing.Point(22, 40);
            this.Voices_Label.Name = "Voices_Label";
            this.Voices_Label.Size = new System.Drawing.Size(39, 13);
            this.Voices_Label.TabIndex = 4;
            this.Voices_Label.Text = "Voices";
            // 
            // Rate_Label
            // 
            this.Rate_Label.AutoSize = true;
            this.Rate_Label.Location = new System.Drawing.Point(22, 90);
            this.Rate_Label.Name = "Rate_Label";
            this.Rate_Label.Size = new System.Drawing.Size(30, 13);
            this.Rate_Label.TabIndex = 5;
            this.Rate_Label.Text = "Rate";
            // 
            // Volume_Label
            // 
            this.Volume_Label.AutoSize = true;
            this.Volume_Label.Location = new System.Drawing.Point(22, 142);
            this.Volume_Label.Name = "Volume_Label";
            this.Volume_Label.Size = new System.Drawing.Size(42, 13);
            this.Volume_Label.TabIndex = 6;
            this.Volume_Label.Text = "Volume";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "EL TTS";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.showToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 274);
            this.Controls.Add(this.Volume_Label);
            this.Controls.Add(this.Rate_Label);
            this.Controls.Add(this.Voices_Label);
            this.Controls.Add(this.volumeTrackBar);
            this.Controls.Add(this.VoiceList_Combo);
            this.Controls.Add(this.rateTrackBar);
            this.Controls.Add(this.Play);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "EL TTS";
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rateTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.TrackBar rateTrackBar;
        private System.Windows.Forms.ComboBox VoiceList_Combo;
        private System.Windows.Forms.TrackBar volumeTrackBar;
        private System.Windows.Forms.Label Voices_Label;
        private System.Windows.Forms.Label Rate_Label;
        private System.Windows.Forms.Label Volume_Label;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
    }
}

