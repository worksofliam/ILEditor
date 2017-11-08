namespace ILEditor.UserTools
{
    partial class SourceEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceEditor));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "application.png");
            this.imageList1.Images.SetKeyName(1, "package.png");
            this.imageList1.Images.SetKeyName(2, "wrench.png");
            this.imageList1.Images.SetKeyName(3, "database.png");
            this.imageList1.Images.SetKeyName(4, "application_edit.png");
            this.imageList1.Images.SetKeyName(5, "application_delete.png");
            this.imageList1.Images.SetKeyName(6, "bricks.png");
            this.imageList1.Images.SetKeyName(7, "brick_edit.png");
            // 
            // SourceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SourceEditor";
            this.Size = new System.Drawing.Size(544, 488);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
    }
}
