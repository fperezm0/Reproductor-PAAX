using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinReproductorMp3
{
    class Cancion
    {
        String nombre, artista, genero, album;
        String ruta;

        public Cancion()
        {
        }

        public Cancion(string nombre, string artista, string genero, string album)
        {
            this.Nombre = nombre;
            this.Artista = artista;
            this.Genero = genero;
            this.Album = album;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Artista { get => artista; set => artista = value; }
        public string Genero { get => genero; set => genero = value; }
        public string Album { get => album; set => album = value; }
        public string Ruta { get => ruta; set => ruta = value; }
    }
}
