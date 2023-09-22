using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Gestion.EF.Models
{
     public class Proyecto_Contratista
    {

        public int Id { set; get; }

        public int ProyectoId { set; get; }
        public Proyecto Proyecto { set; get; }

        [DisplayName("Contratistas")]
        public int ContratistaId { get; set; }
        public Contratistas Contratista { get; set; }

        [MaxLength(250)]
        public string sContratistas { set; get; }
    }
}
