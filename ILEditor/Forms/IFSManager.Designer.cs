namespace ILEditor.Forms
{
    partial class IFSManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IFSManager));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ifsDir = new System.Windows.Forms.TextBox();
            this.ifsDirs = new System.Windows.Forms.ListBox();
            this.delIfs = new System.Windows.Forms.Button();
            this.addDir = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ifsDir);
            this.groupBox3.Controls.Add(this.ifsDirs);
            this.groupBox3.Controls.Add(this.delIfs);
            this.groupBox3.Controls.Add(this.addDir);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(361, 114);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "IFS List";
            // 
            // ifsDir
            // 
            this.ifsDir.Location = new System.Drawing.Point(6, 24);
            this.ifsDir.MaxLength = 128;
            this.ifsDir.Name = "ifsDir";
            this.ifsDir.Size = new System.Drawing.Size(141, 20);
            this.ifsDir.TabIndex = 8;
            this.ifsDir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ifsDir_KeyDown);
            // 
            // ifsDirs
            // 
            this.ifsDirs.FormattingEnabled = true;
            this.ifsDirs.Location = new System.Drawing.Point(153, 19);
            this.ifsDirs.Name = "ifsDirs";
            this.ifsDirs.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.ifsDirs.Size = new System.Drawing.Size(202, 82);
            this.ifsDirs.TabIndex = 8;
            this.ifsDirs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ifsDirs_KeyDown);
            // 
            // delIfs
            // 
            this.delIfs.Location = new System.Drawing.Point(6, 78);
            this.delIfs.Name = "delIfs";
            this.delIfs.Size = new System.Drawing.Size(141, 23);
            this.delIfs.TabIndex = 10;
            this.delIfs.Text = "Delete Selected";
            this.delIfs.UseVisualStyleBackColor = true;
            this.delIfs.Click += new System.EventHandler(this.delIfs_Click);
            // 
            // addDir
            // 
            this.addDir.Location = new System.Drawing.Point(6, 50);
            this.addDir.Name = "addDir";
            this.addDir.Size = new System.Drawing.Size(141, 23);
            this.addDir.TabIndex = 9;
            this.addDir.Text = "Add IFS Directory";
            this.addDir.UseVisualStyleBackColor = true;
            this.addDir.Click += new System.EventHandler(this.addDir_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(298, 132);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 15;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(12, 132);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 16;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // IFSManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 165);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.save);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IFSManager";
            this.Text = "IFS Manager";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox ifsDir;
        private System.Windows.Forms.ListBox ifsDirs;
        private System.Windows.Forms.Button delIfs;
        private System.Windows.Forms.Button addDir;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button cancel;
    }
}