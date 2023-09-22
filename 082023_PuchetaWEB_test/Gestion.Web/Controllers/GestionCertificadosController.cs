using Gestion.EF;
using Gestion.EF.Datas;
using Gestion.EF.Items.ProyCertificados;
using Gestion.EF.Models;
using Gestion.Negocio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
//using SelectPdf;

namespace Gestion.Web.Controllers
{
    public class GestionCertificadosController : Controller
    {

        #region INICIALIZACIÓN

        private readonly IWebHostEnvironment _env;
        public GestionCertificadosController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public ActionResult index()
        {
            var oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    if (oUsuarioLogueado.Id == 1)
                    {
                        //this.CocinarNumerosCertificados();
                        //this.CocinarPCPDACs_CorregirPDAId();
                    }
                    DateTime fecDesde = DateTime.Now.AddMonths(-6);
                    DateTime fecHasta = DateTime.Now;

                    List<ProyCertificado> lProyCertificados = new ProyCertificadoNegocio()
                        .Get_x_Periodo(fecDesde, fecHasta, true);

                    List<Proyecto> lProyectos = new List<Proyecto>();
                    lProyectos.Add(new Proyecto() { Nombre = ValoresUtiles.OptTodos });
                    lProyectos.AddRange(new ProyectoNegocio()
                        .Get_All_Proyecto()
                        .OrderBy(p => p.Nombre)
                        .ToList());

                    List<Contratistas> lContratistas = new List<Contratistas>();
                    lContratistas.Add(new Contratistas() { Nombre = ValoresUtiles.OptTodos });
                    lContratistas.AddRange(new ContratistasNegocio()
                        .Get_All_Contratistas()
                        .OrderBy(c => c.Nombre)
                        .ToList());

                    List<Usuarios> lUsuarios = new RolesUsuariosNegocio()
                        .Get_Usuarios_x_RolId(ValoresUtiles.IdRol_CoordinacionObra);
                    bool EsCoordinadorObra = false;
                    if (lUsuarios.Count > 0)
                        EsCoordinadorObra = lUsuarios.Find(u => u.Id == oUsuarioLogueado.Id) != null;
                    lUsuarios = new RolesUsuariosNegocio()
                        .Get_Usuarios_x_RolId(ValoresUtiles.IdRol_Administracion);
                    bool EsAdministracion = false;
                    if (lUsuarios.Count > 0)
                        EsAdministracion = lUsuarios.Find(u => u.Id == oUsuarioLogueado.Id) != null;
                    EsAdministracion = EsAdministracion ||
                        oUsuarioLogueado.NUsuario == ValoresUtiles.NombreUsuario_Personal ||
                        oUsuarioLogueado.Email == ValoresUtiles.NombreUsuario_Personal;

                    oResult = new
                    {
                        fecDesde = fecDesde.ToString(ValoresUtiles.Formato_yyyy_MM_dd),
                        fecHasta = fecHasta.ToString(ValoresUtiles.Formato_yyyy_MM_dd),
                        lProyectos = lProyectos
                            .Select(p => new { p.Id, p.Nombre }).ToList(),
                        lContratistas = lContratistas
                            .Select(c => new { c.Id, c.Nombre }).ToList(),
                        lProyCertificados = lProyCertificados
                            .Select(cc => new
                            {
                                cc.Id,
                                cc.FechaCreacion,
                                cc.Periodo,
                                cc.ProyectoNombre,
                                EsCertificadoCero = cc.ProyCertAnteriorId == 0
                            })
                            .ToList(),
                        EsCoordinadorObra,
                        EsAdministracion,
                        UsuarioId = oUsuarioLogueado.Id
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return View(oResult);
        }

        #endregion

        #region GETS

        [HttpGet]
        public object GetDatosVistaPreviaCertificado(
            int ProyectoId, string FechaDesde, string FechaHasta)
        {
            object oResult;
            try
            {
                DateTime FecDesde = DateTime.Parse(FechaDesde);
                DateTime FecHasta = DateTime.Parse(FechaHasta);
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    ProyCertificado oProyCertAnterior = new ProyCertificadoNegocio()
                        .Get_ProyCertificadoUltimo_x_ProyectoId(ProyectoId, true);
                    DateTime FecHastaAnterior = DateTime.MinValue;
                    if (oProyCertAnterior != null)
                        FecHastaAnterior = oProyCertAnterior.FecHasta;
                    if (FecDesde < FecHasta)
                    {
                        FecDesde = new DateTime(FecDesde.Year, FecDesde.Month, FecDesde.Day);
                        FecHasta = new DateTime(FecHasta.Year, FecHasta.Month, FecHasta.Day)
                            .AddDays(1).AddTicks(-1);
                        if (oProyCertAnterior == null || FecHastaAnterior.AddDays(1) == FecDesde)
                        {
                            ProyCertificado oProyCertificado = this.GetDatosParaCertificar(ProyectoId, FecDesde, FecHasta);
                            oProyCertificado.ProyectoId = ProyectoId;
                            oProyCertificado.FecDesde = FecDesde;
                            oProyCertificado.FecHasta = FecHasta;
                            oResult = new
                            {
                                oProyCertificado = this.GetItemProyCert_ConAcumuladoCalculado(oProyCertificado)
                            };
                        }
                        else
                        {
                            oResult = new
                            {
                                isError = true,
                                mensaje = "La fecha 'Desde' debe ser 1 día mayor que la fecha 'Hasta' del último certificado."
                            };
                        }
                    }
                    else
                    {
                        oResult = new
                        {
                            isError = true,
                            mensaje = "La fecha Desde debe ser menor a la fecha Hasta."
                        };
                    }
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpGet]
        public object GetProyCertificados(int ProyectoId, string FechaDesde, string FechaHasta)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    DateTime FecDesde = DateTime.Parse(FechaDesde);
                    FecDesde = FecDesde.Date;
                    DateTime FecHasta = DateTime.Parse(FechaHasta);
                    FecHasta = FecHasta.Date;
                    List<ProyCertificado> lProyCertificados = new ProyCertificadoNegocio()
                        .Get_x_ProyId_Periodo(ProyectoId, FecDesde, FecHasta, true)
                        .OrderBy(pc => pc.FecDesde)
                        .ToList();
                    oResult = new
                    {
                        lProyCertificados = lProyCertificados
                            .Select(pc => new
                            {
                                pc.Id,
                                pc.FechaCreacion,
                                pc.Periodo,
                                pc.ProyectoNombre,
                                pc.Cerrado,
                                EsCertificadoCero = pc.ProyCertAnteriorId == 0
                            })
                            .ToList()
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpGet]
        public object GetProyCertificado(int ProyCertificadoId)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    ProyCertificado oProyCertificado = new ProyCertificadoNegocio()
                        .Get_x_Id_SQL(ProyCertificadoId);
                    ItemProyCertificado oIProyCertificado = new ItemProyCertificado();
                    if (oProyCertificado.ProyCertAnteriorId > 0)
                        oIProyCertificado = this.GetItemProyCert_ConAcumuladoCalculado(oProyCertificado);
                    else oIProyCertificado = oProyCertificado.AsData();
                    oResult = new
                    {
                        oProyCertificado = oIProyCertificado
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpGet]
        public object GetProyCertificado_DetallePorContratistas(int ProyCertificadoId)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    ItemProyCert_x_Contratista oIProyCertContr = this.GetItemProyCertContratista(ProyCertificadoId);
                    List<PartidaPresupuestaria> lPartidasPresupuestarias = new PartidaPresupuestariaNegocio()
                        .GetAll()
                        .OrderBy(pp => pp.Codigo)
                        .ToList();
                    oResult = new
                    {
                        oProyCertificado = oIProyCertContr,
                        lPartidasPresupuestarias = lPartidasPresupuestarias
                            .Select(pp => new
                            {
                                pp.Codigo,
                                pp.Descripcion,
                                CodigoDescripcion = pp.Codigo + " - " + pp.Descripcion
                            })
                            .ToList()
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }
        [HttpGet]
        public object GetContratistas_x_Proyecto(int ProyectoId)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    if (ProyectoId > 0)
                    {
                        List<Contratistas> lContratistas = new ProyCert_ContratistaNegocio()
                            .GetContratistas_x_ProyectoId_CertCerrados(ProyectoId);
                        oResult = new
                        {
                            lContratistas = lContratistas.Select(c => new
                            {
                                c.Id,
                                c.Nombre
                            }).ToList()
                        };
                    }
                    else
                    {
                        oResult = new
                        {
                            isError = true,
                            mensaje = "Dato de Proyecto incorrecto.",
                        };
                    }
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #endregion

        #region POST
        public partial class DataPCPPAVisada
        {
            public int PlanProyActId { get; set; }
            public bool Visada { get; set; }
        }
        public partial class DataPCPPAAjuste
        {
            public int PlanProyActId { get; set; }
            public int ContratistaId { get; set; }
            public float Ajuste { get; set; }
        }
        public partial class DataPCPPAExcedente
        {
            public int PlanProyActId { get; set; }
            public bool ExcedenteAutorizado { get; set; }
            public float CantidadAutorizadaExcedente { get; set; }
        }
        public partial class DataPPAMonto
        {
            public int PlanProyActId { get; set; }
            public float Monto { get; set; }
        }
        public partial class DataProyCert
        {
            public int ProyCertificadoId { get; set; }
            public int ProyectoId { get; set; }
            public string FechaDesde { get; set; }
            public string FechaHasta { get; set; }
            public List<DataPCPPAVisada> lVisadas { get; set; }
            public List<DataPCPPAAjuste> lAjustes { get; set; }
            public List<DataPCPPAExcedente> lExcedentes { get; set; }
            public List<DataPPAMonto> lPPAMontos { get; set; }

            public DateTime FecDesde
            {
                get
                {
                    return DateTime.Parse(FechaDesde);
                }
            }
            public DateTime FecHasta
            {
                get
                {
                    return DateTime.Parse(FechaHasta);
                }
            }
        }
        [HttpPost]
        public object GrabarProyCertificado([FromBody] DataProyCert oDPC)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    int ProyCertificadoId = 0;
                    if (oDPC.ProyCertificadoId == 0)
                        ProyCertificadoId = this.GrabarNuevoProyCertificado(oUsuarioLogueado.Id, oDPC);
                    else
                        ProyCertificadoId = this.ActualizarAjustesProyCertificado(oUsuarioLogueado.Id, oDPC);
                    ProyCertificado oProyCertificado = new ProyCertificadoNegocio()
                        .Get_x_Id_SQL(ProyCertificadoId);
                    oResult = new
                    {
                        oProyCertificado = this.GetItemProyCert_ConAcumuladoCalculado(oProyCertificado)
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object GrabarAdicionales([FromBody] List<ProyCert_PDActContr_Adicional> lPCPDAC_As)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    int ProyCertificadoId = lPCPDAC_As.First().ProyCertificadoId;
                    int ContratistaId = lPCPDAC_As.First().ContratistaId;
                    List<ProyCert_PDActContr_Adicional> lPCPDAC_A_Originales = new ProyCert_PDActContr_AdicionalNegocio()
                        .Get_x_ProyCertId(ProyCertificadoId);
                    lPCPDAC_A_Originales = lPCPDAC_A_Originales
                        .Where(pcpdaca => pcpdaca.ContratistaId == ContratistaId)
                        .ToList();
                    lPCPDAC_As.ForEach(pcpdaca =>
                    {
                        if (pcpdaca.Id == 0)
                        {
                            pcpdaca.FecCreacion = DateTime.Now;
                            new ProyCert_PDActContr_AdicionalNegocio().Insert(pcpdaca);
                        }
                        else
                            new ProyCert_PDActContr_AdicionalNegocio().Update(pcpdaca);
                    });
                    lPCPDAC_A_Originales.ForEach(pcpdacao =>
                    {
                        if (lPCPDAC_As.Find(pcpdaca => pcpdaca.Id == pcpdacao.Id) == null)
                        {
                            new ProyCert_PDActContr_AdicionalNegocio().Delete(pcpdacao);
                        }
                    });
                    oResult = new
                    {
                        oProyCertificado = this.GetProyCert_AsData_x_Contratistas(ProyCertificadoId)
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object GrabarDescuentos([FromBody] List<ProyContr_Descuento> lProyContr_Descuentos)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    int ProyectoId = lProyContr_Descuentos.First().ProyectoId;
                    int ContratistaId = lProyContr_Descuentos.First().ContratistaId;
                    List<ProyContr_Descuento> lPCPDAC_A_Originales = new ProyContr_DescuentoNegocio()
                        .Get_x_ProyectoId_ContratistaId(ProyectoId, ContratistaId);
                    lProyContr_Descuentos.ForEach(pcd =>
                    {
                        if (pcd.Id == 0)
                        {
                            new ProyContr_DescuentoNegocio().Insert(pcd);
                        }
                        else new ProyContr_DescuentoNegocio().Update(pcd);
                    });
                    lPCPDAC_A_Originales.ForEach(pcd_o =>
                    {
                        if (lProyContr_Descuentos.Find(pcd => pcd.Id == pcd_o.Id) == null)
                        {
                            new ProyContr_DescuentoNegocio().Delete(pcd_o);
                        }
                    });
                    List<int> lIdsProyectos = new List<int> { ProyectoId };
                    List<int> lIdsContratistas = new List<int> { ContratistaId };
                    List<ProyCert_Contratista> lPC_Contratistas = this.GetProyCertContratistas(new DataFiltroCertContratistas()
                    {
                        FechaDesde = string.Empty,
                        FechaHasta = string.Empty,
                        lIdsProyectos = lIdsProyectos,
                        lIdsContratistas = lIdsContratistas
                    });
                    List<object> lContrCertificados = this.GetContrCertificados(lPC_Contratistas, true);
                    lProyContr_Descuentos = new ProyContr_DescuentoNegocio()
                        .Get_x_ProyectoId_ContratistaId(ProyectoId, ContratistaId);
                    oResult = new
                    {
                        lContrCertificados,
                        lProyContr_Descuentos = lProyContr_Descuentos
                            .Select(pcd => pcd.AsData())
                            .ToList()
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object EliminarDescuentos([FromBody] ProyContr_Descuento oProyConrt_Descuento)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<ProyContr_Descuento> lProyContr_Descuentos = new ProyContr_DescuentoNegocio()
                        .Get_x_ProyectoId_ContratistaId(oProyConrt_Descuento.ProyectoId, oProyConrt_Descuento.ContratistaId);
                    lProyContr_Descuentos.ForEach(pcpdacao =>
                    {
                        new ProyContr_DescuentoNegocio().Delete(pcpdacao);
                    });
                    List<int> lIdsProyectos = new List<int> { oProyConrt_Descuento.ProyectoId };
                    List<int> lIdsContratistas = new List<int> { oProyConrt_Descuento.ContratistaId };
                    List<ProyCert_Contratista> lPC_Contratistas = this.GetProyCertContratistas(new DataFiltroCertContratistas()
                    {
                        FechaDesde = string.Empty,
                        FechaHasta = string.Empty,
                        lIdsProyectos = lIdsProyectos,
                        lIdsContratistas = lIdsContratistas
                    });
                    List<object> lContrCertificados = this.GetContrCertificados(lPC_Contratistas, true);
                    lProyContr_Descuentos = new ProyContr_DescuentoNegocio()
                        .Get_x_ProyectoId_ContratistaId(oProyConrt_Descuento.ProyectoId, oProyConrt_Descuento.ContratistaId);
                    oResult = new
                    {
                        lContrCertificados,
                        lProyContr_Descuentos = lProyContr_Descuentos
                            .Select(pcd => pcd.AsData())
                            .ToList()
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        public partial class DataMontosAjustables
        {
            public int ProyCertificadoId { get; set; }
            public int ContratistaId { get; set; }
            public int PlanProyActId { get; set; }
            public bool MontosAjustables { get; set; }
        }
        [HttpPost]
        public object GrabarAdicionalesPlanificadas([FromBody] List<DataMontosAjustables> lDMontosAjustables)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    int ProyCertificadoId = 0;
                    lDMontosAjustables.ForEach(dma =>
                    {
                        if (ProyCertificadoId == 0) ProyCertificadoId = dma.ProyCertificadoId;
                        List<ProyCert_PDActContr> lPCPDAC = new ProyCert_PDActContrNegocio()
                            .Get_x_ProyCertificadoId_ContratistaId_PlanProyActId(
                                dma.ProyCertificadoId, dma.ContratistaId, dma.PlanProyActId);
                        lPCPDAC.ForEach(pcpdac =>
                        {
                            pcpdac.MontosAjustables = dma.MontosAjustables;
                            new ProyCert_PDActContrNegocio().Update(pcpdac);
                        });
                    });
                    if (ProyCertificadoId > 0)
                    {
                        oResult = new
                        {
                            oProyCertificado = this.GetProyCert_AsData_x_Contratistas(ProyCertificadoId)
                        };
                    }
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object EliminarAdicionales([FromBody] ProyCert_PDActContr_Adicional oPCPDACA_Filtro)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<ProyCert_PDActContr_Adicional> lPCPDAC_A_Originales = new ProyCert_PDActContr_AdicionalNegocio()
                        .Get_x_ProyCertId(oPCPDACA_Filtro.ProyCertificadoId);
                    lPCPDAC_A_Originales = lPCPDAC_A_Originales
                        .Where(pcpdaca => pcpdaca.ContratistaId == oPCPDACA_Filtro.ContratistaId)
                        .ToList();
                    lPCPDAC_A_Originales.ForEach(pcpdacao =>
                    {
                        new ProyCert_PDActContr_AdicionalNegocio().Delete(pcpdacao);
                    });
                    oResult = new
                    {
                        oProyCertificado = this.GetProyCert_AsData_x_Contratistas(oPCPDACA_Filtro.ProyCertificadoId)
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object CerrarContrCertificado([FromBody] ProyCert_PDActContr oPCPDAC)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    ProyCertificado oProyCertificado = new ProyCertificadoNegocio()
                        .Get_x_Id_SQL(oPCPDAC.ProyCertificadoId);
                    List<ProyCert_Contratista> lPCContratistas = new ProyCert_ContratistaNegocio()
                        .Get_x_ProyCertificadoId(oPCPDAC.ProyCertificadoId);
                    ProyCert_Contratista oPCC = lPCContratistas.Find(pcc => pcc.ContratistaId == oPCPDAC.ContratistaId);
                    int numCertificado = new ProyCert_ContratistaNegocio()
                        .GetNuevoNumCertificado(oProyCertificado.ProyectoId, oPCPDAC.ContratistaId);
                    if (oPCC == null)
                    {
                        oPCC = new ProyCert_Contratista
                        {
                            ProyCertificadoId = oPCPDAC.ProyCertificadoId,
                            ContratistaId = oPCPDAC.ContratistaId,
                            NumeroCertificado = numCertificado
                        };
                        new ProyCert_ContratistaNegocio().Insert(oPCC);
                    }
                    else
                    {
                        oPCC.NumeroCertificado = numCertificado;
                        new ProyCert_ContratistaNegocio().Update(oPCC);
                    }

                    List<ProyCert_PDActContr> lPCPDACs = oProyCertificado.lProyCert_PDActContrs
                        .Where(pcpdac => pcpdac.ContratistaId == oPCPDAC.ContratistaId)
                        .ToList();
                    lPCPDACs.ForEach(pcpdac =>
                    {
                        if (pcpdac.PDActContrId == 0)
                        {
                            List<ParteDiario_Actividades> lPDAs = new ParteDiario_ActividadesNegocio()
                                .Get_X_IdPlanProyActividad(pcpdac.PlanProyActId)
                                .OrderByDescending(pda => pda.ParteDiario.Fecha_Creacion)
                                .ToList();
                            int idPDA = 0;
                            if (lPDAs.Count > 0) idPDA = lPDAs.First().Id;
                            if (idPDA > 0)
                            {
                                ParteDiario_Actividades_Contratista oPDAC = new ParteDiario_Actividades_Contratista();
                                oPDAC.ParteDiario_ActividadesId = idPDA;
                                oPDAC.ContratistasId = pcpdac.ContratistaId;
                                oPDAC.Cantidad = pcpdac.Cantidad;
                                oPDAC.TipoRegistro = ValoresUtiles.TipoRegistro_PDAC_Automatico;
                                oPDAC.Fecha = DateTime.Now;
                                pcpdac.PDActContrId = new ParteDiario_Actividades_ContratistaNegocio().Insert(oPDAC);
                            }
                        }
                        pcpdac.Estado = ValoresUtiles.Estado_IncHist_Cerrado;
                        new ProyCert_PDActContrNegocio().Update(pcpdac);
                    });
                    oResult = new
                    {
                        oProyCertificado = this.GetProyCert_AsData_x_Contratistas(oProyCertificado.Id)
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object GrabarContrCertificado([FromBody] ProyCert_Contratista oProyCert_Contratista)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    if (oProyCert_Contratista.Id > 0)
                    {
                        ProyCert_Contratista oPCC = new ProyCert_ContratistaNegocio()
                            .Get_x_Id(oProyCert_Contratista.Id, false);
                        oProyCert_Contratista.NumeroCertificado = oPCC.NumeroCertificado;
                        new ProyCert_ContratistaNegocio().Update(oProyCert_Contratista);
                    }
                    else
                    {
                        new ProyCert_ContratistaNegocio().Insert(oProyCert_Contratista);
                    }
                    oResult = new
                    {
                        oProyCertificado = this.GetProyCert_AsData_x_Contratistas(oProyCert_Contratista.ProyCertificadoId)
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        public partial class oDataProyCertEmpleados
        {
            public int ProyCertificadoId { get; set; }
            public int ContratistaId { get; set; }
            public List<ProyCert_Empleado> lProyCert_Empleados { get; set; }
        }
        [HttpPost]
        public object GrabarProyCertEmpleados([FromBody] oDataProyCertEmpleados oDPCEmpleados)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<ProyCert_Empleado> lProyCertEmpleados_Originales = new ProyCert_EmpleadoNegocio()
                        .Get_x_ProyCertificadoId(oDPCEmpleados.ProyCertificadoId, oDPCEmpleados.ContratistaId);

                    if (oDPCEmpleados.lProyCert_Empleados.Count == 0)
                    {
                        lProyCertEmpleados_Originales.ForEach(pce =>
                            new ProyCert_EmpleadoNegocio().Delete(pce));
                    }
                    else
                    {
                        oDPCEmpleados.lProyCert_Empleados.ForEach(pce =>
                        {
                            if (pce.Id > 0)
                            {
                                new ProyCert_EmpleadoNegocio().Update(pce);
                            }
                            else
                            {
                                new ProyCert_EmpleadoNegocio().Insert(pce);
                            }
                        });
                        lProyCertEmpleados_Originales.ForEach(pce =>
                        {
                            ProyCert_Empleado oPCE = oDPCEmpleados.lProyCert_Empleados.Find(dpce => dpce.Id == pce.Id);
                            if (oPCE == null)
                            {
                                new ProyCert_EmpleadoNegocio().Delete(pce);
                            }
                        });
                    }
                    oResult = new
                    {
                        oProyCertificado = this.GetProyCert_AsData_x_Contratistas(oDPCEmpleados.ProyCertificadoId)
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object GetNombreCertificadoPDF([FromBody] ProyCert_Contratista oProyCertContratista)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    oResult = new
                    {
                        nombreCertificadoPDF = this.CalcularNombrePDFCertificado(
                            oProyCertContratista.ProyCertificadoId, oProyCertContratista.ContratistaId)
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        public partial class oDataPDF
        {
            public string StringPDF { get; set; }
            public int ProyCertificadoId { get; set; }
            public int ContratistaId { get; set; }
        }
        [HttpPost]
        public object EnviarPDF(oDataPDF oDataPDF)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    ItemProyCert_x_Contratista oDPC = this.GetProyCert_AsData_x_Contratistas(oDataPDF.ProyCertificadoId);
                    ItemContrCertificado oDCC = oDPC.lContrCertificados.Find(cc => cc.ContratistaId == oDataPDF.ContratistaId);
                    Contratistas oContratista = new ContratistasNegocio().Get_One_Contratistas(oDCC.ContratistaId);
                    string nombreArchivo = this.CalcularNombrePDFCertificado(oDataPDF.ProyCertificadoId, oContratista.Id);
                    string nombreCaperta = Path.Combine(_env.WebRootPath, "archivos/certificados/obras/");

                    var fileContents = Convert.FromBase64String(oDataPDF.StringPDF);
                    System.IO.File.WriteAllBytes(nombreCaperta + nombreArchivo, fileContents);
                    ProyCertificado oProyCert = new ProyCertificadoNegocio()
                        .Get_x_Id(oDataPDF.ProyCertificadoId, false);
                    oProyCert.Proyecto = new ProyectoNegocio()
                        .Get_One_Proyecto(oProyCert.ProyectoId);
                    return this.EnviarMail(oProyCert, oDCC, nombreCaperta + nombreArchivo);
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        public partial class oDataImporteAcumAnterior
        {
            public int ProyCertificadoId { get; set; }
            public int PlanProyActId { get; set; }
            public float ImporteAcumAnterior { get; set; }
            public int ContratistaId { get; set; }
        }
        [HttpPost]
        public object GrabarImportesAnteriores([FromBody] List<oDataImporteAcumAnterior> lDImpAcumAnts)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    if (lDImpAcumAnts != null && lDImpAcumAnts.Count > 0)
                    {
                        ProyCert_PDActContrNegocio oPCPDACNegocio = new ProyCert_PDActContrNegocio();
                        lDImpAcumAnts.ForEach(diaa =>
                        {
                            List<ProyCert_PDActContr> lPCPDACs = oPCPDACNegocio
                                .Get_x_ProyCertificadoId_ContratistaId_PlanProyActId(
                                diaa.ProyCertificadoId, diaa.ContratistaId, diaa.PlanProyActId)
                                .OrderBy(pcpdac => pcpdac.Id)
                                .ToList();
                            if (lPCPDACs.Count > 0)
                            {
                                ProyCert_PDActContr oPCPDAC = lPCPDACs.First();
                                oPCPDAC.ImporteAcumAnterior = diaa.ImporteAcumAnterior;
                                oPCPDACNegocio.Update(oPCPDAC);
                            }
                        });
                        ProyCertificado oProyCertificado = new ProyCertificadoNegocio()
                            .Get_x_Id_SQL(lDImpAcumAnts.First().ProyCertificadoId);
                        oResult = new
                        {
                            oProyCertificado = oProyCertificado.AsData_DetallePorContratistas()
                        };
                    }
                    else
                    {
                        oResult = new
                        {
                            isError = true,
                            mensaje = "No hay datos para registrar.",
                        };
                    }
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        public partial class DataFiltroCertContratistas
        {
            public string FechaDesde { get; set; }
            public string FechaHasta { get; set; }
            public List<int> lIdsProyectos { get; set; }
            public List<int> lIdsContratistas { get; set; }
        }
        [HttpPost]
        public object GetContratistasCertificados([FromBody] DataFiltroCertContratistas oDFCC)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    DateTime FecDesde = DateTime.Parse(oDFCC.FechaDesde);
                    FecDesde = FecDesde.Date;
                    DateTime FecHasta = DateTime.Parse(oDFCC.FechaHasta);
                    FecHasta = FecHasta.Date;

                    List<ProyCert_Contratista> lPC_Contratistas = this.GetProyCertContratistas(oDFCC);
                    lPC_Contratistas = lPC_Contratistas.OrderBy(pcc => pcc.NumeroCertificado).ToList();
                    List<object> lContrCertificados = this.GetContrCertificados(lPC_Contratistas, false);
                    oResult = new
                    {
                        lContrCertificados
                    };

                    #region CODIGO SUSPENDIDO
                    //List<ProyCertificado> lProyCertificados = new ProyCertificadoNegocio()
                    //    .Get_x_ProyId_Periodo(0, FecDesde, FecHasta, true);
                    //lProyCertificados = lProyCertificados
                    //    .OrderByDescending(pc => pc.FecDesde)
                    //    .ToList();
                    //if (oDFCC.lIdsProyectos.Count > 0)
                    //{
                    //    lProyCertificados = lProyCertificados.Where(pc =>
                    //        oDFCC.lIdsProyectos.Contains(pc.ProyectoId))
                    //        .ToList();
                    //}
                    //List<ItemProyCert_Contratista> lIProyCertContratistas = new List<ItemProyCert_Contratista>();
                    //lProyCertificados.ForEach(pc =>
                    //{
                    //    lIProyCertContratistas.Add(this.GetItemProyCertContratista(pc.Id));
                    //});
                    //List<object> lContrCertificados = new List<object>();
                    //lIProyCertContratistas.ForEach(ipcc =>
                    //{
                    //    if (oDFCC.lIdsContratistas.Count > 0)
                    //    {
                    //        List<ItemContrCertificado> lIContrCertificados_Aux = ipcc.lContrCertificados
                    //            .Where(cc => oDFCC.lIdsContratistas.Contains(cc.ContratistaId) &&
                    //                cc.Abierto == false)
                    //            .ToList();
                    //        lContrCertificados.AddRange(lIContrCertificados_Aux
                    //            .Where(icc => icc.Id > 0)
                    //            .Select(icc => new
                    //            {
                    //                icc.Id,
                    //                ProyCertificadoId = ipcc.Id,
                    //                ipcc.ProyectoId,
                    //                ipcc.ProyectoNombre,
                    //                icc.ContratistaId,
                    //                ContratistaNombre = icc.Nombre,
                    //                icc.NumeroCertificado,
                    //                ipcc.FechaCreacion,
                    //                ipcc.Periodo
                    //            }));
                    //    }
                    //    else
                    //    {
                    //        lContrCertificados.AddRange(ipcc.lContrCertificados
                    //            .Where(icc => icc.Id > 0)
                    //            .Select(icc => new
                    //            {
                    //                icc.Id,
                    //                ProyCertificadoId = ipcc.Id,
                    //                ipcc.Detalle,
                    //                ipcc.ProyectoId,
                    //                ipcc.ProyectoNombre,
                    //                icc.ContratistaId,
                    //                ContratistaNombre = icc.Nombre,
                    //                icc.NumeroCertificado,
                    //                ipcc.FechaCreacion,
                    //                ipcc.Periodo
                    //            }));
                    //    }
                    ////});
                    //oResult = new
                    //{
                    //    lContrCertificados
                    //};
                    #endregion

                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object GenerarCertificadosCero()
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    if (oUsuarioLogueado.Id == 1)
                    {
                        this.GrabarProyCertificadosCero();
                    }
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #endregion

        #region PRIVADOS

        private ProyCertificado GetDatosParaCertificar(int ProyectoId, DateTime FecDesde, DateTime FecHasta)
        {
            ProyCertificado oProyCertificado = new ProyCertificado();
            try
            {
                oProyCertificado.ProyCertAnterior = new ProyCertificadoNegocio()
                    .Get_ProyCertificadoUltimo_x_ProyectoId(ProyectoId, true);
                if (oProyCertificado.ProyCertAnterior != null)
                    oProyCertificado.ProyCertAnteriorId = oProyCertificado.ProyCertAnterior.Id;
                oProyCertificado.Proyecto = new ProyectoNegocio().Get_One_Proyecto(ProyectoId);

                //REDEFINIMOS LAS FECHAS PARA QUE INVOLUCREN EL PRIMER TICK DE LA FECHA DESDE
                //Y EL ULTIMO DE LA FECHA HASTA
                FecDesde = new DateTime(FecDesde.Year, FecDesde.Month, FecDesde.Day);
                FecHasta = new DateTime(FecHasta.Year, FecHasta.Month, FecHasta.Day);
                FecHasta = FecHasta.AddDays(1).AddTicks(-1);

                oProyCertificado.FecDesde = FecDesde;
                oProyCertificado.FecHasta = FecHasta;

                List<ParteDiario_Actividades_Contratista> lPDACs =
                    new ParteDiario_Actividades_ContratistaNegocio()
                    .Get_x_ProyectoId_FecDesde_FecHasta(ProyectoId, FecDesde, FecHasta, true)
                    .Where(pdac => pdac.ContratistasId != ValoresUtiles.IdContratista_Pucheta &&
                        pdac.TipoRegistro == ValoresUtiles.TipoRegistro_PDAC_Manual)
                    .ToList();
                List<Planificacion_Proyecto_Actividades> lPPAs = lPDACs
                    .GroupBy(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_ActividadesId)
                    .Select(g => g.First().ParteDiario_Actividades.Planificacion_Proyecto_Actividades)
                    .ToList();
                oProyCertificado.lProyCert_PlanProyActs = new List<ProyCert_PlanProyAct>();
                oProyCertificado.lProyCert_PDActContrs = new List<ProyCert_PDActContr>();

                lPPAs.ForEach(ppa =>
                {
                    ProyCert_PlanProyAct oPCPPA = new ProyCert_PlanProyAct();
                    oPCPPA.PlanProyActId = ppa.Id;
                    oPCPPA.PlanProyAct = ppa;
                    oPCPPA.MontoPlanificado = ppa.Monto;
                    oPCPPA.CantidadPlanificada = ppa.Cantidad;
                    oPCPPA.UnidadMedida = ppa.UnidadMedida;
                    oPCPPA.PartidaPresupuestariaId = ppa.Planificacion_Actividades.PartidaPresupuestariaId;
                    oPCPPA.CodigoPartidaPresupuestaria = ppa.Planificacion_Actividades.CodigoPartidaPresupuestaria;
                    oPCPPA.DescripcionPartidaPresupuestaria = ppa.Planificacion_Actividades.DescripcionPartidaPresupuestaria;
                    oPCPPA.Actividad = ppa.Actividad;
                    if (string.IsNullOrEmpty(ppa.Detalle))
                        oPCPPA.ActividadDescripcion = "Sin Descripción";
                    else oPCPPA.ActividadDescripcion = ppa.Detalle;
                    oPCPPA.Ubicacion = ppa.Ubicacion;
                    oProyCertificado.lProyCert_PlanProyActs.Add(oPCPPA);
                });
                lPDACs.ForEach(pdac =>
                {
                    ProyCert_PDActContr oPCPDAC = new ProyCert_PDActContr();
                    oPCPDAC.PlanProyActId = pdac.ParteDiario_Actividades.Planificacion_Proyecto_ActividadesId;
                    oPCPDAC.ContratistaId = pdac.ContratistasId;
                    oPCPDAC.Contratista = pdac.Contratistas;
                    oPCPDAC.PDActContrId = pdac.Id;
                    oPCPDAC.PDActContr = pdac;
                    oPCPDAC.Cantidad = pdac.Cantidad;
                    oPCPDAC.Estado = ValoresUtiles.Estado_PCPDAC_Abierto;
                    oProyCertificado.lProyCert_PDActContrs.Add(oPCPDAC);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificados: GetDatosParaCertificar(int, DateTime, DateTime): exception.", ex);
            }
            return oProyCertificado;
        }

        private int GrabarNuevoProyCertificado(int IdUsuarioLogueado, DataProyCert oDPC)
        {
            try
            {
                ProyCertificado oProyCertificado =
                    this.GetDatosParaCertificar(oDPC.ProyectoId, oDPC.FecDesde, oDPC.FecHasta);
                ProyCertificado oPC = new ProyCertificado();
                oPC.ProyectoId = oDPC.ProyectoId;
                oPC.ProyCertAnteriorId = oProyCertificado.ProyCertAnteriorId;
                oPC.FecDesde = oDPC.FecDesde;
                oPC.FecHasta = oDPC.FecHasta;
                oPC.UsuarioCreoId = IdUsuarioLogueado;
                oPC.FecCreacion = DateTime.Now;
                int IdPC = new ProyCertificadoNegocio().Insert(oPC);
                oProyCertificado.Id = IdPC;
                // GRABAMOS AJUSTES
                if (oDPC.lAjustes != null)
                {
                    oDPC.lAjustes.ForEach(a =>
                    {
                        oProyCertificado.lProyCert_PDActContrs.Add(new ProyCert_PDActContr()
                        {
                            PlanProyActId = a.PlanProyActId,
                            ContratistaId = a.ContratistaId,
                            UsuarioCreoId = IdUsuarioLogueado,
                            Cantidad = a.Ajuste,
                            FecCreacion = DateTime.Now,
                        });
                    });
                }

                // GRABAMOS PCPPAs
                List<ProyCert_PlanProyAct> lPCPPAs_Excedidas = new List<ProyCert_PlanProyAct>();
                oProyCertificado.lProyCert_PlanProyActs.ForEach(pcppa =>
                {
                    ProyCert_PlanProyAct oPCPPA = new ProyCert_PlanProyAct();
                    oPCPPA.ProyCertificadoId = IdPC;
                    oPCPPA.PlanProyActId = pcppa.PlanProyActId;
                    oPCPPA.UsuarioCreoId = IdUsuarioLogueado;
                    oPCPPA.Actividad = pcppa.Actividad;
                    oPCPPA.Ubicacion = pcppa.Ubicacion;
                    oPCPPA.UnidadMedida = pcppa.UnidadMedida;
                    oPCPPA.PartidaPresupuestariaId = pcppa.PartidaPresupuestariaId;
                    oPCPPA.CodigoPartidaPresupuestaria = pcppa.CodigoPartidaPresupuestaria;
                    oPCPPA.DescripcionPartidaPresupuestaria = pcppa.DescripcionPartidaPresupuestaria;
                    oPCPPA.ActividadDescripcion = pcppa.ActividadDescripcion;
                    oPCPPA.CantidadPlanificada = pcppa.CantidadPlanificada;
                    oPCPPA.ExcedenteAutorizado = false;
                    oPCPPA.CantidadAutorizadaExcedente = 0;

                    DataPPAMonto oDPPAMonto = oDPC.lPPAMontos.Find(ppam => ppam.PlanProyActId == pcppa.PlanProyActId);
                    if (oDPPAMonto != null) oPCPPA.MontoPlanificado = oDPPAMonto.Monto;
                    else oPCPPA.MontoPlanificado = pcppa.MontoPlanificado;

                    bool visado = false;
                    DataPCPPAVisada oDPCPPAVisado = oDPC.lVisadas.Find(v => v.PlanProyActId == oPCPPA.PlanProyActId);
                    if (oDPCPPAVisado != null)
                        visado = oDPCPPAVisado.Visada;
                    oPCPPA.Visada = visado;

                    oPCPPA.FecCreacion = DateTime.Now;
                    int Id = new ProyCert_PlanProyActNegocio().Insert(oPCPPA);

                    #region NOTIFICAMOS EXCEDENTES

                    pcppa.lPCPDACs = oProyCertificado.lProyCert_PDActContrs
                        .Where(pcpdac => pcpdac.PlanProyActId == oPCPPA.PlanProyActId)
                        .ToList();
                    float CantidadAcumActual = (pcppa.CantidadAcumPeriodo + pcppa.CantidadAcumAjustePeriodo);
                    if (CantidadAcumActual > pcppa.CantidadPlanificada)
                    {
                        lPCPPAs_Excedidas.Add(pcppa);
                    }

                    #endregion

                });
                if (lPCPPAs_Excedidas.Count > 0)
                    this.NotificarExcedentes(oProyCertificado, lPCPPAs_Excedidas);

                // GRABAMOS PCPDACs
                oProyCertificado.lProyCert_PDActContrs.ForEach(pcpdac =>
                {
                    ProyCert_PDActContr oPCPDAC = new ProyCert_PDActContr();
                    oPCPDAC.ProyCertificadoId = IdPC;
                    oPCPDAC.PlanProyActId = pcpdac.PlanProyActId;
                    oPCPDAC.ContratistaId = pcpdac.ContratistaId;
                    oPCPDAC.PDActContrId = pcpdac.PDActContrId;
                    oPCPDAC.Cantidad = pcpdac.Cantidad;
                    oPCPDAC.Estado = ValoresUtiles.Estado_PCPDAC_Abierto;
                    oPCPDAC.UsuarioCreoId = IdUsuarioLogueado;
                    oPCPDAC.FecCreacion = DateTime.Now;
                    int Id = new ProyCert_PDActContrNegocio().Insert(oPCPDAC);
                });
                return IdPC;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificadosController: GrabarNuevoProyCertificado(int, DataProyCert): exception.", ex);
            }
        }

        private int ActualizarAjustesProyCertificado(int IdUsuarioLogueado, DataProyCert oDPC)
        {
            try
            {
                ProyCertificado oProyCertificado = new ProyCertificadoNegocio()
                    .Get_x_Id_SQL(oDPC.ProyCertificadoId);

                // ACTUALIZAMOS AJUSTES
                if (oDPC.lAjustes != null)
                {
                    oDPC.lAjustes.ForEach(a =>
                    {
                        ProyCert_PDActContr oPCPDAC = oProyCertificado.lProyCert_PDActContrs
                            .Find(pcpdac => pcpdac.PlanProyActId == a.PlanProyActId &&
                            pcpdac.ContratistaId == a.ContratistaId &&
                            pcpdac.EsAjuste == true);
                        if (oPCPDAC == null)
                        {
                            oPCPDAC = new ProyCert_PDActContr()
                            {
                                ProyCertificadoId = oProyCertificado.Id,
                                PlanProyActId = a.PlanProyActId,
                                ContratistaId = a.ContratistaId,
                                Cantidad = a.Ajuste,
                                Estado = ValoresUtiles.Estado_PCPDAC_Abierto,
                                UsuarioCreoId = IdUsuarioLogueado,
                                FecCreacion = DateTime.Now,
                            };
                            new ProyCert_PDActContrNegocio().Insert(oPCPDAC);
                        }
                        else
                        {
                            oPCPDAC.Cantidad = a.Ajuste;
                            new ProyCert_PDActContrNegocio().Update(oPCPDAC);
                        }
                    });
                }
                ProyCert_PlanProyActNegocio oPCPPANegocio = new ProyCert_PlanProyActNegocio();
                if (oDPC.lPPAMontos != null)
                {
                    oDPC.lPPAMontos.ForEach(ppam =>
                    {
                        ProyCert_PlanProyAct oPCPPA = oPCPPANegocio
                            .Get_x_ProyCertId_PlanProyActId(oDPC.ProyCertificadoId, ppam.PlanProyActId);
                        oPCPPA.MontoPlanificado = ppam.Monto;
                        oPCPPANegocio.Update(oPCPPA);
                    });
                }
                ProyCert_PlanProyActNegocio oPCPPA_Negocio = new ProyCert_PlanProyActNegocio();
                oProyCertificado.Proyecto = new ProyectoNegocio().Get_One_Proyecto(oProyCertificado.ProyectoId);
                List<RolesUsuarios> lRolesUsuarios = new RolesUsuariosNegocio().GetRolesUsuarios(IdUsuarioLogueado);
                bool EsCoordinadorObras = lRolesUsuarios.Find(ru => ru.RolesId == ValoresUtiles.IdRol_CoordinacionObra) != null;
                if (EsCoordinadorObras && oDPC.lExcedentes != null)
                {
                    oDPC.lExcedentes.ForEach(e =>
                    {
                        ProyCert_PlanProyAct oPCPPA = oProyCertificado.lProyCert_PlanProyActs
                            .Find(pcppa => pcppa.PlanProyActId == e.PlanProyActId);
                        if (oPCPPA != null)
                        {

                            ProyCert_PlanProyAct oPCPPA_Original = oPCPPANegocio
                                .Get_x_ProyCertId_PlanProyActId(oDPC.ProyCertificadoId, e.PlanProyActId);
                            oPCPPA_Original.ExcedenteAutorizado = e.ExcedenteAutorizado;
                            oPCPPA_Original.CantidadAutorizadaExcedente = e.CantidadAutorizadaExcedente;
                            oPCPPA_Negocio.Update(oPCPPA_Original);
                        }
                    });
                }
                List<ProyCert_PlanProyAct> lPCPPAs_Excedidas = new List<ProyCert_PlanProyAct>();

                #region NOTIFICAMOS EXCEDENTES

                oProyCertificado = new ProyCertificadoNegocio()
                    .Get_x_Id_SQL(oDPC.ProyCertificadoId);
                oProyCertificado.lProyCert_PlanProyActs.ForEach(pcppa =>
                {
                    pcppa.lPCPDACs = oProyCertificado.lProyCert_PDActContrs
                        .Where(pcpdac => pcpdac.PlanProyActId == pcppa.PlanProyActId)
                        .ToList();
                    if ((pcppa.CantidadAcumPeriodo + pcppa.CantidadAcumAjustePeriodo) > pcppa.CantidadPlanificada && pcppa.NotificacionId == 0)
                        lPCPPAs_Excedidas.Add(pcppa);
                });
                if (lPCPPAs_Excedidas.Count > 0)
                    this.NotificarExcedentes(oProyCertificado, lPCPPAs_Excedidas);

                #endregion

                oProyCertificado.lProyCert_PlanProyActs.ForEach(pcppa =>
                {
                    ProyCert_PlanProyAct oPCPPA = new ProyCert_PlanProyActNegocio().Get_x_Id(pcppa.Id);
                    bool visada = false;
                    DataPCPPAVisada oDPCPPAVisado = oDPC.lVisadas.Find(v => v.PlanProyActId == oPCPPA.PlanProyActId);
                    if (oDPCPPAVisado != null) visada = oDPCPPAVisado.Visada;
                    if (visada != oPCPPA.Visada)
                    {
                        oPCPPA.Visada = visada;
                        oPCPPA_Negocio.Update(oPCPPA);
                    }
                });

                return oProyCertificado.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificadosController: ActualizarAjustesProyCertificado(int, DataProyCert): exception.", ex);
            }
        }

        private void NotificarExcedentes(ProyCertificado oProyCertificado, List<ProyCert_PlanProyAct> lPCPPA_Excedidas)
        {
            try
            {
                List<RolesUsuarios> lRolesUsuarios = new RolesUsuariosNegocio()
                    .Get_x_RolId(ValoresUtiles.IdRol_CoordinacionObra);
                if (lRolesUsuarios.Count > 0)
                {
                    lRolesUsuarios.ForEach(ru =>
                    {
                        string mensaje =
                            "Se precisa autorización para certificar actividades con excedente en la cantidad real. \r\n" +
                            "Proyecto: " + oProyCertificado.ProyectoNombre +
                            " - Periodo a certificar: " + oProyCertificado.Periodo + "\r\n" +
                            "Listado: \r\n";
                        lPCPPA_Excedidas.ForEach(pcppa =>
                        {
                            string detalle = "-> " + pcppa.Ubicacion + " - " + pcppa.Actividad +
                                " - Cantidad Planificada: " + pcppa.CantidadPlanificada +
                                " - Cantidad Real: " + (pcppa.CantidadAcumPeriodo + pcppa.CantidadAcumAjustePeriodo) + "\r\n";
                            mensaje += detalle;
                        });
                        Usuarios oUsuarioActual = this.getUsuarioActual();
                        Notificacion oNotificacion = new Notificacion();
                        oNotificacion.Mensaje = "Solicitud de aprobación";
                        oNotificacion.Mensaje = mensaje;
                        oNotificacion.UsuarioEmisorId = oUsuarioActual.Id;
                        oNotificacion.FecEmision = DateTime.Now;
                        oNotificacion.UsuarioReceptorId = ru.UsuariosId;
                        oNotificacion.Estado = ValoresUtiles.Estado_Ntf_SinLeer;
                        oNotificacion.EntidadId = oProyCertificado.Id;
                        oNotificacion.EntidadNombre = oProyCertificado.GetType().Name;

                        int NotificacionId = new NotificacionNegocio().Insert(oNotificacion);
                        ProyCert_PlanProyActNegocio oPCPPA_Negocio = new ProyCert_PlanProyActNegocio();
                        lPCPPA_Excedidas.ForEach(pcppa =>
                        {
                            pcppa.NotificacionId = NotificacionId;
                            oPCPPA_Negocio.Update(pcppa);
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificadoController: NotificarExcedente(ProyCert_PlanProyAct, float): exception.", ex);
            }
        }

        private object EnviarMail(ProyCertificado oPC, ItemContrCertificado oDCC, string path)
        {
            object oResult = new object();
            string mensaje = "";
            try
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json", optional: false);
                var configuration = builder.Build();
                string connection = configuration["SiteSettings:AdminEmail"];

                MailMessage email = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                Usuarios oUL = this.getUsuarioActual();
                email.From = new MailAddress(ValoresUtiles.EmailSeguimientoObra);
                email.Subject = "Certificado emitido por " + oUL.Apellido + " " + oUL.Nombre + " - Proyecto: " +
                    oPC.ProyectoNombre + " - Contratista: " + oDCC.Nombre + " - Periodo: " + oPC.Periodo;
                email.SubjectEncoding = System.Text.Encoding.UTF8;
                email.Body = "Certificado emitido por " + oUL.Apellido + " " + oUL.Nombre;
                email.Priority = MailPriority.Normal;

                string formFile = Path.GetFullPath(path);
                email.Attachments.Add(new Attachment(formFile));

                smtp.Host = configuration["setEmail:host"];
                smtp.Port = Convert.ToInt32(configuration["setEmail:port"]);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential(
                    configuration["setEmail:Credentials:user"], configuration["setEmail:Credentials:pass"]);
                smtp.Timeout = 20000;

                bool EnviaEmail = Convert.ToBoolean(configuration["EnviaEmail"]);
                string EmailsDestino = string.Empty;
                List<Usuarios> lUsuarios = new List<Usuarios>();
                List<RolesUsuarios> lRolesUsuarios = new List<RolesUsuarios>();
                if (oDCC.EmiteFactura)
                    lRolesUsuarios = new RolesUsuariosNegocio().Get_x_RolId(ValoresUtiles.IdRol_Administracion);
                else
                    lRolesUsuarios = new RolesUsuariosNegocio().Get_x_RolId(ValoresUtiles.IdRol_RecursosHumanos);
                lUsuarios = lRolesUsuarios
                    .GroupBy(ru => ru.UsuariosId)
                    .Select(g => g.First().Usuarios)
                    .ToList();
                if (lUsuarios.Count > 0)
                {
                    lUsuarios.ForEach(u => EmailsDestino += u.Email + ";");
                    if (!string.IsNullOrEmpty(EmailsDestino))
                    {
                        var mails = EmailsDestino.Split(';');
                        foreach (string dir in mails)
                        {
                            if (!string.IsNullOrEmpty(dir))
                            {
                                string direccion = EnviaEmail ? dir : ValoresUtiles.EmailTesting;
                                email.To.Add(direccion);
                            }
                        }
                        smtp.Send(email);
                        email.Dispose();
                        mensaje = "Certificado enviado correctamente a los siguiente destinatarios: <br/>";
                        EmailsDestino.Trim().Split(';').ToList()
                            .ForEach(email => mensaje += "- " + email + "<br/>");
                        oResult = new { mensaje = mensaje };
                    }
                    else
                    {
                        oResult = new { mensaje = "No hay drestinatarios registrados." };
                    }
                }
                else
                {
                    oResult = new { mensaje = "No hay destinatarios registrados." };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = "Error al intentar enviar el certificado",
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        private ProyCertificado GrabarCertificadoCero(Proyecto oProyecto)
        {
            ProyCertificado oProyCertificado = new ProyCertificado();
            try
            {
                List<ProyCertificado> lProyCertificados = new ProyCertificadoNegocio()
                    .Get_x_ProyectoId(oProyecto.Id);
                ProyCertificado oProyCertUno = lProyCertificados.Find(pc => pc.ProyCertAnteriorId == 0);
                if (oProyCertUno != null)
                {
                    //REDEFINIMOS LAS FECHAS PARA QUE INVOLUCREN EL PRIMER TICK DE LA FECHA DESDE
                    //Y EL ULTIMO DE LA FECHA HASTA
                    DateTime FecDesde = oProyecto.Fecha_Inicio.Date;
                    DateTime FecHasta = oProyCertUno.FecDesde.Date;
                    FecHasta = FecHasta.AddDays(-1);

                    oProyCertificado.ProyectoId = oProyecto.Id;
                    oProyCertificado.FecDesde = FecDesde;
                    oProyCertificado.FecHasta = FecHasta;
                    oProyCertificado.FecCreacion = DateTime.Now;
                    oProyCertificado.UsuarioCreoId = 1;

                    int proyCertId = new ProyCertificadoNegocio().Insert(oProyCertificado);
                    oProyCertUno.ProyCertAnteriorId = proyCertId;

                    List<ParteDiario_Actividades_Contratista> lPDACs =
                        new ParteDiario_Actividades_ContratistaNegocio()
                        .Get_x_ProyectoId_FecDesde_FecHasta(oProyecto.Id, FecDesde, FecHasta, true)
                        .Where(pdac => pdac.ContratistasId != ValoresUtiles.IdContratista_Pucheta)
                        .ToList();
                    List<Planificacion_Proyecto_Actividades> lPPAs = lPDACs
                        .GroupBy(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_ActividadesId)
                        .Select(g => g.First().ParteDiario_Actividades.Planificacion_Proyecto_Actividades)
                        .ToList();

                    lPPAs.ForEach(ppa =>
                    {
                        ProyCert_PlanProyAct oPCPPA = new ProyCert_PlanProyAct();
                        oPCPPA.ProyCertificadoId = proyCertId;
                        oPCPPA.PlanProyActId = ppa.Id;
                        oPCPPA.PlanProyAct = ppa;
                        oPCPPA.MontoPlanificado = ppa.Monto;
                        oPCPPA.CantidadPlanificada = ppa.Cantidad;
                        oPCPPA.UnidadMedida = ppa.UnidadMedida;
                        oPCPPA.PartidaPresupuestariaId = ppa.Planificacion_Actividades.PartidaPresupuestariaId;
                        oPCPPA.CodigoPartidaPresupuestaria = ppa.Planificacion_Actividades.CodigoPartidaPresupuestaria;
                        oPCPPA.DescripcionPartidaPresupuestaria = ppa.Planificacion_Actividades.DescripcionPartidaPresupuestaria;
                        oPCPPA.Actividad = ppa.Actividad;
                        if (string.IsNullOrEmpty(ppa.Detalle))
                            oPCPPA.ActividadDescripcion = "Sin Descripción";
                        else oPCPPA.ActividadDescripcion = ppa.Detalle;
                        oPCPPA.Ubicacion = ppa.Ubicacion;
                        oPCPPA.FecCreacion = DateTime.Now;
                        oPCPPA.UsuarioCreoId = 1;
                        new ProyCert_PlanProyActNegocio().Insert(oPCPPA);
                    });
                    lPDACs.ForEach(pdac =>
                    {
                        ProyCert_PDActContr oPCPDAC = new ProyCert_PDActContr();
                        oPCPDAC.ProyCertificadoId = proyCertId;
                        oPCPDAC.PlanProyActId = pdac.ParteDiario_Actividades.Planificacion_Proyecto_ActividadesId;
                        oPCPDAC.ContratistaId = pdac.ContratistasId;
                        oPCPDAC.Contratista = pdac.Contratistas;
                        oPCPDAC.PDActContrId = pdac.Id;
                        oPCPDAC.PDActContr = pdac;
                        oPCPDAC.Cantidad = pdac.Cantidad;
                        oPCPDAC.Estado = ValoresUtiles.Estado_PCPDAC_Abierto;
                        oPCPDAC.FecCreacion = DateTime.Now;
                        oPCPDAC.UsuarioCreoId = 1;
                        new ProyCert_PDActContrNegocio().Insert(oPCPDAC);
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificados: GetDatosParaCertificar(int, DateTime, DateTime): exception.", ex);
            }
            return oProyCertificado;
        }

        private string CalcularNombrePDFCertificado(int ProyCertificadoId, int ContratistaId)
        {
            try
            {
                Contratistas oContratista = new ContratistasNegocio().Get_One_Contratistas(ContratistaId);
                string nombreArchivo = "Certificado_" + oContratista.Nombre + "_" + ProyCertificadoId + "_";
                string nombreCaperta = Path.Combine(_env.WebRootPath, "archivos/certificados/obras/");
                DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(nombreCaperta);
                FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(nombreArchivo + "*");
                int version = 1;
                foreach (FileInfo foundFile in filesInDir)
                {
                    version++;
                }
                return nombreArchivo + "V" + version + ".pdf";
            }
            catch (Exception ex)
            {
                throw new Exception("Error: GestionCertificados: CalcularNroVersionContrCertificado: exception.", ex);
            }
        }

        private void GrabarProyCertificadosCero()
        {
            try
            {
                List<Proyecto> lProyectos = new ProyectoNegocio().Get_All_Proyecto()
                    .Where(p => p.Estado != ValoresUtiles.Estado_Proy_Ejecutado)
                    .ToList();
                lProyectos.ForEach(p =>
                {
                    this.GrabarCertificadoCero(p);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error: GestionCertificadosController: GrabarProyCertificadosCero(): exception.", ex);
            }
        }

        private ItemProyCert_x_Contratista GetItemProyCertContratista(int proyCertificadoId)
        {
            try
            {
                ProyCertificado oProyCertificado = new ProyCertificadoNegocio()
                    .Get_x_Id_SQL(proyCertificadoId);
                ItemProyCert_x_Contratista oIProyCertContr = new ItemProyCert_x_Contratista();
                if (oProyCertificado.ProyCertAnteriorId > 0)
                    oIProyCertContr = this.GetItemProyCertContr_ConAcumuladoCalculado(oProyCertificado);
                else oIProyCertContr = oProyCertificado.AsData_DetallePorContratistas();
                oIProyCertContr.lContrCertificados.ForEach(cc =>
                {
                    cc.NumeroCertificado = this.GetNumeroContrCertificado(cc, oProyCertificado.ProyCertAnterior);
                });
                List<PartidaPresupuestaria> lPartidasPresupuestarias = new PartidaPresupuestariaNegocio()
                        .GetAll()
                        .OrderBy(pp => pp.Codigo)
                        .ToList();
                oIProyCertContr.lContrCertificados.ForEach(cc =>
                {
                    cc.lDataPartidas.ForEach(dp =>
                    {
                        PartidaPresupuestaria oPartPres = lPartidasPresupuestarias
                            .Find(pp => pp.Codigo.ToUpper() == dp.Codigo.ToUpper());
                        if (oPartPres != null) dp.Descripcion = oPartPres.Descripcion;
                    });
                });
                return oIProyCertContr;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: GestionCertificadosController: GetItemProyCertContratista(int): exception.", ex);
            }
        }

        private ItemProyCertificado GetItemProyCert_ConAcumuladoCalculado(ProyCertificado oProyCertificado)
        {
            try
            {
                ItemProyCertificado oIPC = oProyCertificado.AsData();
                if (oProyCertificado.ProyCertAnteriorId > 0)
                {
                    ItemProyCertificado oIPC_Ant =
                        this.GetItemProyCert_ConAcumuladoCalculado(oProyCertificado.ProyCertAnterior);
                    oIPC.lPCPPAs.ForEach(pcppa =>
                    {
                        ItemProyCert_PlanProyAct oPCPPA_Ant = oIPC_Ant.lPCPPAs
                            .Find(pcppaa => pcppaa.PlanProyActId == pcppa.PlanProyActId);
                        if (oPCPPA_Ant != null)
                        {
                            pcppa.CantidadAcumAnterior += oPCPPA_Ant.CantidadAcumActual;
                            pcppa.CantidadAcumActual += pcppa.CantidadAcumAnterior;
                            pcppa.ImporteAcumAnterior += oPCPPA_Ant.ImporteAcumActual + oPCPPA_Ant._ImporteAcumAnterior;
                            pcppa.ImporteAcumActual += pcppa.ImporteAcumAnterior;
                            pcppa.lContratistasAvances.ForEach(ca =>
                            {
                                ItemContratistaAvance oICA_Ant = oPCPPA_Ant.lContratistasAvances
                                    .Find(caa => caa.ContratistaId == ca.ContratistaId);
                                if (oICA_Ant != null)
                                {
                                    ca.CantidadAcumAnterior += oICA_Ant.CantidadAcumActual;
                                    ca.CantidadAcumActual += ca.CantidadAcumAnterior;
                                    ca.ImporteAcumAnterior += oICA_Ant.ImporteAcumActual;
                                    ca.ImporteAcumActual += ca.ImporteAcumAnterior;
                                }
                            });
                        }
                    });
                }
                return oIPC;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificadoController: GetItemProyCert_ConAcumuladoCalculado(ItemProyCertificado): exception.", ex);
            }
        }

        private int GetNumeroContrCertificado(ItemContrCertificado oIContrCertificado, ProyCertificado oProyCertAnterior)
        {
            try
            {
                if (oProyCertAnterior != null)
                {
                    ItemProyCert_x_Contratista oIProyCertContAnterior = oProyCertAnterior.AsData_DetallePorContratistas();
                    ItemContrCertificado oIContrCertAnterior = oIProyCertContAnterior.lContrCertificados.Find(cc =>
                        cc.ContratistaId == oIContrCertificado.ContratistaId);
                    if (oIContrCertAnterior != null)
                    {
                        if (oProyCertAnterior.ProyCertAnteriorId > 0)
                        {
                            return 1 + this.GetNumeroContrCertificado(oIContrCertAnterior, oProyCertAnterior.ProyCertAnterior);
                        }
                        else return 2;
                    }
                    // puede haber una quincena en medio en la que no haya participado el contratista.
                    else if (oIProyCertContAnterior.lContrCertificados.Count > 0)
                    {
                        if (oProyCertAnterior.ProyCertAnteriorId > 0)
                        {
                            return this.GetNumeroContrCertificado(oIContrCertificado, oProyCertAnterior.ProyCertAnterior);
                        }
                        else return 2;
                    }
                    else return 1;
                }
                else return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificadoController: GetNumeroContrCertificado(ItemContrCertificado, ItemProyCertificado): exception.", ex);
            }
        }

        private ItemProyCert_x_Contratista GetItemProyCertContr_ConAcumuladoCalculado(ProyCertificado oProyCertificado)
        {
            try
            {
                ItemProyCert_x_Contratista oIPCC = oProyCertificado.AsData_DetallePorContratistas();
                if (oProyCertificado.ProyCertAnteriorId > 0)
                {

                    ItemProyCert_x_Contratista oIPCC_Ant =
                        this.GetItemProyCertContr_ConAcumuladoCalculado(oProyCertificado.ProyCertAnterior);
                    oIPCC.lContrCertificados.ForEach(cc =>
                    {
                        ItemContrCertificado oICC_Ant = oIPCC_Ant.lContrCertificados
                            .Find(cca => cca.ContratistaId == cc.ContratistaId);
                        if (oICC_Ant != null)
                        {
                            cc.lPCPPAs.ForEach(pcppa =>
                            {
                                ItemProyCert_PlanProyAct oPCPPA = oICC_Ant.lPCPPAs
                                    .Find(pcppaa => pcppaa.PlanProyActId == pcppa.PlanProyActId);
                                if (oPCPPA != null)
                                {
                                    pcppa.CantidadAcumAnterior += oPCPPA.CantidadAcumActual;
                                    pcppa.CantidadAcumActual += pcppa.CantidadAcumAnterior;
                                    pcppa.ImporteAcumAnterior += oPCPPA.ImporteAcumActual + oPCPPA._ImporteAcumAnterior;
                                    pcppa.ImporteAcumActual += pcppa.ImporteAcumAnterior;
                                }
                            });
                        }
                    });
                }
                return oIPCC;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificadoController: GetItemProyCert_ConAcumuladoCalculado(ItemProyCertificado): exception.", ex);
            }
        }

        #endregion

        #region USUARIO LOGUEADO

        private ItemLoginUsuario getItemLoguinUsuarioActual()
        {
            ItemLoginUsuario d = new ItemLoginUsuario();
            string[] us = (HttpContext.User.Identity.Name).Split("#");

            d.Id = Convert.ToInt32(us[0]);
            d.Nombre = us[1];
            d.Apellido = us[2];
            d.Email = us[3];
            d.Tipo = us[4];
            d.ApellidoNombre = us[1] + ", " + us[2];
            d.Grupo = Convert.ToInt32(us[5]);

            return d;
        }

        private Usuarios getUsuarioActual()
        {
            Usuarios oUsuario = new Usuarios();
            try
            {
                ItemLoginUsuario oILUsuario = this.getItemLoguinUsuarioActual();
                oUsuario = new UsuariosNegocio().Get_Usuario(oILUsuario.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: getUsuarioActual(): exception.", ex);
            }
            return oUsuario;
        }

        #endregion

        #region PROTOTIPO

        [HttpPost]
        public object GetContratistasCertificados_ConData([FromBody] DataFiltroCertContratistas oDFCC)
        {
            object oResult;
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<ProyCert_Contratista> lPC_Contratistas = this.GetProyCertContratistas(oDFCC);
                    List<object> lContrCertificados = this.GetContrCertificados(lPC_Contratistas, true);
                    List<ProyContr_Descuento> lProyContr_Descuentos = new ProyContr_DescuentoNegocio()
                        .Get_x_ProyectoId_ContratistaId(oDFCC.lIdsProyectos.First(), oDFCC.lIdsContratistas.First());
                    oResult = new
                    {
                        lContrCertificados,
                        lProyContr_Descuentos = lProyContr_Descuentos
                            .Select(pcd => pcd.AsData())
                            .ToList()
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        private List<ProyCert_Contratista> GetProyCertContratistas(DataFiltroCertContratistas oDFCC)
        {
            try
            {
                DateTime FecDesde = DateTime.MinValue;
                if (!string.IsNullOrEmpty(oDFCC.FechaDesde)) FecDesde = DateTime.Parse(oDFCC.FechaDesde);
                FecDesde = FecDesde.Date;
                DateTime FecHasta = DateTime.MaxValue;
                if (!string.IsNullOrEmpty(oDFCC.FechaHasta)) FecHasta = DateTime.Parse(oDFCC.FechaHasta);
                FecHasta = FecHasta.Date;
                List<ProyCert_Contratista> lPC_Contratistas = new ProyCert_ContratistaNegocio()
                    .Get_x_lIdsProyectos_lIdsContratistas(oDFCC.lIdsProyectos, oDFCC.lIdsContratistas);
                lPC_Contratistas = lPC_Contratistas
                    .Where(pcc => FecDesde <= pcc.ProyCertificado.FecDesde && pcc.ProyCertificado.FecHasta <= FecHasta)
                    .ToList();
                return lPC_Contratistas;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificadosController: GetProyCertContratistas(DataFiltroCertContratistas): exception.", ex);
            }
        }

        private List<object> GetContrCertificados(List<ProyCert_Contratista> lPC_Contratistas, bool conData)
        {
            try
            {
                List<object> lContrCertificados = new List<object>();
                lPC_Contratistas.GroupBy(pcc => pcc.ProyCertificado.ProyectoId)
                    .ToList()
                    .ForEach(g =>
                    {
                        List<ProyCert_Contratista> lPCCs_Todos = g.ToList();

                        g.ToList().GroupBy(_pcc => _pcc.ProyCertificadoId)
                        .ToList()
                        .ForEach(gr =>
                        {
                            int ProyCertificadoId = gr.First().ProyCertificadoId;
                            List<ProyCert_PlanProyAct> lPCPPAs = new ProyCert_PlanProyActNegocio()
                                .Get_x_ProyCertId(ProyCertificadoId);
                            List<ProyCert_PDActContr> lPCPDACs = new ProyCert_PDActContrNegocio()
                                .Get_x_ProyCertificadoId(ProyCertificadoId);
                            List<ProyCert_PDActContr_Adicional> lPCPDACAs = new ProyCert_PDActContr_AdicionalNegocio()
                                .Get_x_ProyCertId(ProyCertificadoId);

                            List<ProyCert_Contratista> lPCCs = gr.ToList();
                            lPCCs.ForEach(pcc =>
                            {
                                string estado = new ProyCert_ContratistaNegocio()
                                    .ConsultarEstado(pcc.ProyCertificadoId, pcc.ContratistaId);

                                #region CODIGO SUSPENDIDO
                                //int NumeroCertificado = 1;
                                //if (pcc.ProyCertificado.ProyCertAnteriorId != 0)
                                //{
                                //bool continuar = true;
                                //int proyCertAnteriorId = pcc.ProyCertificado.ProyCertAnteriorId;
                                //// YA NO SE CALCULARÁN MAS LOS Nros DE CERT, AHORA SE GRABAN EN LA BD
                                //while (continuar)
                                //{
                                //    ProyCert_Contratista oPCC_Anterior = lPCCs_Todos
                                //        .Find(_pcc => _pcc.ProyCertificadoId == proyCertAnteriorId);
                                //    bool continuarDeTodosModos = false;
                                //    if (oPCC_Anterior == null && proyCertAnteriorId != 0)
                                //    {
                                //        ProyCertificado oPC = new ProyCertificadoNegocio()
                                //            .Get_x_Id(proyCertAnteriorId, false);
                                //        proyCertAnteriorId = oPC.ProyCertAnteriorId;
                                //        continuarDeTodosModos = true;
                                //    }
                                //    if (oPCC_Anterior != null || continuarDeTodosModos)
                                //    {
                                //        if (oPCC_Anterior != null)
                                //        {
                                //            string estadoAnterior = new ProyCert_ContratistaNegocio()
                                //                .ConsultarEstado(oPCC_Anterior.ProyCertificadoId, oPCC_Anterior.ContratistaId);
                                //            if (estadoAnterior == ValoresUtiles.Estado_PCPDAC_Cerrado)
                                //                NumeroCertificado++;
                                //            proyCertAnteriorId = oPCC_Anterior.ProyCertificado.ProyCertAnteriorId;
                                //        }
                                //    }
                                //    else continuar = false;
                                //}
                                //}
                                #endregion

                                if (estado == ValoresUtiles.Estado_PCPDAC_Cerrado)
                                {
                                    if (conData)
                                    {
                                        List<ProyCert_PDActContr> lPCPDACs_Aux = lPCPDACs
                                            .Where(pcpdac => pcpdac.ContratistaId == pcc.ContratistaId)
                                            .ToList();
                                        List<int> lIdsPPA = lPCPDACs_Aux
                                            .GroupBy(pcpdac => pcpdac.PlanProyActId)
                                            .Select(g => g.First().PlanProyActId)
                                            .ToList();
                                        List<ProyCert_PlanProyAct> lPCPPAs_Aux = lPCPPAs
                                            .Where(pcppa => lIdsPPA.Contains(pcppa.PlanProyActId))
                                            .ToList();
                                        List<ProyCert_PDActContr_Adicional> lPCPDACAs_Ajustables_Aux = lPCPDACAs
                                            .Where(pcpdaca => pcpdaca.ContratistaId == pcc.ContratistaId &&
                                                pcpdaca.MontosAjustables == true)
                                            .ToList();
                                        List<ProyCert_PDActContr_Adicional> lPCPDACAs_NoAjustables_Aux = lPCPDACAs
                                            .Where(pcpdaca => pcpdaca.ContratistaId == pcc.ContratistaId &&
                                                pcpdaca.MontosAjustables == false)
                                            .ToList();

                                        //A
                                        float ImporteTotalActual = 0;
                                        lPCPDACs_Aux.ForEach(pcpdac =>
                                        {
                                            ProyCert_PlanProyAct oPCPPA = lPCPPAs_Aux
                                            .Find(pcppa => pcppa.PlanProyActId == pcpdac.PlanProyActId);
                                            ImporteTotalActual += pcpdac.Cantidad * oPCPPA.MontoPlanificado;
                                        });
                                        //B
                                        float ImporteTotalActualAjustable = 0;
                                        lPCPDACAs_Ajustables_Aux.ForEach(pcpdaca =>
                                            ImporteTotalActualAjustable += pcpdaca.Cantidad * pcpdaca.Monto);
                                        //D
                                        float ImporteTotalActualNoAjustable = 0;
                                        lPCPDACAs_NoAjustables_Aux.ForEach(pcpdaca =>
                                            ImporteTotalActualNoAjustable += pcpdaca.Cantidad * pcpdaca.Monto);

                                        float Descuento = pcc.PorcentajeDescuentoAnticipo * ImporteTotalActual / 100;
                                        float Adelanto = pcc.Adelanto;
                                        float TotalAjustable = ImporteTotalActual - Descuento - Adelanto + ImporteTotalActualAjustable;
                                        float DiferenciasIndices = pcc.IndiceActual - pcc.IndiceBase;
                                        float Coeficiente = DiferenciasIndices * 100 / (pcc.IndiceBase == 0 ? 1 : pcc.IndiceBase);
                                        float Actualizacion = Coeficiente * TotalAjustable / 100;
                                        float FondoReparo =
                                            pcc.PorcentajeFondoReparo *
                                            (ImporteTotalActual + ImporteTotalActualAjustable + ImporteTotalActualNoAjustable + Actualizacion) / 100;
                                        lContrCertificados.Add(new
                                        {
                                            pcc.Id,
                                            pcc.ProyCertificadoId,
                                            pcc.ProyCertificado.ProyectoId,
                                            ProyectoNombre = pcc.ProyCertificado.Proyecto.Nombre,
                                            pcc.ContratistaId,
                                            ContratistaNombre = pcc.Contratista.Nombre,
                                            pcc.NumeroCertificado,
                                            pcc.ProyCertificado.FechaCreacion,
                                            pcc.ProyCertificado.Periodo,
                                            FondoReparo
                                        });
                                    }
                                    else
                                    {
                                        lContrCertificados.Add(new
                                        {
                                            pcc.Id,
                                            pcc.ProyCertificadoId,
                                            pcc.ProyCertificado.ProyectoId,
                                            ProyectoNombre = pcc.ProyCertificado.Proyecto.Nombre,
                                            pcc.ContratistaId,
                                            ContratistaNombre = pcc.Contratista.Nombre,
                                            pcc.NumeroCertificado,
                                            pcc.ProyCertificado.FechaCreacion,
                                            pcc.ProyCertificado.Periodo
                                        });
                                    }
                                }
                            });
                        });
                    });
                return lContrCertificados;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: GestionCertificadosController: GetContrCertificados(List<ProyCert_Contratista>): esxception.", ex);
            }
        }

        #endregion

        #region REFORMULACIÓN DE MÉTODOS

        [HttpGet]
        public object GetProyCert_Contratistas(int ProyCertificadoId)
        {
            object oResult;
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<PartidaPresupuestaria> lPartidasPresupuestarias = new PartidaPresupuestariaNegocio()
                            .GetAll()
                            .OrderBy(pp => pp.Codigo)
                            .ToList();
                    oResult = new
                    {
                        oProyCertificado = this.GetProyCert_AsData_x_Contratistas(ProyCertificadoId),
                        lPartidasPresupuestarias = lPartidasPresupuestarias
                            .Select(pp => new
                            {
                                pp.Codigo,
                                pp.Descripcion,
                                CodigoDescripcion = pp.Codigo + " - " + pp.Descripcion
                            })
                            .ToList()
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }
        private ItemProyCert_x_Contratista GetProyCert_AsData_x_Contratistas(int ProyCertificadoId)
        {
            try
            {

                ProyCertificado oProyCertificado = new ProyCertificadoNegocio()
                    .Get_x_Id(ProyCertificadoId, false);
                List<int> lIdsProyCerts = new List<int>();
                lIdsProyCerts.Add(ProyCertificadoId);
                int ProyCertAnteriorId = oProyCertificado.ProyCertAnteriorId;
                bool continuar = ProyCertAnteriorId > 0;
                while (continuar)
                {
                    lIdsProyCerts.Add(ProyCertAnteriorId);
                    ProyCertificado oProyCert_Anterior = new ProyCertificadoNegocio()
                        .Get_x_Id(ProyCertAnteriorId, false);
                    ProyCertAnteriorId = oProyCert_Anterior.ProyCertAnteriorId;
                    continuar = ProyCertAnteriorId > 0;
                }

                #region LISTAS TOTALES
                List<ProyCert_PlanProyAct> lPCPPAs_Todos = new ProyCert_PlanProyActNegocio()
                    .Get_x_ProyectoId(lIdsProyCerts);
                List<ProyCert_PDActContr> lPCPDACs_Todos = new ProyCert_PDActContrNegocio()
                    .Get_x_ProyectoId(lIdsProyCerts);
                List<ProyCert_PDActContr_Adicional> lPCPDACAs_Todos = new ProyCert_PDActContr_AdicionalNegocio()
                    .Get_x_ProyectoId(lIdsProyCerts);
                ProyCert_ContratistaNegocio oPCCNegocio = new ProyCert_ContratistaNegocio();
                List<ProyCert_Contratista> lPCContratistas_Todos = oPCCNegocio
                    .Get_x_lIdsProyCertificados(lIdsProyCerts);
                List<ItemProyCert_Contratista> lIPCContratistas_Todos = new List<ItemProyCert_Contratista>();
                lPCContratistas_Todos.ForEach(pcc =>
                {
                    string estado = oPCCNegocio.ConsultarEstado(pcc.ProyCertificadoId, pcc.ContratistaId);
                    lIPCContratistas_Todos.Add(new ItemProyCert_Contratista()
                    {
                        Id = pcc.Id,
                        ProyCertificadoId = pcc.ProyCertificadoId,
                        ContratistaId = pcc.ContratistaId,
                        NumeroCertificado = pcc.NumeroCertificado,
                        Estado = estado
                    });
                });
                List<ProyCert_Empleado> lPCEmpleados_Todos = new ProyCert_EmpleadoNegocio()
                    .Get_x_ProyectoId(lIdsProyCerts);
                List<int> lIdsContratistas_Todos = lPCPDACs_Todos
                    .GroupBy(pcpdaca => pcpdaca.ContratistaId)
                    .Select(g => g.First().ContratistaId)
                    .ToList();
                List<Contratistas> lContratistas = new ContratistasNegocio()
                    .Get_x_lIds(lIdsContratistas_Todos);
                #endregion

                #region LISTAS ACTUALES
                List<ProyCert_PDActContr> lPCPDACs_Actuales = lPCPDACs_Todos
                    .Where(pcpdca => pcpdca.ProyCertificadoId == ProyCertificadoId)
                    .ToList();
                List<int> lIdsPPAs_Actuales = lPCPDACs_Actuales
                    .GroupBy(pcpdac => pcpdac.PlanProyActId)
                    .Select(g => g.First().PlanProyActId)
                    .ToList();
                List<ProyCert_PlanProyAct> lPCPPAs_Actuales = lPCPPAs_Todos
                    .Where(pcppa => pcppa.ProyCertificadoId == ProyCertificadoId &&
                        lIdsPPAs_Actuales.Contains(pcppa.PlanProyActId) == true)
                    .ToList();
                List<ProyCert_PDActContr_Adicional> lPCPDACAs_Actuales = lPCPDACAs_Todos
                    .Where(pcpdaca => pcpdaca.ProyCertificadoId == ProyCertificadoId)
                    .ToList();
                #endregion

                #region LISTAS ANTERIORES
                List<ProyCert_PDActContr> lPCPDCAs_Anteriores = lPCPDACs_Todos
                    .Where(pcpdca => pcpdca.ProyCertificadoId != ProyCertificadoId)
                    .ToList();
                List<int> lIdsPPAs_Anteriores = lPCPDCAs_Anteriores
                    .GroupBy(pcpdac => pcpdac.PlanProyActId)
                    .Select(g => g.First().PlanProyActId)
                    .ToList();
                List<ProyCert_PlanProyAct> lPCPPAs_Anteriores = lPCPPAs_Todos
                    .Where(pcppa => pcppa.ProyCertificadoId != ProyCertificadoId &&
                        lIdsPPAs_Anteriores.Contains(pcppa.PlanProyActId) == true)
                    .ToList();
                List<ProyCert_PDActContr_Adicional> lPCPDACAs_Anteriores = lPCPDACAs_Todos
                    .Where(pcpdaca => pcpdaca.ProyCertificadoId != ProyCertificadoId)
                    .ToList();
                #endregion

                #region POR CADA CONTRATISTA DEL CERT ACTUAL

                List<int> lIdsContratistas = lPCPDACs_Actuales
                    .GroupBy(pcpdac => pcpdac.ContratistaId)
                    .Select(g => g.First().ContratistaId)
                    .ToList();
                List<ItemContrCertificado> lICCs = new List<ItemContrCertificado>();
                lIdsContratistas.ForEach(cId =>
                {
                    #region PLANIFICADAS

                    #region LISTAS ACTUALES X CONTRATISTA
                    List<ProyCert_PDActContr> lPCPDACs_Actuales_x_Contr = lPCPDACs_Actuales
                        .Where(pcpdac => pcpdac.ContratistaId == cId)
                        .ToList();
                    List<int> lIdsPCPPA_Actuales_x_Contr = lPCPDACs_Actuales_x_Contr
                        .GroupBy(pcpdac => pcpdac.PlanProyActId)
                        .Select(g => g.First().PlanProyActId)
                        .ToList();
                    List<ProyCert_PlanProyAct> lPCPPAs_Actuales_x_Contr = lPCPPAs_Actuales
                        .Where(pcppa => lIdsPCPPA_Actuales_x_Contr.Contains(pcppa.PlanProyActId))
                        .ToList();
                    #endregion

                    #region LISTAS ANTERIORES X CONTRATISTA
                    List<ProyCert_PDActContr> lPCPDACs_Anteriores_x_Contr = lPCPDCAs_Anteriores
                        .Where(pcpdac => pcpdac.ContratistaId == cId)
                        .ToList();
                    List<int> lIdsPCPPA_Anteriores_x_Contr = lPCPDACs_Anteriores_x_Contr
                        .GroupBy(pcpdac => pcpdac.PlanProyActId)
                        .Select(g => g.First().PlanProyActId)
                        .ToList();
                    List<ProyCert_PlanProyAct> lPCPPAs_Anteriores_x_Contr = lPCPPAs_Anteriores
                        .Where(pcppa => lIdsPCPPA_Anteriores_x_Contr.Contains(pcppa.PlanProyActId))
                        .ToList();
                    #endregion

                    #region ACUMULAMOS ACTUALES
                    List<ItemProyCert_PlanProyAct> lIPCPPAs = new List<ItemProyCert_PlanProyAct>();
                    lPCPPAs_Actuales_x_Contr.ForEach(pcppa =>
                    {
                        ItemProyCert_PlanProyAct oIPCPPA = new ItemProyCert_PlanProyAct();
                        if (this.getUsuarioActual().Id == 1)
                            oIPCPPA.Ubicacion = pcppa.PlanProyActId + " - " + pcppa.Ubicacion;
                        else
                            oIPCPPA.Ubicacion = pcppa.Ubicacion;
                        oIPCPPA.Actividad = pcppa.Actividad;
                        oIPCPPA.ActividadDescripcion = pcppa.ActividadDescripcion;
                        oIPCPPA.UnidadMedida = pcppa.UnidadMedida;
                        oIPCPPA.CodigoPartidaPresupuestaria = pcppa.CodigoPartidaPresupuestaria;
                        oIPCPPA.CantidadPlanificada = pcppa.CantidadPlanificada;
                        oIPCPPA.MontoPlanificado = pcppa.MontoPlanificado;
                        oIPCPPA.CantidadAcumPeriodo = lPCPDACs_Actuales_x_Contr
                            .Where(pcpdac => pcpdac.PlanProyActId == pcppa.PlanProyActId)
                            .Sum(pcpac => pcpac.Cantidad);
                        oIPCPPA._ImporteAcumAnterior = lPCPDACs_Actuales_x_Contr
                            .Where(pcpdac => pcpdac.PlanProyActId == pcppa.PlanProyActId)
                            .Sum(pcpac => pcpac.ImporteAcumAnterior);
                        oIPCPPA.ImporteAcumPeriodo = oIPCPPA.CantidadAcumPeriodo * pcppa.MontoPlanificado;
                        oIPCPPA.PlanProyActId = pcppa.PlanProyActId;
                        oIPCPPA.lContratistasAvances = new List<ItemContratistaAvance>();
                        lIPCPPAs.Add(oIPCPPA);
                    });
                    #endregion

                    #region ACUMULAMOS ANTERIORES
                    lPCPPAs_Anteriores_x_Contr
                        .ForEach(pcppa =>
                        {
                            List<ProyCert_PDActContr> lPCPDAC_x_c_x_pc_x_ppa = lPCPDACs_Anteriores_x_Contr
                                .Where(pcpdac => pcpdac.PlanProyActId == pcppa.PlanProyActId &&
                                    pcpdac.ProyCertificadoId == pcppa.ProyCertificadoId)
                                .ToList();
                            ItemProyCert_Contratista oIPCC = lIPCContratistas_Todos
                                .Find(pcc => pcc.ProyCertificadoId == pcppa.ProyCertificadoId &&
                                    pcc.ContratistaId == cId);
                            ItemProyCert_PlanProyAct oIPCPPA = lIPCPPAs
                                .Find(ipcppa => ipcppa.PlanProyActId == pcppa.PlanProyActId);
                            if (oIPCC != null && oIPCC.Estado == ValoresUtiles.Estado_PCPDAC_Cerrado)
                            {
                                if (oIPCPPA == null)
                                {
                                    oIPCPPA = new ItemProyCert_PlanProyAct();
                                    if (this.getUsuarioActual().Id == 1)
                                        oIPCPPA.Ubicacion = pcppa.PlanProyActId + " - " + pcppa.Ubicacion;
                                    else
                                        oIPCPPA.Ubicacion = pcppa.Ubicacion;
                                    oIPCPPA.Actividad = pcppa.Actividad;
                                    oIPCPPA.ActividadDescripcion = pcppa.ActividadDescripcion;
                                    oIPCPPA.UnidadMedida = pcppa.UnidadMedida;
                                    oIPCPPA.CodigoPartidaPresupuestaria = pcppa.CodigoPartidaPresupuestaria;
                                    oIPCPPA.CantidadPlanificada = pcppa.CantidadPlanificada;
                                    oIPCPPA.MontoPlanificado = pcppa.MontoPlanificado;
                                    oIPCPPA.PlanProyActId = pcppa.PlanProyActId;
                                    oIPCPPA.lContratistasAvances = new List<ItemContratistaAvance>();
                                    lIPCPPAs.Add(oIPCPPA);
                                }
                                oIPCPPA.CantidadAcumAnterior += lPCPDAC_x_c_x_pc_x_ppa
                                    .Sum(pcpdac => pcpdac.Cantidad);
                                oIPCPPA.ImporteAcumAnterior += (lPCPDAC_x_c_x_pc_x_ppa
                                    .Sum(pcpdac => pcpdac.Cantidad) * pcppa.MontoPlanificado) +
                                    lPCPDAC_x_c_x_pc_x_ppa.Sum(pcpdac => pcpdac.ImporteAcumAnterior);
                            }
                        });
                    #endregion

                    #region CALCULAMOS ACUMULADO FINAL
                    lIPCPPAs.ForEach(ipcppa =>
                    {
                        ipcppa.CantidadAcumActual = ipcppa.CantidadAcumPeriodo + ipcppa.CantidadAcumAnterior;
                        ipcppa.ImporteAcumActual = ipcppa.ImporteAcumPeriodo + ipcppa.ImporteAcumAnterior;
                    });
                    #endregion

                    #endregion

                    #region ADICIONALES

                    List<ItemProyCert_PDActContr> lIPCACAs = new List<ItemProyCert_PDActContr>();
                    List<ProyCert_PDActContr_Adicional> lPCPDACAs_Actuales_X_Contr = lPCPDACAs_Actuales
                        .Where(pcpdaca => pcpdaca.ContratistaId == cId)
                        .ToList();
                    lIPCACAs = lPCPDACAs_Actuales_X_Contr
                        .Select(pcpdaca => pcpdaca.AsData()).ToList();
                    List<ProyCert_PDActContr_Adicional> lPCPDACAs_Anteriores_X_Contr = lPCPDACAs_Anteriores
                        .Where(pcpdaca => pcpdaca.ContratistaId == cId)
                        .ToList();
                    List<ItemProyCert_PDActContr> lIPCACAs_Anteriores = new List<ItemProyCert_PDActContr>();
                    lIPCACAs_Anteriores = lPCPDACAs_Anteriores_X_Contr
                        .Select(pcpdaca => pcpdaca.AsData()).ToList();
                    lIPCACAs_Anteriores.ForEach(ipcpdaca =>
                    {
                        ItemProyCert_PDActContr oIPCPDACA_Actual = lIPCACAs.Find(ipcpdaca_act =>
                            ipcpdaca_act.Ubicacion.ToUpper() == ipcpdaca.Ubicacion.ToUpper() &&
                            ipcpdaca_act.Actividad.ToUpper() == ipcpdaca.Actividad.ToUpper() &&
                            ipcpdaca_act.ActividadDescripcion.ToUpper() == ipcpdaca.ActividadDescripcion.ToUpper() &&
                            ipcpdaca_act.UnidadMedida.ToUpper() == ipcpdaca.UnidadMedida.ToUpper() &&
                            ipcpdaca_act.CodigoPartidaPresupuestaria.ToUpper() == ipcpdaca.CodigoPartidaPresupuestaria.ToUpper());
                        if (oIPCPDACA_Actual != null)
                        {
                            oIPCPDACA_Actual.CantidadAcumAnterior += ipcpdaca.Cantidad;
                            oIPCPDACA_Actual.ImporteAcumAnterior += ipcpdaca.Monto * ipcpdaca.Cantidad;
                        }
                        else
                        {
                            ipcpdaca.CantidadAcumAnterior = ipcpdaca.Cantidad;
                            ipcpdaca.ImporteAcumAnterior = ipcpdaca.Cantidad * ipcpdaca.Monto;
                            ipcpdaca.Cantidad = 0;
                            ipcpdaca.Monto = 0;
                            lIPCACAs.Add(ipcpdaca);
                        }
                    });
                    #endregion

                    #region INSTANCIAMOS ITEM CERT X CADA CONTRATISTA
                    ProyCert_Contratista oPCC = lPCContratistas_Todos
                        .Find(pcc => pcc.ProyCertificadoId == ProyCertificadoId &&
                            pcc.ContratistaId == cId);
                    if (oPCC == null)
                    {
                        oPCC = new ProyCert_Contratista()
                        {
                            ContratistaId = cId
                        };
                        //Si existe un Cert Anterior para este contratista, debemos traer el indice base
                        ProyCert_Contratista oPCC_Anterior = oPCCNegocio
                            .Get_x_ProyCertId_ContratistaId(oProyCertificado.ProyCertAnteriorId, cId);
                        if (oPCC_Anterior != null)
                        {
                            oPCC.IndiceActual = oPCC_Anterior.IndiceActual;
                            oPCC.IndiceBase = oPCC_Anterior.IndiceBase;
                        }
                    }

                    List<string> lCodPartPres = new List<string>();
                    lPCPPAs_Actuales.ForEach(pcppa =>
                    {
                        if (!lCodPartPres.Contains(pcppa.CodigoPartidaPresupuestaria.ToUpper()))
                        {
                            lCodPartPres.Add(pcppa.CodigoPartidaPresupuestaria.ToUpper());
                        }
                    });
                    lPCPDACAs_Actuales.ForEach(pcpdaca =>
                    {
                        if (!lCodPartPres.Contains(pcpdaca.CodigoPartidaPresupuestaria.ToUpper()))
                        {
                            lCodPartPres.Add(pcpdaca.CodigoPartidaPresupuestaria.ToUpper());
                        }
                    });
                    List<PartidaPresupuestaria> lPartidasPresupuestarias = new PartidaPresupuestariaNegocio().GetAll();
                    ItemContrCertificado oICC = new ItemContrCertificado()
                    {
                        Id = oPCC.Id,
                        EmiteFactura = oPCC.EmiteFactura,
                        PorcentajeIVA = oPCC.PorcentajeIVA,
                        IndiceBase = oPCC.IndiceBase,
                        IndiceActual = oPCC.IndiceActual,
                        PorcentajeActualizacion = oPCC.PorcentajeActualizacion,
                        Adelanto = oPCC.Adelanto,
                        PorcentajeDescuentoAnticipo = oPCC.PorcentajeDescuentoAnticipo,
                        PorcentajeFondoReparo = oPCC.Id == 0 ? 5 : oPCC.PorcentajeFondoReparo,
                        lDataPartidas = lPartidasPresupuestarias.Where(pp =>
                            lCodPartPres.Contains(pp.Codigo.ToUpper()))
                            .Select(pp => new ItemPartidaPresupuestaria()
                            {
                                Codigo = pp.Codigo.ToUpper(),
                                Descripcion = pp.Descripcion
                            })
                            .ToList(),
                        lProyCert_Empleados = lPCEmpleados_Todos
                            .Where(pce =>
                                pce.ProyCertificadoId == oPCC.ProyCertificadoId &&
                                pce.ContratistaId == oPCC.ContratistaId)
                            .Select(pce => pce.AsData())
                            .ToList(),
                        ContratistaId = oPCC.ContratistaId,
                        Nombre = lContratistas.Find(c => c.Id == oPCC.ContratistaId).Nombre,
                        lPCPPAs = lIPCPPAs,
                        lPCPPA_APs = new List<ItemProyCert_PlanProyAct>(),
                        lPCPDAC_As = lIPCACAs,
                        CantTotalAnterior = 0,
                        CantTotalActual = 0,
                        CantTotalAcumulada = 0,
                        ImpTotalAnterior = 0,
                        ImpTotalActual = 0,
                        ImpTotalAcumulado = 0,
                        Abierto = new ProyCert_ContratistaNegocio()
                            .ConsultarEstado(ProyCertificadoId, oPCC.ContratistaId) == ValoresUtiles.Estado_PCPDAC_Abierto,
                        NumeroCertificado = oPCC.Id > 0 ? oPCC.NumeroCertificado : 0,
                    };
                    lICCs.Add(oICC);
                    #endregion
                });

                #endregion

                return new ItemProyCert_x_Contratista()
                {
                    Id = oProyCertificado.Id,
                    Detalle = oProyCertificado.Detalle,
                    ProyectoNombre = oProyCertificado.ProyectoNombre,
                    ProyectoId = oProyCertificado.ProyectoId,
                    FecDesde = oProyCertificado.FecDesde.ToString(ValoresUtiles.Formato_dd_MM_yyyy),
                    FecHasta = oProyCertificado.FecHasta.ToString(ValoresUtiles.Formato_dd_MM_yyyy),
                    FechaCreacion = oProyCertificado.FechaCreacion,
                    lContrCertificados = lICCs,
                    NumeroCertificado = 1,
                    EsCertificadoCero = oProyCertificado.ProyCertAnteriorId == 0
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error: GestionCertificadosController: GetProyCert_AsData_x_Contratistas(int): exception.", ex);
            }
        }

        #endregion

        #region COCINADAS

        private void CocinarNumerosCertificados()
        {
            try
            {
                List<ProyCert_Contratista> lPCC_Todos = new ProyCert_ContratistaNegocio()
                    .Get_All();
                lPCC_Todos.ForEach(pcc => {
                    pcc.NumeroCertificado = 0;
                    new ProyCert_ContratistaNegocio().Update(pcc);
                });
                List<ProyCertificado> lProyCertificados = new ProyCertificadoNegocio()
                    .Get_All();
                lProyCertificados.OrderBy(pc => pc.FecDesde)
                    .ToList()
                    .ForEach(pc =>
                {
                    List<ProyCert_PDActContr> lPCPDACs = new ProyCert_PDActContrNegocio()
                        .Get_x_ProyCertificadoId(pc.Id);
                    List<ProyCert_Contratista> lPCCs = new ProyCert_ContratistaNegocio()
                        .Get_x_ProyCertificadoId(pc.Id);
                    lPCPDACs.GroupBy(pcpdac => pcpdac.ContratistaId)
                        .ToList()
                        .ForEach(g =>
                        {
                            int ContratistaId = g.First().ContratistaId;
                            string Estado = g.ToList()
                                .Where(pcpdac => pcpdac.Estado == ValoresUtiles.Estado_PCPDAC_Abierto)
                                .Count() > 0 ? ValoresUtiles.Estado_PCPDAC_Abierto : ValoresUtiles.Estado_PCPDAC_Cerrado;
                            ProyCert_Contratista oPCC = lPCCs.Find(pcc => pcc.ContratistaId == ContratistaId);
                            int NumCertificado = new ProyCert_ContratistaNegocio()
                                    .GetNuevoNumCertificado(pc.ProyectoId, ContratistaId);
                            if (Estado == ValoresUtiles.Estado_PCPDAC_Cerrado)
                            {
                                if (oPCC == null)
                                {
                                    oPCC = new ProyCert_Contratista
                                    {
                                        ProyCertificadoId = pc.Id,
                                        ContratistaId = ContratistaId,
                                        NumeroCertificado = NumCertificado
                                    };
                                    new ProyCert_ContratistaNegocio().Insert(oPCC);
                                }
                                else
                                {
                                    oPCC.NumeroCertificado = NumCertificado;
                                    new ProyCert_ContratistaNegocio().Update(oPCC);
                                }
                            }
                        });
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error: GestionCertificadosController: CocinarNumerosCertificados(): exception.", ex);
            }
        }

        private void CocinarPCPDACs_CorregirPDAId()
        {
            try
            {
                List<ProyCertificado> lProyCertificados = new ProyCertificadoNegocio()
                    .Get_All()
                    .ToList();
                ProyCert_PDActContrNegocio oPCPDACNegocio = new ProyCert_PDActContrNegocio();
                ParteDiario_ActividadesNegocio oPDANegocio = new ParteDiario_ActividadesNegocio();
                ParteDiario_Actividades_ContratistaNegocio oPDACNegocio = new ParteDiario_Actividades_ContratistaNegocio();
                ProyCert_ContratistaNegocio oPCCNegocio = new ProyCert_ContratistaNegocio();
                lProyCertificados.ForEach(pc =>
                {
                    List<ProyCert_PDActContr> lPCPDACs = oPCPDACNegocio
                        .Get_x_ProyCertificadoId(pc.Id)
                        .Where(pcpdac => pcpdac.Estado == ValoresUtiles.Estado_PCPDAC_Cerrado)
                        .ToList();
                    lPCPDACs.GroupBy(pcpdac => pcpdac.ContratistaId)
                        .ToList()
                        .ForEach(g =>
                        {
                            int ContratistaId = g.First().ContratistaId;
                            ProyCert_Contratista oPCC = oPCCNegocio
                                .Get_x_ProyCertId_ContratistaId(pc.Id, ContratistaId);
                            if (oPCC == null)
                            {
                                oPCC = new ProyCert_Contratista
                                {
                                    ProyCertificadoId = pc.Id,
                                    ContratistaId = ContratistaId
                                };
                                new ProyCert_ContratistaNegocio().Insert(oPCC);
                            }
                            g.ToList().ForEach(pcpdac =>
                            {
                                List<ParteDiario_Actividades> lPDAs = oPDANegocio
                                            .Get_X_IdPlanProyActividad(pcpdac.PlanProyActId)
                                            .Where(pda => pda.ParteDiario.Fecha_Creacion <= pc.FecHasta)
                                            .OrderByDescending(pda => pda.ParteDiario.Fecha_Creacion)
                                            .ToList();
                                int PDAId = 0;
                                if (lPDAs.Count > 0)
                                {
                                    PDAId = lPDAs.First().Id;
                                    ParteDiario_Actividades oPDA = new ParteDiario_ActividadesNegocio()
                                        .Get_One_ParteDiario_Actividades(PDAId);
                                    ParteDiario oPD = new ParteDiarioNegocio()
                                        .Get_One_ParteDiario(oPDA.ParteDiarioId);
                                    if (pcpdac.PDActContrId > 0)
                                    {
                                        ParteDiario_Actividades_Contratista oPDAC = oPDACNegocio
                                            .Get_x_Id(pcpdac.PDActContrId);
                                        if (oPDAC.TipoRegistro == ValoresUtiles.TipoRegistro_PDAC_Automatico)
                                        {
                                            oPDAC.ParteDiario_ActividadesId = PDAId;
                                            oPDAC.ParteDiario_Actividades = lPDAs.Find(pda => pda.Id == PDAId);
                                            oPDAC.Fecha = oPD.Fecha_Creacion;
                                            oPDACNegocio.Update(oPDAC);
                                        }
                                    }
                                    else
                                    {
                                        ParteDiario_Actividades_Contratista oPDAC = new ParteDiario_Actividades_Contratista();
                                        oPDAC.ParteDiario_ActividadesId = PDAId;
                                        oPDAC.ContratistasId = pcpdac.ContratistaId;
                                        oPDAC.Cantidad = pcpdac.Cantidad;
                                        oPDAC.TipoRegistro = ValoresUtiles.TipoRegistro_PDAC_Automatico;
                                        oPDAC.Fecha = oPD.Fecha_Creacion;
                                        pcpdac.PDActContrId = oPDACNegocio.Insert(oPDAC);
                                        oPCPDACNegocio.Update(pcpdac);
                                    }
                                }
                            });
                        });
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error: GestionCertificadosController: CocinarPCPDACs_CorregirPDAId(): exception.", ex);
            }
        }

        #endregion

    }

}