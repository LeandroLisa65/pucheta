using System;

namespace Gestion.EF.Models
{
    public class ProyCert_Contratista
    {
        public int Id { get; set; }
        public int ProyCertificadoId { get; set; }
        public ProyCertificado ProyCertificado { get; set; }
        public int ContratistaId { get; set; }
        public Contratistas Contratista { get; set; }
        public bool EmiteFactura { get; set; }
        public float PorcentajeIVA { get; set; }
        public float IndiceBase { get; set; }
        public float IndiceActual { get; set; }
        public float PorcentajeActualizacion { get; set; }
        public float Adelanto { get; set; }
        public float PorcentajeDescuentoAnticipo { get; set; }
        public float PorcentajeFondoReparo { get; set; }
        public int NumeroCertificado { get; set; }

    }
}
