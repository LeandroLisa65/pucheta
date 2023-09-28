using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class RolesEdit
    {
        public Roles Rol { set; get; }
        public List<ItemAcciones> Acciones { set; get; }
    }
}
