using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Indices
    {
        public long Id { set; get; }
        [MaxLength(10)]
        public string Codigo { set; get; }
        [MaxLength(45)]
        public string Nombre { set; get; }
    }
}
