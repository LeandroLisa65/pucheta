using Gestion.EF;
using Gestion.EF.Models;
using Gestion.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
//using SelectPdf;

namespace Gestion.Web.Controllers
{
    public class EstadisticasController : Controller
    {

        #region INICIALIZACIÓN

        public ActionResult index()
        {
            var oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {

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

        public ActionResult _EstadisticasIncidentes()
        {
            var oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<Proyecto> lProyectos = new ProyectoNegocio()
                        .Get_All_Proyecto()
                        .Where(p => p.Estado == string.Empty)
                        .OrderBy(p => p.Nombre)
                        .ToList(); ;
                    DateTime FecDesde = DateTime.Now.AddMonths(-1);
                    DateTime FecHasta = DateTime.Now;
                    int ProyectoId = 0;
                    if (lProyectos.Count > 0) ProyectoId = lProyectos.First().Id;
                    object oEstInc = new object();
                    if(ProyectoId > 0) oEstInc = this.GetEstadisticasIncidentes(FecDesde, FecHasta, ProyectoId);
                    oResult = new
                    {
                        FecDesde = FecDesde.ToString(ValoresUtiles.Formato_yyyy_MM_dd),
                        FecHasta = FecHasta.ToString(ValoresUtiles.Formato_yyyy_MM_dd),
                        lProyectos = lProyectos
                            .Select(p => new { p.Id, p.Nombre })
                            .ToList(),
                        oEstInc
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
            return PartialView(oResult);
        }

        public ActionResult _EstadisticasCertificados()
        {
            var oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<Proyecto> lProyectos = new ProyectoNegocio()
                        .Get_All_Proyecto()
                        .Where(p => p.Estado == string.Empty)
                        .OrderBy(p => p.Nombre)
                        .ToList(); ;
                    DateTime FecDesde = DateTime.Now.AddMonths(-1);
                    DateTime FecHasta = DateTime.Now;
                    int ProyectoId = 0;
                    if (lProyectos.Count > 0) ProyectoId = lProyectos.First().Id;
                    object oEstCert = new object();
                    if (ProyectoId > 0) oEstCert = this.GetEstadisticasCertificados_x_Proyecto(FecDesde, FecHasta, ProyectoId);
                    object oEstCert_Todos = new object();
                    oEstCert_Todos = this.GetEstadisticasCertificados_x_TodosProyectos(FecDesde, FecHasta);
                    oResult = new
                    {
                        FecDesde = FecDesde.ToString(ValoresUtiles.Formato_yyyy_MM_dd),
                        FecHasta = FecHasta.ToString(ValoresUtiles.Formato_yyyy_MM_dd),
                        lProyectos = lProyectos
                            .Select(p => new { p.Id, p.Nombre })
                            .ToList(),
                        oEstCert,
                        oEstCert_Todos
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
            return PartialView(oResult);
        }

        #endregion

        #region GETS

        #endregion

        #region POTS

        public partial class FiltroEstadistica
        {
            public DateTime FecDesde { get; set; }
            public DateTime FecHasta { get; set; }
            public int ProyectoId { get; set; }
        }

        /// <summary>
        /// Método que devuelve un objeto con los datos estadísticos de incidentes para
        /// un periodo (FesDesde y fecHasta) y un proyecto determinados.
        /// </summary>
        /// <returns>object</returns>
        [HttpPost]
        public object RecargarEstIncidentes(FiltroEstadistica oFiltro)
        {
            var oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    oResult = new
                    {
                        oEstInc = this.GetEstadisticasIncidentes(
                            oFiltro.FecDesde, oFiltro.FecHasta, oFiltro.ProyectoId)
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

        /// <summary>
        /// Método que devuelve un objeto con los datos estadísticos de actividades certificadas para
        /// un periodo (FesDesde y fecHasta) y un proyecto determinados.
        /// El periodo es para las actividades planificadas la fecha en que se han registrado en los partes diarios
        /// y para las actividades adicionales la fecha en que se han registrado en la certificación.
        /// </summary>
        /// <returns>object</returns>
        [HttpPost]
        public object RecargarEstCertificados(FiltroEstadistica oFiltro)
        {
            var oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    oResult = new
                    {
                        oEstCert = this.GetEstadisticasCertificados_x_Proyecto(
                            oFiltro.FecDesde, oFiltro.FecHasta, oFiltro.ProyectoId),
                        oEstCert_Todos = this.GetEstadisticasCertificados_x_TodosProyectos(
                            oFiltro.FecDesde, oFiltro.FecHasta)
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

        #endregion

        #region PRIVADOS

        private object GetEstadisticasIncidentes(DateTime FecDesde, DateTime FecHasta, int ProyectoId)
        {
            try
            {
                List<Incidentes_Historial> lIncidentes = new Incidentes_HistorialNegocio()
                    .Get_x_Periodo_ProyectoId(FecDesde, FecHasta, ProyectoId)
                    .Where(i => i.EstadoId == ValoresUtiles.IdEstado_IncHist_Cerrado)
                    .ToList();
                Proyecto oProyecto = new ProyectoNegocio().Get_One_Proyecto(ProyectoId);
                List<int> lIdsIncidentes = lIncidentes.Select(i => i.Id).ToList();
                List<IncidentesHistorial_Detalle> lIncDetalles = new IncidenteHistorial_DetalleNegocio()
                    .Get_By_lIds(lIdsIncidentes);
                List<dynamic> lDatosIncidentes = new List<dynamic>();
                lIncidentes.ForEach(ih =>
                {
                    List<IncidentesHistorial_Detalle> lIncDet_Filtrados = lIncDetalles
                        .Where(ihd => ihd.IdIncidente_Historial == ih.Id)
                        .OrderBy(ihd => ihd.Asignacion_Fecha)
                        .ToList();
                    string estado = string.Empty;

                    int cantDiasAbierto = 0;
                    DateTime fecInicioAbierto = DateTime.MinValue;
                    DateTime fecFinAbierto = DateTime.MinValue;
                    int cantDiasTratamiento = 0;
                    DateTime fecInicioTratamiento = DateTime.MinValue;
                    DateTime fecFinTratamiento = DateTime.MinValue;
                    int cantDiasEnviado = 0;
                    DateTime fecInicioEnviado = DateTime.MinValue;
                    DateTime fecFinEnviado = DateTime.MinValue;
                    int cantDiasValidacion = 0;
                    DateTime fecInicioValidacion = DateTime.MinValue;
                    DateTime fecFinValidacion = DateTime.MinValue;

                    DateTime fecCreacion = ih.Creacion_Fecha;
                    DateTime fecCierre = DateTime.MinValue;
                    IncidentesHistorial_Detalle oIHD_Cierre = lIncDet_Filtrados
                        .Find(ihd => ihd.Estado == ValoresUtiles.Estado_IncHist_Cerrado);
                    if (oIHD_Cierre != null)
                    {
                        fecCierre = oIHD_Cierre.Asignacion_Fecha;
                        if (fecCierre > fecCreacion)
                        {
                            int cantidadDias = (int)(fecCierre - fecCreacion).TotalDays + 1;
                            DateTime fecha = fecCreacion;
                            List<IncidentesHistorial_Detalle> lIHDetalles = new List<IncidentesHistorial_Detalle>();
                            for (int i = 0; i < cantidadDias; i++)
                            {
                                lIHDetalles = lIncDet_Filtrados
                                    .Where(ihd => ihd.Asignacion_Fecha.Date == fecha.Date)
                                    .ToList();
                                if (lIHDetalles.Count > 0)
                                {
                                    lIHDetalles.ForEach(ihd =>
                                    {
                                        estado = ihd.Estado;
                                        if (estado == ValoresUtiles.Estado_IncHist_Abierto)
                                        {
                                            if (fecInicioAbierto == DateTime.MinValue)
                                                fecInicioAbierto = fecha;
                                            fecFinAbierto = fecha;
                                            cantDiasAbierto++;
                                        }
                                        if (estado == ValoresUtiles.Estado_IncHist_Tratamiento)
                                        {
                                            if (fecInicioTratamiento == DateTime.MinValue)
                                                fecInicioTratamiento = fecha;
                                            fecFinTratamiento = fecha;
                                            cantDiasTratamiento++;
                                        }
                                        if (estado == ValoresUtiles.Estado_IncHist_Enviado)
                                        {
                                            if (fecInicioEnviado == DateTime.MinValue)
                                                fecInicioEnviado = fecha;
                                            fecFinEnviado = fecha;
                                            cantDiasEnviado++;
                                        }
                                        if (estado == "Validacion")
                                        {
                                            if (fecInicioValidacion == DateTime.MinValue)
                                                fecInicioValidacion = fecha;
                                            fecFinValidacion = fecha;
                                            cantDiasValidacion++;
                                        }
                                    });
                                }
                                else
                                {
                                    if (estado == ValoresUtiles.Estado_IncHist_Abierto)
                                    {
                                        if (fecInicioAbierto == DateTime.MinValue)
                                            fecInicioAbierto = fecha;
                                        fecFinAbierto = fecha;
                                        cantDiasAbierto++;
                                    }
                                    if (estado == ValoresUtiles.Estado_IncHist_Tratamiento)
                                    {
                                        if (fecInicioTratamiento == DateTime.MinValue)
                                            fecInicioTratamiento = fecha;
                                        fecFinTratamiento = fecha;
                                        cantDiasTratamiento++;
                                    }
                                    if (estado == ValoresUtiles.Estado_IncHist_Enviado)
                                    {
                                        if (fecInicioEnviado == DateTime.MinValue)
                                            fecInicioEnviado = fecha;
                                        fecFinEnviado = fecha;
                                        cantDiasEnviado++;
                                    }
                                    if (estado == "Validacion")
                                    {
                                        if (fecInicioValidacion == DateTime.MinValue)
                                            fecInicioValidacion = fecha;
                                        fecFinValidacion = fecha;
                                        cantDiasValidacion++;
                                    }
                                }
                                fecha = fecha.AddDays(1);
                            }
                            dynamic oDatoIncidente = new ExpandoObject();

                            oDatoIncidente.Id = ih.Id;
                            oDatoIncidente.FecInicioAbierto = fecInicioAbierto > DateTime.MinValue ?
                                fecInicioAbierto.ToString(ValoresUtiles.Formato_dd_MM_yyyy) : "-";
                            oDatoIncidente.FecFinAbierto = fecFinAbierto > DateTime.MinValue ?
                                fecFinAbierto.ToString(ValoresUtiles.Formato_dd_MM_yyyy) : "-";
                            oDatoIncidente.CantDiasAbierto = cantDiasAbierto;

                            oDatoIncidente.FecInicioTratamiento = fecInicioTratamiento > DateTime.MinValue ?
                                fecInicioTratamiento.ToString(ValoresUtiles.Formato_dd_MM_yyyy) : "-";
                            oDatoIncidente.FecFinTratamiento = fecFinTratamiento > DateTime.MinValue ?
                                fecFinTratamiento.ToString(ValoresUtiles.Formato_dd_MM_yyyy) : "-";
                            oDatoIncidente.CantDiasTratamiento = cantDiasTratamiento;

                            oDatoIncidente.FecInicioEnviado = fecInicioEnviado > DateTime.MinValue ?
                                fecInicioEnviado.ToString(ValoresUtiles.Formato_dd_MM_yyyy) : "-";
                            oDatoIncidente.FecFinEnviado = fecFinEnviado > DateTime.MinValue ?
                                fecFinEnviado.ToString(ValoresUtiles.Formato_dd_MM_yyyy) : "-";
                            oDatoIncidente.CantDiasEnviado = cantDiasEnviado;

                            oDatoIncidente.FecInicioValidacion = fecInicioValidacion > DateTime.MinValue ?
                                fecInicioValidacion.ToString(ValoresUtiles.Formato_dd_MM_yyyy) : "-";
                            oDatoIncidente.FecFinValidacion = fecFinValidacion > DateTime.MinValue ?
                                fecFinValidacion.ToString(ValoresUtiles.Formato_dd_MM_yyyy) : "-";
                            oDatoIncidente.CantDiasValidacion = cantDiasValidacion;

                            oDatoIncidente.FecCierre = fecCierre.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
                            oDatoIncidente.CantDiasTotal = cantidadDias;

                            lDatosIncidentes.Add(oDatoIncidente);
                        }
                    }
                });
                float cantIncidentes = lDatosIncidentes.Count;
                float CantDiasPromedioCierre = 0;
                float CantDiasCierreMaxima = 0;
                float CantDiasPromedioAbierto = 0;
                float CantDiasPromedioEnviado = 0;
                float CantDiasPromedioTratamiento = 0;
                float CantDiasPromedioValidacion = 0;
                if(cantIncidentes > 0)
                {
                    CantDiasPromedioCierre = lDatosIncidentes.Sum(di => di.CantDiasTotal) / cantIncidentes;
                    CantDiasCierreMaxima = lDatosIncidentes.Max(di => di.CantDiasTotal);
                    CantDiasPromedioAbierto = lDatosIncidentes.Sum(di => di.CantDiasAbierto) / cantIncidentes;
                    CantDiasPromedioEnviado = lDatosIncidentes.Sum(di => di.CantDiasEnviado) / cantIncidentes;
                    CantDiasPromedioTratamiento = lDatosIncidentes.Sum(di => di.CantDiasTratamiento) / cantIncidentes;
                    CantDiasPromedioValidacion = lDatosIncidentes.Sum(di => di.CantDiasValidacion) / cantIncidentes;
                }
                return new
                {
                    Titulo = "Incidentes - Proyecto: " + oProyecto.Nombre + " - Periodo: " +
                        FecDesde.ToString(ValoresUtiles.Formato_dd_MM_yyyy) + " - " +
                        FecHasta.ToString(ValoresUtiles.Formato_dd_MM_yyyy),
                    oConsolidado = new
                    {
                        CantDiasPromedioCierre,
                        CantDiasCierreMaxima,
                        CantDiasPromedioAbierto,
                        CantDiasPromedioEnviado,
                        CantDiasPromedioTratamiento,
                        CantDiasPromedioValidacion
                    },
                    lDetalles = lDatosIncidentes
                };
            }
            catch(Exception ex)
            {
                throw new Exception(
                    "Error: EstadisticasController: GetEstadisticasIncidentes(DateTime, DateTime, int): exception.", ex);
            }
        }

        private object GetEstadisticasCertificados_x_Proyecto(DateTime FecDesde, DateTime FecHasta, int ProyectoId)
        {
            try
            {
                List<ParteDiario_Actividades_Contratista> lPDACs =
                    new ParteDiario_Actividades_ContratistaNegocio()
                    .Get_x_ProyectoId_FecDesde_FecHasta(ProyectoId, FecDesde, FecHasta, false)
                    .Where(pdac => pdac.ContratistasId != ValoresUtiles.IdContratista_Pucheta)
                    .ToList();
                List<int> lIdsPDACs = lPDACs
                    .Select(pdac => pdac.Id)
                    .ToList();
                List<ProyCert_PDActContr> lPCPDACs = new ProyCert_PDActContrNegocio()
                    .Get_x_lIdsPDACs(lIdsPDACs);
                List<ProyCert_PlanProyAct> lPCPPAs = new List<ProyCert_PlanProyAct>();
                lPCPDACs.ForEach(pcpdac =>
                {
                    ProyCert_PlanProyAct oPCPPA = lPCPPAs.Find(pcppa =>
                        pcppa.ProyCertificadoId == pcpdac.ProyCertificadoId &&
                        pcppa.PlanProyActId == pcpdac.PlanProyActId);
                    if(oPCPPA == null)
                    {
                        oPCPPA = new ProyCert_PlanProyActNegocio().Get_x_ProyCertId_PlanProyActId(
                            pcpdac.ProyCertificadoId, pcpdac.PlanProyActId);
                        lPCPPAs.Add(oPCPPA);
                    }
                });
                List<ProyCert_PDActContr_Adicional> lPCPDAC_As = new ProyCert_PDActContr_AdicionalNegocio()
                    .Get_x_ProyectoId_FecDesde_FecHasta(ProyectoId, FecDesde, FecHasta);

                dynamic oEstCert = new ExpandoObject();
                Proyecto oProyecto = new ProyectoNegocio().Get_One_Proyecto(ProyectoId);
                oEstCert.ProyectoNombre = oProyecto.Nombre;
                oEstCert.Periodo = FecDesde.ToString(ValoresUtiles.Formato_dd_MM_yyyy) + " - " +
                    FecHasta.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
                oEstCert.TotalActividadesPlanificadas = lPCPPAs.Count;
                oEstCert.TotalActividadesAdicionales = lPCPDAC_As.Count;
                oEstCert.TotalActividades = lPCPPAs.Count + lPCPDAC_As.Count;
                oEstCert.ImporteTotalActPlanificadas = 0.00;

                #region CALCULAMOS LOS TOTALES

                lPCPPAs.ForEach(pcppa =>
                {
                    List<ProyCert_PDActContr> lPCPDACs_filtradas = lPCPDACs
                        .Where(pcpdac => pcpdac.PlanProyActId == pcppa.PlanProyActId &&
                            pcppa.ProyCertificadoId == pcpdac.ProyCertificadoId)
                        .ToList();
                    oEstCert.ImporteTotalActPlanificadas += lPCPDACs_filtradas
                        .Sum(pcpdac => pcpdac.Cantidad * pcppa.MontoPlanificado);
                });
                oEstCert.ImporteTotalActAdicionales = 0.00;
                lPCPDAC_As.ForEach(pcpdac => oEstCert.ImporteTotalActAdicionales += (pcpdac.Cantidad * pcpdac.Monto));

                #endregion

                #region CLASIFICAMOS ADICIONALES 

                List<dynamic> lAdicionales = new List<dynamic>();
                lPCPDAC_As.GroupBy(pcpdaca => pcpdaca.Actividad.ToUpper()).ToList().ForEach(g =>
                {
                    List<ProyCert_PDActContr_Adicional> lPCPDAC_As_Aux = g.ToList();
                    dynamic oDatoActividad = new ExpandoObject();
                    oDatoActividad.Actividad = lPCPDAC_As_Aux.First().Actividad;
                    oDatoActividad.Importe = lPCPDAC_As_Aux.Sum(pcpdaca => pcpdaca.Cantidad * pcpdaca.Monto);
                    lAdicionales.Add(oDatoActividad);
                });
                lAdicionales = lAdicionales.OrderByDescending(c => c.Importe).ToList();
                oEstCert.lAdicionales = lAdicionales;
                List<dynamic> lContratistas = new List<dynamic>();
                lPCPDAC_As.GroupBy(pcpdaca => pcpdaca.ContratistaId).ToList().ForEach(g =>
                {
                    List<ProyCert_PDActContr_Adicional> lPCPDAC_As_Aux = g.ToList();
                    dynamic oDatoContratista = new ExpandoObject();
                    Contratistas oContratista = new ContratistasNegocio()
                        .Get_One_Contratistas(lPCPDAC_As_Aux.First().ContratistaId);
                    oDatoContratista.Contratista = oContratista.Nombre;
                    oDatoContratista.Importe = lPCPDAC_As_Aux.Sum(pcpdaca => pcpdaca.Cantidad * pcpdaca.Monto);
                    lContratistas.Add(oDatoContratista);
                });
                lContratistas = lContratistas.OrderByDescending(c => c.Importe).ToList();
                oEstCert.lContratistas = lContratistas;

                #endregion

                return oEstCert;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: EstadisticasController: GetEstadisticasProyCertificados(DateTime, DateTime, int): exception.", ex);
            }
        }

        private object GetEstadisticasCertificados_x_TodosProyectos(DateTime FecDesde, DateTime FecHasta)
        {
            try
            {
                List<ParteDiario_Actividades_Contratista> lPDACs =
                    new ParteDiario_Actividades_ContratistaNegocio()
                    .Get_x_ProyectoId_FecDesde_FecHasta(0, FecDesde, FecHasta, false)
                    .Where(pdac => pdac.ContratistasId != ValoresUtiles.IdContratista_Pucheta)
                    .ToList();
                List<int> lIdsPDACs = lPDACs
                    .Select(pdac => pdac.Id)
                    .ToList();
                List<ProyCert_PDActContr> lPCPDACs = new ProyCert_PDActContrNegocio()
                    .Get_x_lIdsPDACs(lIdsPDACs);
                List<ProyCert_PlanProyAct> lPCPPAs = new List<ProyCert_PlanProyAct>();
                lPCPDACs.ForEach(pcpdac =>
                {
                    ProyCert_PlanProyAct oPCPPA = lPCPPAs.Find(pcppa =>
                        pcppa.ProyCertificadoId == pcpdac.ProyCertificadoId &&
                        pcppa.PlanProyActId == pcpdac.PlanProyActId);
                    if (oPCPPA == null)
                    {
                        oPCPPA = new ProyCert_PlanProyActNegocio().Get_x_ProyCertId_PlanProyActId(
                            pcpdac.ProyCertificadoId, pcpdac.PlanProyActId);
                        lPCPPAs.Add(oPCPPA);
                    }
                });
                List<ProyCert_PDActContr_Adicional> lPCPDAC_As = new ProyCert_PDActContr_AdicionalNegocio()
                    .Get_x_ProyectoId_FecDesde_FecHasta(0, FecDesde, FecHasta);

                dynamic oEstCert_Todos = new ExpandoObject();
                oEstCert_Todos.Periodo = FecDesde.ToString(ValoresUtiles.Formato_dd_MM_yyyy) + " - " +
                    FecHasta.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
                oEstCert_Todos.TotalActividadesPlanificadas = lPCPPAs.Count;
                oEstCert_Todos.TotalActividadesAdicionales = lPCPDAC_As.Count;
                oEstCert_Todos.TotalActividades = lPCPPAs.Count + lPCPDAC_As.Count;
                oEstCert_Todos.ImporteTotalActPlanificadas = 0.00;

                #region CALCULAMOS LOS TOTALES

                lPCPPAs.ForEach(pcppa =>
                {
                    List<ProyCert_PDActContr> lPCPDACs_filtradas = lPCPDACs
                        .Where(pcpdac => pcpdac.PlanProyActId == pcppa.PlanProyActId &&
                            pcppa.ProyCertificadoId == pcpdac.ProyCertificadoId)
                        .ToList();
                    oEstCert_Todos.ImporteTotalActPlanificadas += lPCPDACs_filtradas
                        .Sum(pcpdac => pcpdac.Cantidad * pcppa.MontoPlanificado);
                });
                oEstCert_Todos.ImporteTotalActAdicionales = 0.00;
                lPCPDAC_As.ForEach(pcpdac => oEstCert_Todos.ImporteTotalActAdicionales += (pcpdac.Cantidad * pcpdac.Monto));

                #endregion

                return oEstCert_Todos;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: EstadisticasController: GetEstadisticasProyCertificados(DateTime, DateTime, int): exception.", ex);
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

        public  Usuarios getUsuarioActual()
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

    }
}