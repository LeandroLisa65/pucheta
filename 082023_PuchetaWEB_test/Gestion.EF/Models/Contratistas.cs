using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gestion.EF.Models
{
    public class Contratistas
    {
        public int Id { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Barrio es obligatorio")]
        [MaxLength(250)]
        [DisplayName("Barrio")]
        public string Barrio { set; get; }

        [Required(ErrorMessage = "El campo Calle es obligatorio")]
        [MaxLength(250)]
        [DisplayName("Calle")]
        public string Calle { set; get; }

        [Required(ErrorMessage = "El campo Altura es obligatorio")]
        [MaxLength(50)]
        [DisplayName("Altura")]
        public string Altura { set; get; }

        [MaxLength(50)]
        [DisplayName("Piso")]
        public string Piso { set; get; }

        [MaxLength(50)]
        [DisplayName("Departamento")]
        public string Dpto { set; get; }

        [Required(ErrorMessage = "El campo Código Postal es obligatorio")]
        [MaxLength(50)]
        [DisplayName("Código Postal")]
        public string CP { set; get; }

        [MaxLength(50)]
        [DisplayName("Telefono")]
        public string Telefono { set; get; }
        [MaxLength(50)]
        [DisplayName("Celular")]
        public string Celular { set; get; }
        [MaxLength(50)]
        [DisplayName("Email")]
        public string Email { set; get; }
        public int RubroId { get; set; }

        [NotMapped]
        public string sRubro { get; set; }


    }
}
