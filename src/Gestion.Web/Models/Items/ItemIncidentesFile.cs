using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;

namespace Gestion.Web.Models
{
    public class ItemIncidentesFile
    {
        public Incidentes inc { set; get; }

        public List<IncidentesTipo> IncidentesTipo { set; get; }

        public List<IFormFile> Archivos { get; set; }
    }
}
