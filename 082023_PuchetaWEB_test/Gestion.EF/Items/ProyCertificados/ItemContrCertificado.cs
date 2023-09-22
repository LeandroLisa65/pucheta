using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Datas
{
    public class ItemContrCertificado
    {
        public int Id { get; set; }
        public bool EmiteFactura { get; set; }
        public float PorcentajeIVA { get; set; }
        public float IndiceBase { get; set; }
        public float IndiceActual { get; set; }
        public float PorcentajeActualizacion { get; set; }
        public float Adelanto { get; set; }
        public float PorcentajeDescuentoAnticipo { get; set; }
        public float PorcentajeFondoReparo { get; set; }
        public List<ItemPartidaPresupuestaria> lDataPartidas { get; set; }
        public List<ItemProyCert_Empleado> lProyCert_Empleados { get; set; }
        public int ContratistaId { get; set; }
        public string Nombre { get; set; }
        public List<ItemProyCert_PlanProyAct> lPCPPAs { get; set; }
        public List<ItemProyCert_PlanProyAct> lPCPPA_APs { get; set; }
        public List<ItemProyCert_PDActContr> lPCPDAC_As { get; set; }
        public float CantTotalAnterior { get; set; }
        public float CantTotalActual { get; set; }
        public float CantTotalAcumulada { get; set; }
        public float ImpTotalAnterior { get; set; }
        public float ImpTotalActual { get; set; }
        public float ImpTotalAcumulado { get; set; }
        public bool Abierto { get; set; }
        public int NumeroCertificado { get; set; }
    }
}
