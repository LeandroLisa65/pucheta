using System.Collections.Generic;
using Gestion.EF.Models;

namespace Gestion.Web.Models
{
    public class Res_ParteDiario_ActividadFinaliza
    {

        //CAMBIAR TODO ESTO

        // PARTE DIARIO
        public int ProyectoId { set; get; }
        public int IdParteDiario { set; get; }
        public int IdPlanificacionActividades { set; get; }
        public int Planificacion_Proyecto_ActividadId { set; get; }
        public int parteDiario_ActividadesId { set; get; }
        public List<Calidad_Actividades_Valoracion> lstItemCalidad { set; get; }

        public string CrearItemCalidad { set; get; }

        public bool MostarItemCalidad { set; get; }

    }
}