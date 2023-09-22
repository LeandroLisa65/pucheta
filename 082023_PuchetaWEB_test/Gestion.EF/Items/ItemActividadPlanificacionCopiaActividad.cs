using Gestion.EF.Models;
using System.Collections.Generic;


namespace Gestion.EF
{
    public class   ItemActividadPlanificacionCopiaActividad
    {
        public int IdPlanificacionProyectoActividad { set; get; }
        public int IdProyecto { set; get; }
        public List<ItemActividadNombre> ListaActividades { set; get; }
        public int actividadesSeleccionada { set; get; }
        public List<Proyecto_Ubicaciones> Proyecto_Ubicaciones { set; get; }
        public Planificacion_Proyecto_Actividades pp { set; get; }
        public int pIdUbicacionActividad { set; get; }

        public string vis_NombreActividad { set; get; }
        public string vis_NombreUbicacion { set; get; }
        public string vis_FechaInicio { set; get; }
        public string vis_FechaHasta { set; get; }
        public string vis_Detalle { set; get; }

    }
}
