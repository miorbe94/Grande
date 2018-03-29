using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grande.Views
{
    public partial class Total : Form
    {
        public Total()
        {
            InitializeComponent();
        }

        public Total(decimal cambio)
        {
            InitializeComponent();
            lblTotal.Text = "$" + cambio;
        }

        private void Total_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
