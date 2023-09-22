using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
     public class ItemParteDiario
    {
        public ParteDiario pd { set; get;}

        public List<Proyecto> Proyecto { set; get; }


    }
}
