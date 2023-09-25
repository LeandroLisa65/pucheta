using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Localidades
    {
        public int Id { set; get; }
        public int ProvinciasId { set; get; }
        [MaxLength(250)]
        public string Localidad { set; get; }
    }
}
