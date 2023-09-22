using Gestion.EF.Datas;
using System;

namespace Gestion.EF.Models
{
    public class ProyCert_Empleado
    {
        public int Id { get; set; }
        public int ProyCertificadoId { get; set; }
        public ProyCertificado ProyCertificado { get; set; }
        public int ContratistaId { get; set; }
        public Contratistas Contratista { get; set; }
        public string ApellidoNombre { get; set; }
        public float Monto { get; set; }

        public ItemProyCert_Empleado AsData()
        {
            ItemProyCert_Empleado oData = new ItemProyCert_Empleado{ 
                Id = this.Id,
                ProyCertificadoId = this.ProyCertificadoId,
                ContratistaId = this.ContratistaId,
                ApellidoNombre = this.ApellidoNombre,
                Monto = this.Monto
            };
            return oData;
        }
    }
}
