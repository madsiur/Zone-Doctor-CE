using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using ZONEDOCTOR.Undo;

namespace ZONEDOCTOR
{
    public partial class TilemapEditor : NewForm
    {
        #region Variables
        // main
        private delegate void Function();
        public NewPictureBox Picture { get { return pictureBoxLocation; } set { pictureBoxLocation = value; } }
        private Locations locations;
        private Location location;
        private Tilemap tilemap;
        private SoliditySet soliditySet;
        private Tileset tileset;
        private Bitmap tilemapImage;
        private Overlay overlay;
        private State state;
        // editors
        private TilesetEditor tilesetEditor;
        private LocationsTemplate locationsTemplate;
        private PaletteEditor paletteEditor;
        // main classes
        private LocationMap locationMap { get { return location.LocationMap; } }
        private LocationExits exits { get { return location.LocationExits; } set { location.LocationExits = value; } }
        private LocationEvents events { get { return location.LocationEvents; } set { location.LocationEvents = value; } }
        private LocationNPCs npcs { get { return location.LocationNPCs; } set { location.LocationNPCs = value; } }
        private LocationTreasures treasures { get { return location.LocationTreasures; } set { location.LocationTreasures = value; } }
        private LocationTemplate template { get { return locationsTemplate.Template; } }
        private int width { get { return Picture.Width; } }
        private int height { get { return Picture.Height; } }
        // buffers
        private CopyBuffer draggedTiles;
        private CopyBuffer copiedTiles;
        private CommandStack commandStack;
        private int commandCount = 0;
        private Bitmap selection;
        private bool defloating = false;
        // hover variables
        private string mouseOverObject;
        private int mouseOverTile = 0;
        private int mouseOverSolidTile = 0;
        private int mouseOverNPC = 0;
        private int mouseOverTreasure;
        private int mouseOverExitField = 0;
        private int mouseOverEventField = 0;
        private string mouseDownObject;
        private int mouseDownNPC = 0;
        private int mouseDownTreasure;
        private int mouseDownExitField = 0;
        private int mouseDownEventField = 0;
        private Point mousePosition = new Point(0, 0);
        private Point mouseDownPosition = new Point(0, 0);
        private Point mouseTilePosition = new Point(0, 0);
        private Point mouseDownTilePosition = new Point(0, 0);
        private Point mouseOverDelta = new Point(0, 0);
        private Point autoScrollPos;
        private bool mouseWithinSameBounds = false;
        private bool mouseEnter = false;
        private int zoom { get { return pictureBoxLocation.Zoom; } set { pictureBoxLocation.Zoom = value; } }
        public int Zoom { get { return zoom; } }
        #endregion
        #region Functions
        // main
        public TilemapEditor(
            Locations locations, Location location, Tilemap tilemap, SoliditySet soliditySet, Tileset tileset, Overlay overlay,
            PaletteEditor paletteEditor, TilesetEditor locationsTileset, LocationsTemplate locationsTemplate)
        {
            this.state = State.Instance;
            this.locations = locations;
            this.location = location;
            this.tilemap = tilemap;
            this.soliditySet = soliditySet;
            this.tileset = tileset;
            this.overlay = overlay;
            this.tilesetEditor = locationsTileset;
            this.paletteEditor = paletteEditor;
            this.locationsTemplate = locationsTemplate;
            this.commandStack = new CommandStack(true);
            InitializeComponent();
            this.pictureBoxLocation.ZoomBoxPosition = new Point(64, 0);
            SetLocationImage();
            // toggle
            buttonToggleBG.Checked = state.BG;
            toggleCartGrid.Checked = state.TileGrid;
            buttonToggleEvents.Checked = state.Events;
            buttonToggleExits.Checked = state.Exits;
            buttonToggleL1.Checked = state.Layer1;
            buttonToggleL2.Checked = state.Layer2;
            buttonToggleL3.Checked = state.Layer3;
            buttonToggleMask.Checked = state.Mask;
            buttonToggleNPCs.Checked = state.NPCs;
            buttonToggleP1.Checked = state.Priority1;
            buttonTogglePhys.Checked = state.Solidity;
        }
        public void Reload(
            Locations locations, Location location, Tilemap tileMap, SoliditySet soliditySet, Tileset tileset, Overlay overlay,
            PaletteEditor paletteEditor, TilesetEditor locationsTileset, LocationsTemplate locationsTemplate)
        {
            this.locations = locations;
            this.location = location;
            this.tilemap = tileMap;
            this.soliditySet = soliditySet;
            this.tileset = tileset;
            this.overlay = overlay;
            this.tilesetEditor = locationsTileset;
            this.paletteEditor = paletteEditor;
            this.locationsTemplate = locationsTemplate;
            this.commandStack = new CommandStack(true);
            selection = null;
            draggedTiles = null;
            overlay.Select = null;
            SetLocationImage();
        }
        private void SetLocationImage()
        {
            int[] pixels = tilemap.Pixels;
            if (location.Index < 2)
            {
                pictureBoxLocation.Size = new Size(4096 * zoom, 4096 * zoom);
                tilemapImage = Do.PixelsToImage(pixels, 4096, 4096);
            }
            else if (location.Index == 2)
            {
                pictureBoxLocation.Size = new Size(2048 * zoom, 2048 * zoom);
                tilemapImage = Do.PixelsToImage(pixels, 2048, 2048);
            }
            else
            {
                Size size = tilemap.Size_p;
                size.Width *= zoom;
                size.Height *= zoom;
                pictureBoxLocation.Size = size;
                tilemapImage = Do.PixelsToImage(pixels, 2048, 1024);
            }
            pictureBoxLocation.Invalidate();
        }
        private void UpdateCoordLabels()
        {
            int x = mousePosition.X;
            int y = mousePosition.Y;
            this.labelTileCoords.Text = "(x: " +
                System.Convert.ToString(mouseTilePosition.X) + ", y: " +
                System.Convert.ToString(mouseTilePosition.Y) + ") Tile  |  ";
            this.labelTileCoords.Text += "(x: " + x + ", y: " + y + ") Pixel";
        }
        // editing
        private bool CompareTiles(int x_, int y_, int layer)
        {
            for (int y = overlay.SelectTS.Y, b = y_; y < overlay.SelectTS.Terminal.Y; y += 16, b += 16)
            {
                for (int x = overlay.SelectTS.X, a = x_; x < overlay.SelectTS.Terminal.X; x += 16, a += 16)
                {
                    if (tilemap.GetTileNum(layer, a, b) != tileset.GetTileNum(layer, x / 16, y / 16))
                        return false;
                }
            }
            return true;
        }
        private void DrawBoundaries(Graphics g)
        {
            Rectangle r = new Rectangle(
                mousePosition.X / 16 * 16 * zoom, mousePosition.Y / 16 * 16 * zoom, 256 * zoom, 224 * zoom);
            Pen insideBorder = new Pen(Color.LightGray, 16);
            Pen edgeBorder = new Pen(Color.Black, 2);
            g.DrawRectangle(insideBorder, r.X - 8, r.Y - 8, r.Width + 16, r.Height + 16);
            g.DrawRectangle(edgeBorder, r.X - 16, r.Y - 16, r.Width + 32, r.Height + 32);
            g.DrawRectangle(edgeBorder, r);
        }
        private void DrawHoverBox(Graphics g)
        {
            int mouseOverSolidTileNum = 0;
            if (state.Solidity && mouseOverSolidTileNum == 0)  // if mod map empty, check if solidity set empty
                mouseOverSolidTileNum = Bits.GetShort(soliditySet.Tileset, mouseOverSolidTile * 2);
            Rectangle r = new Rectangle(mousePosition.X / 16 * 16 * zoom, mousePosition.Y / 16 * 16 * zoom, 16 * zoom, 16 * zoom);
            g.FillRectangle(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), r);
        }
        private void DrawTemplate(Graphics g, int x, int y)
        {
            if (template == null)
            {
                MessageBox.Show("Must select a template to paint to the location.", "ZONE DOCTOR");
                return;
            }
            Point tL = new Point(x / 16 * 16, y / 16 * 16);
            Point bR = new Point((x / 16 * 16) + template.Size.Width, (y / 16 * 16) + template.Size.Height);
            if (template.Even != (((tL.X / 16) % 2) == 0))
            {
                tL.X += 16;
                bR.X += 16;
            }
            int[][] tiles = new int[3][];
            tiles[0] = new int[template.Tilemaps[0].Length / 2];
            tiles[1] = new int[template.Tilemaps[1].Length / 2];
            tiles[2] = new int[template.Tilemaps[2].Length];
            for (int i = 0; i < tiles[0].Length; i++)
            {
                tiles[0][i] = Bits.GetShort(template.Tilemaps[0], i * 2);
                tiles[1][i] = Bits.GetShort(template.Tilemaps[1], i * 2);
                tiles[2][i] = template.Tilemaps[2][i];
            }
            commandStack.Push(new TilemapEdit(this.locations, tilemap, 0, tL, bR, tiles, true, true));
            commandCount++;
            tilemap.RedrawTilemap();
            SetLocationImage();
        }
        private void Draw(Graphics g, int x, int y)
        {
            Tilemap tilemap = this.tilemap;
            if (!state.Solidity)
            {
                int layer = tilesetEditor.Layer;
                // cancel if no selection in the tileset is made
                if (overlay.SelectTS == null)
                    return;
                // cancel if layer doesn't exist
                if (this.tileset.Tilesets_tiles[layer] == null)
                    return;
                // cancel if writing same tile over itself
                if (CompareTiles(x, y, layer))
                    return;
                Point location = new Point(x, y);
                Point terminal = new Point(
                    x + overlay.SelectTS.Width,
                    y + overlay.SelectTS.Height);
                commandStack.Push(
                    new TilemapEdit(
                        locations, tilemap, layer, location, terminal,
                        tilesetEditor.SelectedTiles.Copies, false, editAllLayers.Checked));
                commandCount++;
                // draw the tile
                Point p = new Point(x / 16 * 16, y / 16 * 16);
                Bitmap image = Do.PixelsToImage(
                    tilemap.GetPixels(p, overlay.SelectTS.Size),
                    overlay.SelectTS.Width, overlay.SelectTS.Height);
                p.X *= zoom;
                p.Y *= zoom;
                Rectangle rsrc = new Rectangle(0, 0, image.Width, image.Height);
                Rectangle rdst = new Rectangle(p.X, p.Y, (int)(image.Width * zoom), (int)(image.Height * zoom));
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                if (!state.BG)
                    pictureBoxLocation.Erase(rdst);
                g.DrawImage(image, rdst, rsrc, GraphicsUnit.Pixel);
            }
        }
        private void Erase(Graphics g, int x, int y)
        {
            Tilemap tilemap = this.tilemap;
            if (!state.Solidity)
            {
                int layer = tilesetEditor.Layer;
                // cancel if overwriting the same tile over itself
                if (!editAllLayers.Checked && this.tileset.Tilesets_tiles[layer] == null)
                    return;
                if (!editAllLayers.Checked && tilemap.GetTileNum(layer, x, y) == 0)
                    return;
                if (editAllLayers.Checked &&
                    tilemap.GetTileNum(0, x, y) == 0 &&
                    tilemap.GetTileNum(1, x, y) == 0 &&
                    tilemap.GetTileNum(2, x, y) == 0)
                    return;
                //
                commandStack.Push(
                    new TilemapEdit(
                        this.locations, tilemap, layer, new Point(x, y), new Point(x + 16, y + 16),
                        new int[][] { new int[] { 0 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0 } },
                        false, editAllLayers.Checked));
                commandCount++;
                Point p = new Point(x / 16 * 16, y / 16 * 16);
                Bitmap image = Do.PixelsToImage(tilemap.GetPixels(p, new Size(16, 16)), 16, 16);
                p.X *= zoom; p.Y *= zoom;
                Rectangle rsrc = new Rectangle(0, 0, image.Width, image.Height);
                Rectangle rdst = new Rectangle(p.X, p.Y, (int)(image.Width * zoom), (int)(image.Height * zoom));
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                if (!state.BG)
                    pictureBoxLocation.Erase(rdst);
                g.DrawImage(image, rdst, rsrc, GraphicsUnit.Pixel);
            }
        }
        private void Fill(int x, int y)
        {
            Tilemap tilemap = this.tilemap;
            if (!state.Solidity)
            {
                int layer = tilesetEditor.Layer;
                // cancel if no selection in the tileset is made
                if (overlay.SelectTS == null)
                    return;
                // cancel if layer doesn't exist
                if (tileset.Tilesets_tiles[layer] == null)
                    return;
                // cancel if writing same tile over itself
                if (CompareTiles(x, y, layer))
                    return;
                // store changes
                int[][] changes = new int[3][];
                if (tilemap.Tilemaps_Bytes[0] != null) changes[0] = new int[tilemap.Tilemaps_Bytes[0].Length];
                if (tilemap.Tilemaps_Bytes[1] != null) changes[1] = new int[tilemap.Tilemaps_Bytes[1].Length];
                if (tilemap.Tilemaps_Bytes[2] != null) changes[2] = new int[tilemap.Tilemaps_Bytes[2].Length];
                for (int l = 0; l < 3; l++)
                {
                    if (changes[l] == null) continue;
                    for (int i = 0; i < changes[l].Length && i < tilemap.Tilemaps_Tiles[l].Length; i++)
                    {
                        if (tilemap.Tilemaps_Tiles[l][i] == null) continue;
                        changes[l][i] = tilemap.Tilemaps_Tiles[l][i].Index;
                    }
                }
                // fill up tiles
                Point location = new Point(0, 0);
                Point terminal = new Point(locationMap.Width_p[layer], locationMap.Height_p[layer]);
                int[] fillTile = tilesetEditor.SelectedTiles.Copies[layer];
                int tile = tilemap.GetTileNum(layer, x, y);
                int width = locationMap.Width_p[layer];
                int height = locationMap.Height_p[layer];
                int vwidth = overlay.SelectTS.Width / 16;
                int vheight = overlay.SelectTS.Height / 16;
                //
                if ((Control.ModifierKeys & Keys.Control) == 0)
                    Do.Fill(changes, layer, editAllLayers.Checked, tile, fillTile,
                        x / 16, y / 16, width / 16, height / 16, vwidth, vheight, "");
                else
                    // non-contiguous fill
                    for (int d = 0; d < height / 16; d += vheight)
                    {
                        for (int c = 0; c < width / 16; c += vwidth)
                        {
                            for (int b = 0; b < vheight; b++)
                            {
                                if (changes[layer][(d + b) * (width / 16) + c] != tile)
                                    break;
                                for (int a = 0; a < vwidth; a++)
                                {
                                    if (changes[layer][(d + b) * (width / 16) + c + a] != tile)
                                        break;
                                    changes[layer][(d + b) * (width / 16) + c + a] = fillTile[b * vwidth + a];
                                }
                            }
                        }
                    }
                commandStack.Push(
                    new TilemapEdit(locations, tilemap, layer, location, terminal, changes, false, false));
            }
        }
        public void Swap(int indexA, int indexB, int layer)
        {
            if (indexA < 0 && indexB < 0 && indexA == indexB)
                return;
            int[][] changes = new int[3][];
            if (tilemap.Tilemaps_Bytes[0] != null) changes[0] = new int[tilemap.Tilemaps_Bytes[0].Length];
            if (tilemap.Tilemaps_Bytes[1] != null) changes[1] = new int[tilemap.Tilemaps_Bytes[1].Length];
            if (tilemap.Tilemaps_Bytes[2] != null) changes[2] = new int[tilemap.Tilemaps_Bytes[2].Length];
            for (int l = 0; l < 3; l++)
            {
                if (changes[l] == null) continue;
                for (int i = 0; i < changes[l].Length && i < tilemap.Tilemaps_Tiles[l].Length; i++)
                {
                    if (tilemap.Tilemaps_Tiles[l][i] == null) continue;
                    changes[l][i] = tilemap.Tilemaps_Tiles[l][i].Index;
                }
            }
            byte[] tilemap_bytes = tilemap.Tilemaps_Bytes[layer];
            //
            for (int i = 0; i < tilemap_bytes.Length; i++)
            {
                if (tilemap_bytes[i] == indexA)
                    changes[layer][i] = indexB;
                if (tilemap_bytes[i] == indexB)
                    changes[layer][i] = indexA;
            }
            //
            Point location = new Point(0, 0);
            Point terminal = new Point(locationMap.Width_p[layer], locationMap.Height_p[layer]);
            commandStack.Push(
                new TilemapEdit(locations, tilemap, layer, location, terminal, changes, false, false));
            commandStack.Push(1);
            indexA = -1;
            indexB = -1;
            SetLocationImage();
        }
        private void SelectColor(int x, int y)
        {
            Tilemap tilemap = this.tilemap;
            int layer = tilemap.GetPixelLayer(x, y);
            int tileNum = (y / 16) * (width / 16) + (x / 16);
            int placement = ((x % 16) / 8) + (((y % 16) / 8) * 2);
            Tile tile = this.tileset.Tilesets_tiles[layer][tilemap.GetTileNum(layer, x, y)];
            Subtile subtile = tile.Subtiles[placement];
            int multiplier = layer < 2 ? 16 : 4;
            int index = ((y % 16) % 8) * 8 + ((x % 16) % 8);
            int color = (subtile.Palette * multiplier) + subtile.Colors[index];
            if (color < paletteEditor.StartRow * 16)
                color = paletteEditor.StartRow * 16;
            paletteEditor.CurrentColor = color;
            paletteEditor.Show();
        }
        private void Undo()
        {
            if (!state.Solidity)
            {
                commandStack.UndoCommand();
                SetLocationImage();
            }
        }
        private void Redo()
        {
            if (!state.Solidity)
            {
                commandStack.RedoCommand();
                SetLocationImage();
            }
        }
        private void Cut()
        {
            if (overlay.Select == null || overlay.Select.Size == new Size(0, 0))
                return;
            if (state.Solidity)
                return;
            Copy();
            Delete();
            if (commandCount > 0)
            {
                commandStack.Push(commandCount);
                commandCount = 0;
            }
        }
        private void Copy()
        {
            if (overlay.Select == null || overlay.Select.Size == new Size(0, 0))
                return;
            if (state.Solidity)
                return;
            if (draggedTiles != null)
            {
                this.copiedTiles = draggedTiles;
                return;
            }
            int layer = tilesetEditor.Layer;
            if (editAllLayers.Checked)
                selection = Do.PixelsToImage(
                    tilemap.GetPixels(overlay.Select.Location, overlay.Select.Size),
                    overlay.Select.Width, overlay.Select.Height);
            else
                selection = Do.PixelsToImage(
                    tilemap.GetPixels(layer, overlay.Select.Location, overlay.Select.Size),
                    overlay.Select.Width, overlay.Select.Height);
            int[][] copiedTiles = new int[3][];
            this.copiedTiles = new CopyBuffer(overlay.Select.Width, overlay.Select.Height);
            for (int l = 0; l < 3; l++)
            {
                copiedTiles[l] = new int[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
                for (int y = 0; y < overlay.Select.Height / 16; y++)
                {
                    for (int x = 0; x < overlay.Select.Width / 16; x++)
                    {
                        int tileX = overlay.Select.X + (x * 16);
                        int tileY = overlay.Select.Y + (y * 16);
                        copiedTiles[l][y * (overlay.Select.Width / 16) + x] = tilemap.GetTileNum(l, tileX, tileY);
                    }
                }
            }
            this.copiedTiles.Copies = copiedTiles;
        }
        /// <summary>
        /// Start dragging a current selection.
        /// </summary>
        private void Drag()
        {
            if (overlay.Select == null || overlay.Select.Size == new Size(0, 0))
                return;
            if (!state.Solidity)
            {
                int layer = tilesetEditor.Layer;
                if (editAllLayers.Checked)
                    selection = Do.PixelsToImage(
                        tilemap.GetPixels(overlay.Select.Location, overlay.Select.Size),
                        overlay.Select.Width, overlay.Select.Height);
                else
                    selection = Do.PixelsToImage(
                        tilemap.GetPixels(layer, overlay.Select.Location, overlay.Select.Size),
                        overlay.Select.Width, overlay.Select.Height);
                int[][] copiedTiles = new int[3][];
                this.draggedTiles = new CopyBuffer(overlay.Select.Width, overlay.Select.Height);
                for (int l = 0; l < 3; l++)
                {
                    if (tilemap.Tilemaps_Bytes[l] == null) continue;
                    copiedTiles[l] = new int[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
                    for (int y = 0; y < overlay.Select.Height / 16; y++)
                    {
                        for (int x = 0; x < overlay.Select.Width / 16; x++)
                        {
                            int tileX = overlay.Select.X + (x * 16);
                            int tileY = overlay.Select.Y + (y * 16);
                            copiedTiles[l][y * (overlay.Select.Width / 16) + x] = tilemap.GetTileNum(l, tileX, tileY);
                        }
                    }
                }
                this.draggedTiles.Copies = copiedTiles;
            }
            Delete();
        }
        private void Paste(Point location, CopyBuffer buffer)
        {
            if (state.Solidity)
                return;
            if (buffer == null)
                return;
            if (!buttonEditSelect.Checked)
                buttonEditSelect.PerformClick();
            state.Move = true;
            // now dragging a new selection
            draggedTiles = buffer;
            overlay.Select = new Overlay.Selection(16, location, buffer.Size, Picture);
            pictureBoxLocation.Invalidate();
            defloating = false;
        }
        /// <summary>
        /// Defloat either a dragged selection or a newly pasted selection.
        /// </summary>
        /// <param name="buffer">The dragged selection or the newly pasted selection.</param>
        private void Defloat(CopyBuffer buffer)
        {
            if (state.Solidity)
                return;
            if (buffer == null)
                return;
            if (overlay.Select == null)
                return;
            Point location = new Point();
            location.X = overlay.Select.X / 16 * 16;
            location.Y = overlay.Select.Y / 16 * 16;
            int layer = tilesetEditor.Layer;
            Point terminal = new Point(location.X + buffer.Width, location.Y + buffer.Height);
            commandStack.Push(
                new TilemapEdit(locations, tilemap, layer, location, terminal, buffer.Copies, true, editAllLayers.Checked));
            commandStack.Push(commandCount + 1);
            commandCount = 0;
            SetLocationImage();
            defloating = true;
        }
        /// <summary>
        /// Defloats pasted tiles and clears the selection
        /// </summary>
        private void Defloat()
        {
            if (copiedTiles != null && !defloating)
                Defloat(copiedTiles);
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            state.Move = false;
            overlay.Select = null;
            Cursor.Position = Cursor.Position;
        }
        private void Delete()
        {
            if (overlay.Select == null)
                return;
            if (overlay.Select.Size == new Size(0, 0))
                return;
            if (!state.Solidity)
            {
                int layer = tilesetEditor.Layer;
                if (tileset.Tilesets_tiles[layer] == null)
                    return;
                if (overlay.Select == null)
                    return;
                Point location = overlay.Select.Location;
                Point terminal = overlay.Select.Terminal;
                int[][] changes = new int[][]{
                    new int[overlay.Select.Width * overlay.Select.Height],
                    new int[overlay.Select.Width * overlay.Select.Height],
                    new int[overlay.Select.Width * overlay.Select.Height],
                    new int[overlay.Select.Width * overlay.Select.Height]};
                // Verify layer before creating command
                commandStack.Push(
                    new TilemapEdit(
                        locations, tilemap, layer, location, terminal,
                        changes, false, editAllLayers.Checked));
                SetLocationImage();
            }
        }
        private void Flip(string type)
        {
            if (tilesetEditor.Layer != 2)
                return;
            if (draggedTiles != null)
                Defloat(draggedTiles);
            if (overlay.Select == null)
                return;
            int x_ = overlay.Select.Location.X / 16;
            int y_ = overlay.Select.Location.Y / 16;
            CopyBuffer buffer = new CopyBuffer(overlay.Select.Width, overlay.Select.Height);
            Tile[] tiles = new Tile[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
            for (int y = 0; y < overlay.Select.Height / 16; y++)
            {
                for (int x = 0; x < overlay.Select.Width / 16; x++)
                {
                    tiles[y * (overlay.Select.Width / 16) + x] =
                        tilemap.Tilemaps_Tiles[tilesetEditor.Layer][(y + y_) * 16 + x + x_].Copy();
                }
            }
            if (type == "mirror")
                Do.FlipHorizontal(tiles, overlay.Select.Width / 16, overlay.Select.Height / 16, false);
            else if (type == "invert")
                Do.FlipVertical(tiles, overlay.Select.Width / 16, overlay.Select.Height / 16, false);
            int[][] copiedTiles = new int[3][];
            if (tilemap.Tilemaps_Bytes[2] == null)
                return;
            copiedTiles[2] = new int[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
            for (int y = 0; y < overlay.Select.Height / 16; y++)
            {
                for (int x = 0; x < overlay.Select.Width / 16; x++)
                {
                    int tileX = overlay.Select.X + (x * 16);
                    int tileY = overlay.Select.Y + (y * 16);
                    copiedTiles[2][y * (overlay.Select.Width / 16) + x] = tilemap.GetTileNum(2, tileX, tileY);
                }
            }
            buffer.Copies = copiedTiles;
            Defloat(buffer);
        }
        #endregion
        #region Event Handlers
        // main
        private void pictureBoxLocation_Paint(object sender, PaintEventArgs e)
        {
            RectangleF clone = e.ClipRectangle;
            SizeF remainder = new SizeF((int)(clone.Width % zoom), (int)(clone.Height % zoom));
            clone.Location = new PointF((int)(clone.X / zoom), (int)(clone.Y / zoom));
            clone.Size = new SizeF((int)(clone.Width / zoom), (int)(clone.Height / zoom));
            clone.Width += (int)(remainder.Width * zoom) + 1;
            clone.Height += (int)(remainder.Height * zoom) + 1;
            RectangleF source, dest;
            float[][] matrixItems ={ 
               new float[] {1, 0, 0, 0, 0},
               new float[] {0, 1, 0, 0, 0},
               new float[] {0, 0, 1, 0, 0},
               new float[] {0, 0, 0, (float)overlayOpacity.Value / 100, 0}, 
               new float[] {0, 0, 0, 0, 1}};
            ColorMatrix cm = new ColorMatrix(matrixItems);
            ImageAttributes ia = new ImageAttributes();
            if (overlayOpacity.Value < 100)
                ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Rectangle rdst = new Rectangle(0, 0, zoom * width, zoom * height);
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            if (location.Index > 2 && state.BG)
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.ClipRectangle);
            if (tilemapImage != null)
            {
                clone.Width = Math.Min(tilemapImage.Width, clone.X + clone.Width) - clone.X;
                clone.Height = Math.Min(tilemapImage.Height, clone.Y + clone.Height) - clone.Y;
                source = clone; source.Location = new PointF(0, 0);
                dest = new RectangleF((int)(clone.X * zoom), (int)(clone.Y * zoom), (int)(clone.Width * zoom), (int)(clone.Height * zoom));
                e.Graphics.DrawImage(tilemapImage.Clone(clone, PixelFormat.DontCare), dest, source, GraphicsUnit.Pixel);
            }
            if (state.Move && selection != null)
            {
                Rectangle rsrc = new Rectangle(0, 0, overlay.Select.Width, overlay.Select.Height);
                rdst = new Rectangle(
                    overlay.Select.X * zoom, overlay.Select.Y * zoom,
                    rsrc.Width * zoom, rsrc.Height * zoom);
                e.Graphics.DrawImage(new Bitmap(selection), rdst, rsrc, GraphicsUnit.Pixel);
            }
            if (state.Solidity && state.Layer1 && location.Index != 2)
            {
                if (location.Index < 2)
                    overlay.DrawSolidityMap(e.Graphics, (WorldTilemap)tilemap, soliditySet, zoom);
                else
                    overlay.DrawSolidityMap(e.Graphics, location.LocationMap, (LocationTilemap)tilemap, soliditySet, zoom);
            }
            if (state.Zones && location.Index < 2)
                overlay.DrawZoneGrid(e.Graphics, Color.White, pictureBoxLocation.Size, new Size(512, 512), zoom);
            if (state.Exits)
            {
                overlay.DrawLocationExits(e.Graphics, exits, zoom);
                if (tags.Checked)
                    overlay.DrawLocationExitTags(e.Graphics, exits, zoom);
            }
            if (state.Events)
            {
                overlay.DrawLocationEvents(e.Graphics, events, zoom);
                if (tags.Checked)
                    overlay.DrawLocationEventTags(e.Graphics, events, zoom);
            }
            if (state.NPCs && location.Index > 2)
            {
                overlay.DrawLocationNPCs(e.Graphics, npcs, zoom);
                if (tags.Checked)
                    overlay.DrawLocationNPCTags(e.Graphics, npcs, zoom);
            }
            if (state.Treasures && location.Index > 2)
            {
                overlay.DrawLocationTreasures(e.Graphics, treasures, zoom);
                if (tags.Checked)
                    overlay.DrawLocationTreasureTags(e.Graphics, treasures, zoom);
            }
            if (!state.Dropper && mouseEnter)
                DrawHoverBox(e.Graphics);
            if (state.TileGrid)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBoxLocation.Size, new Size(16, 16), zoom, true);
            if (state.Mask && locationMap.MaskHighX == 0 && locationMap.MaskHighY == 0)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(75, Color.Magenta)), e.ClipRectangle);
            else if (state.Mask)
                overlay.DrawLocationMask(e.Graphics, new Point(locationMap.MaskHighX, locationMap.MaskHighY), new Point(0, 0), zoom);
            if (state.ShowBoundaries && mouseEnter && location.Index > 2)
                overlay.DrawBoundaries(e.Graphics, mouseTilePosition, new Point(locationMap.MaskHighX, locationMap.MaskHighY), zoom);
            else if (state.ShowBoundaries && mouseEnter)
                overlay.DrawBoundaries(e.Graphics, mouseTilePosition, new Point(256, 256), zoom);
            if (state.Select && overlay.Select != null)
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                if (state.TileGrid)
                    overlay.Select.DrawSelectionBox(e.Graphics, zoom, Color.Yellow);
                else
                    overlay.Select.DrawSelectionBox(e.Graphics, zoom);
            }
        }
        private void pictureBoxLocation_MouseDown(object sender, MouseEventArgs e)
        {
            // in case the tileset selection was dragged
            if (tilesetEditor.DraggedTiles != null)
                tilesetEditor.Defloat(tilesetEditor.DraggedTiles);
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X / zoom, width));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height));
            mouseDownObject = null;
            #region Zooming
            autoScrollPos.X = Math.Abs(panelLocationPicture.AutoScrollPosition.X);
            autoScrollPos.Y = Math.Abs(panelLocationPicture.AutoScrollPosition.Y);
            if ((buttonZoomIn.Checked && e.Button == MouseButtons.Left) || (buttonZoomOut.Checked && e.Button == MouseButtons.Right))
            {
                pictureBoxLocation.ZoomIn(e.X, e.Y);
                return;
            }
            else if ((buttonZoomOut.Checked && e.Button == MouseButtons.Left) || (buttonZoomIn.Checked && e.Button == MouseButtons.Right))
            {
                pictureBoxLocation.ZoomOut(e.X, e.Y);
                return;
            }
            #endregion
            if (e.Button == MouseButtons.Right)
                return;
            #region Drawing, Erasing, Selecting
            // if moving an object and outside of it, paste it
            if (state.Move && mouseOverObject != "selection")
            {
                // if copied tiles were pasted and not dragging a non-copied selection
                if (copiedTiles != null && draggedTiles == null)
                    Defloat(copiedTiles);
                if (draggedTiles != null)
                {
                    Defloat(draggedTiles);
                    draggedTiles = null;
                }
                state.Move = false;
            }
            if (state.Select)
            {
                //panelLocationPicture.Focus();
                // if we're not inside a current selection to move it, create a new selection
                if (mouseOverObject != "selection")
                    overlay.Select = new Overlay.Selection(16, x / 16 * 16, y / 16 * 16, 16, 16, Picture);
                // otherwise, start dragging current selection
                else if (mouseOverObject == "selection")
                {
                    mouseDownObject = "selection";
                    mouseDownPosition = overlay.Select.MousePosition(x, y);
                    if (!state.Move)    // only do this if the current selection has not been initially moved
                    {
                        state.Move = true;
                        Drag();
                    }
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    findInTileset.PerformClick();
                    return;
                }
                if (state.Dropper)
                {
                    SelectColor(x, y);
                    return;
                }
                if (state.Template)
                {
                    DrawTemplate(pictureBoxLocation.CreateGraphics(), x, y);
                    panelLocationPicture.AutoScrollPosition = autoScrollPos;
                    return;
                }
                if (state.Draw)
                {
                    Draw(pictureBoxLocation.CreateGraphics(), x, y);
                    panelLocationPicture.AutoScrollPosition = autoScrollPos;
                    return;
                }
                if (state.Fill)
                {
                    Fill(x, y);
                    panelLocationPicture.AutoScrollPosition = autoScrollPos;
                    return;
                }
                if (state.Erase)
                {
                    Erase(pictureBoxLocation.CreateGraphics(), x, y);
                    panelLocationPicture.AutoScrollPosition = autoScrollPos;
                    return;
                }
            }
            #endregion
            #region Object Selection
            if (!state.Template && !state.Draw && !state.Select && !state.Erase && !state.Fill && e.Button == MouseButtons.Left)
            {
                if (state.Mask && mouseOverObject != null && mouseOverObject.StartsWith("mask"))
                {
                    locations.TabControl.SelectedIndex = 1;
                    mouseDownObject = mouseOverObject;
                }
                if (state.Exits && mouseOverObject == "exit")
                {
                    locations.TabControl.SelectedIndex = 4;
                    mouseDownObject = "exit";
                    mouseDownExitField = mouseOverExitField;
                    exits.CurrentExit = mouseDownExitField;
                    exits.SelectedExit = mouseDownExitField;
                    locations.ExitListBox.SelectedIndex = exits.CurrentExit;
                }
                if (state.Events && mouseOverObject == "event" && mouseDownObject == null)
                {
                    locations.TabControl.SelectedIndex = 4;
                    mouseDownObject = "event";
                    mouseDownEventField = mouseOverEventField;
                    events.CurrentEvent = mouseDownEventField;
                    events.SelectedEvent = mouseDownEventField;
                    locations.EventListBox.SelectedIndex = events.CurrentEvent;
                }
                if (state.NPCs && mouseOverObject == "npc" && mouseDownObject == null)
                {
                    locations.TabControl.SelectedIndex = 2;
                    mouseDownObject = "npc";
                    mouseDownNPC = mouseOverNPC;
                    npcs.CurrentNPC = mouseDownNPC;
                    npcs.SelectedNPC = mouseDownNPC;
                    locations.NpcListBox.SelectedIndex = npcs.CurrentNPC;
                }
                if (state.Treasures && mouseOverObject == "treasure" && mouseDownObject == null)
                {
                    locations.TabControl.SelectedIndex = 3;
                    mouseDownObject = "treasure";
                    mouseDownTreasure = mouseOverTreasure;
                    treasures.CurrentTreasure = mouseOverTreasure;
                    treasures.SelectedTreasure = mouseOverTreasure;
                    locations.TreasureListBox.SelectedIndex = treasures.CurrentTreasure;
                }
            }
            #endregion
            panelLocationPicture.AutoScrollPosition = autoScrollPos;
            pictureBoxLocation.Invalidate();
        }
        private void pictureBoxLocation_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int x = Math.Max(0, Math.Min(e.X / zoom, width));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height));
            // drawing 1 or more tiles
            if (!state.Move && !state.Solidity && commandCount > 0)
            {
                this.commandStack.Push(commandCount);
                commandCount = 0;
            }
            mouseDownExitField = -1;
            mouseDownEventField = -1;
            mouseDownNPC = -1;
            mouseDownTreasure = -1;
            mouseDownObject = null;
            if (state.Draw || state.Erase || state.Fill)
            {
                if (!state.Solidity)
                {
                    SetLocationImage();
                }
                else
                    pictureBoxLocation.Invalidate();
            }
            pictureBoxLocation.Focus(locations);
            locations.Modified = true;
        }
        private void pictureBoxLocation_MouseMove(object sender, MouseEventArgs e)
        {
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X / zoom, width));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height));
            // must first check if within same bounds as last call of MouseMove event
            mouseWithinSameBounds = mouseOverTile == (y / 16 * 64) + (x / 16);
            // now set the properties
            mousePosition = new Point(x, y);
            mouseTilePosition = new Point(x / 16, y / 16);
            mouseOverTile = (y / 16 * 64) + (x / 16);
            mouseOverObject = null;
            UpdateCoordLabels();
            #region Highlight in tileset
            if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                int layer_ = 0;
                int index = tilemap.GetTileNum(0, mousePosition.X, mousePosition.Y, true);
                if (index == 0)
                {
                    layer_++;
                    index = tilemap.GetTileNum(1, mousePosition.X, mousePosition.Y, true);
                }
                if (index == 0)
                {
                    layer_++;
                    index = tilemap.GetTileNum(2, mousePosition.X, mousePosition.Y, true);
                }
                tilesetEditor.Layer = layer_;
                tilesetEditor.mousePosition = new Point(index % 16 * 16, index / 16 * 16);
                tilesetEditor.PictureBox.Invalidate();
            }
            #endregion
            #region Zooming
            // if either zoom button is checked, don't do anything else
            if (buttonZoomIn.Checked || buttonZoomOut.Checked)
            {
                pictureBoxLocation.Invalidate();
                return;
            }
            #endregion
            #region Dropper
            // show zoom box for selecting colors
            if (state.Dropper)
                return;
            #endregion
            #region Drawing, erasing, selecting
            if (state.Select)
            {
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.Select != null)
                {
                    // cancel if within same bounds as last call
                    if (overlay.Select.Final == new Point(x + 16, y + 16))
                        return;
                    // otherwise, set the lower right edge of the selection
                    overlay.Select.Final = new Point(
                        Math.Min(x + 16, pictureBoxLocation.Width),
                        Math.Min(y + 16, pictureBoxLocation.Height));
                }
                // if dragging the current selection
                else if (e.Button == MouseButtons.Left && mouseDownObject == "selection")
                    overlay.Select.Location = new Point(x / 16 * 16 - mouseDownPosition.X, y / 16 * 16 - mouseDownPosition.Y);
                // if mouse not clicked and within the current selection
                else if (e.Button == MouseButtons.None && overlay.Select != null && overlay.Select.MouseWithin(x, y))
                {
                    mouseOverObject = "selection";
                    pictureBoxLocation.Cursor = Cursors.SizeAll;
                }
                else
                    pictureBoxLocation.Cursor = Cursors.Cross;
                pictureBoxLocation.Invalidate();
                return;
            }
            if (!state.Solidity)
            {
                if (state.Draw && e.Button == MouseButtons.Left)
                {
                    Draw(pictureBoxLocation.CreateGraphics(), x, y);
                    return;
                }
                else if (state.Erase && e.Button == MouseButtons.Left)
                {
                    Erase(pictureBoxLocation.CreateGraphics(), x, y);
                    return;
                }
                else if (state.Fill && e.Button == MouseButtons.Left)
                {
                    Fill(x, y);
                    return;
                }
            }
            else if (state.Solidity)
            {
                if (!mouseWithinSameBounds)
                {
                    if (state.Draw && e.Button == MouseButtons.Left)
                        Draw(pictureBoxLocation.CreateGraphics(), x, y);
                    if (state.Erase && e.Button == MouseButtons.Left)
                        Erase(pictureBoxLocation.CreateGraphics(), x, y);
                    if (state.Fill && e.Button == MouseButtons.Left)
                        Fill(x, y);
                }
            }
            #endregion
            #region Objects
            if (!state.Template && !state.Draw && !state.Select && !state.Erase && !state.Fill && !state.Dropper)
            {
                #region Check if dragging a field
                if (mouseDownObject != null && e.Button == MouseButtons.Left)  // if dragging a field
                {
                    if (mouseDownObject == "maskSE")
                    {
                        locations.LayerMaskHighX.Value = Math.Max(mouseTilePosition.X, 1);
                        locations.LayerMaskHighY.Value = Math.Max(mouseTilePosition.Y, 1);
                    }
                    if (mouseDownObject == "maskE")
                        locations.LayerMaskHighX.Value = Math.Max(mouseTilePosition.X, 1);
                    if (mouseDownObject == "maskS")
                        locations.LayerMaskHighY.Value = Math.Max(mouseTilePosition.Y, 1);
                    if (mouseDownObject == "exit")
                    {
                        if (exits.Width == 0)
                        {
                            locations.ExitX.Value = mouseTilePosition.X;
                            locations.ExitY.Value = mouseTilePosition.Y;
                        }
                        else if (exits.F == 0)
                        {
                            locations.ExitX.Value = Math.Max(mouseTilePosition.X - mouseOverDelta.X, 0);
                            locations.ExitY.Value = mouseTilePosition.Y;
                        }
                        else
                        {
                            locations.ExitX.Value = mouseTilePosition.X;
                            locations.ExitY.Value = Math.Max(mouseTilePosition.Y - mouseOverDelta.Y, 0);
                        }
                    }
                    if (mouseDownObject == "event")
                    {
                        locations.EventX.Value = mouseTilePosition.X;
                        locations.EventY.Value = mouseTilePosition.Y;
                    }
                    if (mouseDownObject == "npc")
                    {
                        locations.NpcX.Value = mouseTilePosition.X;
                        locations.NpcY.Value = mouseTilePosition.Y + mouseOverDelta.Y;
                    }
                    if (mouseDownObject == "treasure")
                    {
                        locations.TreasureX.Value = mouseTilePosition.X;
                        locations.TreasureY.Value = mouseTilePosition.Y;
                    }
                    pictureBoxLocation.Invalidate();
                    return;
                }
                #endregion
                #region Check if over an object
                else
                {
                    pictureBoxLocation.Cursor = Cursors.Arrow;
                    if (state.Mask)
                    {
                        if (mouseTilePosition.X == locationMap.MaskHighX && mouseTilePosition.Y == locationMap.MaskHighY)
                        {
                            pictureBoxLocation.Cursor = Cursors.SizeNWSE;
                            mouseOverObject = "maskSE";
                        }
                        else if (mouseTilePosition.X == locationMap.MaskHighX &&
                            mouseTilePosition.Y <= locationMap.MaskHighY && mouseTilePosition.Y >= 0)
                        {
                            pictureBoxLocation.Cursor = Cursors.SizeWE;
                            mouseOverObject = "maskE";
                        }
                        else if (mouseTilePosition.Y == locationMap.MaskHighY &&
                            mouseTilePosition.X <= locationMap.MaskHighX && mouseTilePosition.X >= 0)
                        {
                            pictureBoxLocation.Cursor = Cursors.SizeNS;
                            mouseOverObject = "maskS";
                        }
                    }
                    if (state.Exits && exits.Count != 0)
                    {
                        int index_ext = 0;
                        foreach (Exit exit in exits.Exits)
                        {
                            if (exit.Width == 0)
                            {
                                if (exit.X == mouseTilePosition.X &&
                                    exit.Y == mouseTilePosition.Y)
                                {
                                    this.pictureBoxLocation.Cursor = Cursors.Hand;
                                    mouseOverExitField = index_ext;
                                    mouseOverObject = "exit";
                                    break;
                                }
                            }
                            else
                            {
                                if (exit.F == 0 && exit.Y == mouseTilePosition.Y)
                                {
                                    if (exit.X == mouseTilePosition.X ||
                                        (mouseTilePosition.X >= exit.X && mouseTilePosition.X <= exit.X + exit.Width))
                                    {
                                        mouseOverDelta = new Point(mouseTilePosition.X - exit.X, 0);
                                        this.pictureBoxLocation.Cursor = Cursors.Hand;
                                        mouseOverExitField = index_ext;
                                        mouseOverObject = "exit";
                                        break;
                                    }
                                }
                                else if (exit.F == 1 && exit.X == mouseTilePosition.X)
                                {
                                    if (exit.Y == mouseTilePosition.Y ||
                                        (mouseTilePosition.Y >= exit.Y && mouseTilePosition.Y <= exit.Y + exit.Width))
                                    {
                                        mouseOverDelta = new Point(0, mouseTilePosition.Y - exit.Y);
                                        this.pictureBoxLocation.Cursor = Cursors.Hand;
                                        mouseOverExitField = index_ext;
                                        mouseOverObject = "exit";
                                        break;
                                    }
                                }
                            }
                            this.pictureBoxLocation.Cursor = Cursors.Arrow;
                            mouseOverExitField = 0;
                            mouseOverObject = null;
                            index_ext++;
                        }
                    }
                    if (state.Events && events.Count != 0 && mouseOverObject == null)
                    {
                        int index_evt = 0;
                        foreach (Event EVENT in events.Events)
                        {
                            if (EVENT.X == mouseTilePosition.X &&
                                EVENT.Y == mouseTilePosition.Y)
                            {
                                this.pictureBoxLocation.Cursor = Cursors.Hand;
                                mouseOverEventField = index_evt;
                                mouseOverObject = "event";
                                break;
                            }
                            else
                            {
                                this.pictureBoxLocation.Cursor = Cursors.Arrow;
                                mouseOverEventField = 0;
                                mouseOverObject = null;
                            }
                            index_evt++;
                        }
                    }
                    if (state.NPCs && npcs.Count != 0 && mouseOverObject == null)
                    {
                        int index_npc = 0;
                        foreach (NPC npc in npcs.Npcs)
                        {
                            if (npc.X == mouseTilePosition.X &&
                                (npc.Y == mouseTilePosition.Y || npc.Y - 1 == mouseTilePosition.Y))
                            {
                                mouseOverDelta = new Point(0, npc.Y - mouseTilePosition.Y);
                                this.pictureBoxLocation.Cursor = System.Windows.Forms.Cursors.Hand;
                                mouseOverNPC = index_npc;
                                mouseOverObject = "npc";
                                break;
                            }
                            else
                            {
                                this.pictureBoxLocation.Cursor = System.Windows.Forms.Cursors.Arrow;
                                mouseOverNPC = 0;
                                mouseOverObject = null;
                            }
                            index_npc++;
                        }
                    }
                    if (state.Treasures && treasures.Treasures.Count != 0 && mouseOverObject == null)
                    {
                        int index_trs = 0;
                        foreach (Treasure treasure in treasures.Treasures)
                        {
                            if (treasure.X == mouseTilePosition.X &&
                                treasure.Y == mouseTilePosition.Y)
                            {
                                this.pictureBoxLocation.Cursor = System.Windows.Forms.Cursors.Hand;
                                mouseOverTreasure = index_trs;
                                mouseOverObject = "treasure";
                                break;
                            }
                            else
                            {
                                this.pictureBoxLocation.Cursor = System.Windows.Forms.Cursors.Arrow;
                                mouseOverTreasure = 0;
                                mouseOverObject = null;
                            }
                            index_trs++;
                        }
                    }
                }
                #endregion
            }
            #endregion
            if (!state.Solidity &&
                !state.NPCs && !state.Treasures && !state.Exits && !state.Events && !mouseWithinSameBounds)
                pictureBoxLocation.Invalidate();
            else if (state.Solidity ||
                state.NPCs || state.Treasures || state.Exits || state.Events)
                pictureBoxLocation.Invalidate();
        }
        private void pictureBoxLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //toolStripMenuItem5.PerformClick();
        }
        private void pictureBoxLocation_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            //tilesetEditor.HiliteTile = true;
            pictureBoxLocation.Focus(locations);
            pictureBoxLocation.Invalidate();
        }
        private void pictureBoxLocation_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            pictureBoxLocation.Invalidate();
        }
        private void pictureBoxLocation_MouseHover(object sender, EventArgs e)
        {
            //pictureBoxLocation.Invalidate();
        }
        private void pictureBoxLocation_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.G: toggleCartGrid.PerformClick(); break;
                case Keys.A: editAllLayers.PerformClick(); break;
                case Keys.D: buttonEditDraw.PerformClick(); break;
                case Keys.E: buttonEditErase.PerformClick(); break;
                case Keys.P: buttonEditDropper.PerformClick(); break;
                case Keys.F: buttonEditFill.PerformClick(); break;
                case Keys.S: buttonEditSelect.PerformClick(); break;
                case Keys.T: buttonEditTemplate.PerformClick(); break;
                case Keys.Control | Keys.V: buttonEditPaste.PerformClick(); break;
                case Keys.Control | Keys.C: buttonEditCopy.PerformClick(); break;
                case Keys.Delete: buttonEditDelete.PerformClick(); break;
                case Keys.Control | Keys.X: buttonEditCut.PerformClick(); break;
                case Keys.Control | Keys.D: Defloat(); break;
                case Keys.Control | Keys.A: selectAll.PerformClick(); break;
                case Keys.Control | Keys.Z: buttonEditUndo.PerformClick(); break;
                case Keys.Control | Keys.Y: buttonEditRedo.PerformClick(); break;
                case Keys.Control | Keys.D1: tilesetEditor.Layer = 0; break;
                case Keys.Control | Keys.D2: tilesetEditor.Layer = 1; break;
                case Keys.Control | Keys.D3:
                    if (tileset.Tilesets_tiles[2] != null)
                        tilesetEditor.Layer = 2; break;
            }
        }
        private void panelLocationPicture_Scroll(object sender, ScrollEventArgs e)
        {
            autoScrollPos.X = Math.Abs(panelLocationPicture.AutoScrollPosition.X);
            autoScrollPos.Y = Math.Abs(panelLocationPicture.AutoScrollPosition.Y);
            pictureBoxLocation.Invalidate();
            panelLocationPicture.Invalidate();
        }
        //toolstrip buttons
        private void buttonToggleCartGrid_Click(object sender, EventArgs e)
        {
            state.TileGrid = toggleCartGrid.Checked;
            pictureBoxLocation.Invalidate();
        }
        private void buttonToggleBG_Click(object sender, EventArgs e)
        {
            state.BG = buttonToggleBG.Checked;
            tilemap.RedrawTilemap();
            SetLocationImage();
        }
        private void buttonToggleMask_Click(object sender, EventArgs e)
        {
            state.Mask = buttonToggleMask.Checked;
            pictureBoxLocation.Invalidate();
        }
        private void buttonToggleZoning_Click(object sender, EventArgs e)
        {
            state.Zones = buttonToggleZoning.Checked;
            pictureBoxLocation.Invalidate();
        }
        private void buttonToggleBoundaries_Click(object sender, EventArgs e)
        {
            buttonToggleBoundaries.Checked = !buttonToggleBoundaries.Checked;
            state.ShowBoundaries = buttonToggleBoundaries.Checked;
            pictureBoxLocation.Invalidate();
        }
        private void buttonToggleL1_Click(object sender, EventArgs e)
        {
            state.Layer1 = buttonToggleL1.Checked;
            tilemap.RedrawTilemap();
            SetLocationImage();
        }
        private void buttonToggleL2_Click(object sender, EventArgs e)
        {
            state.Layer2 = buttonToggleL2.Checked;
            tilemap.RedrawTilemap();
            SetLocationImage();
        }
        private void buttonToggleL3_Click(object sender, EventArgs e)
        {
            state.Layer3 = buttonToggleL3.Checked;
            tilemap.RedrawTilemap();
            SetLocationImage();
        }
        private void buttonToggleP1_Click(object sender, EventArgs e)
        {
            state.Priority1 = buttonToggleP1.Checked;
            tilemap.RedrawTilemap();
            SetLocationImage();
        }
        private void buttonTogglePhys_Click(object sender, EventArgs e)
        {
            state.Solidity = buttonTogglePhys.Checked;
            pictureBoxLocation.Invalidate();
        }
        private void buttonToggleNPCs_Click(object sender, EventArgs e)
        {
            state.NPCs = buttonToggleNPCs.Checked;
            pictureBoxLocation.Invalidate();
        }
        private void buttonToggleExits_Click(object sender, EventArgs e)
        {
            state.Exits = buttonToggleExits.Checked;
            pictureBoxLocation.Invalidate();
        }
        private void buttonToggleEvents_Click(object sender, EventArgs e)
        {
            state.Events = buttonToggleEvents.Checked;
            pictureBoxLocation.Invalidate();
        }
        private void buttonToggleTreasures_Click(object sender, EventArgs e)
        {
            state.Treasures = buttonToggleTreasures.Checked;
            pictureBoxLocation.Invalidate();
        }
        private void opacityToolStripButton_Click(object sender, EventArgs e)
        {
            panelOpacity.Visible = !panelOpacity.Visible;
        }
        private void tags_Click(object sender, EventArgs e)
        {
            pictureBoxLocation.Invalidate();
        }
        private void overlayOpacity_ValueChanged(object sender, EventArgs e)
        {
            labelOverlayOpacity.Text = overlayOpacity.Value.ToString() + "%";
            pictureBoxLocation.Invalidate();
        }
        // drawing
        private void buttonEditDraw_Click(object sender, EventArgs e)
        {
            state.Draw = buttonEditDraw.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (buttonEditDraw.Checked)
                this.pictureBoxLocation.Cursor = NewCursors.Draw;
            else if (!buttonEditDraw.Checked)
                this.pictureBoxLocation.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxLocation.Invalidate();
        }
        private void buttonEditErase_Click(object sender, EventArgs e)
        {
            state.Erase = buttonEditErase.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (state.Erase)
                this.pictureBoxLocation.Cursor = NewCursors.Erase;
            else if (!state.Erase)
                this.pictureBoxLocation.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxLocation.Invalidate();
        }
        private void buttonEditFill_Click(object sender, EventArgs e)
        {
            state.Fill = buttonEditFill.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (buttonEditFill.Checked)
                this.pictureBoxLocation.Cursor = NewCursors.Fill;
            else if (!buttonEditFill.Checked)
                this.pictureBoxLocation.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxLocation.Invalidate();
        }
        private void buttonEditTemplate_Click(object sender, EventArgs e)
        {
            state.Template = buttonEditTemplate.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (buttonEditTemplate.Checked)
                this.pictureBoxLocation.Cursor = NewCursors.Template;
            else if (!buttonEditTemplate.Checked)
                this.pictureBoxLocation.Cursor = System.Windows.Forms.Cursors.Arrow;
            Defloat();
            pictureBoxLocation.Invalidate();
        }
        private void buttonEditDropper_Click(object sender, EventArgs e)
        {
            state.Dropper = buttonEditDropper.Checked;
            pictureBoxLocation.ZoomBoxEnabled = state.Dropper;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (state.Dropper)
                this.pictureBoxLocation.Cursor = NewCursors.Dropper;
            else if (!state.Dropper)
                this.pictureBoxLocation.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxLocation.Invalidate();
        }
        private void buttonEditSelect_Click(object sender, EventArgs e)
        {
            state.Select = buttonEditSelect.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (state.Select)
                this.pictureBoxLocation.Cursor = Cursors.Cross;
            else if (!state.Select)
                this.pictureBoxLocation.Cursor = Cursors.Arrow;
            if (copiedTiles != null)
                Defloat(copiedTiles);
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            Defloat();
            pictureBoxLocation.Invalidate();
        }
        private void selectAll_Click(object sender, EventArgs e)
        {
            if (!state.Select)
                buttonEditSelect.PerformClick();
            Defloat();
            overlay.Select = new Overlay.Selection(16, 0, 0, width, height, Picture);
            pictureBoxLocation.Invalidate();
        }
        private void buttonEditDelete_Click(object sender, EventArgs e)
        {
            if (!state.Move)
                Delete();
            else
            {
                state.Move = false;
                draggedTiles = null;
                pictureBoxLocation.Invalidate();
            }
            if (!state.Move && commandCount > 0)
            {
                commandStack.Push(commandCount);
                commandCount = 0;
            }
        }
        private void buttonEditUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }
        private void buttonEditRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }
        private void buttonEditCut_Click(object sender, EventArgs e)
        {
            Cut();
        }
        private void buttonEditCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void buttonEditPaste_Click(object sender, EventArgs e)
        {
            if (copiedTiles == null)
                return;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(Math.Abs(autoScrollPos.X) / zoom / 16 * 16, pictureBoxLocation.Width));
            int y = Math.Max(0, Math.Min(Math.Abs(autoScrollPos.Y) / zoom / 16 * 16, pictureBoxLocation.Width));
            x += 32; y += 32;
            if (x + copiedTiles.Width >= width)
                x -= x + copiedTiles.Width - width;
            if (y + copiedTiles.Height >= height)
                y -= x + copiedTiles.Height - height;
            //
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            Paste(new Point(x, y), copiedTiles);
        }
        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (buttonZoomIn.Checked)
                this.pictureBoxLocation.Cursor = NewCursors.ZoomIn;
            else if (!buttonZoomIn.Checked)
                this.pictureBoxLocation.Cursor = System.Windows.Forms.Cursors.Arrow;
        }
        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (buttonZoomOut.Checked)
                this.pictureBoxLocation.Cursor = NewCursors.ZoomOut;
            else if (!buttonZoomOut.Checked)
                this.pictureBoxLocation.Cursor = System.Windows.Forms.Cursors.Arrow;
        }
        // context menu
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            mirrorToolStripMenuItem.Enabled = tilesetEditor.Layer == 2;
            invertToolStripMenuItem.Enabled = tilesetEditor.Layer == 2;
            //
            if (buttonZoomIn.Checked || buttonZoomOut.Checked)
                e.Cancel = true;
            else if (mouseOverObject == "exit")
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;
                objectFunctionToolStripMenuItem.Text = "Load destination";
                objectFunctionToolStripMenuItem.Tag = mouseOverExitField;
                objectFunctionToolStripMenuItem.Visible = true;
            }
            else if (mouseOverObject == "event")
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;
                objectFunctionToolStripMenuItem.Text = "Edit event's script";
                objectFunctionToolStripMenuItem.Tag = mouseOverEventField;
                objectFunctionToolStripMenuItem.Visible = true;
            }
            else if (mouseOverObject == "npc" || mouseOverObject == "npc instance")
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;
                objectFunctionToolStripMenuItem.Text = "Edit npc's script";
                objectFunctionToolStripMenuItem.Tag = mouseOverNPC;
                objectFunctionToolStripMenuItem.Visible = true;
            }
            else
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = true;
                objectFunctionToolStripMenuItem.Visible = false;
            }
        }
        private void findInTileset_Click(object sender, EventArgs e)
        {
            int index = 0;
            // first, use "see-through" approach to look for the exact visible tile clicked on
            int layer = 0;
            if (state.Layer1)
                index = tilemap.GetTileNum(0, mousePosition.X, mousePosition.Y, true);
            if (index == 0)
            {
                layer++;
                if (state.Layer2)
                    index = tilemap.GetTileNum(1, mousePosition.X, mousePosition.Y, true);
            }
            if (index == 0)
            {
                layer++;
                if (state.Layer3)
                    index = tilemap.GetTileNum(2, mousePosition.X, mousePosition.Y, true);
            }
            if (index != 0) // only if not all layers empty
            {
                tilesetEditor.Layer = layer;
                tilesetEditor.MouseDownTile = index;
                return;
            }
            // if all empty, use raw opaque tile searching approach
            layer = 0;
            if (state.Layer1)
                index = tilemap.GetTileNum(0, mousePosition.X, mousePosition.Y, false);
            if (index == 0)
            {
                layer++;
                if (state.Layer2)
                    index = tilemap.GetTileNum(1, mousePosition.X, mousePosition.Y, false);
            }
            if (index == 0)
            {
                layer++;
                if (state.Layer3)
                    index = tilemap.GetTileNum(2, mousePosition.X, mousePosition.Y, false);
            }
            if (index != 0) // only if not all layers empty
                tilesetEditor.Layer = layer;
            tilesetEditor.MouseDownTile = index;
        }
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do.Export(tilemapImage, "location." + location.Index.ToString("d3") + ".png");
        }
        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip("mirror");
        }
        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip("invert");
        }
        private void objectFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objectFunctionToolStripMenuItem.Text == "Load destination")
            {
                Exit exit = exits.Exits[(int)objectFunctionToolStripMenuItem.Tag];
                locations.index = exit.Destination;
            }
            else if (objectFunctionToolStripMenuItem.Text == "Edit event's script")
            {
                Event EVENT = events.Events[(int)objectFunctionToolStripMenuItem.Tag];
                if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                    Model.Program.CreateEventScriptsWindow();
                Model.Program.EventScripts.GotoAddress(EVENT.EventPointer + 0x0A0000);
                Model.Program.EventScripts.BringToFront();
            }
            else if (objectFunctionToolStripMenuItem.Text == "Edit npc's script")
            {
                NPC npc = npcs.Npcs[(int)objectFunctionToolStripMenuItem.Tag];
                if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                    Model.Program.CreateEventScriptsWindow();
                Model.Program.EventScripts.GotoAddress(npc.EventPointer + 0x0A0000);
                Model.Program.EventScripts.BringToFront();
            }
        }
        #endregion
    }
}
