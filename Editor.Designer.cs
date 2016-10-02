
namespace ZONEDOCTOR
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.romInfo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.loadRom = new System.Windows.Forms.ToolStripButton();
            this.loadRomTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.openEventScripts = new System.Windows.Forms.ToolStripButton();
            this.openLocations = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.recentFiles = new System.Windows.Forms.ToolStripDropDownButton();
            this.refreshROM = new System.Windows.Forms.ToolStripButton();
            this.closeROM = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.openSettings = new System.Windows.Forms.ToolStripButton();
            this.restoreElementsToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.history = new System.Windows.Forms.ToolStripButton();
            this.showROMInfo = new System.Windows.Forms.ToolStripButton();
            this.hexViewer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.info = new System.Windows.Forms.ToolStripButton();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.openAll = new System.Windows.Forms.ToolStripButton();
            this.closeAll = new System.Windows.Forms.ToolStripButton();
            this.restoreAll = new System.Windows.Forms.ToolStripButton();
            this.minimizeAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.docking = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.loadAllData = new System.Windows.Forms.ToolStripButton();
            this.clearModel = new System.Windows.Forms.ToolStripButton();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.toolStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(528, 75);
            this.panel4.TabIndex = 317;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.romInfo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 50);
            this.panel1.TabIndex = 332;
            // 
            // romInfo
            // 
            this.romInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.romInfo.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.romInfo.Location = new System.Drawing.Point(94, 0);
            this.romInfo.Name = "romInfo";
            this.romInfo.ReadOnly = true;
            this.romInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.romInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.romInfo.Size = new System.Drawing.Size(434, 50);
            this.romInfo.TabIndex = 4;
            this.romInfo.Text = "";
            this.romInfo.WordWrap = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(94, 50);
            this.label1.TabIndex = 331;
            this.label1.Text = "Rom Name\nHeader\nChecksum\nGamecode";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRom,
            this.loadRomTextBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(528, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 328;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.SizeChanged += new System.EventHandler(this.toolStrip1_SizeChanged);
            // 
            // loadRom
            // 
            this.loadRom.AutoSize = false;
            this.loadRom.Image = global::ZONEDOCTOR.Properties.Resources.cartridge;
            this.loadRom.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.loadRom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadRom.Name = "loadRom";
            this.loadRom.Size = new System.Drawing.Size(93, 22);
            this.loadRom.Text = "Load ROM...";
            this.loadRom.Click += new System.EventHandler(this.loadRom_Click);
            // 
            // loadRomTextBox
            // 
            this.loadRomTextBox.AutoSize = false;
            this.loadRomTextBox.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadRomTextBox.Name = "loadRomTextBox";
            this.loadRomTextBox.ReadOnly = true;
            this.loadRomTextBox.Size = new System.Drawing.Size(433, 18);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip2.Enabled = false;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openEventScripts,
            this.openLocations,
            this.toolStripSeparator1,
            this.openProject});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 100);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(96, 409);
            this.toolStrip2.TabIndex = 329;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // openEventScripts
            // 
            this.openEventScripts.Image = global::ZONEDOCTOR.Properties.Resources.mainEventScripts;
            this.openEventScripts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openEventScripts.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openEventScripts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openEventScripts.Name = "openEventScripts";
            this.openEventScripts.Size = new System.Drawing.Size(94, 22);
            this.openEventScripts.Text = "Event Scripts";
            this.openEventScripts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openEventScripts.ToolTipText = "Edit event scripts and their respective command collections";
            this.openEventScripts.Click += new System.EventHandler(this.openEventScripts_Click);
            // 
            // openLocations
            // 
            this.openLocations.Image = global::ZONEDOCTOR.Properties.Resources.mainLevels;
            this.openLocations.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openLocations.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openLocations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openLocations.Name = "openLocations";
            this.openLocations.Size = new System.Drawing.Size(94, 22);
            this.openLocations.Text = "Locations";
            this.openLocations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openLocations.ToolTipText = "Edit location maps, NPCs, exits and event fields,  etc.";
            this.openLocations.Click += new System.EventHandler(this.openLocations_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(94, 6);
            // 
            // openProject
            // 
            this.openProject.Image = global::ZONEDOCTOR.Properties.Resources.mainNotes;
            this.openProject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openProject.Name = "openProject";
            this.openProject.Size = new System.Drawing.Size(94, 22);
            this.openProject.Text = "Project";
            this.openProject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openProject.ToolTipText = "Open the project database";
            this.openProject.Click += new System.EventHandler(this.openProject_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(357, 6);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(104, 6);
            // 
            // panel2
            // 
            this.panel2.AllowDrop = true;
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(96, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(432, 384);
            this.panel2.TabIndex = 333;
            // 
            // toolStrip4
            // 
            this.toolStrip4.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recentFiles,
            this.refreshROM,
            this.closeROM,
            this.toolStripSeparator5,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator11,
            this.openSettings,
            this.restoreElementsToolStripMenuItem,
            this.toolStripSeparator15,
            this.history,
            this.showROMInfo,
            this.hexViewer,
            this.toolStripSeparator4,
            this.info});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip4.Size = new System.Drawing.Size(528, 25);
            this.toolStrip4.TabIndex = 334;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // recentFiles
            // 
            this.recentFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recentFiles.Image = global::ZONEDOCTOR.Properties.Resources.recentFiles;
            this.recentFiles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.recentFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recentFiles.Name = "recentFiles";
            this.recentFiles.Size = new System.Drawing.Size(31, 22);
            this.recentFiles.ToolTipText = "Recent ROM Files";
            // 
            // refreshROM
            // 
            this.refreshROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshROM.Enabled = false;
            this.refreshROM.Image = global::ZONEDOCTOR.Properties.Resources.cartridgeReload;
            this.refreshROM.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.refreshROM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshROM.Name = "refreshROM";
            this.refreshROM.Size = new System.Drawing.Size(23, 22);
            this.refreshROM.ToolTipText = "Reload ROM";
            this.refreshROM.Click += new System.EventHandler(this.refreshROM_Click);
            // 
            // closeROM
            // 
            this.closeROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeROM.Enabled = false;
            this.closeROM.Image = global::ZONEDOCTOR.Properties.Resources.cartridgeClose;
            this.closeROM.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.closeROM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeROM.Name = "closeROM";
            this.closeROM.Size = new System.Drawing.Size(23, 22);
            this.closeROM.ToolTipText = "Close ROM";
            this.closeROM.Click += new System.EventHandler(this.closeROM_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.save_small;
            this.saveToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripMenuItem.ToolTipText = "Save ROM";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.saveAs_small;
            this.saveAsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveAsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(23, 22);
            this.saveAsToolStripMenuItem.ToolTipText = "Save ROM As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // openSettings
            // 
            this.openSettings.Image = global::ZONEDOCTOR.Properties.Resources.settings;
            this.openSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openSettings.Name = "openSettings";
            this.openSettings.Size = new System.Drawing.Size(23, 22);
            this.openSettings.ToolTipText = "Settings";
            this.openSettings.Click += new System.EventHandler(this.openSettings_Click);
            // 
            // restoreElementsToolStripMenuItem
            // 
            this.restoreElementsToolStripMenuItem.Enabled = false;
            this.restoreElementsToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.importBinary;
            this.restoreElementsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.restoreElementsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.restoreElementsToolStripMenuItem.Name = "restoreElementsToolStripMenuItem";
            this.restoreElementsToolStripMenuItem.Size = new System.Drawing.Size(23, 22);
            this.restoreElementsToolStripMenuItem.ToolTipText = "Import elements from another ROM";
            this.restoreElementsToolStripMenuItem.Click += new System.EventHandler(this.restoreElementsToolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // history
            // 
            this.history.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.history.Image = global::ZONEDOCTOR.Properties.Resources.history;
            this.history.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.history.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(23, 22);
            this.history.ToolTipText = "Event History";
            this.history.Click += new System.EventHandler(this.history_Click);
            // 
            // showROMInfo
            // 
            this.showROMInfo.Checked = true;
            this.showROMInfo.CheckOnClick = true;
            this.showROMInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showROMInfo.Enabled = false;
            this.showROMInfo.Image = global::ZONEDOCTOR.Properties.Resources.romInfo;
            this.showROMInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showROMInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showROMInfo.Name = "showROMInfo";
            this.showROMInfo.Size = new System.Drawing.Size(23, 22);
            this.showROMInfo.ToolTipText = "Show ROM Info";
            this.showROMInfo.Click += new System.EventHandler(this.showROMInfo_Click);
            // 
            // hexViewer
            // 
            this.hexViewer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hexViewer.Image = global::ZONEDOCTOR.Properties.Resources.hexEditor;
            this.hexViewer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.hexViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hexViewer.Name = "hexViewer";
            this.hexViewer.Size = new System.Drawing.Size(23, 22);
            this.hexViewer.Text = "Open Hex Viewer";
            this.hexViewer.Click += new System.EventHandler(this.hexViewer_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // info
            // 
            this.info.Image = global::ZONEDOCTOR.Properties.Resources.about_small;
            this.info.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.info.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(23, 22);
            this.info.ToolTipText = "About";
            this.info.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Enabled = false;
            this.toolStrip3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAll,
            this.closeAll,
            this.restoreAll,
            this.minimizeAll,
            this.toolStripSeparator2,
            this.docking,
            this.toolStripSeparator3,
            this.loadAllData,
            this.clearModel});
            this.toolStrip3.Location = new System.Drawing.Point(96, 100);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(432, 25);
            this.toolStrip3.TabIndex = 335;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // openAll
            // 
            this.openAll.Image = global::ZONEDOCTOR.Properties.Resources.openAll;
            this.openAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openAll.Name = "openAll";
            this.openAll.Size = new System.Drawing.Size(23, 22);
            this.openAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAll.ToolTipText = "Open All Editors";
            this.openAll.Click += new System.EventHandler(this.openAll_Click);
            // 
            // closeAll
            // 
            this.closeAll.Image = global::ZONEDOCTOR.Properties.Resources.closeAll;
            this.closeAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.closeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeAll.Name = "closeAll";
            this.closeAll.Size = new System.Drawing.Size(23, 22);
            this.closeAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeAll.ToolTipText = "Close All Editors";
            this.closeAll.Click += new System.EventHandler(this.closeAll_Click);
            // 
            // restoreAll
            // 
            this.restoreAll.Image = global::ZONEDOCTOR.Properties.Resources.restoreAll;
            this.restoreAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.restoreAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.restoreAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.restoreAll.Name = "restoreAll";
            this.restoreAll.Size = new System.Drawing.Size(23, 22);
            this.restoreAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.restoreAll.ToolTipText = "Restore All Editors";
            this.restoreAll.Click += new System.EventHandler(this.restoreAll_Click);
            // 
            // minimizeAll
            // 
            this.minimizeAll.Image = global::ZONEDOCTOR.Properties.Resources.minimizeAll;
            this.minimizeAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.minimizeAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.minimizeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.minimizeAll.Name = "minimizeAll";
            this.minimizeAll.Size = new System.Drawing.Size(23, 22);
            this.minimizeAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.minimizeAll.ToolTipText = "Minimize All Editors";
            this.minimizeAll.Click += new System.EventHandler(this.minimizeAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // docking
            // 
            this.docking.CheckOnClick = true;
            this.docking.Image = global::ZONEDOCTOR.Properties.Resources.dock;
            this.docking.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.docking.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.docking.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.docking.Name = "docking";
            this.docking.Size = new System.Drawing.Size(23, 22);
            this.docking.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.docking.ToolTipText = "Dock Editors";
            this.docking.Click += new System.EventHandler(this.docking_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // loadAllData
            // 
            this.loadAllData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadAllData.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.loadAllData.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.loadAllData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadAllData.Name = "loadAllData";
            this.loadAllData.Size = new System.Drawing.Size(23, 22);
            this.loadAllData.Text = "Reset Editor Memory";
            this.loadAllData.Click += new System.EventHandler(this.loadAllData_Click);
            // 
            // clearModel
            // 
            this.clearModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearModel.Image = global::ZONEDOCTOR.Properties.Resources.clearModel;
            this.clearModel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearModel.Name = "clearModel";
            this.clearModel.Size = new System.Drawing.Size(23, 22);
            this.clearModel.Text = "Clear Editor Memory";
            this.clearModel.Click += new System.EventHandler(this.clearModel_Click);
            // 
            // Editor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 509);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.toolStrip4);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::ZONEDOCTOR.Properties.Resources.ZONEDOCTOR_icon;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Zone Doctor CE - Final Fantasy 6 Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox romInfo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton loadRom;
        private System.Windows.Forms.ToolStripTextBox loadRomTextBox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton openLocations;
        private System.Windows.Forms.ToolStripButton openEventScripts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton restoreElementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton info;
        private System.Windows.Forms.ToolStripButton openSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton showROMInfo;
        private System.Windows.Forms.ToolStripButton docking;
        private System.Windows.Forms.ToolStripDropDownButton recentFiles;
        private System.Windows.Forms.ToolStripButton openAll;
        private System.Windows.Forms.ToolStripButton closeAll;
        private System.Windows.Forms.ToolStripButton minimizeAll;
        private System.Windows.Forms.ToolStripButton restoreAll;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton loadAllData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton clearModel;
        private System.Windows.Forms.ToolStripButton refreshROM;
        private System.Windows.Forms.ToolStripButton closeROM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton openProject;
        private System.Windows.Forms.ToolStripButton hexViewer;
        private System.Windows.Forms.ToolStripButton history;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}

