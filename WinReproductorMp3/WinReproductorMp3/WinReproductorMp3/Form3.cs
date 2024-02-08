using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinReproductorMp3
{
    public partial class Form3 : Form
    {
        public string getdatu = "";
        int friend;
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Buscar_Click(object sender, EventArgs e)
        {

            friend = existe(txtUsu.Text);


            if (friend != -1)
            {

                MessageBox.Show(" encontrado");
               
            }

            else {
                MessageBox.Show("no encontrado");

            }

        }

        public int existe(string iuser)
        {
            BaseDatos db0;
            db0 = new BaseDatos();
            db0.Conectar();
            db0.CrearComando(@"select  id from users where username = @name and status =1");
            db0.AsignarParametro("@name", SqlDbType.VarChar, iuser);


            var DBReader = db0.EjecutarConsulta();
            if (DBReader.Read())
            {
                return (int.Parse(DBReader["id"].ToString()));
            }

            else
            {
                return -1;
            }
        }

        private void Enviar_Click(object sender, EventArgs e)
        {
            try
            {
                BaseDatos db3;
                db3 = new BaseDatos();
                db3.Conectar();
                db3.CrearComando(@"insert into Friends(id_Usu1 ,id_Usu2 , status, mensaje ) values (@id_Usu1 ,@id_Usu2, @status, @mensajem) ");
                db3.AsignarParametro("@n", SqlDbType.Int, getdatu);
                db3.AsignarParametro("@id_Usu2d", SqlDbType.Int, friend + "");
                db3.AsignarParametro("@status", SqlDbType.Int, "1");
                db3.AsignarParametro("@mensajem", SqlDbType.VarChar, txtMsj.Text);

                MessageBox.Show("mensaje enviado " + getdatu + " a " + friend);
            }

            catch (Exception mx)
            {
                MessageBox.Show(mx + "");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

 
            BaseDatos db0;
            db0 = new BaseDatos();
            db0.Conectar();
            db0.CrearComando(@"select  Mensaje from  Friends where id_Usu1 = @na and id_Usu2 = @n and status =1");
            db0.AsignarParametro("@n", SqlDbType.Int, getdatu);
            db0.AsignarParametro("@na", SqlDbType.Int, friend + "");

            var DBReader = db0.EjecutarConsulta();
            if (DBReader.Read())
            {
                msj.Text = DBReader["1"].ToString();
            }

            else
            {
                MessageBox.Show("no hay mensajes");
            }
        }
    }
}
