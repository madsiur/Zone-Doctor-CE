using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZONEDOCTOR.Properties;
using ZONEDOCTOR.ScriptsEditor;
using ZONEDOCTOR.ScriptsEditor.Commands;

namespace ZONEDOCTOR
{
    public partial class IOElements : NewForm
    {
        private Settings settings = Settings.Default;
        private object element;
        private int currentIndex;
        private string fullPath;
        private Type type;
        // constructor
        public IOElements(object element, int currentIndex, string title)
        {
            this.element = element;
            this.currentIndex = currentIndex;
            this.type = element.GetType();
            this.TopLevel = true;
            InitializeComponent();
            this.Text = title;
            if (element.GetType() == typeof(Locations) && currentIndex < 3)
                radioButtonAll.Enabled = false;
        }
        // event handlers
        private void radioButtonCurrent_CheckedChanged(object sender, EventArgs e)
        {
            browseAll.Enabled = false;
            textBoxAll.Enabled = false;
            browseCurrent.Enabled = true;
            textBoxCurrent.Enabled = true;
            if (radioButtonCurrent.Checked)
            {
                buttonOK.Enabled = textBoxCurrent.Text != "";
            }
            fullPath = textBoxCurrent.Text;
        }
        private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
        {
            browseCurrent.Enabled = false;
            textBoxCurrent.Enabled = false;
            browseAll.Enabled = true;
            textBoxAll.Enabled = true;
            buttonOK.Enabled = true;
            if (radioButtonAll.Checked)
            {
                buttonOK.Enabled = textBoxAll.Text != "";
            }
            fullPath = textBoxAll.Text;
        }
        private void browseCurrent_Click(object sender, EventArgs e)
        {
            TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            string name = this.Text.ToLower().Substring(7, this.Text.Length - 7 - 4);
            string ext = ".dat";
            string filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*";
            if (this.Text.Substring(0, 6) == "EXPORT")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Select directory to export to";
                saveFileDialog.Filter = filter;
                try
                {
                    saveFileDialog.FileName = name + "." + currentIndex.ToString(
                        "d" + ((object[])element).Length.ToString().Length) + ext;
                }
                catch
                {
                    saveFileDialog.FileName = name + "." + currentIndex.ToString("d4") + ext;
                }
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                textBoxCurrent.Text = saveFileDialog.FileName;
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = settings.LastRomPath;
                openFileDialog.Title = "Select file to import from";
                openFileDialog.Filter = filter;
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                textBoxCurrent.Text = openFileDialog.FileName;
            }
            fullPath = textBoxCurrent.Text;
            buttonOK.Enabled = true;
        }
        private void browseAll_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = settings.LastDirectory;
            if (this.Text.Substring(0, 6) == "EXPORT")
                folderBrowserDialog.Description = "Select directory to export to";
            else
                folderBrowserDialog.Description = "Select directory to import from";
            // Display the openFile dialog.
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;
            settings.LastDirectory = folderBrowserDialog.SelectedPath;
            textBoxAll.Text = folderBrowserDialog.SelectedPath;
            fullPath = textBoxAll.Text;
            buttonOK.Enabled = true;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            #region Locations
            if (this.Text == "EXPORT LOCATION DATA...")
            {
                this.Enabled = false;
                if (radioButtonCurrent.Checked)
                {
                    // create the serialized location
                    SerializedLocation sLocation = new SerializedLocation();
                    LocationMap lMap = Model.Locations[currentIndex].LocationMap;
                    sLocation.LocationMap = lMap;// Add it to serialized location data object
                    if (currentIndex >= 3)
                    {
                        sLocation.TilesetL1 = Model.Tilesets[lMap.TilesetL1];
                        sLocation.TilesetL2 = Model.Tilesets[lMap.TilesetL2];
                        sLocation.TilemapL1 = Model.Tilemaps[lMap.TilemapL1];
                        sLocation.TilemapL2 = Model.Tilemaps[lMap.TilemapL2];
                        sLocation.TilemapL3 = Model.Tilemaps[lMap.TilemapL3];
                        sLocation.SoliditySet = Model.SoliditySets[lMap.SoliditySet];
                        sLocation.LocationNPCs = Model.Locations[currentIndex].LocationNPCs;
                        sLocation.LocationTreasures = Model.Locations[currentIndex].LocationTreasures;
                    }
                    else if (currentIndex == 0)
                    {
                        sLocation.TilesetL1 = Model.WOBGraphicSet;
                        sLocation.TilemapL1 = Model.WOBTilemap;
                        sLocation.SoliditySet = Model.WOBSolidity;
                    }
                    else if (currentIndex == 1)
                    {
                        sLocation.TilesetL1 = Model.WORGraphicSet;
                        sLocation.TilemapL1 = Model.WORTilemap;
                        sLocation.SoliditySet = Model.WORSolidity;
                    }
                    else if (currentIndex == 2)
                    {
                        sLocation.TilesetL1 = Model.STGraphicSet;
                        sLocation.TilemapL1 = Model.STTilemap;
                    }
                    sLocation.LocationExits = Model.Locations[currentIndex].LocationExits;
                    sLocation.LocationEvents = Model.Locations[currentIndex].LocationEvents;
                    // finally export the serialized locations
                    Do.Export(sLocation, null, fullPath);
                }
                else
                {
                    // create the serialized location
                    SerializedLocation[] sLocations = new SerializedLocation[415];
                    for (int i = 0; i < sLocations.Length; i++)
                    {
                        sLocations[i] = new SerializedLocation();
                        LocationMap lMap = Model.Locations[i].LocationMap;
                        sLocations[i].LocationMap = lMap;// Add it to serialized location data object
                        sLocations[i].TilesetL1 = Model.Tilesets[lMap.TilesetL1];
                        sLocations[i].TilesetL2 = Model.Tilesets[lMap.TilesetL2];
                        sLocations[i].TilemapL1 = Model.Tilemaps[lMap.TilemapL1];
                        sLocations[i].TilemapL2 = Model.Tilemaps[lMap.TilemapL2];
                        sLocations[i].TilemapL3 = Model.Tilemaps[lMap.TilemapL3];
                        sLocations[i].SoliditySet = Model.SoliditySets[lMap.SoliditySet];
                        sLocations[i].LocationNPCs = Model.Locations[i].LocationNPCs;
                        sLocations[i].LocationTreasures = Model.Locations[i].LocationTreasures;
                        sLocations[i].LocationExits = Model.Locations[i].LocationExits;
                        sLocations[i].LocationEvents = Model.Locations[i].LocationEvents;
                    }
                    // finally export the serialized locations
                    Do.Export(sLocations,
                        fullPath + "\\" + Model.GetFileNameWithoutPath() + " - Locations\\" + "location", "LOCATION", true);
                }
            }
            if (this.Text == "IMPORT LOCATION DATA...")
            {
                this.Enabled = false;
                if (radioButtonCurrent.Checked)
                {
                    SerializedLocation sLocation = new SerializedLocation();
                    try
                    {
                        sLocation = (SerializedLocation)Do.Import(sLocation, fullPath);
                    }
                    catch
                    {
                        MessageBox.Show("File not a location data file.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    LocationMap lMap = sLocation.LocationMap;
                    Model.Locations[currentIndex].LocationMap = lMap;
                    if (currentIndex >= 3)
                    {
                        Model.Tilesets[lMap.TilesetL1] = sLocation.TilesetL1;
                        Model.Tilesets[lMap.TilesetL2] = sLocation.TilesetL2;
                        Model.EditTilesets[lMap.TilesetL1] = true;
                        Model.EditTilesets[lMap.TilesetL2] = true;
                        Model.Tilemaps[lMap.TilemapL1] = sLocation.TilemapL1;
                        Model.Tilemaps[lMap.TilemapL2] = sLocation.TilemapL2;
                        Model.Tilemaps[lMap.TilemapL3] = sLocation.TilemapL3;
                        Model.EditTilemaps[lMap.TilemapL1] = true;
                        Model.EditTilemaps[lMap.TilemapL2] = true;
                        Model.EditTilemaps[lMap.TilemapL3] = true;
                        Model.SoliditySets[lMap.SoliditySet] = sLocation.SoliditySet;
                        Model.EditSoliditySets[lMap.SoliditySet] = true;
                        Model.Locations[currentIndex].LocationNPCs = sLocation.LocationNPCs;
                    }
                    else if (currentIndex == 0)
                    {
                        Model.WOBGraphicSet = sLocation.TilesetL1;
                        Model.WOBTilemap = sLocation.TilemapL1;
                        Model.WOBSolidity = sLocation.SoliditySet;
                        Model.EditWOBGraphicSet = true;
                        Model.EditWOBTilemap = true;
                    }
                    else if (currentIndex == 1)
                    {
                        Model.WORGraphicSet = sLocation.TilesetL1;
                        Model.WORTilemap = sLocation.TilemapL1;
                        Model.WORSolidity = sLocation.SoliditySet;
                        Model.EditWORGraphicSet = true;
                        Model.EditWORTilemap = true;
                    }
                    else if (currentIndex == 2)
                    {
                        Model.STGraphicSet = sLocation.TilesetL1;
                        Model.STTilemap = sLocation.TilemapL1;
                        Model.EditSTGraphicSet = true;
                        Model.EditSTTilemap = true;
                    }
                    Model.Locations[currentIndex].LocationExits = sLocation.LocationExits;
                    Model.Locations[currentIndex].LocationEvents = sLocation.LocationEvents;
                }
                else
                {
                    SerializedLocation[] sLocations = new SerializedLocation[415];
                    for (int i = 0; i < sLocations.Length; i++)
                        sLocations[i] = new SerializedLocation();
                    try
                    {
                        Do.Import(sLocations, fullPath + "\\" + "location", "LOCATION", true);
                    }
                    catch
                    {
                        MessageBox.Show("One or more files not a location data file.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    for (int i = 0; i < sLocations.Length; i++)
                    {
                        Model.Locations[i].LocationMap = sLocations[i].LocationMap;
                        LocationMap lMap = sLocations[i].LocationMap;
                        Model.Locations[i].LocationMap = lMap;
                        Model.Tilesets[lMap.TilesetL1] = sLocations[i].TilesetL1;
                        Model.Tilesets[lMap.TilesetL2] = sLocations[i].TilesetL2;
                        Model.EditTilesets[lMap.TilesetL1] = true;
                        Model.EditTilesets[lMap.TilesetL2] = true;
                        Model.Tilemaps[lMap.TilemapL1] = sLocations[i].TilemapL1;
                        Model.Tilemaps[lMap.TilemapL2] = sLocations[i].TilemapL2;
                        Model.Tilemaps[lMap.TilemapL3] = sLocations[i].TilemapL3;
                        Model.EditTilemaps[lMap.TilemapL1] = true;
                        Model.EditTilemaps[lMap.TilemapL2] = true;
                        Model.EditTilemaps[lMap.TilemapL3] = true;
                        Model.SoliditySets[lMap.SoliditySet] = sLocations[i].SoliditySet;
                        Model.EditSoliditySets[lMap.SoliditySet] = true;
                        Model.Locations[i].LocationNPCs = sLocations[i].LocationNPCs;
                        Model.Locations[i].LocationTreasures = sLocations[i].LocationTreasures;
                        Model.Locations[i].LocationExits = sLocations[i].LocationExits;
                        Model.Locations[i].LocationEvents = sLocations[i].LocationEvents;
                    }
                }
            }
            #endregion
            #region Other
            try
            {
                Element[] array = (Element[])element;
                string name = this.Text.ToLower().Substring(7, this.Text.Length - 7 - 4);
                if (this.Text.Substring(0, 6) == "EXPORT")
                {
                    if (radioButtonCurrent.Checked)
                        Do.Export(array[currentIndex], null, textBoxCurrent.Text);
                    else
                        Do.Export(array,
                            fullPath + "\\" + Model.GetFileNameWithoutPath() + " - " +
                            Lists.ToTitleCase(name) + "s" + "\\" + name,
                            name.ToUpper(), true);
                }
                if (this.Text.Substring(0, 6) == "IMPORT")
                {
                    if (radioButtonCurrent.Checked)
                    {
                        try
                        {
                            array[currentIndex] = (Element)Do.Import(array[currentIndex], fullPath);
                        }
                        catch
                        {
                            MessageBox.Show("Incorrect data file type.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        array[currentIndex].Index = currentIndex;
                    }
                    else
                    {
                        try
                        {
                            Do.Import(array, fullPath + "\\" + name, name.ToUpper(), true);
                        }
                        catch
                        {
                            MessageBox.Show("One or more files incorrect data file type.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        int i = 0;
                        foreach (Element item in array)
                        {
                            item.Index = i++;
                        }
                    }
                }
            }
            catch { }
            #endregion
            this.Tag = radioButtonAll.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
