﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public partial class EditLabel : NewForm
    {
        // variables
        public bool Disable = false;
        private int index
        {
            get
            {
                if (this.number != null)
                    return (int)number.Value;
                if (this.name != null)
                    return name.SelectedIndex;
                return -1;
            }
        }
        private string element;
        private List<EIndex> enotes;
        private EList elist;
        private ProjectDB project { get { return Model.Project; } set { Model.Project = value; } }
        private ComboBox name;
        private ToolStripNumericUpDown number;
        private bool initialized = false;
        private Point editLabelLocation
        {
            get
            {
                if (name != null)
                    return name.Parent.PointToScreen(
                        new Point(name.Bounds.X, name.Bounds.Y + name.Height));
                else
                    return number.GetCurrentParent().PointToScreen(
                        new Point(number.Bounds.X, number.Bounds.Y + number.Height));
            }
        }
        private Timer timer = new Timer();
        // constructor
        public EditLabel(ToolStripControlHost name, ToolStripNumericUpDown number, string element, bool canEditLabel)
        {
            InitializeComponent();
            ContextMenuStrip labelToolStrip = NewLabelToolStrip(canEditLabel);
            if (name != null)
            {
                try
                {
                    this.name = (ComboBox)name.Control;
                    this.name.ContextMenuStrip = labelToolStrip;
                    this.name.SelectedIndexChanged += new EventHandler(name_SelectedIndexChanged);
                }
                catch
                {
                    TextBox textbox = (TextBox)name.Control;
                    textbox.ContextMenuStrip = labelToolStrip;
                }
            }
            if (number != null)
            {
                this.number = number;
                this.number.ContextMenuStrip = labelToolStrip;
                this.number.ValueChanged += new EventHandler(number_ValueChanged);
            }
            SetElement(element);
            RefreshLabel();
            //
            this.Location = editLabelLocation;
            InitializeTimer();
        }
        // functions
        private ContextMenuStrip NewLabelToolStrip(bool canEditLabel)
        {
            ContextMenuStrip labelToolStrip = new ContextMenuStrip();
            labelToolStrip.RenderMode = ToolStripRenderMode.System;
            labelToolStrip.Opening += new CancelEventHandler(labelToolStrip_Opening);
            if (canEditLabel)
            {
                ToolStripMenuItem editLabel = new ToolStripMenuItem();
                editLabel.Image = global::ZONEDOCTOR.Properties.Resources.label;
                editLabel.ImageAlign = ContentAlignment.MiddleCenter;
                editLabel.ImageScaling = ToolStripItemImageScaling.None;
                editLabel.Name = "editLabel";
                editLabel.Text = "Edit Index's Label...";
                editLabel.Click += new EventHandler(editLabel_Click);
                labelToolStrip.Items.Add(editLabel);
            }
            ToolStripMenuItem addToNotes = new ToolStripMenuItem();
            addToNotes.Image = global::ZONEDOCTOR.Properties.Resources.addToNotes;
            addToNotes.ImageAlign = ContentAlignment.MiddleCenter;
            addToNotes.ImageScaling = ToolStripItemImageScaling.None;
            addToNotes.Name = "addToNotes";
            addToNotes.Text = "Add to Project Database...";
            addToNotes.Click += new EventHandler(addToNotes_Click);
            labelToolStrip.Items.Add(addToNotes);
            return labelToolStrip;
        }
        public void SetElement(string element)
        {
            this.element = element;
            if (project == null)
                return;
            switch (element)
            {
                case "Event Scripts": enotes = project.EventScripts; break;
                case "Locations": enotes = project.Locations; break;
            }
            elist = project.ELists.Find(delegate(EList list)
            {
                return list.Name == element;
            });
        }
        private void RefreshLabel()
        {
            this.Updating = true;
            if (elist != null && index >= 0 && index < elist.Labels.Length)
                labelText.Text = elist.Labels[index];
            else if (elist != null && index >= elist.Labels.Length)
                MessageBox.Show("Error loading label in \"" + elist.Name +
                    "\" for index " + index + ". Please report this.",
                    "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Updating = false;
        }
        private bool CheckLoadedProject()
        {
            if (!Model.CheckLoadedProject())
            {
                this.Hide();
                return false;
            }
            SetElement(this.element);
            if (elist != null && index >= 0)
                labelText.Text = elist.Labels[index];
            return true;
        }
        private void InitializeTimer()
        {
            timer.Tick += new EventHandler(delegate
            {
                timer.Stop(); this.Location = editLabelLocation;
            });
            timer.Start();
        }
        // event handlers
        private void number_ValueChanged(object sender, EventArgs e)
        {
            if (Disable)
                return;
            RefreshLabel();
        }
        private void name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Disable)
                return;
            RefreshLabel();
        }
        private void labelToolStrip_Opening(object sender, CancelEventArgs e)
        {
            if (Disable || !CheckLoadedProject())
                e.Cancel = true;
        }
        private void editLabel_Click(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            // make sure project loaded, if not cancel and hide this form
            if (!CheckLoadedProject())
            {
                this.Hide();
                return;
            }
            this.Visible = true;
            if (this.Visible && !initialized)
            {
                this.Location = editLabelLocation;
                initialized = true;
            }
        }
        private void addToNotes_Click(object sender, EventArgs e)
        {
            if (Model.Program.Project == null || !Model.Program.Project.Visible)
                Model.Program.CreateProjectWindow();
            Project temp = Model.Program.Project;
            if (Model.Project == null)
                temp.LoadProject();
            if (Model.Project != null)
            {
                if (elist != null && index < elist.Indexes.Length)
                    temp.AddingFromEditor(element, index, elist.Indexes[index].Label, "(no description)");
                else if (name != null && index < name.Items.Count)
                    temp.AddingFromEditor(element, index, (string)name.SelectedItem, "(no description)");
                temp.BringToFront();
            }
            else
            {
                MessageBox.Show("Could not add element to project database.",
                    "ZONE DOCTOR", MessageBoxButtons.OK);
            }
        }
        private void labelText_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (elist == null)
                return;
            elist.Indexes[index].Label = labelText.Text;
            if (name != null)
            {
                int digits = name.Items.Count.ToString().Length;
                for (int i = 0; i < name.Items.Count; i++)
                    name.Items[i] = Lists.Numerize(elist.Indexes[i].Label, index, digits);
            }
        }
        private void EditLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Hide();
            }
        }
        private void EditLabel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
