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

namespace Grande.Views
{
    public partial class RegistroEscaneo : Form
    {
        private string clave = null;

        public RegistroEscaneo()
        {
            InitializeComponent();
        }

        private void RegistroEscaneo_Load(object sender, EventArgs e)
        {
            
        }

        public string registrar()
        {
            this.ShowDialog();
            return clave;
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            agregar();
        }
            

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }

        public void agregar()
        {
            clave = DAOProductos.existeProducto(txtClave.Text) ? null : txtClave.Text;
            if (clave != null)
                this.Close();
            else
            {
                MessageBox.Show("Este código ya existe en la base de datos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtClave.Text = "";
                txtClave.Focus();
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                agregar();
                e.Handled = true;
            }
        }
    }
}
