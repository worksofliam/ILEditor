namespace ILEditor.UserTools
{
    partial class UserToolList
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Member Browser", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Program Listing", 5);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Tree Browser", 6);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Spool File List", 7);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Project Explorer");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Library List", 3);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Compile Settings", 4);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Connection Settings", 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserToolList));
            this.toollist = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // toollist
            // 
            this.toollist.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.toollist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toollist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            listViewItem1.Tag = "MBR";
            listViewItem2.Tag = "PGM";
            listViewItem3.Tag = "TREE";
            listViewItem4.Tag = "SPL";
            listViewItem5.Tag = "PROJ";
            listViewItem6.Tag = "LIBL";
            listViewItem7.Tag = "CMP";
            listViewItem8.Tag = "CONN";
            this.toollist.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8});
            this.toollist.Location = new System.Drawing.Point(0, 0);
            this.toollist.MultiSelect = false;
            this.toollist.Name = "toollist";
            this.toollist.Size = new System.Drawing.Size(345, 334);
            this.toollist.SmallImageList = this.imageList1;
            this.toollist.TabIndex = 0;
            this.toollist.UseCompatibleStateImageBehavior = false;
            this.toollist.View = System.Windows.Forms.View.SmallIcon;
            this.toollist.DoubleClick += new System.EventHandler(this.toollist_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "edit.png");
            this.imageList1.Images.SetKeyName(1, "application_error.png");
            this.imageList1.Images.SetKeyName(2, "settings.png");
            this.imageList1.Images.SetKeyName(3, "books.png");
            this.imageList1.Images.SetKeyName(4, "ux-design.png");
            this.imageList1.Images.SetKeyName(5, "browser.png");
            this.imageList1.Images.SetKeyName(6, "folder.png");
            this.imageList1.Images.SetKeyName(7, "file.png");
            // 
            // UserToolList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toollist);
            this.Name = "UserToolList";
            this.Size = new System.Drawing.Size(345, 334);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView toollist;
        private System.Windows.Forms.ImageList imageList1;
    }
}
