using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Diagnostics;

namespace EasyScope
{
    public class FormAbout : Form
    {
        private ButtonX buttonX1;
        private IContainer components;
        private LabelX labelX1;
        private LabelX labelX2;
        private GroupBox groupBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox2;
        private FlowLayoutPanel flowLayoutPanel2;
        private LabelX labelX4;
        private LabelX labelX6;
        private LabelX labelX5;
        private LinkLabel linkLabelAboutERW;
        private ListBox listBox1;
        private LabelX labelX3;

        public FormAbout()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.linkLabelAboutERW = new System.Windows.Forms.LinkLabel();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(6, 5);
            this.labelX1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(147, 15);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Software Name: EasyScopeX";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(6, 30);
            this.labelX2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(157, 15);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "Version: V100R001B01D01P08";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(6, 55);
            this.labelX3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(123, 15);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "Copyright (C) 2011-2012";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(187, 325);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.TabIndex = 3;
            this.buttonX1.Text = "&Close";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelX1);
            this.flowLayoutPanel1.Controls.Add(this.labelX2);
            this.flowLayoutPanel1.Controls.Add(this.labelX3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(244, 81);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 199);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enhanced Release for Windows";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.labelX4);
            this.flowLayoutPanel2.Controls.Add(this.labelX5);
            this.flowLayoutPanel2.Controls.Add(this.linkLabelAboutERW);
            this.flowLayoutPanel2.Controls.Add(this.labelX6);
            this.flowLayoutPanel2.Controls.Add(this.listBox1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(244, 180);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(6, 5);
            this.labelX4.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(201, 15);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "Tweaks and enhacements by Erwin Ried";
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.flowLayoutPanel2.SetFlowBreak(this.labelX6, true);
            this.labelX6.Location = new System.Drawing.Point(6, 55);
            this.labelX6.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(95, 15);
            this.labelX6.TabIndex = 2;
            this.labelX6.Text = "Copyright (C) 2013";
            // 
            // linkLabelAboutERW
            // 
            this.linkLabelAboutERW.AutoSize = true;
            this.flowLayoutPanel2.SetFlowBreak(this.linkLabelAboutERW, true);
            this.linkLabelAboutERW.Location = new System.Drawing.Point(63, 30);
            this.linkLabelAboutERW.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.linkLabelAboutERW.Name = "linkLabelAboutERW";
            this.linkLabelAboutERW.Size = new System.Drawing.Size(94, 13);
            this.linkLabelAboutERW.TabIndex = 3;
            this.linkLabelAboutERW.TabStop = true;
            this.linkLabelAboutERW.Text = "http://erwin.ried.cl";
            this.linkLabelAboutERW.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAboutERW_LinkClicked);
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(6, 30);
            this.labelX5.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(45, 15);
            this.labelX5.TabIndex = 2;
            this.labelX5.Text = "Website:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Icons and resources from:",
            "http://icons8.com/",
            "http://iconarchive.com/",
            "",
            "Program icon by:",
            "http://www.iconarchive.com/artist/franksouza183.html"});
            this.listBox1.Location = new System.Drawing.Point(3, 78);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(238, 95);
            this.listBox1.TabIndex = 4;
            // 
            // FormAbout
            // 
            this.ClientSize = new System.Drawing.Size(274, 362);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void linkLabelAboutERW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://erwin.ried.cl");
        }
    }
}