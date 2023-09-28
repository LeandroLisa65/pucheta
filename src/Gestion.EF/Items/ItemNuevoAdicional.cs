using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItemNuevoAdicional
    {
        public double Cantidad { get; set; }
        public string MontoUnitario { get; set; }
        public string IdUbicacion { get; set; }
        public string IdRubro { get; set; }
        public string IdActividad { get; set; }
        public string IdCertificado { get; set; }
        public string UnidadMedida { get; set; }
    }
}
