namespace ILEditor.Forms
{
    partial class LicenceKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenceKey));
            this.label1 = new System.Windows.Forms.Label();
            this.key = new System.Windows.Forms.TextBox();
            this.auth = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Licence Key";
            // 
            // key
            // 
            this.key.Location = new System.Drawing.Point(99, 48);
            this.key.MaxLength = 30;
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(180, 20);
            this.key.TabIndex = 1;
            // 
            // auth
            // 
            this.auth.Location = new System.Drawing.Point(204, 103);
            this.auth.Name = "auth";
            this.auth.Size = new System.Drawing.Size(75, 23);
            this.auth.TabIndex = 2;
            this.auth.Text = "Authenticate";
            this.auth.UseVisualStyleBackColor = true;
            this.auth.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Licence key required to use ILEditor";
            // 
            // LicenceKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 138);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.auth);
            this.Controls.Add(this.key);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(307, 177);
            this.MinimumSize = new System.Drawing.Size(307, 177);
            this.Name = "LicenceKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LicenceKey";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox key;
        private System.Windows.Forms.Button auth;
        private System.Windows.Forms.Label label3;
    }
}