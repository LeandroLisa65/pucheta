using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.EF
{
    public class ItemLoginUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public int Grupo { get; set; }
        public string Avatar { get; set; }
        public int EmpresaId { get; set; }

        public string ApellidoNombre { get; set; }
    }
}
