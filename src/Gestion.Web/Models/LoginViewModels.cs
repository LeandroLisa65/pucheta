using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class LoginViewModels
    {
        [BindProperty]
        public InputModel Input { set; get; }

        [TempData]
        public string ErrorMenssage { set; get; }

        public class InputModel
        {
            [Required (ErrorMessage = "<font color='red'>El campo Email es obligatorio</font>")]
            public string NUsuario { set; get; }

            [Required (ErrorMessage = "<font color='red'>El campo Clave es obligatorio</font>")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "<font color='red'>El número de caracteres de {0} debe ser al menos {2}.</font>", MinimumLength =6)]
            public string Password { set; get; }

            public Boolean RememberMe { set; get; }
        }
    }
}
