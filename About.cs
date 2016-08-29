using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR
{
    public partial class About : NewForm
    {
        private Editor form1;
        public About(Editor form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }
        private void richTextBox1_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}