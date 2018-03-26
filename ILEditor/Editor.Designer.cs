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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourcePhysicalFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.streamFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.localFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWelcomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libraryListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.start5250SessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickMemberSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localCopiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sPFCloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sPFPushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rPGConversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cLFormattingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentAssistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.compileOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutILEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionFTPLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.columnText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newMember = new System.Windows.Forms.ToolStripButton();
            this.saveSource = new System.Windows.Forms.ToolStripButton();
            this.compileButton = new System.Windows.Forms.ToolStripButton();
            this.liblButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.commentButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.acsButton = new System.Windows.Forms.ToolStripButton();
            this.dbgButton = new System.Windows.Forms.ToolStripButton();
            this.dockingPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.startRemoteDebugACSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.sourceToolStripMenuItem,
            this.compileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(681, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.switchSystemToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memberToolStripMenuItem,
            this.sourcePhysicalFileToolStripMenuItem,
            this.streamFileToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // memberToolStripMenuItem
            // 
            this.memberToolStripMenuItem.Name = "memberToolStripMenuItem";
            this.memberToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.memberToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.memberToolStripMenuItem.Text = "Member";
            this.memberToolStripMenuItem.Click += new System.EventHandler(this.memberToolStripMenuItem_Click);
            // 
            // sourcePhysicalFileToolStripMenuItem
            // 
            this.sourcePhysicalFileToolStripMenuItem.Name = "sourcePhysicalFileToolStripMenuItem";
            this.sourcePhysicalFileToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.sourcePhysicalFileToolStripMenuItem.Text = "Source-Physical File";
            this.sourcePhysicalFileToolStripMenuItem.Click += new System.EventHandler(this.sourcePhysicalFileToolStripMenuItem_Click);
            // 
            // streamFileToolStripMenuItem
            // 
            this.streamFileToolStripMenuItem.Name = "streamFileToolStripMenuItem";
            this.streamFileToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.streamFileToolStripMenuItem.Text = "Stream file";
            this.streamFileToolStripMenuItem.Click += new System.EventHandler(this.streamFileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memberToolStripMenuItem1,
            this.streamFileToolStripMenuItem1,
            this.localFileToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
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
            // streamFileToolStripMenuItem1
            // 
            this.streamFileToolStripMenuItem1.Name = "streamFileToolStripMenuItem1";
            this.streamFileToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.streamFileToolStripMenuItem1.Text = "Stream File";
            this.streamFileToolStripMenuItem1.Click += new System.EventHandler(this.streamFileToolStripMenuItem1_Click);
            // 
            // localFileToolStripMenuItem
            // 
            this.localFileToolStripMenuItem.Name = "localFileToolStripMenuItem";
            this.localFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.localFileToolStripMenuItem.Text = "Local File";
            this.localFileToolStripMenuItem.Click += new System.EventHandler(this.localFileToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.saveAsToolStripMenuItem.Text = "Save-As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // switchSystemToolStripMenuItem
            // 
            this.switchSystemToolStripMenuItem.Name = "switchSystemToolStripMenuItem";
            this.switchSystemToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
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
            this.start5250SessionToolStripMenuItem,
            this.startRemoteDebugACSToolStripMenuItem,
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
            // start5250SessionToolStripMenuItem
            // 
            this.start5250SessionToolStripMenuItem.Name = "start5250SessionToolStripMenuItem";
            this.start5250SessionToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.start5250SessionToolStripMenuItem.Text = "Start 5250 Emulator (ACS)";
            this.start5250SessionToolStripMenuItem.Click += new System.EventHandler(this.start5250SessionToolStripMenuItem_Click);
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
            // sourceToolStripMenuItem
            // 
            this.sourceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localCopiesToolStripMenuItem,
            this.languageToolsToolStripMenuItem,
            this.quickCommentToolStripMenuItem,
            this.duplicateLineToolStripMenuItem,
            this.sourceDiffToolStripMenuItem,
            this.contentAssistToolStripMenuItem});
            this.sourceToolStripMenuItem.Name = "sourceToolStripMenuItem";
            this.sourceToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.sourceToolStripMenuItem.Text = "Source";
            // 
            // localCopiesToolStripMenuItem
            // 
            this.localCopiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sPFCloneToolStripMenuItem,
            this.sPFPushToolStripMenuItem,
            this.memberSearchToolStripMenuItem});
            this.localCopiesToolStripMenuItem.Name = "localCopiesToolStripMenuItem";
            this.localCopiesToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.localCopiesToolStripMenuItem.Text = "Local Copies";
            // 
            // sPFCloneToolStripMenuItem
            // 
            this.sPFCloneToolStripMenuItem.Name = "sPFCloneToolStripMenuItem";
            this.sPFCloneToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.sPFCloneToolStripMenuItem.Text = "SPF Clone";
            this.sPFCloneToolStripMenuItem.Click += new System.EventHandler(this.sPFCloneToolStripMenuItem_Click);
            // 
            // sPFPushToolStripMenuItem
            // 
            this.sPFPushToolStripMenuItem.Name = "sPFPushToolStripMenuItem";
            this.sPFPushToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.sPFPushToolStripMenuItem.Text = "SPF Push";
            this.sPFPushToolStripMenuItem.Click += new System.EventHandler(this.sPFPushToolStripMenuItem_Click);
            // 
            // memberSearchToolStripMenuItem
            // 
            this.memberSearchToolStripMenuItem.Name = "memberSearchToolStripMenuItem";
            this.memberSearchToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.memberSearchToolStripMenuItem.Text = "Member Search";
            this.memberSearchToolStripMenuItem.Click += new System.EventHandler(this.memberSearchToolStripMenuItem_Click);
            // 
            // languageToolsToolStripMenuItem
            // 
            this.languageToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rPGConversionToolStripMenuItem,
            this.cLFormattingToolStripMenuItem,
            this.generateSQLToolStripMenuItem});
            this.languageToolsToolStripMenuItem.Name = "languageToolsToolStripMenuItem";
            this.languageToolsToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
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
            // cLFormattingToolStripMenuItem
            // 
            this.cLFormattingToolStripMenuItem.Name = "cLFormattingToolStripMenuItem";
            this.cLFormattingToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.X)));
            this.cLFormattingToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.cLFormattingToolStripMenuItem.Text = "CL Formatter";
            this.cLFormattingToolStripMenuItem.Click += new System.EventHandler(this.cLFormattingToolStripMenuItem_Click);
            // 
            // generateSQLToolStripMenuItem
            // 
            this.generateSQLToolStripMenuItem.Name = "generateSQLToolStripMenuItem";
            this.generateSQLToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.generateSQLToolStripMenuItem.Text = "Generate SQL";
            this.generateSQLToolStripMenuItem.Click += new System.EventHandler(this.generateSQLToolStripMenuItem_Click);
            // 
            // quickCommentToolStripMenuItem
            // 
            this.quickCommentToolStripMenuItem.Name = "quickCommentToolStripMenuItem";
            this.quickCommentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.quickCommentToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.quickCommentToolStripMenuItem.Text = "Quick Comment";
            this.quickCommentToolStripMenuItem.Click += new System.EventHandler(this.quickCommentToolStripMenuItem_Click);
            // 
            // duplicateLineToolStripMenuItem
            // 
            this.duplicateLineToolStripMenuItem.Name = "duplicateLineToolStripMenuItem";
            this.duplicateLineToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.duplicateLineToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.duplicateLineToolStripMenuItem.Text = "Duplicate Line";
            this.duplicateLineToolStripMenuItem.Visible = false;
            this.duplicateLineToolStripMenuItem.Click += new System.EventHandler(this.duplicateLineToolStripMenuItem_Click);
            // 
            // sourceDiffToolStripMenuItem
            // 
            this.sourceDiffToolStripMenuItem.Name = "sourceDiffToolStripMenuItem";
            this.sourceDiffToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.sourceDiffToolStripMenuItem.Text = "Source Diff";
            this.sourceDiffToolStripMenuItem.Click += new System.EventHandler(this.sourceDiffToolStripMenuItem_Click);
            // 
            // contentAssistToolStripMenuItem
            // 
            this.contentAssistToolStripMenuItem.Name = "contentAssistToolStripMenuItem";
            this.contentAssistToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.contentAssistToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.contentAssistToolStripMenuItem.Text = "Content-Assist";
            this.contentAssistToolStripMenuItem.Visible = false;
            this.contentAssistToolStripMenuItem.Click += new System.EventHandler(this.contentAssistToolStripMenuItem_Click);
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileToolStripMenuItem1,
            this.compileOptionsToolStripMenuItem});
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.compileToolStripMenuItem.Text = "Compile";
            this.compileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.compileToolStripMenuItem_DropDownOpening);
            // 
            // compileToolStripMenuItem1
            // 
            this.compileToolStripMenuItem1.Name = "compileToolStripMenuItem1";
            this.compileToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.compileToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            this.compileToolStripMenuItem1.Text = "Compile";
            this.compileToolStripMenuItem1.Click += new System.EventHandler(this.compileToolStripMenuItem1_Click);
            // 
            // compileOptionsToolStripMenuItem
            // 
            this.compileOptionsToolStripMenuItem.Name = "compileOptionsToolStripMenuItem";
            this.compileOptionsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.compileOptionsToolStripMenuItem.Text = "Compile Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutILEditorToolStripMenuItem,
            this.sessionFTPLogToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutILEditorToolStripMenuItem
            // 
            this.aboutILEditorToolStripMenuItem.Name = "aboutILEditorToolStripMenuItem";
            this.aboutILEditorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.aboutILEditorToolStripMenuItem.Text = "About ILEditor";
            this.aboutILEditorToolStripMenuItem.Click += new System.EventHandler(this.aboutILEditorToolStripMenuItem_Click);
            // 
            // sessionFTPLogToolStripMenuItem
            // 
            this.sessionFTPLogToolStripMenuItem.Name = "sessionFTPLogToolStripMenuItem";
            this.sessionFTPLogToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.sessionFTPLogToolStripMenuItem.Text = "Session FTP Log";
            this.sessionFTPLogToolStripMenuItem.Click += new System.EventHandler(this.sessionFTPLogToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText,
            this.columnText});
            this.statusStrip.Location = new System.Drawing.Point(0, 566);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(681, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusText
            // 
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(16, 17);
            this.statusText.Text = "...";
            // 
            // columnText
            // 
            this.columnText.Name = "columnText";
            this.columnText.Size = new System.Drawing.Size(16, 17);
            this.columnText.Text = "...";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMember,
            this.saveSource,
            this.compileButton,
            this.liblButton,
            this.toolStripSeparator3,
            this.undoButton,
            this.redoButton,
            this.commentButton,
            this.toolStripSeparator1,
            this.zoomOutButton,
            this.zoomInButton,
            this.toolStripSeparator2,
            this.acsButton,
            this.dbgButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(681, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // newMember
            // 
            this.newMember.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newMember.Image = global::ILEditor.Properties.Resources.edit1;
            this.newMember.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newMember.Name = "newMember";
            this.newMember.Size = new System.Drawing.Size(23, 22);
            this.newMember.Text = "New Member";
            this.newMember.ToolTipText = "New Member";
            this.newMember.Click += new System.EventHandler(this.newMember_Click);
            // 
            // saveSource
            // 
            this.saveSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveSource.Image = global::ILEditor.Properties.Resources.save;
            this.saveSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveSource.Name = "saveSource";
            this.saveSource.Size = new System.Drawing.Size(23, 22);
            this.saveSource.Text = "Save Source";
            this.saveSource.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // compileButton
            // 
            this.compileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.compileButton.Image = global::ILEditor.Properties.Resources.compile;
            this.compileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(23, 22);
            this.compileButton.Text = "Compile Source";
            this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
            // 
            // liblButton
            // 
            this.liblButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.liblButton.Image = global::ILEditor.Properties.Resources.books;
            this.liblButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.liblButton.Name = "liblButton";
            this.liblButton.Size = new System.Drawing.Size(23, 22);
            this.liblButton.Text = "Library List";
            this.liblButton.Click += new System.EventHandler(this.libraryListToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Image = global::ILEditor.Properties.Resources.undo;
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(23, 22);
            this.undoButton.Text = "Undo";
            this.undoButton.ToolTipText = "Undo";
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Image = global::ILEditor.Properties.Resources.redo;
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(23, 22);
            this.redoButton.Text = "Redo";
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // commentButton
            // 
            this.commentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.commentButton.Image = global::ILEditor.Properties.Resources.comment_sign;
            this.commentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.commentButton.Name = "commentButton";
            this.commentButton.Size = new System.Drawing.Size(23, 22);
            this.commentButton.Text = "Comment Selected";
            this.commentButton.Click += new System.EventHandler(this.commentButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = global::ILEditor.Properties.Resources.zoom_out;
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
            this.zoomOutButton.Text = "Zoom Out";
            this.zoomOutButton.ToolTipText = "Zoom Out";
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // zoomInButton
            // 
            this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInButton.Image = global::ILEditor.Properties.Resources.zoom_in;
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(23, 22);
            this.zoomInButton.Text = "Zoom In";
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // acsButton
            // 
            this.acsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.acsButton.Image = global::ILEditor.Properties.Resources.computer;
            this.acsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.acsButton.Name = "acsButton";
            this.acsButton.Size = new System.Drawing.Size(23, 22);
            this.acsButton.Text = "Launch ACS";
            this.acsButton.Click += new System.EventHandler(this.acsButton_Click);
            // 
            // dbgButton
            // 
            this.dbgButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dbgButton.Image = global::ILEditor.Properties.Resources.bug;
            this.dbgButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dbgButton.Name = "dbgButton";
            this.dbgButton.Size = new System.Drawing.Size(23, 22);
            this.dbgButton.Text = "Launch Debug";
            this.dbgButton.ToolTipText = "Launch Debug";
            this.dbgButton.Click += new System.EventHandler(this.dbgButton_Click);
            // 
            // dockingPanel
            // 
            this.dockingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockingPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockingPanel.Location = new System.Drawing.Point(0, 49);
            this.dockingPanel.Name = "dockingPanel";
            this.dockingPanel.Size = new System.Drawing.Size(681, 517);
            this.dockingPanel.TabIndex = 15;
            // 
            // startRemoteDebugACSToolStripMenuItem
            // 
            this.startRemoteDebugACSToolStripMenuItem.Name = "startRemoteDebugACSToolStripMenuItem";
            this.startRemoteDebugACSToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.startRemoteDebugACSToolStripMenuItem.Text = "Start Remote Debug (ACS)";
            this.startRemoteDebugACSToolStripMenuItem.Click += new System.EventHandler(this.startRemoteDebugACSToolStripMenuItem_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 588);
            this.Controls.Add(this.dockingPanel);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Editor";
            this.Text = "ILEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchSystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourcePhysicalFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem streamFileToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.ToolStripStatusLabel columnText;
        private System.Windows.Forms.ToolStripMenuItem localFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem compileOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWelcomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libraryListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem start5250SessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localCopiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickCommentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sPFCloneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sPFPushToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rPGConversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cLFormattingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateSQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutILEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sessionFTPLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickMemberSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateLineToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton saveSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton undoButton;
        private System.Windows.Forms.ToolStripButton redoButton;
        private System.Windows.Forms.ToolStripButton compileButton;
        private System.Windows.Forms.ToolStripButton commentButton;
        private System.Windows.Forms.ToolStripMenuItem sourceDiffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentAssistToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton newMember;
        private System.Windows.Forms.ToolStripButton liblButton;
        private System.Windows.Forms.ToolStripButton acsButton;
        private System.Windows.Forms.ToolStripButton zoomOutButton;
        private System.Windows.Forms.ToolStripButton zoomInButton;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockingPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton dbgButton;
        private System.Windows.Forms.ToolStripMenuItem startRemoteDebugACSToolStripMenuItem;
    }
}