using Grande.Model;
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
    public partial class Cobro : Form
    {
        DataGridView dg;
        decimal total;

        public Cobro()
        {
            InitializeComponent();
        }

        public Cobro(DataGridView dg)
        {
            InitializeComponent();
            this.dg = dg;
            calcularTotal();
        }

        public void calcularTotal()
        {
            decimal total = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                total += decimal.Parse(dg[2, i].Value.ToString()) * decimal.Parse(dg[3, i].Value.ToString().Substring(1));
            }
            this.total = total;
            lblTotal.Text = "Total: $" + total;
        }

        private void Cobro_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cobrar();
        }

        public void cobrar()
        {
            bool exito = false;

            decimal cambio = 0;
            if (txt.Text == "")
            {
                exito = DAOVenta.venta(dg);
                if (exito)
                {
                    new Total().ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error al procesar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt.Focus();
                }
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                Regex val = new Regex(@"^[0-9]+([.][0-9]+)?$");
                if (val.IsMatch(txt.Text))
                {
                    decimal recibido = decimal.Parse(txt.Text);
                    cambio = recibido - total;
                    if (cambio < 0)
                    {
                        MessageBox.Show("Debes pedir mas dinero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt.Focus();
                    }
                    else
                    {
                        //proceder al pago, mostrar cambio y cerrar
                        exito = DAOVenta.venta(dg);
                        if (exito)
                        {
                            new Total(cambio).ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Error al procesar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt.Focus();
                        }
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Valor no aceptado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt.Focus();
                }
            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                cobrar();
                e.Handled = true;
            }
        }
    }
}
