namespace ILEditor.Forms
{
    partial class ServiceGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceGenerator));
            this.srvpgmlib = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.procname = new System.Windows.Forms.TextBox();
            this.addproc = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.procedures = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.srvpgmnam = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bndsrc = new System.Windows.Forms.TextBox();
            this.modsrc = new System.Windows.Forms.TextBox();
            this.protspf = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.generate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.isC = new System.Windows.Forms.RadioButton();
            this.isRPG = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // srvpgmlib
            // 
            this.srvpgmlib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.srvpgmlib.Location = new System.Drawing.Point(181, 12);
            this.srvpgmlib.MaxLength = 10;
            this.srvpgmlib.Name = "srvpgmlib";
            this.srvpgmlib.Size = new System.Drawing.Size(95, 20);
            this.srvpgmlib.TabIndex = 0;
            this.srvpgmlib.Text = "*CURLIB";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Objects Library";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.procname);
            this.groupBox1.Controls.Add(this.addproc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.procedures);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.srvpgmnam);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 198);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service Program Information";
            // 
            // procname
            // 
            this.procname.Location = new System.Drawing.Point(9, 168);
            this.procname.MaxLength = 32;
            this.procname.Name = "procname";
            this.procname.Size = new System.Drawing.Size(168, 20);
            this.procname.TabIndex = 9;
            this.procname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProcName_KeyDown);
            // 
            // addproc
            // 
            this.addproc.Location = new System.Drawing.Point(183, 167);
            this.addproc.Name = "addproc";
            this.addproc.Size = new System.Drawing.Size(75, 23);
            this.addproc.TabIndex = 8;
            this.addproc.Text = "Add";
            this.addproc.UseVisualStyleBackColor = true;
            this.addproc.Click += new System.EventHandler(this.AddProc_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Procedure List";
            // 
            // procedures
            // 
            this.procedures.FormattingEnabled = true;
            this.procedures.Location = new System.Drawing.Point(9, 66);
            this.procedures.Name = "procedures";
            this.procedures.Size = new System.Drawing.Size(249, 95);
            this.procedures.TabIndex = 6;
            this.procedures.KeyDown += new System.Windows.Forms.KeyEventHandler(this.procedures_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Service Program Name";
            // 
            // srvpgmnam
            // 
            this.srvpgmnam.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.srvpgmnam.Location = new System.Drawing.Point(163, 18);
            this.srvpgmnam.MaxLength = 10;
            this.srvpgmnam.Name = "srvpgmnam";
            this.srvpgmnam.Size = new System.Drawing.Size(95, 20);
            this.srvpgmnam.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.isRPG);
            this.groupBox2.Controls.Add(this.isC);
            this.groupBox2.Controls.Add(this.bndsrc);
            this.groupBox2.Controls.Add(this.modsrc);
            this.groupBox2.Controls.Add(this.protspf);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 242);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 129);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source Information";
            // 
            // bndsrc
            // 
            this.bndsrc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.bndsrc.Location = new System.Drawing.Point(163, 96);
            this.bndsrc.MaxLength = 10;
            this.bndsrc.Name = "bndsrc";
            this.bndsrc.Size = new System.Drawing.Size(95, 20);
            this.bndsrc.TabIndex = 10;
            this.bndsrc.Text = "QSRVSRC";
            // 
            // modsrc
            // 
            this.modsrc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.modsrc.Location = new System.Drawing.Point(163, 70);
            this.modsrc.MaxLength = 10;
            this.modsrc.Name = "modsrc";
            this.modsrc.Size = new System.Drawing.Size(95, 20);
            this.modsrc.TabIndex = 9;
            this.modsrc.Text = "QRPGLESRC";
            // 
            // protspf
            // 
            this.protspf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.protspf.Location = new System.Drawing.Point(163, 44);
            this.protspf.MaxLength = 10;
            this.protspf.Name = "protspf";
            this.protspf.Size = new System.Drawing.Size(95, 20);
            this.protspf.TabIndex = 8;
            this.protspf.Text = "QRPGLEREF";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Binder Source";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Source SPF";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Prototypes SPF";
            // 
            // generate
            // 
            this.generate.Location = new System.Drawing.Point(201, 389);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(75, 23);
            this.generate.TabIndex = 6;
            this.generate.Text = "Generate";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // isC
            // 
            this.isC.AutoSize = true;
            this.isC.Enabled = false;
            this.isC.Location = new System.Drawing.Point(80, 19);
            this.isC.Name = "isC";
            this.isC.Size = new System.Drawing.Size(32, 17);
            this.isC.TabIndex = 11;
            this.isC.Text = "C";
            this.isC.UseVisualStyleBackColor = true;
            // 
            // isRPG
            // 
            this.isRPG.AutoSize = true;
            this.isRPG.Checked = true;
            this.isRPG.Location = new System.Drawing.Point(132, 19);
            this.isRPG.Name = "isRPG";
            this.isRPG.Size = new System.Drawing.Size(61, 17);
            this.isRPG.TabIndex = 12;
            this.isRPG.TabStop = true;
            this.isRPG.Text = "RPGLE";
            this.isRPG.UseVisualStyleBackColor = true;
            // 
            // ServiceGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 424);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.generate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.srvpgmlib);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServiceGenerator";
            this.Text = "RPG Service Program Generator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox srvpgmlib;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox procedures;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox srvpgmnam;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox bndsrc;
        private System.Windows.Forms.TextBox modsrc;
        private System.Windows.Forms.TextBox protspf;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox procname;
        private System.Windows.Forms.Button addproc;
        private System.Windows.Forms.Button generate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton isRPG;
        private System.Windows.Forms.RadioButton isC;
    }
}