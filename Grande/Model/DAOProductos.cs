using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Grande.Model
{
    class DAOProductos
    {
        private static Conexion con = Conexion.getInstance();

        public static void insertar()
        {
            
        }

        public static void actualizar()
        {

        }

        public static DataTable getAll()
        {
            MySqlCommand cm = new MySqlCommand("SELECT * FROM productos;");
            return con.dataTable(cm);
        }

        public static DataTable getAllNoDescription()
        {
            MySqlCommand cm = new MySqlCommand("SELECT clave, nombre, cantidad FROM productos;");
            return con.dataTable(cm);
        }

        public static string getDescripcion(int clave)
        {
            MySqlCommand cm = new MySqlCommand("SELECT descripcion FROM productos where clave = @clave;");
            cm.Parameters.AddWithValue("clave", clave);
            return con.Scalar(cm);
        }

    }
}
