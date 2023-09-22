using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.EF
{
    public class ItemMenus
    {
        public ItemMenu menu { set; get; }
        public List<ItemMenu> menu_h { set; get; }
        public int Orden { set; get; }
    }
}
