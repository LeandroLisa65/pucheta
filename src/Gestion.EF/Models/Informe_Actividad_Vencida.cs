using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion.EF.Models
{
    public class Informe_Actividad_Vencida
    {
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public int ProyectoUbicacionId { get; set; }
        public int PlanActividadId { get; set; }
        public int PlanificacionProyectoActividadId { get; set; }
        public DateTime FecUltimoAvance { get; set; }
        public float PorcAvanceObra { get; set; }
        public DateTime Fecha_Est_Fin { get; set; }
        public int DiasVencida { get; set; }
        public DateTime FecCreacion { get; set; }

        [NotMapped]
        public string sProyecto { get; set; }
        [NotMapped]
        public string sProyUbicacion { get; set; }
        [NotMapped]
        public string sPlanActividad { get; set; }
        [NotMapped]
        public string FechaUltimoAvance
        {
            get
            {
                return this.FecUltimoAvance.ToString("dd-MM-yyyy");
            }
        }
        [NotMapped]
        public string FechaEstimadaFin { 
            get
            {
                return this.Fecha_Est_Fin.ToString("dd-MM-yyyy");
            }
        }
        [NotMapped]
        public string FechaCreacion { get
            {
                return this.FecCreacion.ToString("dd-MM-yyyy");
            }
        }
    }
}
