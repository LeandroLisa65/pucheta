using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class itemCertificado_CertificadoLiquidacionPPresupuestaria
    {
        public int Id { set; get; }
        public string _codigoPartida { set; get; }
        public string _nombrePartida { set; get; }
        public double _certificadoMonto { set; get; }
        public double _fondoReparo { set; get; }    

    }
}
