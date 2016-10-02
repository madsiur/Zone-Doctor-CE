
namespace ZONEDOCTOR
{
    partial class Restore
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Event scripts");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Maps");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("NPCs");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Treasures");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Exits");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Events");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Graphics");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Tilesets");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Tilemaps");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Solidity sets");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Location names");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Locations", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("World of Birth");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("World of Ruin");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("World Maps", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            this.elements = new System.Windows.Forms.TreeView();
            this.browseFreshRom = new System.Windows.Forms.Button();
            this.freshRomTextBox = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.selectAll = new System.Windows.Forms.Button();
            this.deselectAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // elements
            // 
            this.elements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elements.CheckBoxes = true;
            this.elements.Location = new System.Drawing.Point(12, 70);
            this.elements.Name = "elements";
            treeNode1.Name = "EventScripts";
            treeNode1.Text = "Event scripts";
            treeNode2.Name = "Maps";
            treeNode2.Text = "Maps";
            treeNode3.Name = "NPCs";
            treeNode3.Text = "NPCs";
            treeNode4.Name = "Treasures";
            treeNode4.Text = "Treasures";
            treeNode5.Name = "Exits";
            treeNode5.Text = "Exits";
            treeNode6.Name = "Events";
            treeNode6.Text = "Events";
            treeNode7.Name = "Graphics";
            treeNode7.Text = "Graphics";
            treeNode8.Name = "Tilesets";
            treeNode8.Text = "Tilesets";
            treeNode9.Name = "Tilemaps";
            treeNode9.Text = "Tilemaps";
            treeNode10.Name = "SoliditySets";
            treeNode10.Text = "Solidity sets";
            treeNode11.Name = "LocationNames";
            treeNode11.Text = "Location names";
            treeNode12.Name = "Locations";
            treeNode12.Text = "Locations";
            treeNode13.Name = "WorldOfBirth";
            treeNode13.Text = "World of Birth";
            treeNode14.Name = "WorldOfRuin";
            treeNode14.Text = "World of Ruin";
            treeNode15.Name = "WorldMaps";
            treeNode15.Text = "World Maps";
            this.elements.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode12,
            treeNode15});
            this.elements.Size = new System.Drawing.Size(333, 282);
            this.elements.TabIndex = 2;
            this.elements.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.elements_AfterCheck);
            this.elements.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.elements_AfterSelect);
            // 
            // browseFreshRom
            // 
            this.browseFreshRom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseFreshRom.Location = new System.Drawing.Point(318, 14);
            this.browseFreshRom.Name = "browseFreshRom";
            this.browseFreshRom.Size = new System.Drawing.Size(27, 23);
            this.browseFreshRom.TabIndex = 1;
            this.browseFreshRom.Text = "...";
            this.browseFreshRom.UseVisualStyleBackColor = true;
            this.browseFreshRom.Click += new System.EventHandler(this.browseFreshRom_Click);
            // 
            // freshRomTextBox
            // 
            this.freshRomTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.freshRomTextBox.Location = new System.Drawing.Point(12, 14);
            this.freshRomTextBox.Name = "freshRomTextBox";
            this.freshRomTextBox.ReadOnly = true;
            this.freshRomTextBox.Size = new System.Drawing.Size(300, 21);
            this.freshRomTextBox.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(189, 358);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(270, 358);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // selectAll
            // 
            this.selectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAll.Location = new System.Drawing.Point(12, 41);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(164, 23);
            this.selectAll.TabIndex = 3;
            this.selectAll.Text = "SELECT ALL";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // deselectAll
            // 
            this.deselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deselectAll.Location = new System.Drawing.Point(182, 41);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(163, 23);
            this.deselectAll.TabIndex = 4;
            this.deselectAll.Text = "DESELECT ALL";
            this.deselectAll.UseVisualStyleBackColor = true;
            this.deselectAll.Click += new System.EventHandler(this.deselectAll_Click);
            // 
            // Restore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(357, 393);
            this.Controls.Add(this.deselectAll);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.selectAll);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.freshRomTextBox);
            this.Controls.Add(this.browseFreshRom);
            this.Controls.Add(this.elements);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::ZONEDOCTOR.Properties.Resources.ZONEDOCTOR_icon;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "Restore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "IMPORT ELEMENTS FROM ANOTHER ROM...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.TreeView elements;
        private System.Windows.Forms.Button browseFreshRom;
        private System.Windows.Forms.TextBox freshRomTextBox;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button selectAll;
        private System.Windows.Forms.Button deselectAll;
    }
}