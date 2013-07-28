#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

#endregion

namespace EasyScope
{
    public class FormSaveChannel : Form
    {
        private ButtonX buttonX1;
        private ButtonX buttonX2;
        private CheckBoxX ch1box;
        private CheckBoxX ch2box;
        private CheckBoxX ch3box;
        private CheckBoxX ch4box;
        private IContainer components;

        public FormSaveChannel()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            var main = FormMain.getstaticmain();
            main.SaveCSVchSelect(true);
            main.SetSaveSelectCH(ch1box.Checked, ch2box.Checked, ch3box.Checked, ch4box.Checked);
            base.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            var main = FormMain.getstaticmain();
            main.SaveCSVchSelect(false);
            main.SetSaveSelectCH(false, false, false, false);
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.ch1box = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ch2box = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ch3box = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ch4box = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // ch1box
            // 
            this.ch1box.AutoSize = true;
            // 
            // 
            // 
            this.ch1box.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ch1box.Location = new System.Drawing.Point(35, 27);
            this.ch1box.Name = "ch1box";
            this.ch1box.Size = new System.Drawing.Size(45, 15);
            this.ch1box.TabIndex = 0;
            this.ch1box.Text = "CH1";
            // 
            // ch2box
            // 
            this.ch2box.AutoSize = true;
            // 
            // 
            // 
            this.ch2box.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ch2box.Location = new System.Drawing.Point(35, 62);
            this.ch2box.Name = "ch2box";
            this.ch2box.Size = new System.Drawing.Size(45, 15);
            this.ch2box.TabIndex = 1;
            this.ch2box.Text = "CH2";
            // 
            // ch3box
            // 
            this.ch3box.AutoSize = true;
            // 
            // 
            // 
            this.ch3box.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ch3box.Location = new System.Drawing.Point(105, 27);
            this.ch3box.Name = "ch3box";
            this.ch3box.Size = new System.Drawing.Size(45, 15);
            this.ch3box.TabIndex = 2;
            this.ch3box.Text = "CH3";
            // 
            // ch4box
            // 
            this.ch4box.AutoSize = true;
            // 
            // 
            // 
            this.ch4box.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ch4box.Location = new System.Drawing.Point(105, 60);
            this.ch4box.Name = "ch4box";
            this.ch4box.Size = new System.Drawing.Size(45, 15);
            this.ch4box.TabIndex = 3;
            this.ch4box.Text = "CH4";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(13, 107);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 4;
            this.buttonX1.Text = "OK";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(94, 107);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.TabIndex = 5;
            this.buttonX2.Text = "Cancel";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // FormSaveChannel
            // 
            this.ClientSize = new System.Drawing.Size(184, 149);
            this.ControlBox = false;
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.ch4box);
            this.Controls.Add(this.ch3box);
            this.Controls.Add(this.ch2box);
            this.Controls.Add(this.ch1box);
            this.Name = "FormSaveChannel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SaveCHSelect";
            this.Shown += new System.EventHandler(this.SaveCHSelect_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SaveCHSelect_Shown(object sender, EventArgs e)
        {
            var main = FormMain.getstaticmain();
            ch1box.Enabled = main.GetRefreshCH1();
            ch2box.Enabled = main.GetRefreshCH2();
            ch3box.Enabled = main.GetRefreshCH3();
            ch4box.Enabled = main.GetRefreshCH4();
        }
    }
}