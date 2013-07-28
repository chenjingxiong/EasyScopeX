using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace EasyScope
{
    public class FormSerial : Form
    {
        private readonly int Paritytype = -1;
        private readonly int databit;
        private readonly int stopbittype = -1;
        private ComboItem COM1;
        private ComboItem COM10;
        private ComboItem COM2;
        private ComboItem COM3;
        private ComboItem COM4;
        private ComboItem COM5;
        private ComboItem COM6;
        private ComboItem COM7;
        private ComboItem COM8;
        private ComboItem COM9;
        private ComboBoxEx COMport_box;
        private ButtonX SRS232_CANCEL;
        private ButtonX SRS232_OK;
        private int baudrate;
        private string comname = "";
        private IContainer components;
        private GroupPanel groupPanel1;
        private GroupPanel groupPanel2;
        private GroupPanel groupPanel3;
        private GroupPanel groupPanel4;
        private GroupPanel groupPanel5;
        private GroupPanel groupPanel6;
        private RadioButton radioButton3;
        private RadioButton radioButton8;
        private RadioButton radioButton9;
        private RadioButton rate19200;
        private RadioButton rate2400;
        private RadioButton rate300;
        private RadioButton rate38400;
        private RadioButton rate4800;
        private RadioButton rate9600;

        public FormSerial()
        {
            InitializeComponent();
            databit = 8;
            Paritytype = 0;
            stopbittype = 0;
            baudrate = 0x9600;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSerial));
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.COMport_box = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.COM1 = new DevComponents.Editors.ComboItem();
            this.COM2 = new DevComponents.Editors.ComboItem();
            this.COM3 = new DevComponents.Editors.ComboItem();
            this.COM4 = new DevComponents.Editors.ComboItem();
            this.COM5 = new DevComponents.Editors.ComboItem();
            this.COM6 = new DevComponents.Editors.ComboItem();
            this.COM7 = new DevComponents.Editors.ComboItem();
            this.COM8 = new DevComponents.Editors.ComboItem();
            this.COM9 = new DevComponents.Editors.ComboItem();
            this.COM10 = new DevComponents.Editors.ComboItem();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rate38400 = new System.Windows.Forms.RadioButton();
            this.rate19200 = new System.Windows.Forms.RadioButton();
            this.rate4800 = new System.Windows.Forms.RadioButton();
            this.rate9600 = new System.Windows.Forms.RadioButton();
            this.rate2400 = new System.Windows.Forms.RadioButton();
            this.rate300 = new System.Windows.Forms.RadioButton();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.groupPanel5 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel6 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.SRS232_OK = new DevComponents.DotNetBar.ButtonX();
            this.SRS232_CANCEL = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            this.groupPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.COMport_box);
            this.groupPanel1.Location = new System.Drawing.Point(12, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(133, 126);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "Port";
            // 
            // COMport_box
            // 
            this.COMport_box.DisplayMember = "Text";
            this.COMport_box.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.COMport_box.FormattingEnabled = true;
            this.COMport_box.ItemHeight = 15;
            this.COMport_box.Items.AddRange(new object[] {
            this.COM1,
            this.COM2,
            this.COM3,
            this.COM4,
            this.COM5,
            this.COM6,
            this.COM7,
            this.COM8,
            this.COM9,
            this.COM10});
            this.COMport_box.Location = new System.Drawing.Point(19, 36);
            this.COMport_box.Name = "COMport_box";
            this.COMport_box.Size = new System.Drawing.Size(96, 21);
            this.COMport_box.TabIndex = 4;
            // 
            // COM1
            // 
            this.COM1.Text = "COM1";
            // 
            // COM2
            // 
            this.COM2.Text = "COM2";
            // 
            // COM3
            // 
            this.COM3.Text = "COM3";
            // 
            // COM4
            // 
            this.COM4.Text = "COM4";
            // 
            // COM5
            // 
            this.COM5.Text = "COM5";
            // 
            // COM6
            // 
            this.COM6.Text = "COM6";
            // 
            // COM7
            // 
            this.COM7.Text = "COM7";
            // 
            // COM8
            // 
            this.COM8.Text = "COM8";
            // 
            // COM9
            // 
            this.COM9.Text = "COM9";
            // 
            // COM10
            // 
            this.COM10.Text = "COM10";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.rate38400);
            this.groupPanel2.Controls.Add(this.rate19200);
            this.groupPanel2.Controls.Add(this.rate4800);
            this.groupPanel2.Controls.Add(this.rate9600);
            this.groupPanel2.Controls.Add(this.rate2400);
            this.groupPanel2.Controls.Add(this.rate300);
            this.groupPanel2.Location = new System.Drawing.Point(160, 12);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(273, 126);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "Baud Rate";
            // 
            // rate38400
            // 
            this.rate38400.AutoSize = true;
            this.rate38400.Checked = true;
            this.rate38400.Location = new System.Drawing.Point(198, 66);
            this.rate38400.Name = "rate38400";
            this.rate38400.Size = new System.Drawing.Size(55, 17);
            this.rate38400.TabIndex = 5;
            this.rate38400.TabStop = true;
            this.rate38400.Text = "38400";
            this.rate38400.UseVisualStyleBackColor = true;
            this.rate38400.Click += new System.EventHandler(this.rate38400_Click);
            // 
            // rate19200
            // 
            this.rate19200.AutoSize = true;
            this.rate19200.Location = new System.Drawing.Point(107, 66);
            this.rate19200.Name = "rate19200";
            this.rate19200.Size = new System.Drawing.Size(55, 17);
            this.rate19200.TabIndex = 4;
            this.rate19200.Text = "19200";
            this.rate19200.UseVisualStyleBackColor = true;
            this.rate19200.Click += new System.EventHandler(this.rate19200_Click);
            // 
            // rate4800
            // 
            this.rate4800.AutoSize = true;
            this.rate4800.Location = new System.Drawing.Point(198, 18);
            this.rate4800.Name = "rate4800";
            this.rate4800.Size = new System.Drawing.Size(49, 17);
            this.rate4800.TabIndex = 3;
            this.rate4800.Text = "4800";
            this.rate4800.UseVisualStyleBackColor = true;
            this.rate4800.Click += new System.EventHandler(this.rate4800_Click);
            // 
            // rate9600
            // 
            this.rate9600.AutoSize = true;
            this.rate9600.Location = new System.Drawing.Point(15, 66);
            this.rate9600.Name = "rate9600";
            this.rate9600.Size = new System.Drawing.Size(49, 17);
            this.rate9600.TabIndex = 2;
            this.rate9600.Text = "9600";
            this.rate9600.UseVisualStyleBackColor = true;
            this.rate9600.Click += new System.EventHandler(this.rate9600_Click);
            // 
            // rate2400
            // 
            this.rate2400.AutoSize = true;
            this.rate2400.Location = new System.Drawing.Point(107, 18);
            this.rate2400.Name = "rate2400";
            this.rate2400.Size = new System.Drawing.Size(49, 17);
            this.rate2400.TabIndex = 1;
            this.rate2400.Text = "2400";
            this.rate2400.UseVisualStyleBackColor = true;
            this.rate2400.Click += new System.EventHandler(this.rate2400_Click);
            // 
            // rate300
            // 
            this.rate300.AutoSize = true;
            this.rate300.Location = new System.Drawing.Point(15, 18);
            this.rate300.Name = "rate300";
            this.rate300.Size = new System.Drawing.Size(43, 17);
            this.rate300.TabIndex = 0;
            this.rate300.Text = "300";
            this.rate300.UseVisualStyleBackColor = true;
            this.rate300.Click += new System.EventHandler(this.rate300_Click);
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.radioButton3);
            this.groupPanel3.Location = new System.Drawing.Point(14, 144);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(131, 74);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 2;
            this.groupPanel3.Text = "Data Bits";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(33, 18);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(31, 17);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "8";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // groupPanel4
            // 
            this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.radioButton8);
            this.groupPanel4.Controls.Add(this.groupPanel5);
            this.groupPanel4.Location = new System.Drawing.Point(160, 144);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new System.Drawing.Size(131, 74);
            // 
            // 
            // 
            this.groupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel4.Style.BackColorGradientAngle = 90;
            this.groupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderBottomWidth = 1;
            this.groupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderLeftWidth = 1;
            this.groupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderRightWidth = 1;
            this.groupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderTopWidth = 1;
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel4.TabIndex = 3;
            this.groupPanel4.Text = "Parity";
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Checked = true;
            this.radioButton8.Location = new System.Drawing.Point(30, 18);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(51, 17);
            this.radioButton8.TabIndex = 4;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "None";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // groupPanel5
            // 
            this.groupPanel5.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel5.Location = new System.Drawing.Point(160, -11);
            this.groupPanel5.Name = "groupPanel5";
            this.groupPanel5.Size = new System.Drawing.Size(140, 76);
            // 
            // 
            // 
            this.groupPanel5.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel5.Style.BackColorGradientAngle = 90;
            this.groupPanel5.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel5.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderBottomWidth = 1;
            this.groupPanel5.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel5.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderLeftWidth = 1;
            this.groupPanel5.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderRightWidth = 1;
            this.groupPanel5.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderTopWidth = 1;
            this.groupPanel5.Style.CornerDiameter = 4;
            this.groupPanel5.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel5.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel5.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel5.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel5.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel5.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel5.TabIndex = 3;
            this.groupPanel5.Text = "groupPanel5";
            // 
            // groupPanel6
            // 
            this.groupPanel6.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.radioButton9);
            this.groupPanel6.Location = new System.Drawing.Point(302, 144);
            this.groupPanel6.Name = "groupPanel6";
            this.groupPanel6.Size = new System.Drawing.Size(131, 74);
            // 
            // 
            // 
            this.groupPanel6.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel6.Style.BackColorGradientAngle = 90;
            this.groupPanel6.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel6.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderBottomWidth = 1;
            this.groupPanel6.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel6.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderLeftWidth = 1;
            this.groupPanel6.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderRightWidth = 1;
            this.groupPanel6.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderTopWidth = 1;
            this.groupPanel6.Style.CornerDiameter = 4;
            this.groupPanel6.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel6.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel6.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel6.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel6.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel6.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel6.TabIndex = 4;
            this.groupPanel6.Text = "Stop Bits";
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Checked = true;
            this.radioButton9.Location = new System.Drawing.Point(43, 18);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(31, 17);
            this.radioButton9.TabIndex = 0;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "1";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // SRS232_OK
            // 
            this.SRS232_OK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SRS232_OK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SRS232_OK.Location = new System.Drawing.Point(277, 233);
            this.SRS232_OK.Name = "SRS232_OK";
            this.SRS232_OK.Size = new System.Drawing.Size(75, 23);
            this.SRS232_OK.TabIndex = 5;
            this.SRS232_OK.Text = "OK";
            this.SRS232_OK.Click += new System.EventHandler(this.SRS232_OK_Click);
            // 
            // SRS232_CANCEL
            // 
            this.SRS232_CANCEL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SRS232_CANCEL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SRS232_CANCEL.Location = new System.Drawing.Point(358, 233);
            this.SRS232_CANCEL.Name = "SRS232_CANCEL";
            this.SRS232_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.SRS232_CANCEL.TabIndex = 6;
            this.SRS232_CANCEL.Text = "Cancel";
            this.SRS232_CANCEL.Click += new System.EventHandler(this.SRS232_CANCEL_Click);
            // 
            // FormSerial
            // 
            this.ClientSize = new System.Drawing.Size(448, 275);
            this.Controls.Add(this.SRS232_CANCEL);
            this.Controls.Add(this.SRS232_OK);
            this.Controls.Add(this.groupPanel6);
            this.Controls.Add(this.groupPanel4);
            this.Controls.Add(this.groupPanel3);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormSerial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RS-232 Setting";
            this.Shown += new System.EventHandler(this.RS232DLG_Shown);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            this.groupPanel4.ResumeLayout(false);
            this.groupPanel4.PerformLayout();
            this.groupPanel6.ResumeLayout(false);
            this.groupPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        private void rate19200_Click(object sender, EventArgs e)
        {
            baudrate = 0x4b00;
        }

        private void rate2400_Click(object sender, EventArgs e)
        {
            baudrate = 0x960;
        }

        private void rate300_Click(object sender, EventArgs e)
        {
            baudrate = 300;
        }

        private void rate38400_Click(object sender, EventArgs e)
        {
            baudrate = 0x9600;
        }

        private void rate4800_Click(object sender, EventArgs e)
        {
            baudrate = 0x12c0;
        }

        private void rate9600_Click(object sender, EventArgs e)
        {
            baudrate = 0x2580;
        }

        private void RS232DLG_Shown(object sender, EventArgs e)
        {
            var connectManager = ConnectManager.GetConnectManager();
            connectManager.SetConnecType(0);
            COMport_box.Items.Clear();
            var num = connectManager.FindSrc();
            if (num < 1)
            {
                MessageBox.Show("The device is not connected yet，Please connect it first");
            }
            else
            {
                for (var i = 0; i < num; i++)
                {
                    var str = connectManager.resources[i];
                    var length = str.IndexOf(':') - 4;
                    var item = str.Substring(4, length);
                    item = "COM" + item;
                    COMport_box.Items.Insert(i, item);
                }
                COMport_box.SelectedIndex = 0;
            }
        }

        private void SRS232_CANCEL_Click(object sender, EventArgs e)
        {
            ConnectManager.GetConnectManager().SetConnecType(-1);
            base.Close();
        }

        private void SRS232_OK_Click(object sender, EventArgs e)
        {
            var main = FormMain.getstaticmain();
            var comname = COMport_box.SelectedItem.ToString();
            var connectManager = ConnectManager.GetConnectManager();
            connectManager.RS232_Device_Setting(comname, baudrate, databit, Paritytype, stopbittype);
            if (connectManager.OpenSession(comname) == -1)
            {
                MessageBox.Show(
                    "The device can not be open, Please confirm the setting is matching or the cable is connected");
            }
            else
            {
                var startIndex = comname.IndexOf('M') + 1;
                var str2 = comname.Substring(startIndex);
                var sddrstr = "ASRL" + str2 + "::INSTR";
                if (connectManager.device_str_op(sddrstr) == -1)
                {
                    base.Close();
                }
                else
                {
                    main.add_device_toList();
                    main.Updata_UI(0, 0, true);
                    base.Close();
                }
            }
        }
    }
}