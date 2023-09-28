using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.Web.Models;
using Gestion.EF.Models;
using Gestion.EF;
namespace Gestion.Web.Models
{
    public class Res_Gantt
    {
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public string FecProgramadaFin { get; set; }
        public string FecEstimadaFin { get; set; }
        public int DiasDiferencia { get; set; }
        public List<ItemAvences> lAvances { set; get; }
        public object lPPA_Dependencias { set; get; }
        public object lActividades { set; get; }
        public object lUbicaciones { set; get; }
        public object lEstados { set; get; }
        public object lBusqueda { set; get; }

    }
}
