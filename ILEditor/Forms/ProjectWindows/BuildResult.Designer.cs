namespace ILEditor.Forms.ProjectWindows
{
    partial class BuildResult
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
            this.textListing = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textListing
            // 
            this.textListing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textListing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textListing.FormattingEnabled = true;
            this.textListing.ItemHeight = 20;
            this.textListing.Location = new System.Drawing.Point(0, 0);
            this.textListing.Name = "textListing";
            this.textListing.Size = new System.Drawing.Size(438, 427);
            this.textListing.TabIndex = 0;
            // 
            // BuildResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textListing);
            this.Name = "BuildResult";
            this.Size = new System.Drawing.Size(438, 427);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox textListing;
    }
}
