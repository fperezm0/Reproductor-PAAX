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
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
            password.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Loging log=new Loging();
            this.Hide();
            log.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (usuario.Text != String.Empty && password.Text != String.Empty && apellidos.Text != String.Empty && nombre.Text != String.Empty && direcc.Text != String.Empty
                && postal.Text != String.Empty
                && telefono.Text != String.Empty && correo.Text != String.Empty
                )
            {
                try
                {
                    if (!existe(usuario.Text))
                    {
                        BaseDatos db;
                    db = new BaseDatos();

                    db.Conectar();
                    db.CrearComando(@"insert into users (username, password, name ,lastname, addresses ,birthdate ,email ,phone ,postalcode, id_level_usuario, status) values (@username, @password, @name ,@lastname, @addresses ,@birthdate ,@email ,@phone 
,@postalcode, @id_level_usuario, @status) ");
                    db.AsignarParametro("@username", SqlDbType.VarChar, usuario.Text);
                    db.AsignarParametro("@password", SqlDbType.VarChar, password.Text);
                    db.AsignarParametro("@name", SqlDbType.VarChar, nombre.Text);
                    db.AsignarParametro("@lastname", SqlDbType.VarChar, apellidos.Text);
                    db.AsignarParametro("@addresses", SqlDbType.VarChar, direcc.Text);
                    db.AsignarParametro("@birthdate", SqlDbType.VarChar, "1994/02/01");
                    db.AsignarParametro("@email", SqlDbType.VarChar, correo.Text);
                    db.AsignarParametro("@phone", SqlDbType.VarChar, telefono.Text);
                    db.AsignarParametro("@postalcode", SqlDbType.VarChar, postal.Text);
                    db.AsignarParametro("@id_level_usuario", SqlDbType.Int, "1");
                    db.AsignarParametro("@status", SqlDbType.Int, "1");

                    db.EjecutarComando();
                    MessageBox.Show("Registrado");
                    usuario.ResetText();
                    password.ResetText();
                    nombre.ResetText();
                    apellidos.ResetText();
                    direcc.ResetText();
                    correo.ResetText();
                    telefono.ResetText();
                    postal.ResetText();
                    }

                    else
                    {

                        MessageBox.Show("usuario ya existe");

                    }
                }
                catch (Exception ex) { MessageBox.Show("error" + ex); }

            }

            else
            {

                MessageBox.Show("rellene todos los campos");
            }

        }
        private void introducirDatos()
        {
            DatosUsario datos = new DatosUsario();
          
   
       
          
            
           
        }
        public Boolean existe(string iuser)
        {
            BaseDatos db0;
            db0 = new BaseDatos();
            db0.Conectar();
            db0.CrearComando(@"select  id from users where username = @name and status =1");
            db0.AsignarParametro("@name", SqlDbType.VarChar, iuser);


            var DBReader = db0.EjecutarConsulta();
            if (DBReader.Read())
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        private void Registro_Load(object sender, EventArgs e)
        {

        }

        private void correo_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcontraseñ_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
