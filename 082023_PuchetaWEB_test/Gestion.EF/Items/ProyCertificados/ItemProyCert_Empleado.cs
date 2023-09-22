using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Datas
{
    public class ItemProyCert_Empleado
    {
        public int Id { get; set; }
        public int ProyCertificadoId { get; set; }
        public int ContratistaId { get; set; }
        public string ApellidoNombre { get; set; }
        public float Monto { get; set; }
    }
}
