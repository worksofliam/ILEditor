namespace ILEditor.Forms
{
    partial class HostSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostSelect));
            this.systemlist = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cancel = new System.Windows.Forms.Button();
            this.newhost = new System.Windows.Forms.Button();
            this.versionLabel = new System.Windows.Forms.Label();
            this.isOffline = new System.Windows.Forms.CheckBox();
            this.hostRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // systemlist
            // 
            this.systemlist.LargeImageList = this.imageList1;
            this.systemlist.Location = new System.Drawing.Point(12, 12);
            this.systemlist.MultiSelect = false;
            this.systemlist.Name = "systemlist";
            this.systemlist.Size = new System.Drawing.Size(438, 275);
            this.systemlist.TabIndex = 0;
            this.systemlist.UseCompatibleStateImageBehavior = false;
            this.systemlist.DoubleClick += new System.EventHandler(this.SystemList_DoubleClick);
            this.systemlist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SystemList_KeyDown);
            this.systemlist.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SystemList_MouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ibmi.png");
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(12, 293);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // newhost
            // 
            this.newhost.Location = new System.Drawing.Point(375, 293);
            this.newhost.Name = "newhost";
            this.newhost.Size = new System.Drawing.Size(75, 23);
            this.newhost.TabIndex = 2;
            this.newhost.Text = "New Host";
            this.newhost.UseVisualStyleBackColor = true;
            this.newhost.Click += new System.EventHandler(this.NewHost_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(93, 298);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(0, 13);
            this.versionLabel.TabIndex = 3;
            // 
            // isOffline
            // 
            this.isOffline.AutoSize = true;
            this.isOffline.Location = new System.Drawing.Point(289, 297);
            this.isOffline.Name = "isOffline";
            this.isOffline.Size = new System.Drawing.Size(86, 17);
            this.isOffline.TabIndex = 4;
            this.isOffline.Text = "Offline Mode";
            this.isOffline.UseVisualStyleBackColor = true;
            // 
            // hostRightClick
            // 
            this.hostRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.hostRightClick.Name = "hostRightClick";
            this.hostRightClick.Size = new System.Drawing.Size(153, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // HostSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 328);
            this.Controls.Add(this.isOffline);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.newhost);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.systemlist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(478, 367);
            this.MinimumSize = new System.Drawing.Size(478, 367);
            this.Name = "HostSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Host Select";
            this.hostRightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView systemlist;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button newhost;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.CheckBox isOffline;
        private System.Windows.Forms.ContextMenuStrip hostRightClick;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    }
}