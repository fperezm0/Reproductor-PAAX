using System;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WinReproductorMp3
{
    public class BaseDatos
    {
        SqlConnection conect;
        SqlCommand sql;
        public BaseDatos()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "Localhost";
            builder.InitialCatalog = "Paax";
            builder.IntegratedSecurity = true;
            conect = new SqlConnection(builder.ConnectionString);
        }

        public void Conectar()
        {
            try
            {
                if (conect != null && !conect.State.Equals(ConnectionState.Closed))
                {
                    throw new Exception("La conexión ya se encuentra abierta.");
                }
                conect.Open();
            }
            catch (DataException ex)
            {
                throw new DataException("La conexión a la base de datos no se pudo concretar: " + ex.ToString());
            }

        }

        public void DesConectar()
        {
            try
            {
                if (conect.State.Equals(ConnectionState.Open))
                {
                    conect.Close();
                }
            }
            catch (DataException ex)
            {
                throw new DataException("La conexión a la base de datos no se pudo concretar: " + ex.ToString());
            }
        }

        public void CrearComando(string comando) {
            sql = new SqlCommand(comando, conect);
        }

        public void AsignarParametro(string param, SqlDbType sdt, string valor)
        {
            sql.Parameters.Add(param, sdt);
            sql.Parameters[param].Value = valor;
        }

        public void EjecutarComando()
        {
            sql.ExecuteNonQuery();
        }

        public SqlDataReader EjecutarConsulta() {
            return sql.ExecuteReader();
        }


    }
}
