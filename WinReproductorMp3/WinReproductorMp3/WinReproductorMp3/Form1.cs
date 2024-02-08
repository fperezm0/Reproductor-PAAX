
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Media;

namespace WinReproductorMp3
{


    public partial class Form1 : Form
    {
        User u = null;
        public string getdata = "";

        private readonly int vol = 35;//Volumen inicial al cargar el programa
        public string[] ArchivosMP3;   //Lista
        public string[] RutasArchivosMP3; //Ruta
        private string tagArtist = "", tagTitle = "", title = "", tagAlbum = "", Album = "";

        private readonly MediaPlayer Reproductor = new MediaPlayer();//Objeto encargado de reproducir el archivo mp3
        private static TimeSpan tm; //Objeto encargado de guardar el tiempo transcurrido de la canción

        internal static class NativeMethods
        {
            [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
            internal static extern void ReleaseCapture();

            [DllImport("user32.DLL", EntryPoint = "SendMessage")]
            internal static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse); // width of ellipse)

        public Form1()
        {
            InitializeComponent();
            salir();
            login();




        }
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "dJHReKzv0wBcLe2kX4M3jUS84nq6XhSd80XWrgHC",
            BasePath = "https://paax-c031f-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

       


        private void HideSubMenu()
        {
            PanelMedia.Visible = false;
        }

        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        public void AbrirArchivo()
        {

            var BuscarArchivosMP3 = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Archivo mp3|*.mp3|Archivo wav|*.wav|Todos los Archivos|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                FilterIndex = 1
            };
            if (BuscarArchivosMP3.ShowDialog() == DialogResult.OK)
            {
                ArchivosMP3 = BuscarArchivosMP3.SafeFileNames;
                RutasArchivosMP3 = BuscarArchivosMP3.FileNames;
                foreach (var ArchivoMP3 in ArchivosMP3)
                {
                    LstCanciones.Items.Add(ArchivoMP3);
                    LblTotalSongs.Text = (LstCanciones.Items.Count).ToString();
                }
                LstCanciones.SelectedIndex = 0;
            }

        }


