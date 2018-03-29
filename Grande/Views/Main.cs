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
using Grande.POJOS;

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
            checarInventario();
        }

        private void registroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Ventas().ShowDialog();
        }

        public void checarInventario()
        {
            int cantidad = DAOProductos.cantidadProductosBajos();
            if (cantidad > 0)
                btnInventario.Text = "Inventario ( " + cantidad + " )";
            else
                btnInventario.Text = "Inventario";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checarInventario();
        }

        public bool existeEnCarrito(Producto p)
        {
            bool a = false;
            for (int i = 0; i < dgCarrito.Rows.Count; i++)
            {
                if(p.Clave == dgCarrito[0, i].Value.ToString())
                {
                    if(p.Cantidad > int.Parse(dgCarrito[2, i].Value.ToString()))
                    {
                        dgCarrito[2, i].Value = (int.Parse(dgCarrito[2, i].Value.ToString()) + 1) + "";
                    }
                    else
                    {
                        MessageBox.Show("No quedan mas productos en existencia", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }                    
                    a = true;
                    break;
                }
            }
            return a;
        }

        public void agregar()
        {
            string clave = txtCodigo.Text;
            Producto p = DAOProductos.getOne(clave);
            if (p == null)
            {
                MessageBox.Show("Producto no registrado en la base de datos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else if(p.activo == "no"){
                MessageBox.Show("Producto dado de baja en la base de datos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (p.Cantidad > 0)
                {
                    if (!existeEnCarrito(p))
                    {
                        dgCarrito.Rows.Add(new string[] { p.Clave, p.Nombre, "1", "$" + p.Precio });
                    }
                }
                else
                {
                    MessageBox.Show("No quedan mas productos en existencia", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


            }
            txtCodigo.Focus();
            txtCodigo.Text = "";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtCodigo.Text == "")
            {
                MessageBox.Show("Debes escribir la clave del producto para proceder", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigo.Focus();
            }
            else
            {

                agregar();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Seguro quieres cancelar la venta?", "¿Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                limpiado();
            }
            txtCodigo.Focus();
        }

        public void limpiado()
        {
            dgCarrito.Rows.Clear();
            lblTotal.Text = "$0";
        }

        public void total()
        {
            decimal total = 0;
            for (int i = 0; i < dgCarrito.Rows.Count; i++)
            {
                total += decimal.Parse(dgCarrito[2, i].Value.ToString()) * decimal.Parse(dgCarrito[3, i].Value.ToString().Substring(1));
            }
            lblTotal.Text = "$" + total;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int index = dgCarrito.CurrentRow.Index;
            new EditarCantidad(dgCarrito, index).ShowDialog();
            txtCodigo.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Seguro deseas quitar este elemento de la lista?", "¿Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                int index = dgCarrito.CurrentRow.Index;
                dgCarrito.Rows.RemoveAt(index);
            }            
            txtCodigo.Focus();
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            cobrar();
        }

        private void dgCarrito_Paint(object sender, PaintEventArgs e)
        {
            total();
            if(dgCarrito.Rows.Count > 0)
            {
                menu.Enabled = false;
            }
            else
            {
                menu.Enabled = true;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void cobrar()
        {
            if (dgCarrito.Rows.Count < 1)
            {
                MessageBox.Show("Debes agregar minimo un articulo al carrito", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dr = new Cobro(dgCarrito).ShowDialog();
                if(dr == DialogResult.Yes)
                {
                    limpiado();
                }
            }
            checarInventario();
            txtCodigo.Focus();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (txtCodigo.Text == "")
                {
                    cobrar();
                }
                else
                {
                    agregar();
                }
                e.Handled = true;
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
