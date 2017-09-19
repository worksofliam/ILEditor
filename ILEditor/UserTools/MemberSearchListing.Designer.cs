namespace ILEditor.UserTools
{
    partial class MemberSearchListing
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
            this.searchResults = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // searchResults
            // 
            this.searchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchResults.Location = new System.Drawing.Point(0, 0);
            this.searchResults.Name = "searchResults";
            this.searchResults.Size = new System.Drawing.Size(281, 307);
            this.searchResults.TabIndex = 0;
            this.searchResults.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.searchResults_NodeMouseDoubleClick);
            // 
            // MemberSearchListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchResults);
            this.Name = "MemberSearchListing";
            this.Size = new System.Drawing.Size(281, 307);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView searchResults;
    }
}
