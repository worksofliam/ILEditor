namespace ILEditor.Forms
{
    partial class NewSPF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewSPF));
            this.spf = new System.Windows.Forms.TextBox();
            this.lib = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rcdLen = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.create = new System.Windows.Forms.Button();
            this.ccsid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rcdLen)).BeginInit();
            this.SuspendLayout();
            // 
            // spf
            // 
            this.spf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.spf.Location = new System.Drawing.Point(156, 36);
            this.spf.MaxLength = 10;
            this.spf.Name = "spf";
            this.spf.Size = new System.Drawing.Size(100, 20);
            this.spf.TabIndex = 8;
            // 
            // lib
            // 
            this.lib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lib.Location = new System.Drawing.Point(156, 10);
            this.lib.MaxLength = 10;
            this.lib.Name = "lib";
            this.lib.Size = new System.Drawing.Size(100, 20);
            this.lib.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Source-Physical File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Library";
            // 
            // rcdLen
            // 
            this.rcdLen.Location = new System.Drawing.Point(156, 62);
            this.rcdLen.Maximum = new decimal(new int[] {
            32766,
            0,
            0,
            0});
            this.rcdLen.Minimum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.rcdLen.Name = "rcdLen";
            this.rcdLen.Size = new System.Drawing.Size(100, 20);
            this.rcdLen.TabIndex = 9;
            this.rcdLen.Value = new decimal(new int[] {
            112,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Record Length";
            // 
            // create
            // 
            this.create.Location = new System.Drawing.Point(181, 127);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(75, 23);
            this.create.TabIndex = 17;
            this.create.Text = "Create";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.create_Click);
            // 
            // ccsid
            // 
            this.ccsid.Location = new System.Drawing.Point(156, 88);
            this.ccsid.MaxLength = 5;
            this.ccsid.Name = "ccsid";
            this.ccsid.Size = new System.Drawing.Size(100, 20);
            this.ccsid.TabIndex = 19;
            this.ccsid.Text = "*JOB";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "CCSID";
            // 
            // NewSPF
            // 
            this.AcceptButton = this.create;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 163);
            this.Controls.Add(this.ccsid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.create);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rcdLen);
            this.Controls.Add(this.spf);
            this.Controls.Add(this.lib);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewSPF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Source-Physical File";
            ((System.ComponentModel.ISupportInitialize)(this.rcdLen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox spf;
        private System.Windows.Forms.TextBox lib;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown rcdLen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.TextBox ccsid;
        private System.Windows.Forms.Label label4;
    }
}