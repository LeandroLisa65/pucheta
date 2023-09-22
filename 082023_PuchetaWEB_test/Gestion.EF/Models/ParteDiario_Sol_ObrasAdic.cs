using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class ParteDiario_Sol_ObrasAdic
    {
        public int Id { set; get; }
        [DisplayName("Parte Diario ")]
        public int ParteDiarioId { set; get; }
        public ParteDiario ParteDiario { set; get; }

        [MaxLength(250)]
        [DisplayName("Obra Adicional ")]
        public string Obra_Adicional { get; set; }
        [MaxLength(1)]
        [DisplayName("Estado Pedido - Aprobado - Ejecución - Fin")]
        public string Estado { get; set; }
    }
}
