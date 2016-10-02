using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    partial class Locations
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
            this.analyzerInfo = new ZONEDOCTOR.ToolStripListView();
            this.locationInfo = new ZONEDOCTOR.ToolStripListView();
            this.locationNum = new ZONEDOCTOR.ToolStripNumericUpDown();
            this.locationName = new System.Windows.Forms.ToolStripComboBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.npcWalkability = new System.Windows.Forms.CheckedListBox();
            this.npcSpeed = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.npcFace = new System.Windows.Forms.ComboBox();
            this.buttonGotoA = new System.Windows.Forms.Button();
            this.npcVehicle = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.npcPalette = new System.Windows.Forms.NumericUpDown();
            this.npcEventPointer = new System.Windows.Forms.NumericUpDown();
            this.label65 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.npcSpriteIndex = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.npcY = new System.Windows.Forms.NumericUpDown();
            this.npcX = new System.Windows.Forms.NumericUpDown();
            this.npcSpriteSet = new System.Windows.Forms.NumericUpDown();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.npcUnknownBits = new System.Windows.Forms.CheckedListBox();
            this.npcsBytesLeft = new System.Windows.Forms.Label();
            this.npcListBox = new System.Windows.Forms.ListBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label48 = new System.Windows.Forms.Label();
            this.npcCheckMem = new System.Windows.Forms.NumericUpDown();
            this.label52 = new System.Windows.Forms.Label();
            this.npcCheckBit = new System.Windows.Forms.NumericUpDown();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.npcInsertObject = new System.Windows.Forms.ToolStripButton();
            this.npcRemoveObject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.npcMoveUp = new System.Windows.Forms.ToolStripButton();
            this.npcMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.npcCopy = new System.Windows.Forms.ToolStripButton();
            this.npcPaste = new System.Windows.Forms.ToolStripButton();
            this.npcDuplicate = new System.Windows.Forms.ToolStripButton();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.panel52 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.exitUnknownBits = new System.Windows.Forms.CheckedListBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.exitDirection = new System.Windows.Forms.ComboBox();
            this.label119 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.exitX = new System.Windows.Forms.NumericUpDown();
            this.exitY = new System.Windows.Forms.NumericUpDown();
            this.exitWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.exitDestination = new System.Windows.Forms.ComboBox();
            this.exitDestinationFacing = new System.Windows.Forms.ComboBox();
            this.exitToWorldMap = new System.Windows.Forms.CheckBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.exitDestinationY = new System.Windows.Forms.NumericUpDown();
            this.exitShowMessage = new System.Windows.Forms.CheckBox();
            this.exitDestinationX = new System.Windows.Forms.NumericUpDown();
            this.exitListBox = new System.Windows.Forms.ListBox();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.buttonInsertExit = new System.Windows.Forms.ToolStripButton();
            this.buttonDeleteExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.exitsCopyField = new System.Windows.Forms.ToolStripButton();
            this.exitsPasteField = new System.Windows.Forms.ToolStripButton();
            this.exitsDuplicateField = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.exitsBytesLeft = new System.Windows.Forms.ToolStripLabel();
            this.panel68 = new System.Windows.Forms.Panel();
            this.label61 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.buttonGotoD = new System.Windows.Forms.Button();
            this.eventY = new System.Windows.Forms.NumericUpDown();
            this.eventX = new System.Windows.Forms.NumericUpDown();
            this.label127 = new System.Windows.Forms.Label();
            this.eventEventNum = new System.Windows.Forms.NumericUpDown();
            this.eventListBox = new System.Windows.Forms.ListBox();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.buttonInsertEvent = new System.Windows.Forms.ToolStripButton();
            this.buttonDeleteEvent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.eventsCopyField = new System.Windows.Forms.ToolStripButton();
            this.eventsPasteField = new System.Windows.Forms.ToolStripButton();
            this.eventsDuplicateField = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.eventsBytesLeft = new System.Windows.Forms.ToolStripLabel();
            this.label63 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.tbMapName = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.mapPaletteSetNum = new System.Windows.Forms.NumericUpDown();
            this.label46 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label43 = new System.Windows.Forms.Label();
            this.useWorldMapBG = new System.Windows.Forms.CheckBox();
            this.mapSetL3Priority = new System.Windows.Forms.CheckBox();
            this.label41 = new System.Windows.Forms.Label();
            this.mapPhysicalMapNum = new System.Windows.Forms.NumericUpDown();
            this.label45 = new System.Windows.Forms.Label();
            this.mapTilemapL1Num = new System.Windows.Forms.NumericUpDown();
            this.label42 = new System.Windows.Forms.Label();
            this.mapTilemapL2Num = new System.Windows.Forms.NumericUpDown();
            this.mapTilemapL3Num = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.mapGFXSetL3Num = new System.Windows.Forms.NumericUpDown();
            this.mapGFXSet4Num = new System.Windows.Forms.NumericUpDown();
            this.animationL2 = new System.Windows.Forms.NumericUpDown();
            this.mapGFXSet3Num = new System.Windows.Forms.NumericUpDown();
            this.mapGFXSet2Num = new System.Windows.Forms.NumericUpDown();
            this.animationBG = new System.Windows.Forms.NumericUpDown();
            this.animationL3 = new System.Windows.Forms.NumericUpDown();
            this.mapGFXSet1Num = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.mapTilesetL2Num = new System.Windows.Forms.NumericUpDown();
            this.mapTilesetL1Num = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.layerUnknownBits = new System.Windows.Forms.CheckedListBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.mapBattleBGName = new System.Windows.Forms.ComboBox();
            this.label71 = new System.Windows.Forms.Label();
            this.mapRandomBattles = new System.Windows.Forms.CheckBox();
            this.mapBattleBG = new System.Windows.Forms.NumericUpDown();
            this.label38 = new System.Windows.Forms.Label();
            this.mapBattleZone = new System.Windows.Forms.NumericUpDown();
            this.messageName = new System.Windows.Forms.ComboBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.layerEffects = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.windowMask = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.layerColorMathMode = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.layerColorMathIntensity = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.layerPrioritySet = new System.Windows.Forms.NumericUpDown();
            this.layerColorMathBG = new System.Windows.Forms.CheckBox();
            this.layerMainscreenL1 = new System.Windows.Forms.CheckBox();
            this.layerColorMathNPC = new System.Windows.Forms.CheckBox();
            this.layerSubscreenL1 = new System.Windows.Forms.CheckBox();
            this.layerSubscreenNPC = new System.Windows.Forms.CheckBox();
            this.layerColorMathL1 = new System.Windows.Forms.CheckBox();
            this.layerMainscreenNPC = new System.Windows.Forms.CheckBox();
            this.layerMainscreenL2 = new System.Windows.Forms.CheckBox();
            this.layerColorMathL3 = new System.Windows.Forms.CheckBox();
            this.layerSubscreenL2 = new System.Windows.Forms.CheckBox();
            this.layerSubscreenL3 = new System.Windows.Forms.CheckBox();
            this.layerColorMathL2 = new System.Windows.Forms.CheckBox();
            this.layerMainscreenL3 = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.l1width = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.l2width = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.l3width = new System.Windows.Forms.ComboBox();
            this.l3height = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.l2height = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.l1height = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.spriteMask = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.topSync = new System.Windows.Forms.NumericUpDown();
            this.layerMaskHighY = new System.Windows.Forms.NumericUpDown();
            this.layerMaskHighX = new System.Windows.Forms.NumericUpDown();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.layerL2LeftShift = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.layerL2UpShift = new System.Windows.Forms.NumericUpDown();
            this.layerL3UpShift = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.layerL3LeftShift = new System.Windows.Forms.NumericUpDown();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.treasurePropertyName = new System.Windows.Forms.ComboBox();
            this.treasureXCoord = new System.Windows.Forms.NumericUpDown();
            this.treasureType = new System.Windows.Forms.ComboBox();
            this.label64 = new System.Windows.Forms.Label();
            this.treasureYCoord = new System.Windows.Forms.NumericUpDown();
            this.label67 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.treasurePropertyNum = new System.Windows.Forms.NumericUpDown();
            this.label84 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.treasureCheckMem = new System.Windows.Forms.NumericUpDown();
            this.label36 = new System.Windows.Forms.Label();
            this.treasureCheckBit = new System.Windows.Forms.NumericUpDown();
            this.label37 = new System.Windows.Forms.Label();
            this.treasureListBox = new System.Windows.Forms.ListBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.buttonInsertTreasure = new System.Windows.Forms.ToolStripButton();
            this.buttonDeleteTreasure = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.treasureMoveUp = new System.Windows.Forms.ToolStripButton();
            this.treasureMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.treasureCopy = new System.Windows.Forms.ToolStripButton();
            this.treasurePaste = new System.Windows.Forms.ToolStripButton();
            this.treasureDuplicate = new System.Windows.Forms.ToolStripButton();
            this.treasuresBytesLeft = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.navigateBck = new System.Windows.Forms.ToolStripButton();
            this.navigateFwd = new System.Windows.Forms.ToolStripButton();
            this.loadingLocation = new System.Windows.Forms.ToolStripProgressBar();
            this.loadingLocationLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchLocationNames = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonGotoC = new System.Windows.Forms.ToolStripButton();
            this.entranceEvent = new ZONEDOCTOR.ToolStripNumericUpDown();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.musicName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelLocations = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.import = new System.Windows.Forms.ToolStripDropDownButton();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importArchitectureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.arraysToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicSetsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.export = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportArchitectureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.arraysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicSetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLocationImagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.resetLocationMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetNPCDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetTreasuresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetEventDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetExitDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.resetPaletteSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetGraphicSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetTilesetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetTilemapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetSoliditySetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.resetAnimatedGraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetAllComponentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clear = new System.Windows.Forms.ToolStripDropDownButton();
            this.clearLocationDataAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            this.clearTilesetsAll = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTilemapsAll = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPhysicalMapsAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.unusedGraphicSetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unusedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unusedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.unusedToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.unusedToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllComponentsAll = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllComponentsCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.help = new System.Windows.Forms.ToolStripButton();
            this.baseConversion = new System.Windows.Forms.ToolStripButton();
            this.hexEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesButton = new System.Windows.Forms.ToolStripButton();
            this.openTileset = new System.Windows.Forms.ToolStripButton();
            this.openTilemap = new System.Windows.Forms.ToolStripButton();
            this.openPaletteEditor = new System.Windows.Forms.ToolStripButton();
            this.openGraphicEditor = new System.Windows.Forms.ToolStripButton();
            this.openTemplates = new System.Windows.Forms.ToolStripButton();
            this.openPreviewer = new System.Windows.Forms.ToolStripButton();
            this.spaceAnalyzer = new System.Windows.Forms.ToolStripDropDownButton();
            this.numeralsButton = new System.Windows.Forms.ToolStripButton();
            this.CreateNewLocation = new System.ComponentModel.BackgroundWorker();
            this.tabPage8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcEventPointer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcSpriteIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcSpriteSet)).BeginInit();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcCheckMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcCheckBit)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.panel52.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitWidth)).BeginInit();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestinationY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestinationX)).BeginInit();
            this.toolStrip5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventEventNum)).BeginInit();
            this.toolStrip6.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPaletteSetNum)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPhysicalMapNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL1Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL2Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL3Num)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSetL3Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet4Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationL2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet3Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet2Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationBG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationL3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet1Num)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL2Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL1Num)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBattleBG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapBattleZone)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerPrioritySet)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spriteMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topSync)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskHighY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskHighX)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerL2LeftShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL2UpShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL3UpShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL3LeftShift)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treasureXCoord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treasureYCoord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treasurePropertyNum)).BeginInit();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treasureCheckMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treasureCheckBit)).BeginInit();
            this.toolStrip4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelLocations.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // analyzerInfo
            // 
            this.analyzerInfo.AutoSize = false;
            this.analyzerInfo.Name = "analyzerInfo";
            this.analyzerInfo.Size = new System.Drawing.Size(200, 200);
            this.analyzerInfo.View = System.Windows.Forms.View.Details;
            // 
            // locationInfo
            // 
            this.locationInfo.AutoSize = false;
            this.locationInfo.Name = "locationInfo";
            this.locationInfo.Size = new System.Drawing.Size(160, 130);
            this.locationInfo.View = System.Windows.Forms.View.Details;
            // 
            // locationNum
            // 
            this.locationNum.AutoSize = false;
            this.locationNum.ContextMenuStrip = null;
            this.locationNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locationNum.Hexadecimal = false;
            this.locationNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.locationNum.Location = new System.Drawing.Point(211, 2);
            this.locationNum.Maximum = new decimal(new int[] {
            414,
            0,
            0,
            0});
            this.locationNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.locationNum.Name = "locationNum";
            this.locationNum.Size = new System.Drawing.Size(60, 21);
            this.locationNum.Text = "0";
            this.locationNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.locationNum.ValueChanged += new System.EventHandler(this.locationNum_ValueChanged);
            // 
            // locationName
            // 
            this.locationName.AutoSize = false;
            this.locationName.DropDownHeight = 500;
            this.locationName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.locationName.DropDownWidth = 300;
            this.locationName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.locationName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locationName.IntegralHeight = false;
            this.locationName.Name = "locationName";
            this.locationName.Size = new System.Drawing.Size(200, 21);
            this.locationName.SelectedIndexChanged += new System.EventHandler(this.locationName_SelectedIndexChanged);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.panel9);
            this.tabPage8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(260, 661);
            this.tabPage8.TabIndex = 2;
            this.tabPage8.Text = "NPCS";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.groupBox14);
            this.panel9.Controls.Add(this.groupBox13);
            this.panel9.Controls.Add(this.npcsBytesLeft);
            this.panel9.Controls.Add(this.npcListBox);
            this.panel9.Controls.Add(this.groupBox12);
            this.panel9.Controls.Add(this.toolStrip3);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(260, 661);
            this.panel9.TabIndex = 0;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.npcWalkability);
            this.groupBox14.Controls.Add(this.npcSpeed);
            this.groupBox14.Controls.Add(this.label49);
            this.groupBox14.Controls.Add(this.npcFace);
            this.groupBox14.Controls.Add(this.buttonGotoA);
            this.groupBox14.Controls.Add(this.npcVehicle);
            this.groupBox14.Controls.Add(this.label30);
            this.groupBox14.Controls.Add(this.npcPalette);
            this.groupBox14.Controls.Add(this.npcEventPointer);
            this.groupBox14.Controls.Add(this.label65);
            this.groupBox14.Controls.Add(this.label28);
            this.groupBox14.Controls.Add(this.label33);
            this.groupBox14.Controls.Add(this.label29);
            this.groupBox14.Controls.Add(this.label40);
            this.groupBox14.Controls.Add(this.npcSpriteIndex);
            this.groupBox14.Controls.Add(this.label31);
            this.groupBox14.Controls.Add(this.npcY);
            this.groupBox14.Controls.Add(this.npcX);
            this.groupBox14.Controls.Add(this.npcSpriteSet);
            this.groupBox14.Location = new System.Drawing.Point(128, 28);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(132, 292);
            this.groupBox14.TabIndex = 7;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "NPC Properties";
            // 
            // npcWalkability
            // 
            this.npcWalkability.CheckOnClick = true;
            this.npcWalkability.FormattingEnabled = true;
            this.npcWalkability.Items.AddRange(new object[] {
            "Solidify action path",
            "Can walk under",
            "Can walk over",
            "No face on trigger"});
            this.npcWalkability.Location = new System.Drawing.Point(6, 217);
            this.npcWalkability.Name = "npcWalkability";
            this.npcWalkability.Size = new System.Drawing.Size(120, 68);
            this.npcWalkability.TabIndex = 487;
            this.npcWalkability.SelectedIndexChanged += new System.EventHandler(this.npcWalkability_SelectedIndexChanged);
            // 
            // npcSpeed
            // 
            this.npcSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.npcSpeed.Items.AddRange(new object[] {
            "slow",
            "medium",
            "fast",
            "very fast"});
            this.npcSpeed.Location = new System.Drawing.Point(69, 106);
            this.npcSpeed.Name = "npcSpeed";
            this.npcSpeed.Size = new System.Drawing.Size(57, 21);
            this.npcSpeed.TabIndex = 0;
            this.npcSpeed.SelectedIndexChanged += new System.EventHandler(this.npcSpeed_SelectedIndexChanged);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(6, 23);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(46, 13);
            this.label49.TabIndex = 452;
            this.label49.Text = "Sprite #";
            // 
            // npcFace
            // 
            this.npcFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.npcFace.Items.AddRange(new object[] {
            "north",
            "east",
            "south",
            "west"});
            this.npcFace.Location = new System.Drawing.Point(69, 169);
            this.npcFace.Name = "npcFace";
            this.npcFace.Size = new System.Drawing.Size(57, 21);
            this.npcFace.TabIndex = 193;
            this.npcFace.SelectedIndexChanged += new System.EventHandler(this.npcFace_SelectedIndexChanged);
            // 
            // buttonGotoA
            // 
            this.buttonGotoA.FlatAppearance.BorderSize = 0;
            this.buttonGotoA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGotoA.Location = new System.Drawing.Point(6, 64);
            this.buttonGotoA.Name = "buttonGotoA";
            this.buttonGotoA.Size = new System.Drawing.Size(61, 21);
            this.buttonGotoA.TabIndex = 99;
            this.buttonGotoA.Text = "Event #";
            this.buttonGotoA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonGotoA, "Edit NPC event...");
            this.buttonGotoA.UseCompatibleTextRendering = true;
            this.buttonGotoA.UseVisualStyleBackColor = false;
            this.buttonGotoA.Click += new System.EventHandler(this.buttonGotoA_Click);
            // 
            // npcVehicle
            // 
            this.npcVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.npcVehicle.Items.AddRange(new object[] {
            "none",
            "chocobo",
            "magitek armor",
            "raft"});
            this.npcVehicle.Location = new System.Drawing.Point(69, 190);
            this.npcVehicle.Name = "npcVehicle";
            this.npcVehicle.Size = new System.Drawing.Size(57, 21);
            this.npcVehicle.TabIndex = 193;
            this.npcVehicle.SelectedIndexChanged += new System.EventHandler(this.npcVehicle_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 173);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(38, 13);
            this.label30.TabIndex = 467;
            this.label30.Text = "Facing";
            // 
            // npcPalette
            // 
            this.npcPalette.Location = new System.Drawing.Point(69, 43);
            this.npcPalette.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.npcPalette.Name = "npcPalette";
            this.npcPalette.Size = new System.Drawing.Size(57, 21);
            this.npcPalette.TabIndex = 475;
            this.npcPalette.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcPalette.ValueChanged += new System.EventHandler(this.npcPalette_ValueChanged);
            // 
            // npcEventPointer
            // 
            this.npcEventPointer.Hexadecimal = true;
            this.npcEventPointer.Location = new System.Drawing.Point(69, 64);
            this.npcEventPointer.Maximum = new decimal(new int[] {
            262143,
            0,
            0,
            0});
            this.npcEventPointer.Name = "npcEventPointer";
            this.npcEventPointer.Size = new System.Drawing.Size(57, 21);
            this.npcEventPointer.TabIndex = 100;
            this.npcEventPointer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcEventPointer.ValueChanged += new System.EventHandler(this.npcEventPointer_ValueChanged);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(6, 44);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(52, 13);
            this.label65.TabIndex = 474;
            this.label65.Text = "Palette #";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 152);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(45, 13);
            this.label28.TabIndex = 464;
            this.label28.Text = "Y Coord";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 89);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(48, 13);
            this.label33.TabIndex = 472;
            this.label33.Text = "Action #";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 131);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(45, 13);
            this.label29.TabIndex = 463;
            this.label29.Text = "X Coord";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 110);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(37, 13);
            this.label40.TabIndex = 472;
            this.label40.Text = "Speed";
            // 
            // npcSpriteIndex
            // 
            this.npcSpriteIndex.Location = new System.Drawing.Point(69, 85);
            this.npcSpriteIndex.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.npcSpriteIndex.Name = "npcSpriteIndex";
            this.npcSpriteIndex.Size = new System.Drawing.Size(57, 21);
            this.npcSpriteIndex.TabIndex = 101;
            this.npcSpriteIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcSpriteIndex.ValueChanged += new System.EventHandler(this.npcSpriteIndex_ValueChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 194);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(40, 13);
            this.label31.TabIndex = 471;
            this.label31.Text = "Vehicle";
            // 
            // npcY
            // 
            this.npcY.Location = new System.Drawing.Point(69, 148);
            this.npcY.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.npcY.Name = "npcY";
            this.npcY.Size = new System.Drawing.Size(57, 21);
            this.npcY.TabIndex = 109;
            this.npcY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcY.ValueChanged += new System.EventHandler(this.npcY_ValueChanged);
            // 
            // npcX
            // 
            this.npcX.Location = new System.Drawing.Point(69, 127);
            this.npcX.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.npcX.Name = "npcX";
            this.npcX.Size = new System.Drawing.Size(57, 21);
            this.npcX.TabIndex = 108;
            this.npcX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcX.ValueChanged += new System.EventHandler(this.npcX_ValueChanged);
            // 
            // npcSpriteSet
            // 
            this.npcSpriteSet.Location = new System.Drawing.Point(69, 22);
            this.npcSpriteSet.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.npcSpriteSet.Name = "npcSpriteSet";
            this.npcSpriteSet.Size = new System.Drawing.Size(57, 21);
            this.npcSpriteSet.TabIndex = 98;
            this.npcSpriteSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcSpriteSet.ValueChanged += new System.EventHandler(this.npcSpriteSet_ValueChanged);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.npcUnknownBits);
            this.groupBox13.Location = new System.Drawing.Point(128, 401);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(132, 79);
            this.groupBox13.TabIndex = 7;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Unknown Bits";
            // 
            // npcUnknownBits
            // 
            this.npcUnknownBits.CheckOnClick = true;
            this.npcUnknownBits.ColumnWidth = 40;
            this.npcUnknownBits.FormattingEnabled = true;
            this.npcUnknownBits.Items.AddRange(new object[] {
            "4.7",
            "8.3",
            "8.4",
            "8.5",
            "8.6",
            "8.7"});
            this.npcUnknownBits.Location = new System.Drawing.Point(6, 20);
            this.npcUnknownBits.MultiColumn = true;
            this.npcUnknownBits.Name = "npcUnknownBits";
            this.npcUnknownBits.Size = new System.Drawing.Size(120, 52);
            this.npcUnknownBits.TabIndex = 488;
            this.npcUnknownBits.SelectedIndexChanged += new System.EventHandler(this.npcUnknownBits_SelectedIndexChanged);
            // 
            // npcsBytesLeft
            // 
            this.npcsBytesLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.npcsBytesLeft.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcsBytesLeft.Location = new System.Drawing.Point(0, 28);
            this.npcsBytesLeft.Name = "npcsBytesLeft";
            this.npcsBytesLeft.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.npcsBytesLeft.Size = new System.Drawing.Size(126, 21);
            this.npcsBytesLeft.TabIndex = 497;
            this.npcsBytesLeft.Text = "bytes left";
            this.npcsBytesLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // npcListBox
            // 
            this.npcListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.npcListBox.FormattingEnabled = true;
            this.npcListBox.IntegralHeight = false;
            this.npcListBox.Location = new System.Drawing.Point(0, 51);
            this.npcListBox.Name = "npcListBox";
            this.npcListBox.Size = new System.Drawing.Size(126, 610);
            this.npcListBox.TabIndex = 496;
            this.npcListBox.SelectedIndexChanged += new System.EventHandler(this.npcListBox_SelectedIndexChanged);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label48);
            this.groupBox12.Controls.Add(this.npcCheckMem);
            this.groupBox12.Controls.Add(this.label52);
            this.groupBox12.Controls.Add(this.npcCheckBit);
            this.groupBox12.Location = new System.Drawing.Point(128, 326);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(132, 69);
            this.groupBox12.TabIndex = 7;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Show if memory set";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(6, 23);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(46, 13);
            this.label48.TabIndex = 492;
            this.label48.Text = "Address";
            // 
            // npcCheckMem
            // 
            this.npcCheckMem.Hexadecimal = true;
            this.npcCheckMem.Location = new System.Drawing.Point(69, 20);
            this.npcCheckMem.Maximum = new decimal(new int[] {
            8031,
            0,
            0,
            0});
            this.npcCheckMem.Minimum = new decimal(new int[] {
            7904,
            0,
            0,
            0});
            this.npcCheckMem.Name = "npcCheckMem";
            this.npcCheckMem.Size = new System.Drawing.Size(57, 21);
            this.npcCheckMem.TabIndex = 489;
            this.npcCheckMem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcCheckMem.Value = new decimal(new int[] {
            7904,
            0,
            0,
            0});
            this.npcCheckMem.ValueChanged += new System.EventHandler(this.npcCheckMem_ValueChanged);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(6, 44);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(19, 13);
            this.label52.TabIndex = 491;
            this.label52.Text = "Bit";
            // 
            // npcCheckBit
            // 
            this.npcCheckBit.Location = new System.Drawing.Point(69, 41);
            this.npcCheckBit.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.npcCheckBit.Name = "npcCheckBit";
            this.npcCheckBit.Size = new System.Drawing.Size(57, 21);
            this.npcCheckBit.TabIndex = 490;
            this.npcCheckBit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcCheckBit.ValueChanged += new System.EventHandler(this.npcCheckBit_ValueChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip3.CanOverflow = false;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.npcInsertObject,
            this.npcRemoveObject,
            this.toolStripSeparator10,
            this.npcMoveUp,
            this.npcMoveDown,
            this.toolStripSeparator7,
            this.npcCopy,
            this.npcPaste,
            this.npcDuplicate});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(260, 25);
            this.toolStrip3.TabIndex = 486;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // npcInsertObject
            // 
            this.npcInsertObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcInsertObject.Image = global::ZONEDOCTOR.Properties.Resources.new_small;
            this.npcInsertObject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcInsertObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcInsertObject.Name = "npcInsertObject";
            this.npcInsertObject.Size = new System.Drawing.Size(23, 22);
            this.npcInsertObject.Text = "New NPC";
            this.npcInsertObject.Click += new System.EventHandler(this.npcInsertObject_Click);
            // 
            // npcRemoveObject
            // 
            this.npcRemoveObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcRemoveObject.Image = global::ZONEDOCTOR.Properties.Resources.delete_small;
            this.npcRemoveObject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcRemoveObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcRemoveObject.Name = "npcRemoveObject";
            this.npcRemoveObject.Size = new System.Drawing.Size(23, 22);
            this.npcRemoveObject.Text = "Delete NPC";
            this.npcRemoveObject.Click += new System.EventHandler(this.npcRemoveObject_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // npcMoveUp
            // 
            this.npcMoveUp.AutoSize = false;
            this.npcMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcMoveUp.Image = global::ZONEDOCTOR.Properties.Resources.moveup;
            this.npcMoveUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcMoveUp.Name = "npcMoveUp";
            this.npcMoveUp.Size = new System.Drawing.Size(22, 22);
            this.npcMoveUp.Text = "Move NPC Up";
            this.npcMoveUp.Click += new System.EventHandler(this.npcMoveUp_Click);
            // 
            // npcMoveDown
            // 
            this.npcMoveDown.AutoSize = false;
            this.npcMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcMoveDown.Image = global::ZONEDOCTOR.Properties.Resources.movedown;
            this.npcMoveDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcMoveDown.Name = "npcMoveDown";
            this.npcMoveDown.Size = new System.Drawing.Size(22, 22);
            this.npcMoveDown.Text = "Move NPC Down";
            this.npcMoveDown.Click += new System.EventHandler(this.npcMoveDown_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // npcCopy
            // 
            this.npcCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcCopy.Image = global::ZONEDOCTOR.Properties.Resources.copy_small;
            this.npcCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcCopy.Name = "npcCopy";
            this.npcCopy.Size = new System.Drawing.Size(23, 22);
            this.npcCopy.Text = "Copy NPC";
            this.npcCopy.Click += new System.EventHandler(this.npcCopy_Click);
            // 
            // npcPaste
            // 
            this.npcPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcPaste.Image = global::ZONEDOCTOR.Properties.Resources.paste_small;
            this.npcPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcPaste.Name = "npcPaste";
            this.npcPaste.Size = new System.Drawing.Size(23, 22);
            this.npcPaste.Text = "Paste NPC";
            this.npcPaste.Click += new System.EventHandler(this.npcPaste_Click);
            // 
            // npcDuplicate
            // 
            this.npcDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcDuplicate.Image = global::ZONEDOCTOR.Properties.Resources.duplicate_small;
            this.npcDuplicate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcDuplicate.Name = "npcDuplicate";
            this.npcDuplicate.Size = new System.Drawing.Size(23, 22);
            this.npcDuplicate.Text = "Duplicate NPC";
            this.npcDuplicate.Click += new System.EventHandler(this.npcDuplicate_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 85);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "GFX Set 4";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 64);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "GFX Set 3";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 43);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "GFX Set 2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "GFX Set 1";
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.panel52);
            this.tabPage9.Controls.Add(this.panel2);
            this.tabPage9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(260, 661);
            this.tabPage9.TabIndex = 3;
            this.tabPage9.Text = "FIELDS";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // panel52
            // 
            this.panel52.Controls.Add(this.panel1);
            this.panel52.Controls.Add(this.exitListBox);
            this.panel52.Controls.Add(this.toolStrip5);
            this.panel52.Controls.Add(this.panel68);
            this.panel52.Controls.Add(this.label61);
            this.panel52.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel52.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel52.Location = new System.Drawing.Point(0, 0);
            this.panel52.Name = "panel52";
            this.panel52.Size = new System.Drawing.Size(260, 405);
            this.panel52.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox18);
            this.panel1.Controls.Add(this.groupBox15);
            this.panel1.Controls.Add(this.groupBox16);
            this.panel1.Location = new System.Drawing.Point(127, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 337);
            this.panel1.TabIndex = 7;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.exitUnknownBits);
            this.groupBox18.Location = new System.Drawing.Point(0, 74);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(133, 63);
            this.groupBox18.TabIndex = 7;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Other Bits";
            // 
            // exitUnknownBits
            // 
            this.exitUnknownBits.CheckOnClick = true;
            this.exitUnknownBits.FormattingEnabled = true;
            this.exitUnknownBits.Items.AddRange(new object[] {
            "refresh parent map",
            "3.2"});
            this.exitUnknownBits.Location = new System.Drawing.Point(6, 20);
            this.exitUnknownBits.Name = "exitUnknownBits";
            this.exitUnknownBits.Size = new System.Drawing.Size(121, 36);
            this.exitUnknownBits.TabIndex = 492;
            this.exitUnknownBits.SelectedIndexChanged += new System.EventHandler(this.exitUnknownBits_SelectedIndexChanged);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.exitDirection);
            this.groupBox15.Controls.Add(this.label119);
            this.groupBox15.Controls.Add(this.label47);
            this.groupBox15.Controls.Add(this.exitX);
            this.groupBox15.Controls.Add(this.exitY);
            this.groupBox15.Controls.Add(this.exitWidth);
            this.groupBox15.Location = new System.Drawing.Point(0, 0);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(133, 68);
            this.groupBox15.TabIndex = 7;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Exit Coordinates";
            // 
            // exitDirection
            // 
            this.exitDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exitDirection.DropDownWidth = 80;
            this.exitDirection.Items.AddRange(new object[] {
            "Horizontal",
            "Vertical"});
            this.exitDirection.Location = new System.Drawing.Point(37, 41);
            this.exitDirection.Name = "exitDirection";
            this.exitDirection.Size = new System.Drawing.Size(45, 21);
            this.exitDirection.TabIndex = 212;
            this.exitDirection.SelectedIndexChanged += new System.EventHandler(this.exitDirection_SelectedIndexChanged);
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(6, 23);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(23, 13);
            this.label119.TabIndex = 472;
            this.label119.Text = "X,Y";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(6, 44);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(22, 13);
            this.label47.TabIndex = 490;
            this.label47.Text = "F/L";
            // 
            // exitX
            // 
            this.exitX.Location = new System.Drawing.Point(37, 20);
            this.exitX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.exitX.Name = "exitX";
            this.exitX.Size = new System.Drawing.Size(45, 21);
            this.exitX.TabIndex = 131;
            this.exitX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitX.ValueChanged += new System.EventHandler(this.exitX_ValueChanged);
            // 
            // exitY
            // 
            this.exitY.Location = new System.Drawing.Point(82, 20);
            this.exitY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.exitY.Name = "exitY";
            this.exitY.Size = new System.Drawing.Size(45, 21);
            this.exitY.TabIndex = 132;
            this.exitY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitY.ValueChanged += new System.EventHandler(this.exitY_ValueChanged);
            // 
            // exitWidth
            // 
            this.exitWidth.Location = new System.Drawing.Point(82, 41);
            this.exitWidth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.exitWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.exitWidth.Name = "exitWidth";
            this.exitWidth.Size = new System.Drawing.Size(45, 21);
            this.exitWidth.TabIndex = 134;
            this.exitWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.exitWidth.ValueChanged += new System.EventHandler(this.exitWidth_ValueChanged);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.exitDestination);
            this.groupBox16.Controls.Add(this.exitDestinationFacing);
            this.groupBox16.Controls.Add(this.exitToWorldMap);
            this.groupBox16.Controls.Add(this.label59);
            this.groupBox16.Controls.Add(this.label124);
            this.groupBox16.Controls.Add(this.exitDestinationY);
            this.groupBox16.Controls.Add(this.exitShowMessage);
            this.groupBox16.Controls.Add(this.exitDestinationX);
            this.groupBox16.Location = new System.Drawing.Point(0, 143);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(133, 137);
            this.groupBox16.TabIndex = 493;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Destination";
            // 
            // exitDestination
            // 
            this.exitDestination.DropDownHeight = 400;
            this.exitDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exitDestination.DropDownWidth = 300;
            this.exitDestination.IntegralHeight = false;
            this.exitDestination.Location = new System.Drawing.Point(6, 40);
            this.exitDestination.Name = "exitDestination";
            this.exitDestination.Size = new System.Drawing.Size(121, 21);
            this.exitDestination.TabIndex = 196;
            this.exitDestination.SelectedIndexChanged += new System.EventHandler(this.exitDestination_SelectedIndexChanged);
            // 
            // exitDestinationFacing
            // 
            this.exitDestinationFacing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exitDestinationFacing.DropDownWidth = 70;
            this.exitDestinationFacing.Items.AddRange(new object[] {
            "north",
            "east",
            "south",
            "west"});
            this.exitDestinationFacing.Location = new System.Drawing.Point(37, 109);
            this.exitDestinationFacing.Name = "exitDestinationFacing";
            this.exitDestinationFacing.Size = new System.Drawing.Size(90, 21);
            this.exitDestinationFacing.TabIndex = 210;
            this.exitDestinationFacing.SelectedIndexChanged += new System.EventHandler(this.exitDestinationFacing_SelectedIndexChanged);
            // 
            // exitToWorldMap
            // 
            this.exitToWorldMap.Appearance = System.Windows.Forms.Appearance.Button;
            this.exitToWorldMap.BackColor = System.Drawing.SystemColors.Control;
            this.exitToWorldMap.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitToWorldMap.ForeColor = System.Drawing.Color.Gray;
            this.exitToWorldMap.Location = new System.Drawing.Point(6, 20);
            this.exitToWorldMap.Name = "exitToWorldMap";
            this.exitToWorldMap.Size = new System.Drawing.Size(121, 18);
            this.exitToWorldMap.TabIndex = 489;
            this.exitToWorldMap.Text = "TO WORLD MAP";
            this.exitToWorldMap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exitToWorldMap.UseCompatibleTextRendering = true;
            this.exitToWorldMap.UseVisualStyleBackColor = false;
            this.exitToWorldMap.CheckedChanged += new System.EventHandler(this.exitToWorldMap_CheckedChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(6, 90);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(23, 13);
            this.label59.TabIndex = 474;
            this.label59.Text = "X,Y";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(6, 112);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(30, 13);
            this.label124.TabIndex = 488;
            this.label124.Text = "Face";
            // 
            // exitDestinationY
            // 
            this.exitDestinationY.Location = new System.Drawing.Point(82, 88);
            this.exitDestinationY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.exitDestinationY.Name = "exitDestinationY";
            this.exitDestinationY.Size = new System.Drawing.Size(45, 21);
            this.exitDestinationY.TabIndex = 139;
            this.exitDestinationY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitDestinationY.ValueChanged += new System.EventHandler(this.exitDestinationY_ValueChanged);
            // 
            // exitShowMessage
            // 
            this.exitShowMessage.Appearance = System.Windows.Forms.Appearance.Button;
            this.exitShowMessage.BackColor = System.Drawing.SystemColors.Control;
            this.exitShowMessage.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitShowMessage.ForeColor = System.Drawing.Color.Gray;
            this.exitShowMessage.Location = new System.Drawing.Point(6, 64);
            this.exitShowMessage.Name = "exitShowMessage";
            this.exitShowMessage.Size = new System.Drawing.Size(121, 18);
            this.exitShowMessage.TabIndex = 129;
            this.exitShowMessage.Text = "SHOW MESSAGE";
            this.exitShowMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exitShowMessage.UseCompatibleTextRendering = true;
            this.exitShowMessage.UseVisualStyleBackColor = false;
            this.exitShowMessage.CheckedChanged += new System.EventHandler(this.exitShowMessage_CheckedChanged);
            // 
            // exitDestinationX
            // 
            this.exitDestinationX.Location = new System.Drawing.Point(37, 88);
            this.exitDestinationX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.exitDestinationX.Name = "exitDestinationX";
            this.exitDestinationX.Size = new System.Drawing.Size(45, 21);
            this.exitDestinationX.TabIndex = 138;
            this.exitDestinationX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitDestinationX.ValueChanged += new System.EventHandler(this.exitDestinationX_ValueChanged);
            // 
            // exitListBox
            // 
            this.exitListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.exitListBox.FormattingEnabled = true;
            this.exitListBox.IntegralHeight = false;
            this.exitListBox.Location = new System.Drawing.Point(0, 44);
            this.exitListBox.Name = "exitListBox";
            this.exitListBox.Size = new System.Drawing.Size(125, 361);
            this.exitListBox.TabIndex = 498;
            this.exitListBox.SelectedIndexChanged += new System.EventHandler(this.exitListBox_SelectedIndexChanged);
            // 
            // toolStrip5
            // 
            this.toolStrip5.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip5.CanOverflow = false;
            this.toolStrip5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonInsertExit,
            this.buttonDeleteExit,
            this.toolStripSeparator12,
            this.exitsCopyField,
            this.exitsPasteField,
            this.exitsDuplicateField,
            this.toolStripSeparator14,
            this.exitsBytesLeft});
            this.toolStrip5.Location = new System.Drawing.Point(0, 19);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip5.Size = new System.Drawing.Size(260, 25);
            this.toolStrip5.TabIndex = 496;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // buttonInsertExit
            // 
            this.buttonInsertExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonInsertExit.Image = global::ZONEDOCTOR.Properties.Resources.new_small;
            this.buttonInsertExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonInsertExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonInsertExit.Name = "buttonInsertExit";
            this.buttonInsertExit.Size = new System.Drawing.Size(23, 22);
            this.buttonInsertExit.Text = "New Exit";
            this.buttonInsertExit.Click += new System.EventHandler(this.buttonInsertExit_Click);
            // 
            // buttonDeleteExit
            // 
            this.buttonDeleteExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDeleteExit.Image = global::ZONEDOCTOR.Properties.Resources.delete_small;
            this.buttonDeleteExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonDeleteExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDeleteExit.Name = "buttonDeleteExit";
            this.buttonDeleteExit.Size = new System.Drawing.Size(23, 22);
            this.buttonDeleteExit.Text = "Delete Exit";
            this.buttonDeleteExit.Click += new System.EventHandler(this.buttonDeleteExit_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // exitsCopyField
            // 
            this.exitsCopyField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitsCopyField.Image = global::ZONEDOCTOR.Properties.Resources.copy_small;
            this.exitsCopyField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitsCopyField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitsCopyField.Name = "exitsCopyField";
            this.exitsCopyField.Size = new System.Drawing.Size(23, 22);
            this.exitsCopyField.Text = "Copy Exit";
            this.exitsCopyField.Click += new System.EventHandler(this.exitsCopyField_Click);
            // 
            // exitsPasteField
            // 
            this.exitsPasteField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitsPasteField.Image = global::ZONEDOCTOR.Properties.Resources.paste_small;
            this.exitsPasteField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitsPasteField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitsPasteField.Name = "exitsPasteField";
            this.exitsPasteField.Size = new System.Drawing.Size(23, 22);
            this.exitsPasteField.Text = "Paste Exit";
            this.exitsPasteField.Click += new System.EventHandler(this.exitsPasteField_Click);
            // 
            // exitsDuplicateField
            // 
            this.exitsDuplicateField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitsDuplicateField.Image = global::ZONEDOCTOR.Properties.Resources.duplicate_small;
            this.exitsDuplicateField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitsDuplicateField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitsDuplicateField.Name = "exitsDuplicateField";
            this.exitsDuplicateField.Size = new System.Drawing.Size(23, 22);
            this.exitsDuplicateField.Text = "Duplicate Exit";
            this.exitsDuplicateField.Click += new System.EventHandler(this.exitsDuplicateField_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // exitsBytesLeft
            // 
            this.exitsBytesLeft.Name = "exitsBytesLeft";
            this.exitsBytesLeft.Size = new System.Drawing.Size(52, 22);
            this.exitsBytesLeft.Text = "bytes left";
            // 
            // panel68
            // 
            this.panel68.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel68.BackgroundImage = global::ZONEDOCTOR.Properties.Resources._bg;
            this.panel68.Location = new System.Drawing.Point(119, 608);
            this.panel68.Name = "panel68";
            this.panel68.Size = new System.Drawing.Size(121, 80);
            this.panel68.TabIndex = 493;
            // 
            // label61
            // 
            this.label61.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label61.Dock = System.Windows.Forms.DockStyle.Top;
            this.label61.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(0, 0);
            this.label61.Name = "label61";
            this.label61.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label61.Size = new System.Drawing.Size(260, 19);
            this.label61.TabIndex = 453;
            this.label61.Text = "EXIT FIELDS";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox17);
            this.panel2.Controls.Add(this.eventListBox);
            this.panel2.Controls.Add(this.toolStrip6);
            this.panel2.Controls.Add(this.label63);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 405);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 256);
            this.panel2.TabIndex = 498;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.buttonGotoD);
            this.groupBox17.Controls.Add(this.eventY);
            this.groupBox17.Controls.Add(this.eventX);
            this.groupBox17.Controls.Add(this.label127);
            this.groupBox17.Controls.Add(this.eventEventNum);
            this.groupBox17.Location = new System.Drawing.Point(127, 47);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(133, 72);
            this.groupBox17.TabIndex = 493;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Event Properties";
            // 
            // buttonGotoD
            // 
            this.buttonGotoD.FlatAppearance.BorderSize = 0;
            this.buttonGotoD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGotoD.Location = new System.Drawing.Point(6, 20);
            this.buttonGotoD.Name = "buttonGotoD";
            this.buttonGotoD.Size = new System.Drawing.Size(58, 21);
            this.buttonGotoD.TabIndex = 492;
            this.buttonGotoD.Text = "Event #";
            this.buttonGotoD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonGotoD, "Edit event field event...");
            this.buttonGotoD.UseCompatibleTextRendering = true;
            this.buttonGotoD.UseVisualStyleBackColor = false;
            this.buttonGotoD.Click += new System.EventHandler(this.buttonGotoD_Click);
            // 
            // eventY
            // 
            this.eventY.Location = new System.Drawing.Point(82, 45);
            this.eventY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.eventY.Name = "eventY";
            this.eventY.Size = new System.Drawing.Size(45, 21);
            this.eventY.TabIndex = 151;
            this.eventY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventY.ValueChanged += new System.EventHandler(this.eventY_ValueChanged);
            // 
            // eventX
            // 
            this.eventX.Location = new System.Drawing.Point(37, 45);
            this.eventX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.eventX.Name = "eventX";
            this.eventX.Size = new System.Drawing.Size(45, 21);
            this.eventX.TabIndex = 150;
            this.eventX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventX.ValueChanged += new System.EventHandler(this.eventX_ValueChanged);
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(6, 48);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(23, 13);
            this.label127.TabIndex = 473;
            this.label127.Text = "X,Y";
            // 
            // eventEventNum
            // 
            this.eventEventNum.Hexadecimal = true;
            this.eventEventNum.Location = new System.Drawing.Point(66, 20);
            this.eventEventNum.Maximum = new decimal(new int[] {
            262143,
            0,
            0,
            0});
            this.eventEventNum.Name = "eventEventNum";
            this.eventEventNum.Size = new System.Drawing.Size(61, 21);
            this.eventEventNum.TabIndex = 149;
            this.eventEventNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventEventNum.ValueChanged += new System.EventHandler(this.eventEventNum_ValueChanged);
            // 
            // eventListBox
            // 
            this.eventListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.eventListBox.FormattingEnabled = true;
            this.eventListBox.IntegralHeight = false;
            this.eventListBox.Location = new System.Drawing.Point(0, 44);
            this.eventListBox.Name = "eventListBox";
            this.eventListBox.Size = new System.Drawing.Size(125, 212);
            this.eventListBox.TabIndex = 499;
            this.eventListBox.SelectedIndexChanged += new System.EventHandler(this.eventListBox_SelectedIndexChanged);
            // 
            // toolStrip6
            // 
            this.toolStrip6.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip6.CanOverflow = false;
            this.toolStrip6.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonInsertEvent,
            this.buttonDeleteEvent,
            this.toolStripSeparator13,
            this.eventsCopyField,
            this.eventsPasteField,
            this.eventsDuplicateField,
            this.toolStripSeparator18,
            this.eventsBytesLeft});
            this.toolStrip6.Location = new System.Drawing.Point(0, 19);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip6.Size = new System.Drawing.Size(260, 25);
            this.toolStrip6.TabIndex = 497;
            this.toolStrip6.Text = "toolStrip6";
            // 
            // buttonInsertEvent
            // 
            this.buttonInsertEvent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonInsertEvent.Image = global::ZONEDOCTOR.Properties.Resources.new_small;
            this.buttonInsertEvent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonInsertEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonInsertEvent.Name = "buttonInsertEvent";
            this.buttonInsertEvent.Size = new System.Drawing.Size(23, 22);
            this.buttonInsertEvent.Text = "New Event";
            this.buttonInsertEvent.Click += new System.EventHandler(this.buttonInsertEvent_Click);
            // 
            // buttonDeleteEvent
            // 
            this.buttonDeleteEvent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDeleteEvent.Image = global::ZONEDOCTOR.Properties.Resources.delete_small;
            this.buttonDeleteEvent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonDeleteEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDeleteEvent.Name = "buttonDeleteEvent";
            this.buttonDeleteEvent.Size = new System.Drawing.Size(23, 22);
            this.buttonDeleteEvent.Text = "Delete Event";
            this.buttonDeleteEvent.Click += new System.EventHandler(this.buttonDeleteEvent_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // eventsCopyField
            // 
            this.eventsCopyField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eventsCopyField.Image = global::ZONEDOCTOR.Properties.Resources.copy_small;
            this.eventsCopyField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eventsCopyField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eventsCopyField.Name = "eventsCopyField";
            this.eventsCopyField.Size = new System.Drawing.Size(23, 22);
            this.eventsCopyField.Text = "Copy Event";
            this.eventsCopyField.Click += new System.EventHandler(this.eventsCopyField_Click);
            // 
            // eventsPasteField
            // 
            this.eventsPasteField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eventsPasteField.Image = global::ZONEDOCTOR.Properties.Resources.paste_small;
            this.eventsPasteField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eventsPasteField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eventsPasteField.Name = "eventsPasteField";
            this.eventsPasteField.Size = new System.Drawing.Size(23, 22);
            this.eventsPasteField.Text = "Paste Event";
            this.eventsPasteField.Click += new System.EventHandler(this.eventsPasteField_Click);
            // 
            // eventsDuplicateField
            // 
            this.eventsDuplicateField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eventsDuplicateField.Image = global::ZONEDOCTOR.Properties.Resources.duplicate_small;
            this.eventsDuplicateField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eventsDuplicateField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eventsDuplicateField.Name = "eventsDuplicateField";
            this.eventsDuplicateField.Size = new System.Drawing.Size(23, 22);
            this.eventsDuplicateField.Text = "Duplicate Event";
            this.eventsDuplicateField.Click += new System.EventHandler(this.eventsDuplicateField_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
            // 
            // eventsBytesLeft
            // 
            this.eventsBytesLeft.Name = "eventsBytesLeft";
            this.eventsBytesLeft.Size = new System.Drawing.Size(52, 22);
            this.eventsBytesLeft.Text = "bytes left";
            // 
            // label63
            // 
            this.label63.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label63.Dock = System.Windows.Forms.DockStyle.Top;
            this.label63.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(0, 0);
            this.label63.Name = "label63";
            this.label63.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label63.Size = new System.Drawing.Size(260, 19);
            this.label63.TabIndex = 456;
            this.label63.Text = "EVENT FIELDS";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage8);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage9);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ItemSize = new System.Drawing.Size(44, 18);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(5, 4);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(268, 687);
            this.tabControl.TabIndex = 6;
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox21);
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(260, 661);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "MAPS";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.tbMapName);
            this.groupBox21.Location = new System.Drawing.Point(3, 476);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(260, 48);
            this.groupBox21.TabIndex = 241;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Map Name (Zone Doctor CE)";
            // 
            // tbMapName
            // 
            this.tbMapName.Enabled = false;
            this.tbMapName.Location = new System.Drawing.Point(6, 21);
            this.tbMapName.MaxLength = 80;
            this.tbMapName.Name = "tbMapName";
            this.tbMapName.Size = new System.Drawing.Size(245, 21);
            this.tbMapName.TabIndex = 0;
            this.tbMapName.TabStop = false;
            this.tbMapName.TextChanged += new System.EventHandler(this.tbMapName_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.mapPaletteSetNum);
            this.groupBox4.Controls.Add(this.label46);
            this.groupBox4.Location = new System.Drawing.Point(0, 422);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 48);
            this.groupBox4.TabIndex = 240;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Palettes";
            // 
            // mapPaletteSetNum
            // 
            this.mapPaletteSetNum.Location = new System.Drawing.Point(109, 20);
            this.mapPaletteSetNum.Maximum = new decimal(new int[] {
            49,
            0,
            0,
            0});
            this.mapPaletteSetNum.Name = "mapPaletteSetNum";
            this.mapPaletteSetNum.Size = new System.Drawing.Size(145, 21);
            this.mapPaletteSetNum.TabIndex = 80;
            this.mapPaletteSetNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapPaletteSetNum.ValueChanged += new System.EventHandler(this.mapPaletteSetNum_ValueChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 22);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(60, 13);
            this.label46.TabIndex = 157;
            this.label46.Text = "Palette Set";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.useWorldMapBG);
            this.groupBox3.Controls.Add(this.mapSetL3Priority);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.mapPhysicalMapNum);
            this.groupBox3.Controls.Add(this.label45);
            this.groupBox3.Controls.Add(this.mapTilemapL1Num);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.mapTilemapL2Num);
            this.groupBox3.Controls.Add(this.mapTilemapL3Num);
            this.groupBox3.Location = new System.Drawing.Point(0, 275);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 141);
            this.groupBox3.TabIndex = 240;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tilemaps";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 22);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(57, 13);
            this.label43.TabIndex = 132;
            this.label43.Text = "L1 Tilemap";
            // 
            // useWorldMapBG
            // 
            this.useWorldMapBG.Appearance = System.Windows.Forms.Appearance.Button;
            this.useWorldMapBG.BackColor = System.Drawing.SystemColors.Control;
            this.useWorldMapBG.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useWorldMapBG.ForeColor = System.Drawing.Color.Gray;
            this.useWorldMapBG.Location = new System.Drawing.Point(133, 89);
            this.useWorldMapBG.Name = "useWorldMapBG";
            this.useWorldMapBG.Size = new System.Drawing.Size(121, 18);
            this.useWorldMapBG.TabIndex = 155;
            this.useWorldMapBG.Text = "USE WORLD MAP BG";
            this.useWorldMapBG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.useWorldMapBG.UseCompatibleTextRendering = true;
            this.useWorldMapBG.UseVisualStyleBackColor = false;
            this.useWorldMapBG.CheckedChanged += new System.EventHandler(this.useWorldMapBG_CheckedChanged);
            // 
            // mapSetL3Priority
            // 
            this.mapSetL3Priority.Appearance = System.Windows.Forms.Appearance.Button;
            this.mapSetL3Priority.BackColor = System.Drawing.SystemColors.Control;
            this.mapSetL3Priority.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapSetL3Priority.ForeColor = System.Drawing.Color.Gray;
            this.mapSetL3Priority.Location = new System.Drawing.Point(6, 89);
            this.mapSetL3Priority.Name = "mapSetL3Priority";
            this.mapSetL3Priority.Size = new System.Drawing.Size(121, 18);
            this.mapSetL3Priority.TabIndex = 69;
            this.mapSetL3Priority.Text = "L3 PRIORITY 1";
            this.mapSetL3Priority.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mapSetL3Priority.UseCompatibleTextRendering = true;
            this.mapSetL3Priority.UseVisualStyleBackColor = false;
            this.mapSetL3Priority.CheckedChanged += new System.EventHandler(this.mapSetL3Priority_CheckedChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 64);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(57, 13);
            this.label41.TabIndex = 137;
            this.label41.Text = "L3 Tilemap";
            // 
            // mapPhysicalMapNum
            // 
            this.mapPhysicalMapNum.Location = new System.Drawing.Point(109, 113);
            this.mapPhysicalMapNum.Maximum = new decimal(new int[] {
            42,
            0,
            0,
            0});
            this.mapPhysicalMapNum.Name = "mapPhysicalMapNum";
            this.mapPhysicalMapNum.Size = new System.Drawing.Size(145, 21);
            this.mapPhysicalMapNum.TabIndex = 76;
            this.mapPhysicalMapNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapPhysicalMapNum.ValueChanged += new System.EventHandler(this.mapPhysicalMapNum_ValueChanged);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 115);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(60, 13);
            this.label45.TabIndex = 152;
            this.label45.Text = "Solidity Set";
            // 
            // mapTilemapL1Num
            // 
            this.mapTilemapL1Num.Location = new System.Drawing.Point(109, 20);
            this.mapTilemapL1Num.Maximum = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.mapTilemapL1Num.Name = "mapTilemapL1Num";
            this.mapTilemapL1Num.Size = new System.Drawing.Size(145, 21);
            this.mapTilemapL1Num.TabIndex = 70;
            this.mapTilemapL1Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilemapL1Num.ValueChanged += new System.EventHandler(this.mapTilemapL1Num_ValueChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 43);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(57, 13);
            this.label42.TabIndex = 136;
            this.label42.Text = "L2 Tilemap";
            // 
            // mapTilemapL2Num
            // 
            this.mapTilemapL2Num.Location = new System.Drawing.Point(109, 41);
            this.mapTilemapL2Num.Maximum = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.mapTilemapL2Num.Name = "mapTilemapL2Num";
            this.mapTilemapL2Num.Size = new System.Drawing.Size(145, 21);
            this.mapTilemapL2Num.TabIndex = 72;
            this.mapTilemapL2Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilemapL2Num.ValueChanged += new System.EventHandler(this.mapTilemapL2Num_ValueChanged);
            // 
            // mapTilemapL3Num
            // 
            this.mapTilemapL3Num.Location = new System.Drawing.Point(109, 62);
            this.mapTilemapL3Num.Maximum = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.mapTilemapL3Num.Name = "mapTilemapL3Num";
            this.mapTilemapL3Num.Size = new System.Drawing.Size(145, 21);
            this.mapTilemapL3Num.TabIndex = 74;
            this.mapTilemapL3Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilemapL3Num.ValueChanged += new System.EventHandler(this.mapTilemapL3Num_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.mapGFXSetL3Num);
            this.groupBox1.Controls.Add(this.mapGFXSet4Num);
            this.groupBox1.Controls.Add(this.animationL2);
            this.groupBox1.Controls.Add(this.mapGFXSet3Num);
            this.groupBox1.Controls.Add(this.mapGFXSet2Num);
            this.groupBox1.Controls.Add(this.animationBG);
            this.groupBox1.Controls.Add(this.animationL3);
            this.groupBox1.Controls.Add(this.mapGFXSet1Num);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 194);
            this.groupBox1.TabIndex = 240;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graphic Sets";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 127);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 13);
            this.label19.TabIndex = 238;
            this.label19.Text = "Animation L2";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 106);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(59, 13);
            this.label44.TabIndex = 35;
            this.label44.Text = "L3 GFX Set";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 239;
            this.label8.Text = "Animation BG";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(6, 148);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(68, 13);
            this.label39.TabIndex = 239;
            this.label39.Text = "Animation L3";
            // 
            // mapGFXSetL3Num
            // 
            this.mapGFXSetL3Num.Location = new System.Drawing.Point(109, 104);
            this.mapGFXSetL3Num.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.mapGFXSetL3Num.Name = "mapGFXSetL3Num";
            this.mapGFXSetL3Num.Size = new System.Drawing.Size(145, 21);
            this.mapGFXSetL3Num.TabIndex = 61;
            this.mapGFXSetL3Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSetL3Num.ValueChanged += new System.EventHandler(this.mapGFXSetL3Num_ValueChanged);
            // 
            // mapGFXSet4Num
            // 
            this.mapGFXSet4Num.Location = new System.Drawing.Point(109, 83);
            this.mapGFXSet4Num.Maximum = new decimal(new int[] {
            81,
            0,
            0,
            0});
            this.mapGFXSet4Num.Name = "mapGFXSet4Num";
            this.mapGFXSet4Num.Size = new System.Drawing.Size(145, 21);
            this.mapGFXSet4Num.TabIndex = 57;
            this.mapGFXSet4Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSet4Num.ValueChanged += new System.EventHandler(this.mapGFXSet4Num_ValueChanged);
            // 
            // animationL2
            // 
            this.animationL2.Location = new System.Drawing.Point(109, 125);
            this.animationL2.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.animationL2.Name = "animationL2";
            this.animationL2.Size = new System.Drawing.Size(145, 21);
            this.animationL2.TabIndex = 236;
            this.animationL2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.animationL2.ValueChanged += new System.EventHandler(this.animationL2_ValueChanged);
            // 
            // mapGFXSet3Num
            // 
            this.mapGFXSet3Num.Location = new System.Drawing.Point(109, 62);
            this.mapGFXSet3Num.Maximum = new decimal(new int[] {
            81,
            0,
            0,
            0});
            this.mapGFXSet3Num.Name = "mapGFXSet3Num";
            this.mapGFXSet3Num.Size = new System.Drawing.Size(145, 21);
            this.mapGFXSet3Num.TabIndex = 55;
            this.mapGFXSet3Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSet3Num.ValueChanged += new System.EventHandler(this.mapGFXSet3Num_ValueChanged);
            // 
            // mapGFXSet2Num
            // 
            this.mapGFXSet2Num.Location = new System.Drawing.Point(109, 41);
            this.mapGFXSet2Num.Maximum = new decimal(new int[] {
            81,
            0,
            0,
            0});
            this.mapGFXSet2Num.Name = "mapGFXSet2Num";
            this.mapGFXSet2Num.Size = new System.Drawing.Size(145, 21);
            this.mapGFXSet2Num.TabIndex = 53;
            this.mapGFXSet2Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSet2Num.ValueChanged += new System.EventHandler(this.mapGFXSet2Num_ValueChanged);
            // 
            // animationBG
            // 
            this.animationBG.Location = new System.Drawing.Point(109, 167);
            this.animationBG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.animationBG.Name = "animationBG";
            this.animationBG.Size = new System.Drawing.Size(145, 21);
            this.animationBG.TabIndex = 237;
            this.animationBG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.animationBG.ValueChanged += new System.EventHandler(this.animationBG_ValueChanged);
            // 
            // animationL3
            // 
            this.animationL3.Location = new System.Drawing.Point(109, 146);
            this.animationL3.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.animationL3.Name = "animationL3";
            this.animationL3.Size = new System.Drawing.Size(145, 21);
            this.animationL3.TabIndex = 237;
            this.animationL3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.animationL3.ValueChanged += new System.EventHandler(this.animationL3_ValueChanged);
            // 
            // mapGFXSet1Num
            // 
            this.mapGFXSet1Num.Location = new System.Drawing.Point(109, 20);
            this.mapGFXSet1Num.Maximum = new decimal(new int[] {
            81,
            0,
            0,
            0});
            this.mapGFXSet1Num.Name = "mapGFXSet1Num";
            this.mapGFXSet1Num.Size = new System.Drawing.Size(145, 21);
            this.mapGFXSet1Num.TabIndex = 51;
            this.mapGFXSet1Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSet1Num.ValueChanged += new System.EventHandler(this.mapGFXSet1Num_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label34);
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.Controls.Add(this.mapTilesetL2Num);
            this.groupBox2.Controls.Add(this.mapTilesetL1Num);
            this.groupBox2.Location = new System.Drawing.Point(0, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 69);
            this.groupBox2.TabIndex = 240;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tilesets";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 22);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(52, 13);
            this.label34.TabIndex = 123;
            this.label34.Text = "L1 Tileset";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 43);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(52, 13);
            this.label35.TabIndex = 127;
            this.label35.Text = "L2 Tileset";
            // 
            // mapTilesetL2Num
            // 
            this.mapTilesetL2Num.Location = new System.Drawing.Point(109, 41);
            this.mapTilesetL2Num.Maximum = new decimal(new int[] {
            74,
            0,
            0,
            0});
            this.mapTilesetL2Num.Name = "mapTilesetL2Num";
            this.mapTilesetL2Num.Size = new System.Drawing.Size(145, 21);
            this.mapTilesetL2Num.TabIndex = 65;
            this.mapTilesetL2Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilesetL2Num.ValueChanged += new System.EventHandler(this.mapTilesetL2Num_ValueChanged);
            // 
            // mapTilesetL1Num
            // 
            this.mapTilesetL1Num.Location = new System.Drawing.Point(109, 20);
            this.mapTilesetL1Num.Maximum = new decimal(new int[] {
            74,
            0,
            0,
            0});
            this.mapTilesetL1Num.Name = "mapTilesetL1Num";
            this.mapTilesetL1Num.Size = new System.Drawing.Size(145, 21);
            this.mapTilesetL1Num.TabIndex = 63;
            this.mapTilesetL1Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilesetL1Num.ValueChanged += new System.EventHandler(this.mapTilesetL1Num_ValueChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox22);
            this.tabPage3.Controls.Add(this.groupBox11);
            this.tabPage3.Controls.Add(this.groupBox9);
            this.tabPage3.Controls.Add(this.messageName);
            this.tabPage3.Controls.Add(this.groupBox10);
            this.tabPage3.Controls.Add(this.label53);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.groupBox8);
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(260, 661);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "LAYERS";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.tbLocation);
            this.groupBox22.Location = new System.Drawing.Point(0, 33);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(260, 48);
            this.groupBox22.TabIndex = 242;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Change Message";
            // 
            // tbLocation
            // 
            this.tbLocation.Location = new System.Drawing.Point(6, 21);
            this.tbLocation.MaxLength = 80;
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(245, 21);
            this.tbLocation.TabIndex = 0;
            this.tbLocation.TabStop = false;
            this.tbLocation.TextChanged += new System.EventHandler(this.tbLocation_TextChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.layerUnknownBits);
            this.groupBox11.Location = new System.Drawing.Point(0, 630);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(260, 48);
            this.groupBox11.TabIndex = 7;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Unknown Bits";
            // 
            // layerUnknownBits
            // 
            this.layerUnknownBits.CheckOnClick = true;
            this.layerUnknownBits.ColumnWidth = 120;
            this.layerUnknownBits.FormattingEnabled = true;
            this.layerUnknownBits.Items.AddRange(new object[] {
            "1.6",
            "6.7"});
            this.layerUnknownBits.Location = new System.Drawing.Point(6, 20);
            this.layerUnknownBits.MultiColumn = true;
            this.layerUnknownBits.Name = "layerUnknownBits";
            this.layerUnknownBits.Size = new System.Drawing.Size(248, 20);
            this.layerUnknownBits.TabIndex = 0;
            this.layerUnknownBits.SelectedIndexChanged += new System.EventHandler(this.layerUnknownBits_SelectedIndexChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.mapBattleBGName);
            this.groupBox9.Controls.Add(this.label71);
            this.groupBox9.Controls.Add(this.mapRandomBattles);
            this.groupBox9.Controls.Add(this.mapBattleBG);
            this.groupBox9.Controls.Add(this.label38);
            this.groupBox9.Controls.Add(this.mapBattleZone);
            this.groupBox9.Location = new System.Drawing.Point(0, 445);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(260, 69);
            this.groupBox9.TabIndex = 7;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Battle Properties";
            // 
            // mapBattleBGName
            // 
            this.mapBattleBGName.BackColor = System.Drawing.SystemColors.Window;
            this.mapBattleBGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapBattleBGName.DropDownWidth = 150;
            this.mapBattleBGName.Items.AddRange(new object[] {
            "Grasslands (WoB)",
            "Forest (WoR)",
            "Desert (WoB)",
            "Forest (WoB)",
            "Zozo, inside building",
            "Land (WoR)",
            "Veldt (WoB)",
            "Clouds, falling",
            "Narshe",
            "Mines (WoB)",
            "Mines (WoR)",
            "Mountains, outside",
            "Mountains, inside",
            "Raft, in rapids",
            "Imperial camp",
            "Train, exterior",
            "Train, interior",
            "Caves (WoB)",
            "Snow field",
            "South Figaro",
            "Imperial Vector 1",
            "Floating Continent",
            "Kefka\'s Tower, outside",
            "Opera House, stage",
            "Opera House, rafters",
            "Burning House",
            "Figaro Castle, interior",
            "Imperial Magitek Facility",
            "Colosseum",
            "Imperial Vector 2",
            "Thamasa",
            "Waterfall",
            "Owzer\'s House",
            "Phantom Train boss",
            "Esper World Bridge",
            "Underwater",
            "Zozo, outside",
            "Airship Deck, center",
            "Daryl\'s Tomb",
            "Ancient Castle",
            "Kefka\'s Tower, inside",
            "Airship Deck, right (WoR)",
            "Caves (lava)",
            "Fanatic\'s Tower, inside",
            "Imperial Vector, cart ride",
            "Fanatic\'s Tower, outside",
            "Cyan\'s Dream",
            "Desert (WoR)",
            "Airship Deck, right (WoB)",
            "___unused",
            "___unused",
            "Last Battle, 1st tier",
            "Last Battle, 2nd tier",
            "Last Battle, 3rd tier",
            "Last Battle, Kefka",
            "Figaro Castle, tentacles"});
            this.mapBattleBGName.Location = new System.Drawing.Point(127, 20);
            this.mapBattleBGName.Name = "mapBattleBGName";
            this.mapBattleBGName.Size = new System.Drawing.Size(127, 21);
            this.mapBattleBGName.TabIndex = 0;
            this.mapBattleBGName.SelectedIndexChanged += new System.EventHandler(this.mapBattleBGName_SelectedIndexChanged);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(6, 22);
            this.label71.Name = "label71";
            this.label71.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label71.Size = new System.Drawing.Size(57, 16);
            this.label71.TabIndex = 0;
            this.label71.Text = "Battlefield";
            // 
            // mapRandomBattles
            // 
            this.mapRandomBattles.Appearance = System.Windows.Forms.Appearance.Button;
            this.mapRandomBattles.BackColor = System.Drawing.SystemColors.Control;
            this.mapRandomBattles.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mapRandomBattles.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapRandomBattles.ForeColor = System.Drawing.Color.Gray;
            this.mapRandomBattles.Location = new System.Drawing.Point(127, 42);
            this.mapRandomBattles.Name = "mapRandomBattles";
            this.mapRandomBattles.Size = new System.Drawing.Size(126, 19);
            this.mapRandomBattles.TabIndex = 5;
            this.mapRandomBattles.Text = "RANDOM BATTLES";
            this.mapRandomBattles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mapRandomBattles.UseCompatibleTextRendering = true;
            this.mapRandomBattles.UseVisualStyleBackColor = false;
            this.mapRandomBattles.CheckedChanged += new System.EventHandler(this.mapRandomBattles_CheckedChanged);
            // 
            // mapBattleBG
            // 
            this.mapBattleBG.Location = new System.Drawing.Point(76, 20);
            this.mapBattleBG.Maximum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.mapBattleBG.Name = "mapBattleBG";
            this.mapBattleBG.Size = new System.Drawing.Size(50, 21);
            this.mapBattleBG.TabIndex = 1;
            this.mapBattleBG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapBattleBG.ValueChanged += new System.EventHandler(this.mapBattleBG_ValueChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 43);
            this.label38.Name = "label38";
            this.label38.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label38.Size = new System.Drawing.Size(64, 16);
            this.label38.TabIndex = 3;
            this.label38.Text = "Battle Zone";
            // 
            // mapBattleZone
            // 
            this.mapBattleZone.Location = new System.Drawing.Point(76, 41);
            this.mapBattleZone.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.mapBattleZone.Name = "mapBattleZone";
            this.mapBattleZone.Size = new System.Drawing.Size(50, 21);
            this.mapBattleZone.TabIndex = 4;
            this.mapBattleZone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapBattleZone.ValueChanged += new System.EventHandler(this.mapBattleZone_ValueChanged);
            // 
            // messageName
            // 
            this.messageName.BackColor = System.Drawing.SystemColors.Window;
            this.messageName.DropDownHeight = 301;
            this.messageName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.messageName.DropDownWidth = 250;
            this.messageName.IntegralHeight = false;
            this.messageName.Location = new System.Drawing.Point(61, 6);
            this.messageName.Name = "messageName";
            this.messageName.Size = new System.Drawing.Size(194, 21);
            this.messageName.TabIndex = 119;
            this.messageName.TabStop = false;
            this.messageName.SelectedIndexChanged += new System.EventHandler(this.layerMessageBox_SelectedIndexChanged);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.layerEffects);
            this.groupBox10.Controls.Add(this.label1);
            this.groupBox10.Controls.Add(this.windowMask);
            this.groupBox10.Location = new System.Drawing.Point(0, 518);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(260, 106);
            this.groupBox10.TabIndex = 7;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Layer Effects";
            // 
            // layerEffects
            // 
            this.layerEffects.CheckOnClick = true;
            this.layerEffects.FormattingEnabled = true;
            this.layerEffects.Items.AddRange(new object[] {
            "heat wave L1",
            "heat wave L2",
            "heat wave L3",
            "can use warp",
            "can use X-Zone warp",
            "searchlights enabled"});
            this.layerEffects.Location = new System.Drawing.Point(6, 47);
            this.layerEffects.MultiColumn = true;
            this.layerEffects.Name = "layerEffects";
            this.layerEffects.Size = new System.Drawing.Size(248, 52);
            this.layerEffects.TabIndex = 0;
            this.layerEffects.SelectedIndexChanged += new System.EventHandler(this.layerEffects_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 139;
            this.label1.Text = "Window Mask";
            // 
            // windowMask
            // 
            this.windowMask.BackColor = System.Drawing.SystemColors.Window;
            this.windowMask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.windowMask.Items.AddRange(new object[] {
            "default",
            "Imperial Camp spotlight",
            "Ebot\'s Rock spotlight",
            "searchlight/pyramid"});
            this.windowMask.Location = new System.Drawing.Point(127, 20);
            this.windowMask.Name = "windowMask";
            this.windowMask.Size = new System.Drawing.Size(127, 21);
            this.windowMask.TabIndex = 138;
            this.windowMask.SelectedIndexChanged += new System.EventHandler(this.windowMask_SelectedIndexChanged);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.BackColor = System.Drawing.SystemColors.Control;
            this.label53.Location = new System.Drawing.Point(6, 9);
            this.label53.Name = "label53";
            this.label53.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label53.Size = new System.Drawing.Size(51, 16);
            this.label53.TabIndex = 137;
            this.label53.Text = "Message";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.layerColorMathMode);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.layerColorMathIntensity);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label95);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.label96);
            this.groupBox5.Controls.Add(this.layerPrioritySet);
            this.groupBox5.Controls.Add(this.layerColorMathBG);
            this.groupBox5.Controls.Add(this.layerMainscreenL1);
            this.groupBox5.Controls.Add(this.layerColorMathNPC);
            this.groupBox5.Controls.Add(this.layerSubscreenL1);
            this.groupBox5.Controls.Add(this.layerSubscreenNPC);
            this.groupBox5.Controls.Add(this.layerColorMathL1);
            this.groupBox5.Controls.Add(this.layerMainscreenNPC);
            this.groupBox5.Controls.Add(this.layerMainscreenL2);
            this.groupBox5.Controls.Add(this.layerColorMathL3);
            this.groupBox5.Controls.Add(this.layerSubscreenL2);
            this.groupBox5.Controls.Add(this.layerSubscreenL3);
            this.groupBox5.Controls.Add(this.layerColorMathL2);
            this.groupBox5.Controls.Add(this.layerMainscreenL3);
            this.groupBox5.Location = new System.Drawing.Point(0, 84);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(260, 140);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Layer Properties";
            // 
            // layerColorMathMode
            // 
            this.layerColorMathMode.BackColor = System.Drawing.SystemColors.Window;
            this.layerColorMathMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerColorMathMode.Items.AddRange(new object[] {
            "Plus",
            "Minus"});
            this.layerColorMathMode.Location = new System.Drawing.Point(183, 112);
            this.layerColorMathMode.Name = "layerColorMathMode";
            this.layerColorMathMode.Size = new System.Drawing.Size(71, 21);
            this.layerColorMathMode.TabIndex = 119;
            this.layerColorMathMode.SelectedIndexChanged += new System.EventHandler(this.layerColorMathMode_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 22);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(60, 13);
            this.label32.TabIndex = 137;
            this.label32.Text = "Priority Set";
            // 
            // layerColorMathIntensity
            // 
            this.layerColorMathIntensity.BackColor = System.Drawing.SystemColors.Window;
            this.layerColorMathIntensity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerColorMathIntensity.Items.AddRange(new object[] {
            "Full",
            "Half"});
            this.layerColorMathIntensity.Location = new System.Drawing.Point(72, 112);
            this.layerColorMathIntensity.Name = "layerColorMathIntensity";
            this.layerColorMathIntensity.Size = new System.Drawing.Size(66, 21);
            this.layerColorMathIntensity.TabIndex = 119;
            this.layerColorMathIntensity.SelectedIndexChanged += new System.EventHandler(this.layerColorMathIntensity_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 90);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 13);
            this.label22.TabIndex = 176;
            this.label22.Text = "Color Math";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 48);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(61, 13);
            this.label20.TabIndex = 123;
            this.label20.Text = "Mainscreen";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(6, 115);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(50, 13);
            this.label95.TabIndex = 178;
            this.label95.Text = "Intensity";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 69);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 13);
            this.label21.TabIndex = 138;
            this.label21.Text = "Subscreen";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(144, 115);
            this.label96.Name = "label96";
            this.label96.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label96.Size = new System.Drawing.Size(35, 16);
            this.label96.TabIndex = 177;
            this.label96.Text = "Mode";
            // 
            // layerPrioritySet
            // 
            this.layerPrioritySet.Location = new System.Drawing.Point(72, 20);
            this.layerPrioritySet.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.layerPrioritySet.Name = "layerPrioritySet";
            this.layerPrioritySet.Size = new System.Drawing.Size(71, 21);
            this.layerPrioritySet.TabIndex = 8;
            this.layerPrioritySet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerPrioritySet.ValueChanged += new System.EventHandler(this.layerPrioritySet_ValueChanged);
            // 
            // layerColorMathBG
            // 
            this.layerColorMathBG.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathBG.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathBG.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathBG.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathBG.Location = new System.Drawing.Point(217, 87);
            this.layerColorMathBG.Name = "layerColorMathBG";
            this.layerColorMathBG.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathBG.TabIndex = 24;
            this.layerColorMathBG.Text = "BG";
            this.layerColorMathBG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathBG.UseCompatibleTextRendering = true;
            this.layerColorMathBG.UseVisualStyleBackColor = false;
            this.layerColorMathBG.CheckedChanged += new System.EventHandler(this.layerColorMathBG_CheckedChanged);
            // 
            // layerMainscreenL1
            // 
            this.layerMainscreenL1.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerMainscreenL1.BackColor = System.Drawing.SystemColors.Control;
            this.layerMainscreenL1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerMainscreenL1.ForeColor = System.Drawing.Color.Gray;
            this.layerMainscreenL1.Location = new System.Drawing.Point(73, 45);
            this.layerMainscreenL1.Name = "layerMainscreenL1";
            this.layerMainscreenL1.Size = new System.Drawing.Size(35, 21);
            this.layerMainscreenL1.TabIndex = 10;
            this.layerMainscreenL1.Text = "L1";
            this.layerMainscreenL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerMainscreenL1.UseCompatibleTextRendering = true;
            this.layerMainscreenL1.UseVisualStyleBackColor = false;
            this.layerMainscreenL1.CheckedChanged += new System.EventHandler(this.layerMainscreenL1_CheckedChanged);
            // 
            // layerColorMathNPC
            // 
            this.layerColorMathNPC.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathNPC.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathNPC.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathNPC.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathNPC.Location = new System.Drawing.Point(181, 87);
            this.layerColorMathNPC.Name = "layerColorMathNPC";
            this.layerColorMathNPC.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathNPC.TabIndex = 23;
            this.layerColorMathNPC.Text = "NPC";
            this.layerColorMathNPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathNPC.UseCompatibleTextRendering = true;
            this.layerColorMathNPC.UseVisualStyleBackColor = false;
            this.layerColorMathNPC.CheckedChanged += new System.EventHandler(this.layerColorMathNPC_CheckedChanged);
            // 
            // layerSubscreenL1
            // 
            this.layerSubscreenL1.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerSubscreenL1.BackColor = System.Drawing.SystemColors.Control;
            this.layerSubscreenL1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerSubscreenL1.ForeColor = System.Drawing.Color.Gray;
            this.layerSubscreenL1.Location = new System.Drawing.Point(73, 66);
            this.layerSubscreenL1.Name = "layerSubscreenL1";
            this.layerSubscreenL1.Size = new System.Drawing.Size(35, 21);
            this.layerSubscreenL1.TabIndex = 15;
            this.layerSubscreenL1.Text = "L1";
            this.layerSubscreenL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerSubscreenL1.UseCompatibleTextRendering = true;
            this.layerSubscreenL1.UseVisualStyleBackColor = false;
            this.layerSubscreenL1.CheckedChanged += new System.EventHandler(this.layerSubscreenL1_CheckedChanged);
            // 
            // layerSubscreenNPC
            // 
            this.layerSubscreenNPC.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerSubscreenNPC.BackColor = System.Drawing.SystemColors.Control;
            this.layerSubscreenNPC.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerSubscreenNPC.ForeColor = System.Drawing.Color.Gray;
            this.layerSubscreenNPC.Location = new System.Drawing.Point(181, 66);
            this.layerSubscreenNPC.Name = "layerSubscreenNPC";
            this.layerSubscreenNPC.Size = new System.Drawing.Size(35, 21);
            this.layerSubscreenNPC.TabIndex = 18;
            this.layerSubscreenNPC.Text = "NPC";
            this.layerSubscreenNPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerSubscreenNPC.UseCompatibleTextRendering = true;
            this.layerSubscreenNPC.UseVisualStyleBackColor = false;
            this.layerSubscreenNPC.CheckedChanged += new System.EventHandler(this.layerSubscreenNPC_CheckedChanged);
            // 
            // layerColorMathL1
            // 
            this.layerColorMathL1.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathL1.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathL1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathL1.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathL1.Location = new System.Drawing.Point(73, 87);
            this.layerColorMathL1.Name = "layerColorMathL1";
            this.layerColorMathL1.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathL1.TabIndex = 20;
            this.layerColorMathL1.Text = "L1";
            this.layerColorMathL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathL1.UseCompatibleTextRendering = true;
            this.layerColorMathL1.UseVisualStyleBackColor = false;
            this.layerColorMathL1.CheckedChanged += new System.EventHandler(this.layerColorMathL1_CheckedChanged);
            // 
            // layerMainscreenNPC
            // 
            this.layerMainscreenNPC.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerMainscreenNPC.BackColor = System.Drawing.SystemColors.Control;
            this.layerMainscreenNPC.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerMainscreenNPC.ForeColor = System.Drawing.Color.Gray;
            this.layerMainscreenNPC.Location = new System.Drawing.Point(181, 45);
            this.layerMainscreenNPC.Name = "layerMainscreenNPC";
            this.layerMainscreenNPC.Size = new System.Drawing.Size(35, 21);
            this.layerMainscreenNPC.TabIndex = 13;
            this.layerMainscreenNPC.Text = "NPC";
            this.layerMainscreenNPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerMainscreenNPC.UseCompatibleTextRendering = true;
            this.layerMainscreenNPC.UseVisualStyleBackColor = false;
            this.layerMainscreenNPC.CheckedChanged += new System.EventHandler(this.layerMainscreenNPC_CheckedChanged);
            // 
            // layerMainscreenL2
            // 
            this.layerMainscreenL2.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerMainscreenL2.BackColor = System.Drawing.SystemColors.Control;
            this.layerMainscreenL2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerMainscreenL2.ForeColor = System.Drawing.Color.Gray;
            this.layerMainscreenL2.Location = new System.Drawing.Point(109, 45);
            this.layerMainscreenL2.Name = "layerMainscreenL2";
            this.layerMainscreenL2.Size = new System.Drawing.Size(35, 21);
            this.layerMainscreenL2.TabIndex = 11;
            this.layerMainscreenL2.Text = "L2";
            this.layerMainscreenL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerMainscreenL2.UseCompatibleTextRendering = true;
            this.layerMainscreenL2.UseVisualStyleBackColor = false;
            this.layerMainscreenL2.CheckedChanged += new System.EventHandler(this.layerMainscreenL2_CheckedChanged);
            // 
            // layerColorMathL3
            // 
            this.layerColorMathL3.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathL3.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathL3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathL3.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathL3.Location = new System.Drawing.Point(145, 87);
            this.layerColorMathL3.Name = "layerColorMathL3";
            this.layerColorMathL3.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathL3.TabIndex = 22;
            this.layerColorMathL3.Text = "L3";
            this.layerColorMathL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathL3.UseCompatibleTextRendering = true;
            this.layerColorMathL3.UseVisualStyleBackColor = false;
            this.layerColorMathL3.CheckedChanged += new System.EventHandler(this.layerColorMathL3_CheckedChanged);
            // 
            // layerSubscreenL2
            // 
            this.layerSubscreenL2.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerSubscreenL2.BackColor = System.Drawing.SystemColors.Control;
            this.layerSubscreenL2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerSubscreenL2.ForeColor = System.Drawing.Color.Gray;
            this.layerSubscreenL2.Location = new System.Drawing.Point(109, 66);
            this.layerSubscreenL2.Name = "layerSubscreenL2";
            this.layerSubscreenL2.Size = new System.Drawing.Size(35, 21);
            this.layerSubscreenL2.TabIndex = 16;
            this.layerSubscreenL2.Text = "L2";
            this.layerSubscreenL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerSubscreenL2.UseCompatibleTextRendering = true;
            this.layerSubscreenL2.UseVisualStyleBackColor = false;
            this.layerSubscreenL2.CheckedChanged += new System.EventHandler(this.layerSubscreenL2_CheckedChanged);
            // 
            // layerSubscreenL3
            // 
            this.layerSubscreenL3.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerSubscreenL3.BackColor = System.Drawing.SystemColors.Control;
            this.layerSubscreenL3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerSubscreenL3.ForeColor = System.Drawing.Color.Gray;
            this.layerSubscreenL3.Location = new System.Drawing.Point(145, 66);
            this.layerSubscreenL3.Name = "layerSubscreenL3";
            this.layerSubscreenL3.Size = new System.Drawing.Size(35, 21);
            this.layerSubscreenL3.TabIndex = 17;
            this.layerSubscreenL3.Text = "L3";
            this.layerSubscreenL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerSubscreenL3.UseCompatibleTextRendering = true;
            this.layerSubscreenL3.UseVisualStyleBackColor = false;
            this.layerSubscreenL3.CheckedChanged += new System.EventHandler(this.layerSubscreenL3_CheckedChanged);
            // 
            // layerColorMathL2
            // 
            this.layerColorMathL2.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathL2.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathL2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathL2.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathL2.Location = new System.Drawing.Point(109, 87);
            this.layerColorMathL2.Name = "layerColorMathL2";
            this.layerColorMathL2.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathL2.TabIndex = 21;
            this.layerColorMathL2.Text = "L2";
            this.layerColorMathL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathL2.UseCompatibleTextRendering = true;
            this.layerColorMathL2.UseVisualStyleBackColor = false;
            this.layerColorMathL2.CheckedChanged += new System.EventHandler(this.layerColorMathL2_CheckedChanged);
            // 
            // layerMainscreenL3
            // 
            this.layerMainscreenL3.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerMainscreenL3.BackColor = System.Drawing.SystemColors.Control;
            this.layerMainscreenL3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerMainscreenL3.ForeColor = System.Drawing.Color.Gray;
            this.layerMainscreenL3.Location = new System.Drawing.Point(145, 45);
            this.layerMainscreenL3.Name = "layerMainscreenL3";
            this.layerMainscreenL3.Size = new System.Drawing.Size(35, 21);
            this.layerMainscreenL3.TabIndex = 12;
            this.layerMainscreenL3.Text = "L3";
            this.layerMainscreenL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerMainscreenL3.UseCompatibleTextRendering = true;
            this.layerMainscreenL3.UseVisualStyleBackColor = false;
            this.layerMainscreenL3.CheckedChanged += new System.EventHandler(this.layerMainscreenL3_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.l1width);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.label3);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.l2width);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.l3width);
            this.groupBox8.Controls.Add(this.l3height);
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Controls.Add(this.l2height);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.l1height);
            this.groupBox8.Location = new System.Drawing.Point(0, 353);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(260, 88);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Layer Dimensions";
            // 
            // l1width
            // 
            this.l1width.BackColor = System.Drawing.SystemColors.Window;
            this.l1width.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.l1width.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048"});
            this.l1width.Location = new System.Drawing.Point(30, 19);
            this.l1width.Name = "l1width";
            this.l1width.Size = new System.Drawing.Size(55, 21);
            this.l1width.TabIndex = 0;
            this.l1width.SelectedIndexChanged += new System.EventHandler(this.l1width_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "L1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "x";
            // 
            // l2width
            // 
            this.l2width.BackColor = System.Drawing.SystemColors.Window;
            this.l2width.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.l2width.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048"});
            this.l2width.Location = new System.Drawing.Point(30, 40);
            this.l2width.Name = "l2width";
            this.l2width.Size = new System.Drawing.Size(55, 21);
            this.l2width.TabIndex = 0;
            this.l2width.SelectedIndexChanged += new System.EventHandler(this.l2width_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "L3";
            // 
            // l3width
            // 
            this.l3width.BackColor = System.Drawing.SystemColors.Window;
            this.l3width.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.l3width.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048"});
            this.l3width.Location = new System.Drawing.Point(30, 61);
            this.l3width.Name = "l3width";
            this.l3width.Size = new System.Drawing.Size(55, 21);
            this.l3width.TabIndex = 0;
            this.l3width.SelectedIndexChanged += new System.EventHandler(this.l3width_SelectedIndexChanged);
            // 
            // l3height
            // 
            this.l3height.BackColor = System.Drawing.SystemColors.Window;
            this.l3height.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.l3height.Items.AddRange(new object[] {
            "256",
            "512",
            "1024"});
            this.l3height.Location = new System.Drawing.Point(102, 61);
            this.l3height.Name = "l3height";
            this.l3height.Size = new System.Drawing.Size(55, 21);
            this.l3height.TabIndex = 0;
            this.l3height.SelectedIndexChanged += new System.EventHandler(this.l3height_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(87, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "x";
            // 
            // l2height
            // 
            this.l2height.BackColor = System.Drawing.SystemColors.Window;
            this.l2height.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.l2height.Items.AddRange(new object[] {
            "256",
            "512",
            "1024"});
            this.l2height.Location = new System.Drawing.Point(102, 40);
            this.l2height.Name = "l2height";
            this.l2height.Size = new System.Drawing.Size(55, 21);
            this.l2height.TabIndex = 0;
            this.l2height.SelectedIndexChanged += new System.EventHandler(this.l2height_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "L2";
            // 
            // l1height
            // 
            this.l1height.BackColor = System.Drawing.SystemColors.Window;
            this.l1height.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.l1height.Items.AddRange(new object[] {
            "256",
            "512",
            "1024"});
            this.l1height.Location = new System.Drawing.Point(102, 19);
            this.l1height.Name = "l1height";
            this.l1height.Size = new System.Drawing.Size(55, 21);
            this.l1height.TabIndex = 0;
            this.l1height.SelectedIndexChanged += new System.EventHandler(this.l1height_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.spriteMask);
            this.groupBox6.Controls.Add(this.label26);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.topSync);
            this.groupBox6.Controls.Add(this.layerMaskHighY);
            this.groupBox6.Controls.Add(this.layerMaskHighX);
            this.groupBox6.Location = new System.Drawing.Point(0, 228);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(260, 69);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Layer Mask";
            // 
            // spriteMask
            // 
            this.spriteMask.Location = new System.Drawing.Point(204, 41);
            this.spriteMask.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spriteMask.Name = "spriteMask";
            this.spriteMask.Size = new System.Drawing.Size(50, 21);
            this.spriteMask.TabIndex = 2;
            this.spriteMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spriteMask.ValueChanged += new System.EventHandler(this.spriteMask_ValueChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 44);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(46, 13);
            this.label26.TabIndex = 101;
            this.label26.Text = "Scrolling";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 23);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(59, 13);
            this.label25.TabIndex = 96;
            this.label25.Text = "Right Edge";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(130, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 100;
            this.label7.Text = "Sprite Mask";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(130, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 13);
            this.label15.TabIndex = 100;
            this.label15.Text = "Bottom Edge";
            // 
            // topSync
            // 
            this.topSync.Location = new System.Drawing.Point(73, 41);
            this.topSync.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.topSync.Name = "topSync";
            this.topSync.Size = new System.Drawing.Size(50, 21);
            this.topSync.TabIndex = 102;
            this.topSync.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.topSync.ValueChanged += new System.EventHandler(this.topSync_ValueChanged);
            // 
            // layerMaskHighY
            // 
            this.layerMaskHighY.Location = new System.Drawing.Point(204, 20);
            this.layerMaskHighY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.layerMaskHighY.Name = "layerMaskHighY";
            this.layerMaskHighY.Size = new System.Drawing.Size(50, 21);
            this.layerMaskHighY.TabIndex = 28;
            this.layerMaskHighY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerMaskHighY.ValueChanged += new System.EventHandler(this.layerMaskHighY_ValueChanged);
            // 
            // layerMaskHighX
            // 
            this.layerMaskHighX.Location = new System.Drawing.Point(73, 20);
            this.layerMaskHighX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.layerMaskHighX.Name = "layerMaskHighX";
            this.layerMaskHighX.Size = new System.Drawing.Size(50, 21);
            this.layerMaskHighX.TabIndex = 27;
            this.layerMaskHighX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerMaskHighX.ValueChanged += new System.EventHandler(this.layerMaskHighX_ValueChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.layerL2LeftShift);
            this.groupBox7.Controls.Add(this.label23);
            this.groupBox7.Controls.Add(this.layerL2UpShift);
            this.groupBox7.Controls.Add(this.layerL3UpShift);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.layerL3LeftShift);
            this.groupBox7.Location = new System.Drawing.Point(0, 301);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(260, 48);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Layer -(X,Y) Shifting";
            // 
            // layerL2LeftShift
            // 
            this.layerL2LeftShift.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerL2LeftShift.Location = new System.Drawing.Point(30, 20);
            this.layerL2LeftShift.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.layerL2LeftShift.Name = "layerL2LeftShift";
            this.layerL2LeftShift.Size = new System.Drawing.Size(44, 21);
            this.layerL2LeftShift.TabIndex = 31;
            this.layerL2LeftShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerL2LeftShift.ValueChanged += new System.EventHandler(this.layerL2LeftShift_ValueChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 24);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(18, 13);
            this.label23.TabIndex = 82;
            this.label23.Text = "L2";
            // 
            // layerL2UpShift
            // 
            this.layerL2UpShift.Location = new System.Drawing.Point(74, 20);
            this.layerL2UpShift.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.layerL2UpShift.Name = "layerL2UpShift";
            this.layerL2UpShift.Size = new System.Drawing.Size(44, 21);
            this.layerL2UpShift.TabIndex = 33;
            this.layerL2UpShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerL2UpShift.ValueChanged += new System.EventHandler(this.layerL2UpShift_ValueChanged);
            // 
            // layerL3UpShift
            // 
            this.layerL3UpShift.Location = new System.Drawing.Point(192, 20);
            this.layerL3UpShift.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.layerL3UpShift.Name = "layerL3UpShift";
            this.layerL3UpShift.Size = new System.Drawing.Size(44, 21);
            this.layerL3UpShift.TabIndex = 34;
            this.layerL3UpShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerL3UpShift.ValueChanged += new System.EventHandler(this.layerL3UpShift_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(124, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 85;
            this.label9.Text = "L3";
            // 
            // layerL3LeftShift
            // 
            this.layerL3LeftShift.Location = new System.Drawing.Point(148, 20);
            this.layerL3LeftShift.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.layerL3LeftShift.Name = "layerL3LeftShift";
            this.layerL3LeftShift.Size = new System.Drawing.Size(44, 21);
            this.layerL3LeftShift.TabIndex = 32;
            this.layerL3LeftShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerL3LeftShift.ValueChanged += new System.EventHandler(this.layerL3LeftShift_ValueChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel8);
            this.tabPage1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(260, 661);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "TREASURES";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.groupBox19);
            this.panel8.Controls.Add(this.groupBox20);
            this.panel8.Controls.Add(this.treasureListBox);
            this.panel8.Controls.Add(this.toolStrip4);
            this.panel8.Controls.Add(this.treasuresBytesLeft);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(260, 661);
            this.panel8.TabIndex = 1;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.treasurePropertyName);
            this.groupBox19.Controls.Add(this.treasureXCoord);
            this.groupBox19.Controls.Add(this.treasureType);
            this.groupBox19.Controls.Add(this.label64);
            this.groupBox19.Controls.Add(this.treasureYCoord);
            this.groupBox19.Controls.Add(this.label67);
            this.groupBox19.Controls.Add(this.label83);
            this.groupBox19.Controls.Add(this.treasurePropertyNum);
            this.groupBox19.Controls.Add(this.label84);
            this.groupBox19.Controls.Add(this.label73);
            this.groupBox19.Location = new System.Drawing.Point(128, 28);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(132, 131);
            this.groupBox19.TabIndex = 495;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Treasure Properties";
            // 
            // treasurePropertyName
            // 
            this.treasurePropertyName.DropDownHeight = 392;
            this.treasurePropertyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.treasurePropertyName.DropDownWidth = 200;
            this.treasurePropertyName.IntegralHeight = false;
            this.treasurePropertyName.Location = new System.Drawing.Point(71, 104);
            this.treasurePropertyName.Name = "treasurePropertyName";
            this.treasurePropertyName.Size = new System.Drawing.Size(55, 21);
            this.treasurePropertyName.TabIndex = 0;
            this.treasurePropertyName.SelectedIndexChanged += new System.EventHandler(this.treasurePropertyName_SelectedIndexChanged);
            // 
            // treasureXCoord
            // 
            this.treasureXCoord.Location = new System.Drawing.Point(71, 20);
            this.treasureXCoord.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.treasureXCoord.Name = "treasureXCoord";
            this.treasureXCoord.Size = new System.Drawing.Size(55, 21);
            this.treasureXCoord.TabIndex = 485;
            this.treasureXCoord.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.treasureXCoord.ValueChanged += new System.EventHandler(this.treasureXCoord_ValueChanged);
            // 
            // treasureType
            // 
            this.treasureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.treasureType.DropDownWidth = 96;
            this.treasureType.Items.AddRange(new object[] {
            "empty",
            "empty",
            "monster-in-a-box",
            "item",
            "GP"});
            this.treasureType.Location = new System.Drawing.Point(71, 62);
            this.treasureType.Name = "treasureType";
            this.treasureType.Size = new System.Drawing.Size(55, 21);
            this.treasureType.TabIndex = 0;
            this.treasureType.SelectedIndexChanged += new System.EventHandler(this.treasureType_SelectedIndexChanged);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(6, 23);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(45, 13);
            this.label64.TabIndex = 463;
            this.label64.Text = "X Coord";
            // 
            // treasureYCoord
            // 
            this.treasureYCoord.Location = new System.Drawing.Point(71, 41);
            this.treasureYCoord.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.treasureYCoord.Name = "treasureYCoord";
            this.treasureYCoord.Size = new System.Drawing.Size(55, 21);
            this.treasureYCoord.TabIndex = 486;
            this.treasureYCoord.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.treasureYCoord.ValueChanged += new System.EventHandler(this.treasureYCoord_ValueChanged);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(6, 44);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(45, 13);
            this.label67.TabIndex = 464;
            this.label67.Text = "Y Coord";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(6, 107);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(19, 13);
            this.label83.TabIndex = 483;
            this.label83.Text = "...";
            // 
            // treasurePropertyNum
            // 
            this.treasurePropertyNum.Location = new System.Drawing.Point(71, 83);
            this.treasurePropertyNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.treasurePropertyNum.Name = "treasurePropertyNum";
            this.treasurePropertyNum.Size = new System.Drawing.Size(55, 21);
            this.treasurePropertyNum.TabIndex = 480;
            this.treasurePropertyNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.treasurePropertyNum.ValueChanged += new System.EventHandler(this.treasurePropertyNum_ValueChanged);
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(6, 86);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(19, 13);
            this.label84.TabIndex = 484;
            this.label84.Text = "...";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(6, 65);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(31, 13);
            this.label73.TabIndex = 482;
            this.label73.Text = "Type";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.treasureCheckMem);
            this.groupBox20.Controls.Add(this.label36);
            this.groupBox20.Controls.Add(this.treasureCheckBit);
            this.groupBox20.Controls.Add(this.label37);
            this.groupBox20.Location = new System.Drawing.Point(128, 165);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(132, 68);
            this.groupBox20.TabIndex = 495;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Show if memory set";
            // 
            // treasureCheckMem
            // 
            this.treasureCheckMem.Hexadecimal = true;
            this.treasureCheckMem.Location = new System.Drawing.Point(71, 20);
            this.treasureCheckMem.Maximum = new decimal(new int[] {
            7807,
            0,
            0,
            0});
            this.treasureCheckMem.Minimum = new decimal(new int[] {
            7712,
            0,
            0,
            0});
            this.treasureCheckMem.Name = "treasureCheckMem";
            this.treasureCheckMem.Size = new System.Drawing.Size(55, 21);
            this.treasureCheckMem.TabIndex = 494;
            this.treasureCheckMem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.treasureCheckMem.Value = new decimal(new int[] {
            7792,
            0,
            0,
            0});
            this.treasureCheckMem.ValueChanged += new System.EventHandler(this.treasureCheckMem_ValueChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 23);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(46, 13);
            this.label36.TabIndex = 492;
            this.label36.Text = "Address";
            // 
            // treasureCheckBit
            // 
            this.treasureCheckBit.Location = new System.Drawing.Point(71, 41);
            this.treasureCheckBit.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.treasureCheckBit.Name = "treasureCheckBit";
            this.treasureCheckBit.Size = new System.Drawing.Size(55, 21);
            this.treasureCheckBit.TabIndex = 495;
            this.treasureCheckBit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.treasureCheckBit.ValueChanged += new System.EventHandler(this.treasureCheckBit_ValueChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 44);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(19, 13);
            this.label37.TabIndex = 491;
            this.label37.Text = "Bit";
            // 
            // treasureListBox
            // 
            this.treasureListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treasureListBox.FormattingEnabled = true;
            this.treasureListBox.IntegralHeight = false;
            this.treasureListBox.Location = new System.Drawing.Point(0, 51);
            this.treasureListBox.Name = "treasureListBox";
            this.treasureListBox.Size = new System.Drawing.Size(126, 610);
            this.treasureListBox.TabIndex = 497;
            this.treasureListBox.SelectedIndexChanged += new System.EventHandler(this.treasureListBox_SelectedIndexChanged);
            // 
            // toolStrip4
            // 
            this.toolStrip4.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip4.CanOverflow = false;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonInsertTreasure,
            this.buttonDeleteTreasure,
            this.toolStripSeparator9,
            this.treasureMoveUp,
            this.treasureMoveDown,
            this.toolStripSeparator11,
            this.treasureCopy,
            this.treasurePaste,
            this.treasureDuplicate});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip4.Size = new System.Drawing.Size(260, 25);
            this.toolStrip4.TabIndex = 486;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // buttonInsertTreasure
            // 
            this.buttonInsertTreasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonInsertTreasure.Image = global::ZONEDOCTOR.Properties.Resources.new_small;
            this.buttonInsertTreasure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonInsertTreasure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonInsertTreasure.Name = "buttonInsertTreasure";
            this.buttonInsertTreasure.Size = new System.Drawing.Size(23, 22);
            this.buttonInsertTreasure.Text = "New NPC";
            this.buttonInsertTreasure.Click += new System.EventHandler(this.buttonInsertTreasure_Click);
            // 
            // buttonDeleteTreasure
            // 
            this.buttonDeleteTreasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDeleteTreasure.Image = global::ZONEDOCTOR.Properties.Resources.delete_small;
            this.buttonDeleteTreasure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonDeleteTreasure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDeleteTreasure.Name = "buttonDeleteTreasure";
            this.buttonDeleteTreasure.Size = new System.Drawing.Size(23, 22);
            this.buttonDeleteTreasure.Text = "Delete NPC";
            this.buttonDeleteTreasure.Click += new System.EventHandler(this.buttonDeleteTreasure_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // treasureMoveUp
            // 
            this.treasureMoveUp.AutoSize = false;
            this.treasureMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.treasureMoveUp.Image = global::ZONEDOCTOR.Properties.Resources.moveup;
            this.treasureMoveUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.treasureMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.treasureMoveUp.Name = "treasureMoveUp";
            this.treasureMoveUp.Size = new System.Drawing.Size(22, 22);
            this.treasureMoveUp.Text = "Move NPC Up";
            this.treasureMoveUp.Click += new System.EventHandler(this.treasureMoveUp_Click);
            // 
            // treasureMoveDown
            // 
            this.treasureMoveDown.AutoSize = false;
            this.treasureMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.treasureMoveDown.Image = global::ZONEDOCTOR.Properties.Resources.movedown;
            this.treasureMoveDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.treasureMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.treasureMoveDown.Name = "treasureMoveDown";
            this.treasureMoveDown.Size = new System.Drawing.Size(22, 22);
            this.treasureMoveDown.Text = "Move NPC Down";
            this.treasureMoveDown.Click += new System.EventHandler(this.treasureMoveDown_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // treasureCopy
            // 
            this.treasureCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.treasureCopy.Image = global::ZONEDOCTOR.Properties.Resources.copy_small;
            this.treasureCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.treasureCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.treasureCopy.Name = "treasureCopy";
            this.treasureCopy.Size = new System.Drawing.Size(23, 22);
            this.treasureCopy.Text = "Copy NPC";
            this.treasureCopy.Click += new System.EventHandler(this.treasureCopy_Click);
            // 
            // treasurePaste
            // 
            this.treasurePaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.treasurePaste.Image = global::ZONEDOCTOR.Properties.Resources.paste_small;
            this.treasurePaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.treasurePaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.treasurePaste.Name = "treasurePaste";
            this.treasurePaste.Size = new System.Drawing.Size(23, 22);
            this.treasurePaste.Text = "Paste NPC";
            this.treasurePaste.Click += new System.EventHandler(this.treasurePaste_Click);
            // 
            // treasureDuplicate
            // 
            this.treasureDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.treasureDuplicate.Image = global::ZONEDOCTOR.Properties.Resources.duplicate_small;
            this.treasureDuplicate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.treasureDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.treasureDuplicate.Name = "treasureDuplicate";
            this.treasureDuplicate.Size = new System.Drawing.Size(23, 22);
            this.treasureDuplicate.Text = "Duplicate NPC";
            this.treasureDuplicate.Click += new System.EventHandler(this.treasureDuplicate_Click);
            // 
            // treasuresBytesLeft
            // 
            this.treasuresBytesLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.treasuresBytesLeft.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treasuresBytesLeft.Location = new System.Drawing.Point(0, 28);
            this.treasuresBytesLeft.Name = "treasuresBytesLeft";
            this.treasuresBytesLeft.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.treasuresBytesLeft.Size = new System.Drawing.Size(126, 21);
            this.treasuresBytesLeft.TabIndex = 463;
            this.treasuresBytesLeft.Text = "bytes left";
            this.treasuresBytesLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locationName,
            this.locationNum,
            this.navigateBck,
            this.navigateFwd,
            this.loadingLocation,
            this.loadingLocationLabel,
            this.toolStripSeparator19,
            this.searchBox,
            this.searchLocationNames,
            this.toolStripSeparator3,
            this.buttonGotoC,
            this.entranceEvent,
            this.toolStripSeparator16,
            this.toolStripLabel2,
            this.musicName,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1010, 25);
            this.toolStrip1.TabIndex = 2;
            // 
            // navigateBck
            // 
            this.navigateBck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigateBck.Enabled = false;
            this.navigateBck.Image = global::ZONEDOCTOR.Properties.Resources.back;
            this.navigateBck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.navigateBck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigateBck.Name = "navigateBck";
            this.navigateBck.Size = new System.Drawing.Size(23, 22);
            this.navigateBck.ToolTipText = "Navigate Backward";
            this.navigateBck.Click += new System.EventHandler(this.navigateBck_Click);
            // 
            // navigateFwd
            // 
            this.navigateFwd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigateFwd.Enabled = false;
            this.navigateFwd.Image = global::ZONEDOCTOR.Properties.Resources.foward;
            this.navigateFwd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.navigateFwd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigateFwd.Name = "navigateFwd";
            this.navigateFwd.Size = new System.Drawing.Size(23, 22);
            this.navigateFwd.ToolTipText = "Navigate Forward";
            this.navigateFwd.Click += new System.EventHandler(this.navigateFwd_Click);
            // 
            // loadingLocation
            // 
            this.loadingLocation.Maximum = 512;
            this.loadingLocation.Name = "loadingLocation";
            this.loadingLocation.Size = new System.Drawing.Size(400, 24);
            this.loadingLocation.Step = 1;
            this.loadingLocation.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.loadingLocation.Visible = false;
            // 
            // loadingLocationLabel
            // 
            this.loadingLocationLabel.Name = "loadingLocationLabel";
            this.loadingLocationLabel.Size = new System.Drawing.Size(0, 22);
            this.loadingLocationLabel.Visible = false;
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 25);
            // 
            // searchBox
            // 
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(145, 25);
            // 
            // searchLocationNames
            // 
            this.searchLocationNames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchLocationNames.Image = global::ZONEDOCTOR.Properties.Resources.search;
            this.searchLocationNames.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchLocationNames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchLocationNames.Name = "searchLocationNames";
            this.searchLocationNames.Size = new System.Drawing.Size(23, 22);
            this.searchLocationNames.Text = "Search Location Names";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonGotoC
            // 
            this.buttonGotoC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonGotoC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonGotoC.Name = "buttonGotoC";
            this.buttonGotoC.Size = new System.Drawing.Size(52, 22);
            this.buttonGotoC.Text = "EVENT #";
            this.buttonGotoC.ToolTipText = "Click to edit event";
            this.buttonGotoC.Click += new System.EventHandler(this.buttonGotoC_Click);
            // 
            // entranceEvent
            // 
            this.entranceEvent.AutoSize = false;
            this.entranceEvent.ContextMenuStrip = null;
            this.entranceEvent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entranceEvent.Hexadecimal = true;
            this.entranceEvent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.entranceEvent.Location = new System.Drawing.Point(551, 2);
            this.entranceEvent.Maximum = new decimal(new int[] {
            262143,
            0,
            0,
            0});
            this.entranceEvent.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.entranceEvent.Name = "entranceEvent";
            this.entranceEvent.Size = new System.Drawing.Size(60, 21);
            this.entranceEvent.Text = "0";
            this.entranceEvent.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.entranceEvent.ValueChanged += new System.EventHandler(this.entranceEvent_ValueChanged);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel2.Text = "MUSIC";
            // 
            // musicName
            // 
            this.musicName.DropDownHeight = 300;
            this.musicName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.musicName.DropDownWidth = 300;
            this.musicName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.musicName.IntegralHeight = false;
            this.musicName.Name = "musicName";
            this.musicName.Size = new System.Drawing.Size(170, 25);
            this.musicName.SelectedIndexChanged += new System.EventHandler(this.musicName_SelectedIndexChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator4.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem6.Text = "toolStripMenuItem6";
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            // 
            // panelLocations
            // 
            this.panelLocations.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelLocations.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLocations.Controls.Add(this.tabControl);
            this.panelLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLocations.Location = new System.Drawing.Point(0, 50);
            this.panelLocations.Name = "panelLocations";
            this.panelLocations.Size = new System.Drawing.Size(1010, 691);
            this.panelLocations.TabIndex = 506;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.toolStripSeparator5,
            this.import,
            this.export,
            this.toolStripDropDownButton1,
            this.clear,
            this.toolStripSeparator6,
            this.toolStripDropDownButton2,
            this.help,
            this.baseConversion,
            this.hexEditor,
            this.toolStripSeparator15,
            this.propertiesButton,
            this.openTileset,
            this.openTilemap,
            this.toolStripSeparator1,
            this.openPaletteEditor,
            this.openGraphicEditor,
            this.toolStripSeparator2,
            this.openTemplates,
            this.openPreviewer,
            this.spaceAnalyzer,
            this.numeralsButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(1010, 25);
            this.toolStrip2.TabIndex = 507;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // save
            // 
            this.save.Image = global::ZONEDOCTOR.Properties.Resources.save_small;
            this.save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(23, 22);
            this.save.ToolTipText = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // import
            // 
            this.import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.importArchitectureToolStripMenuItem,
            this.toolStripSeparator30,
            this.arraysToolStripMenuItem1,
            this.graphicSetsToolStripMenuItem1});
            this.import.Image = global::ZONEDOCTOR.Properties.Resources.importData;
            this.import.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(27, 22);
            this.import.ToolTipText = "Import";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.importData;
            this.allToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.allToolStripMenuItem.Text = "Import Location Data...";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.importLocationDataAll_Click);
            // 
            // importArchitectureToolStripMenuItem
            // 
            this.importArchitectureToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.importBinary;
            this.importArchitectureToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importArchitectureToolStripMenuItem.Name = "importArchitectureToolStripMenuItem";
            this.importArchitectureToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.importArchitectureToolStripMenuItem.Text = "Import Architecture...";
            this.importArchitectureToolStripMenuItem.Click += new System.EventHandler(this.importArchitectureToolStripMenuItem_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(182, 6);
            // 
            // arraysToolStripMenuItem1
            // 
            this.arraysToolStripMenuItem1.Image = global::ZONEDOCTOR.Properties.Resources.importBinary;
            this.arraysToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.arraysToolStripMenuItem1.Name = "arraysToolStripMenuItem1";
            this.arraysToolStripMenuItem1.Size = new System.Drawing.Size(185, 22);
            this.arraysToolStripMenuItem1.Text = "Import Arrays...";
            this.arraysToolStripMenuItem1.Click += new System.EventHandler(this.arraysToolStripMenuItem1_Click);
            // 
            // graphicSetsToolStripMenuItem1
            // 
            this.graphicSetsToolStripMenuItem1.Image = global::ZONEDOCTOR.Properties.Resources.importBinary;
            this.graphicSetsToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.graphicSetsToolStripMenuItem1.Name = "graphicSetsToolStripMenuItem1";
            this.graphicSetsToolStripMenuItem1.Size = new System.Drawing.Size(185, 22);
            this.graphicSetsToolStripMenuItem1.Text = "Import Graphic Set...";
            this.graphicSetsToolStripMenuItem1.Click += new System.EventHandler(this.graphicSetsToolStripMenuItem1_Click);
            // 
            // export
            // 
            this.export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.exportArchitectureToolStripMenuItem,
            this.toolStripSeparator28,
            this.arraysToolStripMenuItem,
            this.graphicSetsToolStripMenuItem,
            this.exportLocationImagesToolStripMenuItem1});
            this.export.Image = global::ZONEDOCTOR.Properties.Resources.exportData;
            this.export.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(27, 22);
            this.export.ToolTipText = "Export";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::ZONEDOCTOR.Properties.Resources.exportData;
            this.toolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(197, 22);
            this.toolStripMenuItem1.Text = "Export Location Data...";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.exportLocationDataAll_Click);
            // 
            // exportArchitectureToolStripMenuItem
            // 
            this.exportArchitectureToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.exportBinary;
            this.exportArchitectureToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportArchitectureToolStripMenuItem.Name = "exportArchitectureToolStripMenuItem";
            this.exportArchitectureToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exportArchitectureToolStripMenuItem.Text = "Export Architecture...";
            this.exportArchitectureToolStripMenuItem.Click += new System.EventHandler(this.exportArchitectureToolStripMenuItem_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(194, 6);
            // 
            // arraysToolStripMenuItem
            // 
            this.arraysToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.exportBinary;
            this.arraysToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.arraysToolStripMenuItem.Name = "arraysToolStripMenuItem";
            this.arraysToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.arraysToolStripMenuItem.Text = "Export Arrays...";
            this.arraysToolStripMenuItem.Click += new System.EventHandler(this.arraysToolStripMenuItem_Click);
            // 
            // graphicSetsToolStripMenuItem
            // 
            this.graphicSetsToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.exportBinary;
            this.graphicSetsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.graphicSetsToolStripMenuItem.Name = "graphicSetsToolStripMenuItem";
            this.graphicSetsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.graphicSetsToolStripMenuItem.Text = "Export Graphic Sets...";
            this.graphicSetsToolStripMenuItem.Click += new System.EventHandler(this.graphicSetsToolStripMenuItem_Click);
            // 
            // exportLocationImagesToolStripMenuItem1
            // 
            this.exportLocationImagesToolStripMenuItem1.Image = global::ZONEDOCTOR.Properties.Resources.exportImage;
            this.exportLocationImagesToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportLocationImagesToolStripMenuItem1.Name = "exportLocationImagesToolStripMenuItem1";
            this.exportLocationImagesToolStripMenuItem1.Size = new System.Drawing.Size(197, 22);
            this.exportLocationImagesToolStripMenuItem1.Text = "Export Location Images...";
            this.exportLocationImagesToolStripMenuItem1.Click += new System.EventHandler(this.exportLocationImagesAll_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetLocationMapToolStripMenuItem,
            this.resetNPCDataToolStripMenuItem,
            this.resetTreasuresToolStripMenuItem,
            this.resetEventDataToolStripMenuItem,
            this.resetExitDataToolStripMenuItem,
            this.toolStripSeparator17,
            this.resetPaletteSetToolStripMenuItem,
            this.resetGraphicSetToolStripMenuItem,
            this.resetTilesetsToolStripMenuItem,
            this.resetTilemapsToolStripMenuItem,
            this.resetSoliditySetToolStripMenuItem,
            this.toolStripSeparator20,
            this.resetAnimatedGraphicsToolStripMenuItem,
            this.resetAllComponentsToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(27, 22);
            // 
            // resetLocationMapToolStripMenuItem
            // 
            this.resetLocationMapToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetLocationMapToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetLocationMapToolStripMenuItem.Name = "resetLocationMapToolStripMenuItem";
            this.resetLocationMapToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetLocationMapToolStripMenuItem.Text = "Reset location map";
            this.resetLocationMapToolStripMenuItem.Click += new System.EventHandler(this.resetLocationMapToolStripMenuItem_Click);
            // 
            // resetNPCDataToolStripMenuItem
            // 
            this.resetNPCDataToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetNPCDataToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetNPCDataToolStripMenuItem.Name = "resetNPCDataToolStripMenuItem";
            this.resetNPCDataToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetNPCDataToolStripMenuItem.Text = "Reset NPCs";
            this.resetNPCDataToolStripMenuItem.Click += new System.EventHandler(this.resetNPCDataToolStripMenuItem_Click);
            // 
            // resetTreasuresToolStripMenuItem
            // 
            this.resetTreasuresToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetTreasuresToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetTreasuresToolStripMenuItem.Name = "resetTreasuresToolStripMenuItem";
            this.resetTreasuresToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetTreasuresToolStripMenuItem.Text = "Reset treasures";
            this.resetTreasuresToolStripMenuItem.Click += new System.EventHandler(this.resetTreasuresToolStripMenuItem_Click);
            // 
            // resetEventDataToolStripMenuItem
            // 
            this.resetEventDataToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetEventDataToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetEventDataToolStripMenuItem.Name = "resetEventDataToolStripMenuItem";
            this.resetEventDataToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetEventDataToolStripMenuItem.Text = "Reset event fields";
            this.resetEventDataToolStripMenuItem.Click += new System.EventHandler(this.resetEventDataToolStripMenuItem_Click);
            // 
            // resetExitDataToolStripMenuItem
            // 
            this.resetExitDataToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetExitDataToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetExitDataToolStripMenuItem.Name = "resetExitDataToolStripMenuItem";
            this.resetExitDataToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetExitDataToolStripMenuItem.Text = "Reset exit fields";
            this.resetExitDataToolStripMenuItem.Click += new System.EventHandler(this.resetExitDataToolStripMenuItem_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(189, 6);
            // 
            // resetPaletteSetToolStripMenuItem
            // 
            this.resetPaletteSetToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetPaletteSetToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetPaletteSetToolStripMenuItem.Name = "resetPaletteSetToolStripMenuItem";
            this.resetPaletteSetToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetPaletteSetToolStripMenuItem.Text = "Reset palette set";
            this.resetPaletteSetToolStripMenuItem.Click += new System.EventHandler(this.resetPaletteSetToolStripMenuItem_Click);
            // 
            // resetGraphicSetToolStripMenuItem
            // 
            this.resetGraphicSetToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetGraphicSetToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetGraphicSetToolStripMenuItem.Name = "resetGraphicSetToolStripMenuItem";
            this.resetGraphicSetToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetGraphicSetToolStripMenuItem.Text = "Reset graphic set";
            this.resetGraphicSetToolStripMenuItem.Click += new System.EventHandler(this.resetGraphicSetToolStripMenuItem_Click);
            // 
            // resetTilesetsToolStripMenuItem
            // 
            this.resetTilesetsToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetTilesetsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetTilesetsToolStripMenuItem.Name = "resetTilesetsToolStripMenuItem";
            this.resetTilesetsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetTilesetsToolStripMenuItem.Text = "Reset tilesets";
            this.resetTilesetsToolStripMenuItem.Click += new System.EventHandler(this.resetTilesetsToolStripMenuItem_Click);
            // 
            // resetTilemapsToolStripMenuItem
            // 
            this.resetTilemapsToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetTilemapsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetTilemapsToolStripMenuItem.Name = "resetTilemapsToolStripMenuItem";
            this.resetTilemapsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetTilemapsToolStripMenuItem.Text = "Reset tilemaps";
            this.resetTilemapsToolStripMenuItem.Click += new System.EventHandler(this.resetTilemapsToolStripMenuItem_Click);
            // 
            // resetSoliditySetToolStripMenuItem
            // 
            this.resetSoliditySetToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetSoliditySetToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetSoliditySetToolStripMenuItem.Name = "resetSoliditySetToolStripMenuItem";
            this.resetSoliditySetToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetSoliditySetToolStripMenuItem.Text = "Reset solidity set";
            this.resetSoliditySetToolStripMenuItem.Click += new System.EventHandler(this.resetSoliditySetToolStripMenuItem_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(189, 6);
            // 
            // resetAnimatedGraphicsToolStripMenuItem
            // 
            this.resetAnimatedGraphicsToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetAnimatedGraphicsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetAnimatedGraphicsToolStripMenuItem.Name = "resetAnimatedGraphicsToolStripMenuItem";
            this.resetAnimatedGraphicsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetAnimatedGraphicsToolStripMenuItem.Text = "Reset animated graphics";
            this.resetAnimatedGraphicsToolStripMenuItem.Click += new System.EventHandler(this.resetAnimatedGraphicsToolStripMenuItem_Click);
            // 
            // resetAllComponentsToolStripMenuItem
            // 
            this.resetAllComponentsToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.reset;
            this.resetAllComponentsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetAllComponentsToolStripMenuItem.Name = "resetAllComponentsToolStripMenuItem";
            this.resetAllComponentsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetAllComponentsToolStripMenuItem.Text = "Reset all components";
            this.resetAllComponentsToolStripMenuItem.Click += new System.EventHandler(this.resetAllComponentsToolStripMenuItem_Click);
            // 
            // clear
            // 
            this.clear.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLocationDataAll,
            this.toolStripSeparator38,
            this.clearTilesetsAll,
            this.clearTilemapsAll,
            this.clearPhysicalMapsAll,
            this.toolStripSeparator29,
            this.unusedGraphicSetsToolStripMenuItem,
            this.unusedToolStripMenuItem,
            this.unusedToolStripMenuItem1,
            this.unusedToolStripMenuItem2,
            this.unusedToolStripMenuItem3,
            this.toolStripSeparator8,
            this.clearAllComponentsAll,
            this.clearAllComponentsCurrent});
            this.clear.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.clear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(28, 22);
            this.clear.ToolTipText = "Clear";
            // 
            // clearLocationDataAll
            // 
            this.clearLocationDataAll.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.clearLocationDataAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearLocationDataAll.Name = "clearLocationDataAll";
            this.clearLocationDataAll.Size = new System.Drawing.Size(207, 22);
            this.clearLocationDataAll.Text = "Location Data...";
            this.clearLocationDataAll.Click += new System.EventHandler(this.clearLocationDataAll_Click);
            // 
            // toolStripSeparator38
            // 
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            this.toolStripSeparator38.Size = new System.Drawing.Size(204, 6);
            // 
            // clearTilesetsAll
            // 
            this.clearTilesetsAll.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.clearTilesetsAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearTilesetsAll.Name = "clearTilesetsAll";
            this.clearTilesetsAll.Size = new System.Drawing.Size(207, 22);
            this.clearTilesetsAll.Text = "Tilesets...";
            this.clearTilesetsAll.Click += new System.EventHandler(this.clearTilesetsAll_Click);
            // 
            // clearTilemapsAll
            // 
            this.clearTilemapsAll.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.clearTilemapsAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearTilemapsAll.Name = "clearTilemapsAll";
            this.clearTilemapsAll.Size = new System.Drawing.Size(207, 22);
            this.clearTilemapsAll.Text = "Tilemaps...";
            this.clearTilemapsAll.Click += new System.EventHandler(this.clearTilemapsAll_Click);
            // 
            // clearPhysicalMapsAll
            // 
            this.clearPhysicalMapsAll.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.clearPhysicalMapsAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearPhysicalMapsAll.Name = "clearPhysicalMapsAll";
            this.clearPhysicalMapsAll.Size = new System.Drawing.Size(207, 22);
            this.clearPhysicalMapsAll.Text = "Solidity Sets...";
            this.clearPhysicalMapsAll.Click += new System.EventHandler(this.clearPhysicalMapsAll_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(204, 6);
            // 
            // unusedGraphicSetsToolStripMenuItem
            // 
            this.unusedGraphicSetsToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.unusedGraphicSetsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedGraphicSetsToolStripMenuItem.Name = "unusedGraphicSetsToolStripMenuItem";
            this.unusedGraphicSetsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.unusedGraphicSetsToolStripMenuItem.Text = "Unused graphic sets...";
            this.unusedGraphicSetsToolStripMenuItem.Click += new System.EventHandler(this.unusedGraphicSetsToolStripMenuItem_Click);
            // 
            // unusedToolStripMenuItem
            // 
            this.unusedToolStripMenuItem.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.unusedToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedToolStripMenuItem.Name = "unusedToolStripMenuItem";
            this.unusedToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.unusedToolStripMenuItem.Text = "Unused tilesets...";
            this.unusedToolStripMenuItem.Click += new System.EventHandler(this.unusedToolStripMenuItem_Click);
            // 
            // unusedToolStripMenuItem1
            // 
            this.unusedToolStripMenuItem1.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.unusedToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedToolStripMenuItem1.Name = "unusedToolStripMenuItem1";
            this.unusedToolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.unusedToolStripMenuItem1.Text = "Unused tilemaps...";
            this.unusedToolStripMenuItem1.Click += new System.EventHandler(this.unusedToolStripMenuItem1_Click);
            // 
            // unusedToolStripMenuItem2
            // 
            this.unusedToolStripMenuItem2.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.unusedToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedToolStripMenuItem2.Name = "unusedToolStripMenuItem2";
            this.unusedToolStripMenuItem2.Size = new System.Drawing.Size(207, 22);
            this.unusedToolStripMenuItem2.Text = "Unused solidity sets...";
            this.unusedToolStripMenuItem2.Click += new System.EventHandler(this.unusedToolStripMenuItem2_Click);
            // 
            // unusedToolStripMenuItem3
            // 
            this.unusedToolStripMenuItem3.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.unusedToolStripMenuItem3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedToolStripMenuItem3.Name = "unusedToolStripMenuItem3";
            this.unusedToolStripMenuItem3.Size = new System.Drawing.Size(207, 22);
            this.unusedToolStripMenuItem3.Text = "Unused (all components)...";
            this.unusedToolStripMenuItem3.Click += new System.EventHandler(this.unusedToolStripMenuItem3_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(204, 6);
            // 
            // clearAllComponentsAll
            // 
            this.clearAllComponentsAll.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.clearAllComponentsAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearAllComponentsAll.Name = "clearAllComponentsAll";
            this.clearAllComponentsAll.Size = new System.Drawing.Size(207, 22);
            this.clearAllComponentsAll.Text = "All Components (all)...";
            this.clearAllComponentsAll.Click += new System.EventHandler(this.clearAllComponentsAll_Click);
            // 
            // clearAllComponentsCurrent
            // 
            this.clearAllComponentsCurrent.Image = global::ZONEDOCTOR.Properties.Resources.clear_small;
            this.clearAllComponentsCurrent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearAllComponentsCurrent.Name = "clearAllComponentsCurrent";
            this.clearAllComponentsCurrent.Size = new System.Drawing.Size(207, 22);
            this.clearAllComponentsCurrent.Text = "All Components (current)...";
            this.clearAllComponentsCurrent.Click += new System.EventHandler(this.clearAllComponentsCurrent_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locationInfo});
            this.toolStripDropDownButton2.Image = global::ZONEDOCTOR.Properties.Resources.about_small;
            this.toolStripDropDownButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(27, 22);
            this.toolStripDropDownButton2.ToolTipText = "Location Offsets";
            // 
            // help
            // 
            this.help.CheckOnClick = true;
            this.help.Image = global::ZONEDOCTOR.Properties.Resources.help_small;
            this.help.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(23, 22);
            this.help.ToolTipText = "Help Tips";
            // 
            // baseConversion
            // 
            this.baseConversion.CheckOnClick = true;
            this.baseConversion.Image = global::ZONEDOCTOR.Properties.Resources.baseConversion;
            this.baseConversion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.baseConversion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.baseConversion.Name = "baseConversion";
            this.baseConversion.Size = new System.Drawing.Size(23, 22);
            this.baseConversion.ToolTipText = "Base Conversion";
            // 
            // hexEditor
            // 
            this.hexEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hexEditor.Image = global::ZONEDOCTOR.Properties.Resources.hexEditor;
            this.hexEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.hexEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hexEditor.Name = "hexEditor";
            this.hexEditor.Size = new System.Drawing.Size(23, 22);
            this.hexEditor.Text = "View location map raw hex";
            this.hexEditor.Click += new System.EventHandler(this.hexEditor_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // propertiesButton
            // 
            this.propertiesButton.Checked = true;
            this.propertiesButton.CheckOnClick = true;
            this.propertiesButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.propertiesButton.Image = global::ZONEDOCTOR.Properties.Resources.showMain;
            this.propertiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(23, 22);
            this.propertiesButton.ToolTipText = "Properties Tabs";
            this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
            // 
            // openTileset
            // 
            this.openTileset.CheckOnClick = true;
            this.openTileset.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openTileset.Image = global::ZONEDOCTOR.Properties.Resources.openTilesets;
            this.openTileset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openTileset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTileset.Name = "openTileset";
            this.openTileset.Size = new System.Drawing.Size(23, 22);
            this.openTileset.ToolTipText = "Tileset";
            this.openTileset.Click += new System.EventHandler(this.openTileset_Click);
            // 
            // openTilemap
            // 
            this.openTilemap.CheckOnClick = true;
            this.openTilemap.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openTilemap.Image = global::ZONEDOCTOR.Properties.Resources.openMap;
            this.openTilemap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openTilemap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTilemap.Name = "openTilemap";
            this.openTilemap.Size = new System.Drawing.Size(23, 22);
            this.openTilemap.ToolTipText = "Tilemap";
            this.openTilemap.Click += new System.EventHandler(this.openTilemap_Click);
            // 
            // openPaletteEditor
            // 
            this.openPaletteEditor.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openPaletteEditor.Image = global::ZONEDOCTOR.Properties.Resources.openPalettes;
            this.openPaletteEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPaletteEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openPaletteEditor.Name = "openPaletteEditor";
            this.openPaletteEditor.Size = new System.Drawing.Size(23, 22);
            this.openPaletteEditor.ToolTipText = "Palettes";
            this.openPaletteEditor.Click += new System.EventHandler(this.openPaletteEditor_Click);
            // 
            // openGraphicEditor
            // 
            this.openGraphicEditor.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openGraphicEditor.Image = global::ZONEDOCTOR.Properties.Resources.openGraphics;
            this.openGraphicEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openGraphicEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openGraphicEditor.Name = "openGraphicEditor";
            this.openGraphicEditor.Size = new System.Drawing.Size(23, 22);
            this.openGraphicEditor.ToolTipText = "BPP Graphics";
            this.openGraphicEditor.Click += new System.EventHandler(this.openGraphicEditor_Click);
            // 
            // openTemplates
            // 
            this.openTemplates.CheckOnClick = true;
            this.openTemplates.Image = global::ZONEDOCTOR.Properties.Resources.openTemplates;
            this.openTemplates.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTemplates.Name = "openTemplates";
            this.openTemplates.Size = new System.Drawing.Size(23, 22);
            this.openTemplates.ToolTipText = "Templates";
            this.openTemplates.Click += new System.EventHandler(this.openTemplates_Click);
            // 
            // openPreviewer
            // 
            this.openPreviewer.Image = global::ZONEDOCTOR.Properties.Resources.preview;
            this.openPreviewer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPreviewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openPreviewer.Name = "openPreviewer";
            this.openPreviewer.Size = new System.Drawing.Size(23, 22);
            this.openPreviewer.ToolTipText = "Previewer";
            this.openPreviewer.Click += new System.EventHandler(this.openPreviewer_Click);
            // 
            // spaceAnalyzer
            // 
            this.spaceAnalyzer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.spaceAnalyzer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzerInfo});
            this.spaceAnalyzer.Image = global::ZONEDOCTOR.Properties.Resources.spaceAnalyzer;
            this.spaceAnalyzer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.spaceAnalyzer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.spaceAnalyzer.Name = "spaceAnalyzer";
            this.spaceAnalyzer.Size = new System.Drawing.Size(31, 22);
            this.spaceAnalyzer.Text = "Space Analyzer";
            this.spaceAnalyzer.DropDownOpening += new System.EventHandler(this.spaceAnalyzer_DropDownOpening);
            // 
            // numeralsButton
            // 
            this.numeralsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.numeralsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.numeralsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.numeralsButton.Name = "numeralsButton";
            this.numeralsButton.Size = new System.Drawing.Size(23, 22);
            this.numeralsButton.Visible = false;
            this.numeralsButton.Click += new System.EventHandler(this.numeralsButton_Click);
            // 
            // CreateNewLocation
            // 
            this.CreateNewLocation.WorkerSupportsCancellation = true;
            this.CreateNewLocation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CreateNewLocation_DoWork);
            this.CreateNewLocation.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CreateNewLocation_ProgressChanged);
            this.CreateNewLocation.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CreateNewLocation_RunWorkerCompleted);
            // 
            // Locations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 741);
            this.Controls.Add(this.panelLocations);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::ZONEDOCTOR.Properties.Resources.ZONEDOCTOR_icon;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "Locations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LOCATIONS - Zone Doctor CE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Locations_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Locations_FormClosed);
            this.tabPage8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcEventPointer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcSpriteIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcSpriteSet)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcCheckMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcCheckBit)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.panel52.ResumeLayout(false);
            this.panel52.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitWidth)).EndInit();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestinationY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestinationX)).EndInit();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventEventNum)).EndInit();
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPaletteSetNum)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPhysicalMapNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL1Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL2Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL3Num)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSetL3Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet4Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationL2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet3Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet2Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationBG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationL3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet1Num)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL2Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL1Num)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBattleBG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapBattleZone)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerPrioritySet)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spriteMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topSync)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskHighY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskHighX)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerL2LeftShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL2UpShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL3UpShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL3LeftShift)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treasureXCoord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treasureYCoord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treasurePropertyNum)).EndInit();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treasureCheckMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treasureCheckBit)).EndInit();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelLocations.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonGotoA;
        private Button buttonGotoD;
        private CheckBox exitShowMessage;
        private CheckBox layerColorMathBG;
        private CheckBox layerColorMathL1;
        private CheckBox layerColorMathL2;
        private CheckBox layerColorMathL3;
        private CheckBox layerColorMathNPC;
        private CheckBox layerMainscreenL1;
        private CheckBox layerMainscreenL2;
        private CheckBox layerMainscreenL3;
        private CheckBox layerMainscreenNPC;
        private CheckBox layerSubscreenL1;
        private CheckBox layerSubscreenL2;
        private CheckBox layerSubscreenL3;
        private CheckBox layerSubscreenNPC;
        private CheckBox mapSetL3Priority;
        private ComboBox exitDestination;
        private ComboBox exitDirection;
        private ComboBox exitDestinationFacing;
        private ComboBox layerColorMathIntensity;
        private ComboBox layerColorMathMode;
        private ComboBox messageName;
        private ComboBox npcFace;
        private Label label119;
        private Label label124;
        private Label label127;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label25;
        private Label label29;
        private Label label30;
        private Label label32;
        private Label label34;
        private Label label35;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private Label label45;
        private Label label46;
        private Label label47;
        private Label label49;
        private Label label53;
        private Label label59;
        private Label label61;
        private Label label63;
        private Label label9;
        private Label label95;
        private Label label96;
        private NumericUpDown eventX;
        private NumericUpDown eventY;
        private NumericUpDown eventEventNum;
        private NumericUpDown exitWidth;
        private NumericUpDown exitDestinationX;
        private NumericUpDown exitDestinationY;
        private NumericUpDown exitX;
        private NumericUpDown exitY;
        private NumericUpDown layerL2LeftShift;
        private NumericUpDown layerL2UpShift;
        private NumericUpDown layerL3LeftShift;
        private NumericUpDown layerL3UpShift;
        private NumericUpDown layerMaskHighX;
        private NumericUpDown layerMaskHighY;
        private NumericUpDown layerPrioritySet;
        private NumericUpDown mapGFXSet1Num;
        private NumericUpDown mapGFXSet2Num;
        private NumericUpDown mapGFXSet3Num;
        private NumericUpDown mapGFXSet4Num;
        private NumericUpDown mapGFXSetL3Num;
        private NumericUpDown mapPaletteSetNum;
        private NumericUpDown mapPhysicalMapNum;
        private NumericUpDown mapTilemapL1Num;
        private NumericUpDown mapTilemapL2Num;
        private NumericUpDown mapTilemapL3Num;
        private NumericUpDown mapTilesetL1Num;
        private NumericUpDown mapTilesetL2Num;
        private NumericUpDown npcEventPointer;
        private NumericUpDown npcSpriteSet;
        private NumericUpDown npcSpriteIndex;
        private NumericUpDown npcX;
        private NumericUpDown npcY;
        private Panel panel52;
        private Panel panel68;
        private Panel panel9;
        private TabControl tabControl;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage8;
        private TabPage tabPage9;
        private ToolStrip toolStrip1;
        private ToolStripButton openPreviewer;
        private ToolStripButton openGraphicEditor;
        private ToolStripButton openPaletteEditor;
        private ToolStripButton openTilemap;
        private ToolStripButton openTileset;
        private ToolStripMenuItem allToolStripMenuItem;
        private ToolStripMenuItem arraysToolStripMenuItem;
        private ToolStripMenuItem arraysToolStripMenuItem1;
        private ToolStripMenuItem clearAllComponentsAll;
        private ToolStripMenuItem clearAllComponentsCurrent;
        private ToolStripMenuItem clearLocationDataAll;
        private ToolStripMenuItem clearPhysicalMapsAll;
        private ToolStripMenuItem clearTilemapsAll;
        private ToolStripMenuItem clearTilesetsAll;
        private ToolStripMenuItem exportLocationImagesToolStripMenuItem1;
        private ToolStripMenuItem graphicSetsToolStripMenuItem;
        private ToolStripMenuItem graphicSetsToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem unusedToolStripMenuItem;
        private ToolStripMenuItem unusedToolStripMenuItem1;
        private ToolStripMenuItem unusedToolStripMenuItem2;
        private ToolStripMenuItem unusedToolStripMenuItem3;
        private ToolStripSeparator toolStripSeparator28;
        private ToolStripSeparator toolStripSeparator29;
        private ToolStripSeparator toolStripSeparator30;
        private ToolStripSeparator toolStripSeparator38;
        private ToolStripSeparator toolStripSeparator8;
        private ToolTip toolTip1;
        private ToolStripButton openTemplates;
        private Panel panel2;
        private Panel panelLocations;
        private System.Windows.Forms.ToolStripComboBox locationName;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton searchLocationNames;
        private ToolStripTextBox searchBox;
        private ToolStrip toolStrip2;
        private ToolStripButton save;
        private ToolStripDropDownButton import;
        private ToolStripDropDownButton export;
        private ToolStripDropDownButton clear;
        private ToolStripButton help;
        private ToolStripButton baseConversion;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton propertiesButton;
        private Label label28;
        private ToolStripNumericUpDown locationNum;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStrip toolStrip3;
        private ToolStripButton npcMoveUp;
        private ToolStripButton npcMoveDown;
        private ToolStripButton npcCopy;
        private ToolStripButton npcPaste;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton npcDuplicate;
        private ToolStripButton npcInsertObject;
        private ToolStripButton npcRemoveObject;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStrip toolStrip6;
        private ToolStripButton buttonDeleteEvent;
        private ToolStrip toolStrip5;
        private ToolStripButton buttonInsertExit;
        private ToolStripButton buttonDeleteExit;
        private ToolStripButton buttonInsertEvent;
        private ToolStripButton eventsCopyField;
        private ToolStripButton eventsPasteField;
        private ToolStripButton eventsDuplicateField;
        private ToolStripButton exitsCopyField;
        private ToolStripButton exitsPasteField;
        private ToolStripButton exitsDuplicateField;
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox musicName;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripButton buttonGotoC;
        private ToolStripNumericUpDown entranceEvent;
        private CheckedListBox layerEffects;
        private ComboBox l3height;
        private ComboBox l3width;
        private ComboBox l2height;
        private ComboBox l2width;
        private ComboBox l1height;
        private ComboBox l1width;
        private Label label3;
        private Label label12;
        private Label label11;
        private CheckedListBox layerUnknownBits;
        private NumericUpDown spriteMask;
        private CheckBox mapRandomBattles;
        private Label label38;
        private Label label71;
        private NumericUpDown mapBattleZone;
        private NumericUpDown mapBattleBG;
        private ComboBox mapBattleBGName;
        private Label label19;
        private Label label39;
        private NumericUpDown animationL2;
        private NumericUpDown animationL3;
        private CheckBox useWorldMapBG;
        private Label label31;
        private ComboBox npcVehicle;
        private NumericUpDown npcCheckBit;
        private NumericUpDown npcCheckMem;
        private Label label52;
        private Label label48;
        private CheckedListBox npcUnknownBits;
        private CheckedListBox npcWalkability;
        private Label label40;
        private ComboBox npcSpeed;
        private NumericUpDown npcPalette;
        private Label label65;
        private CheckBox exitToWorldMap;
        private CheckedListBox exitUnknownBits;
        private TabPage tabPage1;
        private Panel panel8;
        private Label label36;
        private Label label37;
        private ToolStrip toolStrip4;
        private ToolStripButton buttonInsertTreasure;
        private ToolStripButton buttonDeleteTreasure;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripButton treasureMoveUp;
        private ToolStripButton treasureMoveDown;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripButton treasureCopy;
        private ToolStripButton treasurePaste;
        private ToolStripButton treasureDuplicate;
        private Label label64;
        private Label label67;
        private Label label83;
        private NumericUpDown treasurePropertyNum;
        private Label label84;
        private Label label73;
        private ComboBox treasurePropertyName;
        private ComboBox treasureType;
        private NumericUpDown treasureCheckMem;
        private NumericUpDown treasureCheckBit;
        private NumericUpDown treasureXCoord;
        private NumericUpDown treasureYCoord;
        private ListBox npcListBox;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripSeparator toolStripSeparator13;
        private Label label26;
        private NumericUpDown topSync;
        private ListBox treasureListBox;
        private ListBox exitListBox;
        private ListBox eventListBox;
        private Label label33;
        private BackgroundWorker CreateNewLocation;
        private ToolStripLabel loadingLocationLabel;
        private ToolStripProgressBar loadingLocation;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripLabel exitsBytesLeft;
        private ToolStripSeparator toolStripSeparator18;
        private ToolStripLabel eventsBytesLeft;
        private Label npcsBytesLeft;
        private Label treasuresBytesLeft;
        private ToolStripButton numeralsButton;
        private ToolStripButton hexEditor;
        private ToolStripSeparator toolStripSeparator19;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox5;
        private GroupBox groupBox11;
        private GroupBox groupBox10;
        private GroupBox groupBox9;
        private GroupBox groupBox8;
        private GroupBox groupBox7;
        private GroupBox groupBox6;
        private Label label6;
        private Label label5;
        private Label label4;
        private GroupBox groupBox14;
        private GroupBox groupBox13;
        private GroupBox groupBox12;
        private GroupBox groupBox18;
        private GroupBox groupBox16;
        private GroupBox groupBox15;
        private GroupBox groupBox17;
        private GroupBox groupBox19;
        private GroupBox groupBox20;
        private Panel panel1;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripButton navigateBck;
        private ToolStripButton navigateFwd;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem resetLocationMapToolStripMenuItem;
        private ToolStripMenuItem resetNPCDataToolStripMenuItem;
        private ToolStripMenuItem resetTreasuresToolStripMenuItem;
        private ToolStripMenuItem resetEventDataToolStripMenuItem;
        private ToolStripMenuItem resetExitDataToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator17;
        private ToolStripMenuItem resetPaletteSetToolStripMenuItem;
        private ToolStripMenuItem resetGraphicSetToolStripMenuItem;
        private ToolStripMenuItem resetTilesetsToolStripMenuItem;
        private ToolStripMenuItem resetTilemapsToolStripMenuItem;
        private ToolStripMenuItem resetSoliditySetToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator20;
        private ToolStripMenuItem resetAllComponentsToolStripMenuItem;
        private ZONEDOCTOR.ToolStripListView locationInfo;
        private ZONEDOCTOR.ToolStripListView analyzerInfo;
        private ToolStripMenuItem resetAnimatedGraphicsToolStripMenuItem;
        private ToolStripMenuItem unusedGraphicSetsToolStripMenuItem;
        private ToolStripDropDownButton spaceAnalyzer;
        private Label label7;
        private Label label1;
        private ComboBox windowMask;
        private Label label8;
        private NumericUpDown animationBG;
        private ToolStripMenuItem importArchitectureToolStripMenuItem;
        private ToolStripMenuItem exportArchitectureToolStripMenuItem;
        private GroupBox groupBox21;
        private TextBox tbMapName;
        private GroupBox groupBox22;
        private TextBox tbLocation;
    }
}

