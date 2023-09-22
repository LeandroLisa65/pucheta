using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gestion.EF.Models
{
    public partial class UsuariosLog
    {
        public int Id { get; set; }
        public int? IdUsuarios { get; set; }
        [MaxLength(20)]
        public string Ip { get; set; }
        public DateTime? Fecha { get; set; }
        [MaxLength(250)]
        public string Detalle { get; set; }
        public int AccionesId { get; set; }
    }
}
