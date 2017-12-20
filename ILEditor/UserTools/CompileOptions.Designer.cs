namespace ILEditor.UserTools
{
    partial class CompileOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompileOptions));
            this.commandList = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newType = new System.Windows.Forms.ToolStripButton();
            this.newCommand = new System.Windows.Forms.ToolStripButton();
            this.rightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setDefaultForTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.rightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandList
            // 
            this.commandList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandList.LargeImageList = this.imageList1;
            this.commandList.Location = new System.Drawing.Point(0, 25);
            this.commandList.MultiSelect = false;
            this.commandList.Name = "commandList";
            this.commandList.Size = new System.Drawing.Size(317, 281);
            this.commandList.SmallImageList = this.imageList1;
            this.commandList.TabIndex = 3;
            this.commandList.UseCompatibleStateImageBehavior = false;
            this.commandList.View = System.Windows.Forms.View.Tile;
            this.commandList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.commandList_MouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cubedark.png");
            this.imageList1.Images.SetKeyName(1, "cube.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newType,
            this.newCommand});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(317, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newType
            // 
            this.newType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newType.Image = ((System.Drawing.Image)(resources.GetObject("newType.Image")));
            this.newType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newType.Name = "newType";
            this.newType.Size = new System.Drawing.Size(111, 22);
            this.newType.Text = "New Compile Type";
            this.newType.Click += new System.EventHandler(this.newType_Click);
            // 
            // newCommand
            // 
            this.newCommand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newCommand.Image = ((System.Drawing.Image)(resources.GetObject("newCommand.Image")));
            this.newCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newCommand.Name = "newCommand";
            this.newCommand.Size = new System.Drawing.Size(95, 22);
            this.newCommand.Text = "New Command";
            this.newCommand.Click += new System.EventHandler(this.newCommand_Click);
            // 
            // rightClick
            // 
            this.rightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDefaultForTypeToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.rightClick.Name = "contextMenuStrip1";
            this.rightClick.Size = new System.Drawing.Size(178, 70);
            // 
            // setDefaultForTypeToolStripMenuItem
            // 
            this.setDefaultForTypeToolStripMenuItem.Name = "setDefaultForTypeToolStripMenuItem";
            this.setDefaultForTypeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.setDefaultForTypeToolStripMenuItem.Text = "Set Default for Type";
            this.setDefaultForTypeToolStripMenuItem.Click += new System.EventHandler(this.setDefaultForTypeToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // CompileOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.commandList);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CompileOptions";
            this.Size = new System.Drawing.Size(317, 306);
            this.Load += new System.EventHandler(this.CompileOptions_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.rightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView commandList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newType;
        private System.Windows.Forms.ToolStripButton newCommand;
        private System.Windows.Forms.ContextMenuStrip rightClick;
        private System.Windows.Forms.ToolStripMenuItem setDefaultForTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}
