﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace week_number_in_tray
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            ShowIcon();
        }

        private void InitializeComponent()
        {
            this.WindowState = FormWindowState.Minimized;
            this.components = new System.ComponentModel.Container();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.bgColorDialog = new System.Windows.Forms.ColorDialog();
            this.btnBgColor = new System.Windows.Forms.Button();
            this.lblBgColor = new System.Windows.Forms.Label();
            this.lblFontColor = new System.Windows.Forms.Label();
            this.bgColorDisplay = new System.Windows.Forms.TextBox();
            this.fontColorDisplay = new System.Windows.Forms.TextBox();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.fontColorDialog = new System.Windows.Forms.ColorDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.IconClicked);
            // 
            // btnBgColor
            // 
            this.btnBgColor.Location = new System.Drawing.Point(197, 12);
            this.btnBgColor.Name = "btnBgColor";
            this.btnBgColor.Size = new System.Drawing.Size(75, 23);
            this.btnBgColor.TabIndex = 0;
            this.btnBgColor.Text = "Select";
            this.btnBgColor.UseVisualStyleBackColor = true;
            this.btnBgColor.Click += new System.EventHandler(this.BgColorBtnClicked);
            // 
            // lblBgColor
            // 
            this.lblBgColor.AutoSize = true;
            this.lblBgColor.Location = new System.Drawing.Point(12, 16);
            this.lblBgColor.Name = "lblBgColor";
            this.lblBgColor.Size = new System.Drawing.Size(101, 15);
            this.lblBgColor.TabIndex = 1;
            this.lblBgColor.Text = "Background color";
            // 
            // lblFontColor
            // 
            this.lblFontColor.AutoSize = true;
            this.lblFontColor.Location = new System.Drawing.Point(12, 44);
            this.lblFontColor.Name = "lblFontColor";
            this.lblFontColor.Size = new System.Drawing.Size(61, 15);
            this.lblFontColor.TabIndex = 2;
            this.lblFontColor.Text = "Font color";
            // 
            // bgColorDisplay
            // 
            this.bgColorDisplay.BackColor = System.Drawing.SystemColors.Window;
            this.bgColorDisplay.Enabled = false;
            this.bgColorDisplay.Location = new System.Drawing.Point(168, 12);
            this.bgColorDisplay.Name = "bgColorDisplay";
            this.bgColorDisplay.Size = new System.Drawing.Size(23, 23);
            this.bgColorDisplay.TabIndex = 3;
            this.bgColorDisplay.BackColor = Properties.Settings.Default.bgColor;
            // 
            // fontColorDisplay
            // 
            this.fontColorDisplay.Enabled = false;
            this.fontColorDisplay.Location = new System.Drawing.Point(168, 41);
            this.fontColorDisplay.Name = "fontColorDisplay";
            this.fontColorDisplay.Size = new System.Drawing.Size(23, 23);
            this.fontColorDisplay.TabIndex = 5;
            this.fontColorDisplay.BackColor = Properties.Settings.Default.fontColor;
            // 
            // btnFontColor
            // 
            this.btnFontColor.Location = new System.Drawing.Point(197, 41);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(75, 23);
            this.btnFontColor.TabIndex = 4;
            this.btnFontColor.Text = "Select";
            this.btnFontColor.UseVisualStyleBackColor = true;
            this.btnFontColor.Click += new System.EventHandler(this.FontColorBtnClicked);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 70);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.SaveBtnClicked);
            // 
            // Form
            // 
            this.ClientSize = new System.Drawing.Size(284, 102);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.fontColorDisplay);
            this.Controls.Add(this.btnFontColor);
            this.Controls.Add(this.bgColorDisplay);
            this.Controls.Add(this.lblFontColor);
            this.Controls.Add(this.lblBgColor);
            this.Controls.Add(this.btnBgColor);
            this.Name = "Form";
            this.Resize += new System.EventHandler(this.FormResized);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected void ShowIcon()
        {
            Color bgColor = Properties.Settings.Default.bgColor;
            Color fontColor = Properties.Settings.Default.fontColor;

            string week = GetWeek().ToString();

            Font font = new Font("Tahoma", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(fontColor);
            Bitmap bitmap = new Bitmap(16, 16);
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            g.Clear(bgColor);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            g.DrawString(week, font, brush, -1, 0);
            IntPtr hIcon = bitmap.GetHicon();
            Icon icon = Icon.FromHandle(hIcon);

            this.Icon = icon;
            notifyIcon.Icon = icon;
            notifyIcon.Text = $"Currently it's week {week}";
            notifyIcon.Visible = true;

            bitmap.Dispose();
            icon.Dispose();
        }

        public int GetWeek()
        {
            CultureInfo cultureInfo = new CultureInfo("fi-FI");
            Calendar calendar = cultureInfo.Calendar;
            CalendarWeekRule calendarWeekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            return calendar.GetWeekOfYear(DateTime.Now, calendarWeekRule, firstDayOfWeek);
        }

        private void IconClicked(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void FormResized(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void BgColorBtnClicked(object sender, EventArgs e)
        {
            bgColorDialog.ShowDialog();
            bgColorDisplay.BackColor = bgColorDialog.Color;
            Properties.Settings.Default.bgColor = bgColorDialog.Color;
        }

        private void FontColorBtnClicked(object sender, EventArgs e)
        {
            fontColorDialog.ShowDialog();
            fontColorDisplay.BackColor = fontColorDialog.Color;
            Properties.Settings.Default.fontColor = fontColorDialog.Color;
        }

        private void SaveBtnClicked(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            ShowIcon();
        }
    }
}
