using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class Res_IncidentesIndicadoresHome
    {
        public int indCreados { set; get; }
        public int indAsignados { set; get; }
        public int indPorVencer { set; get; }
        public int indVencidos { set; get; }
    }
}
