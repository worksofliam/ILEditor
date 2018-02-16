namespace ILEditor.Forms
{
    partial class QuickMemberSearch
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
            this.memberValue = new System.Windows.Forms.TextBox();
            this.memberList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // memberValue
            // 
            this.memberValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.memberValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memberValue.Location = new System.Drawing.Point(12, 41);
            this.memberValue.MaxLength = 32;
            this.memberValue.Name = "memberValue";
            this.memberValue.Size = new System.Drawing.Size(330, 26);
            this.memberValue.TabIndex = 0;
            this.memberValue.TextChanged += new System.EventHandler(this.memberValue_TextChanged);
            this.memberValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.memberValue_KeyDown);
            // 
            // memberList
            // 
            this.memberList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memberList.FormattingEnabled = true;
            this.memberList.ItemHeight = 20;
            this.memberList.Items.AddRange(new object[] {
            "Enter search value"});
            this.memberList.Location = new System.Drawing.Point(12, 73);
            this.memberList.Name = "memberList";
            this.memberList.Size = new System.Drawing.Size(330, 184);
            this.memberList.TabIndex = 1;
            this.memberList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.memberList_KeyDown);
            this.memberList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.memberList_MouseDoubleClick);
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
            // QuickMemberSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(354, 269);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.memberList);
            this.Controls.Add(this.memberValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QuickMemberSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuickMemberSearch";
            this.Deactivate += new System.EventHandler(this.QuickMemberSearch_Deactivate);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox memberValue;
        private System.Windows.Forms.ListBox memberList;
        private System.Windows.Forms.Label label1;
    }
}