using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public partial class Locations
    {
        // variables
        private delegate void Function();
        private PaletteEditor paletteEditor;
        private GraphicEditor graphicEditor;
        private TilesetEditor tilesetEditor;
        public TilemapEditor tilemapEditor;
        private LocationsTemplate locationTemplate;
        private Previewer previewer;
        // functions
        private void PaletteUpdate()
        {
            //while (CreateNewLocation.IsBusy)
            //    Application.DoEvents();
            if (closingEditor)
                return;
            fullUpdate = false;
            RefreshLocation();
        }
        private void GraphicUpdate()
        {
            //while (CreateNewLocation.IsBusy)
            //    Application.DoEvents();
            if (closingEditor)
                return;
            if (index <= 2)
                tileset.Assemble(index < 2 ? 256 : 128);
            else
                tileset.Assemble(16, tilesetEditor.Layer);
            fullUpdate = false;
            RefreshLocation();
        }
        private void TilemapUpdate()
        {
            if (closingEditor)
                return;
        }
        private void TilesetUpdate()
        {
            //while (CreateNewLocation.IsBusy)
            //    Application.DoEvents();
            if (closingEditor)
                return;
            tilemap.Assemble();
            tilemap.RedrawTilemap();
            fullUpdate = false;
            RefreshLocation();
        }
        //
        private void LoadPaletteEditor()
        {
            if (paletteEditor == null)
            {
                paletteEditor = new PaletteEditor(new Function(PaletteUpdate), paletteSet, 8, 0, 8);
                paletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditor.Reload(new Function(PaletteUpdate), paletteSet, 8, 0, 8);
        }
        private void LoadGraphicEditor()
        {
            if (graphicEditor == null)
            {
                graphicEditor = new GraphicEditor(new Function(GraphicUpdate),
                    tileset.Graphics, tileset.Graphics.Length, 0, paletteSet, 0, index < 3 ? (byte)0x21 : (byte)0x20);
                graphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicEditor.Reload(new Function(GraphicUpdate),
                    tileset.Graphics, tileset.Graphics.Length, 0, paletteSet, 0, index < 3 ? (byte)0x21 : (byte)0x20);
        }
        private void LoadTilemapEditor()
        {
            if (tilemapEditor == null)
            {
                tilemapEditor = new TilemapEditor(
                    this, this.location, this.tilemap, this.soliditySet, this.tileset, this.overlay,
                    this.paletteEditor, this.tilesetEditor, this.locationTemplate);
                tilemapEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tilemapEditor.Reload(
                  this, this.location, this.tilemap, this.soliditySet, this.tileset, this.overlay,
                  this.paletteEditor, this.tilesetEditor, this.locationTemplate);
        }
        private void LoadTilesetEditor()
        {
            if (tilesetEditor == null)
            {
                tilesetEditor = new TilesetEditor(this.tileset, this.soliditySet, new Function(TilesetUpdate), this.paletteSet, this.overlay);
                tilesetEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tilesetEditor.Reload(this.tileset, this.soliditySet, new Function(TilesetUpdate), this.paletteSet, this.overlay);
            tilesetEditor.EnableLayers(true, tileset.Type != TilesetType.World, tileset.Type != TilesetType.World);
        }
        private void LoadTemplateEditor()
        {
            if (locationTemplate == null)
            {
                locationTemplate = new LocationsTemplate(this, this.overlay);
                locationTemplate.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                locationTemplate.Reload(this, this.overlay);
        }
        private void LoadPreviewer()
        {
            if (previewer == null || !previewer.Visible)
            {
                previewer = new Previewer((int)this.locationNum.Value, EType.Location);
                previewer.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                previewer.Reload((int)this.locationNum.Value, EType.Location);
        }
        // event handlers
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        //
        private void propertiesButton_Click(object sender, EventArgs e)
        {
            tabControl.Visible = propertiesButton.Checked;
        }
        private void openPaletteEditor_Click(object sender, EventArgs e)
        {
            paletteEditor.Visible = true;
        }
        private void openGraphicEditor_Click(object sender, EventArgs e)
        {
            graphicEditor.Visible = true;
        }
        private void openTileset_Click(object sender, EventArgs e)
        {
            tilesetEditor.Visible = openTileset.Checked;
            tilesetEditor.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - tilesetEditor.Size.Width - 5, this.Location.Y);
        }
        private void openTilemap_Click(object sender, EventArgs e)
        {
            tilemapEditor.Visible = openTilemap.Checked;
            tilemapEditor.Size = new Size(
                Screen.PrimaryScreen.WorkingArea.Width - tilesetEditor.Width - this.Width - 10, this.Height);
            tilemapEditor.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
        }
        private void openTemplates_Click(object sender, EventArgs e)
        {
            locationTemplate.Visible = openTemplates.Checked;
            locationTemplate.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - locationTemplate.Size.Width, this.Location.Y);
        }
        private void openPreviewer_Click(object sender, EventArgs e)
        {
            LoadPreviewer();
            previewer.Show();
            previewer.BringToFront();
        }
    }
}
