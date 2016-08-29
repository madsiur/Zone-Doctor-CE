using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ZONEDOCTOR.Properties;

namespace ZONEDOCTOR
{
    public static class Icons
    {
        private static Bitmap emptyBlock;
        public static Bitmap Emptyblock
        {
            get
            {
                if (emptyBlock == null)
                {
                    emptyBlock = new Bitmap(256, 256);
                    Graphics g = Graphics.FromImage(emptyBlock);
                    Bitmap transparent = Resources._transparent;
                    for (int y = 0; y < 256; y += 8)
                    {
                        for (int x = 0; x < 256; x += 8)
                            g.DrawImage(transparent, x, y);
                    }
                }
                return emptyBlock;
            }
        }
    }
}
