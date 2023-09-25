using Gestion.EF.Datas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Gestion.EF.Models
{
    public class ProyCertificado
    {
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
        public int ProyCertAnteriorId { get; set; }
        public ProyCertificado ProyCertAnterior { get; set; }
        public DateTime FecDesde { get; set; }
        public DateTime FecHasta { get; set; }
        public string Estado { get; set; }
        public DateTime FecCreacion { get; set; }
        public int UsuarioCreoId { get; set; }

        public List<ProyCert_PlanProyAct> lProyCert_PlanProyActs { get; set; }
        public List<ProyCert_PDActContr> lProyCert_PDActContrs { get; set; }
        public List<ProyCert_PDActContr_Adicional> lProyCert_PDActContr_Adicionales { get; set; }
        public List<ProyCert_Contratista> lProyCert_Contratistas { get; set; }
        public List<ProyCert_Empleado> lProyCert_Empleados { get; set; }

        #region PROPIEDADES DERIVADAS

        public string FechaCreacion
        {
            get
            {
                return this.FecCreacion.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }
        public string FechaDesde
        {
            get
            {
                return this.FecDesde.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }
        public string FechaHasta
        {
            get
            {
                return this.FecHasta.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }
        public string Periodo
        {
            get
            {
                return this.FechaDesde + " - " + this.FechaHasta;
            }
        }
        public string ProyectoNombre
        {
            get
            {
                string nombre = "Sin Proyecto Asignado";
                if (this.Proyecto != null)
                    nombre = this.Proyecto.Nombre;
                return nombre;
            }
        }
        public string Detalle
        {
            get
            {
                return this.ProyectoNombre +
                    " - Periodo: " + this.FechaDesde + " - " + this.FechaHasta;
            }
        }
        public bool Cerrado
        {
            get
            {
                bool cerrado = false;
                if (this.lProyCert_PDActContrs != null)
                {
                    if (this.lProyCert_PDActContrs.Where(pcpdac => pcpdac.Estado == ValoresUtiles.Estado_PCPDAC_Abierto).Count() > 0)
                        cerrado = false;
                    else cerrado = true;
                }
                return cerrado;
            }
        }

        [NotMapped]
        public List<ItemProyCert_PlanProyAct> lPCPPAs
        {
            get
            {
                try
                {
                    List<ItemProyCert_PlanProyAct> lPCPPAs = new List<ItemProyCert_PlanProyAct>();
                    this.lProyCert_PlanProyActs.ForEach(pcppa =>
                    {
                        var a = this.lProyCert_PDActContrs.Find(p => p.PDActContrId == 42475);
                        List<ProyCert_PDActContr> lPCPDACs = this.lProyCert_PDActContrs
                            .Where(pcpdac =>
                                pcpdac.PDActContrId > 0 &&
                                pcpdac.PlanProyActId == pcppa.PlanProyActId)
                            .ToList();
                        lPCPDACs.AddRange(this.lProyCert_PDActContrs
                            .Where(pcpdac => pcpdac.PlanProyActId == pcppa.PlanProyActId &&
                                pcpdac.PDActContrId == 0)
                            .ToList());
                        pcppa.lPCPDACs = lPCPDACs;
                        ItemProyCert_PlanProyAct oDataPCPPA = pcppa.AsData();
                        lPCPPAs.Add(oDataPCPPA);
                    });
                    return lPCPPAs;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: ProyCertificado: lPCPPAs: exception.", ex);
                }
            }
        }
        [NotMapped]
        public List<Contratistas> lContratistas
        {
            get
            {
                List<Contratistas> lContratistas = new List<Contratistas>();
                try
                {
                    if (this.lProyCert_PDActContrs != null)
                    {
                        lContratistas = this.lProyCert_PDActContrs
                            .GroupBy(pcpdac => pcpdac.ContratistaId)
                            .Select(g => g.First().Contratista)
                            .ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: ProyCertificado: lContratistas: exception.", ex);
                }
                return lContratistas;
            }
        }
        [NotMapped]
        public int NumeroCertificado
        {
            get
            {
                try
                {
                    if (this.ProyCertAnteriorId == 0)
                        return 1;
                    else return 1 + this.ProyCertAnterior.NumeroCertificado;
                }
                catch(Exception ex)
                {
                    throw new Exception("Error: ProyCertificado: NumeroCertificado: exception.", ex);
                }
            }
        }

        public ItemProyCertificado AsData()
        {
            ItemProyCertificado oIProyCertificado = new ItemProyCertificado();
            try
            {
                int UsuarioCoordinadorProyecto = 0;
                if (this.Proyecto != null) UsuarioCoordinadorProyecto = this.Proyecto.UsuariosId;
                oIProyCertificado = new ItemProyCertificado
                {
                    Id = this.Id,
                    Detalle = this.Detalle,
                    ProyectoNombre = this.ProyectoNombre,
                    ProyectoId = this.ProyectoId,
                    UsuarioCoordinadorProyecto = UsuarioCoordinadorProyecto,
                    Cerrado = this.Cerrado,
                    FecDesde = this.FecDesde.ToString(ValoresUtiles.Formato_yyyy_MM_dd),
                    FecHasta = this.FecHasta.ToString(ValoresUtiles.Formato_yyyy_MM_dd),
                    FechaCreacion = this.FechaCreacion,
                    NumeroCertificado = this.NumeroCertificado,
                    lPCPPAs = this.lPCPPAs
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCertificado: AsData(): exception.", ex);
            }
            return oIProyCertificado;
        }

        public ItemProyCert_x_Contratista AsData_DetallePorContratistas()
        {
            ItemProyCert_x_Contratista oDProyCert = new ItemProyCert_x_Contratista();
            try
            {
                List<ItemContrCertificado> lContrCertificados = new List<ItemContrCertificado>();
                this.lContratistas.ForEach(c =>
                {
                    #region ACTIVIDADES (PLANIFICADAS, ADIC. PLAN y ADICIONALES)

                    List<ProyCert_PDActContr> lPCPDACs = this.lProyCert_PDActContrs
                        .Where(pcpdac => pcpdac.ContratistaId == c.Id)
                        .ToList();
                    List<int> lIdsPPAs = lPCPDACs
                        .GroupBy(pcpdac => pcpdac.PlanProyActId)
                        .Select(g => g.First().PlanProyActId)
                        .ToList();
                    List<ItemProyCert_PlanProyAct> lPCPPAs = new List<ItemProyCert_PlanProyAct>();
                    List<ItemProyCert_PlanProyAct> lPCPPA_APs = new List<ItemProyCert_PlanProyAct>();

                    float CantTotalAnterior = 0;
                    float CantTotalActual = 0;
                    float CantTotalAcumulada = 0;
                    float ImpTotalAnterior = 0;
                    float ImpTotalActual = 0;
                    float ImpTotalAcumulado = 0;

                    lIdsPPAs.ForEach(idppa =>
                    {
                        ProyCert_PlanProyAct oPCPPA = this.lProyCert_PlanProyActs
                            .Find(pcppa => pcppa.PlanProyActId == idppa);
                        if (oPCPPA.PlanProyActId > 0 && oPCPPA.PlanProyAct.EsAdicional == false)
                        {
                            oPCPPA.lPCPDACs = lPCPDACs
                                .Where(pcpdac => pcpdac.PlanProyActId == idppa && pcpdac.ContratistaId == c.Id)
                                .ToList();
                            lPCPPAs.Add(oPCPPA.AsData());
                        }
                        else if (oPCPPA.PlanProyAct.EsAdicional)
                        {
                            oPCPPA.lPCPDACs = lPCPDACs
                                .Where(pcpdac => pcpdac.PlanProyActId == idppa && pcpdac.ContratistaId == c.Id)
                                .ToList();
                            lPCPPA_APs.Add(oPCPPA.AsData());
                        }
                        float cantidad = oPCPPA.lPCPDACs.Sum(pcpdac => pcpdac.Cantidad);
                        CantTotalActual += cantidad;
                        CantTotalAcumulada += cantidad; //SUMAR ANTERIOR
                        ImpTotalActual += (cantidad * oPCPPA.MontoPlanificado);
                        ImpTotalAcumulado += (cantidad * oPCPPA.MontoPlanificado); //SUMAR ANTERIOR
                    });
                    this.lProyCert_PDActContr_Adicionales.ForEach(pcpdaca =>
                    {
                        if (pcpdaca.ContratistaId == c.Id)
                        {
                            CantTotalActual += pcpdaca.Cantidad;
                            CantTotalAcumulada += pcpdaca.Cantidad;//SUMAR ANTERIOR
                            ImpTotalActual += (pcpdaca.Cantidad * pcpdaca.Monto);
                            ImpTotalAcumulado += (pcpdaca.Cantidad * pcpdaca.Monto);//SUMAR ANTERIOR
                        }
                    });
                    ProyCert_Contratista oPCContratista = this.lProyCert_Contratistas
                        .Find(pcc => pcc.ContratistaId == c.Id);

                    #endregion

                    #region AJUSTE DE TOTALES y MONTOS POR EMPLEADO

                    int ProyCertContratistaId = 0;
                    bool EmiteFactura = false;
                    float PorcentajeIVA = 0;
                    float IndiceBase = 0;
                    float IndiceActual = 0;
                    float PorcentajeActualizacion = 0;
                    float Adelanto = 0;
                    float PorcentajeDescuentoAnticipo = 0;
                    float PorcentajeFondoReparo = 0;
                    if(oPCContratista != null)
                    {
                        ProyCertContratistaId = oPCContratista.Id;
                        EmiteFactura = oPCContratista.EmiteFactura;
                        PorcentajeIVA = oPCContratista.PorcentajeIVA;
                        IndiceBase = oPCContratista.IndiceBase;
                        IndiceActual = oPCContratista.IndiceActual;
                        PorcentajeActualizacion = oPCContratista.PorcentajeActualizacion;
                        Adelanto = oPCContratista.Adelanto;
                        PorcentajeDescuentoAnticipo = oPCContratista.PorcentajeDescuentoAnticipo;
                        PorcentajeFondoReparo = oPCContratista.PorcentajeFondoReparo;
                    }
                    else
                    {
                        PorcentajeFondoReparo = 5;
                    }
                    List<ProyCert_Empleado> lPCEs = this.lProyCert_Empleados
                        .Where(pce => pce.ContratistaId == c.Id).ToList();

                    #endregion

                    #region TOTALES POR PARTIDA

                    List<ItemPartidaPresupuestaria> lDataPartidas = new List<ItemPartidaPresupuestaria>();
                    List<PartidaPresupuestaria> lPartPresupuestarias = new List<PartidaPresupuestaria>();
                    List<Planificacion_Proyecto_Actividades> lPPAs = this.lProyCert_PlanProyActs
                        .GroupBy(pcppa => pcppa.PlanProyActId)
                        .Select(g => g.First().PlanProyAct)
                        .ToList();
                    lPartPresupuestarias = lPPAs
                        .GroupBy(ppa => ppa.Planificacion_Actividades.PartidaPresupuestariaId)
                        .Select(g => g.First().Planificacion_Actividades.PartidaPresupuestaria)
                        .ToList();
                    this.lProyCert_PDActContr_Adicionales.ForEach(pcpdaca =>
                    {
                        PartidaPresupuestaria oPP = lPartPresupuestarias
                            .Find(pp => pp.Codigo.ToUpper() == pcpdaca.CodigoPartidaPresupuestaria.ToUpper());
                        if(oPP == null)
                        {
                            lPartPresupuestarias.Add(new PartidaPresupuestaria()
                            {
                                Id = 0,
                                Codigo=pcpdaca.CodigoPartidaPresupuestaria,
                                Descripcion = pcpdaca.CodigoPartidaPresupuestaria + " - ADICIONAL"
                            });
                        }
                    });
                    lPartPresupuestarias.ForEach(pp =>
                    {
                        float importePartida = 0;
                        lPPAs.ForEach(ppa =>
                        {
                            float cantidad = 0;
                            if (ppa.Planificacion_Actividades.PartidaPresupuestariaId == pp.Id)
                            {
                                this.lProyCert_PDActContrs
                                    .Where(pcpdac => pcpdac.PlanProyActId == ppa.Id && pcpdac.ContratistaId == c.Id)
                                    .ToList()
                                    .ForEach(pcpdac =>
                                    {
                                        cantidad += pcpdac.Cantidad;
                                    });
                            }
                            ProyCert_PlanProyAct oPCPPA = this.lProyCert_PlanProyActs.Find(pcppa => pcppa.PlanProyActId == ppa.Id);
                            importePartida += (oPCPPA.MontoPlanificado * cantidad);
                        });
                        this.lProyCert_PDActContr_Adicionales
                            .Where(pcpdaca => pcpdaca.ContratistaId == c.Id &&
                                pcpdaca.CodigoPartidaPresupuestaria.ToUpper() == pp.Codigo.ToUpper())
                            .ToList()
                            .ForEach(pcpdaca =>
                            {
                                importePartida += (pcpdaca.Cantidad * pcpdaca.Monto);
                            });
                        ProyCert_Contratista oPCC = this.lProyCert_Contratistas.Find(pcc => pcc.ContratistaId == c.Id);
                        float porcentajeActualizacion = 0;
                        float porcentajeFondoReparo = 0;
                        if (oPCC != null) {
                            porcentajeActualizacion = oPCC.PorcentajeActualizacion;
                            porcentajeFondoReparo = oPCC.PorcentajeFondoReparo;
                        }
                        float importePartidaAjustado = importePartida + (porcentajeActualizacion * importePartida / 100);
                        float importeFondoReparo = porcentajeFondoReparo * importePartidaAjustado / 100;
                        lDataPartidas.Add(new ItemPartidaPresupuestaria()
                        {
                            Id = pp.Id,
                            Codigo = pp.Codigo,
                            Descripcion = pp.Descripcion,
                            ImportePartida = importePartida,
                            ImportePartidaAjustado = importePartidaAjustado,
                            ImporteFondoReparo = importeFondoReparo
                        });
                    });

                    #endregion

                    #region ENCAPSULADO DE LA DATA COMPLETA PARA EL CONTRATISTA X

                    lContrCertificados.Add(new ItemContrCertificado()
                    {
                        Id = ProyCertContratistaId,
                        EmiteFactura = EmiteFactura,
                        PorcentajeIVA = PorcentajeIVA,
                        IndiceBase = IndiceBase,
                        IndiceActual= IndiceActual,
                        PorcentajeActualizacion= PorcentajeActualizacion,
                        Adelanto = Adelanto,
                        PorcentajeDescuentoAnticipo= PorcentajeDescuentoAnticipo,
                        PorcentajeFondoReparo= PorcentajeFondoReparo,
                        lDataPartidas= lDataPartidas,
                        lProyCert_Empleados = lPCEs
                            .Select(pce => pce.AsData()).ToList(),
                        ContratistaId = c.Id,
                        Nombre = c.Nombre,
                        lPCPPAs = lPCPPAs,
                        lPCPPA_APs = lPCPPA_APs,
                        lPCPDAC_As = this.lProyCert_PDActContr_Adicionales
                            .Where(pcpdaca => pcpdaca.ContratistaId==c.Id)
                            .Select(pcpdac => pcpdac.AsData())
                            .ToList(),
                        CantTotalAnterior=CantTotalAnterior,
                        CantTotalActual=CantTotalActual,
                        CantTotalAcumulada= CantTotalAcumulada,
                        ImpTotalAnterior= ImpTotalAnterior,
                        ImpTotalActual= ImpTotalActual,
                        ImpTotalAcumulado= ImpTotalAcumulado,
                        Abierto = this.lProyCert_PDActContrs
                            .Where(pcpdac => pcpdac.ContratistaId == c.Id &&
                                pcpdac.Estado == ValoresUtiles.Estado_PCPDAC_Abierto)
                            .Count() > 0,
                    });

                    #endregion
                });
                oDProyCert = new ItemProyCert_x_Contratista()
                {
                    Id = this.Id,
                    Detalle = this.Detalle,
                    ProyectoNombre = this.ProyectoNombre,
                    ProyectoId = this.ProyectoId,
                    FecDesde = this.FecDesde.ToString(ValoresUtiles.Formato_dd_MM_yyyy),
                    FecHasta = this.FecHasta.ToString(ValoresUtiles.Formato_dd_MM_yyyy),
                    FechaCreacion = this.FechaCreacion,
                    lContrCertificados = lContrCertificados,
                    NumeroCertificado = this.NumeroCertificado,
                    EsCertificadoCero = this.ProyCertAnteriorId == 0
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Certificado_Contratista: AsData_DetallePorContratistas(): exception.", ex);
            }
            return oDProyCert;
        }

        #endregion

    }
}
