using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItemUsuarios
    {
        public Usuarios Usuarios { set; get; }
        public List<ItemRoles> Roles { set; get; }

        public List<Provincias> Provincias { set; get; }
        public List<Dispositivos> Dispositivos { set; get; }
       


    }
}
