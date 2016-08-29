
namespace ZONEDOCTOR
{
    public partial class Previewer
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
            this.emuPathLabel = new System.Windows.Forms.Label();
            this.changeEmuButton = new System.Windows.Forms.Button();
            this.eventListBox = new System.Windows.Forms.ListBox();
            this.launchButton = new System.Windows.Forms.Button();
            this.romLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.selectIndex = new System.Windows.Forms.NumericUpDown();
            this.cancelButton = new System.Windows.Forms.Button();
            this.emuPathTextBox = new System.Windows.Forms.TextBox();
            this.romPathTextBox = new System.Windows.Forms.TextBox();
            this.zsnesArgs = new System.Windows.Forms.TextBox();
            this.linkLabelZSNES = new System.Windows.Forms.LinkLabel();
            this.adjustX = new System.Windows.Forms.NumericUpDown();
            this.adjustY = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.dynamicROMPath = new System.Windows.Forms.CheckBox();
            this.defaultZSNES = new System.Windows.Forms.Button();
            this.snes9xArgs = new System.Windows.Forms.TextBox();
            this.defaultSNES9X = new System.Windows.Forms.Button();
            this.linkLabelSNES9X = new System.Windows.Forms.LinkLabel();
            this.partySlot3 = new System.Windows.Forms.ComboBox();
            this.partySlot2 = new System.Windows.Forms.ComboBox();
            this.partySlot1 = new System.Windows.Forms.ComboBox();
            this.partySlot0 = new System.Windows.Forms.ComboBox();
            this.maxOutStats = new System.Windows.Forms.CheckBox();
            this.equipSprintShoes = new System.Windows.Forms.CheckBox();
            this.equipMoogleCharm = new System.Windows.Forms.CheckBox();
            this.walkThroughWalls = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.selectIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustY)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // emuPathLabel
            // 
            this.emuPathLabel.AutoSize = true;
            this.emuPathLabel.BackColor = System.Drawing.SystemColors.Control;
            this.emuPathLabel.Location = new System.Drawing.Point(6, 21);
            this.emuPathLabel.Name = "emuPathLabel";
            this.emuPathLabel.Size = new System.Drawing.Size(78, 13);
            this.emuPathLabel.TabIndex = 0;
            this.emuPathLabel.Text = "Emulator Path:";
            // 
            // changeEmuButton
            // 
            this.changeEmuButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeEmuButton.BackColor = System.Drawing.SystemColors.Control;
            this.changeEmuButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeEmuButton.Location = new System.Drawing.Point(524, 18);
            this.changeEmuButton.Name = "changeEmuButton";
            this.changeEmuButton.Size = new System.Drawing.Size(62, 21);
            this.changeEmuButton.TabIndex = 1;
            this.changeEmuButton.Text = "...";
            this.changeEmuButton.UseCompatibleTextRendering = true;
            this.changeEmuButton.UseVisualStyleBackColor = false;
            this.changeEmuButton.Click += new System.EventHandler(this.changeEmuButton_Click);
            // 
            // eventListBox
            // 
            this.eventListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventListBox.FormattingEnabled = true;
            this.eventListBox.IntegralHeight = false;
            this.eventListBox.Location = new System.Drawing.Point(3, 17);
            this.eventListBox.Name = "eventListBox";
            this.eventListBox.Size = new System.Drawing.Size(412, 307);
            this.eventListBox.TabIndex = 2;
            this.eventListBox.SelectedIndexChanged += new System.EventHandler(this.eventListBox_SelectedIndexChanged);
            // 
            // launchButton
            // 
            this.launchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.launchButton.FlatAppearance.BorderSize = 0;
            this.launchButton.Location = new System.Drawing.Point(6, 301);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(80, 23);
            this.launchButton.TabIndex = 3;
            this.launchButton.Text = "Launch";
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // romLabel
            // 
            this.romLabel.AutoSize = true;
            this.romLabel.BackColor = System.Drawing.SystemColors.Control;
            this.romLabel.Location = new System.Drawing.Point(6, 42);
            this.romLabel.Name = "romLabel";
            this.romLabel.Size = new System.Drawing.Size(57, 13);
            this.romLabel.TabIndex = 4;
            this.romLabel.Text = "Rom Path:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Index";
            // 
            // selectIndex
            // 
            this.selectIndex.Location = new System.Drawing.Point(103, 15);
            this.selectIndex.Maximum = new decimal(new int[] {
            13428223,
            0,
            0,
            0});
            this.selectIndex.Name = "selectIndex";
            this.selectIndex.Size = new System.Drawing.Size(65, 21);
            this.selectIndex.TabIndex = 5;
            this.selectIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.selectIndex.ValueChanged += new System.EventHandler(this.selectNumericUpDown_ValueChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(88, 301);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // emuPathTextBox
            // 
            this.emuPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.emuPathTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.emuPathTextBox.Location = new System.Drawing.Point(90, 18);
            this.emuPathTextBox.Name = "emuPathTextBox";
            this.emuPathTextBox.ReadOnly = true;
            this.emuPathTextBox.Size = new System.Drawing.Size(432, 21);
            this.emuPathTextBox.TabIndex = 8;
            // 
            // romPathTextBox
            // 
            this.romPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.romPathTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.romPathTextBox.Location = new System.Drawing.Point(90, 39);
            this.romPathTextBox.Name = "romPathTextBox";
            this.romPathTextBox.ReadOnly = true;
            this.romPathTextBox.Size = new System.Drawing.Size(432, 21);
            this.romPathTextBox.TabIndex = 9;
            // 
            // zsnesArgs
            // 
            this.zsnesArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zsnesArgs.Location = new System.Drawing.Point(90, 81);
            this.zsnesArgs.Name = "zsnesArgs";
            this.zsnesArgs.Size = new System.Drawing.Size(432, 21);
            this.zsnesArgs.TabIndex = 17;
            // 
            // linkLabelZSNES
            // 
            this.linkLabelZSNES.AutoSize = true;
            this.linkLabelZSNES.BackColor = System.Drawing.SystemColors.Control;
            this.linkLabelZSNES.Location = new System.Drawing.Point(6, 84);
            this.linkLabelZSNES.Name = "linkLabelZSNES";
            this.linkLabelZSNES.Size = new System.Drawing.Size(77, 13);
            this.linkLabelZSNES.TabIndex = 19;
            this.linkLabelZSNES.TabStop = true;
            this.linkLabelZSNES.Tag = "";
            this.linkLabelZSNES.Text = "ZSNESW Args:";
            this.linkLabelZSNES.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelZSNES_LinkClicked);
            // 
            // adjustX
            // 
            this.adjustX.Location = new System.Drawing.Point(38, 15);
            this.adjustX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.adjustX.Name = "adjustX";
            this.adjustX.Size = new System.Drawing.Size(65, 21);
            this.adjustX.TabIndex = 21;
            this.adjustX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // adjustY
            // 
            this.adjustY.Location = new System.Drawing.Point(103, 15);
            this.adjustY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.adjustY.Name = "adjustY";
            this.adjustY.Size = new System.Drawing.Size(65, 21);
            this.adjustY.TabIndex = 22;
            this.adjustY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label5.Size = new System.Drawing.Size(25, 16);
            this.label5.TabIndex = 24;
            this.label5.Text = "X,Y";
            // 
            // dynamicROMPath
            // 
            this.dynamicROMPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dynamicROMPath.Appearance = System.Windows.Forms.Appearance.Button;
            this.dynamicROMPath.BackColor = System.Drawing.SystemColors.Control;
            this.dynamicROMPath.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicROMPath.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.dynamicROMPath.Location = new System.Drawing.Point(524, 39);
            this.dynamicROMPath.Name = "dynamicROMPath";
            this.dynamicROMPath.Size = new System.Drawing.Size(62, 21);
            this.dynamicROMPath.TabIndex = 27;
            this.dynamicROMPath.Text = "DYNAMIC";
            this.dynamicROMPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dynamicROMPath.UseCompatibleTextRendering = true;
            this.dynamicROMPath.UseVisualStyleBackColor = false;
            this.dynamicROMPath.CheckedChanged += new System.EventHandler(this.dynamicROMPath_CheckedChanged);
            // 
            // defaultZSNES
            // 
            this.defaultZSNES.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultZSNES.BackColor = System.Drawing.SystemColors.Control;
            this.defaultZSNES.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultZSNES.Location = new System.Drawing.Point(524, 81);
            this.defaultZSNES.Name = "defaultZSNES";
            this.defaultZSNES.Size = new System.Drawing.Size(62, 21);
            this.defaultZSNES.TabIndex = 28;
            this.defaultZSNES.Text = "DEFAULT";
            this.defaultZSNES.UseCompatibleTextRendering = true;
            this.defaultZSNES.UseVisualStyleBackColor = false;
            this.defaultZSNES.Click += new System.EventHandler(this.defaultZSNES_Click);
            // 
            // snes9xArgs
            // 
            this.snes9xArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.snes9xArgs.Location = new System.Drawing.Point(90, 60);
            this.snes9xArgs.Name = "snes9xArgs";
            this.snes9xArgs.Size = new System.Drawing.Size(432, 21);
            this.snes9xArgs.TabIndex = 17;
            // 
            // defaultSNES9X
            // 
            this.defaultSNES9X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultSNES9X.BackColor = System.Drawing.SystemColors.Control;
            this.defaultSNES9X.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultSNES9X.Location = new System.Drawing.Point(524, 60);
            this.defaultSNES9X.Name = "defaultSNES9X";
            this.defaultSNES9X.Size = new System.Drawing.Size(62, 21);
            this.defaultSNES9X.TabIndex = 28;
            this.defaultSNES9X.Text = "DEFAULT";
            this.defaultSNES9X.UseCompatibleTextRendering = true;
            this.defaultSNES9X.UseVisualStyleBackColor = false;
            this.defaultSNES9X.Click += new System.EventHandler(this.defaultSNES9X_Click);
            // 
            // linkLabelSNES9X
            // 
            this.linkLabelSNES9X.AutoSize = true;
            this.linkLabelSNES9X.BackColor = System.Drawing.SystemColors.Control;
            this.linkLabelSNES9X.Location = new System.Drawing.Point(6, 63);
            this.linkLabelSNES9X.Name = "linkLabelSNES9X";
            this.linkLabelSNES9X.Size = new System.Drawing.Size(73, 13);
            this.linkLabelSNES9X.TabIndex = 19;
            this.linkLabelSNES9X.TabStop = true;
            this.linkLabelSNES9X.Tag = "";
            this.linkLabelSNES9X.Text = "SNES9X Args:";
            this.linkLabelSNES9X.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSNES9X_LinkClicked);
            // 
            // partySlot3
            // 
            this.partySlot3.DropDownHeight = 392;
            this.partySlot3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partySlot3.DropDownWidth = 220;
            this.partySlot3.FormattingEnabled = true;
            this.partySlot3.IntegralHeight = false;
            this.partySlot3.Location = new System.Drawing.Point(6, 83);
            this.partySlot3.Name = "partySlot3";
            this.partySlot3.Size = new System.Drawing.Size(162, 21);
            this.partySlot3.TabIndex = 21;
            this.partySlot3.SelectedIndexChanged += new System.EventHandler(this.partySlot_SelectedIndexChanged);
            // 
            // partySlot2
            // 
            this.partySlot2.DropDownHeight = 392;
            this.partySlot2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partySlot2.DropDownWidth = 220;
            this.partySlot2.FormattingEnabled = true;
            this.partySlot2.IntegralHeight = false;
            this.partySlot2.Location = new System.Drawing.Point(6, 62);
            this.partySlot2.Name = "partySlot2";
            this.partySlot2.Size = new System.Drawing.Size(162, 21);
            this.partySlot2.TabIndex = 21;
            this.partySlot2.SelectedIndexChanged += new System.EventHandler(this.partySlot_SelectedIndexChanged);
            // 
            // partySlot1
            // 
            this.partySlot1.DropDownHeight = 392;
            this.partySlot1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partySlot1.DropDownWidth = 220;
            this.partySlot1.FormattingEnabled = true;
            this.partySlot1.IntegralHeight = false;
            this.partySlot1.Location = new System.Drawing.Point(6, 41);
            this.partySlot1.Name = "partySlot1";
            this.partySlot1.Size = new System.Drawing.Size(162, 21);
            this.partySlot1.TabIndex = 21;
            this.partySlot1.SelectedIndexChanged += new System.EventHandler(this.partySlot_SelectedIndexChanged);
            // 
            // partySlot0
            // 
            this.partySlot0.DropDownHeight = 392;
            this.partySlot0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partySlot0.DropDownWidth = 220;
            this.partySlot0.FormattingEnabled = true;
            this.partySlot0.IntegralHeight = false;
            this.partySlot0.Location = new System.Drawing.Point(6, 20);
            this.partySlot0.Name = "partySlot0";
            this.partySlot0.Size = new System.Drawing.Size(162, 21);
            this.partySlot0.TabIndex = 21;
            this.partySlot0.SelectedIndexChanged += new System.EventHandler(this.partySlot_SelectedIndexChanged);
            // 
            // maxOutStats
            // 
            this.maxOutStats.Appearance = System.Windows.Forms.Appearance.Button;
            this.maxOutStats.BackColor = System.Drawing.SystemColors.Control;
            this.maxOutStats.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxOutStats.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.maxOutStats.Location = new System.Drawing.Point(6, 110);
            this.maxOutStats.Name = "maxOutStats";
            this.maxOutStats.Size = new System.Drawing.Size(162, 23);
            this.maxOutStats.TabIndex = 28;
            this.maxOutStats.Text = "MAX STATS";
            this.maxOutStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.maxOutStats.UseCompatibleTextRendering = true;
            this.maxOutStats.UseVisualStyleBackColor = false;
            this.maxOutStats.CheckedChanged += new System.EventHandler(this.maxOutStats_CheckedChanged);
            // 
            // equipSprintShoes
            // 
            this.equipSprintShoes.AutoSize = true;
            this.equipSprintShoes.Checked = true;
            this.equipSprintShoes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.equipSprintShoes.Location = new System.Drawing.Point(7, 12);
            this.equipSprintShoes.Name = "equipSprintShoes";
            this.equipSprintShoes.Size = new System.Drawing.Size(116, 17);
            this.equipSprintShoes.TabIndex = 39;
            this.equipSprintShoes.Text = "Equip Sprint Shoes";
            this.equipSprintShoes.UseVisualStyleBackColor = true;
            this.equipSprintShoes.CheckedChanged += new System.EventHandler(this.equipment_CheckedChanged);
            // 
            // equipMoogleCharm
            // 
            this.equipMoogleCharm.AutoSize = true;
            this.equipMoogleCharm.Checked = true;
            this.equipMoogleCharm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.equipMoogleCharm.Location = new System.Drawing.Point(7, 29);
            this.equipMoogleCharm.Name = "equipMoogleCharm";
            this.equipMoogleCharm.Size = new System.Drawing.Size(124, 17);
            this.equipMoogleCharm.TabIndex = 39;
            this.equipMoogleCharm.Text = "Equip Moogle Charm";
            this.equipMoogleCharm.UseVisualStyleBackColor = true;
            this.equipMoogleCharm.CheckedChanged += new System.EventHandler(this.equipment_CheckedChanged);
            // 
            // walkThroughWalls
            // 
            this.walkThroughWalls.AutoSize = true;
            this.walkThroughWalls.Location = new System.Drawing.Point(7, 46);
            this.walkThroughWalls.Name = "walkThroughWalls";
            this.walkThroughWalls.Size = new System.Drawing.Size(116, 17);
            this.walkThroughWalls.TabIndex = 39;
            this.walkThroughWalls.Text = "Walk through walls";
            this.walkThroughWalls.UseVisualStyleBackColor = true;
            this.walkThroughWalls.CheckedChanged += new System.EventHandler(this.equipment_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.selectIndex);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 43);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.maxOutStats);
            this.groupBox3.Controls.Add(this.partySlot3);
            this.groupBox3.Controls.Add(this.partySlot2);
            this.groupBox3.Controls.Add(this.partySlot1);
            this.groupBox3.Controls.Add(this.partySlot0);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 85);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(174, 141);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Allies in Party";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.eventListBox);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 108);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(418, 327);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Source of Entrance";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.changeEmuButton);
            this.groupBox5.Controls.Add(this.defaultSNES9X);
            this.groupBox5.Controls.Add(this.defaultZSNES);
            this.groupBox5.Controls.Add(this.dynamicROMPath);
            this.groupBox5.Controls.Add(this.zsnesArgs);
            this.groupBox5.Controls.Add(this.snes9xArgs);
            this.groupBox5.Controls.Add(this.romPathTextBox);
            this.groupBox5.Controls.Add(this.emuPathTextBox);
            this.groupBox5.Controls.Add(this.emuPathLabel);
            this.groupBox5.Controls.Add(this.linkLabelZSNES);
            this.groupBox5.Controls.Add(this.romLabel);
            this.groupBox5.Controls.Add(this.linkLabelSNES9X);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(592, 108);
            this.groupBox5.TabIndex = 40;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Emulator Properties";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.adjustX);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.adjustY);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(0, 43);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(174, 42);
            this.groupBox6.TabIndex = 40;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Character Coordinates";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.launchButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(418, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 327);
            this.panel1.TabIndex = 41;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.equipSprintShoes);
            this.groupBox7.Controls.Add(this.walkThroughWalls);
            this.groupBox7.Controls.Add(this.equipMoogleCharm);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(0, 226);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(174, 68);
            this.groupBox7.TabIndex = 41;
            this.groupBox7.TabStop = false;
            // 
            // Previewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 435);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox5);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::ZONEDOCTOR.Properties.Resources.ZONEDOCTOR_icon;
            this.Name = "Previewer";
            ((System.ComponentModel.ISupportInitialize)(this.selectIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustY)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Label emuPathLabel;
        private System.Windows.Forms.Button changeEmuButton;
        private System.Windows.Forms.ListBox eventListBox;
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.Label romLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown selectIndex;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox emuPathTextBox;
        private System.Windows.Forms.TextBox romPathTextBox;
        private System.Windows.Forms.TextBox zsnesArgs;
        private System.Windows.Forms.LinkLabel linkLabelZSNES;
        private System.Windows.Forms.NumericUpDown adjustX;
        private System.Windows.Forms.NumericUpDown adjustY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox dynamicROMPath;
        private System.Windows.Forms.Button defaultZSNES;
        private System.Windows.Forms.TextBox snes9xArgs;
        private System.Windows.Forms.Button defaultSNES9X;
        private System.Windows.Forms.LinkLabel linkLabelSNES9X;
        private System.Windows.Forms.CheckBox maxOutStats;
        private System.Windows.Forms.ComboBox partySlot3;
        private System.Windows.Forms.ComboBox partySlot2;
        private System.Windows.Forms.ComboBox partySlot1;
        private System.Windows.Forms.ComboBox partySlot0;
        private System.Windows.Forms.CheckBox equipSprintShoes;
        private System.Windows.Forms.CheckBox equipMoogleCharm;
        private System.Windows.Forms.CheckBox walkThroughWalls;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox7;
    }
}