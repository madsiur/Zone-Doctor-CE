using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public partial class EditValue : NewForm
    {
        // variables
        // constructor
        public EditValue()
        {
            InitializeComponent();
            this.Location = Cursor.Position;
        }
        // event handlers
        private void button1_Click(object sender, EventArgs e)
        {
            this.Tag = (int)numericUpDown1.Value;
            this.Close();
        }
    }
}
