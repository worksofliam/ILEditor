namespace ILEditor.Forms
{
    partial class FindReferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindReferences));
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lib = new System.Windows.Forms.TextBox();
            this.pgm = new System.Windows.Forms.TextBox();
            this.create = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.style = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Library";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Program";
            // 
            // lib
            // 
            this.lib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lib.Location = new System.Drawing.Point(141, 12);
            this.lib.MaxLength = 10;
            this.lib.Name = "lib";
            this.lib.Size = new System.Drawing.Size(100, 20);
            this.lib.TabIndex = 13;
            // 
            // pgm
            // 
            this.pgm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.pgm.Location = new System.Drawing.Point(141, 38);
            this.pgm.MaxLength = 10;
            this.pgm.Name = "pgm";
            this.pgm.Size = new System.Drawing.Size(100, 20);
            this.pgm.TabIndex = 14;
            this.pgm.Text = "*ALL";
            // 
            // create
            // 
            this.create.Location = new System.Drawing.Point(153, 97);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(88, 23);
            this.create.TabIndex = 15;
            this.create.Text = "Create";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.fetch_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(12, 97);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(88, 23);
            this.cancel.TabIndex = 16;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // style
            // 
            this.style.FormattingEnabled = true;
            this.style.Items.AddRange(new object[] {
            "breadthfirst",
            "circle",
            "grid"});
            this.style.Location = new System.Drawing.Point(141, 64);
            this.style.Name = "style";
            this.style.Size = new System.Drawing.Size(100, 21);
            this.style.TabIndex = 17;
            this.style.Text = "breadthfirst";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Diagram Style";
            // 
            // FindReferences
            // 
            this.AcceptButton = this.create;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(253, 132);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.style);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.create);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lib);
            this.Controls.Add(this.pgm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FindReferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find References";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox lib;
        private System.Windows.Forms.TextBox pgm;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.ComboBox style;
        private System.Windows.Forms.Label label2;
    }
}