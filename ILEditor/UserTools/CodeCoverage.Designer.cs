namespace ILEditor.UserTools
{
    partial class CodeCoverage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeCoverage));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newcctest = new System.Windows.Forms.ToolStripButton();
            this.tests = new System.Windows.Forms.ListView();
            this.rightClickTest = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editTest = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTest = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.rightClickTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newcctest});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(314, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newcctest
            // 
            this.newcctest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newcctest.Image = ((System.Drawing.Image)(resources.GetObject("newcctest.Image")));
            this.newcctest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newcctest.Name = "newcctest";
            this.newcctest.Size = new System.Drawing.Size(112, 22);
            this.newcctest.Text = "New Coverage Test";
            this.newcctest.Click += new System.EventHandler(this.newcctest_Click);
            // 
            // tests
            // 
            this.tests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tests.Location = new System.Drawing.Point(0, 25);
            this.tests.Name = "tests";
            this.tests.Size = new System.Drawing.Size(314, 274);
            this.tests.SmallImageList = this.imageList1;
            this.tests.TabIndex = 1;
            this.tests.UseCompatibleStateImageBehavior = false;
            this.tests.View = System.Windows.Forms.View.List;
            this.tests.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tests_MouseClick);
            // 
            // rightClickTest
            // 
            this.rightClickTest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.editTest,
            this.deleteTest});
            this.rightClickTest.Name = "rightClickTest";
            this.rightClickTest.Size = new System.Drawing.Size(108, 70);
            // 
            // editTest
            // 
            this.editTest.Name = "editTest";
            this.editTest.Size = new System.Drawing.Size(107, 22);
            this.editTest.Text = "Edit";
            this.editTest.Click += new System.EventHandler(this.editTest_Click);
            // 
            // deleteTest
            // 
            this.deleteTest.Name = "deleteTest";
            this.deleteTest.Size = new System.Drawing.Size(107, 22);
            this.deleteTest.Text = "Delete";
            this.deleteTest.Click += new System.EventHandler(this.deleteTest_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "sitemap.png");
            // 
            // CodeCoverage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 299);
            this.Controls.Add(this.tests);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CodeCoverage";
            this.Text = "Code Coverage";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.rightClickTest.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newcctest;
        private System.Windows.Forms.ListView tests;
        private System.Windows.Forms.ContextMenuStrip rightClickTest;
        private System.Windows.Forms.ToolStripMenuItem editTest;
        private System.Windows.Forms.ToolStripMenuItem deleteTest;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
    }
}
