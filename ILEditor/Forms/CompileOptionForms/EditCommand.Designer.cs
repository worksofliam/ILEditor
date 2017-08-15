namespace ILEditor.Forms.CompileOptionForms
{
    partial class EditCommand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCommand));
            this.command = new System.Windows.Forms.TextBox();
            this.types = new System.Windows.Forms.ComboBox();
            this.name = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // command
            // 
            this.command.Location = new System.Drawing.Point(12, 40);
            this.command.Name = "command";
            this.command.Size = new System.Drawing.Size(326, 20);
            this.command.TabIndex = 0;
            // 
            // types
            // 
            this.types.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.types.FormattingEnabled = true;
            this.types.Location = new System.Drawing.Point(12, 13);
            this.types.Name = "types";
            this.types.Size = new System.Drawing.Size(121, 21);
            this.types.Sorted = true;
            this.types.TabIndex = 1;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(139, 13);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(101, 20);
            this.name.TabIndex = 2;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(246, 12);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(92, 23);
            this.save.TabIndex = 3;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // EditCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 74);
            this.Controls.Add(this.save);
            this.Controls.Add(this.name);
            this.Controls.Add(this.types);
            this.Controls.Add(this.command);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditCommand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Command";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox command;
        private System.Windows.Forms.ComboBox types;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Button save;
    }
}