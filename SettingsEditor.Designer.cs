
namespace ZONEDOCTOR
{
    partial class SettingsEditor
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.customDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.visualThemeSystem = new System.Windows.Forms.RadioButton();
            this.visualThemeStandard = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.romDirectory = new System.Windows.Forms.RadioButton();
            this.customDirectory = new System.Windows.Forms.RadioButton();
            this.buttonCustomDirectory = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbExpansionData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbExpansionTilemaps = new System.Windows.Forms.TextBox();
            this.nudBanks = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExpand = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBanks)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.ColumnWidth = 214;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Auto-load last used ROM",
            "Auto-create all editor data",
            "Auto-load last used Notes DB",
            "Create back-up ROM on save",
            "Create back-up ROM on load",
            "Verify ROM",
            "Show encryption warnings",
            "Remember last loaded indexes"});
            this.checkedListBox1.Location = new System.Drawing.Point(12, 12);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(456, 68);
            this.checkedListBox1.TabIndex = 0;
            // 
            // customDirectoryTextBox
            // 
            this.customDirectoryTextBox.Location = new System.Drawing.Point(123, 35);
            this.customDirectoryTextBox.Name = "customDirectoryTextBox";
            this.customDirectoryTextBox.ReadOnly = true;
            this.customDirectoryTextBox.Size = new System.Drawing.Size(294, 21);
            this.customDirectoryTextBox.TabIndex = 1;
            // 
            // buttonDefault
            // 
            this.buttonDefault.Location = new System.Drawing.Point(135, 191);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(87, 23);
            this.buttonDefault.TabIndex = 2;
            this.buttonDefault.Text = "Default...";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(393, 191);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // visualThemeSystem
            // 
            this.visualThemeSystem.AutoSize = true;
            this.visualThemeSystem.Location = new System.Drawing.Point(6, 20);
            this.visualThemeSystem.Name = "visualThemeSystem";
            this.visualThemeSystem.Size = new System.Drawing.Size(60, 17);
            this.visualThemeSystem.TabIndex = 4;
            this.visualThemeSystem.TabStop = true;
            this.visualThemeSystem.Text = "System";
            this.visualThemeSystem.UseVisualStyleBackColor = true;
            // 
            // visualThemeStandard
            // 
            this.visualThemeStandard.AutoSize = true;
            this.visualThemeStandard.Location = new System.Drawing.Point(6, 37);
            this.visualThemeStandard.Name = "visualThemeStandard";
            this.visualThemeStandard.Size = new System.Drawing.Size(69, 17);
            this.visualThemeStandard.TabIndex = 4;
            this.visualThemeStandard.TabStop = true;
            this.visualThemeStandard.Text = "Standard";
            this.visualThemeStandard.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.visualThemeSystem);
            this.groupBox1.Controls.Add(this.visualThemeStandard);
            this.groupBox1.Location = new System.Drawing.Point(12, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(87, 60);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visual Theme";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.romDirectory);
            this.groupBox2.Controls.Add(this.customDirectory);
            this.groupBox2.Controls.Add(this.customDirectoryTextBox);
            this.groupBox2.Controls.Add(this.buttonCustomDirectory);
            this.groupBox2.Location = new System.Drawing.Point(12, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 62);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Back-up ROM location";
            // 
            // romDirectory
            // 
            this.romDirectory.AutoSize = true;
            this.romDirectory.Location = new System.Drawing.Point(6, 20);
            this.romDirectory.Name = "romDirectory";
            this.romDirectory.Size = new System.Drawing.Size(94, 17);
            this.romDirectory.TabIndex = 4;
            this.romDirectory.TabStop = true;
            this.romDirectory.Text = "ROM directory";
            this.romDirectory.UseVisualStyleBackColor = true;
            // 
            // customDirectory
            // 
            this.customDirectory.AutoSize = true;
            this.customDirectory.Location = new System.Drawing.Point(6, 37);
            this.customDirectory.Name = "customDirectory";
            this.customDirectory.Size = new System.Drawing.Size(111, 17);
            this.customDirectory.TabIndex = 4;
            this.customDirectory.TabStop = true;
            this.customDirectory.Text = "Custom directory:";
            this.customDirectory.UseVisualStyleBackColor = true;
            // 
            // buttonCustomDirectory
            // 
            this.buttonCustomDirectory.Location = new System.Drawing.Point(423, 35);
            this.buttonCustomDirectory.Name = "buttonCustomDirectory";
            this.buttonCustomDirectory.Size = new System.Drawing.Size(27, 21);
            this.buttonCustomDirectory.TabIndex = 2;
            this.buttonCustomDirectory.Text = "...";
            this.buttonCustomDirectory.UseVisualStyleBackColor = true;
            this.buttonCustomDirectory.Click += new System.EventHandler(this.buttonCustomDirectory_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(312, 191);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Cancel";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(231, 191);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnExpand);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.nudBanks);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tbExpansionTilemaps);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tbExpansionData);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(13, 232);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(337, 144);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Map Expansion";
            // 
            // tbExpansionData
            // 
            this.tbExpansionData.Location = new System.Drawing.Point(299, 21);
            this.tbExpansionData.MaxLength = 2;
            this.tbExpansionData.Name = "tbExpansionData";
            this.tbExpansionData.Size = new System.Drawing.Size(24, 22);
            this.tbExpansionData.TabIndex = 2;
            this.tbExpansionData.Text = "F3";
            this.tbExpansionData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "NPCs, Events, Exits and Chests expansion bank";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(283, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "$";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(283, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "$";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(10, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(242, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "Locations Tilemaps expansion starting bank";
            // 
            // tbExpansionTilemaps
            // 
            this.tbExpansionTilemaps.Location = new System.Drawing.Point(299, 49);
            this.tbExpansionTilemaps.MaxLength = 2;
            this.tbExpansionTilemaps.Name = "tbExpansionTilemaps";
            this.tbExpansionTilemaps.Size = new System.Drawing.Size(24, 22);
            this.tbExpansionTilemaps.TabIndex = 5;
            this.tbExpansionTilemaps.Text = "F4";
            this.tbExpansionTilemaps.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudBanks
            // 
            this.nudBanks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudBanks.Location = new System.Drawing.Point(286, 77);
            this.nudBanks.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudBanks.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudBanks.Name = "nudBanks";
            this.nudBanks.Size = new System.Drawing.Size(37, 22);
            this.nudBanks.TabIndex = 8;
            this.nudBanks.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudBanks.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(10, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "Number of banks for Tilemaps data";
            // 
            // btnExpand
            // 
            this.btnExpand.Location = new System.Drawing.Point(243, 107);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(80, 23);
            this.btnExpand.TabIndex = 10;
            this.btnExpand.Text = "Expand";
            this.btnExpand.UseVisualStyleBackColor = true;
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // SettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 570);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.checkedListBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::ZONEDOCTOR.Properties.Resources.ZONEDOCTOR_icon;
            this.Name = "SettingsEditor";
            this.Text = "ZONE DOCTOR - Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBanks)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TextBox customDirectoryTextBox;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.RadioButton visualThemeSystem;
        private System.Windows.Forms.RadioButton visualThemeStandard;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton romDirectory;
        private System.Windows.Forms.RadioButton customDirectory;
        private System.Windows.Forms.Button buttonCustomDirectory;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnExpand;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudBanks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbExpansionTilemaps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbExpansionData;
    }
}