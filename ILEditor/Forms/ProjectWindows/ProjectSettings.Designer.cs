namespace ILEditor.Forms.ProjectWindows
{
    partial class ProjectSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectSettings));
            this.buildLib = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.projList = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.modList = new System.Windows.Forms.ListBox();
            this.modPath = new System.Windows.Forms.TextBox();
            this.addmod = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buildLib
            // 
            this.buildLib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.buildLib.Location = new System.Drawing.Point(286, 34);
            this.buildLib.MaxLength = 10;
            this.buildLib.Name = "buildLib";
            this.buildLib.Size = new System.Drawing.Size(100, 20);
            this.buildLib.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Build library";
            // 
            // projList
            // 
            this.projList.CheckBoxes = true;
            this.projList.Location = new System.Drawing.Point(99, 99);
            this.projList.Name = "projList";
            this.projList.Size = new System.Drawing.Size(287, 97);
            this.projList.TabIndex = 2;
            this.projList.UseCompatibleStateImageBehavior = false;
            this.projList.View = System.Windows.Forms.View.List;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Local project dependancies (pre-build)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Static module dependancies (object list)";
            // 
            // modList
            // 
            this.modList.FormattingEnabled = true;
            this.modList.Location = new System.Drawing.Point(99, 243);
            this.modList.Name = "modList";
            this.modList.Size = new System.Drawing.Size(287, 95);
            this.modList.TabIndex = 5;
            this.modList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.modList_KeyDown);
            // 
            // modPath
            // 
            this.modPath.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.modPath.Location = new System.Drawing.Point(180, 344);
            this.modPath.MaxLength = 21;
            this.modPath.Name = "modPath";
            this.modPath.Size = new System.Drawing.Size(206, 20);
            this.modPath.TabIndex = 6;
            this.modPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.modPath_KeyDown);
            // 
            // addmod
            // 
            this.addmod.Location = new System.Drawing.Point(99, 343);
            this.addmod.Name = "addmod";
            this.addmod.Size = new System.Drawing.Size(75, 20);
            this.addmod.TabIndex = 7;
            this.addmod.Text = "Add";
            this.addmod.UseVisualStyleBackColor = true;
            this.addmod.Click += new System.EventHandler(this.addmod_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(403, 407);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 8;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(12, 407);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 9;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // ProjectSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 442);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.save);
            this.Controls.Add(this.addmod);
            this.Controls.Add(this.modPath);
            this.Controls.Add(this.modList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.projList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buildLib);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProjectSettings";
            this.Text = "Project Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox buildLib;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView projList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox modList;
        private System.Windows.Forms.TextBox modPath;
        private System.Windows.Forms.Button addmod;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button cancel;
    }
}