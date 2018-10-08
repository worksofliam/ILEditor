namespace ILEditor.Forms
{
    partial class UpdateProgram
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateProgram));
            this.pgmTypeText = new System.Windows.Forms.Label();
            this.modules = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.pgm = new System.Windows.Forms.TextBox();
            this.customModules = new System.Windows.Forms.ListBox();
            this.customModule = new System.Windows.Forms.TextBox();
            this.addMod = new System.Windows.Forms.Button();
            this.delMod = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.update = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.binderSrcBox = new System.Windows.Forms.GroupBox();
            this.bndMbr = new System.Windows.Forms.TextBox();
            this.bndSpf = new System.Windows.Forms.TextBox();
            this.bndLib = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.updSrvSrc = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bndDirs = new System.Windows.Forms.ListBox();
            this.bndDir = new System.Windows.Forms.TextBox();
            this.delBnd = new System.Windows.Forms.Button();
            this.addBnd = new System.Windows.Forms.Button();
            this.actgrp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.binderSrcBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgmTypeText
            // 
            this.pgmTypeText.AutoSize = true;
            this.pgmTypeText.Location = new System.Drawing.Point(12, 18);
            this.pgmTypeText.Name = "pgmTypeText";
            this.pgmTypeText.Size = new System.Drawing.Size(46, 13);
            this.pgmTypeText.TabIndex = 0;
            this.pgmTypeText.Text = "Program";
            // 
            // modules
            // 
            this.modules.CheckBoxes = true;
            this.modules.Location = new System.Drawing.Point(6, 23);
            this.modules.Name = "modules";
            this.modules.Size = new System.Drawing.Size(299, 125);
            this.modules.SmallImageList = this.imageList1;
            this.modules.TabIndex = 1;
            this.modules.UseCompatibleStateImageBehavior = false;
            this.modules.View = System.Windows.Forms.View.List;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cubedark.png");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Module Selection";
            // 
            // pgm
            // 
            this.pgm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.pgm.Location = new System.Drawing.Point(207, 15);
            this.pgm.MaxLength = 21;
            this.pgm.Name = "pgm";
            this.pgm.ReadOnly = true;
            this.pgm.Size = new System.Drawing.Size(119, 20);
            this.pgm.TabIndex = 3;
            // 
            // customModules
            // 
            this.customModules.FormattingEnabled = true;
            this.customModules.Location = new System.Drawing.Point(152, 19);
            this.customModules.Name = "customModules";
            this.customModules.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.customModules.Size = new System.Drawing.Size(129, 82);
            this.customModules.TabIndex = 4;
            this.customModules.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customModules_KeyDown);
            // 
            // customModule
            // 
            this.customModule.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.customModule.Location = new System.Drawing.Point(27, 25);
            this.customModule.MaxLength = 21;
            this.customModule.Name = "customModule";
            this.customModule.Size = new System.Drawing.Size(119, 20);
            this.customModule.TabIndex = 5;
            this.customModule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customModule_KeyDown);
            // 
            // addMod
            // 
            this.addMod.Location = new System.Drawing.Point(27, 48);
            this.addMod.Name = "addMod";
            this.addMod.Size = new System.Drawing.Size(119, 23);
            this.addMod.TabIndex = 6;
            this.addMod.Text = "Add Module";
            this.addMod.UseVisualStyleBackColor = true;
            this.addMod.Click += new System.EventHandler(this.addMod_Click);
            // 
            // delMod
            // 
            this.delMod.Location = new System.Drawing.Point(27, 73);
            this.delMod.Name = "delMod";
            this.delMod.Size = new System.Drawing.Size(119, 23);
            this.delMod.TabIndex = 7;
            this.delMod.Text = "Delete Selected";
            this.delMod.UseVisualStyleBackColor = true;
            this.delMod.Click += new System.EventHandler(this.delMod_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.modules);
            this.groupBox1.Location = new System.Drawing.Point(15, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 154);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modules to Update";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.customModules);
            this.groupBox2.Controls.Add(this.customModule);
            this.groupBox2.Controls.Add(this.delMod);
            this.groupBox2.Controls.Add(this.addMod);
            this.groupBox2.Location = new System.Drawing.Point(15, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 113);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Custom Modules to Add";
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(571, 293);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 10;
            this.update.Text = "Update";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(332, 293);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 11;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // binderSrcBox
            // 
            this.binderSrcBox.Controls.Add(this.updSrvSrc);
            this.binderSrcBox.Controls.Add(this.bndMbr);
            this.binderSrcBox.Controls.Add(this.bndSpf);
            this.binderSrcBox.Controls.Add(this.label4);
            this.binderSrcBox.Controls.Add(this.bndLib);
            this.binderSrcBox.Controls.Add(this.label1);
            this.binderSrcBox.Controls.Add(this.label3);
            this.binderSrcBox.Location = new System.Drawing.Point(332, 162);
            this.binderSrcBox.Name = "binderSrcBox";
            this.binderSrcBox.Size = new System.Drawing.Size(311, 122);
            this.binderSrcBox.TabIndex = 12;
            this.binderSrcBox.TabStop = false;
            this.binderSrcBox.Text = "Binder Source (for *SRVPGM)";
            // 
            // bndMbr
            // 
            this.bndMbr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.bndMbr.Enabled = false;
            this.bndMbr.Location = new System.Drawing.Point(205, 94);
            this.bndMbr.MaxLength = 10;
            this.bndMbr.Name = "bndMbr";
            this.bndMbr.Size = new System.Drawing.Size(100, 20);
            this.bndMbr.TabIndex = 18;
            // 
            // bndSpf
            // 
            this.bndSpf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.bndSpf.Enabled = false;
            this.bndSpf.Location = new System.Drawing.Point(205, 68);
            this.bndSpf.MaxLength = 10;
            this.bndSpf.Name = "bndSpf";
            this.bndSpf.Size = new System.Drawing.Size(100, 20);
            this.bndSpf.TabIndex = 17;
            // 
            // bndLib
            // 
            this.bndLib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.bndLib.Enabled = false;
            this.bndLib.Location = new System.Drawing.Point(205, 42);
            this.bndLib.MaxLength = 10;
            this.bndLib.Name = "bndLib";
            this.bndLib.Size = new System.Drawing.Size(100, 20);
            this.bndLib.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Member Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Source-Physical File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Library";
            // 
            // updSrvSrc
            // 
            this.updSrvSrc.AutoSize = true;
            this.updSrvSrc.Checked = true;
            this.updSrvSrc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.updSrvSrc.Location = new System.Drawing.Point(9, 22);
            this.updSrvSrc.Name = "updSrvSrc";
            this.updSrvSrc.Size = new System.Drawing.Size(87, 17);
            this.updSrvSrc.TabIndex = 19;
            this.updSrvSrc.Text = "Don\'t update";
            this.updSrvSrc.UseVisualStyleBackColor = true;
            this.updSrvSrc.CheckedChanged += new System.EventHandler(this.updSrvSrc_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bndDir);
            this.groupBox3.Controls.Add(this.bndDirs);
            this.groupBox3.Controls.Add(this.delBnd);
            this.groupBox3.Controls.Add(this.addBnd);
            this.groupBox3.Location = new System.Drawing.Point(332, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(311, 114);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Binding Directories";
            // 
            // bndDirs
            // 
            this.bndDirs.FormattingEnabled = true;
            this.bndDirs.Location = new System.Drawing.Point(153, 19);
            this.bndDirs.Name = "bndDirs";
            this.bndDirs.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.bndDirs.Size = new System.Drawing.Size(129, 82);
            this.bndDirs.TabIndex = 8;
            this.bndDirs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bndDirs_KeyDown);
            // 
            // bndDir
            // 
            this.bndDir.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.bndDir.Location = new System.Drawing.Point(28, 27);
            this.bndDir.MaxLength = 21;
            this.bndDir.Name = "bndDir";
            this.bndDir.Size = new System.Drawing.Size(119, 20);
            this.bndDir.TabIndex = 8;
            this.bndDir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bndDir_KeyDown);
            // 
            // delBnd
            // 
            this.delBnd.Location = new System.Drawing.Point(28, 75);
            this.delBnd.Name = "delBnd";
            this.delBnd.Size = new System.Drawing.Size(119, 23);
            this.delBnd.TabIndex = 10;
            this.delBnd.Text = "Delete Selected";
            this.delBnd.UseVisualStyleBackColor = true;
            this.delBnd.Click += new System.EventHandler(this.delBnd_Click);
            // 
            // addBnd
            // 
            this.addBnd.Location = new System.Drawing.Point(28, 50);
            this.addBnd.Name = "addBnd";
            this.addBnd.Size = new System.Drawing.Size(119, 23);
            this.addBnd.TabIndex = 9;
            this.addBnd.Text = "Add Binding Directory";
            this.addBnd.UseVisualStyleBackColor = true;
            this.addBnd.Click += new System.EventHandler(this.addBnd_Click);
            // 
            // actgrp
            // 
            this.actgrp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.actgrp.Location = new System.Drawing.Point(527, 15);
            this.actgrp.MaxLength = 10;
            this.actgrp.Name = "actgrp";
            this.actgrp.Size = new System.Drawing.Size(119, 20);
            this.actgrp.TabIndex = 20;
            this.actgrp.Text = "*SAME";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(333, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Activation Group";
            // 
            // UpdateProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 325);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.actgrp);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.binderSrcBox);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.update);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pgm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pgmTypeText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateProgram";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Program";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.binderSrcBox.ResumeLayout(false);
            this.binderSrcBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pgmTypeText;
        private System.Windows.Forms.ListView modules;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox pgm;
        private System.Windows.Forms.ListBox customModules;
        private System.Windows.Forms.TextBox customModule;
        private System.Windows.Forms.Button addMod;
        private System.Windows.Forms.Button delMod;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.GroupBox binderSrcBox;
        private System.Windows.Forms.TextBox bndMbr;
        private System.Windows.Forms.TextBox bndSpf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox bndLib;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox updSrvSrc;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox bndDirs;
        private System.Windows.Forms.TextBox bndDir;
        private System.Windows.Forms.Button delBnd;
        private System.Windows.Forms.Button addBnd;
        private System.Windows.Forms.TextBox actgrp;
        private System.Windows.Forms.Label label5;
    }
}