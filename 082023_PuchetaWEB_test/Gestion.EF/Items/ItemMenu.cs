using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.EF
{
    public class ItemMenu
    {
        public string Nombre { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public int Orden { get; set; }
        public string Activo { get; set; }
    }
}
