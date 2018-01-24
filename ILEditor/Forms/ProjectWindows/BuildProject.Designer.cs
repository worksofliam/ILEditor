namespace ILEditor.Forms.ProjectWindows
{
    partial class BuildProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildProject));
            this.build = new System.Windows.Forms.Button();
            this.projList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // build
            // 
            this.build.Location = new System.Drawing.Point(15, 57);
            this.build.Name = "build";
            this.build.Size = new System.Drawing.Size(227, 23);
            this.build.TabIndex = 0;
            this.build.Text = "Build";
            this.build.UseVisualStyleBackColor = true;
            this.build.Click += new System.EventHandler(this.build_Click);
            // 
            // projList
            // 
            this.projList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.projList.FormattingEnabled = true;
            this.projList.Location = new System.Drawing.Point(121, 12);
            this.projList.Name = "projList";
            this.projList.Size = new System.Drawing.Size(121, 21);
            this.projList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Local Project";
            // 
            // BuildProject
            // 
            this.AcceptButton = this.build;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 90);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.projList);
            this.Controls.Add(this.build);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BuildProject";
            this.Text = "Build Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button build;
        private System.Windows.Forms.ComboBox projList;
        private System.Windows.Forms.Label label1;
    }
}