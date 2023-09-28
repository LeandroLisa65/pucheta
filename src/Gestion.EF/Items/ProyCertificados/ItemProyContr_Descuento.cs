using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Datas
{
    public class ItemProyContr_Descuento
    {
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public int ContratistaId { get; set; }
        public string Fecha { get; set; }
        public string Documento { get; set; }
        public string Numero { get; set; }
        public float Monto { get; set; }
    }
}
