    using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Models
{
    public class CertContr_PlanProyAct
    {
        public int Id { get; set; }
        public int CertContrId { get; set; }
        public ContrCertificado CertContr { get; set; }
        public int PlanProyActId { get; set; }
        public Planificacion_Proyecto_Actividades PlanProyAct { get; set; }

        public int NroItem { get; set; }
        public string Ubicacion { get; set; }
        public string Actividad { get; set; }
        public string UnidadMedida { get; set; }
        public string Partida { get; set; }
        public float CantidadPlanificada { get; set; }
        public float MontoPlanificado { get; set; }
        public float TotalPlanificado
        {
            get
            {
                return this.CantidadPlanificada * this.MontoPlanificado;
            }
        }
        public float CantidadAcumAnterior { get; set; }
        public float CantidadActual { get; set; }
        public float CantidadAjuste { get; set; }
        public float CantidadAcumulada
        {
            get
            {
                return this.CantidadAcumAnterior + this.CantidadActual + this.CantidadAjuste;
            }
        }
        public float ImporteAcumAnterior { get; set; }
        public float ImporteActual
        {
            get
            {
                return this.MontoPlanificado * (this.CantidadActual + this.CantidadAjuste);
            }
        }
        public float ImporteAcumulado
        {
            get
            {
                return this.ImporteAcumAnterior + this.ImporteActual;
            }
        }
    }
}
