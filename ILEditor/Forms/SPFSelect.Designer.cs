namespace ILEditor.Forms
{
    partial class SPFSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPFSelect));
            this.spf = new System.Windows.Forms.TextBox();
            this.lib = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.select = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // spf
            // 
            this.spf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.spf.Location = new System.Drawing.Point(160, 38);
            this.spf.MaxLength = 10;
            this.spf.Name = "spf";
            this.spf.Size = new System.Drawing.Size(100, 20);
            this.spf.TabIndex = 8;
            // 
            // lib
            // 
            this.lib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lib.Location = new System.Drawing.Point(160, 12);
            this.lib.MaxLength = 10;
            this.lib.Name = "lib";
            this.lib.Size = new System.Drawing.Size(100, 20);
            this.lib.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Source-Physical File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Library";
            // 
            // select
            // 
            this.select.Location = new System.Drawing.Point(160, 75);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(100, 23);
            this.select.TabIndex = 9;
            this.select.Text = "Select";
            this.select.UseVisualStyleBackColor = true;
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(12, 75);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(100, 23);
            this.cancel.TabIndex = 10;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // SPFSelect
            // 
            this.AcceptButton = this.select;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(272, 108);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.select);
            this.Controls.Add(this.spf);
            this.Controls.Add(this.lib);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SPFSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Source-Physical File Select";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox spf;
        private System.Windows.Forms.TextBox lib;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button select;
        private System.Windows.Forms.Button cancel;
    }
}