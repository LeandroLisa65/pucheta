using Gestion.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Models
{
    public class ItemListaContratista
    {
        public int Id { set; get; }
        public List<Proyecto_Contratista> Lista { set; get; }
    }
}
