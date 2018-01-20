namespace ILEditor.UserTools
{
    partial class BindingDirectory
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Loading..");
            this.entriesList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.objectName = new System.Windows.Forms.ToolStripTextBox();
            this.objectType = new System.Windows.Forms.ToolStripComboBox();
            this.objectLib = new System.Windows.Forms.ToolStripTextBox();
            this.objectActivation = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // entriesList
            // 
            this.entriesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.entriesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entriesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entriesList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.entriesList.Location = new System.Drawing.Point(0, 0);
            this.entriesList.MultiSelect = false;
            this.entriesList.Name = "entriesList";
            this.entriesList.Size = new System.Drawing.Size(672, 494);
            this.entriesList.TabIndex = 0;
            this.entriesList.UseCompatibleStateImageBehavior = false;
            this.entriesList.View = System.Windows.Forms.View.Details;
            this.entriesList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.entriesList_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Object";
            this.columnHeader1.Width = 145;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 106;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Library";
            this.columnHeader3.Width = 103;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Activation";
            this.columnHeader4.Width = 108;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Date";
            this.columnHeader5.Width = 95;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Time";
            this.columnHeader6.Width = 96;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectName,
            this.objectType,
            this.objectLib,
            this.objectActivation,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 469);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(672, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // objectName
            // 
            this.objectName.AutoToolTip = true;
            this.objectName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.objectName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.objectName.MaxLength = 10;
            this.objectName.Name = "objectName";
            this.objectName.Size = new System.Drawing.Size(100, 25);
            this.objectName.ToolTipText = "Object";
            // 
            // objectType
            // 
            this.objectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectType.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.objectType.Items.AddRange(new object[] {
            "*SRVPGM",
            "*MODULE"});
            this.objectType.Name = "objectType";
            this.objectType.Size = new System.Drawing.Size(121, 25);
            this.objectType.ToolTipText = "Object Type";
            // 
            // objectLib
            // 
            this.objectLib.AutoToolTip = true;
            this.objectLib.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.objectLib.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.objectLib.MaxLength = 10;
            this.objectLib.Name = "objectLib";
            this.objectLib.Size = new System.Drawing.Size(100, 25);
            this.objectLib.ToolTipText = "Object Library";
            // 
            // objectActivation
            // 
            this.objectActivation.AutoToolTip = true;
            this.objectActivation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectActivation.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.objectActivation.Items.AddRange(new object[] {
            "*IMMED",
            "*DEFER"});
            this.objectActivation.Name = "objectActivation";
            this.objectActivation.Size = new System.Drawing.Size(121, 25);
            this.objectActivation.ToolTipText = "Activation";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::ILEditor.Properties.Resources.notebook;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton1.Text = "Add Entry";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // BindingDirectory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.entriesList);
            this.Name = "BindingDirectory";
            this.Size = new System.Drawing.Size(672, 494);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView entriesList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox objectName;
        private System.Windows.Forms.ToolStripComboBox objectType;
        private System.Windows.Forms.ToolStripTextBox objectLib;
        private System.Windows.Forms.ToolStripComboBox objectActivation;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}
