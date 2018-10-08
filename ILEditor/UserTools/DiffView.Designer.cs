namespace ILEditor.UserTools
{
    partial class DiffView
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.oldContent = new System.Windows.Forms.RichTextBox();
            this.newContent = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.oldContent);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.newContent);
            this.splitContainer1.Size = new System.Drawing.Size(441, 323);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 0;
            // 
            // oldContent
            // 
            this.oldContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oldContent.Location = new System.Drawing.Point(0, 0);
            this.oldContent.Name = "oldContent";
            this.oldContent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oldContent.Size = new System.Drawing.Size(228, 323);
            this.oldContent.TabIndex = 0;
            this.oldContent.Text = "";
            // 
            // newContent
            // 
            this.newContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newContent.Location = new System.Drawing.Point(0, 0);
            this.newContent.Name = "newContent";
            this.newContent.Size = new System.Drawing.Size(209, 323);
            this.newContent.TabIndex = 0;
            this.newContent.Text = "";
            // 
            // DiffView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 323);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DiffView";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox oldContent;
        private System.Windows.Forms.RichTextBox newContent;
    }
}
