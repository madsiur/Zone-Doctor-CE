using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public partial class Locations
    {
        #region Variables
        private LocationEvents events { get { return location.LocationEvents; } set { location.LocationEvents = value; } }
        private Object copyEvent;
        public ListBox EventListBox { get { return eventListBox; } set { eventListBox = value; } }
        public NumericUpDown EventX { get { return eventX; } set { eventX = value; } }
        public NumericUpDown EventY { get { return eventY; } set { eventY = value; } }
        #endregion
        #region Methods
        private void InitializeEventProperties()
        {
            this.Updating = true;
            eventListBox.Items.Clear();
            entranceEvent.Value = events.EntranceEvent;
            if (events.Events.Count == 0)
            {
                this.Updating = false;
                groupBox17.Enabled = false;
                eventsCopyField.Enabled = false;
                eventsDuplicateField.Enabled = false;
                buttonDeleteEvent.Enabled = false;
                return;
            }
            //
            for (int i = 0; i < events.Events.Count; i++)
                eventListBox.Items.Add("EVENT #" + i.ToString());
            events.CurrentEvent = events.SelectedEvent = eventListBox.SelectedIndex = 0;
            RefreshEventProperties();
            this.Updating = false;
        }
        private void RefreshEventProperties()
        {
            this.Updating = true;
            groupBox17.Enabled = events.Events.Count != 0;
            eventsCopyField.Enabled = events.Events.Count != 0;
            eventsDuplicateField.Enabled = events.Events.Count != 0;
            buttonDeleteEvent.Enabled = events.Events.Count != 0;
            //
            eventX.Value = events.X;
            eventY.Value = events.Y;
            eventEventNum.Value = events.EventPointer;
            eventsBytesLeft.Text = CalculateFreeEventSpace() + " bytes left";
            eventsBytesLeft.BackColor = CalculateFreeEventSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        private int CalculateFreeEventSpace()
        {
            int used = 0;
            for (int i = 0; i < 415; i++)
            {
                for (int a = 0; a < locations[i].LocationEvents.Events.Count; a++)
                    used += 5;
            }
            return 0x16CE - used;
        }
        //
        private void AddNewEvent(Event newEvent)
        {
            if (CalculateFreeEventSpace() >= 5)
            {
                this.eventListBox.Focus();
                if (events.Count < 72)
                {
                    if (eventListBox.Items.Count > 0)
                        events.New(eventListBox.SelectedIndex + 1, newEvent);
                    else
                        events.New(0, newEvent);
                    int reselect;
                    if (eventListBox.Items.Count > 0)
                        reselect = eventListBox.SelectedIndex;
                    else
                        reselect = -1;
                    eventListBox.BeginUpdate();
                    this.eventListBox.Items.Clear();
                    for (int i = 0; i < events.Count; i++)
                        this.eventListBox.Items.Add("EVENT #" + i.ToString());
                    this.eventListBox.SelectedIndex = reselect + 1;
                    eventListBox.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more event fields. The maximum number of event fields allowed is 72.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the field. " + MaximumSpaceExceeded("events"),
                    "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Event Handlers
        private void eventListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            events.CurrentEvent = eventListBox.SelectedIndex;
            events.SelectedEvent = eventListBox.SelectedIndex;
            RefreshEventProperties();
            this.picture.Invalidate();
        }
        private void entranceEvent_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            events.EntranceEvent = (int)entranceEvent.Value;
        }
        private void buttonInsertEvent_Click(object sender, EventArgs e)
        {
            Point p = new Point(Math.Abs(this.picture.Left) / 16, Math.Abs(this.picture.Top) / 16);
            if (CalculateFreeEventSpace() >= 5)
            {
                this.eventListBox.Focus();
                if (events.Count < 72)
                {
                    if (eventListBox.Items.Count > 0)
                        events.New(eventListBox.SelectedIndex + 1, p);
                    else
                        events.New(0, p);
                    int reselect;
                    if (eventListBox.Items.Count > 0)
                        reselect = eventListBox.SelectedIndex;
                    else
                        reselect = -1;
                    //
                    eventListBox.BeginUpdate();
                    this.eventListBox.Items.Clear();
                    for (int i = 0; i < events.Count; i++)
                        this.eventListBox.Items.Add("EVENT #" + i.ToString());
                    this.eventListBox.SelectedIndex = reselect + 1;
                    eventListBox.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more event fields. The maximum number of event fields allowed per location is 72.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the field. " + MaximumSpaceExceeded("events"),
                    "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void buttonDeleteEvent_Click(object sender, EventArgs e)
        {
            eventListBox.Focus();
            if (eventListBox.SelectedIndex != -1 && events.CurrentEvent == eventListBox.SelectedIndex)
            {
                events.Remove();
                int reselect = eventListBox.SelectedIndex;
                if (reselect == eventListBox.Items.Count - 1)
                    reselect--;
                //
                eventListBox.BeginUpdate();
                eventListBox.Items.Clear();
                for (int i = 0; i < events.Events.Count; i++)
                    eventListBox.Items.Add("EVENT #" + i.ToString());
                if (eventListBox.Items.Count > 0)
                    eventListBox.SelectedIndex = reselect;
                else
                {
                    eventListBox.SelectedIndex = -1;
                    this.picture.Invalidate();
                    groupBox17.Enabled = false;
                    RefreshEventProperties();
                }
                eventListBox.EndUpdate();
            }
        }
        private void eventX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            events.X = (byte)eventX.Value;
            this.picture.Invalidate();
        }
        private void eventY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            events.Y = (byte)eventY.Value;
            this.picture.Invalidate();
        }
        private void eventEventNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            events.EventPointer = (int)eventEventNum.Value;
            this.picture.Invalidate();
        }
        //
        private void eventsCopyField_Click(object sender, EventArgs e)
        {
            copyEvent = events.Event.Copy();
            eventsPasteField.Enabled = true;
        }
        private void eventsPasteField_Click(object sender, EventArgs e)
        {
            if (copyEvent == null)
                return;
            AddNewEvent((Event)copyEvent);
        }
        private void eventsDuplicateField_Click(object sender, EventArgs e)
        {
            AddNewEvent(events.Event.Copy());
        }
        //
        private void buttonGotoC_Click(object sender, EventArgs e)
        {
            if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                Model.Program.CreateEventScriptsWindow();
            int offset = (int)entranceEvent.Value + 0x0A0000;
            Model.Program.EventScripts.GotoAddress(offset);
            Model.Program.EventScripts.BringToFront();
        }
        private void buttonGotoD_Click(object sender, EventArgs e)
        {
            if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                Model.Program.CreateEventScriptsWindow();
            int offset = (int)eventEventNum.Value + 0x0A0000;
            Model.Program.EventScripts.GotoAddress(offset);
            Model.Program.EventScripts.BringToFront();
        }
        #endregion
    }
}
