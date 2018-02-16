namespace ILEditor.UserTools
{
    partial class OutlineView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutlineView));
            this.funcList = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.updateButton = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // funcList
            // 
            this.funcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.funcList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.funcList.ImageIndex = 0;
            this.funcList.ImageList = this.imageList1;
            this.funcList.Location = new System.Drawing.Point(0, 0);
            this.funcList.Name = "funcList";
            this.funcList.SelectedImageIndex = 0;
            this.funcList.Size = new System.Drawing.Size(284, 236);
            this.funcList.TabIndex = 0;
            this.funcList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.funcList_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cube.png");
            this.imageList1.Images.SetKeyName(1, "redo.png");
            this.imageList1.Images.SetKeyName(2, "file.png");
            this.imageList1.Images.SetKeyName(3, "cubedark.png");
            this.imageList1.Images.SetKeyName(4, "diagram.png");
            this.imageList1.Images.SetKeyName(5, "compile.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // updateButton
            // 
            this.updateButton.Image = global::ILEditor.Properties.Resources.refresh;
            this.updateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(103, 22);
            this.updateButton.Text = "Update Listing";
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.funcList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 236);
            this.panel1.TabIndex = 2;
            // 
            // OutlineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.CloseButton = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "OutlineView";
            this.Text = "Outline View";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView funcList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton updateButton;
        private System.Windows.Forms.Panel panel1;
    }
}