        public void llenarMusica()
        {
            if (u != null)
            {
                BaseDatos db0;
                db0 = new BaseDatos();
                db0.Conectar();
                db0.CrearComando(@"select s.name ,s.rut 
from  Follow f, Artists a  ,Songs s 
where f.id_Usu1 = @u
and  a.id = f.id_Artist   
and  a.id = s.collaborators 
 ");
                db0.AsignarParametro("@u", SqlDbType.Int, u.Id + "");


                var DBReader = db0.EjecutarConsulta();
                if (DBReader.Read())
                {
                    MessageBox.Show(DBReader["name"].ToString());
                    MessageBox.Show(DBReader["rut"].ToString());
                }

                else
                {

                }




            }
        }

            public void recoendarmusica() 
            {
               




                MessageBox.Show("la recomendación del dia es el artista " + "anathema" + " con la cancion dreaming light");


               




                






            }

            private void AplicarTag()
        {

            //TAG TITULO DE LA CANCIÓN
            TagLib.File mp3tag = TagLib.File.Create(RutasArchivosMP3[LstCanciones.SelectedIndex]);
            //var duration = mp3tag.Properties.Duration.ToString(@"mm\:ss");

            if (mp3tag.Tag.Title != null && mp3tag.Tag.Title.Length > 1)
            {
                title = mp3tag.Tag.Title;
                LblCancion.Text = title;
               
            }
            else
            {
                mp3tag.Tag.Title = "Título No Disponible";
            }
            //TAG ALBUM DE LA CANCIÓN
            if (mp3tag.Tag.Album == null) ///SI EL ALBUM NO TIENE CONTENIDO
            {
                Album = "Desconocido";
            }
            else
            {
                //DE LO CONTRARIO EXTRAE EL NOMBRE DEL ALBUM
                Album = mp3tag.Tag.Album;
            }
            //TAG ARTISTA O ARTISTAS DE LA CANCIÓN
            if (mp3tag.Tag.Performers.Length == 0)//SI NO HAY NOMBRE DEL ARTISTA
            {
                tagArtist = "Autor Desconocido";
                foreach (string str in mp3tag.Tag.Performers)
                {
                    tagArtist += str;
                    tagArtist += "; ";
                }
                tagArtist = tagArtist.Substring(0, tagArtist.Length);
                LblAutor.Text = tagArtist;
            }
            else
            {
                //DE LO CONTRARIO SE EJECUTA ESTO
                string[] performers = mp3tag.Tag.Performers;
                if (title.Length > 2 && Album != null && performers[0].Length > 1)
                {
                    tagTitle = title;
                    tagArtist = performers[0].ToString();
                    tagAlbum = Album;
                    LblAutor.Text = tagArtist;//ARTISTA
                    LblCancion.Text = tagTitle;//TITULO
                    LblAlbum.Text = tagAlbum;//ALBUM



                }


            }

            try
            {


                if (!existeCopyright(tagArtist))
                {
                    Cancion c = new Cancion();
                    c.Artista = tagArtist;
                    c.Album = tagAlbum;

                    c.Nombre = tagTitle;
                    c.Ruta = RutasArchivosMP3[LstCanciones.SelectedIndex];
                    Insert(c);
                    if (u != null)
                    {
                        int idArtista = existe(tagArtist);
                        int idAlbum = existeAlbum(tagAlbum, idArtista);
                        int idSongs = existeSong(tagTitle, idAlbum);
                        if (idArtista == 1)
                        {
                          
                        }



                        if (idAlbum == 1)
                        {

                           

                        }


                        if (idSongs == 1)
                        {

                          
                        }



                    }
                }

                else {
                    MessageBox.Show("artista con copyright ");
                }

            }
            catch (Exception ex) {

                MessageBox.Show("error" + ex);
            }

        }



        public int existe(string artist) {
          
            
            
                return 1;
            
        


           }


        private void Insert(Cancion c)
        {

            var setter = client.Set("cancion/" + c.Nombre, c);
            MessageBox.Show("guardado en la nube");
        }
        public int existeAlbum(string artist , int album)
        {
           
            
                return 1;
            



        }
        public Boolean existeCopyright(string artist)
        {
         
                return false;
            



        }

        public int existeSong(string song, int album)
        {
          
            
                return 1;
            



        }


        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TStripMenuSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSalir_MouseEnter(object sender, EventArgs e)
        {
            BtnSalir.BackgroundImage = Properties.Resources.closewindow_hover;
        }




        private void BtnSalir_MouseLeave(object sender, EventArgs e)
        {
            BtnSalir.BackgroundImage = Properties.Resources.closewindow_click;
        }

        private void BtnMedia_Click(object sender, EventArgs e)
        {
            ShowSubMenu(PanelMedia);//APARECE EL PANEL MEDIA CON SUS CONTENIDOS
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            AbrirArchivo();
            HideSubMenu();
        }

        private void EvtSiguienteCancion(object sender, EventArgs e)
        {
            if (Reproductor.HasAudio)
            {
                if (LstCanciones.SelectedIndex < LstCanciones.Items.Count - 1)
                {
                    LstCanciones.SelectedIndex += 1;//ADELANTE UNA CANCIÓN
                    AplicarTag();
                    Reproductor.Open(new Uri(RutasArchivosMP3[LstCanciones.SelectedIndex].ToString()));// RUTA
                    LstCanciones.Update();//ACTUALIZA
                }
                else
                {
                    MessageBox.Show("Esta es la última canción", "Reproductor MP3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LblTTranscurrido.Text = "--:--";// TIEMPO PASA A CERO O EL LABEL QUE TENGA PREDETERMINADO
                    TmTiempo.Stop();//SE DETIENE EL TIMER
                    tm = TimeSpan.Zero;//TIEMSPAN A CERO
                    Reproductor.Stop();//SE DETIENE EL REPRODUCTOR
                    TmTiempo.Enabled = false;//SE DEHABILTA EL TIMER
                    BtnDetener.PerformClick();
                }
            }
        }

        private void EvtCancionAnterior(object sender, EventArgs e)
        {
            if (LstCanciones.SelectedIndex != 0)
            {
                LstCanciones.SelectedIndex -= 1;
                AplicarTag();
                LstCanciones.Update();
            }
            else
            {
                LstCanciones.SelectedIndex = 0;
                LstCanciones.Update();
                MessageBox.Show("Esta es la primera canción", "Reproductor MP3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EvtActualizarTiempo(object sender, EventArgs e)
        {
            tm = Reproductor.Position;//POSICION DEL TIMESPAN DEL REPRODUCTOR
            LblTTranscurrido.Text = tm.ToString(@"mm\:ss");//TIEMPO TRANCURRIDO  SE VE EN EL LABEL
            LblTiempo.Text = LblTTranscurrido.Text;//MISMO VALOR PARA AMBOS LABELS
            LblConteo.Text = LstCanciones.SelectedIndex + 1 + " de " + LstCanciones.Items.Count;// MUESTRA EL # DE LA  CANCION ACTUAL Y CUANTAS HAY

            if (Reproductor.NaturalDuration.HasTimeSpan == true)
            {
                MtStatus.Maximum = (int)Reproductor.NaturalDuration.TimeSpan.TotalSeconds;//LO QUE DURA LA CANCION
                MtStatus.Value = (int)Reproductor.Position.TotalSeconds;//POSICION EN MILISEGUNDOS
                LblFinal.Text = Reproductor.NaturalDuration.TimeSpan.ToString(@"mm\:ss");// MUESTRA LA DURACION TOTAL DE LA CANCION ACTUAL
                LblTotal.Text = LblFinal.Text;//MISMO VALOR PARA AMBOS LABELS
            }
            else
            {
                MtStatus.Value = 0;//TRACKBAR SE UBICA AL INICIO
                LblFinal.Text = "--:--";// CERO
            }
        }

        private void BtnDetener_Click(object sender, EventArgs e)
        {
            stop();
        }

        public void stop() {
            LblTTranscurrido.Text = "--:--";
            LblTiempo.Text = "--:--";
            LblFinal.Text = "--:--";
            LblTotal.Text = "--:--";
            MtStatus.Value = 0;// LA BARRA ESTADO DE TRACKBAR SE UBICA AL INICIO
            BtnPausa.Visible = false;//BOTON PAUSA DESAPARECE Y APARECE BOTON PLAY
            TmTiempo.Stop();//SE DETIENE EL TIMER
            TmTiempo.Enabled = false;//SE DESHABILITA EL TIMER
            tm = TimeSpan.Zero;//TimeSpan A CERO
            Reproductor.Stop();//REPRODUCTOR SE DETIENE
        }

        private void BtnPausa_Click(object sender, EventArgs e)
        {
            BtnPausa.Visible = false;// BOTON PAUSA DESPARECE Y APARECE EL BOTON PLAY
            tm = Reproductor.Position;// SE DETIENE LA POSICIÓN DEL TIMESPAN
            Reproductor.Pause();//PAUSA
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (LstCanciones.Items.Count != 0)//SI LISTBOX NO ESTÁ VACÍO
            {
                BtnPausa.Visible = true;    // EL BOTÓN PAUSA APARECE
                LblCancion.Text = Path.GetFileNameWithoutExtension(ArchivosMP3[LstCanciones.SelectedIndex]);
                TmTiempo.Enabled = true;//SE HABILITA EL TIMER
                TmTiempo.Start();//INICIA EL TIMER
                Reproductor.Open(new Uri(RutasArchivosMP3[LstCanciones.SelectedIndex]));//RUTA
                Reproductor.Position = tm;//POSICION DEL REPRODUCTOR
                Reproductor.Play();//REPRODUCE EL REPRODUCTOR CON LA CANCION SELECCIONADA
                Reproductor.MediaEnded += EvtSiguienteCancion;//AVANZA A LA PROXIMA CANCION AL TERMINA LA ANTERIOR
            }
            else
            {
                MessageBox.Show("No hay canciones para reproducir", "Reproductor MP3", MessageBoxButtons.OK, MessageBoxIcon.Information);// SI YA NO HAY MAS CACNIONES
            }
        }

        private void LstCanciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            TmTiempo.Stop();
            //Reíniciamos el valor de tiempo que lleva transcurrido la canción actual
            tm = TimeSpan.Zero;
            AplicarTag();
            LblCancion.Text = Path.GetFileNameWithoutExtension(ArchivosMP3[LstCanciones.SelectedIndex]);//ARCHIVOS SIN EXTENSIÓN
            BtnPlay.PerformClick();//SE REPRODUCE AL SELECCIONAR
        }

        private void MtStatus_Scroll(object sender, EventArgs e)
        {
            Reproductor.Position = TimeSpan.FromSeconds(MtStatus.Value);// AVANZA O RETROCEDE EL TRACKBAR
        }

        private void MtVolumen_ValueChanged(object sender, decimal value)
        {
            Reproductor.Volume = MtVolumen.Value / 100.0f;// VALOR DEL CONTROL DE VOLUMEN ENTRE 100.0F
            LblVol.Text = Reproductor.Volume.ToString();//MUESTRA EL VALOR EN EL LABEL
            BtnVolumen.BackgroundImage = Properties.Resources.low_volume10;//IMAGEN DE INICIO
            LblVolumen.Text = "VOLUMEN";

            if (Reproductor.Volume > 0.65)// SI ES MAYOR A 0.65F
            {
                BtnVolumen.BackgroundImage = Properties.Resources.audio_100px;
            }
            else if (Reproductor.Volume > 0.36)// SI ES MAYOR A 0.36F
            {
                BtnVolumen.BackgroundImage = Properties.Resources.voice100;
            }
            else if (Reproductor.Volume == 0)// SI SU VALOR ES 0
            {
                BtnVolumen.BackgroundImage = Properties.Resources.mute_100;
                LblVolumen.Text = "SILENCIO";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TemaColores.ElegirTema("Defecto");
            //PanelBotones.BackColor = TemaColores.PanelBotones;
            //MsTitulo.BackColor = TemaColores.BarraTitulo;
            //PanelMedia.BackColor = TemaColores.PanelPadre;
            try
            {
                client = new FireSharp.FirebaseClient(fcon);
            }
            catch
            {
                MessageBox.Show("problema");
            }
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            LblVol.Text = (MtVolumen.Value = (int)(Reproductor.Volume = vol)).ToString();
            BtnVolumen.BackgroundImage = Properties.Resources.low_volume10;
        }

        private void MsTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void PanelMenu_MouseMove(object sender, MouseEventArgs e)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void PanelTagLib_MouseMove(object sender, MouseEventArgs e)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void PanelConteo_MouseMove(object sender, MouseEventArgs e)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void MtVolumen_Scroll(object sender, EventArgs e)
        {
            Reproductor.Volume = MtVolumen.Value / 100.0f;
            LblVol.Text = Reproductor.Volume.ToString();
        }

        private void BtnVerRepro_Click(object sender, EventArgs e)
        {
            PxEqualizador.BringToFront();
            HideSubMenu();
        }

        private void BtnVerLista_Click(object sender, EventArgs e)
        {
            LstCanciones.BringToFront();
            HideSubMenu();
        }

        private void TStripMenuItemAgregar_Click(object sender, EventArgs e)
        {
            AbrirArchivo();
        }

        private void LblCancion_Click(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (u == null) {
                Loging l = new Loging();
                this.Hide();
                l.Show();
                stop();
            }

        }

        public void salir(){
            stop();
            botones4.Hide();
            btnChar.Hide();
           btnDes.Hide();
            botones1.Hide();
            botones2.Hide();
            getdata = "";
            lab1.Text = "";
                limpiar();
            botones3.Show();
            u = null;
        
        }

        public void login()
        {


            if (getdata != String.Empty)
            {

                stop();
                botones3.Hide();
                btnChar.Show();
                botones1.Show();
                botones2.Show();
                btnDes.Show();
                lab1.Text = "Bienvenido: " + getdata;
                

             
                

             

            }

           
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            login();
            recoendarmusica();


        }

      

            private void Us_Click(object sender, EventArgs e)
        {

        }

        private void TStripMenuItemArchivos_Click(object sender, EventArgs e)
        {

        }

        private void chat_Click(object sender, EventArgs e)
        {
            if (u != null)
            {
              

            }
            else { } 
        }


        public void html() {


            var dateAndTime = DateTime.Now;
            var date = dateAndTime.ToLongDateString();

            String ruta = "C:\\Users\\Lobo\\Desktop\\WinReproductorMp3\\WinReproductorMp3\\WinReproductorMp3\\bin\\Debug\\";
            String html = "<!DOCTYPE html> " + "<html > " + " <head > " +
 "   <title> Reporte de Usuario</title > " +
  " </head > " +
   " <body > " +
    "<div align =left > <img src =https://us.123rf.com/450wm/jovanas/jovanas1810/jovanas181000210/109248296-pir%C3%A1mide-de-el-castillo-en-chichen-itza-icono-plano.jpg?ver=6 width= 200px  align=center></div>" +
            "<h1> Paax </h1 > " +
"<div class=div_contenido  > </div>" +
"<div class=div_footer > " +
"_____________________________________________________________________________________<br />" +
"<br>" +
"<br>" +
"<br>" +
"<br>" +
"<div align =left > <img src =https://thumbs.dreamstime.com/b/notas-musicales-patr%C3%B3n-de-vector-volador-sano-sonido-composici%C3%B3n-signos-estallar-fondo-pantalla-m%C3%BAsica-club-vintage-vuelo-218857212.jpg height= 200px  align=center></div>" +
    "<div class= col-md-12  >" +
      "<div class=  box >" +
        "<div class=  box-header with-border + >" +
         " <h3 class=  box-title > Reporte Usuario </h3>" +
        "</div>" +
        "<div >" +
          "<table border = + 1px” + >" +
             " <thead>" +
             " <tr bgcolor =#00FFD1>" +
                "<th >id</th>" +
                "<th>Username</th>" +
               " <th>nombre</th>" +
               " <th>apellido</th>" +
               "  <th>email</th>" +
               "  <th>status</th>" +
               " <th >fecha de reporte </th>" +
              "</tr> " +
              "</thead>" +
              "<tbody>" +
               " <tr “>" +
               "   <td style = + width: 1px  >" + u.Id + "</td>" +
              "    <td > " + u.Username + " </td>" +
            "       <td >" + u.Name + " </ td > " +
        "        <td >" + u.Lastname + "</td>" +
            "         <td >" + u.Correo + "</td>" +
          "            <td>" + u.Status + "</td>" +
         "         <td>" +
                  date +
       "           </td>" +
      "          </tr>" +
     "         </tbody>" +
    "      </table>" +
   "     </div>" +
  " <div class=“ + box-footer + ></div>" +
 " </div>" +
 "</div>" +
"<h1 align = right” >Codigo QR</h1>" +
"<div align = left> <img src =  https://www.codigos-qr.com/qr/php/qr_img.php?d=ID:1%20User%20name:Enrique%20Password:123Name:%20F%20Enrique%20Perez%20Correo:%20fperezm@toluca.tecnm.mxFecha:1%20mi%C3%83%C2%A9rcoles,%208%20de%20diciembre%20de%202021&s=4&e=     align=  left  ></div>" +
"</div>" +
"</body>" +
"</html>";

            File.WriteAllText(ruta + "prueba.html", html);
        }

        private void botones1_Click(object sender, EventArgs e)
        {
            if (u != null)
            {
                try
                {

                    html();
                    string path = AppDomain.CurrentDomain.BaseDirectory + "wkhtmltopdf/wkhtmltopdf.exe";
                    ProcessStartInfo p = new ProcessStartInfo();
                    p.UseShellExecute = false;
                    p.FileName = path;

                    string arg1 = "";

                    arg1 = "prueba.html" +" C:\\Users\\Lobo\\Desktop\\Equipo05EvilCorpReporte.pdf";

                    p.Arguments = arg1;

                    using (Process pro = Process.Start(p))
                    {
                        pro.WaitForExit();
                    }
                    MessageBox.Show("PDf creado");
                    Console.Read();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("error " + ex);
                }
            }

            else {

                MessageBox.Show("se necesita iniciar session para generar reporte");
            }
        }

        private void ChkVolumen_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkVolumen.Checked == true)// SI ESTA MARCADO EL CHECKBOX HACE LO SIGUIENTE
            {
                Reproductor.Volume = 0;
                MtVolumen.Value = 0;
                BtnVolumen.BackgroundImage = Properties.Resources.mute_100;
                LblVolumen.Text = "SILENCIO";
            }
            else
            {
                //SI NO ESTÁ MARCADO EL CHECKBOX HACE LO SIGUIENTE
                Reproductor.Volume = 0.35f;
                MtVolumen.Value = 35;
                BtnVolumen.BackgroundImage = Properties.Resources.low_volume10;
                LblVolumen.Text = "VOLUMEN";
            }
        }

        private void botones2_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void btnDes_Click(object sender, EventArgs e)
        {
            Form2 desca = new Form2();
            desca.Show();
        }

        private void botones3_Click(object sender, EventArgs e)
        {
            if (u == null)
            {
                Loging l = new Loging();
                this.Hide();
                l.Show();
                stop();
            }
        }

        private void btnChar_Click(object sender, EventArgs e)
        {
            Form3 eg = new Form3();
            eg.getdatu = u.Id + "";
            Form3 eg2 = eg;
            eg2.Show();
        }

        private void botones4_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("Ingrese artista con copyright");
                string aer = Console.ReadLine();

              
            }

            catch (Exception exx) {
                Console.WriteLine("error"+exx);
            }
        }

        private void BtnVolumen_Click(object sender, EventArgs e)
        {
            if (ChkVolumen.CheckState == CheckState.Unchecked)// SI NO ESTA MARCADO HACE ESTO
            {
                ChkVolumen.Checked = true;
            }
            else
            {
                //DE LO CONTRARIO HACE ESTO
                ChkVolumen.Checked = false;
            }
        }

        private void MtBalance_ValueChanged(object sender, decimal value)
        {
            Reproductor.Balance = MtBalance.Value;
        }

        public void limpiar()
        {
            if (LstCanciones.Items.Count == 0)
            {
              
                HideSubMenu();// ESCONDE EL PANEL MEDIA
            }
            else if (MessageBox.Show("Está seguro que desea quitar este listado?", "Reproductor MP3",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LblConteo.Text = "0 de 0";
                LblTotalSongs.Text = "0";
                LblCancion.Text = "Título de la Canción";
                LblAutor.Text = "Autor/Cantante";
                LblAlbum.Text = "Album";
                MtVolumen.Value = 35;
                BtnDetener.PerformClick();
                LstCanciones.Items.Clear();
                MessageBox.Show("Se ha borrado el listado actual");
                HideSubMenu();
            }
            else
            {
                HideSubMenu();// ESCONDE EL PANEL MEDIA
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        
    }
}