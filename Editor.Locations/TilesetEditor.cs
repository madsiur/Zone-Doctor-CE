using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using ZONEDOCTOR.Undo;

namespace ZONEDOCTOR
{
    public partial class TilesetEditor : NewForm
    {
        #region Variables
        // main
        private delegate void Function();
        private Delegate update;
        public int Layer
        {
            get
            {
                if (tabControl1.SelectedIndex >= 0)
                    return tabControl1.SelectedIndex;
                return 0;
            }
            set { tabControl1.SelectedIndex = value; }
        }
        private PictureBox pictureBox
        {
            get
            {
                if (tabControl1.SelectedIndex == 0)
                    return pictureBoxTilesetL1;
                else if (tabControl1.SelectedIndex == 1)
                    return pictureBoxTilesetL2;
                else
                    return pictureBoxTilesetL3;
            }
            set
            {
                if (tabControl1.SelectedIndex == 0)
                    pictureBoxTilesetL1 = value;
                else if (tabControl1.SelectedIndex == 1)
                    pictureBoxTilesetL2 = value;
                else
                    pictureBoxTilesetL3 = value;
            }
        }
        public PictureBox PictureBox { get { return pictureBox; } set { pictureBox = value; } }
        private Tileset tileset;
        private SoliditySet soliditySet;
        private PaletteSet paletteSet;
        private Bitmap tilesetImage, priority1;
        private Overlay overlay;
        public TileEditor TileEditor;
        // mouse
        private int zoom = 1;
        private bool mouseEnter = false;
        private int mouseOverTile = 0;
        private int mouseDownTile = 0;
        public int MouseDownTile
        {
            get { return mouseDownTile; }
            set
            {
                mouseDownTile = value;
                MouseEventArgs mouseEventArgs = new MouseEventArgs(
                    MouseButtons.Left, 1,
                    value % 16 * 16,
                    value / 16 * 16, 0);
                pictureBoxTileset_MouseDown(null, mouseEventArgs);
                pictureBoxTileset_MouseUp(null, mouseEventArgs);
                overlay.SelectTS.ZoomRegion(tilesetImage);
                pictureBox.Invalidate();
            }
        }
        private string mouseOverObject;
        private string mouseDownObject;
        private Point mouseDownPosition;
        public Point mousePosition;
        private bool moving = false;
        //
        private int replaceTileA = -1;
        private int replaceTileB = -1;
        private bool replaceChoose = false;
        private bool replaceSet = false;
        // buffers and stacks
        private Bitmap selection;
        private CopyBuffer copiedTiles;
        private CopyBuffer selectedTiles; public CopyBuffer SelectedTiles { get { return selectedTiles; } }
        private CopyBuffer draggedTiles; public CopyBuffer DraggedTiles { get { return draggedTiles; } }
        private CommandStack commandStack = new CommandStack();
        #endregion
        #region Functions
        // main
        public TilesetEditor(Tileset tileset, SoliditySet soliditySet, Delegate update, PaletteSet paletteSet, Overlay overlay)
        {
            this.tileset = tileset;
            this.soliditySet = soliditySet;
            this.paletteSet = paletteSet;
            this.overlay = overlay;
            this.update = update;
            //
            InitializeComponent();
            if (this.tileset.Tilesets_tiles[Layer] == null)
            {
                if (this.tileset.Tilesets_tiles[0] != null)
                    this.Layer = 0;
                else if (this.tileset.Tilesets_tiles[1] != null)
                    this.Layer = 1;
                else if (this.tileset.Tilesets_tiles[2] != null)
                    this.Layer = 2;
                else
                    this.Layer = 0;
            }
            LoadTileEditor();
            SetTileSetImage();
        }
        public void Reload(Tileset tileset, SoliditySet soliditySet, Delegate update, PaletteSet paletteSet, Overlay overlay)
        {
            mouseDownTile = 0;
            //
            this.tileset = tileset;
            this.soliditySet = soliditySet;
            this.paletteSet = paletteSet;
            this.overlay = overlay;
            this.update = update;
            //
            this.replaceSet = false;
            this.replaceChoose = false;
            this.replaceTileA = -1;
            this.replaceTileB = -1;
            //
            if (this.tileset.Tilesets_tiles[Layer] == null)
            {
                if (this.tileset.Tilesets_tiles[0] != null)
                    this.Layer = 0;
                else if (this.tileset.Tilesets_tiles[1] != null)
                    this.Layer = 1;
                else if (this.tileset.Tilesets_tiles[2] != null)
                    this.Layer = 2;
                else
                    this.Layer = 0;
            }
            LoadTileEditor();
            SetTileSetImage();
        }
        private void SetTileSetImage()
        {
            int[] tileSetPixels;
            int[] priority1Pixels;
            if (tileset.Tilesets_tiles[Layer] != null)
            {
                tileSetPixels = Do.TilesetToPixels(tileset.Tilesets_tiles[Layer], 16, 16, 0, false);
                priority1Pixels = Do.TilesetToPixels(tileset.Tilesets_tiles[Layer], 16, 16, 0, true);
            }
            else
            {
                tileSetPixels = new int[256 * 256];
                priority1Pixels = new int[256 * 256];
            }
            tilesetImage = Do.PixelsToImage(tileSetPixels, 256, 256);
            priority1 = Do.PixelsToImage(priority1Pixels, 256, 256);
            pictureBox.Invalidate();
        }
        public void EnableLayers(bool L1, bool L2, bool L3)
        {
            int index = 0;
            if (L1 && !tabControl1.TabPages.Contains(tabPage1))
                tabControl1.TabPages.Insert(index++, tabPage1);
            else if (!L1 && tabControl1.Contains(tabPage1))
                tabControl1.TabPages.Remove(tabPage1);
            else if (tabControl1.Contains(tabPage1))
                index++;
            //
            if (L2 && !tabControl1.TabPages.Contains(tabPage2))
                tabControl1.TabPages.Insert(index++, tabPage2);
            else if (!L2 && tabControl1.Contains(tabPage2))
                tabControl1.TabPages.Remove(tabPage2);
            else if (tabControl1.Contains(tabPage2))
                index++;
            //
            if (L3 && !tabControl1.TabPages.Contains(tabPage3))
                tabControl1.TabPages.Insert(index++, tabPage3);
            else if (!L3 && tabControl1.Contains(tabPage3))
                tabControl1.TabPages.Remove(tabPage3);
            else if (tabControl1.Contains(tabPage3))
                index++;
        }
        // tile editor
        private void TileUpdate()
        {
            this.tileset.Assemble(16, Layer);
            this.soliditySet.Assemble();
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        private void LoadTileEditor()
        {
            if (tileset.Tilesets_tiles[Layer] == null)
                return;
            switch (Layer)
            {
                case 2: // layer 3
                    if (TileEditor == null)
                    {
                        TileEditor = new TileEditor(new Function(TileUpdate),
                            this.tileset.Tilesets_tiles[Layer][mouseDownTile],
                            this.soliditySet.Tiles[mouseDownTile],
                            tileset.GraphicsL3, paletteSet, 0x10);
                        TileEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
                    }
                    else
                        TileEditor.Reload(new Function(TileUpdate),
                            this.tileset.Tilesets_tiles[Layer][mouseDownTile],
                            this.soliditySet.Tiles[mouseDownTile],
                            tileset.GraphicsL3, paletteSet, 0x10);
                    break;
                default:
                    if (TileEditor == null)
                    {
                        TileEditor = new TileEditor(new Function(TileUpdate),
                        this.tileset.Tilesets_tiles[Layer][mouseDownTile],
                            this.soliditySet.Tiles[mouseDownTile],
                        tileset.Graphics, paletteSet, 0x20);
                        TileEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
                    }
                    else
                        TileEditor.Reload(new Function(TileUpdate),
                        this.tileset.Tilesets_tiles[Layer][mouseDownTile],
                            this.soliditySet.Tiles[mouseDownTile],
                        tileset.Graphics, paletteSet, 0x20);
                    break;
            }
        }
        // editing
        private void DrawHoverBox(Graphics g)
        {
            Rectangle r = new Rectangle(mousePosition.X / 16 * 16 * zoom, mousePosition.Y / 16 * 16 * zoom, 16 * zoom, 16 * zoom);
            g.FillRectangle(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), r);
        }
        private void Copy()
        {
            if (overlay.SelectTS == null)
                return;
            if (draggedTiles != null)
            {
                this.copiedTiles = draggedTiles;
                return;
            }
            // make the copy
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            this.copiedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            Tile[] copiedTiles = new Tile[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    copiedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_].Copy();
                }
            }
            this.copiedTiles.Tiles = copiedTiles;
        }
        /// <summary>
        /// Start dragging a selection.
        /// </summary>
        private void Drag()
        {
            if (overlay.SelectTS == null)
                return;
            // make the copy
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            this.draggedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            Tile[] draggedTiles = new Tile[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    draggedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_].Copy();
                }
            }
            this.draggedTiles.Tiles = draggedTiles;
            selection = new Bitmap(this.draggedTiles.Image);
            Delete();
        }
        private void Cut()
        {
            Copy();
            Delete();
        }
        private void Paste(Point location, CopyBuffer buffer)
        {
            if (buffer == null)
                return;
            moving = true;
            // now dragging a new selection
            draggedTiles = buffer;
            selection = buffer.Image;
            overlay.SelectTS = new Overlay.Selection(16, location, buffer.Size, PictureBox);
            this.pictureBox.Invalidate();
        }
        /// <summary>
        /// "Cements" either a dragged selection or a newly pasted selection.
        /// </summary>
        /// <param name="buffer">The dragged selection or the newly pasted selection.</param>
        public void Defloat(CopyBuffer buffer)
        {
            if (buffer == null)
                return;
            if (overlay.SelectTS == null)
                return;
            selection = null;
            int x_ = overlay.SelectTS.X / 16;
            int y_ = overlay.SelectTS.Y / 16;
            for (int y = 0; y < buffer.Height / 16; y++)
            {
                for (int x = 0; x < buffer.Width / 16; x++)
                {
                    if (y + y_ < 0 || y + y_ >= 16 ||
                        x + x_ < 0 || x + x_ >= 16)
                        continue;
                    int index = (y + y_) * 16 + x + x_;
                    Tile tile = buffer.Tiles[y * (buffer.Width / 16) + x];
                    tileset.Tilesets_tiles[Layer][index] = tile.Copy();
                    tileset.Tilesets_tiles[Layer][index].Index = index;
                }
            }
            tileset.DrawTileset(tileset.Tilesets_tiles[Layer], tileset.Tilesets_bytes[Layer]);
            tileset.Assemble(16, Layer);
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        private void Delete()
        {
            if (overlay.SelectTS == null)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                    tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_].Clear();
            }
            tileset.DrawTileset(tileset.Tilesets_tiles[Layer], tileset.Tilesets_bytes[Layer]);
            tileset.Assemble(16, Layer);
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        private void Flip(string type)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);
            if (overlay.SelectTS == null)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            CopyBuffer buffer = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            Tile[] copiedTiles = new Tile[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    copiedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_].Copy();
                }
            }
            if (type == "mirror")
                Do.FlipHorizontal(copiedTiles, overlay.SelectTS.Width / 16, overlay.SelectTS.Height / 16, true);
            else if (type == "invert")
                Do.FlipVertical(copiedTiles, overlay.SelectTS.Width / 16, overlay.SelectTS.Height / 16, true);
            buffer.Tiles = copiedTiles;
            Defloat(buffer);
        }
        private void Priority1(bool priority1)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);
            if (overlay.SelectTS == null)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    Tile tile = tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_];
                    foreach (Subtile subtile in tile.Subtiles)
                        subtile.Priority1 = priority1;
                }
            }
            tileset.DrawTileset(tileset.Tilesets_tiles[Layer], tileset.Tilesets_bytes[Layer]);
            tileset.Assemble(16, Layer);
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        private void AdjustSolidity(bool var)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);
            if (overlay.SelectTS == null)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    SolidityTile tile = soliditySet.Tiles[(y + y_) * 16 + x + x_];
                    tile.AirshipShadow = 0;
                    tile.AlwaysFaceUp = false;
                    tile.B0b3 = false;
                    tile.B0b4 = false;
                    tile.B0b7 = false;
                    tile.B1b3 = false;
                    tile.B1b4 = false;
                    tile.B1b5 = false;
                    tile.Battlefield = 0;
                    tile.BlockAirship = false;
                    tile.BlockChocobo = false;
                    tile.EnableRandomBattle = false;
                    tile.HideSpriteLegs = false;
                    tile.KefkaTower = false;
                    tile.PhoenixCave = false;
                    tile.SolidTier1 = var;
                    tile.SolidTier2 = false;
                    tile.Veldt = false;
                    tile.East = true;
                    tile.West = true;
                    tile.North = true;
                    tile.South = true;
                    tile.Stairs = 0;
                    tile.Solid = var;
                    tile.Door = false;
                    tile.PassableQuadrants = true;
                }
            }
            soliditySet.Assemble();
            pictureBox.Invalidate();            
        }
        private void Palette(int palette)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);
            if (overlay.SelectTS == null)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    int index = 0;
                    Tile tile = tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_];
                    foreach (Subtile subtile in tile.Subtiles)
                    {
                        Subtile temp = Do.DrawSubtile((ushort)subtile.Index, (byte)palette, subtile.Priority1,
                            subtile.Mirror, subtile.Invert, tileset.Graphics, paletteSet.Palettes, 0x20);
                        tile.Subtiles[index++] = temp;
                    }
                }
            }
            tileset.DrawTileset(tileset.Tilesets_tiles[Layer], tileset.Tilesets_bytes[Layer]);
            tileset.Assemble(16, Layer);
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        #endregion
        #region Event handlers
        // main
        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);
            else
                overlay.SelectTS = null;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTileSetImage();
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = e.TabPageIndex < 0 || tileset.Tilesets_tiles[e.TabPageIndex] == null;
        }
        // image
        private void pictureBoxTileset_Paint(object sender, PaintEventArgs e)
        {
            if (tilesetImage == null)
                return;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            Rectangle rdst = new Rectangle(0, 0, 256, 256);
            if (buttonToggleBG.Checked)
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), rdst);
            e.Graphics.DrawImage(tilesetImage, rdst, 0, 0, 256, 256, GraphicsUnit.Pixel);
            if (moving && selection != null)
            {
                Rectangle rsrc = new Rectangle(0, 0, overlay.SelectTS.Width, overlay.SelectTS.Height);
                rdst = new Rectangle(
                    overlay.SelectTS.X * zoom, overlay.SelectTS.Y * zoom,
                    rsrc.Width * zoom, rsrc.Height * zoom);
                e.Graphics.DrawImage(new Bitmap(selection), rdst, rsrc, GraphicsUnit.Pixel);
                Do.DrawString(e.Graphics, new Point(rdst.X, rdst.Y + rdst.Height),
                    "click/drag", Color.White, Color.Black, new Font("Tahoma", 6.75F, FontStyle.Bold));
            }
            if (buttonToggleSolidity.Checked && Layer == 0)
                overlay.DrawSoliditySet(e.Graphics, (Tileset)tileset, soliditySet);
            float[][] matrixItems ={ 
               new float[] {1, 0, 0, 0, 0},
               new float[] {0, 1, 0, 0, 0},
               new float[] {0, 0, 1, 0, 0},
               new float[] {0, 0, 0, 0.50F, 0}, 
               new float[] {0, 0, 0, 0, 1}};
            ColorMatrix cm = new ColorMatrix(matrixItems);
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            if (priority1 != null && buttonToggleP1.Checked)
                e.Graphics.DrawImage(priority1, rdst, 0, 0, 256, 256, GraphicsUnit.Pixel, ia);
            if (mouseEnter)
                DrawHoverBox(e.Graphics);
            if (buttonToggleCartGrid.Checked)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBox.Size, new Size(16, 16), 1, true);
            if (overlay.SelectTS != null)
            {
                if (buttonToggleCartGrid.Checked)
                    overlay.SelectTS.DrawSelectionBox(e.Graphics, 1, Color.Yellow);
                else
                    overlay.SelectTS.DrawSelectionBox(e.Graphics, 1);
            }
        }
        private void pictureBoxTileset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 1)
                return;
            if (e.Button == MouseButtons.Right)
                return;
            // swap tiles
            if (replaceChoose)
            {
                replaceTileA = mouseOverTile;
                MessageBox.Show("Now select the second tile to swap.", "ZONE DOCTOR",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                replaceChoose = false;
                replaceSet = true;
                return;
            }
            else if (replaceSet)
            {
                replaceTileB = mouseOverTile;
                TilemapEditor tilemapEditor = ((Locations)this.Parent.FindForm()).tilemapEditor;
                tilemapEditor.Swap(replaceTileA, replaceTileB, Layer);
                replaceSet = false;
                return;
            }
            //
            mouseDownObject = null;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBox.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBox.Height));
            pictureBox.Focus();
            // if moving an object and outside of it, paste it
            if (moving && mouseOverObject != "selection")
            {
                // if copied tiles were pasted and not dragging a non-copied selection
                if (copiedTiles != null && draggedTiles == null)
                    Defloat(copiedTiles);
                if (draggedTiles != null)
                {
                    Defloat(draggedTiles);
                    draggedTiles = null;
                }
                selection = null;
                moving = false;
            }
            // if making a new selection
            if (e.Button == MouseButtons.Left && mouseOverObject == null)
                overlay.SelectTS = new Overlay.Selection(16, x / 16 * 16, y / 16 * 16, 16, 16, PictureBox);
            // if moving a current selection
            if (!lockEditing.Checked && e.Button == MouseButtons.Left && mouseOverObject == "selection")
            {
                mouseDownObject = "selection";
                mouseDownPosition = overlay.SelectTS.MousePosition(x, y);
                if (!moving)    // only do this if the current selection has not been initially moved
                {
                    moving = true;
                    Drag();
                }
            }
            mouseDownTile = y / 16 * 16 + (x / 16);
            LoadTileEditor();
        }
        private void pictureBoxTileset_MouseMove(object sender, MouseEventArgs e)
        {
            mouseOverObject = null;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBox.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBox.Height));
            mousePosition = new Point(x, y);
            // if making a new selection
            if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.SelectTS != null)
                overlay.SelectTS.Final = new Point(
                        Math.Min(x + 16, pictureBox.Width),
                        Math.Min(y + 16, pictureBox.Height));
            // if dragging the current selection
            if (e.Button == MouseButtons.Left && mouseDownObject == "selection")
                overlay.SelectTS.Location = new Point(
                    x / 16 * 16 - mouseDownPosition.X,
                    y / 16 * 16 - mouseDownPosition.Y);
            // check if over selection
            if (!lockEditing.Checked && e.Button == MouseButtons.None && overlay.SelectTS != null && overlay.SelectTS.MouseWithin(x, y))
            {
                mouseOverObject = "selection";
                pictureBox.Cursor = Cursors.SizeAll;
            }
            else
                pictureBox.Cursor = Cursors.Cross;
            pictureBox.Invalidate();
            mouseOverTile = y / 16 * 16 + (x / 16);
            labelTileIndex.Text = "Tile index: " + mouseOverTile + " ($" + mouseOverTile.ToString("X2") + ")";
        }
        private void pictureBoxTileset_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            if (overlay.SelectTS == null)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            if (this.selectedTiles == null)
                this.selectedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            int[][] selectedTiles = new int[3][];
            selectedTiles[Layer] = new int[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                    selectedTiles[Layer][y * (overlay.SelectTS.Width / 16) + x] = (y + y_) * 16 + x + x_;
            }
            this.selectedTiles.Copies = selectedTiles;
            pictureBox.Focus();
        }
        private void pictureBoxTileset_MouseClick(object sender, MouseEventArgs e)
        {
        }
        private void pictureBoxTileset_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }
        private void pictureBoxTileset_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            pictureBox.Invalidate();
        }
        private void pictureBoxTileset_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            pictureBox.Invalidate();
        }
        private void pictureBoxTileset_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V) && !lockEditing.Checked)
                buttonEditPaste.PerformClick();
            if (e.KeyData == (Keys.Control | Keys.C) && !lockEditing.Checked)
                buttonEditCopy.PerformClick();
            if (e.KeyData == Keys.Delete && !lockEditing.Checked)
                buttonEditDelete.PerformClick();
            if (e.KeyData == (Keys.Control | Keys.X) && !lockEditing.Checked)
                buttonEditCut.PerformClick();
            if (e.KeyData == (Keys.Control | Keys.D))
            {
                if (draggedTiles != null)
                    Defloat(draggedTiles);
                else
                {
                    overlay.SelectTS = null;
                    Cursor.Position = Cursor.Position;
                    pictureBox.Invalidate();
                }
            }
            if (e.KeyData == (Keys.Control | Keys.A))
            {
                overlay.SelectTS = new Overlay.Selection(16, 0, 0, 256, 256, PictureBox);
                pictureBox.Invalidate();
            }
        }
        private void lockEditing_CheckedChanged(object sender, EventArgs e)
        {
            buttonEditDelete.Enabled = !lockEditing.Checked;
            buttonEditCut.Enabled = !lockEditing.Checked;
            buttonEditCopy.Enabled = !lockEditing.Checked;
            buttonEditPaste.Enabled = !lockEditing.Checked;
        }
        // toolstrip
        private void buttonToggleTileEditor_Click(object sender, EventArgs e)
        {
            TileEditor.Visible = true;
        }
        private void buttonToggleCartGrid_Click(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }
        private void buttonToggleBG_Click(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }
        private void buttonToggleP1_Click(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }
        private void buttonToggleSolidity_Click(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }
        private void buttonEditDelete_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Delete();
        }
        private void buttonEditCut_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Cut();
        }
        private void buttonEditCopy_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Copy();
        }
        private void buttonEditPaste_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            if (draggedTiles != null)
                Defloat(draggedTiles);
            Paste(new Point(16, 16), copiedTiles);
        }
        private void swapTiles_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select the first tile to swap.", "ZONE DOCTOR",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            replaceTileA = -1;
            replaceTileB = -1;
            replaceChoose = true;
        }
        private void buttonEditUndo_Click(object sender, EventArgs e)
        {
        }
        private void buttonEditRedo_Click(object sender, EventArgs e)
        {
        }
        // contextmenustrip
        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            cutToolStripMenuItem2.Enabled = !lockEditing.Checked;
            copyToolStripMenuItem2.Enabled = !lockEditing.Checked;
            pasteToolStripMenuItem2.Enabled = !lockEditing.Checked;
            deleteToolStripMenuItem2.Enabled = !lockEditing.Checked;
        }
        private void setPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditValue editValue = new EditValue();
            editValue.ShowDialog();
            if (editValue.Tag == null)
                return;
            int index = (int)editValue.Tag;
            Palette(index);
        }
        private void setSolidity_Click(object sender, EventArgs e)
        {
            AdjustSolidity(true);
        }
        private void clearSolidity_Click(object sender, EventArgs e)
        {
            AdjustSolidity(false);
        }
        private void priority1SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Priority1(true);
        }
        private void priority1ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Priority1(false);
        }
        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Flip("mirror");
        }
        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Flip("invert");
        }
        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do.Export(tilesetImage, "tileset.png");
        }
        // editors
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            update.DynamicInvoke();
        }
        #endregion
    }
}
