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
using Grande.POJOS;

namespace Grande.Views
{
    public partial class RegistroDatos : Form
    {
        public static int AGREGAR = 1;
        public static int EDITAR = 2;
        private int accion;
        private Producto p;

        public RegistroDatos()
        {
            InitializeComponent();
        }

        public RegistroDatos(string clave, int estado)
        {
            InitializeComponent();
            txtCodigo.Text = clave;
            accion = estado;
            if (estado == 1)
            {
                this.Text = "Agregar producto";
                
            }else if (estado == 2)
            {
                this.Text = "Editar producto";
                p = DAOProductos.getOne(clave);
                if(p != null)
                {
                    txtCantidad.Text = p.Cantidad + "";
                    txtCantidadMinima.Text = p.CantidadMinima + "";
                    txtDescripción.Text = p.Descripcion;
                    txtNombre.Text = p.Nombre;
                    txtPrecio.Text = p.Precio + "";
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void registrarNuevo()
        {
            if (txtNombre.Text != "" && lbl.Text != "" && txtCantidad.Text != "" && txtCantidadMinima.Text != "")
            {
                Producto p = new Producto();
                p.Clave = txtCodigo.Text;
                p.Descripcion = txtDescripción.Text;
                p.Nombre = txtNombre.Text;
                p.Precio = decimal.Parse(txtPrecio.Text);
                p.Cantidad = int.Parse(txtCantidad.Text);
                p.CantidadMinima = int.Parse(txtCantidadMinima.Text);
                bool a = DAOProductos.agregarProducto(p);
                if (a)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al registrar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debes llenar todos los campos con excepción de la descripción que es opcional", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void editarRegistro()
        {
            if (txtNombre.Text != "" && lbl.Text != "" && txtCantidad.Text != "" && txtCantidadMinima.Text != "")
            {
                Producto p = new Producto();
                p.Clave = txtCodigo.Text;
                p.Descripcion = txtDescripción.Text;
                p.Nombre = txtNombre.Text;
                p.Precio = decimal.Parse(txtPrecio.Text);
                p.Cantidad = int.Parse(txtCantidad.Text);
                p.CantidadMinima = int.Parse(txtCantidadMinima.Text);
                bool a = DAOProductos.editarProducto(p);
                if (a)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al editar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debes llenar todos los campos con excepción de la descripción que es opcional", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if(accion == 1)            {
                registrarNuevo();
            }else if(accion == 2)
            {
                editarRegistro();
            }
            
            
        }

        private void RegistroDatos_Load(object sender, EventArgs e)
        {

        }
    }
}
