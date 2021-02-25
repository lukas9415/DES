using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            button2.Visible = true;

            button3.Visible = false;
            button1.Visible = false;
            label3.Visible = false;
        }

        private void decryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            button2.Visible = false;

            button3.Visible = true;
            button1.Visible = true;
            label3.Visible = true;
        }
    }
}
