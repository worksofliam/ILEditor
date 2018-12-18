namespace ILEditor.Forms
{
    partial class QuickFileSearch
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameValue = new System.Windows.Forms.TextBox();
            this.fileList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameValue
            // 
            this.nameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameValue.Location = new System.Drawing.Point(12, 41);
            this.nameValue.MaxLength = 32;
            this.nameValue.Name = "nameValue";
            this.nameValue.Size = new System.Drawing.Size(330, 26);
            this.nameValue.TabIndex = 0;
            this.nameValue.TextChanged += new System.EventHandler(this.fileValue_TextChanged);
            this.nameValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileValue_KeyDown);
            // 
            // fileList
            // 
            this.fileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileList.FormattingEnabled = true;
            this.fileList.ItemHeight = 20;
            this.fileList.Items.AddRange(new object[] {
            "Enter search value"});
            this.fileList.Location = new System.Drawing.Point(12, 73);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(330, 184);
            this.fileList.TabIndex = 1;
            this.fileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileList_KeyDown);
            this.fileList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.fileList_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search Recent Members..";
            // 
            // QuickFileSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(354, 269);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.nameValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QuickFileSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuickMemberSearch";
            this.Deactivate += new System.EventHandler(this.QuickFileSearch_Deactivate);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameValue;
        private System.Windows.Forms.ListBox fileList;
        private System.Windows.Forms.Label label1;
    }
}