using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class AccionesEdit
    {
        public Acciones Accion { get; set; }
        public List<ItemSelect> ddlMenu { get; set; }
    }
}
