using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
     public class ItemCopiaActividadUbicacion
    {

        public Planificacion_Proyecto_Actividades pp { set; get; }

        public List<Proyecto_Ubicaciones> Proyecto_Ubicaciones { set; get; }

        public int pIdUbicacionOrigen { set; get; }

        public int pIdUbicacionFInal { set; get; }

    }
}
