using AxCWUIControlsLib;
using AxCWUIControlsLib;
using CWUIControlsLib;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevComponents.DotNetBar.Rendering;
using DevComponents.Editors;
using DevComponents.Editors;
using EasyScope.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms;
using ColumnHeader = System.Windows.Forms.ColumnHeader;
using TabControl = System.Windows.Forms.TabControl;
using Timer = System.Timers.Timer;

namespace EasyScope
{
    public partial class FormMain : RibbonForm
    {
        private readonly PrintDocument printbmpDoc, printDoc;
        private bool bautobmpcheck, bAutoRefeshTrace, bAutoRefreshBMP;
        private bool bch1Refresh, bCH1save, bch2Refresh, bCH2save, bch3Refresh, bCH3save, bch4Refresh;
        private bool bCH4save, bRefreshTrace, bsaveoperation, bSeccessfulRefreshBMP;
        private byte[] Bmpbuff, Configbuff, databuff, waveformbuff;
        private float CH1maxamp, CH2maxamp, CH3maxamp, CH4maxamp;
        private float cursorY1, cursorY2, cursorY3, cursorY4;
        private float maxamp;
        private float yposzoommax, yposzoommin;
        private int connectDeviceCHnumbs;
        private int cursorX1, cursorX2;
        private int selectCHindex;
        private int xposzoommax, xposzoommin;
        private static CWave CH2wave, CH3wave, CH4wave, CurrentWave;
        private static string currentch = "";
        private static Timer mytime, mytime2;
        public bool bhavedevice;
        public static FormMain mainfrm_static;
        private static CWave CH1wave;

        public FormMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            mainfrm_static = this;
            bautobmpcheck = false;
            bhavedevice = false;
            Remove.Enabled = false;
            DisConnect.Enabled = false;
            Connect.Enabled = false;
            buttonItem29.Enabled = false;
            bsaveoperation = false;
            bCH1save = false;
            bCH2save = false;
            bCH3save = false;
            bCH4save = false;
            bch1Refresh = false;
            bch2Refresh = false;
            bch3Refresh = false;
            bch4Refresh = false;
            bSeccessfulRefreshBMP = false;
            bRefreshTrace = false;
            bAutoRefreshBMP = false;
            bAutoRefeshTrace = false;
            CH1wave = CWave.Getwave();
            CH2wave = CWave.Getwave();
            CH3wave = CWave.Getwave();
            CH4wave = CWave.Getwave();
            CurrentWave = CH1wave;
            SetBufferSize();
            ribbonTabItem1.Select();
            printDoc = new PrintDocument();
            printDoc.PrintPage += printDocument_PrintPage;
            printbmpDoc = new PrintDocument();
            printbmpDoc.PrintPage += printDocument_PrintPage2;
            mytime = new Timer();
            mytime.Elapsed += theout;
            mytime2 = new Timer();
            mytime2.Elapsed += AutoRefreshTace;

            // Style manager
            LoadApplicationStyle();
        }

        #region "Style and UI Color"

        private void LoadApplicationStyle()
        {
            // Color
            ChangeAccentColor(Settings.Default.AccentColor, false);
            ChangeApplicationStyle(Settings.Default.AppStyle, false);

            // Remove current options
            RebuildApplicationStyleMenu();
        }

        private void ChangeAccentColor(Color color, bool save = true)
        {
            this.styleManagerMain.MetroColorParameters =
                new MetroColorGeneratorParameters(
                    this.styleManagerMain.MetroColorParameters.CanvasColor,
                    color);

            if (save)
            {
                Settings.Default.AccentColor = color;
                Settings.Default.Save();
            }
        }

        private void colorPickerDropDownInterface_SelectedColorChanged(object sender, EventArgs e)
        {
            ChangeAccentColor(colorPickerDropDownInterface.SelectedColor);
        }

        private void colorPickerDropDownInterface_ColorPreview(object sender, ColorPreviewEventArgs e)
        {
            ChangeAccentColor(e.Color, false);
        }

        private void colorPickerDropDownInterface_PopupClose(object sender, EventArgs e)
        {
            ChangeAccentColor(Settings.Default.AccentColor);
        }

        private void RebuildApplicationStyleMenu()
        {
            // Remove older
            while (buttonItemStyle.SubItems.Count > 1)
                buttonItemStyle.SubItems.RemoveAt(1);

            var i = 0;
            var prev = "";

            foreach (eStyle style in Enum.GetValues(typeof (eStyle)))
            {
                var n = style.ToString();

                if (n != prev)
                {
                    var b = new ButtonItem("style" + i, GetPrettyStyleName(n)) {BeginGroup = i == 0, Tag = style};

                    if (style.Equals(Settings.Default.AppStyle))
                        b.Checked = true;

                    var styleInstance = style;
                    b.Click += (o, args) => ChangeApplicationStyle(styleInstance);

                    i++;
                    buttonItemStyle.SubItems.Add(b);
                }
                prev = n;
            }
        }


