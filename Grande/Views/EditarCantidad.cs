using Grande.Model;
using Grande.POJOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grande.Views
{
    public partial class EditarCantidad : Form
    {
        DataGridView dg;
        int row;
        Producto p;
        string clave;


        public EditarCantidad()
        {
            InitializeComponent();
        }

        public EditarCantidad(DataGridView dg, int row)
        {
            InitializeComponent();
            this.dg = dg;
            this.row = row;
            clave = dg[0, row].Value.ToString();
            p = DAOProductos.getOne(clave);
            lblDisponibles.Text = p.Cantidad + " disponibles";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EditarCantidad_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cambiar();
        }

        public void cambiar()
        {
            Regex reg = new Regex("^[0-9]+$");
            if (reg.IsMatch(textBox1.Text))
            {
                try
                {
                    int valor = int.Parse(textBox1.Text);
                    if (valor > 0)
                    {

                        int stock = p.Cantidad;
                        if (stock >= valor)
                        {
                            dg[2, row].Value = int.Parse(textBox1.Text);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No productos suficientes en existencia", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            textBox1.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Debes agregar un número mayor a 0", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        textBox1.Focus();
                    }
                }
                catch
                {
                    MessageBox.Show("El valor debe ser un número entero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Focus();
                }
            }
            else
            {
                MessageBox.Show("El valor debe ser un número entero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                cambiar();
                e.Handled = true;
            }
        }
    }
}
