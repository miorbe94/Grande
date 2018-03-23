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
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            DataTable dt = DAOProductos.getAll();
            if (dt != null)
                dgProductos.DataSource = dt;
            else
                MessageBox.Show("Error al obtener el inventario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
