using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class UsuarioEdit
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NUsuario { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string Avatar { get; set; }
        public bool Activo { get; set; }
        public bool Borrado { get; set; }
        public string Tipo { get; set; }
        public string TipoDocumento { get; set; }        
        public string nDocumento { get; set; }

        public int DepartamentoId { get; set; }
        public int UsuarioTipoConvivienteId { get; set; }

        public List<RolesUsuarios> Roles { get; set; }
    }
}
