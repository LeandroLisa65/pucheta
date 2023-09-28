using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class ItemParteDiarioGrabaIncidente
    {

        public int Id { set; get; }
        public int ParteDiarioId { set; get; }
        public int ProyectoId { set; get; }
        public string sProyecto { set; get; }
        public int IncidenteId { set; get; }
        public int ContratistaId { set; get; }
        public string ComentarioHI { set; get; }
        public string SolicitadoPor { set; get; }
        public string sIncidente { set; get; }
        public string sContratista { set; get; }
        public int AreaId { set; get; }
        public string sAreaActual { set; get; }
        public int EstadoId { set; get; }
        public DateTime FechaCierre { set; get; }
        [NotMapped]
        public string Foto { set; get; }
        public List<IFormFile> Archivos { get; set; }
        public List<List<string>> lFotos { get; set; }
        public List<int> lIdsArchivosAdjuntos { get; set; }

        public bool IsArchivosIncidentes { set; get; }
        public string mUsuarioActual { set; get; }
        public long mIdUsuarioActual { set; get; }

    }
}
