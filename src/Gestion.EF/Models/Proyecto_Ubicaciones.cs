using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Proyecto_Ubicaciones
    {
        public int Id { set; get; }
        
        [MaxLength(250)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [DisplayName("Nombre de la Ubicación")]
        public string Nombre { get; set; }

        [DisplayName("Descripción de la Ubicación")]
        public string Descripción { get; set; }

        [DisplayName("Proyecto")]
        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }

        public List<Planificacion_Proyecto_Actividades> Planificacion_Contratistas_Actividades { get; set; }

    }
}
