using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Datas
{
    public class ItemProyCert_x_Contratista
    {
        /// <summary>
        /// Id de la entidad ProyCertificado
        /// </summary>
        public int Id { get; set; }
        public string Detalle { get; set; }
        public int ProyectoId { get; set; }
        public string ProyectoNombre { get; set; }
        public string FecDesde { get; set; }
        public string FecHasta { get; set; }
        public string FechaCreacion { get; set; }
        public List<ItemContrCertificado> lContrCertificados { get; set; }
        public bool EsCertificadoCero { get; set; }
        public int NumeroCertificado { get; set; }
        public string Periodo
        {
            get
            {
                return this.FecDesde + " - " + this.FecHasta;
            }
        }
    }
}
