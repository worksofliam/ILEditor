namespace ILEditor.Forms
{
    partial class JobSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobSettings));
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.curlib = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.libraryListBox = new System.Windows.Forms.ListBox();
            this.newLibrary = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.delButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.homeDir = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.buildLib = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(270, 283);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(58, 21);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(212, 283);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(54, 21);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // curlib
            // 
            this.curlib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.curlib.Location = new System.Drawing.Point(106, 140);
            this.curlib.Margin = new System.Windows.Forms.Padding(2);
            this.curlib.MaxLength = 10;
            this.curlib.Name = "curlib";
            this.curlib.Size = new System.Drawing.Size(79, 20);
            this.curlib.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 143);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Your Current Library";
            // 
            // libraryListBox
            // 
            this.libraryListBox.FormattingEnabled = true;
            this.libraryListBox.Location = new System.Drawing.Point(5, 22);
            this.libraryListBox.Margin = new System.Windows.Forms.Padding(2);
            this.libraryListBox.Name = "libraryListBox";
            this.libraryListBox.Size = new System.Drawing.Size(179, 108);
            this.libraryListBox.TabIndex = 8;
            this.libraryListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // newLibrary
            // 
            this.newLibrary.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.newLibrary.Location = new System.Drawing.Point(194, 59);
            this.newLibrary.Margin = new System.Windows.Forms.Padding(2);
            this.newLibrary.MaxLength = 10;
            this.newLibrary.Name = "newLibrary";
            this.newLibrary.Size = new System.Drawing.Size(116, 20);
            this.newLibrary.TabIndex = 9;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(194, 84);
            this.addButton.Margin = new System.Windows.Forms.Padding(2);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(116, 21);
            this.addButton.TabIndex = 10;
            this.addButton.Text = "Add library";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 4;
            // 
            // delButton
            // 
            this.delButton.Location = new System.Drawing.Point(194, 109);
            this.delButton.Margin = new System.Windows.Forms.Padding(2);
            this.delButton.Name = "delButton";
            this.delButton.Size = new System.Drawing.Size(116, 21);
            this.delButton.TabIndex = 12;
            this.delButton.Text = "Delete selected";
            this.delButton.UseVisualStyleBackColor = true;
            this.delButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.libraryListBox);
            this.groupBox1.Controls.Add(this.delButton);
            this.groupBox1.Controls.Add(this.curlib);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.newLibrary);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 170);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Library List";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.homeDir);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.buildLib);
            this.groupBox2.Location = new System.Drawing.Point(12, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(316, 79);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IFS Settings";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 22);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 13);
            this.label21.TabIndex = 25;
            this.label21.Text = "Home Directory";
            // 
            // homeDir
            // 
            this.homeDir.Location = new System.Drawing.Point(106, 19);
            this.homeDir.MaxLength = 256;
            this.homeDir.Name = "homeDir";
            this.homeDir.Size = new System.Drawing.Size(204, 20);
            this.homeDir.TabIndex = 24;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 13);
            this.label17.TabIndex = 22;
            this.label17.Text = "Build Library";
            // 
            // buildLib
            // 
            this.buildLib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.buildLib.Location = new System.Drawing.Point(210, 48);
            this.buildLib.MaxLength = 10;
            this.buildLib.Name = "buildLib";
            this.buildLib.Size = new System.Drawing.Size(100, 20);
            this.buildLib.TabIndex = 23;
            // 
            // JobSettings
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(340, 315);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JobSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Job Settings";
            this.Load += new System.EventHandler(this.libraryList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox curlib;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox libraryListBox;
        private System.Windows.Forms.TextBox newLibrary;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button delButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox homeDir;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox buildLib;
    }
}