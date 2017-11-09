namespace ILEditor.Forms
{
    partial class MemberCompareSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberCompareSelect));
            this.mbrA = new System.Windows.Forms.TextBox();
            this.spfA = new System.Windows.Forms.TextBox();
            this.libA = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mbrB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.libB = new System.Windows.Forms.TextBox();
            this.spfB = new System.Windows.Forms.TextBox();
            this.compare = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mbrA
            // 
            this.mbrA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mbrA.Location = new System.Drawing.Point(166, 71);
            this.mbrA.MaxLength = 10;
            this.mbrA.Name = "mbrA";
            this.mbrA.Size = new System.Drawing.Size(100, 20);
            this.mbrA.TabIndex = 11;
            // 
            // spfA
            // 
            this.spfA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.spfA.Location = new System.Drawing.Point(166, 45);
            this.spfA.MaxLength = 10;
            this.spfA.Name = "spfA";
            this.spfA.Size = new System.Drawing.Size(100, 20);
            this.spfA.TabIndex = 10;
            // 
            // libA
            // 
            this.libA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.libA.Location = new System.Drawing.Point(166, 19);
            this.libA.MaxLength = 10;
            this.libA.Name = "libA";
            this.libA.Size = new System.Drawing.Size(100, 20);
            this.libA.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Member Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Source-Physical File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Library";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.mbrA);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.libA);
            this.groupBox1.Controls.Add(this.spfA);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Member One";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.mbrB);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.libB);
            this.groupBox2.Controls.Add(this.spfB);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 100);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Member Two";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Library";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Member Name";
            // 
            // mbrB
            // 
            this.mbrB.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mbrB.Location = new System.Drawing.Point(166, 71);
            this.mbrB.MaxLength = 10;
            this.mbrB.Name = "mbrB";
            this.mbrB.Size = new System.Drawing.Size(100, 20);
            this.mbrB.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Source-Physical File";
            // 
            // libB
            // 
            this.libB.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.libB.Location = new System.Drawing.Point(166, 19);
            this.libB.MaxLength = 10;
            this.libB.Name = "libB";
            this.libB.Size = new System.Drawing.Size(100, 20);
            this.libB.TabIndex = 9;
            // 
            // spfB
            // 
            this.spfB.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.spfB.Location = new System.Drawing.Point(166, 45);
            this.spfB.MaxLength = 10;
            this.spfB.Name = "spfB";
            this.spfB.Size = new System.Drawing.Size(100, 20);
            this.spfB.TabIndex = 10;
            // 
            // compare
            // 
            this.compare.Location = new System.Drawing.Point(209, 224);
            this.compare.Name = "compare";
            this.compare.Size = new System.Drawing.Size(75, 23);
            this.compare.TabIndex = 14;
            this.compare.Text = "Compare";
            this.compare.UseVisualStyleBackColor = true;
            this.compare.Click += new System.EventHandler(this.compare_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(12, 224);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 15;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // MemberCompareSelect
            // 
            this.AcceptButton = this.compare;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(296, 257);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.compare);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MemberCompareSelect";
            this.Text = "Member Compare";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox mbrA;
        private System.Windows.Forms.TextBox spfA;
        private System.Windows.Forms.TextBox libA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mbrB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox libB;
        private System.Windows.Forms.TextBox spfB;
        private System.Windows.Forms.Button compare;
        private System.Windows.Forms.Button cancel;
    }
}