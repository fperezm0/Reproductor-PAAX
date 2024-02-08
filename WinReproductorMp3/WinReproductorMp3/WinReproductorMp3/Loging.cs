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
    public partial class Loging : Form
    {

      public string getdata;
        public Loging()
        {
            InitializeComponent();
            label9.Text = getdata;
            txtp.PasswordChar = '*';


        }

        private void Loging_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registro reg=new Registro();
            this.Hide();
            reg.Show();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (txtUs.Text=="Usuario")
            {
                txtUs.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtUs.Text == "")
            {
                txtUs.Text = "Usuario";
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (txtp.Text=="Contraseña")
            {
                txtp.Text = "";
                txtp.UseSystemPasswordChar = true;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (txtp.Text == "")
            {
                txtp.UseSystemPasswordChar = false;
                txtp.Text = "Contraseña";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {

                if (txtUs.Text != string.Empty &&  txtp.Text != string.Empty)
                {
                   
                     
                       
                        if (txtUs.Text=="kike")
                        {
                            Form1 reg = new Form1();
                            reg.getdata = txtUs.Text;
                            Form1 reg2 = reg;
                            reg2.Show();
                            this.Hide();
                        }

                        else
                        {
                            MessageBox.Show("no se encontro el usuario");
                            txtUs.ResetText();
                            txtp.ResetText();
                        }
                  
            }
            else {

                MessageBox.Show("rellene todos los campos");
            }


            }
            catch (Exception ex) { MessageBox.Show("error"+ex); }//Metodo para conectar


        }

   
            private void introducirDatos()
        {
            DatosUsario datos = new DatosUsario();
            datos.nombreUsuario = txtUs.Text;
            datos.password = txtp.Text;
        }

        private void txtp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
