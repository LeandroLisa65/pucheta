using Gestion.EF.Datas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Models
{
    public class ProyCert_PDActContr_Adicional
    {
        public int Id { get; set; }
        public int ProyCertificadoId { get; set; }
        public ProyCertificado ProyCertificado { get; set; }
        public int ContratistaId { get; set; }
        public Contratistas Contratista { get; set; }
        public string Actividad { get; set; }
        public string ActividadDescripcion { get; set; }
        public string Ubicacion { get; set; }
        public string UnidadMedida { get; set; }
        public string CodigoPartidaPresupuestaria { get; set; }
        public float Cantidad { get; set; }
        public float Monto { get; set; }
        public DateTime FecCreacion { get; set; }
        public int UsuarioCreoId { get; set; }
        public bool MontosAjustables { get; set; }

        public ItemProyCert_PDActContr AsData()
        {
            try
            {
                ItemProyCert_PDActContr oIPCAC = new ItemProyCert_PDActContr()
                {
                    Id = this.Id,
                    ProyCertificadoId = this.ProyCertificadoId,
                    ContratistaId = this.ContratistaId,
                    Cantidad = this.Cantidad,
                    Monto = this.Monto,
                    Actividad = this.Actividad,
                    ActividadDescripcion = this.ActividadDescripcion,
                    Ubicacion = this.Ubicacion,
                    UnidadMedida = this.UnidadMedida,
                    CodigoPartidaPresupuestaria = this.CodigoPartidaPresupuestaria,
                    MontosAjustables = this.MontosAjustables,
                };
                return oIPCAC;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ItemProyCertPDActContr: AsData(): exception.", ex);
            }
        }
    }
}
