using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Provincias
    {
        public int Id { set; get; }
        [MaxLength(250)]
        public string Provincia { set; get; }
        public List <Localidades> Localidades { set; get; }
    }
}
