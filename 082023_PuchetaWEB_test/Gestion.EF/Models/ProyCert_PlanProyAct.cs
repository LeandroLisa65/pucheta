using Gestion.EF.Datas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Gestion.EF.Models
{
    public class ProyCert_PlanProyAct
    {
        public int Id { get; set; }
        public int ProyCertificadoId { get; set; }
        public ProyCertificado ProyCertificado { get; set; }
        public int PlanProyActId { get; set; }
        public Planificacion_Proyecto_Actividades PlanProyAct { get; set; }
        public string Ubicacion { get; set; }
        public string Actividad { get; set; }
        public string ActividadDescripcion { get; set; }
        public string UnidadMedida { get; set; }
        public int PartidaPresupuestariaId { get; set; }
        public string CodigoPartidaPresupuestaria { get; set; }
        public string DescripcionPartidaPresupuestaria { get; set; }
        public float CantidadPlanificada { get; set; }
        public float MontoPlanificado { get; set; }
        public bool ExcedenteAutorizado { get; set; }
        public float CantidadAutorizadaExcedente { get; set; }
        public int NotificacionId { get; set; }
        public bool Visada { get; set; }

        public DateTime FecCreacion { get; set; }
        public int UsuarioCreoId { get; set; }

        [NotMapped]
        public List<ProyCert_PDActContr> lPCPDACs { get; set; }

        #region PORPIEDADES DERIVADAS

        public float ImportePlanificado
        {
            get
            {
                return this.MontoPlanificado * this.CantidadPlanificada;
            }
        }
        public float CantidadAcumPeriodo
        {
            get
            {
                return this.lPCPDACs
                    .Where(pcpdac => pcpdac.EsAjuste == false)
                    .Sum(pcpdac => pcpdac.Cantidad);
            }
        }
        public float ImporteAcumPeriodo
        {
            get
            {
                return this.CantidadAcumPeriodo * this.MontoPlanificado;
            }
        }
        public float CantidadAcumAjustePeriodo
        {
            get
            {
                return this.lPCPDACs
                    .Where(pcpdac => pcpdac.EsAjuste == true)
                    .Sum(pcpdac => pcpdac.Cantidad);
            }
        }
        public float ImporteAcumAjustePeriodo
        {
            get
            {
                return this.CantidadAcumAjustePeriodo * this.MontoPlanificado;
            }
        }
        public float PorcentajeCumplido
        {
            get
            {
                float cantidaPlanificada = this.CantidadPlanificada;
                if (cantidaPlanificada == 0) cantidaPlanificada = 1;
                return (this.CantidadAcumPeriodo + this.CantidadAcumAjustePeriodo) * 100 / this.CantidadPlanificada;
            }
        }
        public float ImportePresupuestado
        {
            get
            {
                float importePresupuestado = 0;
                if (this.PlanProyAct != null)
                {
                    importePresupuestado = this.PlanProyAct.Cantidad * this.PlanProyAct.Monto;
                }
                return importePresupuestado;
            }
        }
        public float PorcentajeCumplidoSobreImportePresupuestado
        {
            get
            {
                float porcentaje = 0;
                if(this.PlanProyAct != null)
                {
                    float importePresupuestado = this.PlanProyAct.Cantidad * this.PlanProyAct.Monto;
                    if (importePresupuestado == 0) importePresupuestado = 1;
                    return (this.ImporteAcumPeriodo + this.ImporteAcumAjustePeriodo) * 100 / importePresupuestado;
                }
                return porcentaje;
            }
        }
        public float MontoPlanOriginal
        {
            get
            {
                float monto = 0;
                if (this.PlanProyAct != null)
                    monto = this.PlanProyAct.Monto;
                return monto;
            }
        }
        public float CantidadPlanOriginal
        {
            get
            {
                float cantidad = 0;
                if (this.PlanProyAct != null)
                    cantidad = this.PlanProyAct.Cantidad;
                return cantidad;
            }
        }

        #endregion

        public ItemProyCert_PlanProyAct AsData()
        {
            ItemProyCert_PlanProyAct oData = new ItemProyCert_PlanProyAct();
            try
            {
                float cantidaPlanificada = this.CantidadPlanificada;
                if (cantidaPlanificada == 0) cantidaPlanificada = 1;

                List<ItemContratistaAvance> lContratistasAvances = new List<ItemContratistaAvance>();
                if (this.PlanProyActId > 0)
                {
                    lContratistasAvances = this.lPCPDACs
                        .GroupBy(pcpdac => pcpdac.ContratistaId)
                        .Select(g => new ItemContratistaAvance()
                        {
                            ContratistaId = g.First().ContratistaId,
                            ContratistaNombre = g.First().Contratista.Nombre,
                            CantidadAcumAnterior = 0,
                            CantidadAcumPeriodo = g.ToList()
                                .Where(pcpdac => pcpdac.EsAjuste == false)
                                .Sum(pcpdac => pcpdac.Cantidad),
                            CantidadAcumAjustePeriodo = g.ToList()
                                .Where(pcpdac => pcpdac.EsAjuste == true)
                                .Sum(pcpdac => pcpdac.Cantidad),
                            //falta sumar el anterior
                            CantidadAcumActual = g.ToList()
                                .Sum(pcpdac => pcpdac.Cantidad),
                            ImporteAcumAnterior = g.ToList()
                                .Sum(pcpdac => pcpdac.ImporteAcumAnterior),
                            ImporteAcumPeriodo = this.MontoPlanificado * g.ToList()
                                .Where(pcpdac => pcpdac.EsAjuste == false)
                                .Sum(pcpdac => pcpdac.Cantidad),
                            ImporteAcumAjustePeriodo = this.MontoPlanificado * g.ToList()
                                .Where(pcpdac => pcpdac.EsAjuste == true)
                                .Sum(pcpdac => pcpdac.Cantidad),
                            ImporteAcumActual = this.MontoPlanificado * g.ToList()
                                .Sum(pcpdac => pcpdac.Cantidad),
                            //falta sumar el anterior
                            PorcentajeCumplidoSobreAcumuladoActual = g.ToList()
                                .Sum(pcpdac => pcpdac.Cantidad) * 100 / cantidaPlanificada,
                            Abierto = g.ToList()
                                .Count(pcpdac => pcpdac.Estado == ValoresUtiles.Estado_PCPDAC_Cerrado) == 0,
                            Ajustado = g.ToList()
                                .Count(pcpdac => pcpdac.EsAjuste == true) > 0,
                            MontosAjustables = g.ToList()
                                .Count(pcpdac => pcpdac.MontosAjustables == false) == 0
                        })
                        .ToList();
                }
                oData = new ItemProyCert_PlanProyAct()
                {
                    Id = this.Id,
                    PlanProyActId = this.PlanProyActId,
                    Ubicacion = this.Ubicacion,
                    Actividad = this.Actividad,
                    ActividadDescripcion = this.ActividadDescripcion,
                    UnidadMedida = this.UnidadMedida,
                    PartidaPresupuestariaId = this.PartidaPresupuestariaId,
                    CodigoPartidaPresupuestaria = this.CodigoPartidaPresupuestaria,
                    DescripcionPartidaPresupuestaria = this.DescripcionPartidaPresupuestaria,
                    CantidadPlanificada = this.CantidadPlanificada,
                    MontoPlanificado = this.Id == 0 ? this.PlanProyAct.Monto : this.MontoPlanificado,
                    MontoPlanificadoOriginal = this.PlanProyAct.Monto,
                    ImportePlanificado = this.ImportePlanificado,
                    /// por la complejidad de estos dos campos, los cargamos desde GestionCertificadosController
                    CantidadAcumAnterior = 0,
                    // Aca se suman (si es que se suman) las cantidades acumuladas anteriores
                    // de los certificados tipo "Cero"
                    ImporteAcumAnterior = lContratistasAvances.Sum(ca => ca.ImporteAcumAnterior),
                    _ImporteAcumAnterior = lContratistasAvances.Sum(ca => ca.ImporteAcumAnterior),
                    CantidadAcumPeriodo = this.CantidadAcumPeriodo,
                    ImporteAcumPeriodo = this.ImporteAcumPeriodo,
                    CantidadAcumAjustePeriodo = this.CantidadAcumAjustePeriodo,
                    ImporteAcumAjustePeriodo = this.ImporteAcumAjustePeriodo,
                    CantidadAcumActual = (this.CantidadAcumPeriodo + this.CantidadAcumAjustePeriodo),
                    ImporteAcumActual = (this.ImporteAcumPeriodo + this.ImporteAcumAjustePeriodo),
                    PorcentajeCumplido = this.PorcentajeCumplido,
                    PorcentajeCumplidoSobreImportePresupuestado = this.PorcentajeCumplidoSobreImportePresupuestado,
                    ImportePresupuestado = this.ImportePresupuestado,
                    NotificacionId = this.NotificacionId,
                    ExcedenteAutorizado = this.ExcedenteAutorizado,
                    CantidadAutorizadaExcedente = this.CantidadAutorizadaExcedente,
                    Visada = this.Visada,
                    lContratistasAvances = lContratistasAvances
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Certificado_Contratista: AsData(): exception.", ex);
            }
            return oData;
        }

    }
}
