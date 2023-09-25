using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Planificacion_Actividades_Tareas
    {
        public int Id { set; get; }

        [DisplayName("Actividades")]
        public int Planificacion_ActividadesId { set; get; }
        public Planificacion_Actividades Planificacion_Actividades { set; get; }

        [MaxLength(250)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [DisplayName("Nombre de la Tarea")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        [DisplayName("Descripción de la Tarea")]
        public string Descripción { get; set; }
        public bool Activo { get; set; }
    }
}
