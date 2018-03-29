using Grande.Model;
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
            bool flag = true; //se debe dar cambio
            bool proceder = true; //se debe ejecutar la venta
            bool errorCambio = false; 

            decimal cambio = 0;
            if (textBox1.Text == "")
            {
                flag = false;
            }else
            {
                try
                {
                    decimal recibido = decimal.Parse(textBox1.Text);
                    cambio = recibido - total;                    
                    if (cambio < 0)
                    {
                        errorCambio = true;
                        proceder = false;
                    }
                }
                catch
                {
                    MessageBox.Show("Error al calcular el cambio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    proceder = false;
                    errorCambio = true;
                }
                
            }
            if (proceder)
            {
                bool a = DAOVenta.venta(dg);
                if (a)
                {
                    if (!flag)
                    {
                        new Total().ShowDialog();
                    }
                    else
                    {
                        new Total(cambio).ShowDialog();                        
                    }
                }
                else
                {
                    MessageBox.Show("Error al procesar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (errorCambio)
                {
                    MessageBox.Show("Debe pedir mas dinero", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Error al procesar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (!errorCambio)
            {
                this.Close();
            }
        }
    }
}
