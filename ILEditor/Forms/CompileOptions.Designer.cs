namespace ILEditor.Forms
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompileOptions));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.commandList = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.newType = new System.Windows.Forms.ToolStripButton();
            this.newCommand = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newType,
            this.newCommand});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(328, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // commandList
            // 
            this.commandList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandList.LargeImageList = this.imageList1;
            this.commandList.Location = new System.Drawing.Point(0, 25);
            this.commandList.Name = "commandList";
            this.commandList.Size = new System.Drawing.Size(328, 293);
            this.commandList.SmallImageList = this.imageList1;
            this.commandList.TabIndex = 1;
            this.commandList.UseCompatibleStateImageBehavior = false;
            this.commandList.View = System.Windows.Forms.View.Tile;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "application.png");
            this.imageList1.Images.SetKeyName(1, "application_go.png");
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
            // CompileOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 318);
            this.Controls.Add(this.commandList);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CompileOptions";
            this.Text = "Compile Options";
            this.Load += new System.EventHandler(this.CompileOptions_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView commandList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton newType;
        private System.Windows.Forms.ToolStripButton newCommand;
    }
}