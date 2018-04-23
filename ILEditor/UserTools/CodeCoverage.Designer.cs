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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeCoverage));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newcctest = new System.Windows.Forms.ToolStripButton();
            this.tests = new System.Windows.Forms.ListView();
            this.toolStrip1.SuspendLayout();
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
            this.tests.TabIndex = 1;
            this.tests.UseCompatibleStateImageBehavior = false;
            this.tests.View = System.Windows.Forms.View.List;
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newcctest;
        private System.Windows.Forms.ListView tests;
    }
}
