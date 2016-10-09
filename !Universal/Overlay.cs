using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ZONEDOCTOR.Properties;

namespace ZONEDOCTOR
{
    public class Overlay
    {
        #region Variables
        private State state = State.Instance;
        // overlay objects
        public List<Bitmap> NPCImages;
        public int alpha = 255;
        // selecting
        public Selection Select;
        public Selection SelectTS;
        //
        public class Selection
        {
            /// <summary>
            /// The picture box control associated with the selection.
            /// </summary>
            private PictureBox picture;
            public PictureBox Picture { get { return picture; } set { picture = value; } }
            // marching ants
            private Timer antTimer;
            private int antOffset = 63;
            private Timer glowTimer;
            private int glowOpacity = 0;
            private Timer zoomTimer;
            private Bitmap zoomRegion;
            private int zoomFactor = 1;
            // dimensions, coordinates
            public int X { get { return Math.Min(initial.X, final.X); } }
            public int Y { get { return Math.Min(initial.Y, final.Y); } }
            public int Height { get { return size.Height; } }
            public int Width { get { return size.Width; } }
            private int unit = 1; public int Unit { get { return unit; } set { unit = value; } }
            private Size size;
            public Size Size
            {
                get
                {
                    return new Size(
                        Math.Abs(initial.X - final.X),
                        Math.Abs(initial.Y - final.Y));
                }
                set
                {
                    size = new Size(value.Width / unit * unit, value.Height / unit * unit);
                    final = new Point(initial.X + size.Width, initial.Y + size.Height);
                }
            }
            /// <summary>
            /// The real location (upper-left corner), in pixels, of the selection.
            /// </summary>
            public Point Location
            {
                get
                {
                    return new Point(
                        Math.Min(initial.X, final.X),
                        Math.Min(initial.Y, final.Y));
                }
                set
                {
                    initial = value;
                    final = new Point(value.X + size.Width, value.Y + size.Height);
                }
            }
            /// <summary>
            /// The real ending (lower-right corner), in pixels, of the selection.
            /// </summary>
            public Point Terminal
            {
                get
                {
                    return new Point(
                        Math.Max(initial.X, final.X),
                        Math.Max(initial.Y, final.Y));
                }
                set
                {
                    final = value;
                    initial = new Point(value.X - size.Width, value.Y - size.Height);
                }
            }
            private Point initial;
            /// <summary>
            /// Where, in pixels, the selection was first started.
            /// </summary>
            public Point Initial
            {
                get { return initial; }
                set
                {
                    initial = new Point(value.X / unit * unit, value.Y / unit * unit);
                }
            }
            private Point final;
            /// <summary>
            /// Where, in pixels, the selection was finished.
            /// </summary>
            public Point Final
            {
                get { return final; }
                set
                {
                    final = new Point(value.X / unit * unit, value.Y / unit * unit);
                    size = new Size(
                        Math.Abs(initial.X - final.X),
                        Math.Abs(initial.Y - final.Y));
                }
            }
            /// <summary>
            /// Returns a rectangle containing the location and size of the selection.
            /// </summary>
            public Rectangle Region { get { return new Rectangle(Location, Size); } }
            public Selection(int unit, Point initial, Size size, PictureBox picture)
            {
                this.unit = unit;
                this.initial = new Point(initial.X / unit * unit, initial.Y / unit * unit);
                this.size = new Size(size.Width / unit * unit, size.Height / unit * unit);
                this.final = new Point(initial.X + size.Width, initial.Y + size.Height);
                this.picture = picture;
                this.antTimer = new Timer();
                this.antTimer.Tick += new EventHandler(antTimer_Tick);
                this.antTimer.Start();
            }
            public Selection(int unit, int x, int y, int width, int height, PictureBox picture)
            {
                this.unit = unit;
                this.initial = new Point(x / unit * unit, y / unit * unit);
                this.size = new Size(width / unit * unit, height / unit * unit);
                this.final = new Point(x + size.Width, y + size.Height);
                this.picture = picture;
                this.antTimer = new Timer();
                this.antTimer.Tick += new EventHandler(antTimer_Tick);
                this.antTimer.Start();
            }
            /// <summary>
            /// Returns the mouse's relative position within the selection.
            /// </summary>
            /// <param name="x">The X coord of the mouse in the entire image.</param>
            /// <param name="y">The Y coord of the mouse in the entire image.</param>
            /// <returns></returns>
            public Point MousePosition(int x, int y)
            {
                Point mouse = new Point();
                mouse.X = (x / unit * unit) - Math.Min(initial.X, final.X);
                mouse.Y = (y / unit * unit) - Math.Min(initial.Y, final.Y);
                return mouse;
            }
            /// <summary>
            /// Checks if the mouse's coordinates inside the entire image are within the selection.
            /// </summary>
            /// <param name="x">The X coord of the mouse in the entire image.</param>
            /// <param name="y">The Y coord of the mouse in the entire image.</param>
            /// <returns></returns>
            public bool MouseWithin(int x, int y)
            {
                Point mouse = MousePosition((x / unit * unit), (y / unit * unit));
                if (mouse.X < 0 || mouse.X >= size.Width)
                    return false;
                if (mouse.Y < 0 || mouse.Y >= size.Height)
                    return false;
                return true;
            }
            /// <summary>
            /// Returns an image of the selection's region within a specified image.
            /// </summary>
            /// <param name="image">The image containing the selection.</param>
            /// <returns></returns>
            public Bitmap GetSelectionImage(Bitmap image)
            {
                return image.Clone(Region, System.Drawing.Imaging.PixelFormat.DontCare);
            }
            // drawing the selection box
            public void DrawSelectionBox(Graphics g, int z, Color color)
            {
                Point start = Location;
                Point stop = Terminal;
                if (stop.X == start.X)
                    return;
                if (stop.Y == start.Y)
                    return;
                Point p = new Point(start.X * z, start.Y * z);
                Size s = new Size((stop.X * z) - (start.X * z) - 1, (stop.Y * z) - (start.Y * z) - 1);
                Pen penw = new Pen(color);
                Pen penb = new Pen(Color.FromArgb(color.R / 4, color.G / 4, color.B / 4));
                penb.DashOffset = antOffset;
                penb.DashPattern = new float[] { 4, 4 };
                Rectangle src = new Rectangle(p, s);
                if (glowOpacity > 0)
                    g.FillRectangle(new SolidBrush(Color.FromArgb(glowOpacity, color)), src);
                g.DrawRectangle(penw, src);
                g.DrawRectangle(penb, src);
                //
                if (zoomFactor > 1 && zoomRegion != null)
                {
                    int x = src.X - ((Width * zoomFactor - Width) / 2);
                    int y = src.Y - ((Height * zoomFactor - Height) / 2);
                    Rectangle dst = new Rectangle(x, y, Width * zoomFactor, Height * zoomFactor);
                    src = new Rectangle(0, 0, Width + 1, Height + 1);
                    g.DrawImage(zoomRegion, dst, src, GraphicsUnit.Pixel);
                }
            }
            public void DrawSelectionBox(Graphics g, int z)
            {
                DrawSelectionBox(g, z, Color.White);
            }
            public void GlowSelection()
            {
                glowOpacity = 255;
                glowTimer = new Timer();
                glowTimer.Tick += new EventHandler(glowTimer_Tick);
                glowTimer.Start();
            }
            public void ZoomRegion(Bitmap image)
            {
                zoomRegion = image.Clone(Region, PixelFormat.DontCare);
                zoomFactor = 8;
                zoomTimer = new Timer();
                zoomTimer.Interval = 20;
                zoomTimer.Tick += new EventHandler(zoomTimer_Tick);
                zoomTimer.Start();
            }
            //
            private void glowTimer_Tick(object sender, EventArgs e)
            {
                glowOpacity -= 32;
                if (glowOpacity <= 0)
                    glowTimer.Stop();
                this.picture.Refresh();
            }
            private void antTimer_Tick(object sender, EventArgs e)
            {
                if (this.Width < 4 || this.Height < 4)
                    return;
                antOffset--;
                if (antOffset < 0)
                    antOffset = 63;
                this.picture.Refresh();
            }
            private void zoomTimer_Tick(object sender, EventArgs e)
            {
                zoomFactor--;
                if (zoomFactor <= 1)
                {
                    zoomRegion = null;
                    zoomTimer.Stop();
                }
                this.picture.Refresh();
            }
        }
        #endregion
        #region Functions
        public Overlay()
        {
        }
        public void DrawTileGrid(Graphics g, Color c, Size s, Size u, bool dashed, int offset)
        {
            DrawTileGrid(g, c, s, u, 1, dashed, offset);
        }
        public void DrawTileGrid(Graphics g, Color c, Size s, Size u, int z, bool dashed)
        {
            DrawTileGrid(g, c, s, u, z, dashed, 0);
        }
        public void DrawTileGrid(Graphics g, Color c, Size s, Size u, int z, bool dashed, int offset)
        {
            c = Color.FromArgb(alpha, c);
            Pen p = new Pen(new SolidBrush(c));
            if (dashed)
                p.DashPattern = new float[] { 1, 1 };
            Point h = new Point();
            Point v = new Point();
            for (h.Y = z * u.Height + offset; h.Y < s.Height + offset; h.Y += z * u.Height)
                g.DrawLine(p, h, new Point(h.X + s.Width, h.Y));
            for (v.X = z * u.Width + offset; v.X < s.Width + offset; v.X += z * u.Width)
                g.DrawLine(p, v, new Point(v.X, v.Y + s.Height));
        }
        public void DrawLocationMask(Graphics g, Point stop, Point start, int z)
        {
            Point p = new Point(start.X * 16 * z, start.Y * 16 * z);
            Size s = new Size((stop.X - start.X) * 16 + 16 * z, (stop.Y - start.Y) * 16 + 16 * z);
            Brush b = new SolidBrush(Color.FromArgb(75, Color.Magenta));
            if (p.X == 0) p.X++; if (p.Y == 0) p.Y++;
            Rectangle r = new Rectangle(p, s);
            if (r.Right >= 1024 - 1 * z) r.Width = 1024 - 2 * z;
            if (r.Bottom >= 1024 - 1 * z) r.Height = 1024 - 2 * z;
            g.FillRectangle(b, r);
        }
        public void DrawBoundaries(Graphics g, Point start, Point mask, double z)
        {
            if (mask.X == 0) mask.X = 256;
            if (mask.Y == 0) mask.Y = 256;
            Pen border = new Pen(Color.FromArgb(128, 128, 128), (int)(8 * z));
            Rectangle bounds = new Rectangle();
            bounds.X = Math.Max(Math.Min(((start.X - 7) * 16) - 8, mask.X * 16 - 256), 0);
            bounds.X = (int)(bounds.X * z);
            bounds.Y = Math.Max(Math.Min(((start.Y - 7) * 16), mask.Y * 16 - 224), 0);
            bounds.Y = (int)(bounds.Y * z);
            bounds.Width = (int)(256 * z);
            bounds.Height = (int)(224 * z);
            border.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            g.DrawRectangle(border, bounds);
            SolidBrush darkness = new SolidBrush(Color.FromArgb(128, Color.Black));
            Rectangle dark = new Rectangle();
            dark.X = (int)g.ClipBounds.X;
            dark.Y = (int)g.ClipBounds.Y;
            dark.Width = (int)g.ClipBounds.Width;
            dark.Height = bounds.Y - (int)g.ClipBounds.Y;
            g.FillRectangle(darkness, dark);
            dark.Y = bounds.Y;
            dark.Width = bounds.X - (int)g.ClipBounds.X;
            dark.Height = bounds.Height;
            g.FillRectangle(darkness, dark);
            dark.X = bounds.Right;
            dark.Width = (int)g.ClipBounds.Right - bounds.Right;
            g.FillRectangle(darkness, dark);
            dark.X = (int)g.ClipBounds.X;
            dark.Y = bounds.Y + bounds.Height;
            dark.Width = (int)g.ClipBounds.Width;
            dark.Height = (int)g.ClipBounds.Bottom - bounds.Bottom;
            g.FillRectangle(darkness, dark);
        }
        public void DrawZoneGrid(Graphics g, Color c, Size s, Size u, double z)
        {
            c = Color.FromArgb(alpha, c);
            Color c_ = Color.FromArgb(alpha, Color.Black);
            Pen p = new Pen(new SolidBrush(c));
            Point h = new Point();
            Point v = new Point();
            for (h.Y = (int)(z * u.Height); h.Y < s.Height; h.Y += (int)(z * u.Height))
                g.DrawLine(p, h, new Point(h.X + s.Width, h.Y));
            for (v.X = (int)(z * u.Width); v.X < s.Width; v.X += (int)(z * u.Width))
                g.DrawLine(p, v, new Point(v.X, v.Y + s.Height));
            Font zoneFont = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    string name = "ZONE #" + (y * 8 + x);
                    g.DrawString(name, zoneFont, new SolidBrush(c_), new PointF(x * (int)(512 * z) + 7, y * (int)(512 * z) + 9));
                    g.DrawString(name, zoneFont, new SolidBrush(c), new PointF(x * (int)(512 * z) + 8, y * (int)(512 * z) + 8));
                }
            }
        }
        public void DrawHoverBox(Graphics g, Point location, Size size, int zoom, bool fill)
        {
            int x = location.X;
            int y = location.Y;
            Rectangle r = new Rectangle(x * zoom, y * zoom, size.Width * zoom, size.Height * zoom);
            if (fill)
                g.FillRectangle(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), r);
            else
            {
                r.Width--;
                r.Height--;
                g.DrawRectangle(Pens.Gray, r);
            }
        }
        // location elements
        public void DrawLocationExits(Graphics g, LocationExits exits, double z)
        {
            if (exits.Count == 0)
                return;
            foreach (Exit exit in exits.Exits)
            {
                if (exit != exits.Exit)
                    DrawLocationExit(g, exits, exit, z);
            }
            if (exits.Exit != null)
                DrawLocationExit(g, exits, exits.Exit, z);
        }
        private void DrawLocationExit(Graphics g, LocationExits exits, Exit exit, double z)
        {
            Rectangle r;
            Pen pen = new Pen(Color.Yellow, 2);
            SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.Yellow));
            if (exit.Width > 0)
            {
                if (exit.F == 0)
                    r = new Rectangle(
                        new Point((int)(exit.X * 16 * z), (int)(exit.Y * 16 * z)),
                        new Size((int)((exit.Width + 1) * 16 * z), (int)(16 * z)));
                else
                    r = new Rectangle(
                        new Point((int)(exit.X * 16 * z), (int)(exit.Y * 16 * z)),
                        new Size((int)(16 * z), (int)((exit.Width + 1) * 16 * z)));
            }
            else
                r = new Rectangle(
                    new Point((int)(exit.X * 16 * z), (int)(exit.Y * 16 * z)),
                    new Size((int)(16 * z), (int)(16 * z)));
            r.X++; r.Y++;
            r.Width -= 2; r.Height -= 2;
            if (exit == exits.Exit)
                g.FillRectangle(brush, r);
            g.DrawRectangle(pen, r);
        }
        public void DrawLocationExitTags(Graphics g, LocationExits exits, double z)
        {
            if (exits.Count == 0)
                return;
            // draw exit strings
            Rectangle r = new Rectangle();
            SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.Yellow));
            SolidBrush brush_ = new SolidBrush(Color.FromArgb(192, Color.Yellow));
            Font font = new Font("Tahoma", 8.25F);
            Font font_ = new Font("Tahoma", 8.25F, FontStyle.Bold);
            foreach (Exit exit in exits.Exits)
            {
                if (exit.Width > 0)
                {
                    if (exit.F == 0)
                        r = new Rectangle(
                            new Point((int)(exit.X * 16 * z), (int)(exit.Y * 16 * z)),
                            new Size((int)((exit.Width + 1) * 16 * z), (int)(16 * z)));
                    else
                        r = new Rectangle(
                            new Point((int)(exit.X * 16 * z), (int)(exit.Y * 16 * z)),
                            new Size((int)(16 * z), (int)((exit.Width + 1) * 16 * z)));
                }
                else
                    r = new Rectangle(
                        new Point((int)(exit.X * 16 * z), (int)(exit.Y * 16 * z)),
                        new Size((int)(16 * z), (int)(16 * z)));
                r.X++; r.Y += 3;
                r.Width -= 2; r.Height -= 2;
                string name = Lists.Numerize(Model.LevelNames, exit.Destination);
                RectangleF label = new RectangleF(new PointF(r.X, r.Y + r.Height),
                    g.MeasureString(name, font_, new PointF(0, 0), StringFormat.GenericDefault));
                if (exit == exits.Exit)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(192, Color.Black)), label);
                    g.DrawString(name, font_, brush_, r.X, r.Y + r.Height);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Black)), label);
                    g.DrawString(name, font, brush, r.X, r.Y + r.Height);
                }
            }
        }
        //
        public void DrawLocationEvents(Graphics g, LocationEvents events, double z)
        {
            if (events.Count == 0)
                return;
            foreach (Event temp in events.Events)
            {
                if (temp != events.Event)
                    DrawLocationEvent(g, events, temp, z);
            }
            if (events.Event != null)
                DrawLocationEvent(g, events, events.Event, z);
        }
        private void DrawLocationEvent(Graphics g, LocationEvents events, Event temp, double z)
        {
            Rectangle r = new Rectangle();
            Pen pen = new Pen(Color.FromArgb(255, 0, 255, 0), 2);
            SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 255, 0));
            r = new Rectangle(new Point((int)(temp.X * 16 * z), (int)(temp.Y * 16 * z)), new Size((int)(16 * z), (int)(16 * z)));
            r.X++; r.Y++;
            r.Width -= 2; r.Height -= 2;
            if (temp == events.Event)
                g.FillRectangle(brush, r);
            g.DrawRectangle(pen, r);
        }
        public void DrawLocationEventTags(Graphics g, LocationEvents events, int z)
        {
            if (events.Count == 0)
                return;
            // draw event strings
            foreach (Event EVENT in events.Events)
            {
                if (EVENT != events.Event)
                    DrawLocationEventTag(g, events, EVENT, z);
            }
            if (events.Event != null)
                DrawLocationEventTag(g, events, events.Event, z);
        }
        public void DrawLocationEventTag(Graphics g, LocationEvents events, Event temp, int z)
        {
            Rectangle r = new Rectangle();
            SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 255, 0));
            SolidBrush brush_ = new SolidBrush(Color.FromArgb(192, 0, 255, 0));
            Font font = new Font("Tahoma", 8.25F);
            Font font_ = new Font("Tahoma", 8.25F, FontStyle.Bold | FontStyle.Underline);
            Font font_b = new Font("Tahoma", 8.25F, FontStyle.Bold);
            Font lucida = new Font("Lucida Console", 8.25F);
            r = new Rectangle((int)(temp.X * 16 * z), (int)(temp.Y * 16 * z), (int)(16 * z), (int)(16 * z));
            r.X++; r.Y += 3;
            r.Width -= 2; r.Height -= 2;
            string name = "$" + temp.EventPointer.ToString("X6") + " ($" + (temp.EventPointer + 0xCA0000).ToString("X6") + ")";
            RectangleF label = new RectangleF(new PointF(r.X, r.Y + r.Height),
                g.MeasureString(name, font_, new PointF(0, 0), StringFormat.GenericDefault));
            if (temp == events.Event)
            {
                // draw commands
                r.Y += r.Height;
                g.FillRectangle(new SolidBrush(Color.FromArgb(192, Color.Black)), label);
                g.DrawString(name, font_, brush_, r.X, r.Y);
                string script = Do.EventScriptToText(Model.GetEventScript(temp.EventPointer + 0x0A0000), temp.EventPointer + 0x0A0000, 8, 40);
                RectangleF commandbox = new RectangleF(r.X + 2, r.Y + 16, 256, (label.Height - 2) * script.Split('\n').Length);
                g.FillRectangle(brush_, commandbox);
                g.DrawString(script, font_b, new SolidBrush(Color.Black), r.X + 6, r.Y + 20);
            }
            else
            {
                // draw commands
                g.DrawString(name, font, brush, r.X, r.Y + r.Height);
            }
        }
        //
        public void DrawLocationNPCs(Graphics g, LocationNPCs npcs, double z)
        {
            if (npcs.Count == 0)
                return;
            NPC temp;
            Rectangle r;
            Pen p = new Pen(Color.Red, 2);
            SolidBrush b = new SolidBrush(Color.FromArgb(128, Color.Red));
            Bitmap sprite;
            for (int i = 0; i < npcs.Npcs.Count; i++)
            {
                temp = (NPC)npcs.Npcs[i];
                r = new Rectangle();
                r.Location = new Point((int)(temp.X * 16 * z), (int)(temp.Y * 16 * z));
                r.Size = new Size((int)(16 * z), (int)(32 * z));
                r.X++; r.Y -= (int)(16 * z); r.Y++;
                r.Width -= 2; r.Height -= 2;
                sprite = Do.PixelsToImage(temp.Pixels, 16, 32);
                Rectangle rdst = new Rectangle((int)(r.X - z), (int)(r.Y - z), (int)(16 * z), (int)(32 * z));
                Rectangle rsrc = new Rectangle(0, 0, 16, 32);
                if (i == npcs.SelectedNPC)
                    g.FillRectangle(b, r);
                g.DrawRectangle(p, r);
                g.DrawImage(sprite, rdst, rsrc, GraphicsUnit.Pixel);
            }
        }
        public void DrawLocationNPCTags(Graphics g, LocationNPCs npcs, int z)
        {
            if (npcs.Count == 0)
                return;
            // draw npc strings
            int index = 0;
            int current = 0;
            foreach (NPC npc in npcs.Npcs)
            {
                if (npc != npcs.Npc)
                    DrawLocationNPCTag(g, npcs, npc, index++, null, z);
                else
                    current = index++;
            }
            if (npcs.Npc != null)
                DrawLocationNPCTag(g, npcs, npcs.Npc, current, null, z);
        }
        private void DrawLocationNPCTag(Graphics g, LocationNPCs npcs, NPC npc, int index, NPC parent, int z)
        {
            Rectangle r = new Rectangle();
            SolidBrush brush = new SolidBrush(Color.FromArgb(128, 255, 0, 0));
            SolidBrush brush_ = new SolidBrush(Color.FromArgb(192, 255, 0, 0));
            Font font = new Font("Tahoma", 8.25F);
            Font font_ = new Font("Tahoma", 8.25F, FontStyle.Bold | FontStyle.Underline);
            Font font_b = new Font("Tahoma", 8.25F, FontStyle.Bold);
            Font lucida = new Font("Lucida Console", 8.25F);
            r = new Rectangle((int)(npc.X * 16 * z), (int)(npc.Y * 16 * z), (int)(16 * z), (int)(16 * z));
            r.X++; r.Y += 3;
            r.Width -= 2; r.Height -= 2;
            string name = "NPC #" + index;
            RectangleF label = new RectangleF(new PointF(r.X, r.Y + r.Height),
                g.MeasureString(name, font_, new PointF(0, 0), StringFormat.GenericDefault));
            if (npc != npcs.Npc)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Black)), label);
                g.DrawString(name, font, brush, r.X, r.Y + 14);
            }
            else
            {
                r.Y += r.Height;
                g.FillRectangle(new SolidBrush(Color.FromArgb(192, Color.Black)), label);
                g.DrawString(name, font_, brush_, r.X, r.Y);
                // draw commands
                RectangleF commandbox;
                string text = "$" + npc.EventPointer.ToString("X6") + " ($" + (npc.EventPointer + 0xCA0000).ToString("X6") + ")\n";
                text += Do.EventScriptToText(Model.GetEventScript(npc.EventPointer + 0x0A0000), npc.EventPointer + 0x0A0000, 8, 80);
                commandbox = new RectangleF(r.X + 2, r.Y + 16, 512, (label.Height - 2) * text.Split('\n').Length);
                g.FillRectangle(brush_, commandbox);
                g.DrawString(text, font_b, new SolidBrush(Color.Black), r.X + 6, r.Y + 20);
            }
        }
        //
        public void DrawLocationTreasures(Graphics g, LocationTreasures treasures, double z)
        {
            if (treasures.Treasures.Count == 0)
                return;
            foreach (Treasure treasure in treasures.Treasures)
            {
                if (treasure != treasures.Treasure)
                    DrawLocationTreasure(g, treasures, treasure, z);
            }
            if (treasures.Treasure != null)
                DrawLocationTreasure(g, treasures, treasures.Treasure, z);
        }
        private void DrawLocationTreasure(Graphics g, LocationTreasures treasures, Treasure treasure, double z)
        {
            Pen pen = new Pen(Color.Blue, 2);
            SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.Blue));
            Rectangle r = new Rectangle();
            r.Location = new Point((int)(treasure.X * 16 * z), (int)(treasure.Y * 16 * z));
            r.Size = new Size((int)(16 * z), (int)(16 * z));
            r.X++; r.Y++;
            r.Width -= 2; r.Height -= 2;
            if (treasure == treasures.Treasure)
                g.FillRectangle(brush, r);
            g.DrawRectangle(pen, r);
        }
        public void DrawLocationTreasureTags(Graphics g, LocationTreasures treasures, double z)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.White));
            SolidBrush brush_ = new SolidBrush(Color.FromArgb(192, Color.White));
            Font font = new Font("Tahoma", 8.25F);
            Font font_ = new Font("Tahoma", 8.25F, FontStyle.Bold);
            Rectangle r = new Rectangle();
            foreach (Treasure treasure in treasures.Treasures)
            {
                r.Location = new Point((int)(treasure.X * 16 * z), (int)(treasure.Y * 16 * z));
                r.Size = new Size((int)(16 * z), (int)(16 * z));
                r.X++; r.Y += 3;
                r.Width -= 2; r.Height -= 2;
                string name = "";
                switch (treasure.Type)
                {
                    case 0:
                    case 1: name = "(empty)"; break;
                    case 2: name = "Monster-in-a-box: pack {" + treasure.PropertyNum + "}"; break;
                    case 3: name = "{" + treasure.PropertyNum.ToString("d3") + "} " + Model.ItemNames.GetUnsortedName(treasure.PropertyNum); break;
                    case 4: name = (treasure.PropertyNum * 100) + " GP"; break;
                }
                r.Y += r.Height;
                RectangleF label = new RectangleF(new PointF(r.X, r.Y),
                    g.MeasureString(name, font_, new PointF(0, 0), StringFormat.GenericDefault));
                if (treasure == treasures.Treasure)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(192, Color.Black)), label);
                    g.DrawString(name, font_, brush_, r.X, r.Y);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Black)), label);
                    g.DrawString(name, font, brush, r.X, r.Y);
                }
            }
        }
        //
        public void DrawSolidityMap(Graphics g, LocationMap map, LocationTilemap tilemap, SoliditySet soliditySet, double z)
        {
            SolidBrush b = new SolidBrush(Color.FromArgb(128, Color.Orange));
            Rectangle r = new Rectangle(0, 0, (int)(16 * z), (int)(16 * z));
            bool[] quadrants = new bool[4];
            Size s = new Size();
            s.Width = (int)(256 * Math.Pow(2, map.Width[0]) / 16);
            s.Height = (int)(256 * Math.Pow(2, map.Height[0]) / 16);
            for (int y = 0; y < s.Height; y++)
            {
                for (int x = 0; x < s.Width; x++)
                {
                    r.X = (int)(x * (int)(16 * z));
                    r.Y = (int)(y * (int)(16 * z));
                    SolidityTile tile = soliditySet.Tiles[tilemap.GetTileNum(0, s.Width, x * 16, y * 16)];
                    if (tile.Solid)
                        g.FillRectangle(b, r);
                    else if (tile.Stairs == 1)
                        g.FillPolygon(b,
                            new Point[] { new Point(16 + r.X, 0 + r.Y), new Point(0 + r.X, 16 + r.Y), new Point(16 + r.X, 16 + r.Y) }, FillMode.Winding);
                    else if (tile.Stairs == 2)
                        g.FillPolygon(b,
                            new Point[] { new Point(0 + r.X, 0 + r.Y), new Point(16 + r.X, 16 + r.Y), new Point(0 + r.X, 16 + r.Y) }, FillMode.Winding);
                    else
                    {
                        if (!tile.East)
                            g.FillRectangle(
                                new SolidBrush(Color.FromArgb(128, Color.OrangeRed)),
                                new Rectangle(r.X, r.Y, 4, 16));
                        if (!tile.West)
                            g.FillRectangle(
                                new SolidBrush(Color.FromArgb(128, Color.OrangeRed)),
                                new Rectangle(r.X + 12, r.Y, 4, 16));
                        if (!tile.South)
                            g.FillRectangle(
                                new SolidBrush(Color.FromArgb(128, Color.OrangeRed)),
                                new Rectangle(r.X, r.Y, 16, 4));
                        if (!tile.North)
                            g.FillRectangle(
                                new SolidBrush(Color.FromArgb(128, Color.OrangeRed)),
                                new Rectangle(r.X, r.Y + 12, 16, 4));
                    }
                }
            }
        }
        public void DrawSolidityMap(Graphics g, WorldTilemap tileMap, SoliditySet soliditySet, double z)
        {
            SolidBrush b = new SolidBrush(Color.FromArgb(128, Color.Orange));
            Rectangle r = new Rectangle(0, 0, (int)(16 * z), (int)(16 * z));
            for (int y = 0; y < 256; y++)
            {
                for (int x = 0; x < 256; x++)
                {
                    r.X = (int)(x * (int)(16 * z));
                    r.Y = (int)(y * (int)(16 * z));
                    SolidityTile tile = soliditySet.Tiles[tileMap.GetTileNum(0, x * 16, y * 16)];
                    if (tile.Solid)
                        g.FillRectangle(b, r);
                }
            }
        }
        public void DrawSoliditySet(Graphics g, Tileset tileset, SoliditySet soliditySet)
        {
            SolidBrush b = new SolidBrush(Color.FromArgb(128, Color.Orange));
            Rectangle r = new Rectangle(0, 0, 16, 16);
            if (tileset.Type == TilesetType.Location)
            {
                bool[] quadrants = new bool[4];
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        r.X = x * 16;
                        r.Y = y * 16;
                        SolidityTile tile = soliditySet.Tiles[y * 16 + x];
                        if (tile.Solid)
                            g.FillRectangle(b, r);
                        else
                        {
                            if (!tile.East)
                                g.FillRectangle(
                                    new SolidBrush(Color.FromArgb(128, Color.OrangeRed)),
                                    new Rectangle(r.X, r.Y, 4, 16));
                            if (!tile.West)
                                g.FillRectangle(
                                    new SolidBrush(Color.FromArgb(128, Color.OrangeRed)),
                                    new Rectangle(r.X + 12, r.Y, 4, 16));
                            if (!tile.South)
                                g.FillRectangle(
                                    new SolidBrush(Color.FromArgb(128, Color.OrangeRed)),
                                    new Rectangle(r.X, r.Y, 16, 4));
                            if (!tile.North)
                                g.FillRectangle(
                                    new SolidBrush(Color.FromArgb(128, Color.OrangeRed)),
                                    new Rectangle(r.X, r.Y + 12, 16, 4));
                        }
                    }
                }
            }
            else
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        r.X = x * 16;
                        r.Y = y * 16;
                        SolidityTile tile = soliditySet.Tiles[y * 16 + x];
                        if (tile.Solid)
                            g.FillRectangle(b, r);
                    }
                }
            }
        }
        #endregion
    }
}
