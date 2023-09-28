using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;


namespace Gestion.EF
{
    public class UsuarioMenu_Login
    {
        public int Idusuario { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public int? Grupo { get; set; }
        public string Avatar { get; set; }

        public List<Menu_Login> gMenu { get; set; }

    }
}
