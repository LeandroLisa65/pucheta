using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Dispositivos
    {
        public int Id { set; get; }
        [MaxLength(50)]
        public string Detalle { set; get; }
        public bool Activo { set; get; }
    }
}
