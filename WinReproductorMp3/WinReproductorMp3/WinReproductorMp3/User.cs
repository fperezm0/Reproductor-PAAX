using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinReproductorMp3
{
    class User
    {
        int id;
        String username, password, lastname, correo  ;
        int nivel,status;
        String name;

        

        public User(int id)
        {
            this.Id = id;
            this.nivel = 1;
        }

        public User(int id, string username, string password, string lastname, string correo, int nivel, int status)
        {
            this.Id = id;
            this.username = username;
            this.password = password;
            this.lastname = lastname;
            this.correo = correo;
            this.nivel = nivel;
            this.status = status;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Correo { get => correo; set => correo = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Status { get => status; set => status = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
