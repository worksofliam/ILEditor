namespace ILEditor.UserTools
{
    partial class IFSBrowser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IFSBrowser));
            this.files = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpf = new System.Windows.Forms.ToolStrip();
            this.manageDirs = new System.Windows.Forms.ToolStripButton();
            this.setHomeDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightClickMenu.SuspendLayout();
            this.addSpf.SuspendLayout();
            this.SuspendLayout();
            // 
            // files
            // 
            this.files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.files.ImageIndex = 0;
            this.files.ImageList = this.imageList1;
            this.files.LabelEdit = true;
            this.files.Location = new System.Drawing.Point(0, 0);
            this.files.Name = "files";
            this.files.SelectedImageIndex = 0;
            this.files.Size = new System.Drawing.Size(244, 279);
            this.files.TabIndex = 0;
            this.files.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.files_AfterLabelEdit);
            this.files.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.files_AfterExpand);
            this.files.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.files_NodeMouseClick);
            this.files.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.files_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder_empty.png");
            this.imageList1.Images.SetKeyName(1, "file.png");
            this.imageList1.Images.SetKeyName(2, "info.png");
            this.imageList1.Images.SetKeyName(3, "warning.png");
            // 
            // rightClickMenu
            // 
            this.rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.makeShortcutToolStripMenuItem,
            this.setHomeDirectoryToolStripMenuItem});
            this.rightClickMenu.Name = "rightClickMenu";
            this.rightClickMenu.Size = new System.Drawing.Size(175, 136);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.directoryToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // directoryToolStripMenuItem
            // 
            this.directoryToolStripMenuItem.Name = "directoryToolStripMenuItem";
            this.directoryToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.directoryToolStripMenuItem.Text = "Directory";
            this.directoryToolStripMenuItem.Click += new System.EventHandler(this.directoryToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // makeShortcutToolStripMenuItem
            // 
            this.makeShortcutToolStripMenuItem.Name = "makeShortcutToolStripMenuItem";
            this.makeShortcutToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.makeShortcutToolStripMenuItem.Text = "Make shortcut";
            this.makeShortcutToolStripMenuItem.Click += new System.EventHandler(this.makeShortcutToolStripMenuItem_Click);
            // 
            // addSpf
            // 
            this.addSpf.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addSpf.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageDirs});
            this.addSpf.Location = new System.Drawing.Point(0, 254);
            this.addSpf.Name = "addSpf";
            this.addSpf.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.addSpf.Size = new System.Drawing.Size(244, 25);
            this.addSpf.TabIndex = 2;
            this.addSpf.Text = "toolStrip1";
            // 
            // manageDirs
            // 
            this.manageDirs.Image = global::ILEditor.Properties.Resources.folder;
            this.manageDirs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.manageDirs.Name = "manageDirs";
            this.manageDirs.Size = new System.Drawing.Size(129, 22);
            this.manageDirs.Text = "Manage Directories";
            this.manageDirs.Click += new System.EventHandler(this.manageDirs_Click);
            // 
            // setHomeDirectoryToolStripMenuItem
            // 
            this.setHomeDirectoryToolStripMenuItem.Name = "setHomeDirectoryToolStripMenuItem";
            this.setHomeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.setHomeDirectoryToolStripMenuItem.Text = "Set home directory";
            this.setHomeDirectoryToolStripMenuItem.Click += new System.EventHandler(this.setHomeDirectoryToolStripMenuItem_Click);
            // 
            // IFSBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 279);
            this.Controls.Add(this.addSpf);
            this.Controls.Add(this.files);
            this.Name = "IFSBrowser";
            this.Load += new System.EventHandler(this.IFSBrowser_Load);
            this.rightClickMenu.ResumeLayout(false);
            this.addSpf.ResumeLayout(false);
            this.addSpf.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView files;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip rightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem directoryToolStripMenuItem;
        private System.Windows.Forms.ToolStrip addSpf;
        private System.Windows.Forms.ToolStripButton manageDirs;
        private System.Windows.Forms.ToolStripMenuItem makeShortcutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setHomeDirectoryToolStripMenuItem;
    }
}
