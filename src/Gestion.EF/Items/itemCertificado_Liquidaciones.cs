using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class itemCertificado_Liquidaciones
    {
        public int cdl_Id { set; get; }
        public int cdl_IdCertificados { set; get; }
        public string cdl_NroNotaPedido { set; get; }
        public string cdl_Nro_Poliza { set; get; }
        public double? cdl_Actividades_Adicionales { set; get; }
        public double? cdl_Acumulado_Actividades_Planificadas { set; get; }

        public double? cdl_Ajuste_Actualizacion { set; get; }
        public double? cdl_Ajuste_Ind_Actual { set; get; }
        public double? cdl_Ajuste_Ind_Base { set; get; }
        public double? cdl_Ajuste_Porc_Coeficiente { set; get; }
        public double? cdl_Descuento_Por_Adelanto { set; get; }
        public double? cdl_Descuento_Por_Anticipo_Monto { set; get; }
        public double? cdl_Descuento_Por_Anticipo_Porcentaje { set; get; }
        public double? cdl_Fondo_Reparo_Monto { set; get; }

        public double? cdl_Fondo_Reparo_Porcentaje { set; get; }
        public double? cdl_Iva_Monto { set; get; }

        public double? cdl_Iva_Porcentaje { set; get; }
        public double? cdl_Neto_Facturar { set; get; }
        public double? cdl_Total_A_Facturar { set; get; }
        public double? cdl_Total_A_Pagar { set; get; }
        public double? cdl_Total_Sujeto_Ajuste { set; get; }

    }
}
