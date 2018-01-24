namespace ILEditor.Forms.ProjectWindows
{
    partial class NewProjectWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectWindow));
            this.next = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.projName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ile_dynamic = new System.Windows.Forms.RadioButton();
            this.ile_static = new System.Windows.Forms.RadioButton();
            this.ile_pgm = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.objName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lang_c = new System.Windows.Forms.RadioButton();
            this.lang_rpg = new System.Windows.Forms.RadioButton();
            this.projDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.findDir = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // next
            // 
            this.next.Location = new System.Drawing.Point(519, 383);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(75, 23);
            this.next.TabIndex = 5;
            this.next.Text = "Next";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(12, 383);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // projName
            // 
            this.projName.Location = new System.Drawing.Point(339, 19);
            this.projName.MaxLength = 30;
            this.projName.Name = "projName";
            this.projName.Size = new System.Drawing.Size(100, 20);
            this.projName.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ile_dynamic);
            this.groupBox1.Controls.Add(this.ile_static);
            this.groupBox1.Controls.Add(this.ile_pgm);
            this.groupBox1.Location = new System.Drawing.Point(78, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project Type";
            // 
            // ile_dynamic
            // 
            this.ile_dynamic.AutoSize = true;
            this.ile_dynamic.Location = new System.Drawing.Point(18, 72);
            this.ile_dynamic.Name = "ile_dynamic";
            this.ile_dynamic.Size = new System.Drawing.Size(178, 17);
            this.ile_dynamic.TabIndex = 10;
            this.ile_dynamic.Text = "ILE Dynamic Library (*SRVPGM)";
            this.ile_dynamic.UseVisualStyleBackColor = true;
            // 
            // ile_static
            // 
            this.ile_static.AutoSize = true;
            this.ile_static.Location = new System.Drawing.Point(18, 49);
            this.ile_static.Name = "ile_static";
            this.ile_static.Size = new System.Drawing.Size(151, 17);
            this.ile_static.TabIndex = 10;
            this.ile_static.Text = "ILE Static Libraries (*MOD)";
            this.ile_static.UseVisualStyleBackColor = true;
            // 
            // ile_pgm
            // 
            this.ile_pgm.AutoSize = true;
            this.ile_pgm.Checked = true;
            this.ile_pgm.Location = new System.Drawing.Point(18, 26);
            this.ile_pgm.Name = "ile_pgm";
            this.ile_pgm.Size = new System.Drawing.Size(120, 17);
            this.ile_pgm.TabIndex = 10;
            this.ile_pgm.TabStop = true;
            this.ile_pgm.Text = "ILE Program (*PGM)";
            this.ile_pgm.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Object Name";
            // 
            // objName
            // 
            this.objName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.objName.Location = new System.Drawing.Point(423, 235);
            this.objName.MaxLength = 10;
            this.objName.Name = "objName";
            this.objName.Size = new System.Drawing.Size(100, 20);
            this.objName.TabIndex = 2;
            this.objName.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lang_c);
            this.groupBox2.Controls.Add(this.lang_rpg);
            this.groupBox2.Location = new System.Drawing.Point(78, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Base Language";
            // 
            // lang_c
            // 
            this.lang_c.AutoSize = true;
            this.lang_c.Enabled = false;
            this.lang_c.Location = new System.Drawing.Point(18, 49);
            this.lang_c.Name = "lang_c";
            this.lang_c.Size = new System.Drawing.Size(51, 17);
            this.lang_c.TabIndex = 10;
            this.lang_c.TabStop = true;
            this.lang_c.Text = "ILE C";
            this.lang_c.UseVisualStyleBackColor = true;
            // 
            // lang_rpg
            // 
            this.lang_rpg.AutoSize = true;
            this.lang_rpg.Location = new System.Drawing.Point(18, 26);
            this.lang_rpg.Name = "lang_rpg";
            this.lang_rpg.Size = new System.Drawing.Size(67, 17);
            this.lang_rpg.TabIndex = 10;
            this.lang_rpg.TabStop = true;
            this.lang_rpg.Text = "ILE RPG";
            this.lang_rpg.UseVisualStyleBackColor = true;
            // 
            // projDir
            // 
            this.projDir.Location = new System.Drawing.Point(9, 58);
            this.projDir.MaxLength = 256;
            this.projDir.Name = "projDir";
            this.projDir.ReadOnly = true;
            this.projDir.Size = new System.Drawing.Size(357, 20);
            this.projDir.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Directory";
            // 
            // findDir
            // 
            this.findDir.Location = new System.Drawing.Point(372, 58);
            this.findDir.Name = "findDir";
            this.findDir.Size = new System.Drawing.Size(67, 20);
            this.findDir.TabIndex = 4;
            this.findDir.Text = "Find";
            this.findDir.UseVisualStyleBackColor = true;
            this.findDir.Click += new System.EventHandler(this.findDir_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.findDir);
            this.groupBox3.Controls.Add(this.projName);
            this.groupBox3.Controls.Add(this.projDir);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(78, 263);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(445, 91);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Project Information";
            // 
            // NewProjectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 424);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.objName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.next);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NewProjectWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New ILE Project";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox projName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ile_dynamic;
        private System.Windows.Forms.RadioButton ile_static;
        private System.Windows.Forms.RadioButton ile_pgm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox objName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton lang_c;
        private System.Windows.Forms.RadioButton lang_rpg;
        private System.Windows.Forms.TextBox projDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button findDir;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}