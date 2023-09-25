using System;

namespace Gestion.EF.Models
{
    public class ProyCert_PDActContr
    {
        public int Id { get; set; }
        public int ProyCertificadoId { get; set; }
        public ProyCertificado ProyCertificado { get; set; }
        public int PDActContrId { get; set; }
        public int PlanProyActId { get; set; }
        public int ContratistaId { get; set; }
        public Contratistas Contratista { get; set; }
        public ParteDiario_Actividades_Contratista PDActContr { get; set; }
        public float Cantidad { get; set; }
        public string Estado { get; set; }
        public DateTime FecCreacion { get; set; }
        public int UsuarioCreoId { get; set; }
        public bool MontosAjustables { get; set; }

        /// <summary>
        /// Propiedad solo utilzada para grabar el importe acum ant en los certificados tipo "Cero"
        /// Para contemplar los montos no registrados por datos que no exiten en el sistema.
        /// </summary>
        public float ImporteAcumAnterior { get; set; }

        #region PROPIEDADES DERIVADAS

        public bool EsAjuste
        {
            get
            {
                bool esAjuste = false;
                if (this.PDActContr != null)
                    esAjuste = this.PDActContr.TipoRegistro == ValoresUtiles.TipoRegistro_PDAC_Automatico;
                else
                    esAjuste = this.PDActContrId == 0;
                return esAjuste;
            }
        }

        #endregion
    }
}
