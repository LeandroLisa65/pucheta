using System.Collections.Generic;
using Gestion.EF.Models;

namespace Gestion.Web.Models
{
    public class Res_IncidentesHistorial
    {
        public List<string> lstProyectos { set; get; }
        public List<string> lstTiposIncidentes { set; get; }
        public List<string> lstAreas { set; get; }
    }
}
