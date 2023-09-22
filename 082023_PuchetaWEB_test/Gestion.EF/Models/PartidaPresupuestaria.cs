using System;
using System.ComponentModel;

namespace Gestion.EF.Models
{
    public class PartidaPresupuestaria
    {
        public int Id { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
    }
}
