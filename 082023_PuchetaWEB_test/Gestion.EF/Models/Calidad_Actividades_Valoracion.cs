using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion.EF.Models
{
    public class Calidad_Actividades_Valoracion
    {
        public int Id { set; get;}
        public int IdPlanificacion_Proyecto_ActividadId { set; get; }
        public int IdParteDiario { set; get; }
        public int IdCalidad_Items { set; get; }
        [DisplayName("Valor")]
        public string Valor { set; get; }

        [MaxLength(100)]
        [DisplayName("Descripcion")]
        public string Descripcion { set; get; }

        public int IdIncidente { set; get; } 
        public int IdUsuario { set; get; }

        [NotMapped]
        public List<IFormFile> Archivos { get; set; }
        [NotMapped]
        public string DetalleTareaCalidad { set; get; } //Detalle de los datos del item de calidad
        [NotMapped]
        public string DetalleCompletoTareaCalidad { set; get; } //Detalle de los datos del item de calidad
        [NotMapped]
        public string sCalidadNombre { get; set; }
        [NotMapped]
        public string sCalidadItemCompleto { get; set; }
        [NotMapped]
        public string sProyecto { get; set; }
        [NotMapped]
        public string sUbicacion { get; set; }
        [NotMapped]
        public string sRubro { get; set; }
        [NotMapped]
        public string sActividad { get; set; }
        [NotMapped]
        public string sUsuario { get; set; }
        [NotMapped]
        public string sFecha { get; set; }
        [NotMapped]
        public string sValor { get; set; }
        [NotMapped]
        public string sIncidenteEstado { get; set; }

        [NotMapped]
        public string Avance { get; set; }



    }
}
