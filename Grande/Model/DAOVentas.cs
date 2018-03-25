using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grande.Model
{
    public class DAOVentas
    {
        private static Conexion con = Conexion.getInstance();

        public static DataTable getAll()
        {
            MySqlCommand cm = new MySqlCommand("SELECT folio as Folio, fecha as Fecha FROM ventas order by folio desc;");
            return con.dataTable(cm);
        }

        public static DataTable getProductosByVenta(int folio)
        {
            MySqlCommand cm = new MySqlCommand("select p.clave, p.nombre, v.cantidad, p.precio from productos_has_ventas v join productos p on p.clave = v.Productos_clave where ventas_folio = @folio;");
            cm.Parameters.AddWithValue("folio", folio);
            return con.dataTable(cm);
        }
    }
}
