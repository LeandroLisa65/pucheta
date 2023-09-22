using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Models
{
    public class itemDTIndicadoresIncidentes
    {
        public string Nombre { set; get; }
        public string Estado { set; get; }
        public string Creador { set; get; }
        public string Proyecto { set; get; }
        public string F_Creacion { set; get; }
        public string F_Cierre { set; get; }
        public int Duracion { set; get; }
        public int Dias_Abierto { set; get; }
        public int Cantidad { set; get; }
    }
}
