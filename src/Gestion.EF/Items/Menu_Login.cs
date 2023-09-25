using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class Menu_Login
    {
        public Menus Menu { get; set; }
        public List<Menus> MenuSub { get; set; }
    }
}
