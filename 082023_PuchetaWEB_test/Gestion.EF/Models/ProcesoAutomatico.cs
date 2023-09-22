using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Models
{
    public class ProcesoAutomatico
    {
        public int Id { get; set; }
        public DateTime FecCreacion { get; set; }
        public int EntidadId { get; set; }
        public string Entidad { get; set; }
        public string Motivo { get; set; }
    }
}
