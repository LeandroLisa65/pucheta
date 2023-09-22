using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Models
{
    public class FiltroBusquedaCertificado
    {
        public long? Idproyecto { set; get; }
        public long? IdContratista { set; get; }
        public string fechaHasta { set; get; }
        public string fechaDesde { set; get; }
        public string nroCertificado { set; get; }

    }
}
