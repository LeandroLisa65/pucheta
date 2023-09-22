using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;

namespace Gestion.Web.Models
{

    public class mobile_Usuarios
    {
        public string Email { set; get; }
        public string Clave { set; get; }
        public Usuarios oUsuario { set; get; }


    }
}
