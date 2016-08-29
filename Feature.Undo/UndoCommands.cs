using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZONEDOCTOR.ScriptsEditor;
using ZONEDOCTOR.ScriptsEditor.Commands;

namespace ZONEDOCTOR.Undo
{
    class GraphicEdit : Command
    {
        private byte[] changes;
        private byte[] graphics;
        private bool autoRedo = false;
        public bool AutoRedo() { return this.autoRedo; }
        //
        public GraphicEdit(byte[] graphics, byte[] changes)
        {
            this.graphics = graphics;
            this.changes = changes;
        }
        public void Execute()
        {
            byte[] temp = Bits.Copy(graphics);
            changes.CopyTo(graphics, 0);
            temp.CopyTo(changes, 0);
        }
    }
    class TilemapCommand : Command
    {
        private byte[] src;
        private Size srcSize;
        private byte[] changes;
        private Point location;
        private Size size;
        private bool autoRedo = false;
        public bool AutoRedo() { return this.autoRedo; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src">The source array to modify.</param>
        /// <param name="srcWidth">The width, in tiles, of the source array.</param>
        /// <param name="srcHeight">The height, in tiles, of the source array.</param>
        /// <param name="changes">The changes to apply to the source array.</param>
        /// <param name="x">The X location, in tiles, where the changes will be applied.</param>
        /// <param name="y">The Y location, in tiles, where the changes will be applied.</param>
        /// <param name="width">The width, in tiles, of the changes.</param>
        /// <param name="height">The height, in tiles, of the changes.</param>
        public TilemapCommand(byte[] src, int srcWidth, int srcHeight, byte[] changes, int x, int y, int width, int height)
        {
            this.src = src;
            this.changes = new byte[changes.Length];
            changes.CopyTo(this.changes, 0);
            this.srcSize = new Size(srcWidth, srcHeight);
            this.size = new Size(width, height);
            this.location = new Point(x, y);
            Execute();
        }
        public void Execute()
        {
            for (int y = location.Y, y_ = 0; y < location.Y + size.Height && y < 16; y++, y_++)
            {
                for (int x = location.X, x_ = 0; x < location.X + size.Width && x < 16; x++, x_++)
                {
                    if (x < 0 || y < 0 || x_ < 0 || y_ < 0) continue;
                    byte temp = src[y * srcSize.Width + x];
                    src[y * srcSize.Width + x] = changes[y_ * size.Width + x_];
                    changes[y_ * size.Width + x_] = temp;
                }
            }
        }
    }
    class TilesetCommand : Command
    {
        private byte[] oldTileset;
        private Tileset tileset;
        private byte[] graphics;
        private int index;
        private byte format;
        private System.Windows.Forms.ToolStripComboBox name;
        private bool autoRedo = false; public bool AutoRedo() { return this.autoRedo; }
        //
        public TilesetCommand(Tileset tileset, byte[] oldTileset, byte[] graphics,
            byte format, System.Windows.Forms.ToolStripComboBox name)
        {
            this.tileset = tileset;
            this.oldTileset = oldTileset;
            this.graphics = graphics;
            this.format = format;
            this.name = name;
            if (name != null)
                this.index = (int)name.SelectedIndex;
        }
        //
        public void Execute()
        {
            if (tileset != null)
            {
                byte[] temp = Bits.Copy(tileset.Tileset_bytes);
                oldTileset.CopyTo(tileset.Tileset_bytes, 0);
                tileset.DrawTileset(tileset.Tileset_bytes, tileset.Tileset_tiles, graphics, format);
                oldTileset = temp;
                if (name != null)
                    name.SelectedIndex = index;
            }
        }
    }
}
