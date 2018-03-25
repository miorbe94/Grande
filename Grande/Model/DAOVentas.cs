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
            MySqlCommand cm = new MySqlCommand("SELECT folio as Folio, fecha as Fecha FROM ventas;");
            return con.dataTable(cm);
        }
    }
}
