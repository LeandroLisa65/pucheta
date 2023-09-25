using Gestion.Web.Models.Items;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class ItemParteDiarioIncidenteGraba
    {
        public int Id { set; get; }
        public int ParteDiarioId { set; get; }
        public int IncidenteId { set; get; }

        public int NoConformidadId { set; get; }

        public int ContratistaId { set; get; }

        public string Comentario { set; get; }

        public string SolicitadoPor { set; get; }
        public string sIncidente { set; get; }
        public string sContratista { set; get; }

        public string Criticidad { set; get; }

        public List<IFormFile> Archivos { get; set; }
        public List<ItemArchivoBase64> lArchivosBase64 { get; set; }
        public List<List<string>> lFotos { get; set; }
        public List<int> lIdsArchivosAdjuntos { get; set; }

        public bool IsArchivosIncidentes { set; get; }
    }
}
