namespace ILEditor.Forms
{
    partial class Connection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Connection));
            this.save = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pass = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.host = new System.Windows.Forms.TextBox();
            this.selectedFont = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.highlight_line = new System.Windows.Forms.ComboBox();
            this.show_spaces = new System.Windows.Forms.ComboBox();
            this.indent_size = new System.Windows.Forms.NumericUpDown();
            this.cur_size = new System.Windows.Forms.TextBox();
            this.findACS = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.validACS = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.systemInfo = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ftpes = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.infoBox = new System.Windows.Forms.RichTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.dataConnectionType = new System.Windows.Forms.ComboBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.homeDir = new System.Windows.Forms.TextBox();
            this.buildLib = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.fetchJobLog = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.prntLib = new System.Windows.Forms.TextBox();
            this.prntObj = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.darkMode = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.conv_tabs = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.indent_size)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(282, 243);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 13;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Username";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(218, 62);
            this.pass.MaxLength = 100;
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(135, 20);
            this.pass.TabIndex = 10;
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(218, 36);
            this.user.MaxLength = 10;
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(135, 20);
            this.user.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Host name";
            // 
            // host
            // 
            this.host.Location = new System.Drawing.Point(218, 10);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(135, 20);
            this.host.TabIndex = 7;
            // 
            // selectedFont
            // 
            this.selectedFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectedFont.FormattingEnabled = true;
            this.selectedFont.Items.AddRange(new object[] {
            "Consolas",
            "Courier New",
            "Lucida Console",
            "Lucida Sans Typewriter"});
            this.selectedFont.Location = new System.Drawing.Point(220, 6);
            this.selectedFont.Name = "selectedFont";
            this.selectedFont.Size = new System.Drawing.Size(135, 21);
            this.selectedFont.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Font";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Highlight editing line";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Show spaces (by DOT)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Indent Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Current Zoom";
            // 
            // highlight_line
            // 
            this.highlight_line.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.highlight_line.FormattingEnabled = true;
            this.highlight_line.Items.AddRange(new object[] {
            "true",
            "false"});
            this.highlight_line.Location = new System.Drawing.Point(220, 110);
            this.highlight_line.Name = "highlight_line";
            this.highlight_line.Size = new System.Drawing.Size(135, 21);
            this.highlight_line.TabIndex = 3;
            // 
            // show_spaces
            // 
            this.show_spaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.show_spaces.FormattingEnabled = true;
            this.show_spaces.Items.AddRange(new object[] {
            "true",
            "false"});
            this.show_spaces.Location = new System.Drawing.Point(220, 83);
            this.show_spaces.Name = "show_spaces";
            this.show_spaces.Size = new System.Drawing.Size(135, 21);
            this.show_spaces.TabIndex = 2;
            // 
            // indent_size
            // 
            this.indent_size.Location = new System.Drawing.Point(220, 57);
            this.indent_size.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.indent_size.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.indent_size.Name = "indent_size";
            this.indent_size.Size = new System.Drawing.Size(135, 20);
            this.indent_size.TabIndex = 1;
            this.indent_size.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cur_size
            // 
            this.cur_size.Location = new System.Drawing.Point(220, 32);
            this.cur_size.MaxLength = 10;
            this.cur_size.Name = "cur_size";
            this.cur_size.ReadOnly = true;
            this.cur_size.Size = new System.Drawing.Size(135, 20);
            this.cur_size.TabIndex = 0;
            // 
            // findACS
            // 
            this.findACS.Location = new System.Drawing.Point(278, 6);
            this.findACS.Name = "findACS";
            this.findACS.Size = new System.Drawing.Size(75, 23);
            this.findACS.TabIndex = 2;
            this.findACS.Text = "Find ACS";
            this.findACS.UseVisualStyleBackColor = true;
            this.findACS.Click += new System.EventHandler(this.findACS_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "ACS executable";
            // 
            // validACS
            // 
            this.validACS.AutoSize = true;
            this.validACS.Enabled = false;
            this.validACS.Location = new System.Drawing.Point(223, 10);
            this.validACS.Name = "validACS";
            this.validACS.Size = new System.Drawing.Size(49, 17);
            this.validACS.TabIndex = 0;
            this.validACS.Text = "Valid";
            this.validACS.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(369, 237);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.systemInfo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.pass);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.host);
            this.tabPage1.Controls.Add(this.user);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(361, 211);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connection";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 92);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(303, 13);
            this.label18.TabIndex = 16;
            this.label18.Text = "Leave password blank to be prompted for it before connecting.";
            // 
            // systemInfo
            // 
            this.systemInfo.AutoSize = true;
            this.systemInfo.Location = new System.Drawing.Point(8, 116);
            this.systemInfo.Name = "systemInfo";
            this.systemInfo.Size = new System.Drawing.Size(57, 13);
            this.systemInfo.TabIndex = 15;
            this.systemInfo.Text = "systemInfo";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.linkLabel1);
            this.tabPage6.Controls.Add(this.ftpes);
            this.tabPage6.Controls.Add(this.label20);
            this.tabPage6.Controls.Add(this.infoBox);
            this.tabPage6.Controls.Add(this.label19);
            this.tabPage6.Controls.Add(this.dataConnectionType);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(361, 211);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "FTP";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(8, 19);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(114, 13);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Use FTP SSL (Explicit)";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ftpes
            // 
            this.ftpes.AutoSize = true;
            this.ftpes.Location = new System.Drawing.Point(293, 18);
            this.ftpes.Name = "ftpes";
            this.ftpes.Size = new System.Drawing.Size(60, 17);
            this.ftpes.TabIndex = 17;
            this.ftpes.Text = "FTPES";
            this.ftpes.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 189);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(216, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "Changing these settings will require a restart.";
            // 
            // infoBox
            // 
            this.infoBox.Location = new System.Drawing.Point(8, 68);
            this.infoBox.MaxLength = 1000;
            this.infoBox.Name = "infoBox";
            this.infoBox.ReadOnly = true;
            this.infoBox.Size = new System.Drawing.Size(347, 118);
            this.infoBox.TabIndex = 2;
            this.infoBox.Text = "";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 45);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(114, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "Data Connection Type";
            // 
            // dataConnectionType
            // 
            this.dataConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataConnectionType.FormattingEnabled = true;
            this.dataConnectionType.Items.AddRange(new object[] {
            "AutoPassive",
            "AutoActive",
            "EPRT",
            "EPSV",
            "PASV",
            "PASVEX",
            "PORT"});
            this.dataConnectionType.Location = new System.Drawing.Point(232, 41);
            this.dataConnectionType.Name = "dataConnectionType";
            this.dataConnectionType.Size = new System.Drawing.Size(121, 21);
            this.dataConnectionType.TabIndex = 0;
            this.dataConnectionType.SelectedIndexChanged += new System.EventHandler(this.infoBox_SelectionChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label21);
            this.tabPage5.Controls.Add(this.homeDir);
            this.tabPage5.Controls.Add(this.buildLib);
            this.tabPage5.Controls.Add(this.label17);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(361, 211);
            this.tabPage5.TabIndex = 6;
            this.tabPage5.Text = "IFS";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 17);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 13);
            this.label21.TabIndex = 21;
            this.label21.Text = "Home Directory";
            // 
            // homeDir
            // 
            this.homeDir.Location = new System.Drawing.Point(125, 14);
            this.homeDir.MaxLength = 256;
            this.homeDir.Name = "homeDir";
            this.homeDir.Size = new System.Drawing.Size(228, 20);
            this.homeDir.TabIndex = 20;
            // 
            // buildLib
            // 
            this.buildLib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.buildLib.Location = new System.Drawing.Point(253, 40);
            this.buildLib.MaxLength = 10;
            this.buildLib.Name = "buildLib";
            this.buildLib.Size = new System.Drawing.Size(100, 20);
            this.buildLib.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 43);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "Build Library";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.conv_tabs);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.fetchJobLog);
            this.tabPage2.Controls.Add(this.selectedFont);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.cur_size);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.indent_size);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.show_spaces);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.highlight_line);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(361, 211);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 168);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(130, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "Show Job Log on Compile";
            // 
            // fetchJobLog
            // 
            this.fetchJobLog.AutoSize = true;
            this.fetchJobLog.Location = new System.Drawing.Point(220, 167);
            this.fetchJobLog.Name = "fetchJobLog";
            this.fetchJobLog.Size = new System.Drawing.Size(94, 17);
            this.fetchJobLog.TabIndex = 15;
            this.fetchJobLog.Text = "Fetch Job Log";
            this.fetchJobLog.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(361, 211);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Spool Listing";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.prntLib);
            this.groupBox1.Controls.Add(this.prntObj);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 88);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output Queue";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(197, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Will only show spool files for current user";
            // 
            // prntLib
            // 
            this.prntLib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.prntLib.Location = new System.Drawing.Point(239, 19);
            this.prntLib.MaxLength = 10;
            this.prntLib.Name = "prntLib";
            this.prntLib.Size = new System.Drawing.Size(100, 20);
            this.prntLib.TabIndex = 3;
            // 
            // prntObj
            // 
            this.prntObj.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.prntObj.Location = new System.Drawing.Point(239, 41);
            this.prntObj.MaxLength = 10;
            this.prntObj.Name = "prntObj";
            this.prntObj.Size = new System.Drawing.Size(100, 20);
            this.prntObj.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Library";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Object";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.darkMode);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.findACS);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.validACS);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(361, 211);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "ILEditor";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 186);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(193, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Some of these settings require a restart.";
            // 
            // darkMode
            // 
            this.darkMode.AutoSize = true;
            this.darkMode.Location = new System.Drawing.Point(223, 33);
            this.darkMode.Name = "darkMode";
            this.darkMode.Size = new System.Drawing.Size(101, 17);
            this.darkMode.TabIndex = 4;
            this.darkMode.Text = "Use Dark Mode";
            this.darkMode.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Dark Mode";
            // 
            // conv_tabs
            // 
            this.conv_tabs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conv_tabs.FormattingEnabled = true;
            this.conv_tabs.Items.AddRange(new object[] {
            "true",
            "false"});
            this.conv_tabs.Location = new System.Drawing.Point(220, 137);
            this.conv_tabs.Name = "conv_tabs";
            this.conv_tabs.Size = new System.Drawing.Size(135, 21);
            this.conv_tabs.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 140);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Convert tabs to spaces";
            // 
            // Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 274);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Connection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection Settings";
            ((System.ComponentModel.ISupportInitialize)(this.indent_size)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox host;
        private System.Windows.Forms.ComboBox show_spaces;
        private System.Windows.Forms.NumericUpDown indent_size;
        private System.Windows.Forms.TextBox cur_size;
        private System.Windows.Forms.ComboBox highlight_line;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox validACS;
        private System.Windows.Forms.Button findACS;
        private System.Windows.Forms.ComboBox selectedFont;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox prntObj;
        private System.Windows.Forms.TextBox prntLib;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox darkMode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ComboBox dataConnectionType;
        private System.Windows.Forms.RichTextBox infoBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox ftpes;
        private System.Windows.Forms.Label systemInfo;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox homeDir;
        private System.Windows.Forms.TextBox buildLib;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox fetchJobLog;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox conv_tabs;
    }
}