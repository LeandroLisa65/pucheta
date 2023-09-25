using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class Certificados_detalle_planificado
    {
        [Key]
        public long Id { set; get; }
        public long? IdCertificados{ set; get; }
        public long? IdPA { set; get; }
        //Bloque 1
        public string RubroNombre { set; get; }
        public long? RubroId { set; get; }

        public string ActividadNombre { set; get; }
        public string PartidaCodigo { set; get; }
        public long? PartidaId { set; get; }

        public long? IdPPA { set; get; }
        public long? UbicacionId { set; get; }
        public string UbicacionNombre { set; get; }
        public string Detalle { set; get; }
        public long? IdNotaPedido { set; get; }
        public string NroNotaPedido { set; get; }
        public long? IdNotaPedidoDetalle { set; get; }
      

        [NotMapped]
        public string UnidadMedida { set; get; }

        [NotMapped]
        public string Cantidad_Asignada_UniMedida { set; get; }
        public double? Cantidad_Asignada { set; get; }
        public double? Monto_Unitario { set; get; }
        public double? Monto_Total { set; get; }
        //--

        //Check Liquidacion//

        public bool? Act_Presenta_Liquidacion { set; get; }
        //--

        //Acuul Anter - F Desde - F Hasta - Avanc Periodo - Acumulado Total//

        //------------NUEVO BLOQUE  3------------------//
        public double? Act_Acum_Ant_Unid { set; get; }
        public DateTime? Act_F_Desde { set; get; }
        public DateTime? Act_F_Hasta { set; get; }
        public double? Act_Avance_Periodo_Unid { set; get; }
        public double? Act_Acum_Total_Unid { set; get; }


        [NotMapped]
        public string vis_Liquida_FDesde { set; get; }
        [NotMapped]
        public string vis_Liquida_FHasta { set; get; }

      //-----------------------------------------//
       //------------NUEVO BLOQUE  4------------------//
        public double? Act_Acum_Ant_Moneda { set; get; }
        public double? Act_Cert_Actual_Moneda { set; get; }
        public double? Act_Acum_Actual_Moneda { set; get; }
        //-----------------------------------------//
        //------------BLOQUE INDEFINIDO------------------//
        [NotMapped]
        public bool? Act_Esta_Liquidado { set; get; }
        [NotMapped]
        public bool? Act_Se_Modifico { set; get; }

        //-----------------------------------------//

    }
}