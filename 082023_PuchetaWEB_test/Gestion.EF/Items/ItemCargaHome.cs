using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
     public class ItemCargaHome
    {

        public List<ObrasParteDiario> lstObrasParteDiarios { set; get; }
        public List<Carga_Incidentes> lstIncidentes { set; get; }


    }

    public class ObrasParteDiario
    {
        public int id { set; get; }
        public string Proyecto { set; get; }
        public DateTime fechaUltima { set; get; }
    }

    public class Carga_Incidentes
    {
        public int IncidenteId { set; get; }
        public string sIncidente { get; set; }
        public string sComentario { set; get; }
        public string sCriticidad { set; get; }
        public string sProyecto { set; get; }
        public DateTime sFecha { set; get; }
    }
}
