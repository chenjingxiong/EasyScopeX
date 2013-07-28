#region

using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

#endregion

namespace EasyScope
{
    public class FormConnect : Form
    {
        private ButtonX buttonCancel;
        private ButtonX buttonOK;
        private IContainer components;
        private ListViewEx connect_list;

        public FormConnect()
        {
            InitializeComponent();
        }

        private void add_cancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void add_divece_Click(object sender, EventArgs e)
        {
            var index = 0;
            var scoperesources = "";
            var main = FormMain.getstaticmain();
            var connectManager = ConnectManager.GetConnectManager();
            if (connectManager.GetConnectType() == 1)
            {
                if (connect_list.Items.Count <= 0)
                {
                    main.Updata_UI(0, 0, false);
                    MessageBox.Show("The device is not connected yet，Please connect it first");
                    return;
                }
                if (connect_list.FocusedItem != null)
                {
                    index = connect_list.FocusedItem.Index;
                    scoperesources = connect_list.Items[index].Text;
                }
                else
                {
                    scoperesources = connect_list.Items[index].Text;
                }
            }
            if (scoperesources != "")
            {
                if (connectManager.OpenSession(scoperesources) == -1)
                {
                    MessageBox.Show(
                        "The device can not be open, Please confirm the setting is matching or the cable is connected");
                }
                else
                {
                    if (connectManager.isconnected())
                    {
                        if (connectManager.device_str_op(scoperesources) == -1)
                        {
                            return;
                        }
                        main.add_device_toList();
                        main.Updata_UI(0, 0, true);
                    }
                    else
                    {
                        MessageBox.Show("The device is not connected yet，Please connect it first");
                    }
                    base.Close();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnect));
            this.connect_list = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonOK = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // connect_list
            // 
            this.connect_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.connect_list.Border.Class = "ListViewBorder";
            this.connect_list.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.connect_list.Location = new System.Drawing.Point(14, 14);
            this.connect_list.Margin = new System.Windows.Forms.Padding(5);
            this.connect_list.Name = "connect_list";
            this.connect_list.Size = new System.Drawing.Size(370, 168);
            this.connect_list.TabIndex = 2;
            this.connect_list.UseCompatibleStateImageBehavior = false;
            this.connect_list.View = System.Windows.Forms.View.List;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonCancel.Location = new System.Drawing.Point(309, 192);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.add_cancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonOK.Location = new System.Drawing.Point(224, 192);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(5);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.add_divece_Click);
            // 
            // FormConnect
            // 
            this.ClientSize = new System.Drawing.Size(399, 229);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.connect_list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "FormConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Available devices";
            this.Load += new System.EventHandler(this.USBTMCDeviceDLG_Shown);
            this.ResumeLayout(false);

        }

        private void USBTMCDeviceDLG_Shown(object sender, EventArgs e)
        {
            var num = 0;
            var connectManager = ConnectManager.GetConnectManager();
            if (connectManager.GetConnectType() == 1)
            {
                num = connectManager.FindSrc();
                if (num > 0)
                {
                    for (var i = 0; i < num; i++)
                    {
                        connect_list.Items.Add(connectManager.resources[i]);
                    }
                    buttonOK.Enabled = true;
                }
                else
                {
                    buttonOK.Enabled = false;
                    //MessageBox.Show("Have not find any device.");
                }
            }
        }
    }
}