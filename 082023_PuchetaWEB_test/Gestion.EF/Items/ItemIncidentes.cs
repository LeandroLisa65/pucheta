using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItemIncidentes
    {
        public Incidentes inc { set; get; }

        public List<IncidentesTipo> IncidentesTipo { set; get;}


    }
}
