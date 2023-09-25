using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class Roles
    {
        public int Id { set; get; }
        [Required]
        [DisplayName("Nombre o Detalle del Rol")]
        public string Detalle { set; get; }

        public List<AccionesRoles>  Acciones { set; get; }
        public bool Activo { set; get; }
        public bool Borrado { set; get; }

    }
}
