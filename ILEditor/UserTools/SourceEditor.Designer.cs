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
            this.replacewindow = new System.Windows.Forms.Panel();
            this.replace = new System.Windows.Forms.Button();
            this.replace_val = new System.Windows.Forms.TextBox();
            this.search_val = new System.Windows.Forms.TextBox();
            this.replacewindow.SuspendLayout();
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
            // replacewindow
            // 
            this.replacewindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.replacewindow.Controls.Add(this.replace);
            this.replacewindow.Controls.Add(this.replace_val);
            this.replacewindow.Controls.Add(this.search_val);
            this.replacewindow.Location = new System.Drawing.Point(395, 3);
            this.replacewindow.Name = "replacewindow";
            this.replacewindow.Size = new System.Drawing.Size(146, 66);
            this.replacewindow.TabIndex = 0;
            this.replacewindow.Visible = false;
            this.replacewindow.Leave += new System.EventHandler(this.replacewindow_Leave);
            // 
            // replace
            // 
            this.replace.Image = ((System.Drawing.Image)(resources.GetObject("replace.Image")));
            this.replace.Location = new System.Drawing.Point(111, 9);
            this.replace.Name = "replace";
            this.replace.Size = new System.Drawing.Size(28, 46);
            this.replace.TabIndex = 3;
            this.replace.UseVisualStyleBackColor = true;
            this.replace.Click += new System.EventHandler(this.replace_Click);
            // 
            // replace_val
            // 
            this.replace_val.Location = new System.Drawing.Point(5, 35);
            this.replace_val.Name = "replace_val";
            this.replace_val.Size = new System.Drawing.Size(100, 20);
            this.replace_val.TabIndex = 1;
            // 
            // search_val
            // 
            this.search_val.Location = new System.Drawing.Point(5, 9);
            this.search_val.Name = "search_val";
            this.search_val.Size = new System.Drawing.Size(100, 20);
            this.search_val.TabIndex = 0;
            // 
            // SourceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.replacewindow);
            this.Name = "SourceEditor";
            this.Size = new System.Drawing.Size(544, 488);
            this.replacewindow.ResumeLayout(false);
            this.replacewindow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel replacewindow;
        private System.Windows.Forms.Button replace;
        private System.Windows.Forms.TextBox replace_val;
        private System.Windows.Forms.TextBox search_val;
    }
}
