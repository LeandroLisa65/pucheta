using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
     public class Incidentes_Historial
    {
        public int Id { set; get; }

        [DisplayName("Proyecto")]
        public int ProyectoId { set; get; }
      

        [DisplayName("Contratista")]
        public int ContratistasId { get; set; }
        
        [DisplayName("Incidente")]
        public int IncidenteId { set; get; }

        [DisplayName("ParteDiario")]
        public int ParteDiarioId { set; get; }

        [DisplayName("Area Asignada")]
        public int AreaId { set; get; }

        [DisplayName("Estado")]
        public int EstadoId { set; get;}

        [DisplayName("Fecha Est. para Cierre")]
        public DateTime Fecha_Cierre { set; get; }
        //Creacion de Incidente

        [DisplayName("Descripción")]
        public string Creacion_Descripcion { set; get;}

        [DisplayName("Fecha Creación")]
        public DateTime Creacion_Fecha { get; set; }

        [DisplayName("Usuario Creo")]
        public int Creacion_UsuarioId { set; get; }

        [NotMapped]
        public string sProyecto { set; get; }

        [NotMapped]
        public string sIncidente { set; get; }

        [NotMapped]
        public string sContratista { set; get; }

        [NotMapped]
        public string sEstado { set; get; }

        [NotMapped]
        public string sAreaActual { set; get; }

        [NotMapped]
        public string sColorFondo { set; get; }

        [NotMapped]
        public string sDetalleTratamiento { set; get; }

        [NotMapped]
        public string sUltimoComentarioAgregado { set; get; } //Visualiza por tener perfil Supervisor

        [NotMapped]
        public string sUsuarioDueño { set; get; }

        [NotMapped]
        public bool IsArchivosIncidentes { set; get; }

        [NotMapped]
        public List<IFormFile> Archivos { get; set; }

        [NotMapped]
        public List<int> lIdsArchivosAdjuntos { get; set; }

        [NotMapped]
        public bool AgregarEmail { set; get; }

        [NotMapped]
        public string IngresarEmail { set; get; }

        [NotMapped]
        public string Visualiza_CR { set; get; }
        [NotMapped]
        public string Visualiza_CA { set; get; }
        [NotMapped]
        public string Visualiza_PA { set; get; }
        [NotMapped]
        public string Visualiza_AS { set; get; }
        [NotMapped]
        public string Visualiza_SUP { set; get; } //Visualiza por tener perfil Supervisor
    }
}
