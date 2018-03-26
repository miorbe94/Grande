using Grande.Model;
using Grande.POJOS;
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
    public partial class EditarCantidad : Form
    {
        DataGridView dg;
        int row;

        public EditarCantidad()
        {
            InitializeComponent();
        }

        public EditarCantidad(DataGridView dg, int row)
        {
            InitializeComponent();
            this.dg = dg;
            this.row = row;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EditarCantidad_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int valor = int.Parse(textBox1.Text);
                if (valor > 0)
                {
                    string clave = dg[0,row].Value.ToString();
                    Producto p = DAOProductos.getOne(clave);
                    int stock = p.Cantidad;
                    if(stock >= valor)
                    {
                        dg[2, row].Value = textBox1.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No productos suficientes en existencia", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Debes agregar un número mayor a 0", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("El valor debe ser un número entero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
    }
}
