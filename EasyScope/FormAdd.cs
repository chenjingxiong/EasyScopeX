#region

using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

#endregion

namespace EasyScope
{
    public class FormAdd : Form
    {
        private ButtonX TCP_IP;
        private ButtonX USBRAW;
        private ButtonX USMTMC;
        private ButtonX buttonCancel;
        private IContainer components;

        public FormAdd()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            var scoperesources = "USB0::0xF4EC::0xEE3A::1020-1062-6003-00::INSTR";
            if (ConnectManager.GetConnectManager().OpenSession(scoperesources) == -1)
            {
                MessageBox.Show(
                    "The device can not be open, Please confirm the setting is matching or the cable is connected");
            }
            else
            {
                base.Hide();
                base.Close();
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdd));
            this.TCP_IP = new DevComponents.DotNetBar.ButtonX();
            this.USMTMC = new DevComponents.DotNetBar.ButtonX();
            this.USBRAW = new DevComponents.DotNetBar.ButtonX();
            this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // TCP_IP
            // 
            this.TCP_IP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.TCP_IP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.TCP_IP.Location = new System.Drawing.Point(243, 12);
            this.TCP_IP.Name = "TCP_IP";
            this.TCP_IP.Size = new System.Drawing.Size(109, 42);
            this.TCP_IP.TabIndex = 3;
            this.TCP_IP.Text = "VXI11";
            this.TCP_IP.Click += new System.EventHandler(this.TCP_IP_Click);
            // 
            // USMTMC
            // 
            this.USMTMC.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.USMTMC.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.USMTMC.Location = new System.Drawing.Point(13, 12);
            this.USMTMC.Name = "USMTMC";
            this.USMTMC.Size = new System.Drawing.Size(109, 42);
            this.USMTMC.TabIndex = 1;
            this.USMTMC.Text = "USBTMC";
            this.USMTMC.Click += new System.EventHandler(this.USMTMC_Click);
            // 
            // USBRAW
            // 
            this.USBRAW.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.USBRAW.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.USBRAW.Location = new System.Drawing.Point(128, 12);
            this.USBRAW.Name = "USBRAW";
            this.USBRAW.Size = new System.Drawing.Size(109, 42);
            this.USBRAW.TabIndex = 2;
            this.USBRAW.Text = "RS-232";
            this.USBRAW.Click += new System.EventHandler(this.RS232_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonCancel.Location = new System.Drawing.Point(277, 68);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormAdd
            // 
            this.ClientSize = new System.Drawing.Size(365, 105);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.TCP_IP);
            this.Controls.Add(this.USBRAW);
            this.Controls.Add(this.USMTMC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Connection";
            this.ResumeLayout(false);

        }

        private void RS232_Click(object sender, EventArgs e)
        {
            /*base.Hide();
            base.Close();
            base.Update();*/
            Close();
            var rsdlg = new FormSerial();
            rsdlg.ShowDialog();
            rsdlg.Update();
        }

        private void TCP_IP_Click(object sender, EventArgs e)
        {
            /*base.Hide();
            base.Close();
            base.Update();*/
            Close();
            ConnectManager.GetConnectManager().SetConnecType(2);
            var dialog = new FormNetwork();
            dialog.ShowDialog();
            dialog.Update();
        }

        private void USMTMC_Click(object sender, EventArgs e)
        {
            /*base.Hide();
            base.Close();
            base.Update();*/
            Close();
            ConnectManager.GetConnectManager().SetConnecType(1);
            var edlg = new FormConnect();
            edlg.ShowDialog();
            edlg.Update();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}