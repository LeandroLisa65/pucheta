using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
     public class ItemContratista
    {
        public ParteDiario_Contratista pc { set; get;}

        public List<ParteDiario> ParteDiario { set; get;}
        public List<Usuarios> Usuarios { set; get;}


    }
}
