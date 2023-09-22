using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gestion.EF.Models
{
    public class RolesUsuarios
    {
        public int Id { set; get; }

        [Required]
        public int UsuariosId { set; get; }
        public Usuarios Usuarios { set; get; }

        [Required]
        public int RolesId { set; get; }
        public Roles Roles { set; get; }

    }
}
