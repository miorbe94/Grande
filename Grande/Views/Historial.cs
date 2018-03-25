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
    public partial class Ventas : Form
    {
        public Ventas()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void cargarVentas()
        {
            DataTable dt = DAOVentas.getAll();
            if(dt != null)
            {
                dgVentas.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Error al cargar las ventas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Historial_Load(object sender, EventArgs e)
        {
            cargarVentas();
        }
    }
}
