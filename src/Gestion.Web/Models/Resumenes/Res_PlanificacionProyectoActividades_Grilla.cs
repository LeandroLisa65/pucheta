using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;

namespace Gestion.Web.Models
{
    public class Res_PlanificacionProyectoActividades_Grilla
    {

        public List<Res_PPA> lResPPAs { set; get; }
        public List<string> lstDistinctUbicacion { set; get; }
        public List<string> lstDistinctRubro { set; get; }
        public List<string> lstDistinctActividades { set; get; }
        public List<string> lstDistinctContratistas { set; get; }
        public List<string> lstOrden { set; get; }

    }
}
