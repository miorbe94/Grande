using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grande.Model
{
    public class DAOVenta
    {
        private static Conexion con = Conexion.getInstance();

        public static bool venta(DataGridView dg)
        {
            MySqlTransaction tr = null;
            MySqlCommand cm = null;
            MySqlCommand cm2 = null;
            try
            {
                con.abrirConexion();
                tr = con.transaction();
                cm = new MySqlCommand("insert into ventas (fecha) values(now());");
                con.executeNonQueryTransaction(cm);
                cm = new MySqlCommand("select max(folio) from ventas;");
                string venta = con.ScalarTransactiction(cm);
                for (int i = 0; i < dg.Rows.Count; i++)
                {
                    cm = new MySqlCommand("insert into productos_has_ventas values(@producto, @venta, @cantidad);");
                    cm.Parameters.Clear();
                    cm.Parameters.AddWithValue("@producto", dg[0, i].Value.ToString());
                    cm.Parameters.AddWithValue("@venta", venta);
                    cm.Parameters.AddWithValue("@cantidad", dg[2, i].Value.ToString());
                    con.executeNonQueryTransaction(cm);

                    cm.Parameters.Clear();
                    cm = new MySqlCommand("UPDATE productos SET cantidad=cantidad - @cantidad WHERE clave=@producto;");
                    cm.Parameters.AddWithValue("@cantidad", dg[2, i].Value.ToString());
                    cm.Parameters.AddWithValue("@producto", dg[0, i].Value.ToString());
                    con.executeNonQueryTransaction(cm);
                }

                tr.Commit();
                return true;
            }
            catch
            {
                tr.Rollback();
                return false;
            }
            finally
            {
                
                con.cerrarConexion();
            }

            //return true o false
        }
    }
}
