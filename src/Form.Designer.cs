
using System;

namespace week_number_in_tray
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ColorDialog bgColorDialog;
        private System.Windows.Forms.Button btnBgColor;
        private System.Windows.Forms.Label lblBgColor;
        private System.Windows.Forms.Label lblFontColor;
        private System.Windows.Forms.TextBox bgColorDisplay;
        private System.Windows.Forms.TextBox fontColorDisplay;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.ColorDialog fontColorDialog;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbxRunOnStartup;
        private System.Windows.Forms.Timer timer;
    }
}

