using Gestion.EF.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class ItemUsuarioEditAvatar
    {

      
        public int Id { get; set; }
        [DisplayName("Nombre de Usuario")]
        [Required(ErrorMessage = "El campo Nombre de Usuario es obligatorio")]
        public string NUsuario { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Ha ingresado caracteres no permitidos.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Ha ingresado caracteres no permitidos.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio")]                            
        [RegularExpression(@"^[\w%_\-.\d]+@[\w.\-]+\.[A-Za-z]{2,6}$", ErrorMessage = "Ingrese un email valido")]
        public string Email { get; set; }

        public string Avatar { get; set; }
        public IFormFile FAvatar { get; set; }

        public List<UsuariosDireccion> UsuariosDireccion { get; set; }
        public List<UsuariosDispositivo> UsuariosDispositivo { get; set; }
        public List<Provincias> Provincias { set; get; }
       
    }
}
