namespace ILEditor.UserTools
{
    partial class ProjectExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectExplorer));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.newProject = new System.Windows.Forms.ToolStripSplitButton();
            this.projView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.projRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.projRightClick.SuspendLayout();
            this.fileRightClick.SuspendLayout();
            this.folderRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProject});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(280, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // newProject
            // 
            this.newProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.newProject.Image = ((System.Drawing.Image)(resources.GetObject("newProject.Image")));
            this.newProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newProject.Name = "newProject";
            this.newProject.Size = new System.Drawing.Size(87, 20);
            this.newProject.Text = "New Project";
            this.newProject.ButtonClick += new System.EventHandler(this.newProject_ButtonClick);
            // 
            // projView
            // 
            this.projView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projView.ImageIndex = 0;
            this.projView.ImageList = this.imageList1;
            this.projView.LabelEdit = true;
            this.projView.Location = new System.Drawing.Point(0, 22);
            this.projView.Name = "projView";
            this.projView.SelectedImageIndex = 0;
            this.projView.Size = new System.Drawing.Size(280, 333);
            this.projView.TabIndex = 1;
            this.projView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.projView_BeforeLabelEdit);
            this.projView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.projView_AfterLabelEdit);
            this.projView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.projView_MouseClick);
            this.projView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.projView_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cubedark.png");
            this.imageList1.Images.SetKeyName(1, "settings.png");
            this.imageList1.Images.SetKeyName(2, "compile.png");
            this.imageList1.Images.SetKeyName(3, "folder_empty.png");
            this.imageList1.Images.SetKeyName(4, "file.png");
            // 
            // projRightClick
            // 
            this.projRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeProjectToolStripMenuItem});
            this.projRightClick.Name = "projRightClick";
            this.projRightClick.Size = new System.Drawing.Size(158, 26);
            // 
            // removeProjectToolStripMenuItem
            // 
            this.removeProjectToolStripMenuItem.Name = "removeProjectToolStripMenuItem";
            this.removeProjectToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.removeProjectToolStripMenuItem.Text = "Remove Project";
            this.removeProjectToolStripMenuItem.Click += new System.EventHandler(this.removeProjectToolStripMenuItem_Click);
            // 
            // fileRightClick
            // 
            this.fileRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.fileRightClick.Name = "contextMenuStrip1";
            this.fileRightClick.Size = new System.Drawing.Size(118, 48);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // folderRightClick
            // 
            this.folderRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem});
            this.folderRightClick.Name = "folderRightClick";
            this.folderRightClick.Size = new System.Drawing.Size(120, 26);
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.newFileToolStripMenuItem.Text = "New File";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.refreshToolStripMenuItem.Text = "Refresh Projects";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // ProjectExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.projView);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ProjectExplorer";
            this.Size = new System.Drawing.Size(280, 355);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.projRightClick.ResumeLayout(false);
            this.fileRightClick.ResumeLayout(false);
            this.folderRightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSplitButton newProject;
        private System.Windows.Forms.TreeView projView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip projRightClick;
        private System.Windows.Forms.ToolStripMenuItem removeProjectToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip fileRightClick;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip folderRightClick;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    }
}
