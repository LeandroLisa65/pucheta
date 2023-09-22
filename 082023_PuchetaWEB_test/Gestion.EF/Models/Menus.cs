using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gestion.EF.Models
{
    public partial class Menus
    {
        [DisplayName("Ids")]
        public int Id { get; set; }
        [DisplayName("Idp")]
        public int? IdMenuPadre { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo Path es obligatorio")]
        public string Path { get; set; }
        [Required(ErrorMessage = "El campo Icon es obligatorio")]
        public string Icon { get; set; }
        [StringLength(1, ErrorMessage = "Campo Tipo Máximo 1 caracter a ingresar")]
        public string Tipo { get; set; }
        public int? Orden { get; set; }
        public bool Activo { set; get; }
        public bool Borrado { set; get; }

     
    }
}
