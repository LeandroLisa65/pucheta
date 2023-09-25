using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class itemCertificado_AplicacionesPago
    {
        public int cap_Id { set; get; }
        public int cap_IdCertificados { set; get; }
        public string cap_NombreApellido { set; get; }
        public string cap_Monto { set; get; }
        public string cap_operacion { set; get; }

    }
}
