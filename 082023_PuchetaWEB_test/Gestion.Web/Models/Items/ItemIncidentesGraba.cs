using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class ItemIncidentesGraba
    {
        public Incidentes inc { set; get; }
        public List<IFormFile> Archivos { get; set; }
        public string RolResponsable2 { set; get; }
    }
}