        private static string GetPrettyStyleName(string toString)
        {
            var s = "";
            var last = 'a';

            foreach (var c in toString.ToCharArray())
            {
                if (last >= 'a' && last <= 'z')
                {
                    if ((c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
                        s += " ";

                }
                else
                {
                    if (last >= '0' && last <= '9')
                    {
                        if (c >= 'A' && c <= 'Z')
                            s += " ";

                    }
                }

                s += c;
                last = c;
            }

            return s;
        }

        private void ChangeApplicationStyle(eStyle appStyle, bool save = true)
        {
            this.styleManagerMain.ManagerStyle = appStyle;

            if (save)
            {
                Settings.Default.AppStyle = appStyle;
                Settings.Default.Save();
            }

            RebuildApplicationStyleMenu();
        }

        #endregion

        private void About_Click(object sender, EventArgs e)
        {
            new FormAbout().Show();
        }

        public void add_device_toList()
        {
            var scpoe = MyScope.Getscpoe();
            var device = scpoe.GetDevice();
            var status = scpoe.Getstatus();
            var bus = scpoe.Getbus();
            var address = scpoe.Getaddress();
            var nO = scpoe.GetNO();
            treeView1.Visible = true;
            var item = new ListViewItem(device);
            item.SubItems.Add(status);
            item.SubItems.Add(bus);
            item.SubItems.Add(address);
            item.SubItems.Add(nO);
            DevicelistView.Items.Add(item);
            DevicelistView.Update();
            var count = DevicelistView.Items.Count;
            if (count != 0)
            {
                DevicelistView.Items[count - 1].Selected = true;
                DevicelistView.Items[count - 1].SubItems[0].ForeColor = Color.Blue;
                for (var i = 0; i < (count - 1); i++)
                {
                    DevicelistView.Items[i].Selected = false;
                    DevicelistView.Items[i].SubItems[0].ForeColor = Color.Black;
                }
            }
            treeView1.Nodes[0].Text = address;
            treeView1.ExpandAll();
            updata_CH_list(device);
            base.Update();
        }

        public void AddItemToList()
        {
            DevicelistView.Items.Add("aaaaaaaaaa");
        }

        private void Addsetupfile_toList(string filepathname)
        {
            var num = filepathname.LastIndexOf('\\');
            var text = filepathname.Substring(num + 1);
            var str2 = filepathname.Replace(@"\" + text, "");
            var str3 = DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            var item = new ListViewItem(text);
            item.SubItems.Add(str3);
            item.SubItems.Add(str2);
            SetupListView.Items.Add(item);
            SetupListView.Update();
            SetupListView.Items[0].Selected = true;
        }

        private string AscllToChar(byte asciiCode)
        {
            if ((asciiCode < 0) || (asciiCode > 0xff))
            {
                throw new Exception("ASCII Code is not valid.");
            }
            var encoding = new ASCIIEncoding();
            var bytes = new[] {asciiCode};
            return encoding.GetString(bytes);
        }

        public void AutoRefreshTace(object source, ElapsedEventArgs e)
        {
            var flag = false;
            var flag2 = false;
            var flag3 = false;
            var flag4 = false;
            flag = CH1.Checked;
            flag2 = CH2.Checked;
            flag3 = CH3.Checked;
            flag4 = CH4.Checked;
            if ((flag || flag2) || (flag3 || flag4))
            {
                if (flag)
                {
                    reflash_waveform("C1", 1);
                    bch1Refresh = true;
                }
                if (flag2)
                {
                    reflash_waveform("C2", 2);
                    bch2Refresh = true;
                }
                if (flag3)
                {
                    reflash_waveform("C3", 3);
                    bch3Refresh = true;
                }
                if (flag4)
                {
                    reflash_waveform("C4", 4);
                    bch4Refresh = true;
                }
            }
        }

        private void AutoRefreshTrace_CheckedBindableChanged(object sender, EventArgs e)
        {
            if (AutoRefreshTrace.Checked)
            {
                if (checkBoxItem3.Checked)
                {
                    checkBoxItem3.Checked = false;
                    bautobmpcheck = false;
                    bAutoRefreshBMP = false;
                }
                bAutoRefeshTrace = true;
                if (AutoTracecomboBox.Items.Count > 0)
                {
                    switch (AutoTracecomboBox.SelectedIndex)
                    {
                        case 0:
                            mytime2.AutoReset = true;
                            mytime2.Enabled = true;
                            mytime2.Interval = 3000.0;
                            mytime2.Start();
                            return;

                        case 1:
                            mytime2.AutoReset = true;
                            mytime2.Enabled = true;
                            mytime2.Interval = 5000.0;
                            mytime2.Start();
                            return;

                        case 2:
                            mytime2.AutoReset = true;
                            mytime2.Enabled = true;
                            mytime2.Interval = 10000.0;
                            mytime2.Start();
                            return;

                        case 3:
                            mytime2.AutoReset = true;
                            mytime2.Enabled = true;
                            mytime2.Interval = 30000.0;
                            mytime2.Start();
                            return;

                        case 4:
                            mytime2.AutoReset = true;
                            mytime2.Enabled = true;
                            mytime2.Interval = 60000.0;
                            mytime2.Start();
                            return;
                    }
                }
            }
            else
            {
                mytime2.AutoReset = false;
                mytime2.Enabled = false;
                mytime2.Stop();
                mytime2.Close();
                bAutoRefeshTrace = false;
                bRefreshTrace = true;
                CurscheckBox.Enabled = true;
                TraceProt.Enabled = true;
                paramCHselect.Enabled = true;
            }
        }

        private void AutoTracecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AutoRefreshTrace.Checked && (AutoTracecomboBox.Items.Count > 0))
            {
                switch (AutoTracecomboBox.SelectedIndex)
                {
                    case 0:
                        mytime2.AutoReset = true;
                        mytime2.Enabled = true;
                        mytime2.Interval = 3000.0;
                        mytime2.Start();
                        return;

                    case 1:
                        mytime2.AutoReset = true;
                        mytime2.Enabled = true;
                        mytime2.Interval = 5000.0;
                        mytime2.Start();
                        return;

                    case 2:
                        mytime2.AutoReset = true;
                        mytime2.Enabled = true;
                        mytime2.Interval = 10000.0;
                        mytime2.Start();
                        return;

                    case 3:
                        mytime2.AutoReset = true;
                        mytime2.Enabled = true;
                        mytime2.Interval = 30000.0;
                        mytime2.Start();
                        return;

                    case 4:
                        mytime2.AutoReset = true;
                        mytime2.Enabled = true;
                        mytime2.Interval = 60000.0;
                        mytime2.Start();
                        break;
                }
            }
        }

        private void bar3_SizeChanged(object sender, EventArgs e)
        {

        }

        private void bar3_VisibleChanged(object sender, EventArgs e)
        {
            e.ToString();
        }

        private void bmp_reflesh_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                tabControl1.SelectTab("BmpPage");
                reflesh_bmp();
                if (bSeccessfulRefreshBMP)
                {
                    buttonItem27.Enabled = true;
                    buttonItem31.Enabled = true;
                    buttonItem32.Enabled = true;
                    buttonItem33.Enabled = true;
                }
            }
        }

        private Bitmap bmp_resize(Bitmap bmp, int with, int high)
        {
            var width = BmpPage.Width;
            var height = BmpPage.Height;
            var image = new Bitmap(bmp, width, height);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(image, width, height);
                graphics.Dispose();
                return image;
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            if (SetupListView.Items.Count > 0)
            {
                SetupListView.Items.Clear();
            }
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("SCPI_CMD");
            richTextBox1.Focus();
            checkBoxItem5.Checked = true;
            checkBoxItem6.Checked = false;
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var itemindex = -1;
                if (DevicelistView.Items.Count > 0)
                {
                    if (DevicelistView.FocusedItem != null)
                    {
                        itemindex = DevicelistView.FocusedItem.Index;
                    }
                    else
                    {
                        itemindex = 0;
                    }
                    var connectManager = ConnectManager.GetConnectManager();
                    if (connectManager.isconnected())
                    {
                        if (connectManager.CloseSession() == -1)
                        {
                            MessageBox.Show("The device disconnected has meet problem");
                        }
                        else
                        {
                            MyScope.Getscpoe().Setstatus("Dead");
                            updata_list_item(itemindex);
                        }
                    }
                }
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            bar1.AutoHide = false;
        }

        private void buttonItem22_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                if (ConnectManager.GetConnectManager().isconnected())
                {
                    if (currentch == "Channel1")
                    {
                        reflash_waveform("C1", 1);
                        selectCHindex = 1;
                        paramCHselect.SelectedIndex = 0;
                        bch1Refresh = true;
                    }
                    else if (currentch == "Channel2")
                    {
                        reflash_waveform("C2", 2);
                        selectCHindex = 2;
                        paramCHselect.SelectedIndex = 1;
                        bch2Refresh = true;
                    }
                    else if (currentch == "Channel3")
                    {
                        reflash_waveform("C3", 3);
                        selectCHindex = 3;
                        paramCHselect.SelectedIndex = 2;
                        bch3Refresh = true;
                    }
                    else if (currentch == "Channel4")
                    {
                        reflash_waveform("C4", 4);
                        selectCHindex = 4;
                        paramCHselect.SelectedIndex = 3;
                        bch4Refresh = true;
                    }
                    else
                    {
                        reflash_waveform("C1", 1);
                        selectCHindex = 1;
                        paramCHselect.SelectedIndex = 0;
                        bch1Refresh = true;
                    }
                    bRefreshTrace = true;
                }
                else
                {
                    MessageBox.Show("The device is not connected yet，Please connect it first");
                }
            }
        }

        private void buttonItem24_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var select = new FormSaveChannel();
                select.ShowDialog();
                select.Update();
                if (bsaveoperation && ((bCH1save || bCH2save) || (bCH3save || bCH4save)))
                {
                    var dialog = new SaveFileDialog
                                     {
                                         Filter = "csv files (*.csv)|*.csv|bin files(*.bin)|*.bin",
                                         RestoreDirectory = true
                                     };
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var filterIndex = dialog.FilterIndex;
                        var fileName = dialog.FileName;
                        var xposzoommax = this.xposzoommax;
                        switch (filterIndex)
                        {
                            case 1:
                                {
                                    var writer = new StreamWriter(fileName, false, Encoding.Default);
                                    var str2 = "";
                                    var str3 = "";
                                    var str4 = "";
                                    var str5 = "";
                                    var str6 = "";
                                    var str7 = "";
                                    var str8 = "";
                                    str7 = DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                                    str7 = "Save time," + str7;
                                    writer.WriteLine(str7);
                                    str8 = "Channel,CH1,CH2,CH3,CH4";
                                    writer.WriteLine(str8);
                                    str6 = "point,value";
                                    writer.WriteLine(str6);
                                    for (var i = 0; i < xposzoommax; i++)
                                    {
                                        if (bCH1save)
                                        {
                                            str2 = i.ToString() + "," + CH1wave.wavedata[i].ToString();
                                        }
                                        if (bCH2save)
                                        {
                                            if (bCH1save)
                                            {
                                                str3 = "," + CH2wave.wavedata[i].ToString();
                                            }
                                            else
                                            {
                                                str3 = i.ToString() + ",," + CH2wave.wavedata[i].ToString();
                                            }
                                        }
                                        if (bCH3save)
                                        {
                                            if (bCH1save || bCH2save)
                                            {
                                                if (bCH2save)
                                                {
                                                    str4 = "," + CH3wave.wavedata[i].ToString();
                                                }
                                                else if (bCH1save)
                                                {
                                                    str4 = ",," + CH3wave.wavedata[i].ToString();
                                                }
                                            }
                                            else
                                            {
                                                str4 = i.ToString() + ",,," + CH3wave.wavedata[i].ToString();
                                            }
                                        }
                                        if (bCH4save)
                                        {
                                            if ((bCH1save || bCH2save) || bCH3save)
                                            {
                                                if (bCH3save)
                                                {
                                                    str5 = "," + CH4wave.wavedata[i].ToString();
                                                }
                                                else if (bCH2save)
                                                {
                                                    str5 = ",," + CH4wave.wavedata[i].ToString();
                                                }
                                                else if (bCH1save)
                                                {
                                                    str5 = ",,," + CH4wave.wavedata[i].ToString();
                                                }
                                            }
                                            else
                                            {
                                                str4 = i.ToString() + ",,,," + CH4wave.wavedata[i].ToString();
                                            }
                                        }
                                        str6 = str2 + str3 + str4 + str5;
                                        writer.WriteLine(str6);
                                    }
                                    writer.Flush();
                                    writer.Close();
                                    return;
                                }
                            case 2:
                                {
                                    var output = new FileStream(fileName, FileMode.Create);
                                    var writer2 = new BinaryWriter(output);
                                    if (databuff.Length > 0)
                                    {
                                        writer2.Write(databuff, 0, xposzoommax);
                                        writer2.Flush();
                                        writer2.Close();
                                        output.Close();
                                        return;
                                    }
                                    MessageBox.Show("There is no waveform");
                                    break;
                                }
                        }
                    }
                }
            }
        }

        private void buttonItem25_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var width = WGraph.Width;
                var height = WGraph.Height;
                var bitmap = new Bitmap(width, height);
                using (var image = WGraph.ControlImage())
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.DrawImage(image, new Rectangle(0, 0, width, height));
                        Clipboard.SetDataObject(bitmap, true);
                        bitmap.Dispose();
                        graphics.Dispose();
                    }
                }
            }
        }

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("Setups");
            var dialog = new FolderBrowserDialog
                             {
                                 Description = "Please select the path"
                             };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var str2 in Directory.GetFiles(dialog.SelectedPath, "*.lss", SearchOption.TopDirectoryOnly))
                {
                    var info = new FileInfo(str2);
                    var name = info.Name;
                    var text = info.LastWriteTime.ToString();
                    var directoryName = info.DirectoryName;
                    var item = new ListViewItem(name);
                    item.SubItems.Add(text);
                    item.SubItems.Add(directoryName);
                    SetupListView.Items.Add(item);
                    SetupListView.Update();
                }
            }
        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                Clipboard.SetDataObject(pictureBox1.Image, true);
            }
        }

        private void buttonItem29_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                new FormControlPanel().ShowDialog();
            }
        }

        private void buttonItem31_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var dialog = new SaveFileDialog
                                 {
                                     Filter = "BMP files (*.bmp)|*.bmp",
                                     RestoreDirectory = true
                                 };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Stream stream;
                    if (((stream = dialog.OpenFile()) != null) && (Bmpbuff.Length > 0))
                    {
                        stream.Write(Bmpbuff, 0, Bmpbuff.Length);
                        stream.Close();
                    }
                }
            }
        }

        private void buttonItem32_Click(object sender, EventArgs e)
        {
            if (CheckDeviceStatus()) return;

            var dialog = new PrintDialog
                             {
                                 Document = printbmpDoc
                             };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                printbmpDoc.Print();
            }
        }

        private void buttonItem33_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var dialog = new PrintPreviewDialog
                                 {
                                     Document = printbmpDoc
                                 };
                try
                {
                    dialog.ShowDialog();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void buttonItem34_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var dialog = new PrintDialog
                                 {
                                     Document = printDoc
                                 };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
        }

        private void buttonItem35_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var dialog = new PrintPreviewDialog
                                 {
                                     Document = printDoc
                                 };
                try
                {
                    dialog.ShowDialog();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void buttonItem36_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var connectManager = ConnectManager.GetConnectManager();
                var flag = connectManager.isconnected();
                var connectType = connectManager.GetConnectType();
                if (flag)
                {
                    const string cmd = "PNSU?";
                    connectManager.WriteStrCmd(cmd);
                    if (connectType == 0)
                    {
                        connectManager.Delay(0x4b0);
                    }
                    var filelenth = connectManager.ReadUsbtmcSetup(ref Configbuff);
                    SaveSetupFile(ref Configbuff, filelenth);
                }
                else
                {
                    MessageBox.Show("The device is not connected yet，Please connect it first");
                }
            }
        }

        private void buttonItem37_Click_1(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var index = -1;
                if ((SetupListView.Items.Count > 0) && (SetupListView.Items[0].Text != ""))
                {
                    if (SetupListView.FocusedItem != null)
                    {
                        index = SetupListView.FocusedItem.Index;
                    }
                    else
                    {
                        index = 0;
                    }
                    var filename = SetupListView.Items[0].SubItems[2].Text + @"\" + SetupListView.Items[index].Text;
                    OpenFile_AndSend(filename);
                }
            }
        }

        private void buttonItem38_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("Setups");
            var dialog = new OpenFileDialog
                             {
                                 Filter = "setup files (*.lss)|*.lss",
                                 RestoreDirectory = true
                             };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var info = new FileInfo(dialog.FileName);
                var name = info.Name;
                var text = info.LastWriteTime.ToString();
                var directoryName = info.DirectoryName;
                var item = new ListViewItem(name);
                item.SubItems.Add(text);
                item.SubItems.Add(directoryName);
                SetupListView.Items.Add(item);
                SetupListView.Update();
            }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var dialog = new FormAdd();
                dialog.ShowDialog();
                dialog.Update();
            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var index = -1;
                if (DevicelistView.FocusedItem != null)
                {
                    index = DevicelistView.FocusedItem.Index;
                }
                else
                {
                    index = 0;
                }
                if (DevicelistView.Items.Count > 0)
                {
                    if (DevicelistView.Items.Count == 1)
                    {
                        treeView1.Visible = false;
                        Remove.Enabled = false;
                        DisConnect.Enabled = false;
                        Connect.Enabled = false;
                    }
                    DevicelistView.Items[index].Remove();
                    DevicelistView.Update();
                }
            }
        }

        private WD_PARAM ByteTostruct(ref byte[] prambuff)
        {
            var cb = Marshal.SizeOf(typeof (WD_PARAM));
            var destination = Marshal.AllocHGlobal(cb);
            Marshal.Copy(prambuff, 0, destination, cb);
            var wd_param = (WD_PARAM) Marshal.PtrToStructure(destination, typeof (WD_PARAM));
            Marshal.FreeHGlobal(destination);
            return wd_param;
        }

        private void checkBoxItem3_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            if (checkBoxItem3.Checked)
            {
                if (AutoRefreshTrace.Checked)
                {
                    AutoRefreshTrace.Checked = false;
                    bAutoRefeshTrace = false;
                }
                bautobmpcheck = true;
                bAutoRefreshBMP = true;
                if (comboBoxItem2.Items.Count > 0)
                {
                    switch (comboBoxItem2.SelectedIndex)
                    {
                        case 0:
                            mytime.AutoReset = true;
                            mytime.Enabled = true;
                            mytime.Interval = 1000.0;
                            mytime.Start();
                            return;

                        case 1:
                            mytime.AutoReset = true;
                            mytime.Enabled = true;
                            mytime.Interval = 2000.0;
                            mytime.Start();
                            return;

                        case 2:
                            mytime.AutoReset = true;
                            mytime.Enabled = true;
                            mytime.Interval = 5000.0;
                            mytime.Start();
                            return;

                        case 3:
                            mytime.AutoReset = true;
                            mytime.Enabled = true;
                            mytime.Interval = 10000.0;
                            mytime.Start();
                            return;

                        case 4:
                            mytime.AutoReset = true;
                            mytime.Enabled = true;
                            mytime.Interval = 120000.0;
                            mytime.Start();
                            return;

                        case 5:
                            mytime.AutoReset = true;
                            mytime.Enabled = true;
                            mytime.Interval = 300000.0;
                            mytime.Start();
                            return;
                    }
                }
            }
            else
            {
                mytime.AutoReset = false;
                mytime.Enabled = false;
                mytime.Stop();
                mytime.Close();
                bautobmpcheck = false;
                bAutoRefreshBMP = false;
            }
        }

        private void checkBoxItem5_Click(object sender, EventArgs e)
        {
            checkBoxItem5.Checked = true;
            checkBoxItem6.Checked = false;
        }

        private void checkBoxItem6_Click(object sender, EventArgs e)
        {
            checkBoxItem6.Checked = true;
            checkBoxItem5.Checked = false;
        }

        private void checkBoxItem9_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                if (TraceProt.Checked)
                {
                    Propritybar.AutoHide = false;
                    var selectedIndex = paramCHselect.SelectedIndex;
                    updata_proprites(selectedIndex + 1);
                }
                else
                {
                    Propritybar.AutoHide = true;
                }
            }
        }

        private bool CheckDeviceStatus()
        {
            var flag = false;
            if (bAutoRefeshTrace || bAutoRefreshBMP)
            {
                flag = true;
                MessageBox.Show("Right now the device is in auto refresh mode, please stop it first.");
            }
            return flag;
        }

        private void comboBoxItem2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bautobmpcheck)
            {
                switch (comboBoxItem2.SelectedIndex)
                {
                    case 0:
                        mytime.AutoReset = true;
                        mytime.Enabled = true;
                        mytime.Interval = 1000.0;
                        mytime.Start();
                        return;

                    case 1:
                        mytime.AutoReset = true;
                        mytime.Enabled = true;
                        mytime.Interval = 2000.0;
                        mytime.Start();
                        return;

                    case 2:
                        mytime.AutoReset = true;
                        mytime.Enabled = true;
                        mytime.Interval = 5000.0;
                        mytime.Start();
                        return;

                    case 3:
                        mytime.AutoReset = true;
                        mytime.Enabled = true;
                        mytime.Interval = 10000.0;
                        mytime.Start();
                        return;

                    case 4:
                        mytime.AutoReset = true;
                        mytime.Enabled = true;
                        mytime.Interval = 120000.0;
                        mytime.Start();
                        return;

                    case 5:
                        mytime.AutoReset = true;
                        mytime.Enabled = true;
                        mytime.Interval = 300000.0;
                        mytime.Start();
                        break;
                }
            }
        }

        private void commandTheme_Executed(object sender, EventArgs e)
        {
            var source = sender as ICommandSource;
            if (source.CommandParameter is string)
            {
                var scheme =
                    (eOffice2007ColorScheme)
                    Enum.Parse(typeof (eOffice2007ColorScheme), source.CommandParameter.ToString());
                ribbonControl1.Office2007ColorTable = scheme;
            }
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                var itemindex = -1;
                var scoperesources = "";
                if (DevicelistView.Items.Count > 0)
                {
                    if (DevicelistView.FocusedItem != null)
                    {
                        itemindex = DevicelistView.FocusedItem.Index;
                    }
                    else
                    {
                        itemindex = 0;
                    }
                    scoperesources = DevicelistView.Items[itemindex].SubItems[3].Text;
                    if (scoperesources != "")
                    {
                        if (ConnectManager.GetConnectManager().OpenSession(scoperesources) == -1)
                        {
                            MessageBox.Show(
                                "The device can not be open, Please confirm the setting is matching or the cable is connected");
                        }
                        else
                        {
                            MyScope.Getscpoe().Setstatus("Alive");
                            updata_list_item(itemindex);
                        }
                    }
                }
            }
        }

        private void CurscheckBox_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                if (!CurscheckBox.Checked)
                {
                    bar3.AutoHide = true;
                    Resetcurpos.Enabled = false;
                    WGraph.Cursors.Item(1).Visible = false;
                    WGraph.Cursors.Item(2).Visible = false;
                    WGraph.TrackMode = CWGraphTrackModes.cwGTrackAllEvents;
                }
                else
                {
                    bar3.AutoHide = false;
                    Resetcurpos.Enabled = true;
                    CWave wave = null;
                    switch ((paramCHselect.SelectedIndex + 1))
                    {
                        case 1:
                            wave = CH1wave;
                            break;

                        case 2:
                            wave = CH2wave;
                            break;

                        case 3:
                            wave = CH3wave;
                            break;

                        case 4:
                            wave = CH4wave;
                            break;
                    }
                    if (wave != null)
                    {
                        WGraph.Cursors.Item(1).Visible = true;
                        WGraph.Cursors.Item(2).Visible = true;
                        WGraph.Cursors.Item(1).XPosition = ((xposzoommax - xposzoommin)/3) + xposzoommin;
                        WGraph.Cursors.Item(2).XPosition = xposzoommax - ((xposzoommax - xposzoommin)/3);
                        WGraph.Cursors.Item(1).YPosition = 0;
                        WGraph.Cursors.Item(2).YPosition = 0;
                        cursorX1 = ((xposzoommax - xposzoommin)/3) + xposzoommin;
                        cursorX2 = xposzoommax - ((xposzoommax - xposzoommin)/3);
                        cursorY1 = wave.wavedata[cursorX1];
                        cursorY2 = wave.wavedata[cursorX2];
                        textBoxX5.Text = cursorX1.ToString();
                        textBoxX6.Text = wave.wavedata[cursorX1].ToString("F3") + "v";
                        textBoxX7.Text = cursorX2.ToString();
                        textBoxX8.Text = wave.wavedata[cursorX2].ToString("F3") + "v";
                        textBoxX9.Text = (cursorX2 - cursorX1).ToString();
                        textBoxX10.Text = ((wave.wavedata[cursorX2] - wave.wavedata[cursorX1])).ToString("F3") + "v";
                        textBoxX13.Text = "0.0v";
                        textBoxX12.Text = "0.0v";
                        textBoxX11.Text = "0.0v";
                        var selectedIndex = paramCHselect.SelectedIndex;
                        Updata_measur(selectedIndex + 1);
                        WGraph.TrackMode = CWGraphTrackModes.cwGTrackDragCursor;
                    }
                }
            }
        }

        private void cursor_view_group_autosize()
        {
            groupPanel3.Width = (groupPanel1.Width/2) - 10;
            groupPanel4.Left = groupPanel3.Width + 20;
            groupPanel4.Width = (groupPanel1.Width/2) - 10;
            labelX1.Width = (int) ((groupPanel3.Width*39.0)/269.0);
            textBoxX5.Left = labelX1.Right + 9;
            textBoxX5.Width = (int) ((groupPanel3.Width*70.0)/269.0);
            labelX2.Left = textBoxX5.Right + 9;
            labelX2.Width = (int) ((groupPanel3.Width*39.0)/269.0);
            textBoxX6.Left = labelX2.Right + 9;
            textBoxX6.Width = (int) ((groupPanel3.Width*70.0)/269.0);
            labelX3.Width = (int) ((groupPanel3.Width*39.0)/269.0);
            textBoxX7.Left = labelX3.Right + 9;
            textBoxX7.Width = (int) ((groupPanel3.Width*70.0)/269.0);
            labelX4.Left = textBoxX7.Right + 9;
            labelX4.Width = (int) ((groupPanel3.Width*39.0)/269.0);
            textBoxX8.Left = labelX4.Right + 9;
            textBoxX8.Width = (int) ((groupPanel3.Width*70.0)/269.0);
            labelX5.Width = (int) ((groupPanel3.Width*39.0)/269.0);
            textBoxX9.Left = labelX5.Right + 9;
            textBoxX9.Width = (int) ((groupPanel3.Width*70.0)/269.0);
            labelX5.Width = (int) ((groupPanel3.Width*39.0)/269.0);
            textBoxX9.Left = labelX5.Right + 9;
            textBoxX9.Width = (int) ((groupPanel3.Width*70.0)/269.0);
            labelX6.Left = textBoxX9.Right + 9;
            labelX6.Width = (int) ((groupPanel3.Width*39.0)/269.0);
            textBoxX10.Left = labelX6.Right + 9;
            textBoxX10.Width = (int) ((groupPanel3.Width*70.0)/269.0);
            labelX9.Width = (int) ((groupPanel4.Width*39.0)/183.0);
            textBoxX13.Left = labelX9.Right + 9;
            textBoxX13.Width = (int) ((groupPanel4.Width*70.0)/183.0);
            labelX8.Width = (int) ((groupPanel4.Width*39.0)/183.0);
            textBoxX12.Left = labelX8.Right + 9;
            textBoxX12.Width = (int) ((groupPanel4.Width*70.0)/183.0);
            labelX7.Width = (int) ((groupPanel4.Width*39.0)/183.0);
            textBoxX11.Left = labelX7.Right + 9;
            textBoxX11.Width = (int) ((groupPanel4.Width*70.0)/183.0);
        }

        private void DevicelistView_MouseClick(object sender, MouseEventArgs e)
        {
            var index = 0;
            if (DevicelistView.FocusedItem != null)
            {
                index = DevicelistView.FocusedItem.Index;
            }
            var scoperesources = "";
            scoperesources = DevicelistView.Items[index].SubItems[3].Text;
            var devicename = "";
            devicename = DevicelistView.Items[index].SubItems[0].Text;
            if (scoperesources != "")
            {
                var connectManager = ConnectManager.GetConnectManager();
                if (connectManager.OpenSession(scoperesources) == -1)
                {
                    MessageBox.Show(
                        "The device can not be open, Please confirm the setting is matching or the cable is connected");
                }
                else
                {
                    MyScope.Getscpoe().Setstatus("Alive");
                    new FormConnect();
                    if (connectManager.device_str_op(scoperesources) != -1)
                    {
                        DevicelistView.Items[index].Selected = true;
                        DevicelistView.Items[index].SubItems[0].ForeColor = Color.Blue;
                        var count = DevicelistView.Items.Count;
                        for (var i = 0; i < count; i++)
                        {
                            if (i != index)
                            {
                                DevicelistView.Items[i].Selected = false;
                                DevicelistView.Items[i].SubItems[0].ForeColor = Color.Black;
                            }
                        }
                        treeView1.Nodes[0].Text = scoperesources;
                        updata_CH_list(devicename);
                        treeView1.ExpandAll();
                        Updata_UI(0, 0, true);
                    }
                }
            }
        }

        private void EXCU_SCPI_Click(object sender, EventArgs e)
        {
            var cmd = "";
            var responseString = "";
            var num = richTextBox1.Lines.Count();
            cmd = richTextBox1.Lines[num - 2];
            var connectManager = ConnectManager.GetConnectManager();
            if (connectManager.isconnected())
            {
                if (cmd != "")
                {
                    if (cmd.Contains("?"))
                    {
                        connectManager.WriteStrCmd(cmd);
                        if (connectManager.ReadStrFromDevice(out responseString) != -1)
                        {
                            richTextBox2.AppendText(responseString);
                            base.Update();
                        }
                    }
                    else
                    {
                        connectManager.WriteStrCmd(cmd);
                    }
                }
            }
            else
            {
                MessageBox.Show("The device is not connected yet，Please connect it first");
            }
        }

        public TreeNode findnode(TreeNode root, string strname)
        {
            if (root == null)
            {
                return null;
            }
            if (root.Name == strname)
            {
                return root;
            }
            TreeNode node = null;
            foreach (TreeNode node2 in root.Nodes)
            {
                node = findnode(node2, strname);
                if (node != null)
                {
                    return node;
                }
            }
            return node;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var connectManager = ConnectManager.GetConnectManager();
            if (connectManager.isconnected() && (connectManager.CloseSession() == -1))
            {
                MessageBox.Show("The device disconnected has meet problem");
            }
        }

        public int GetchNumb()
        {
            return connectDeviceCHnumbs;
        }

        private string getdatastr(string str)
        {
            var chArray = new char[2];
            var str2 = "";
            chArray[0] = ' ';
            chArray[1] = ',';
            var index = 0;
            var str3 = "";
            str.TrimEnd(new[] {'\n'});
            str.TrimEnd(new[] {'\r'});
            if (str.IndexOf(chArray[1], 0) == -1)
            {
                index = str.IndexOf(chArray[0], 0);
                str3 = str.Substring(index + 1);
                str2 = getdigitstr(str3);
            }
            return str2;
        }

        private string getdigitstr(string str)
        {
            var str2 = "";
            var chArray = new[] {'e', 'E', 'm', 'M', 'k', 'K', 'V'};
            var index = 0;
            index = str.IndexOf(chArray[0], 0);
            var num3 = 0;
            num3 = str.IndexOf(chArray[1], 0);
            str.IndexOf(chArray[2], 0);
            str.IndexOf(chArray[3], 0);
            str.IndexOf(chArray[4], 0);
            str.IndexOf(chArray[5], 0);
            if ((index != -1) || (num3 != -1))
            {
                var startIndex = 0;
                startIndex = str.IndexOf(chArray[6], 0);
                if (startIndex != -1)
                {
                    str = str.Remove(startIndex, 1);
                    str2 = double.Parse(str).ToString();
                }
            }
            return str2;
        }

        public bool GetRefreshCH1()
        {
            return bch1Refresh;
        }

        public bool GetRefreshCH2()
        {
            return bch2Refresh;
        }

        public bool GetRefreshCH3()
        {
            return bch3Refresh;
        }

        public bool GetRefreshCH4()
        {
            return bch4Refresh;
        }

        public static FormMain getstaticmain()
        {
            return mainfrm_static;
        }

        private float getvdiv(string ch)
        {
            var cmd = ch + ":VDIV?";
            var responseString = "";
            var num = 0f;
            var connectManager = ConnectManager.GetConnectManager();
            if (!connectManager.isconnected())
            {
                return num;
            }
            connectManager.WriteStrCmd(cmd);
            if (connectManager.ReadStrFromDevice(out responseString) == -1)
            {
                return 0f;
            }
            return Convert.ToSingle(getdatastr(responseString));
        }

        private float getzerolin(string ch, float vdiv)
        {
            var cmd = ch + ":OFST?";
            var num = 0f;
            var responseString = "";
            var connectManager = ConnectManager.GetConnectManager();
            if (!connectManager.isconnected())
            {
                return num;
            }
            connectManager.WriteStrCmd(cmd);
            if (connectManager.ReadStrFromDevice(out responseString) == -1)
            {
                return 0f;
            }
            return Convert.ToSingle(getdatastr(responseString));
        }

        private void Help_Click_1(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + @"\EasyScopeXEN.chm");
            }
            catch
            {
            }
        }

        private void Horizontal_Zoom_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                WGraph.TrackMode = CWGraphTrackModes.cwGTrackZoomRectX;
                HsliderItem.Enabled = true;
            }
        }

        private void HsliderItem_ValueChanging(object sender, CancelIntValueEventArgs e)
        {
            var num = float.Parse(e.NewValue.ToString());
            var num2 = (int) num;
            CWave wave = null;
            if (selectCHindex == 1)
            {
                wave = CH1wave;
            }
            else if (selectCHindex == 2)
            {
                wave = CH2wave;
            }
            else if (selectCHindex == 3)
            {
                wave = CH3wave;
            }
            else if (selectCHindex == 4)
            {
                wave = CH4wave;
            }
            if (((wave != null) && (num2 >= 0)) && (num2 <= 100))
            {
                var num3 = 0;
                var num4 = 0;
                var num5 = 0f;
                num5 = (wave.wavelen/100)*num;
                num3 = (int) num5;
                WGraph.Axes.Item(1).Minimum = num3;
                num4 = num3 + (xposzoommax - xposzoommin);
                WGraph.Axes.Item(1).Maximum = num4;
                xposzoommin = num3;
                xposzoommax = num4;
            }
        }

        private void measure_view_autosize()
        {
            labelX10.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX11.Left = labelX10.Right + 20;
            labelX11.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX12.Left = labelX11.Right + 20;
            labelX12.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX13.Left = labelX12.Right + 20;
            labelX13.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX14.Left = labelX13.Right + 20;
            labelX14.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX15.Left = labelX14.Right + 20;
            labelX15.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX16.Left = labelX15.Right + 20;
            labelX16.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX14.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX15.Left = textBoxX14.Right + 20;
            textBoxX15.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX16.Left = textBoxX15.Right + 20;
            textBoxX16.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX17.Left = textBoxX16.Right + 20;
            textBoxX17.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX18.Left = textBoxX17.Right + 20;
            textBoxX18.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX19.Left = textBoxX18.Right + 20;
            textBoxX19.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX20.Left = textBoxX19.Right + 20;
            textBoxX20.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX23.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX22.Left = labelX23.Right + 20;
            labelX22.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX21.Left = labelX22.Right + 20;
            labelX21.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX20.Left = labelX21.Right + 20;
            labelX20.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX19.Left = labelX20.Right + 20;
            labelX19.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX18.Left = labelX19.Right + 20;
            labelX18.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX17.Left = labelX18.Right + 20;
            labelX17.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX27.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX26.Left = textBoxX27.Right + 20;
            textBoxX26.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX25.Left = textBoxX26.Right + 20;
            textBoxX25.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX24.Left = textBoxX25.Right + 20;
            textBoxX24.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX23.Left = textBoxX24.Right + 20;
            textBoxX23.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX22.Left = textBoxX23.Right + 20;
            textBoxX22.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX21.Left = textBoxX22.Right + 20;
            textBoxX21.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX30.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX29.Left = labelX30.Right + 20;
            labelX29.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX28.Left = labelX29.Right + 20;
            labelX28.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX27.Left = labelX28.Right + 20;
            labelX27.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX26.Left = labelX27.Right + 20;
            labelX26.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX25.Left = labelX26.Right + 20;
            labelX25.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            labelX24.Left = labelX25.Right + 20;
            labelX24.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX34.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX33.Left = textBoxX34.Right + 20;
            textBoxX33.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX32.Left = textBoxX33.Right + 20;
            textBoxX32.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX31.Left = textBoxX32.Right + 20;
            textBoxX31.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX30.Left = textBoxX31.Right + 20;
            textBoxX30.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX29.Left = textBoxX30.Right + 20;
            textBoxX29.Width = (int) ((groupPanel2.Width*63.0)/583.0);
            textBoxX28.Left = textBoxX29.Right + 20;
            textBoxX28.Width = (int) ((groupPanel2.Width*63.0)/583.0);
        }

        private void OpenFile_AndSend(string filename)
        {
            var connectManager = ConnectManager.GetConnectManager();
            if (connectManager.isconnected())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var buffer = new byte[stream.Length];
                    connectManager.SetSetupfileLen((int) stream.Length);
                    while (stream.Read(buffer, 0, buffer.Length) > 0)
                    {
                        connectManager.WriteBufferData(ref buffer);
                    }
                    return;
                }
            }
            MessageBox.Show("The device is not connected yet，Please connect it first");
        }

        private void paramCHselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus() && (paramCHselect.Items.Count > 0))
            {
                var selectedIndex = paramCHselect.SelectedIndex;
                selectCHindex = selectedIndex + 1;
                if (selectedIndex >= 0)
                {
                    if (CurscheckBox.Checked)
                    {
                        Updata_measur(selectedIndex + 1);
                    }
                    if (TraceProt.Checked)
                    {
                        updata_proprites(selectedIndex + 1);
                    }
                }
            }
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            /*var image = (Bitmap) pictureBox1.Image;
            if (image != null)
            {
                var width = image.Width;
                var height = image.Height;
                pictureBox1.Image = bmp_resize(image, width, height);
            }*/
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var graphics = e.Graphics;
            var left = e.MarginBounds.Left;
            var top = e.MarginBounds.Top;
            WGraph.PlotAreaColor = Color.White;
            var image = WGraph.ControlImage();
            if (image != null)
            {
                var x = e.MarginBounds.X;
                var y = e.MarginBounds.Y;
                var width = image.Width;
                var height = image.Height;
                if ((width/e.MarginBounds.Width) > (height/e.MarginBounds.Height))
                {
                    width = e.MarginBounds.Width;
                    height = (image.Height*e.MarginBounds.Width)/image.Width;
                }
                else
                {
                    height = e.MarginBounds.Height;
                    width = (image.Width*e.MarginBounds.Height)/image.Height;
                }
                var destRect = new Rectangle(x, y, width, height);
                e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                WGraph.PlotAreaColor = Color.Black;
            }
        }

        private void printDocument_PrintPage2(object sender, PrintPageEventArgs e)
        {
            var graphics = e.Graphics;
            var left = e.MarginBounds.Left;
            var top = e.MarginBounds.Top;
            var image = pictureBox1.Image;
            if (image != null)
            {
                var x = e.MarginBounds.X;
                var y = e.MarginBounds.Y;
                var width = image.Width;
                var height = image.Height;
                if ((width/e.MarginBounds.Width) > (height/e.MarginBounds.Height))
                {
                    width = e.MarginBounds.Width;
                    height = (image.Height*e.MarginBounds.Width)/image.Width;
                }
                else
                {
                    height = e.MarginBounds.Height;
                    width = (image.Width*e.MarginBounds.Height)/image.Height;
                }
                var destRect = new Rectangle(x, y, width, height);
                e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            }
        }

        private void reflash_waveform(string ch, int chIndex)
        {
            var cmd = "WFSU TYPE,1";
            var connectManager = ConnectManager.GetConnectManager();
            var flag = connectManager.isconnected();
            if (flag)
            {
                connectManager.WriteStrCmd(cmd);
            }
            var num = 0;
            var str2 = ch + ":WF? ALL";
            var connectType = connectManager.GetConnectType();
            if (flag)
            {
                connectManager.WriteStrCmd(str2);
                if (connectType == 0)
                {
                    connectManager.Delay(0x7d0);
                }
                num = connectManager.ReadUsbtmcData(ref databuff);
            }
            if (databuff != null)
            {
                if (chIndex == 1)
                {
                    CH1wave.getwavedata_measure(ch);
                }
                else if (chIndex == 2)
                {
                    CH2wave.getwavedata_measure(ch);
                }
                else if (chIndex == 3)
                {
                    CH3wave.getwavedata_measure(ch);
                }
                else if (chIndex == 4)
                {
                    CH4wave.getwavedata_measure(ch);
                }
                var length = Marshal.SizeOf(typeof (WD_PARAM));
                var destinationArray = new byte[length];
                Array.Copy(databuff, 0, destinationArray, 0, length);
                var wd_param = new WD_PARAM();
                wd_param = ByteTostruct(ref destinationArray);
                if (chIndex == 1)
                {
                    CH1wave.setproperites(wd_param);
                }
                else if (chIndex == 2)
                {
                    CH2wave.setproperites(wd_param);
                }
                else if (chIndex == 3)
                {
                    CH3wave.setproperites(wd_param);
                }
                else if (chIndex == 4)
                {
                    CH4wave.setproperites(wd_param);
                }
                var num4 = num;
                var num5 = 0x16f;
                var len = (num4 - num5) - 2;
                waveformbuff = new byte[len];
                var buffer2 = new byte[] {0x84};
                var index = 0;
                var num8 = 0;
                for (index = num5; index < (num4 - 2); index++)
                {
                    num8 = index - num5;
                    waveformbuff[num8] = (byte) (databuff[index] + buffer2[0]);
                }
                setshowForm(ref waveformbuff, len, ch, chIndex);
                Updata_measur(chIndex);
                updata_proprites(chIndex);
                CurscheckBox.Enabled = true;
                buttonItem24.Enabled = true;
                buttonItem25.Enabled = true;
                buttonItem34.Enabled = true;
                buttonItem35.Enabled = true;
                Horizontal_Zoom.Enabled = true;
                Vertical_Zoom.Enabled = true;
                CurscheckBox.Enabled = true;
                ResetZoom.Enabled = true;
                TraceProt.Enabled = true;
                if (bAutoRefeshTrace)
                {
                    CurscheckBox.Enabled = false;
                    TraceProt.Enabled = false;
                    paramCHselect.Enabled = false;
                }
            }
            else
            {
                CurscheckBox.Enabled = false;
                buttonItem24.Enabled = false;
                buttonItem25.Enabled = false;
                buttonItem34.Enabled = false;
                buttonItem35.Enabled = false;
                Horizontal_Zoom.Enabled = false;
                Vertical_Zoom.Enabled = false;
                CurscheckBox.Enabled = false;
                ResetZoom.Enabled = false;
                TraceProt.Enabled = false;
            }
        }

        private void reflesh_bmp()
        {
            var connectManager = ConnectManager.GetConnectManager();
            var flag = connectManager.isconnected();
            var connectType = connectManager.GetConnectType();
            if (flag)
            {
                var cmd = "SCDP";
                connectManager.WriteStrCmd(cmd);
                if (connectType == 0)
                {
                    connectManager.Delay(0xbb8);
                }
                connectManager.ReadUsbtmcBmpData(ref Bmpbuff, 480*234*3); // WxHx(RGB=3 bytes)
            }
            else
            {
                MessageBox.Show("The device is not connected yet，Please connect it first");
                return;
            }
            var path = Application.StartupPath + @"\temp.bmp";
            var stream = new FileStream(path, FileMode.OpenOrCreate);
            var handle = stream.Handle;
            switch (connectType)
            {
                case 1:
                case 2:
                    stream.Write(Bmpbuff, 0, Bmpbuff.Length);
                    stream.Close();
                    setbmp_file(path);
                    File.Delete(path);
                    break;

                default:
                    if (connectType == 0)
                    {
                        stream.Write(Bmpbuff, 0, 0x52479);
                        stream.Close();
                        setbmp_file(path);
                        File.Delete(path);
                    }
                    break;
            }
            bSeccessfulRefreshBMP = true;
        }

        private void Resetcurpos_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                CWave wave = null;
                switch ((paramCHselect.SelectedIndex + 1))
                {
                    case 1:
                        wave = CH1wave;
                        break;

                    case 2:
                        wave = CH2wave;
                        break;

                    case 3:
                        wave = CH3wave;
                        break;

                    case 4:
                        wave = CH4wave;
                        break;
                }
                if (wave != null)
                {
                    WGraph.Cursors.Item(1).Visible = true;
                    WGraph.Cursors.Item(2).Visible = true;
                    WGraph.Cursors.Item(1).XPosition = ((xposzoommax - xposzoommin)/3) + xposzoommin;
                    WGraph.Cursors.Item(2).XPosition = xposzoommax - ((xposzoommax - xposzoommin)/3);
                    WGraph.Cursors.Item(1).YPosition = 0;
                    WGraph.Cursors.Item(2).YPosition = 0;
                    cursorX1 = ((xposzoommax - xposzoommin)/3) + xposzoommin;
                    cursorX2 = xposzoommax - ((xposzoommax - xposzoommin)/3);
                    cursorY1 = wave.wavedata[cursorX1];
                    cursorY2 = wave.wavedata[cursorX2];
                    textBoxX5.Text = cursorX1.ToString();
                    textBoxX6.Text = wave.wavedata[cursorX1].ToString("F3") + "v";
                    textBoxX7.Text = cursorX2.ToString();
                    textBoxX8.Text = wave.wavedata[cursorX2].ToString("F3") + "v";
                    textBoxX9.Text = (cursorX2 - cursorX1).ToString();
                    textBoxX10.Text = ((wave.wavedata[cursorX2] - wave.wavedata[cursorX1])).ToString("F3") + "v";
                    textBoxX13.Text = "0.0v";
                    textBoxX12.Text = "0.0v";
                    textBoxX11.Text = "0.0v";
                    WGraph.TrackMode = CWGraphTrackModes.cwGTrackDragCursor;
                }
            }
        }

        private void ResetZoom_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                CWave wave = null;
                if (selectCHindex == 1)
                {
                    wave = CH1wave;
                }
                else if (selectCHindex == 2)
                {
                    wave = CH2wave;
                }
                else if (selectCHindex == 3)
                {
                    wave = CH3wave;
                }
                else if (selectCHindex == 4)
                {
                    wave = CH4wave;
                }
                if (wave != null)
                {
                    WGraph.Axes.Item(2).SetMinMax(-maxamp, maxamp);
                    WGraph.Axes.Item(1).Minimum = 0;
                    WGraph.Axes.Item(1).Maximum = wave.wavelen;
                    WGraph.Plots.Item(selectCHindex).PlotY(wave.wavedata, 0, 1);
                    xposzoommin = 0;
                    xposzoommax = wave.wavelen;
                    VsliderItem.Enabled = false;
                    HsliderItem.Enabled = false;
                    if (CurscheckBox.Checked)
                    {
                        WGraph.TrackMode = CWGraphTrackModes.cwGTrackDragCursor;
                    }
                    else
                    {
                        WGraph.TrackMode = CWGraphTrackModes.cwGTrackPlotAreaEvents;
                    }
                }
            }
        }

        private void ribbonTabItem2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("SCPI_CMD");
            richTextBox1.Focus();
            checkBoxItem5.Checked = true;
            checkBoxItem6.Checked = false;
        }

        private void ribbonTabItem3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("WavePage");
            if (ConnectManager.GetConnectManager().isconnected())
            {
                buttonItem22.Enabled = true;
                AutoRefreshTrace.Enabled = true;
                if (bRefreshTrace)
                {
                    buttonItem24.Enabled = true;
                    buttonItem25.Enabled = true;
                    buttonItem34.Enabled = true;
                    buttonItem35.Enabled = true;
                    Horizontal_Zoom.Enabled = true;
                    Vertical_Zoom.Enabled = true;
                    CurscheckBox.Enabled = true;
                    ResetZoom.Enabled = true;
                    TraceProt.Enabled = true;
                }
            }
            else
            {
                buttonItem22.Enabled = false;
                AutoRefreshTrace.Enabled = false;
                buttonItem24.Enabled = false;
                buttonItem25.Enabled = false;
                buttonItem34.Enabled = false;
                buttonItem35.Enabled = false;
                Horizontal_Zoom.Enabled = false;
                HsliderItem.Enabled = false;
                Vertical_Zoom.Enabled = false;
                VsliderItem.Enabled = false;
                CurscheckBox.Enabled = false;
                TraceProt.Enabled = false;
                ResetZoom.Enabled = false;
            }
        }

        private void ribbonTabItem4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("BmpPage");
            comboBoxItem2.SelectedIndex = 1;
            if (ConnectManager.GetConnectManager().isconnected())
            {
                buttonScreenCaptureRefresh.Enabled = true;
                checkBoxItem3.Enabled = true;
                if (bSeccessfulRefreshBMP)
                {
                    buttonItem27.Enabled = true;
                    buttonItem31.Enabled = true;
                    buttonItem32.Enabled = true;
                    buttonItem33.Enabled = true;
                }
                comboBoxItem2.Enabled = true;
            }
            else
            {
                buttonScreenCaptureRefresh.Enabled = false;
                checkBoxItem3.Enabled = false;
                if (!bSeccessfulRefreshBMP)
                {
                    buttonItem27.Enabled = false;
                    buttonItem31.Enabled = false;
                    buttonItem32.Enabled = false;
                    buttonItem33.Enabled = false;
                }
                comboBoxItem2.Enabled = false;
            }
        }

        private void ribbonTabItem6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("Setups");
            if (ConnectManager.GetConnectManager().isconnected())
            {
                buttonItem36.Enabled = true;
                buttonItem37.Enabled = true;
            }
            else
            {
                buttonItem36.Enabled = false;
                buttonItem37.Enabled = false;
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') && !CheckDeviceStatus())
            {
                var num = richTextBox1.Lines.Count();
                var cmd = "";
                cmd = richTextBox1.Lines[num - 2];
                var connectManager = ConnectManager.GetConnectManager();
                if (connectManager.isconnected())
                {
                    if (cmd != "")
                    {
                        if (cmd.Contains("?"))
                        {
                            connectManager.WriteStrCmd(cmd);
                            if (checkBoxItem5.Checked)
                            {
                                string str;
                                if (connectManager.ReadStrFromDevice(out str) == -1)
                                {
                                    return;
                                }
                                cmd = cmd.ToUpper();
                                var text = "";
                                if ((cmd == "*IDN?") && (str != ""))
                                {
                                    text = SCPIStringOperation(str);
                                }
                                else
                                {
                                    text = str;
                                }
                                richTextBox2.AppendText(text);
                            }
                            else if (checkBoxItem6.Checked)
                            {
                                var s = "";
                                var responseString = "";
                                cmd = cmd.ToUpper();
                                if (connectManager.ReadStrFromDevice(out responseString) == -1)
                                {
                                    return;
                                }
                                if ((cmd == "*IDN?") && (responseString != ""))
                                {
                                    s = SCPIStringOperation(responseString);
                                }
                                else
                                {
                                    s = responseString;
                                }
                                var bytes = Encoding.ASCII.GetBytes(s);
                                var str6 = "";
                                var num4 = 1;
                                var length = bytes.Length;
                                for (var i = 0; i < bytes.Length; i++)
                                {
                                    if (length >= 0x10)
                                    {
                                        if ((num4%0x11) != 0)
                                        {
                                            str6 = bytes[i].ToString("X");
                                            richTextBox2.AppendText(str6);
                                            richTextBox2.AppendText("  ");
                                            num4++;
                                        }
                                        else
                                        {
                                            richTextBox2.AppendText("      ");
                                            for (var j = 0; j < 0x10; j++)
                                            {
                                                str6 = AscllToChar(bytes[(i - 0x10) + j]);
                                                richTextBox2.AppendText(str6);
                                            }
                                            richTextBox2.AppendText("\n");
                                            num4 = 1;
                                            i--;
                                            length -= 0x10;
                                        }
                                    }
                                    else if ((num4%length) != 0)
                                    {
                                        str6 = bytes[i].ToString("X");
                                        richTextBox2.AppendText(str6);
                                        richTextBox2.AppendText("  ");
                                        num4++;
                                    }
                                    else
                                    {
                                        var num8 = (0x11 - length)*6;
                                        for (var k = 0; k < num8; k++)
                                        {
                                            richTextBox2.AppendText(" ");
                                        }
                                        richTextBox2.AppendText("      ");
                                        for (var m = 0; m < length; m++)
                                        {
                                            str6 = AscllToChar(bytes[((i - length) + 1) + m]);
                                            richTextBox2.AppendText(str6);
                                        }
                                    }
                                }
                                richTextBox2.AppendText("\n");
                            }
                            base.Update();
                        }
                        else
                        {
                            connectManager.WriteStrCmd(cmd);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The device is not connected yet，Please connect it first");
                }
            }
        }

        public void SaveCSVchSelect(bool bsave)
        {
            bsaveoperation = bsave;
        }

        private void SaveSetupFile(ref byte[] Configbuff, int filelenth)
        {
            var dialog = new SaveFileDialog
                             {
                                 Filter = "setup files (*.lss)|*.lss",
                                 RestoreDirectory = true
                             };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = dialog.FileName;
                var stream = dialog.OpenFile();
                if (stream != null)
                {
                    stream.Write(Configbuff, 0, filelenth);
                    stream.Close();
                    tabControl1.SelectTab("Setups");
                    Addsetupfile_toList(fileName);
                }
            }
        }

        private void scpicomboBox_ComboBoxTextChanged(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            var text = scpicomboBox.SelectedItem.ToString();
            richTextBox1.AppendText(text);
            base.Update();
        }

        private string SCPIStringOperation(string scpistr)
        {
            scpistr.Contains("ATTEN");
            var anyOf = new[] {','};
            var startIndex = scpistr.IndexOfAny(anyOf, 0);
            var oldValue = "";
            var str3 = scpistr.Substring(startIndex + 1);
            startIndex = str3.IndexOfAny(anyOf, 0);
            oldValue = str3.Remove(startIndex);
            var str4 = oldValue.Substring(0, 4);
            var str5 = oldValue.Substring(oldValue.Length - 1, 1);
            if ((str4 == "SDS1") && (str5 == "X"))
            {
                scpistr = scpistr.Replace("ATTEN", "");
                scpistr = scpistr.Replace(oldValue, "");
            }
            return scpistr;
        }

        private void setbmp_file(string file)
        {
            if (pictureBox1.InvokeRequired)
            {
                Lload_Bmp_Callback method = setbmp_file;
                Invoke(method, new object[] {file});
            }
            else
            {
                pictureBox1.Load(file);
            }
        }

        public void SetBufferSize()
        {
            databuff = new byte[0x200008];
            Bmpbuff = new byte[0x200008];
            Configbuff = new byte[0x2800];
            waveformbuff = new byte[0x200008];
        }

        public void SetSaveSelectCH(bool bch1, bool bch2, bool bch3, bool bch4)
        {
            bCH1save = bch1;
            bCH2save = bch2;
            bCH3save = bch3;
            bCH4save = bch4;
        }

        private void setshowForm(ref byte[] buff, int len, string ch, int chIndex)
        {
            var vdiv = 1f;
            var num2 = 0f;
            var num3 = 0f;
            var num4 = 0f;
            var num5 = 0f;
            var num6 = 0f;
            var num7 = 0f;
            vdiv = getvdiv(ch);
            num7 = getzerolin(ch, vdiv);
            switch (chIndex)
            {
                case 1:
                    CH1wave.wavedata = new float[len];
                    break;
                case 2:
                    CH2wave.wavedata = new float[len];
                    break;
                case 3:
                    CH3wave.wavedata = new float[len];
                    break;
                case 4:
                    CH4wave.wavedata = new float[len];
                    break;
            }
            for (var i = 0; i < len; i++)
            {
                switch (chIndex)
                {
                    case 1:
                        CH1wave.wavedata[i] = (((buff[i] - ((num7/vdiv)*25f)) - 132f)*(vdiv/25f)) + num7;
                        break;
                    case 2:
                        CH2wave.wavedata[i] = (((buff[i] - ((num7/vdiv)*25f)) - 132f)*(vdiv/25f)) + num7;
                        break;
                    case 3:
                        CH3wave.wavedata[i] = (((buff[i] - ((num7/vdiv)*25f)) - 132f)*(vdiv/25f)) + num7;
                        break;
                    case 4:
                        CH4wave.wavedata[i] = (((buff[i] - ((num7/vdiv)*25f)) - 132f)*(vdiv/25f)) + num7;
                        break;
                }
            }
            switch (chIndex)
            {
                case 1:
                    num2 = 4f*vdiv;
                    CH1maxamp = num2;
                    break;
                case 2:
                    num3 = 4f*vdiv;
                    CH2maxamp = num3;
                    break;
                case 3:
                    num4 = 4f*vdiv;
                    CH3maxamp = num4;
                    break;
                case 4:
                    num5 = 4f*vdiv;
                    CH4maxamp = num5;
                    break;
            }
            num6 = CH1maxamp > CH2maxamp ? CH1maxamp : CH2maxamp;
            if (CH3maxamp > num6)
            {
                num6 = CH3maxamp;
            }
            if (CH4maxamp > num6)
            {
                num6 = CH4maxamp;
            }
            maxamp = num6;
            WGraph.Axes.Item(2).SetMinMax(-maxamp, maxamp);
            WGraph.Axes.Item(1).Maximum = len - 1;
            switch (chIndex)
            {
                case 1:
                    CH1wave.wavelen = len - 1;
                    xposzoommax = CH1wave.wavelen;
                    break;
                case 2:
                    CH2wave.wavelen = len - 1;
                    xposzoommax = CH2wave.wavelen;
                    break;
                case 3:
                    CH3wave.wavelen = len - 1;
                    xposzoommax = CH3wave.wavelen;
                    break;
                case 4:
                    CH4wave.wavelen = len - 1;
                    xposzoommax = CH4wave.wavelen;
                    break;
            }
            xposzoommin = 0;
            yposzoommin = -maxamp;
            yposzoommax = maxamp;
            switch (ch)
            {
                case "C1":
                    WGraph.Plots.Item(1).PlotY(CH1wave.wavedata, 0, 1);
                    WGraph.Refresh();
                    break;
                case "C2":
                    WGraph.Plots.Item(2).PlotY(CH2wave.wavedata, 0, 1);
                    WGraph.Refresh();
                    break;
                case "C3":
                    WGraph.Plots.Item(3).PlotY(CH3wave.wavedata, 0, 1);
                    WGraph.Refresh();
                    break;
                case "C4":
                    WGraph.Plots.Item(4).PlotY(CH4wave.wavedata, 0, 1);
                    WGraph.Refresh();
                    break;
            }
        }

        public void theout(object source, ElapsedEventArgs e)
        {
            reflesh_bmp();
            if (bSeccessfulRefreshBMP)
            {
                buttonItem27.Enabled = true;
                buttonItem31.Enabled = true;
                buttonItem32.Enabled = true;
                buttonItem33.Enabled = true;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var name = e.Node.Name;
            currentch = e.Node.Name;
            switch (name)
            {
                case "Channel1":
                    if (!CheckDeviceStatus())
                    {
                        tabControl1.SelectTab("WavePage");
                        ribbonTabItem3.Select();
                        if (bch1Refresh)
                        {
                            WGraph.Plots.Item(1).ClearData();
                            WGraph.Plots.Item(1).Visible = false;
                            WGraph.Plots.Item(1).Visible = true;
                            WGraph.Refresh();
                            bRefreshTrace = false;
                            bch1Refresh = false;
                            return;
                        }
                        reflash_waveform("C1", 1);
                        selectCHindex = 1;
                        paramCHselect.SelectedIndex = 0;
                        bRefreshTrace = true;
                        bch1Refresh = true;
                        AutoRefreshTrace.Enabled = true;
                        buttonItem22.Enabled = true;
                    }
                    return;

                case "Channel2":
                    if (!CheckDeviceStatus())
                    {
                        tabControl1.SelectTab("WavePage");
                        ribbonTabItem3.Select();
                        if (bch2Refresh)
                        {
                            WGraph.Plots.Item(2).ClearData();
                            WGraph.Plots.Item(2).Visible = false;
                            WGraph.Plots.Item(2).Visible = true;
                            WGraph.Refresh();
                            bRefreshTrace = false;
                            bch2Refresh = false;
                            return;
                        }
                        reflash_waveform("C2", 2);
                        selectCHindex = 2;
                        paramCHselect.SelectedIndex = 1;
                        bRefreshTrace = true;
                        bch2Refresh = true;
                        AutoRefreshTrace.Enabled = true;
                        buttonItem22.Enabled = true;
                    }
                    return;

                case "Channel3":
                    if (!CheckDeviceStatus())
                    {
                        tabControl1.SelectTab("WavePage");
                        ribbonTabItem3.Select();
                        if (bch3Refresh)
                        {
                            WGraph.Plots.Item(3).ClearData();
                            WGraph.Plots.Item(3).Visible = false;
                            WGraph.Plots.Item(3).Visible = true;
                            WGraph.Refresh();
                            bRefreshTrace = false;
                            bch3Refresh = false;
                            return;
                        }
                        reflash_waveform("C3", 3);
                        selectCHindex = 3;
                        paramCHselect.SelectedIndex = 2;
                        bRefreshTrace = true;
                        bch3Refresh = true;
                        AutoRefreshTrace.Enabled = true;
                        buttonItem22.Enabled = true;
                    }
                    return;

                case "Channel4":
                    if (!CheckDeviceStatus())
                    {
                        tabControl1.SelectTab("WavePage");
                        ribbonTabItem3.Select();
                        if (bch4Refresh)
                        {
                            WGraph.Plots.Item(4).ClearData();
                            WGraph.Plots.Item(4).Visible = false;
                            WGraph.Plots.Item(4).Visible = true;
                            WGraph.Refresh();
                            bRefreshTrace = false;
                            bch4Refresh = false;
                            return;
                        }
                        reflash_waveform("C4", 4);
                        selectCHindex = 4;
                        paramCHselect.SelectedIndex = 3;
                        bRefreshTrace = true;
                        bch4Refresh = true;
                        AutoRefreshTrace.Enabled = true;
                        buttonItem22.Enabled = true;
                    }
                    return;

                case "scpi_cmd":
                    tabControl1.SelectTab("SCPI_CMD");
                    ribbonTabItem2.Select();
                    richTextBox1.Focus();
                    checkBoxItem5.Checked = true;
                    checkBoxItem6.Checked = false;
                    return;

                case "VPanel":
                    if (!CheckDeviceStatus())
                    {
                        new FormControlPanel().ShowDialog();
                    }
                    return;

                case "BMP_Capture":
                    if (CheckDeviceStatus())
                    {
                        return;
                    }
                    tabControl1.SelectTab("BmpPage");
                    ribbonTabItem4.Select();
                    comboBoxItem2.SelectedIndex = 1;
                    reflesh_bmp();
                    if (bSeccessfulRefreshBMP)
                    {
                        buttonItem27.Enabled = true;
                        buttonItem31.Enabled = true;
                        buttonItem32.Enabled = true;
                        buttonItem33.Enabled = true;
                        return;
                    }
                    break;

                case "SCOPSetups":
                    tabControl1.SelectTab("Setups");
                    ribbonTabItem6.Select();
                    break;
            }
        }

        public void updata_CH_list(string devicename)
        {
            var str = "";
            var connectManager = ConnectManager.GetConnectManager();
            connectManager.GetDeviceType();
            foreach (CWPlot p in WGraph.Plots)
            {
                p.ClearData();
                p.Visible = false;
                p.Visible = true;
            }
            WGraph.Refresh();
            var cmd = "CHS?";
            connectManager.WriteStrCmd(cmd);
            var responseString = "";
            if (connectManager.ReadStrFromDevice(out responseString) != -1)
            {
                if (responseString != "")
                {
                    responseString.TrimStart(new[] {' '});
                    responseString.TrimEnd(new[] {' '});
                    var length = responseString.Length;
                    str = responseString.Substring(length - 2, 1);
                }
                if (str == "2")
                {
                    connectDeviceCHnumbs = 2;
                    var root = treeView1.Nodes[0];
                    TreeNode node2 = null;
                    node2 = findnode(root, "Channel3");
                    if (node2 != null)
                    {
                        node2.Remove();
                    }
                    node2 = null;
                    node2 = findnode(root, "Channel4");
                    if (node2 != null)
                    {
                        node2.Remove();
                    }
                    CH3.Enabled = false;
                    CH4.Enabled = false;
                }
                else if (str == "4")
                {
                    connectDeviceCHnumbs = 4;
                    var node3 = treeView1.Nodes[0];
                    TreeNode node = null;
                    if (findnode(node3, "Channel3") == null)
                    {
                        TreeNode node5 = null;
                        node5 = findnode(node3, "Channel1");
                        node = new TreeNode("Channel3")
                                   {
                                       Name = "Channel3"
                                   };
                        node5.Parent.Nodes.Add(node);
                        CH3.Enabled = true;
                    }
                    node = null;
                    if (findnode(node3, "Channel4") == null)
                    {
                        TreeNode node6 = null;
                        node6 = findnode(node3, "Channel1");
                        node = new TreeNode("Channel4")
                                   {
                                       Name = "Channel4"
                                   };
                        node6.Parent.Nodes.Add(node);
                        CH4.Enabled = true;
                    }
                }
            }
        }

        public void updata_list_item(int itemindex)
        {
            var scpoe = MyScope.Getscpoe();
            var device = scpoe.GetDevice();
            var status = scpoe.Getstatus();
            var bus = scpoe.Getbus();
            var address = scpoe.Getaddress();
            var nO = scpoe.GetNO();
            DevicelistView.Items[itemindex].Text = device;
            DevicelistView.Items[itemindex].SubItems[1].Text = status;
            DevicelistView.Items[itemindex].SubItems[2].Text = bus;
            DevicelistView.Items[itemindex].SubItems[3].Text = address;
            DevicelistView.Items[itemindex].SubItems[4].Text = nO;
        }

        private void Updata_measur(int chIndex)
        {
            CWave wave = null;
            if (chIndex == 1)
            {
                wave = CH1wave;
            }
            else if (chIndex == 2)
            {
                wave = CH2wave;
            }
            else if (chIndex == 3)
            {
                wave = CH3wave;
            }
            else if (chIndex == 4)
            {
                wave = CH4wave;
            }
            if (wave != null)
            {
                textBoxX14.Text = wave.amp;
                textBoxX15.Text = wave.max;
                textBoxX16.Text = wave.min;
                textBoxX17.Text = wave.mean;
                textBoxX18.Text = wave.vmea;
                textBoxX19.Text = wave.Base;
                textBoxX20.Text = wave.top;
                textBoxX21.Text = wave.RPREshoot;
                textBoxX22.Text = wave.OVSPshoot;
                textBoxX23.Text = wave.FPREshoot;
                textBoxX24.Text = wave.OVSNshoot;
                textBoxX25.Text = wave.crms;
                textBoxX26.Text = wave.rms;
                textBoxX27.Text = wave.pkpk;
                textBoxX28.Text = wave.burstwidth;
                textBoxX29.Text = wave.nduty;
                textBoxX30.Text = wave.pduty;
                textBoxX31.Text = wave.fall_time;
                textBoxX32.Text = wave.rise_time;
                textBoxX33.Text = wave.perid;
                textBoxX34.Text = wave.frequency;
            }
        }

        private void updata_proprites(int chIndex)
        {
            CWave wave = null;
            if (chIndex == 1)
            {
                wave = CH1wave;
            }
            else if (chIndex == 2)
            {
                wave = CH2wave;
            }
            else if (chIndex == 3)
            {
                wave = CH3wave;
            }
            else if (chIndex == 4)
            {
                wave = CH4wave;
            }
            if (wave != null)
            {
                textBoxItem3.ControlText = wave.h_div;
                textBoxItem1.ControlText = wave.v_div;
                textBoxItem2.ControlText = wave.h_start;
                textBoxItem4.ControlText = wave.h_stop;
                textBoxItem5.ControlText = wave.v_start;
                textBoxItem6.ControlText = wave.v_stop;
                textBoxItem7.ControlText = wave.points;
                textBoxItem10.ControlText = wave.v_offset;
            }
        }

        public void Updata_UI(int MenuIndex, int itemIndex, bool value)
        {
            bhavedevice = true;
            if (MenuIndex == 0)
            {
                if (itemIndex == 0)
                {
                    Remove.Enabled = value;
                    DisConnect.Enabled = value;
                    Connect.Enabled = value;
                    buttonItem29.Enabled = value;
                }
                else if (itemIndex == 2)
                {
                    Remove.Enabled = value;
                }
                else if (itemIndex == 3)
                {
                    DisConnect.Enabled = value;
                }
                else if (itemIndex == 3)
                {
                    Connect.Enabled = value;
                }
            }
        }

        private void Vertical_Zoom_Click(object sender, EventArgs e)
        {
            if (!CheckDeviceStatus())
            {
                WGraph.TrackMode = CWGraphTrackModes.cwGTrackZoomRectY;
                VsliderItem.Enabled = true;
            }
        }

        private void VsliderItem_ValueChanging(object sender, CancelIntValueEventArgs e)
        {
            var num = float.Parse(e.NewValue.ToString());
            var num2 = 0f;
            var num3 = 0f;
            var num4 = 0f;
            num4 = (maxamp/2f) - ((maxamp/100f)*num);
            num3 = num4;
            num4 = num3 - (yposzoommax - yposzoommin);
            num2 = num4;
            WGraph.Axes.Item(2).Maximum = num3;
            WGraph.Axes.Item(2).Minimum = num2;
            yposzoommin = num2;
            yposzoommax = num3;
        }

        private void WGraph_CursorChange_1(object sender, _DCWGraphEvents_CursorChangeEvent e)
        {
            var str = e.cursorIndex.ToString();
            CWave wave = null;
            switch ((paramCHselect.SelectedIndex + 1))
            {
                case 1:
                    wave = CH1wave;
                    break;

                case 2:
                    wave = CH2wave;
                    break;

                case 3:
                    wave = CH3wave;
                    break;

                case 4:
                    wave = CH4wave;
                    break;
            }
            if (wave != null)
            {
                switch (str)
                {
                    case "1":
                        {
                            var num2 = float.Parse(WGraph.Cursors.Item(1).XPosition.ToString());
                            var index = 0;
                            index = (int) (num2 + 0.5);
                            cursorX1 = index;
                            var single1 = wave.wavedata[index];
                            textBoxX5.Text = index.ToString();
                            var str2 = wave.wavedata[index].ToString();
                            textBoxX6.Text = wave.wavedata[index].ToString("F3") + "v";
                            textBoxX9.Text = (cursorX2 - cursorX1).ToString();
                            textBoxX10.Text = ((wave.wavedata[cursorX2] - wave.wavedata[cursorX1])).ToString("F3") + "v";
                            num2 = float.Parse(WGraph.Cursors.Item(1).YPosition.ToString());
                            cursorY3 = num2;
                            textBoxX13.Text = num2.ToString("F3") + "v";
                            textBoxX11.Text = ((cursorY4 - cursorY3)).ToString("F3") + "v";
                            return;
                        }
                    case "2":
                        {
                            var num4 = float.Parse(WGraph.Cursors.Item(2).XPosition.ToString());
                            var num5 = 0;
                            num5 = (int) (num4 + 0.5);
                            cursorX2 = num5;
                            textBoxX7.Text = num5.ToString();
                            textBoxX8.Text = wave.wavedata[num5].ToString("F3") + "v";
                            textBoxX9.Text = (cursorX2 - cursorX1).ToString();
                            textBoxX10.Text = ((wave.wavedata[cursorX2] - wave.wavedata[cursorX1])).ToString("F3") + "v";
                            num4 = float.Parse(WGraph.Cursors.Item(2).YPosition.ToString());
                            cursorY4 = num4;
                            textBoxX12.Text = num4.ToString("F3") + "v";
                            textBoxX11.Text = ((cursorY4 - cursorY3)).ToString("F3") + "v";
                            break;
                        }
                }
            }
        }

        private void WGraph_CursorMouseMove(object sender, _DCWGraphEvents_CursorMouseMoveEvent e)
        {
            WGraph.TrackMode = CWGraphTrackModes.cwGTrackDragCursor;
        }

        private void WGraph_Zoom(object sender, EventArgs e)
        {
            var num = float.Parse(WGraph.Axes.Item(1).Minimum.ToString());
            xposzoommin = (int) (num + 0.5);
            num = float.Parse(WGraph.Axes.Item(1).Maximum.ToString());
            xposzoommax = (int) (num + 0.5);
            num = float.Parse(WGraph.Axes.Item(2).Minimum.ToString());
            yposzoommin = num;
            num = float.Parse(WGraph.Axes.Item(2).Maximum.ToString());
            yposzoommax = num;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 0x204)
            {
                base.WndProc(ref m);
            }
        }

        private delegate void Lload_Bmp_Callback(string bmgfile);

        private void labelItem8_Click(object sender, EventArgs e)
        {

        }
    }
}