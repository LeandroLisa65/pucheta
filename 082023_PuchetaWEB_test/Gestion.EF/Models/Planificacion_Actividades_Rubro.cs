using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Planificacion_Actividades_Rubro
    {
        public int Id { set; get; }

        [MaxLength(250)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [DisplayName("Nombre del Rubro")]
        public string Nombre { get; set; }

        public bool Activo { get; set; }
    }
}
