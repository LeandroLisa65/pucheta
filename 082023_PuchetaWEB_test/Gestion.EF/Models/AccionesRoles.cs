using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;

namespace Gestion.EF.Models
{
    public class AccionesRoles
    {
        public int Id { set; get; }

        [Required]
        public int RolesId { set; get; }
      
        [Required]
        public int AccionesID { set; get; }
        public Acciones Acciones { set; get; }



    }
}
