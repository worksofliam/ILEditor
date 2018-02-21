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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberSearchListing));
            this.searchResults = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // searchResults
            // 
            this.searchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResults.ImageIndex = 0;
            this.searchResults.ImageList = this.imageList1;
            this.searchResults.Location = new System.Drawing.Point(0, 0);
            this.searchResults.Name = "searchResults";
            this.searchResults.SelectedImageIndex = 0;
            this.searchResults.Size = new System.Drawing.Size(281, 307);
            this.searchResults.TabIndex = 0;
            this.searchResults.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.searchResults_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "warning.png");
            this.imageList1.Images.SetKeyName(1, "info.png");
            this.imageList1.Images.SetKeyName(2, "file.png");
            this.imageList1.Images.SetKeyName(3, "edit.png");
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
        private System.Windows.Forms.ImageList imageList1;
    }
}
