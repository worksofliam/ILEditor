namespace ILEditor.Forms
{
    partial class NewSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewSystem));
            this.host = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.user = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.alias = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ftpes = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // host
            // 
            this.host.Location = new System.Drawing.Point(146, 42);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(135, 20);
            this.host.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Host name / IP address";
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(146, 68);
            this.user.MaxLength = 10;
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(135, 20);
            this.user.TabIndex = 2;
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(146, 94);
            this.pass.MaxLength = 100;
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(135, 20);
            this.pass.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(206, 169);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 4;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // alias
            // 
            this.alias.Location = new System.Drawing.Point(146, 16);
            this.alias.MaxLength = 16;
            this.alias.Name = "alias";
            this.alias.Size = new System.Drawing.Size(135, 20);
            this.alias.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Alias name";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 125);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(114, 13);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Use FTP SSL (Explicit)";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ftpes
            // 
            this.ftpes.AutoSize = true;
            this.ftpes.Location = new System.Drawing.Point(221, 124);
            this.ftpes.Name = "ftpes";
            this.ftpes.Size = new System.Drawing.Size(60, 17);
            this.ftpes.TabIndex = 17;
            this.ftpes.Text = "FTPES";
            this.ftpes.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 164);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(166, 26);
            this.label18.TabIndex = 19;
            this.label18.Text = "Leave password blank to be \r\nprompted for it before connecting.";
            // 
            // NewSystem
            // 
            this.AcceptButton = this.save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 204);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.ftpes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.alias);
            this.Controls.Add(this.save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.user);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.host);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New System";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox host;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.TextBox alias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox ftpes;
        private System.Windows.Forms.Label label18;
    }
}