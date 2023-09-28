using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF.Datas
{
    public class ItemProyCert_PlanProyAct
    {
        public int Id { get; set; }
        public int PlanProyActId { get; set; }
        public string  Ubicacion { get; set; }
        public string Actividad { get; set; }
        public string ActividadDescripcion { get; set; }
        public string UnidadMedida { get; set; }
        public int PartidaPresupuestariaId { get; set; }
        public string CodigoPartidaPresupuestaria { get; set; }
        public string DescripcionPartidaPresupuestaria { get; set; }
        public float CantidadPlanificada { get; set; }
        public float MontoPlanificado { get; set; }
        public float MontoPlanificadoOriginal { get; set; }
        public float ImportePlanificado { get; set; }
        public float CantidadAcumAnterior { get; set; }
        public float ImporteAcumAnterior { get; set; }
        public float _ImporteAcumAnterior { get; set; }
        public float CantidadAcumPeriodo { get; set; }
        public float ImporteAcumPeriodo { get; set; }
        public float CantidadAcumAjustePeriodo { get; set; }
        public float ImporteAcumAjustePeriodo { get; set; }
        public float CantidadAcumActual { get; set; }
        public float ImporteAcumActual { get; set; }
        public float PorcentajeCumplido { get; set; }
        public float PorcentajeCumplidoSobreImportePresupuestado { get; set; }
        public float ImportePresupuestado { get; set; }
        public int NotificacionId { get; set; }
        public bool ExcedenteAutorizado { get; set; }
        public float CantidadAutorizadaExcedente { get; set; }
        public bool Visada { get; set; }
        public List<ItemContratistaAvance> lContratistasAvances { get; set; }
        public bool MontosAjustables
        {
            get
            {
                return this.lContratistasAvances.Find(ca => ca.MontosAjustables == false) == null;
            }
        }
                    
    }
}
