namespace ILEditor
{
    partial class Editor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMemberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWelcomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libraryListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localCopiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sPFCloneToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rPGConversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cLFormatterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceProgramGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickMemberSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherForTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.editortabs = new System.Windows.Forms.TabControl();
            this.usercontrol = new System.Windows.Forms.TabControl();
            this.toolstabrightclick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.newButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.liblButton = new System.Windows.Forms.ToolStripButton();
            this.compileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.tool_bar = new System.Windows.Forms.ToolStrip();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolstabrightclick.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tool_bar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.compileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(833, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeMemberToolStripMenuItem,
            this.switchSystemToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memberToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // memberToolStripMenuItem
            // 
            this.memberToolStripMenuItem.Name = "memberToolStripMenuItem";
            this.memberToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.memberToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.memberToolStripMenuItem.Text = "Member";
            this.memberToolStripMenuItem.Click += new System.EventHandler(this.memberToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memberToolStripMenuItem1});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // memberToolStripMenuItem1
            // 
            this.memberToolStripMenuItem1.Name = "memberToolStripMenuItem1";
            this.memberToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.memberToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.memberToolStripMenuItem1.Text = "Member";
            this.memberToolStripMenuItem1.Click += new System.EventHandler(this.memberToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // closeMemberToolStripMenuItem
            // 
            this.closeMemberToolStripMenuItem.Name = "closeMemberToolStripMenuItem";
            this.closeMemberToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeMemberToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.closeMemberToolStripMenuItem.Text = "Close Member";
            this.closeMemberToolStripMenuItem.Visible = false;
            this.closeMemberToolStripMenuItem.Click += new System.EventHandler(this.closeMemberToolStripMenuItem_Click);
            // 
            // switchSystemToolStripMenuItem
            // 
            this.switchSystemToolStripMenuItem.Name = "switchSystemToolStripMenuItem";
            this.switchSystemToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.switchSystemToolStripMenuItem.Text = "Switch System";
            this.switchSystemToolStripMenuItem.Click += new System.EventHandler(this.switchSystemToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolboxToolStripMenuItem,
            this.openWelcomeToolStripMenuItem,
            this.connectionSettingsToolStripMenuItem,
            this.libraryListToolStripMenuItem,
            this.localCopiesToolStripMenuItem,
            this.languageToolsToolStripMenuItem,
            this.quickMemberSearchToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // openToolboxToolStripMenuItem
            // 
            this.openToolboxToolStripMenuItem.Name = "openToolboxToolStripMenuItem";
            this.openToolboxToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.openToolboxToolStripMenuItem.Text = "Open Toolbox";
            this.openToolboxToolStripMenuItem.Click += new System.EventHandler(this.openToolboxToolStripMenuItem_Click);
            // 
            // openWelcomeToolStripMenuItem
            // 
            this.openWelcomeToolStripMenuItem.Name = "openWelcomeToolStripMenuItem";
            this.openWelcomeToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.openWelcomeToolStripMenuItem.Text = "Open Welcome";
            this.openWelcomeToolStripMenuItem.Click += new System.EventHandler(this.openWelcomeToolStripMenuItem_Click);
            // 
            // connectionSettingsToolStripMenuItem
            // 
            this.connectionSettingsToolStripMenuItem.Name = "connectionSettingsToolStripMenuItem";
            this.connectionSettingsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.connectionSettingsToolStripMenuItem.Text = "Connection Settings";
            this.connectionSettingsToolStripMenuItem.Click += new System.EventHandler(this.connectionSettingsToolStripMenuItem_Click);
            // 
            // libraryListToolStripMenuItem
            // 
            this.libraryListToolStripMenuItem.Name = "libraryListToolStripMenuItem";
            this.libraryListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.libraryListToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.libraryListToolStripMenuItem.Text = "Library List";
            this.libraryListToolStripMenuItem.Click += new System.EventHandler(this.libraryListToolStripMenuItem_Click);
            // 
            // localCopiesToolStripMenuItem
            // 
            this.localCopiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sPFCloneToolStripMenuItem1,
            this.searchMembersToolStripMenuItem});
            this.localCopiesToolStripMenuItem.Name = "localCopiesToolStripMenuItem";
            this.localCopiesToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.localCopiesToolStripMenuItem.Text = "Local Copies";
            // 
            // sPFCloneToolStripMenuItem1
            // 
            this.sPFCloneToolStripMenuItem1.Name = "sPFCloneToolStripMenuItem1";
            this.sPFCloneToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.sPFCloneToolStripMenuItem1.Text = "SPF Clone";
            this.sPFCloneToolStripMenuItem1.Click += new System.EventHandler(this.sPFCloneToolStripMenuItem1_Click);
            // 
            // searchMembersToolStripMenuItem
            // 
            this.searchMembersToolStripMenuItem.Name = "searchMembersToolStripMenuItem";
            this.searchMembersToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.searchMembersToolStripMenuItem.Text = "Search Members";
            this.searchMembersToolStripMenuItem.Click += new System.EventHandler(this.searchMembersToolStripMenuItem_Click);
            // 
            // languageToolsToolStripMenuItem
            // 
            this.languageToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rPGConversionToolStripMenuItem,
            this.cLFormatterToolStripMenuItem,
            this.serviceProgramGeneratorToolStripMenuItem});
            this.languageToolsToolStripMenuItem.Name = "languageToolsToolStripMenuItem";
            this.languageToolsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.languageToolsToolStripMenuItem.Text = "Language Tools";
            // 
            // rPGConversionToolStripMenuItem
            // 
            this.rPGConversionToolStripMenuItem.Name = "rPGConversionToolStripMenuItem";
            this.rPGConversionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.rPGConversionToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.rPGConversionToolStripMenuItem.Text = "RPG Conversion";
            this.rPGConversionToolStripMenuItem.Click += new System.EventHandler(this.rPGConversionToolStripMenuItem_Click);
            // 
            // cLFormatterToolStripMenuItem
            // 
            this.cLFormatterToolStripMenuItem.Name = "cLFormatterToolStripMenuItem";
            this.cLFormatterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.X)));
            this.cLFormatterToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.cLFormatterToolStripMenuItem.Text = "CL Formatter";
            this.cLFormatterToolStripMenuItem.Click += new System.EventHandler(this.cLFormatterToolStripMenuItem_Click);
            // 
            // serviceProgramGeneratorToolStripMenuItem
            // 
            this.serviceProgramGeneratorToolStripMenuItem.Enabled = false;
            this.serviceProgramGeneratorToolStripMenuItem.Name = "serviceProgramGeneratorToolStripMenuItem";
            this.serviceProgramGeneratorToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.serviceProgramGeneratorToolStripMenuItem.Text = "Service Program Generator";
            this.serviceProgramGeneratorToolStripMenuItem.Click += new System.EventHandler(this.serviceProgramGeneratorToolStripMenuItem_Click);
            // 
            // quickMemberSearchToolStripMenuItem
            // 
            this.quickMemberSearchToolStripMenuItem.Name = "quickMemberSearchToolStripMenuItem";
            this.quickMemberSearchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.quickMemberSearchToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.quickMemberSearchToolStripMenuItem.Text = "Quick Member Search";
            this.quickMemberSearchToolStripMenuItem.Visible = false;
            this.quickMemberSearchToolStripMenuItem.Click += new System.EventHandler(this.quickMemberSearchToolStripMenuItem_Click);
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileCurrentToolStripMenuItem,
            this.otherForTypeToolStripMenuItem});
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.compileToolStripMenuItem.Text = "Compile";
            this.compileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.compileToolStripMenuItem_DropDownOpening);
            // 
            // compileCurrentToolStripMenuItem
            // 
            this.compileCurrentToolStripMenuItem.Name = "compileCurrentToolStripMenuItem";
            this.compileCurrentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.compileCurrentToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.compileCurrentToolStripMenuItem.Text = "Compile";
            this.compileCurrentToolStripMenuItem.Click += new System.EventHandler(this.compileCurrentToolStripMenuItem_Click);
            // 
            // otherForTypeToolStripMenuItem
            // 
            this.otherForTypeToolStripMenuItem.Name = "otherForTypeToolStripMenuItem";
            this.otherForTypeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.otherForTypeToolStripMenuItem.Text = "Compile Options";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(833, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(16, 17);
            this.statusLabel.Text = "...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.editortabs);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.usercontrol);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(833, 531);
            this.splitContainer1.SplitterDistance = 662;
            this.splitContainer1.TabIndex = 2;
            // 
            // editortabs
            // 
            this.editortabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editortabs.Location = new System.Drawing.Point(0, 0);
            this.editortabs.Name = "editortabs";
            this.editortabs.SelectedIndex = 0;
            this.editortabs.Size = new System.Drawing.Size(662, 531);
            this.editortabs.TabIndex = 0;
            this.editortabs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.editortabs_MouseClick);
            // 
            // usercontrol
            // 
            this.usercontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usercontrol.Location = new System.Drawing.Point(0, 0);
            this.usercontrol.Name = "usercontrol";
            this.usercontrol.SelectedIndex = 0;
            this.usercontrol.Size = new System.Drawing.Size(167, 531);
            this.usercontrol.TabIndex = 0;
            this.usercontrol.MouseClick += new System.Windows.Forms.MouseEventHandler(this.usercontrol_MouseClick);
            // 
            // toolstabrightclick
            // 
            this.toolstabrightclick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.toolstabrightclick.Name = "toolstabrightclick";
            this.toolstabrightclick.Size = new System.Drawing.Size(104, 26);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(833, 531);
            this.panel1.TabIndex = 4;
            // 
            // newButton
            // 
            this.newButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newButton.Image = ((System.Drawing.Image)(resources.GetObject("newButton.Image")));
            this.newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(23, 22);
            this.newButton.Text = "New Member";
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "Save Member";
            this.saveButton.ToolTipText = "Save Member";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // liblButton
            // 
            this.liblButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.liblButton.Image = ((System.Drawing.Image)(resources.GetObject("liblButton.Image")));
            this.liblButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.liblButton.Name = "liblButton";
            this.liblButton.Size = new System.Drawing.Size(23, 22);
            this.liblButton.Text = "Library List";
            this.liblButton.Click += new System.EventHandler(this.liblButton_Click);
            // 
            // compileButton
            // 
            this.compileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.compileButton.Image = ((System.Drawing.Image)(resources.GetObject("compileButton.Image")));
            this.compileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(23, 22);
            this.compileButton.Text = "Compile";
            this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // zoomInButton
            // 
            this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInButton.Image")));
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(23, 22);
            this.zoomInButton.Text = "toolStripButton1";
            this.zoomInButton.ToolTipText = "Zoom In";
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.Image")));
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
            this.zoomOutButton.Text = "Zoom Out";
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // tool_bar
            // 
            this.tool_bar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.saveButton,
            this.liblButton,
            this.compileButton,
            this.toolStripSeparator1,
            this.zoomInButton,
            this.zoomOutButton});
            this.tool_bar.Location = new System.Drawing.Point(0, 24);
            this.tool_bar.Name = "tool_bar";
            this.tool_bar.Size = new System.Drawing.Size(833, 25);
            this.tool_bar.TabIndex = 3;
            this.tool_bar.Text = "toolStrip1";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 602);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tool_bar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Editor";
            this.Text = "Idle - IBM i Development";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.Load += new System.EventHandler(this.Editor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolstabrightclick.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tool_bar.ResumeLayout(false);
            this.tool_bar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl usercontrol;
        private System.Windows.Forms.TabControl editortabs;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip toolstabrightclick;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWelcomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libraryListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem otherForTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem languageToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rPGConversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cLFormatterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchSystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localCopiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sPFCloneToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem serviceProgramGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchMembersToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem quickMemberSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMemberToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton newButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton liblButton;
        private System.Windows.Forms.ToolStripButton compileButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton zoomInButton;
        private System.Windows.Forms.ToolStripButton zoomOutButton;
        private System.Windows.Forms.ToolStrip tool_bar;
    }
}

