using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItemPUA
    {
        public Proyecto proyecto { set; get; }
        public Proyecto_Ubicaciones proyecto_ubicaciones { set; get; }        
    }
}
