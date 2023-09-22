using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Datas
{
    public class ItemContratistaAvance
    {
        public int ContratistaId { get; set; }
        public string ContratistaNombre { get; set; }
        public float CantidadAcumAnterior { get; set; }
        public float CantidadAcumPeriodo { get; set; }
        public float CantidadAcumAjustePeriodo { get; set; }
        public float CantidadAcumActual { get; set; }
        public float ImporteAcumAnterior { get; set; }
        public float ImporteAcumPeriodo { get; set; }
        public float ImporteAcumAjustePeriodo { get; set; }
        public float ImporteAcumActual { get; set; }
        public float PorcentajeCumplidoSobreAcumuladoActual { get; set; }
        public bool Abierto { get; set; }
        public bool Ajustado { get; set; }
        public bool MontosAjustables { get; set; }
    }
}
