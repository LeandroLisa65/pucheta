using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
     public class ItemPlanificacion_Actividades
    {
        public Planificacion_Actividades pa { set; get;}
        
        public List<Planificacion_Actividades_Rubro> Planificacion_Actividades_Rubro { set; get;}
    }
}
