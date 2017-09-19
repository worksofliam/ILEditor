namespace ILEditor.Forms
{
    partial class MemberSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberSearch));
            this.search = new System.Windows.Forms.Button();
            this.spf = new System.Windows.Forms.TextBox();
            this.lib = new System.Windows.Forms.TextBox();
            this.searchVal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.caseSense = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openClone = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(224, 112);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(100, 23);
            this.search.TabIndex = 0;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // spf
            // 
            this.spf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.spf.Location = new System.Drawing.Point(118, 76);
            this.spf.MaxLength = 10;
            this.spf.Name = "spf";
            this.spf.Size = new System.Drawing.Size(100, 20);
            this.spf.TabIndex = 12;
            // 
            // lib
            // 
            this.lib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lib.Location = new System.Drawing.Point(12, 76);
            this.lib.MaxLength = 10;
            this.lib.Name = "lib";
            this.lib.Size = new System.Drawing.Size(100, 20);
            this.lib.TabIndex = 11;
            // 
            // searchVal
            // 
            this.searchVal.Location = new System.Drawing.Point(12, 25);
            this.searchVal.MaxLength = 80;
            this.searchVal.Name = "searchVal";
            this.searchVal.Size = new System.Drawing.Size(206, 20);
            this.searchVal.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Containing text";
            // 
            // caseSense
            // 
            this.caseSense.AutoSize = true;
            this.caseSense.Checked = true;
            this.caseSense.CheckState = System.Windows.Forms.CheckState.Checked;
            this.caseSense.Location = new System.Drawing.Point(224, 28);
            this.caseSense.Name = "caseSense";
            this.caseSense.Size = new System.Drawing.Size(94, 17);
            this.caseSense.TabIndex = 15;
            this.caseSense.Text = "Case sensitive";
            this.caseSense.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Library";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "SPF";
            // 
            // openClone
            // 
            this.openClone.AutoSize = true;
            this.openClone.Location = new System.Drawing.Point(9, 120);
            this.openClone.Name = "openClone";
            this.openClone.Size = new System.Drawing.Size(213, 13);
            this.openClone.TabIndex = 18;
            this.openClone.TabStop = true;
            this.openClone.Text = "Will only search cloned source-physical files";
            this.openClone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openClone_LinkClicked);
            // 
            // MemberSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 146);
            this.Controls.Add(this.openClone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.caseSense);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchVal);
            this.Controls.Add(this.spf);
            this.Controls.Add(this.lib);
            this.Controls.Add(this.search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MemberSearch";
            this.Text = "Member Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button search;
        private System.Windows.Forms.TextBox spf;
        private System.Windows.Forms.TextBox lib;
        private System.Windows.Forms.TextBox searchVal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox caseSense;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel openClone;
    }
}