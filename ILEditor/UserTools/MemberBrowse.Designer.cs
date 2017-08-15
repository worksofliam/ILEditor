namespace ILEditor.UserTools
{
    partial class MemberBrowse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberBrowse));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.library = new System.Windows.Forms.ToolStripTextBox();
            this.spf = new System.Windows.Forms.ToolStripTextBox();
            this.fetchButton = new System.Windows.Forms.ToolStripButton();
            this.addmember = new System.Windows.Forms.ToolStripButton();
            this.membercount = new System.Windows.Forms.ToolStripLabel();
            this.memberList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.library,
            this.spf,
            this.fetchButton,
            this.addmember,
            this.membercount});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(376, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // library
            // 
            this.library.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.library.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.library.MaxLength = 10;
            this.library.Name = "library";
            this.library.Size = new System.Drawing.Size(100, 25);
            // 
            // spf
            // 
            this.spf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.spf.MaxLength = 10;
            this.spf.Name = "spf";
            this.spf.Size = new System.Drawing.Size(100, 25);
            // 
            // fetchButton
            // 
            this.fetchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fetchButton.Image = global::ILEditor.Properties.Resources.script_go;
            this.fetchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fetchButton.Name = "fetchButton";
            this.fetchButton.Size = new System.Drawing.Size(23, 22);
            this.fetchButton.Text = "toolStripButton1";
            this.fetchButton.Click += new System.EventHandler(this.fetchButton_Click);
            // 
            // addmember
            // 
            this.addmember.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addmember.Enabled = false;
            this.addmember.Image = global::ILEditor.Properties.Resources.script_add;
            this.addmember.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addmember.Name = "addmember";
            this.addmember.Size = new System.Drawing.Size(23, 22);
            this.addmember.Click += new System.EventHandler(this.addmember_Click);
            // 
            // membercount
            // 
            this.membercount.Name = "membercount";
            this.membercount.Size = new System.Drawing.Size(66, 22);
            this.membercount.Text = "0 members";
            // 
            // memberList
            // 
            this.memberList.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.memberList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.memberList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memberList.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memberList.Location = new System.Drawing.Point(0, 25);
            this.memberList.MultiSelect = false;
            this.memberList.Name = "memberList";
            this.memberList.Size = new System.Drawing.Size(376, 291);
            this.memberList.SmallImageList = this.imageList1;
            this.memberList.TabIndex = 1;
            this.memberList.UseCompatibleStateImageBehavior = false;
            this.memberList.View = System.Windows.Forms.View.Details;
            this.memberList.DoubleClick += new System.EventHandler(this.memberList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 93;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 68;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Text";
            this.columnHeader3.Width = 170;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "script.png");
            this.imageList1.Images.SetKeyName(1, "folder_explore.png");
            this.imageList1.Images.SetKeyName(2, "sitemap_color.png");
            // 
            // MemberBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.memberList);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MemberBrowse";
            this.Size = new System.Drawing.Size(376, 316);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox library;
        private System.Windows.Forms.ToolStripTextBox spf;
        private System.Windows.Forms.ToolStripButton fetchButton;
        private System.Windows.Forms.ListView memberList;
        private System.Windows.Forms.ToolStripLabel membercount;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripButton addmember;
    }
}
