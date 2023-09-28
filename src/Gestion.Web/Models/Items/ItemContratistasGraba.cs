using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class ItemContratistasGraba
    {

        public float Cantidad { set; get; }
        public int ContratistasId { set; get; }
        public int ParteDiario_ActividadesId { set; get; }
        public List<IFormFile> ArchivosPD { get; set; }
        public bool IsArchivoContratistas { set; get; }
        public int ProyectoId { set; get; }
        public int IdParteDiario { set; get; }
        public int IdPlanificacionActividades { set; get; }
        public int Planificacion_Proyecto_ActividadId { set; get; }
        public int parteDiario_ActividadesId { set; get; }
        public List<Calidad_Actividades_Valoracion> lstItemCalidad { set; get; }
        public string lstItemCalidad2 { set; get; }
        public bool MostarItemCalidad { set; get; }
        public string CrearItemCalidad { set; get; }


    }
}
