using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grande.Model
{
    public class Conexion
    {
        private const string CADENA_DE_CONEXION = "Server=localhost;Database=grande;Uid=grande;Pwd=barberiagrande;";

        private MySqlConnection con = new MySqlConnection(CADENA_DE_CONEXION);

        private static Conexion cn;

        private Conexion() { }

        public static Conexion getInstance()
        {
            if(cn == null)
            {
                cn = new Conexion();
            }
            return cn;
        }

        public bool executeNonQuery(MySqlCommand cm)
        {
            try
            {
                con.Open();
                cm.Connection = con;
                cm.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable dataTable(MySqlCommand cm)
        {
            try
            {
                cm.Connection = con;
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public string Scalar(MySqlCommand cm)
        {
            try
            {
                cm.Connection = con;
                con.Open();
                return cm.ExecuteScalar().ToString();
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool hayRenglones(MySqlCommand cm)
        {
            try
            {
                cm.Connection = con;
                con.Open();
                MySqlDataReader dr = cm.ExecuteReader();
                return dr.HasRows;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

    }
}
