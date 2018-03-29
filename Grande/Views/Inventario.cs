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
using BarcodeLib;
using System.Drawing.Imaging;

namespace Grande.Views
{
    public partial class Inventario : Form
    {

        private bool activos = true;

        public Inventario()
        {
            InitializeComponent();
        }

        public void cargarTabla(string texto)
        {
            DataTable dt;
            if (checkFaltantes.Checked == true)
            {
                dt = DAOProductos.getAllNoDescriptionFaltantes(texto);
            }
            else
            {
                dt = DAOProductos.getAllNoDescription(texto, activos);
            }
            if (dt != null)
                dgProductos.DataSource = dt;
            else
                MessageBox.Show("Error al obtener el inventario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            cargarTabla("");
        }

        private void dgProductos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int index = dgProductos.CurrentRow.Index;
                string clave = dgProductos[0, index].Value.ToString();
                txtDescripcion.Text = DAOProductos.getDescripcion(clave);
            }
            catch
            {
            }

        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            string a = new RegistroEscaneo().registrar();
            if (a != null)
            {
                new RegistroDatos(a, RegistroDatos.AGREGAR).ShowDialog();
                cargarTabla("");
                dgProductos.Focus();
            }
                
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgProductos.Rows.Count < 1)
            {
                MessageBox.Show("No hay elementos en la lista", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int index = dgProductos.CurrentRow.Index;
                string mensaje = "";
                if (checkEliminados.Checked)
                {
                    mensaje = "¿Deseas restaurar este producto al inventario?";
                }
                else
                {
                    mensaje = "¿Seguro deseas eliminar este producto?";
                }
                DialogResult dr = MessageBox.Show(mensaje, "¿Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    bool a = false;
                    if (checkEliminados.Checked)
                    {
                        a = DAOProductos.restaurarProducto(dgProductos[0, index].Value.ToString());
                    }
                    else
                    {
                        a = DAOProductos.eliminarProducto(dgProductos[0, index].Value.ToString());
                    }

                    if (a)
                        cargarTabla("");
                    else
                        MessageBox.Show("Error al procesar petición", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtBuscador.Text = "";
                cargarTabla("");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(dgProductos.Rows.Count < 1)
            {
                MessageBox.Show("No hay elementos en la lista", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int index = dgProductos.CurrentRow.Index;
                new RegistroDatos(dgProductos[0, index].Value.ToString(), RegistroDatos.EDITAR).ShowDialog();
                cargarTabla("");
            }
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            cargarTabla(txtBuscador.Text);
        }

        private void dgProductos_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkFaltantes_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFaltantes.Checked)
            {
                checkEliminados.Checked = false;
            }            
            txtBuscador.Text = "";
            cargarTabla(txtBuscador.Text);
            txtBuscador.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string clave = dgProductos[0, dgProductos.CurrentRow.Index].Value.ToString();

            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            Image img = (Image)codigo.Encode(BarcodeLib.TYPE.CODE128B, clave, Color.Black, Color.White, 300, 100);
            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.AddExtension = true;
            dialogo.Filter = "Image PNG (*.png)|*.png";
            dialogo.ShowDialog();
            if (!string.IsNullOrEmpty(dialogo.FileName))
            {
                img.Save(dialogo.FileName, ImageFormat.Png);
            }
            img.Dispose();
        }

        private void checkEliminados_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEliminados.Checked)
            {
                activos = false;
                checkFaltantes.Checked = false;
                btnEliminar.Text = "Restaurar";
            }
            else
            {
                activos = true;
                btnEliminar.Text = "Eliminar";
            }
            txtBuscador.Text = "";
            cargarTabla(txtBuscador.Text);
            txtBuscador.Focus();
        }
    }
}
