
namespace ZONEDOCTOR
{
    partial class TileEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileEditor));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonInvertTile = new System.Windows.Forms.Button();
            this.buttonMirrorTile = new System.Windows.Forms.Button();
            this.panel111 = new System.Windows.Forms.Panel();
            this.pictureBoxTile = new System.Windows.Forms.PictureBox();
            this.pictureBoxSubtile = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.physicalSolidTile = new System.Windows.Forms.CheckBox();
            this.physicalStairs = new System.Windows.Forms.ComboBox();
            this.label114 = new System.Windows.Forms.Label();
            this.physicalTileProperties = new System.Windows.Forms.CheckedListBox();
            this.physicalOtherBits = new System.Windows.Forms.CheckedListBox();
            this.physicalBattleBG = new System.Windows.Forms.ComboBox();
            this.physicalAirship = new System.Windows.Forms.ComboBox();
            this.label112 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.physicalProperties = new System.Windows.Forms.CheckedListBox();
            this.physicalUnknown = new System.Windows.Forms.CheckedListBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.autoUpdate = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label141 = new System.Windows.Forms.Label();
            this.label142 = new System.Windows.Forms.Label();
            this.subtileStatus = new System.Windows.Forms.CheckedListBox();
            this.subtileIndex = new System.Windows.Forms.NumericUpDown();
            this.subtilePalette = new System.Windows.Forms.NumericUpDown();
            this.groupBoxSolid = new System.Windows.Forms.GroupBox();
            this.groupBoxSolidWM = new System.Windows.Forms.GroupBox();
            this.panel111.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSubtile)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subtileIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtilePalette)).BeginInit();
            this.groupBoxSolid.SuspendLayout();
            this.groupBoxSolidWM.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.Location = new System.Drawing.Point(12, 259);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.Location = new System.Drawing.Point(93, 259);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.FlatAppearance.BorderSize = 0;
            this.buttonReset.Location = new System.Drawing.Point(174, 259);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "Reset";
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonInvertTile
            // 
            this.buttonInvertTile.FlatAppearance.BorderSize = 2;
            this.buttonInvertTile.Location = new System.Drawing.Point(174, 40);
            this.buttonInvertTile.Name = "buttonInvertTile";
            this.buttonInvertTile.Size = new System.Drawing.Size(75, 23);
            this.buttonInvertTile.TabIndex = 500;
            this.buttonInvertTile.Text = "Invert";
            this.buttonInvertTile.Click += new System.EventHandler(this.buttonInvertTile_Click);
            // 
            // buttonMirrorTile
            // 
            this.buttonMirrorTile.FlatAppearance.BorderSize = 2;
            this.buttonMirrorTile.Location = new System.Drawing.Point(174, 12);
            this.buttonMirrorTile.Name = "buttonMirrorTile";
            this.buttonMirrorTile.Size = new System.Drawing.Size(75, 23);
            this.buttonMirrorTile.TabIndex = 499;
            this.buttonMirrorTile.Text = "Mirror";
            this.buttonMirrorTile.Click += new System.EventHandler(this.buttonMirrorTile_Click);
            // 
            // panel111
            // 
            this.panel111.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel111.Controls.Add(this.pictureBoxTile);
            this.panel111.Location = new System.Drawing.Point(12, 12);
            this.panel111.Name = "panel111";
            this.panel111.Size = new System.Drawing.Size(68, 68);
            this.panel111.TabIndex = 497;
            // 
            // pictureBoxTile
            // 
            this.pictureBoxTile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxTile.BackgroundImage")));
            this.pictureBoxTile.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTile.Name = "pictureBoxTile";
            this.pictureBoxTile.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxTile.TabIndex = 446;
            this.pictureBoxTile.TabStop = false;
            this.pictureBoxTile.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTile_Paint);
            this.pictureBoxTile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTile_MouseClick);
            // 
            // pictureBoxSubtile
            // 
            this.pictureBoxSubtile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxSubtile.BackgroundImage")));
            this.pictureBoxSubtile.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSubtile.Name = "pictureBoxSubtile";
            this.pictureBoxSubtile.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxSubtile.TabIndex = 446;
            this.pictureBoxSubtile.TabStop = false;
            this.pictureBoxSubtile.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxSubtile_Paint);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBoxSubtile);
            this.panel1.Location = new System.Drawing.Point(86, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(68, 68);
            this.panel1.TabIndex = 498;
            // 
            // physicalSolidTile
            // 
            this.physicalSolidTile.Appearance = System.Windows.Forms.Appearance.Button;
            this.physicalSolidTile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.physicalSolidTile.FlatAppearance.BorderSize = 0;
            this.physicalSolidTile.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.physicalSolidTile.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.physicalSolidTile.ForeColor = System.Drawing.Color.Gray;
            this.physicalSolidTile.Location = new System.Drawing.Point(6, 20);
            this.physicalSolidTile.Name = "physicalSolidTile";
            this.physicalSolidTile.Size = new System.Drawing.Size(248, 23);
            this.physicalSolidTile.TabIndex = 510;
            this.physicalSolidTile.Text = "SOLID TILE, CANNOT BE WALKED ON";
            this.physicalSolidTile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.physicalSolidTile.UseCompatibleTextRendering = true;
            this.physicalSolidTile.UseVisualStyleBackColor = false;
            this.physicalSolidTile.CheckedChanged += new System.EventHandler(this.physicalSolidTile_CheckedChanged);
            // 
            // physicalStairs
            // 
            this.physicalStairs.BackColor = System.Drawing.SystemColors.Window;
            this.physicalStairs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.physicalStairs.Items.AddRange(new object[] {
            "(none)",
            "up-right",
            "up-left"});
            this.physicalStairs.Location = new System.Drawing.Point(60, 47);
            this.physicalStairs.Name = "physicalStairs";
            this.physicalStairs.Size = new System.Drawing.Size(194, 21);
            this.physicalStairs.TabIndex = 0;
            this.physicalStairs.SelectedIndexChanged += new System.EventHandler(this.physicalStairs_SelectedIndexChanged);
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(6, 50);
            this.label114.Name = "label114";
            this.label114.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label114.Size = new System.Drawing.Size(36, 16);
            this.label114.TabIndex = 512;
            this.label114.Text = "Stairs";
            // 
            // physicalTileProperties
            // 
            this.physicalTileProperties.CheckOnClick = true;
            this.physicalTileProperties.ColumnWidth = 60;
            this.physicalTileProperties.Items.AddRange(new object[] {
            "Passable from west",
            "Passable from east",
            "Passable from north",
            "Passable from south",
            "Character always faces up"});
            this.physicalTileProperties.Location = new System.Drawing.Point(6, 74);
            this.physicalTileProperties.Name = "physicalTileProperties";
            this.physicalTileProperties.Size = new System.Drawing.Size(248, 84);
            this.physicalTileProperties.TabIndex = 511;
            this.physicalTileProperties.SelectedIndexChanged += new System.EventHandler(this.physicalTileProperties_SelectedIndexChanged);
            // 
            // physicalOtherBits
            // 
            this.physicalOtherBits.CheckOnClick = true;
            this.physicalOtherBits.ColumnWidth = 120;
            this.physicalOtherBits.Items.AddRange(new object[] {
            "Solid to tier 1",
            "Solid to tier 2",
            "0.3",
            "0.4",
            "Door",
            "1.4",
            "1.5",
            "Passable quadrants"});
            this.physicalOtherBits.Location = new System.Drawing.Point(6, 164);
            this.physicalOtherBits.MultiColumn = true;
            this.physicalOtherBits.Name = "physicalOtherBits";
            this.physicalOtherBits.Size = new System.Drawing.Size(248, 100);
            this.physicalOtherBits.TabIndex = 515;
            this.physicalOtherBits.SelectedIndexChanged += new System.EventHandler(this.physicalOtherBits_SelectedIndexChanged);
            // 
            // physicalBattleBG
            // 
            this.physicalBattleBG.BackColor = System.Drawing.SystemColors.Window;
            this.physicalBattleBG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.physicalBattleBG.Items.AddRange(new object[] {
            "Grasslands (WoB)",
            "Forest (WoR)",
            "Desert (WoB)",
            "Forest (WoB)",
            "Land (WoR) A",
            "Land (WoR) B",
            "Veldt",
            "Clouds, falling"});
            this.physicalBattleBG.Location = new System.Drawing.Point(120, 41);
            this.physicalBattleBG.Name = "physicalBattleBG";
            this.physicalBattleBG.Size = new System.Drawing.Size(134, 21);
            this.physicalBattleBG.TabIndex = 0;
            this.physicalBattleBG.SelectedIndexChanged += new System.EventHandler(this.physicalBattleBG_SelectedIndexChanged);
            // 
            // physicalAirship
            // 
            this.physicalAirship.BackColor = System.Drawing.SystemColors.Window;
            this.physicalAirship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.physicalAirship.Items.AddRange(new object[] {
            "smallest",
            "small",
            "large",
            "largest"});
            this.physicalAirship.Location = new System.Drawing.Point(120, 20);
            this.physicalAirship.Name = "physicalAirship";
            this.physicalAirship.Size = new System.Drawing.Size(134, 21);
            this.physicalAirship.TabIndex = 0;
            this.physicalAirship.SelectedIndexChanged += new System.EventHandler(this.physicalAirship_SelectedIndexChanged);
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(6, 44);
            this.label112.Name = "label112";
            this.label112.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label112.Size = new System.Drawing.Size(53, 16);
            this.label112.TabIndex = 507;
            this.label112.Text = "Battle BG";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(6, 23);
            this.label111.Name = "label111";
            this.label111.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label111.Size = new System.Drawing.Size(102, 16);
            this.label111.TabIndex = 507;
            this.label111.Text = "Airship shadow size";
            // 
            // physicalProperties
            // 
            this.physicalProperties.CheckOnClick = true;
            this.physicalProperties.ColumnWidth = 60;
            this.physicalProperties.Items.AddRange(new object[] {
            "Chocobo cannot travel on tile",
            "Airship cannot land on tile",
            "Solid tile, cannot be walked on",
            "Bottom half of sprite is transparent",
            "Random battles enabled"});
            this.physicalProperties.Location = new System.Drawing.Point(6, 68);
            this.physicalProperties.Name = "physicalProperties";
            this.physicalProperties.Size = new System.Drawing.Size(248, 84);
            this.physicalProperties.TabIndex = 504;
            this.physicalProperties.SelectedIndexChanged += new System.EventHandler(this.physicalProperties_SelectedIndexChanged);
            // 
            // physicalUnknown
            // 
            this.physicalUnknown.CheckOnClick = true;
            this.physicalUnknown.ColumnWidth = 120;
            this.physicalUnknown.Items.AddRange(new object[] {
            "0.7",
            "1.3",
            "1.4",
            "Veldt",
            "Phoenix Cave",
            "Kefka\'s Tower"});
            this.physicalUnknown.Location = new System.Drawing.Point(6, 158);
            this.physicalUnknown.MultiColumn = true;
            this.physicalUnknown.Name = "physicalUnknown";
            this.physicalUnknown.Size = new System.Drawing.Size(248, 100);
            this.physicalUnknown.TabIndex = 504;
            this.physicalUnknown.SelectedIndexChanged += new System.EventHandler(this.physicalUnknown_SelectedIndexChanged);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(12, 230);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 535;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // autoUpdate
            // 
            this.autoUpdate.AutoSize = true;
            this.autoUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.autoUpdate.Location = new System.Drawing.Point(93, 236);
            this.autoUpdate.Name = "autoUpdate";
            this.autoUpdate.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.autoUpdate.Size = new System.Drawing.Size(91, 17);
            this.autoUpdate.TabIndex = 534;
            this.autoUpdate.Text = "Auto-update";
            this.autoUpdate.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label141);
            this.groupBox1.Controls.Add(this.label142);
            this.groupBox1.Controls.Add(this.subtileStatus);
            this.groupBox1.Controls.Add(this.subtileIndex);
            this.groupBox1.Controls.Add(this.subtilePalette);
            this.groupBox1.Location = new System.Drawing.Point(12, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 125);
            this.groupBox1.TabIndex = 536;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Subtile Properties";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(6, 22);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(35, 13);
            this.label141.TabIndex = 0;
            this.label141.Text = "Index";
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Location = new System.Drawing.Point(6, 43);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(41, 13);
            this.label142.TabIndex = 2;
            this.label142.Text = "Palette";
            // 
            // subtileStatus
            // 
            this.subtileStatus.BackColor = System.Drawing.SystemColors.Window;
            this.subtileStatus.CheckOnClick = true;
            this.subtileStatus.ColumnWidth = 60;
            this.subtileStatus.Items.AddRange(new object[] {
            "Priority 1",
            "Mirror",
            "Invert"});
            this.subtileStatus.Location = new System.Drawing.Point(6, 68);
            this.subtileStatus.Name = "subtileStatus";
            this.subtileStatus.Size = new System.Drawing.Size(130, 52);
            this.subtileStatus.TabIndex = 4;
            this.subtileStatus.SelectedIndexChanged += new System.EventHandler(this.tileAttributes_SelectedIndexChanged);
            // 
            // subtileIndex
            // 
            this.subtileIndex.Location = new System.Drawing.Point(74, 20);
            this.subtileIndex.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.subtileIndex.Name = "subtileIndex";
            this.subtileIndex.Size = new System.Drawing.Size(62, 21);
            this.subtileIndex.TabIndex = 1;
            this.subtileIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.subtileIndex.ValueChanged += new System.EventHandler(this.tile8x8Tile_ValueChanged);
            // 
            // subtilePalette
            // 
            this.subtilePalette.Location = new System.Drawing.Point(74, 41);
            this.subtilePalette.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.subtilePalette.Name = "subtilePalette";
            this.subtilePalette.Size = new System.Drawing.Size(62, 21);
            this.subtilePalette.TabIndex = 3;
            this.subtilePalette.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.subtilePalette.ValueChanged += new System.EventHandler(this.subtilePalette_ValueChanged);
            // 
            // groupBoxSolid
            // 
            this.groupBoxSolid.Controls.Add(this.physicalTileProperties);
            this.groupBoxSolid.Controls.Add(this.physicalStairs);
            this.groupBoxSolid.Controls.Add(this.physicalSolidTile);
            this.groupBoxSolid.Controls.Add(this.label114);
            this.groupBoxSolid.Controls.Add(this.physicalOtherBits);
            this.groupBoxSolid.Location = new System.Drawing.Point(255, 12);
            this.groupBoxSolid.Name = "groupBoxSolid";
            this.groupBoxSolid.Size = new System.Drawing.Size(260, 270);
            this.groupBoxSolid.TabIndex = 537;
            this.groupBoxSolid.TabStop = false;
            this.groupBoxSolid.Text = "Solid Tile Properties";
            // 
            // groupBoxSolidWM
            // 
            this.groupBoxSolidWM.Controls.Add(this.physicalBattleBG);
            this.groupBoxSolidWM.Controls.Add(this.physicalAirship);
            this.groupBoxSolidWM.Controls.Add(this.label111);
            this.groupBoxSolidWM.Controls.Add(this.physicalProperties);
            this.groupBoxSolidWM.Controls.Add(this.label112);
            this.groupBoxSolidWM.Controls.Add(this.physicalUnknown);
            this.groupBoxSolidWM.Location = new System.Drawing.Point(255, 12);
            this.groupBoxSolidWM.Name = "groupBoxSolidWM";
            this.groupBoxSolidWM.Size = new System.Drawing.Size(260, 270);
            this.groupBoxSolidWM.TabIndex = 538;
            this.groupBoxSolidWM.TabStop = false;
            this.groupBoxSolidWM.Text = "Solid Tile Properties";
            // 
            // TileEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 294);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.autoUpdate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonInvertTile);
            this.Controls.Add(this.buttonMirrorTile);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.panel111);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxSolid);
            this.Controls.Add(this.groupBoxSolidWM);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = global::ZONEDOCTOR.Properties.Resources.ZONEDOCTOR_icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TileEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TILE EDITOR";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TileEditor_FormClosing);
            this.panel111.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSubtile)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subtileIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtilePalette)).EndInit();
            this.groupBoxSolid.ResumeLayout(false);
            this.groupBoxSolid.PerformLayout();
            this.groupBoxSolidWM.ResumeLayout(false);
            this.groupBoxSolidWM.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel111;
        private System.Windows.Forms.PictureBox pictureBoxSubtile;
        private System.Windows.Forms.PictureBox pictureBoxTile;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonInvertTile;
        private System.Windows.Forms.Button buttonMirrorTile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox physicalSolidTile;
        private System.Windows.Forms.ComboBox physicalStairs;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.CheckedListBox physicalTileProperties;
        private System.Windows.Forms.CheckedListBox physicalOtherBits;
        private System.Windows.Forms.ComboBox physicalBattleBG;
        private System.Windows.Forms.ComboBox physicalAirship;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.CheckedListBox physicalProperties;
        private System.Windows.Forms.CheckedListBox physicalUnknown;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox autoUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.CheckedListBox subtileStatus;
        private System.Windows.Forms.NumericUpDown subtileIndex;
        private System.Windows.Forms.NumericUpDown subtilePalette;
        private System.Windows.Forms.GroupBox groupBoxSolid;
        private System.Windows.Forms.GroupBox groupBoxSolidWM;
    }
}