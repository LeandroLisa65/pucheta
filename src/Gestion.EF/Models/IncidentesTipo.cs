using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
     public  class IncidentesTipo
    {
        public int Id { set; get; }

        [MaxLength(100)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [DisplayName("Nombre")]
        public string Nombre { set; get;}

        [DisplayName("Descripción")]
        public string Descripcion { set; get;}

      

    }
}
