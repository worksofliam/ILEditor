namespace ILEditor.Forms
{
    partial class CoverageEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoverageEdit));
            this.covcmd = new System.Windows.Forms.TextBox();
            this.covname = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.moduleType = new System.Windows.Forms.ComboBox();
            this.customModules = new System.Windows.Forms.ListBox();
            this.customModule = new System.Windows.Forms.TextBox();
            this.delMod = new System.Windows.Forms.Button();
            this.addMod = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // covcmd
            // 
            this.covcmd.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.covcmd.Location = new System.Drawing.Point(129, 11);
            this.covcmd.MaxLength = 256;
            this.covcmd.Name = "covcmd";
            this.covcmd.Size = new System.Drawing.Size(370, 25);
            this.covcmd.TabIndex = 1;
            // 
            // covname
            // 
            this.covname.Location = new System.Drawing.Point(342, 79);
            this.covname.Name = "covname";
            this.covname.Size = new System.Drawing.Size(155, 20);
            this.covname.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.moduleType);
            this.groupBox2.Controls.Add(this.customModules);
            this.groupBox2.Controls.Add(this.customModule);
            this.groupBox2.Controls.Add(this.delMod);
            this.groupBox2.Controls.Add(this.addMod);
            this.groupBox2.Location = new System.Drawing.Point(15, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 109);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Custom Modules to Add";
            // 
            // moduleType
            // 
            this.moduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleType.FormattingEnabled = true;
            this.moduleType.Items.AddRange(new object[] {
            "*PGM",
            "*SRVPGM"});
            this.moduleType.Location = new System.Drawing.Point(6, 48);
            this.moduleType.Name = "moduleType";
            this.moduleType.Size = new System.Drawing.Size(119, 21);
            this.moduleType.TabIndex = 8;
            // 
            // customModules
            // 
            this.customModules.FormattingEnabled = true;
            this.customModules.Location = new System.Drawing.Point(131, 22);
            this.customModules.Name = "customModules";
            this.customModules.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.customModules.Size = new System.Drawing.Size(184, 56);
            this.customModules.TabIndex = 4;
            this.customModules.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customModules_KeyDown);
            // 
            // customModule
            // 
            this.customModule.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.customModule.Location = new System.Drawing.Point(6, 22);
            this.customModule.MaxLength = 21;
            this.customModule.Name = "customModule";
            this.customModule.Size = new System.Drawing.Size(119, 20);
            this.customModule.TabIndex = 5;
            this.customModule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customModule_KeyDown);
            // 
            // delMod
            // 
            this.delMod.Location = new System.Drawing.Point(131, 84);
            this.delMod.Name = "delMod";
            this.delMod.Size = new System.Drawing.Size(184, 21);
            this.delMod.TabIndex = 7;
            this.delMod.Text = "Delete Selected";
            this.delMod.UseVisualStyleBackColor = true;
            this.delMod.Click += new System.EventHandler(this.delMod_Click);
            // 
            // addMod
            // 
            this.addMod.Location = new System.Drawing.Point(6, 75);
            this.addMod.Name = "addMod";
            this.addMod.Size = new System.Drawing.Size(119, 30);
            this.addMod.TabIndex = 6;
            this.addMod.Text = "Add Module";
            this.addMod.UseVisualStyleBackColor = true;
            this.addMod.Click += new System.EventHandler(this.addMod_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Command to Run";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Coverage Test Name";
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(342, 137);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(157, 23);
            this.save.TabIndex = 11;
            this.save.Text = "Save and Finish";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(342, 108);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(157, 23);
            this.cancel.TabIndex = 12;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // CoverageEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 169);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.covname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.covcmd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CoverageEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Coverage Edit";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox covcmd;
        private System.Windows.Forms.TextBox covname;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox customModules;
        private System.Windows.Forms.TextBox customModule;
        private System.Windows.Forms.Button delMod;
        private System.Windows.Forms.Button addMod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox moduleType;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button cancel;
    }
}