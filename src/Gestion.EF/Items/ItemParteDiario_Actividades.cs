using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
      public class ItemParteDiario_Actividades
    {
        public ParteDiario_Actividades pa { set; get;}

        public List<Planificacion_Proyecto_Actividades> Planificacion_Proyecto_Actividades { set; get;}

        public List<ParteDiario> ParteDiario { set; get;}
        public List<Usuarios> Usuarios { set; get;}


    }
}
