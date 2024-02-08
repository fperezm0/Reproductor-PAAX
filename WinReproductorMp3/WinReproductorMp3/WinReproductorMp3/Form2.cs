using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace WinReproductorMp3
{
    public partial class Form2 : Form
    {
      

        public Form2()
        {
            InitializeComponent();
           
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            
                var youTube = YouTube.Default; // starting point for YouTube actions
                var video = youTube.GetVideo(TextUrl.Text); // gets a Video object with info about the video
                File.WriteAllBytes(@"C:\\kike\\" + video.FullName, video.GetBytes());

                var inputFile = new MediaFile { Filename = @"C:\\kike\\" + video.FullName };
                var outputFile = new MediaFile { Filename = $"{@"C:\\Users\\Lobo\Music\\PaaxMusic\\" + video.FullName}.mp3" };

                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);

                    engine.Convert(inputFile, outputFile);
                }





                MessageBox.Show("se ha descargado " + video.Title + " satisfactoriamente ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex);
            }
            this.Hide();
        }

      

    }
}
