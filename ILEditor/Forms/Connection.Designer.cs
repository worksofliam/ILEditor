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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.prntLib = new System.Windows.Forms.TextBox();
            this.prntObj = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.darkMode = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.toolbarSide = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.indent_size)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.label3.Location = new System.Drawing.Point(7, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Username";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(218, 58);
            this.pass.MaxLength = 100;
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(135, 20);
            this.pass.TabIndex = 10;
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(218, 32);
            this.user.MaxLength = 10;
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(135, 20);
            this.user.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Host name";
            // 
            // host
            // 
            this.host.Location = new System.Drawing.Point(218, 6);
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
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Executable";
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
            // tabPage2
            // 
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
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.toolbarSide);
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
            // toolbarSide
            // 
            this.toolbarSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolbarSide.FormattingEnabled = true;
            this.toolbarSide.Items.AddRange(new object[] {
            "Right",
            "Left"});
            this.toolbarSide.Location = new System.Drawing.Point(223, 56);
            this.toolbarSide.Name = "toolbarSide";
            this.toolbarSide.Size = new System.Drawing.Size(130, 21);
            this.toolbarSide.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 59);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Toolbar side";
            // 
            // Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 278);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Connection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection Settings";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.indent_size)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox toolbarSide;
    }
}