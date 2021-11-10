using System;
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
using Microsoft.Win32;

namespace week_number_in_tray
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            SetProperties();
            ShowIcon();
        }

        private bool allowVisible;
        private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static readonly string StartupValue = "week-number-in-tray";

        private void InitializeComponent()
        {
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
            this.cbxRunOnStartup = new System.Windows.Forms.CheckBox();
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
            // 
            // fontColorDisplay
            // 
            this.fontColorDisplay.Enabled = false;
            this.fontColorDisplay.Location = new System.Drawing.Point(168, 41);
            this.fontColorDisplay.Name = "fontColorDisplay";
            this.fontColorDisplay.Size = new System.Drawing.Size(23, 23);
            this.fontColorDisplay.TabIndex = 5;
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
            // cbxRunOnStartup
            // 
            this.cbxRunOnStartup.AutoSize = true;
            this.cbxRunOnStartup.Location = new System.Drawing.Point(12, 73);
            this.cbxRunOnStartup.Name = "cbxRunOnStartup";
            this.cbxRunOnStartup.Size = new System.Drawing.Size(104, 19);
            this.cbxRunOnStartup.TabIndex = 7;
            this.cbxRunOnStartup.Text = "Run on startup";
            this.cbxRunOnStartup.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.ClientSize = new System.Drawing.Size(284, 102);
            this.Controls.Add(this.cbxRunOnStartup);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.fontColorDisplay);
            this.Controls.Add(this.btnFontColor);
            this.Controls.Add(this.bgColorDisplay);
            this.Controls.Add(this.lblFontColor);
            this.Controls.Add(this.lblBgColor);
            this.Controls.Add(this.btnBgColor);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "Week-number-in-tray";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Resize += new System.EventHandler(this.FormResized);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void SetVisibleCore(bool value)
        {
            if (!allowVisible)
            {
                value = false;
                if (!this.IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(value);
        }

        private void SetProperties()
        {
            this.bgColorDisplay.BackColor = Properties.Settings.Default.bgColor;
            this.fontColorDisplay.BackColor = Properties.Settings.Default.fontColor;
            this.cbxRunOnStartup.Checked = Properties.Settings.Default.runOnStartup;
        }

        private void ShowIcon()
        {
            Color bgColor = Properties.Settings.Default.bgColor;
            Color fontColor = Properties.Settings.Default.fontColor;

            string week = GetWeek().ToString();

            using Font font = new Font("Tahoma", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            using Brush brush = new SolidBrush(fontColor);
            using Bitmap bitmap = new Bitmap(16, 16);
            using Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            g.Clear(bgColor);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            g.DrawString(week, font, brush, -1, 0);
            IntPtr hIcon = bitmap.GetHicon();
            using Icon icon = Icon.FromHandle(hIcon);

            this.Icon = icon;
            notifyIcon.Icon = icon;
            notifyIcon.Text = $"Currently it's week {week}";
            notifyIcon.Visible = true;
        }

        private int GetWeek()
        {
            CultureInfo cultureInfo = new CultureInfo("fi-FI");
            Calendar calendar = cultureInfo.Calendar;
            CalendarWeekRule calendarWeekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            return calendar.GetWeekOfYear(DateTime.Now, calendarWeekRule, firstDayOfWeek);
        }

        private void runOnStartup()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
            if (key != null)
            {
                if (this.cbxRunOnStartup.Checked)
                {
                    key.SetValue(StartupValue, Application.StartupPath.ToString() + StartupValue + ".exe");
                    Properties.Settings.Default.runOnStartup = true;
                }
                else
                {
                    key.DeleteValue(StartupValue);
                    Properties.Settings.Default.runOnStartup = false;
                }
            }
            else
                Console.WriteLine("Could not open the sub key");
        }

        private void IconClicked(object sender, EventArgs e)
        {
            allowVisible = true;
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void FormResized(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                Hide();
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
            runOnStartup();
            Properties.Settings.Default.Save();
            ShowIcon();
        }
    }
}
