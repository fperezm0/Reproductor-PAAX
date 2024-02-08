using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WinReproductorMp3
{
    internal class DatosUsario
    {
        public String nombreUsuario { get; set; }
        public String password { get; set; }
        public String nombre { get; set; }
        public String apellido { get; set; }
        public String direccion { get; set; }
        public String fecha { get; set; }
        public String correo { get; set; }
        public String telefono { get; set; }
        public String codigoPostal { get; set; }

        public bool conexionBD()
        {
            try
            {
                SqlConnection conn = new SqlConnection("");
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "insert into users values";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
