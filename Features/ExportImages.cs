using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ZONEDOCTOR.Properties;

namespace ZONEDOCTOR
{
    public partial class ExportImages : NewForm
    {
        #region Variables
        private int currentIndex;
        private Location[] locations { get { return Model.Locations; } }
        private PaletteSet[] paletteSets { get { return Model.PaletteSets; } set { Model.PaletteSets = value; } }
        private PrioritySet[] prioritySets { get { return Model.PrioritySets; } set { Model.PrioritySets = value; } }
        private ProgressBar progressBar;
        #endregion
        // Constructor
        public ExportImages(int currentIndex)
        {
            this.currentIndex = currentIndex;
            InitializeComponent();
        }
        // functions
        private void Export()
        {
            bool crop = oneImageCropped.Checked;
            int start;
            int end;
            string fullPath;
            if (current.Checked)
            {
                start = currentIndex;
                end = currentIndex + 1;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image file (*.png)|*.png";
                saveFileDialog.FileName = "Location #" + start.ToString("d3");
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save Image";
                if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                    Settings.Default.LastDirectory = saveFileDialog.FileName;
                fullPath = saveFileDialog.FileName;
            }
            else
            {
                start = (int)fromIndex.Value;
                end = (int)(toIndex.Value + 1);
                // first, open and create directory
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.SelectedPath = Settings.Default.LastDirectory;
                folderBrowserDialog1.Description = "Select directory to export to";
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    Settings.Default.LastDirectory = folderBrowserDialog1.SelectedPath;
                else
                    return;
                fullPath = folderBrowserDialog1.SelectedPath + "\\" + Model.GetFileNameWithoutPath() + " - Location Images\\";
                DirectoryInfo di = new DirectoryInfo(fullPath);
                if (!di.Exists)
                    di.Create();
            }
            // set the backgroundworker properties
            Export_Worker.DoWork += (s, e) => Export_Worker_DoWork(s, e, fullPath, crop, start, end, current.Checked);
            Export_Worker.ProgressChanged += new ProgressChangedEventHandler(Export_Worker_ProgressChanged);
            Export_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Export_Worker_RunWorkerCompleted);
            progressBar = new ProgressBar("EXPORTING LOCATION IMAGES...", locations.Length, Export_Worker);
            progressBar.Show();
            Export_Worker.RunWorkerAsync();
            this.Enabled = false;
            while (Export_Worker.IsBusy)
                Application.DoEvents();
            this.Enabled = true;
        }
        private void Export_Worker_DoWork(object sender, DoWorkEventArgs e, string fullPath,
            bool crop, int start, int end, bool current)
        {
            for (int i = start; i < end; i++)
            {
                if (Export_Worker.CancellationPending)
                    break;
                Export_Worker.ReportProgress(i);
                BackgroundWorker bgw = null;
                LocationMap lmap = locations[i].LocationMap;
                PaletteSet pset = null;
                Tilemap tmap;
                Tileset tset;
                if (i <= 2)
                {
                    switch (i)
                    {
                        case 0: pset = paletteSets[48]; break;
                        case 1: pset = paletteSets[49]; break;
                        case 2: pset = paletteSets[50]; break;
                    }
                    tset = new Tileset(lmap, pset, TilesetType.World);
                    tmap = new WorldTilemap(locations[i], tset, bgw);
                }
                else
                {
                    pset = paletteSets[lmap.PaletteSet];
                    tset = new Tileset(lmap, pset);
                    tmap = new LocationTilemap(locations[i], tset, bgw);
                }
                int[] pixels;
                Rectangle region;
                if (crop && i >= 3)
                {
                    region = new Rectangle(0, 0,
                            lmap.MaskHighX * 16 + 16,
                            lmap.MaskHighY * 16 + 16);
                    if (lmap.MaskHighX == 0) region.Width = tmap.Size_p.Width;
                    if (lmap.MaskHighY == 0) region.Height = tmap.Size_p.Height;
                    pixels = Do.GetPixelRegion(tmap.Pixels, region, tmap.Width_p, tmap.Height_p);
                }
                else
                {
                    region = new Rectangle(0, 0, tmap.Width_p, tmap.Height_p);
                    pixels = tmap.Pixels;
                }
                Bitmap image = Do.PixelsToImage(pixels, region.Width, region.Height);
                if (!current)
                    image.Save(fullPath + "Location #" + i.ToString("d3") + ".png", ImageFormat.Png);
                else
                    image.Save(fullPath, ImageFormat.Png);
                continue;
            }
        }
        private void Export_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar != null && progressBar.Visible)
                progressBar.PerformStep("EXPORTING Location #" + e.ProgressPercentage + " IMAGE");
        }
        private void Export_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (progressBar != null && progressBar.Visible)
                progressBar.Close();
        }
        // event handlers
        private void range_CheckedChanged(object sender, EventArgs e)
        {
            fromIndex.Enabled = range.Checked;
            toIndex.Enabled = range.Checked;
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            Export();
            this.Close();
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
