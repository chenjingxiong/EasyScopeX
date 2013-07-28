using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace EasyScope
{
    public partial class FormControlPanel : Form
    {
        public FormControlPanel()
        {
            InitializeComponent();
        }
        
        private void acquire_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 8,1";
            SendSCPIcmd(cmdstr);
        }

        private void allfunction_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 18,0";
            SendSCPIcmd(cmdstr);
        }

        private void allfunctionleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 18,-1";
            SendSCPIcmd(cmdstr);
        }

        private void allfunctionright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 18,1";
            SendSCPIcmd(cmdstr);
        }

        private void auto_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 17,1";
            SendSCPIcmd(cmdstr);
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 19,1";
            SendSCPIcmd(cmdstr);
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 20,1";
            SendSCPIcmd(cmdstr);
        }

        private void buttonX16_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 21,1";
            SendSCPIcmd(cmdstr);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 2,1";
            SendSCPIcmd(cmdstr);
        }

        private void buttonX25_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 1,1";
            SendSCPIcmd(cmdstr);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 3,1";
            SendSCPIcmd(cmdstr);
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 4,1";
            SendSCPIcmd(cmdstr);
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 5,1";
            SendSCPIcmd(cmdstr);
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 0,1";
            SendSCPIcmd(cmdstr);
        }

        private void CH1_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 25,1";
            SendSCPIcmd(cmdstr);
        }

        private void ch1offset_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 33,0";
            SendSCPIcmd(cmdstr);
        }

        private void ch1offsetleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 33,-1";
            SendSCPIcmd(cmdstr);
        }

        private void ch1offsetright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 33,1";
            SendSCPIcmd(cmdstr);
        }

        private void ch1VDIV_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 29,0";
            SendSCPIcmd(cmdstr);
        }

        private void ch1vdivleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 29,-1";
            SendSCPIcmd(cmdstr);
        }

        private void ch1vdivright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 29,1";
            SendSCPIcmd(cmdstr);
        }

        private void CH2_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 26,1";
            SendSCPIcmd(cmdstr);
        }

        private void ch2offset_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 34,0";
            SendSCPIcmd(cmdstr);
        }

        private void ch2offsetleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 34,-1";
            SendSCPIcmd(cmdstr);
        }

        private void ch2offsetright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 34,1";
            SendSCPIcmd(cmdstr);
        }

        private void ch2VDIV_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 30,0";
            SendSCPIcmd(cmdstr);
        }

        private void ch2vdivleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 30,-1";
            SendSCPIcmd(cmdstr);
        }

        private void ch2vdivright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 30,1";
            SendSCPIcmd(cmdstr);
        }

        private void CH3_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 27,1";
            SendSCPIcmd(cmdstr);
        }

        private void ch3offset_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 35,0";
            SendSCPIcmd(cmdstr);
        }

        private void ch3offsetleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 35,-1";
            SendSCPIcmd(cmdstr);
        }

        private void ch3offsetright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 35,1";
            SendSCPIcmd(cmdstr);
        }

        private void ch3VDIV_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 31,0";
            SendSCPIcmd(cmdstr);
        }

        private void ch3vdivleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 31,-1";
            SendSCPIcmd(cmdstr);
        }

        private void ch3vdivright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 31,1";
            SendSCPIcmd(cmdstr);
        }

        private void CH4_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 28,1";
            SendSCPIcmd(cmdstr);
        }

        private void ch4offset_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 36,0";
            SendSCPIcmd(cmdstr);
        }

        private void ch4offsetleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 36,-1";
            SendSCPIcmd(cmdstr);
        }

        private void ch4offsetright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 36,1";
            SendSCPIcmd(cmdstr);
        }

        private void ch4VDIV_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 32,0";
            SendSCPIcmd(cmdstr);
        }

        private void ch4vdivleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 32,-1";
            SendSCPIcmd(cmdstr);
        }

        private void ch4vdivright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 32,1";
            SendSCPIcmd(cmdstr);
        }

        private void CURSORS_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 7,1";
            SendSCPIcmd(cmdstr);
        }

        private void Default_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 10,1";
            SendSCPIcmd(cmdstr);
        }

        private void display_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 12,1";
            SendSCPIcmd(cmdstr);
        }

        private void HELP_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 14,1";
            SendSCPIcmd(cmdstr);
        }

        private void HORI_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 39,1";
            SendSCPIcmd(cmdstr);
        }

        private void horiposleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 38,-1";
            SendSCPIcmd(cmdstr);
        }

        private void horiposright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 38,1";
            SendSCPIcmd(cmdstr);
        }

        private void MATH_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 23,1";
            SendSCPIcmd(cmdstr);
        }

        private void measure_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 11,1";
            SendSCPIcmd(cmdstr);
        }
        
        private void print_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 6,1";
            SendSCPIcmd(cmdstr);
        }

        private void REF_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 24,1";
            SendSCPIcmd(cmdstr);
        }

        private void runstop_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 16,1";
            SendSCPIcmd(cmdstr);
        }

        private void save_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 9,1";
            SendSCPIcmd(cmdstr);
        }

        private void SendSCPIcmd(string cmdstr)
        {
            var connectManager = ConnectManager.GetConnectManager();
            if (connectManager.isconnected())
            {
                connectManager.WriteStrCmd(cmdstr);
            }
            else
            {
                MessageBox.Show("The device is not connected yet，Please connect it first");
            }
        }

        private void single_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 15,1";
            SendSCPIcmd(cmdstr);
        }

        private void TimeBase_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 37,0";
            SendSCPIcmd(cmdstr);
        }

        private void timebaseleft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 37,-1";
            SendSCPIcmd(cmdstr);
        }

        private void timebaseright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 37,1";
            SendSCPIcmd(cmdstr);
        }

        private void triggerlevellesft_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 22,-1";
            SendSCPIcmd(cmdstr);
        }

        private void triggerlevelright_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 22,1";
            SendSCPIcmd(cmdstr);
        }

        private void TriggerPos_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 38,0";
            SendSCPIcmd(cmdstr);
        }

        private void TrigLevel_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 22,0";
            SendSCPIcmd(cmdstr);
        }

        private void uitlity_Click(object sender, EventArgs e)
        {
            var cmdstr = " $$SY_FP 13,1";
            SendSCPIcmd(cmdstr);
        }

        private void Panel_CML_Shown(object sender, EventArgs e)
        {
            if (ConnectManager.GetConnectManager().isconnected())
            {
                switch (FormMain.getstaticmain().GetchNumb())
                {
                    case 2:
                        CH3.Enabled = false;
                        ch3offset.Enabled = false;
                        ch3offsetleft.Enabled = false;
                        ch3VDIV.Enabled = false;
                        ch3vdivleft.Enabled = false;
                        ch3offsetright.Enabled = false;
                        ch3vdivright.Enabled = false;
                        CH4.Enabled = false;
                        ch4offset.Enabled = false;
                        ch4offsetleft.Enabled = false;
                        ch4VDIV.Enabled = false;
                        ch4vdivleft.Enabled = false;
                        ch4offsetright.Enabled = false;
                        ch4vdivright.Enabled = false;
                        return;

                    case 4:
                        CH3.Enabled = true;
                        ch3offset.Enabled = true;
                        ch3offsetleft.Enabled = true;
                        ch3VDIV.Enabled = true;
                        ch3vdivleft.Enabled = true;
                        ch3offsetright.Enabled = true;
                        ch3vdivright.Enabled = true;
                        CH4.Enabled = true;
                        ch4offset.Enabled = true;
                        ch4offsetleft.Enabled = true;
                        ch4VDIV.Enabled = true;
                        ch4vdivleft.Enabled = true;
                        ch4offsetright.Enabled = true;
                        ch4vdivright.Enabled = true;
                        break;
                }
            }
        }

    }
}