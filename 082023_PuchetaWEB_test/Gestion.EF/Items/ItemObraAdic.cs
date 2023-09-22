using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
     public class ItemObraAdic
    {
        public ParteDiario_Sol_ObrasAdic oa { set; get;}

        public List<ParteDiario> ParteDiario { set; get;}
    }
}
