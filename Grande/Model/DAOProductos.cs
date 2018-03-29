using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grande.POJOS;
using MySql.Data.MySqlClient;

namespace Grande.Model
{
    class DAOProductos
    {
        private static Conexion con = Conexion.getInstance();

        public static int cantidadProductosBajos()
        {
            MySqlCommand cm = new MySqlCommand("select count(*) from productos where cantidad <= cantidadminima;");
            return int.Parse(con.Scalar(cm));
        }

        public static DataTable getAll()
        {
            MySqlCommand cm = new MySqlCommand("SELECT * FROM productos;");
            return con.dataTable(cm);
        }

        public static DataTable getAllNoDescription(string texto)
        {
            MySqlCommand cm = new MySqlCommand("SELECT clave as Clave, nombre as Nombre, cantidad as Cantidad, cantidadminima as `Cantidad minima`, precio as Precio FROM productos where nombre like @nom;");
            cm.Parameters.AddWithValue("nom", "%"+texto+"%");
            return con.dataTable(cm);
        }

        public static DataTable getAllNoDescriptionFaltantes(string texto)
        {
            MySqlCommand cm = new MySqlCommand("SELECT clave as Clave, nombre as Nombre, cantidad as Cantidad, cantidadminima as `Cantidad minima`, precio as Precio FROM productos where nombre like @nom and cantidad <= cantidadminima;");
            cm.Parameters.AddWithValue("nom", "%" + texto + "%");
            return con.dataTable(cm);
        }

        public static string getDescripcion(string clave)
        {
            MySqlCommand cm = new MySqlCommand("SELECT descripcion FROM productos where clave = @clave;");
            cm.Parameters.AddWithValue("clave", clave);
            return con.Scalar(cm);
        }

        public static bool existeProducto(string clave)
        {
            MySqlCommand cm = new MySqlCommand("SELECT * FROM productos where clave = @clave;");
            cm.Parameters.AddWithValue("clave", clave);
            return con.hayRenglones(cm);
        }

        public static bool agregarProducto(Producto p)
        {
            MySqlCommand cm = new MySqlCommand("insert into productos values(@clave, @nombre, @descripcion, @cantidad, @cantidadminima, @precio)");
            cm.Parameters.AddWithValue("clave", p.Clave);
            cm.Parameters.AddWithValue("nombre", p.Nombre);
            cm.Parameters.AddWithValue("descripcion", p.Descripcion);
            cm.Parameters.AddWithValue("cantidad", p.Cantidad);
            cm.Parameters.AddWithValue("cantidadminima", p.CantidadMinima);
            cm.Parameters.AddWithValue("precio", p.Precio);
            return con.executeNonQuery(cm);
        }

        public static bool eliminarProducto(string clave)
        {
            MySqlCommand cm = new MySqlCommand("UPDATE `grande`.`productos` SET `cantidad`= 0, `cantidadminima` = 0 WHERE `clave`=@clave;");
            cm.Parameters.AddWithValue("clave", clave);
            return con.executeNonQuery(cm);
        }

        public static bool editarProducto(Producto p)
        {
            MySqlCommand cm = new MySqlCommand("UPDATE `grande`.`productos` SET `nombre`=@nombre, `descripcion`=@descripcion, `cantidad`=@cantidad, `cantidadminima`=@cantidadminima, `precio`=@precio WHERE `clave`=@clave;");
            cm.Parameters.AddWithValue("clave", p.Clave);
            cm.Parameters.AddWithValue("nombre", p.Nombre);
            cm.Parameters.AddWithValue("descripcion", p.Descripcion);
            cm.Parameters.AddWithValue("cantidad", p.Cantidad);
            cm.Parameters.AddWithValue("cantidadminima", p.CantidadMinima);
            cm.Parameters.AddWithValue("precio", p.Precio);
            return con.executeNonQuery(cm);
        }

        public static Producto getOne(string clave)
        {
            MySqlCommand cm = new MySqlCommand("SELECT * FROM productos where clave = @clave;");
            cm.Parameters.AddWithValue("clave", clave);
            DataTable dt = con.dataTable(cm);
            if (dt.Rows.Count > 0)
            {
                Producto p = new Producto();
                p.Clave = dt.Rows[0][0].ToString();
                p.Nombre = dt.Rows[0][1].ToString();
                p.Descripcion = dt.Rows[0][2].ToString();
                p.Cantidad = int.Parse(dt.Rows[0][3].ToString());
                p.CantidadMinima = int.Parse(dt.Rows[0][4].ToString());
                p.Precio = decimal.Parse(dt.Rows[0][5].ToString());
                return p;
            }
            else
            {
                return null;
            }
            
        }



    }
}
