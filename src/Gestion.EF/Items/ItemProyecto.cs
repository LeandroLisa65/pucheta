using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
     public class ItemProyecto
    {
        public Proyecto pr { set; get;}
        public List<Usuarios> Usuarios { set; get;}
    }
}
