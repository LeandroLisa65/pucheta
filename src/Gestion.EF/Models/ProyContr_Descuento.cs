using Gestion.EF.Datas;
using System;

namespace Gestion.EF.Models
{
    public class ProyContr_Descuento
    {
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
        public int ContratistaId { get; set; }
        public Contratistas Contratista { get; set; }
        public DateTime Fecha { get; set; }
        public string Documento { get; set; }
        public string Numero { get; set; }
        public float Monto { get; set; }

        public ItemProyContr_Descuento AsData()
        {
            ItemProyContr_Descuento oData = new ItemProyContr_Descuento{ 
                Id = this.Id,
                ProyectoId = this.ProyectoId,
                ContratistaId = this.ContratistaId,
                Fecha = this.Fecha.ToString(ValoresUtiles.Formato_dd_MM_yyyy),
                Documento = this.Documento,
                Numero = this.Numero,
                Monto = this.Monto
            };
            return oData;
        }
    }
}
