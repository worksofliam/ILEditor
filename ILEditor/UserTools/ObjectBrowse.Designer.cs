namespace ILEditor.UserTools
{
    partial class ObjectBrowse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectBrowse));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.library = new System.Windows.Forms.ToolStripTextBox();
            this.fetchButton = new System.Windows.Forms.ToolStripButton();
            this.programcount = new System.Windows.Forms.ToolStripLabel();
            this.objectList = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.objectRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.objectInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.objectRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.library,
            this.fetchButton,
            this.programcount});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(577, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // library
            // 
            this.library.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.library.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.library.MaxLength = 10;
            this.library.Name = "library";
            this.library.Size = new System.Drawing.Size(100, 25);
            // 
            // fetchButton
            // 
            this.fetchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fetchButton.Image = ((System.Drawing.Image)(resources.GetObject("fetchButton.Image")));
            this.fetchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fetchButton.Name = "fetchButton";
            this.fetchButton.Size = new System.Drawing.Size(23, 22);
            this.fetchButton.Text = "Fetch";
            this.fetchButton.Click += new System.EventHandler(this.fetchButton_Click);
            // 
            // programcount
            // 
            this.programcount.Name = "programcount";
            this.programcount.Size = new System.Drawing.Size(54, 22);
            this.programcount.Text = "0 objects";
            // 
            // objectList
            // 
            this.objectList.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.objectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.objectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectList.Location = new System.Drawing.Point(0, 25);
            this.objectList.MultiSelect = false;
            this.objectList.Name = "objectList";
            this.objectList.Size = new System.Drawing.Size(577, 318);
            this.objectList.SmallImageList = this.imageList1;
            this.objectList.TabIndex = 2;
            this.objectList.UseCompatibleStateImageBehavior = false;
            this.objectList.View = System.Windows.Forms.View.Details;
            this.objectList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.objectList_MouseClick);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Name";
            this.columnHeader5.Width = 116;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Extension";
            this.columnHeader6.Width = 87;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Type";
            this.columnHeader7.Width = 87;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Text";
            this.columnHeader8.Width = 261;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "application.png");
            this.imageList1.Images.SetKeyName(1, "bricks.png");
            this.imageList1.Images.SetKeyName(2, "brick.png");
            // 
            // objectRightClick
            // 
            this.objectRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectInformationToolStripMenuItem,
            this.openSourceToolStripMenuItem});
            this.objectRightClick.Name = "objectRightClick";
            this.objectRightClick.Size = new System.Drawing.Size(176, 70);
            this.objectRightClick.Opening += new System.ComponentModel.CancelEventHandler(this.objectRightClick_Opening);
            // 
            // objectInformationToolStripMenuItem
            // 
            this.objectInformationToolStripMenuItem.Name = "objectInformationToolStripMenuItem";
            this.objectInformationToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.objectInformationToolStripMenuItem.Text = "Object Information";
            this.objectInformationToolStripMenuItem.Click += new System.EventHandler(this.objectInformationToolStripMenuItem_Click);
            // 
            // openSourceToolStripMenuItem
            // 
            this.openSourceToolStripMenuItem.Name = "openSourceToolStripMenuItem";
            this.openSourceToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.openSourceToolStripMenuItem.Text = "Open Source";
            this.openSourceToolStripMenuItem.Click += new System.EventHandler(this.openSourceToolStripMenuItem_Click);
            // 
            // ObjectBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.objectList);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ObjectBrowse";
            this.Size = new System.Drawing.Size(577, 343);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.objectRightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox library;
        private System.Windows.Forms.ToolStripLabel programcount;
        private System.Windows.Forms.ToolStripButton fetchButton;
        private System.Windows.Forms.ListView objectList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ContextMenuStrip objectRightClick;
        private System.Windows.Forms.ToolStripMenuItem objectInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSourceToolStripMenuItem;
    }
}
