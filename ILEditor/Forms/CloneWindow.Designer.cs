namespace ILEditor.Forms
{
    partial class CloneWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloneWindow));
            this.memberList = new System.Windows.Forms.ListView();
            this.lib = new System.Windows.Forms.TextBox();
            this.fetch = new System.Windows.Forms.Button();
            this.clone = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // memberList
            // 
            this.memberList.BackColor = System.Drawing.Color.White;
            this.memberList.CheckBoxes = true;
            this.memberList.Enabled = false;
            this.memberList.Location = new System.Drawing.Point(12, 63);
            this.memberList.Name = "memberList";
            this.memberList.Size = new System.Drawing.Size(399, 285);
            this.memberList.TabIndex = 0;
            this.memberList.UseCompatibleStateImageBehavior = false;
            this.memberList.View = System.Windows.Forms.View.List;
            // 
            // lib
            // 
            this.lib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lib.Location = new System.Drawing.Point(187, 11);
            this.lib.MaxLength = 10;
            this.lib.Name = "lib";
            this.lib.Size = new System.Drawing.Size(224, 20);
            this.lib.TabIndex = 1;
            // 
            // fetch
            // 
            this.fetch.Location = new System.Drawing.Point(293, 37);
            this.fetch.Name = "fetch";
            this.fetch.Size = new System.Drawing.Size(118, 20);
            this.fetch.TabIndex = 3;
            this.fetch.Text = "Fetch Member List";
            this.fetch.UseVisualStyleBackColor = true;
            this.fetch.Click += new System.EventHandler(this.fetch_Click);
            // 
            // clone
            // 
            this.clone.Enabled = false;
            this.clone.Location = new System.Drawing.Point(336, 352);
            this.clone.Name = "clone";
            this.clone.Size = new System.Drawing.Size(75, 23);
            this.clone.TabIndex = 4;
            this.clone.Text = "Clone";
            this.clone.UseVisualStyleBackColor = true;
            this.clone.Click += new System.EventHandler(this.clone_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(12, 352);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Library";
            // 
            // CloneWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 385);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.clone);
            this.Controls.Add(this.fetch);
            this.Controls.Add(this.lib);
            this.Controls.Add(this.memberList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CloneWindow";
            this.Text = "Clone Source-Physical File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView memberList;
        private System.Windows.Forms.TextBox lib;
        private System.Windows.Forms.Button fetch;
        private System.Windows.Forms.Button clone;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label1;
    }
}