using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class ParteDiario_Actividades_Contratista
    {
        public int Id { set; get; }

        public int ParteDiario_ActividadesId { set; get; }
        public ParteDiario_Actividades ParteDiario_Actividades { set; get; }

        public int ParteDiarioId { set; get; } 
        public int NotaPedidoId { set; get; } //Nota Pedido relacionada a esta actividad asiganda al contratista
        public int NotaPedidoDetalleId { set; get; } //Nota Pedido detalle relacionada
        [DisplayName("Contratistas")]
        public int ContratistasId { get; set; }
        public Contratistas Contratistas { get; set; }

        /// <summary>
        /// Cantidad avance real no acumulado.
        /// </summary>
        [DisplayName("Cantidad")]
        [Required(ErrorMessage = "El campo Cantidad es obligatorio")]
        public float Cantidad { set; get; }

        public decimal AvanceActual { set; get; } //Avance de la Actividad para este registro del parte diario
        [NotMapped]
        public string _AvanceActual { set; get; }
        

        public DateTime Fecha { set; get; }
        public string TipoRegistro { set; get; }
        public bool Finalizado { set; get; }
        public bool TrabajoHoy { set; get; }
        public bool? TrabajoAyer { set; get; }

        [NotMapped]
        public float Avance { set; get; }

    }
}
