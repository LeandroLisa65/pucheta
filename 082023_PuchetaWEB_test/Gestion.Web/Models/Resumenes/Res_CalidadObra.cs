using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.Web.Models;
using Gestion.EF.Models;
using Gestion.EF;
namespace Gestion.Web.Models
{
    public class Res_CalidadObra
    {
        public List<Calidad_Actividades_Valoracion> lstItemCalidad { set; get; }
        public List<string> lstProyectos { set; get; }
        public List<string> lstActividades { set; get; }
        public List<string> lstRubros { set; get; }
    }
}
