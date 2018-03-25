using Grande.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grande.Model;

namespace Grande
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Inventario().ShowDialog();
        }

        private void registroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Ventas().ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dgCarrito_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Seguro quieres cancelar la venta?", "¿Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                limpiado();
            }
        }

        public void limpiado()
        {
            //limpiar dgv
            //limpiar arraylist
            //limpiar total
        }
    }
}
