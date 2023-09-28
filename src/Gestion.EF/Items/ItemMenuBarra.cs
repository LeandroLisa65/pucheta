using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.EF
{
    public class ItemMenuBarra
    {
        public int IdUsuario { get; set; }
        public string NombreApellido {get; set;}  
        public string Avatar { get; set; }
        public string IP { get; set; }

        public int EmpresaId { get; set; }
        public List<ItemMenus> Menu { get; set; }
    }
}
