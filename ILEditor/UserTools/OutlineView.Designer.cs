namespace ILEditor.UserTools
{
    partial class OutlineView
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
            this.funcList = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // funcList
            // 
            this.funcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.funcList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.funcList.Location = new System.Drawing.Point(0, 0);
            this.funcList.Name = "funcList";
            this.funcList.Size = new System.Drawing.Size(284, 261);
            this.funcList.TabIndex = 0;
            this.funcList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.funcList_NodeMouseDoubleClick);
            // 
            // OutlineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.CloseButton = false;
            this.Controls.Add(this.funcList);
            this.Name = "OutlineView";
            this.Text = "Outline View";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView funcList;
    }
}
