using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Gestion.Web.Models
{
    public class ItemContratistasFile
    {
        public ParteDiario_Actividades_Contratista pac { set; get; }
        public List<Contratistas> Contratistas { set; get; }

        public List<IFormFile> ArchivosPD { get; set; }
        public int ProyectoId { set; get; }
        public int IdParteDiario { set; get; }
        public int IdPlanificacionActividades { set; get; }
        public int Planificacion_Proyecto_ActividadId { set; get; }
        public int parteDiario_ActividadesId { set; get; }
        public List<Calidad_Actividades_Valoracion> lstItemCalidad { set; get; }

        public bool MostarItemCalidad { set; get; }

    }
}
