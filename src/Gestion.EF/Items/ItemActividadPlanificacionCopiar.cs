using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.EF
{
    public class ItemActividadPlanificacionCopiar
    {
        public int IdPlanificacionProyectoActividad { set; get; }
        public List<int> Proyecto_UbicacionesId { set; get; }
        public int Planificacion_ActividadesId { set; get; }
         public string Fecha_Est_Incio { set; get; }
        public string Fecha_Est_Fin { set; get; }
    }
}
