using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class Certificados_detalle_adicional
    {
        [Key]
        public long cda_Id { set; get; }
        [NotMapped]
        public string vis_nro_Certificado_Historico { set; get; }
        [NotMapped]
        public string vis_fecha_certificado_Historico { set; get; }
        public long? cda_IdCertificados { set; get; }
       
        public long? cda_UbicacionId { set; get; }
        [NotMapped]
        public string vis_cda_UbicacionNombre { set; get; }
        public long? cda_RubroId { set; get; }
        [NotMapped]
        public string vis_cda_RubroNombre { set; get; }
        public long? cda_ActividadaId { set; get; }
        [NotMapped]
        public string vis_cda_ActividadNombre { set; get; }
        public string cda_Comentario { set; get; }
        public string cda_UnidadMedida { set; get; }
        public long? cda_Partida_Id { set; get; }
        [NotMapped]
        public string vis_cda_PartidaNombre { set; get; }
        //----//
        public double? cda_Importe_Cantidad_Asignada { set; get; }
        [NotMapped]
        public string cda_Importe_Cantidad_Asignada_UnidadMedida { set; get; }
        public double? cda_Importe_Monto_Unitario { set; get; }
        public DateTime? cda_Importe_FHasta { set; get; }
        [NotMapped]
        public string vis_cda_Importe_FHasta { set; get; }
        //----//
        public double? cda_Moneda_Certificado_Actual { set; get; }
        public bool? cda_EstaAprobada { set; get; }

        [NotMapped]
        public string vis_cda_EstaAprobada { set; get; }

        //----//
    }
}