#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace EasyScope
{
    public class FormNetwork : Form
    {
        private Button button1;
        private Button button2;
        private IContainer components;
        private Label label1;
        private TextBox textBox1;

        public FormNetwork()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var scoperesources = "";
            var main = FormMain.getstaticmain();
            var connectManager = ConnectManager.GetConnectManager();
            var str3 = textBox1.Text.Trim();
            var startIndex = -1;
            for (startIndex = str3.IndexOf(' '); startIndex != -1; startIndex = str3.IndexOf(' '))
            {
                str3 = str3.Remove(startIndex, 1);
                startIndex = 0;
            }
            scoperesources = "TCPIP0::" + str3 + "::inst0::INSTR";
            if (scoperesources != "")
            {
                if (connectManager.OpenSession(scoperesources) == -1)
                {
                    MessageBox.Show("The device can not be open, Please confirm the IP address");
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
                    base.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNetwork));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter network address of device:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 20);
            this.textBox1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormNetwork
            // 
            this.ClientSize = new System.Drawing.Size(186, 89);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNetwork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VXI11Dialog";
            this.Shown += new System.EventHandler(this.VXI11Dialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void VXI11Dialog_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}