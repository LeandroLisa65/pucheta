using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestion.Web.Models;
using Gestion.EF.Models;
using Gestion.Negocio;
using Gestion.EF;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Gestion.Web.Controllers
{
    public class IncidentesController : Controller
    {
        // GET: Incidentes
        private readonly IWebHostEnvironment _env;

        public IncidentesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        #region Incidentes
        public ActionResult index()
        {
            return View();
        }

        #region IncidentesHistorial

        public ActionResult IncidentesHistorial(int id)
        {
            object oResult = new object();
            List<string> lstProyectos = new ProyectoNegocio().Get_All_Proyecto().Where(p => p.Estado != "Ejecutado")
                .OrderBy(p => p.Nombre)
                .Select(o => o.Nombre)
                .Distinct()
                
                .ToList();
            List<string> lTipos = new IncidentesNegocio()
                .Get_All_Incidentes()
                .Where(i => i.Rectype == "Novedad")
                .Select(i => i.Nombre)
                .Distinct()
                .OrderBy(n => n)
                .ToList();
            List<string> lContratistas = new ContratistasNegocio()
                .Get_All_Contratistas()
                .Select(c => c.Nombre)
                .Distinct()
                .OrderBy(n => n)
                .ToList();
            List<string> lCriticidades = ValoresUtiles.Get_lCriticidades_ParteDiarioIncidente();
            oResult = new
            {
                lstProyectos,
                lTipos,
                lContratistas,
                lCriticidades
            };
            return View(oResult);
        }

        public partial class FiltroPeriodoFechas
        {
            public string FechaDesde { get; set; }
            public string FechaHasta { get; set; }
            public DateTime FecDesde
            {
                get
                {
                    DateTime fecDesde = new DateTime();
                    try
                    {
                        if(!string.IsNullOrEmpty(this.FechaDesde))
                            fecDesde = DateTime.Parse(this.FechaDesde);
                        else
                            fecDesde = DateTime.Today.AddMonths(-1);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: FiltroPeriodoFechas: FecDesde: exception.", ex);
                    }
                    return fecDesde;
                }
            }
            public DateTime FecHasta
            {
                get
                {
                    DateTime fecHasta = new DateTime();
                    try
                    {
                        if (!string.IsNullOrEmpty(this.FechaHasta))
                            fecHasta = DateTime.Parse(this.FechaHasta);
                        else
                            fecHasta = DateTime.Today;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: FiltroPeriodoFechas: FecHasta: exception.", ex);
                    }
                    return fecHasta;
                }
            }
        }

        public ActionResult _IncidentesHistorialGrilla(FiltroPeriodoFechas oFiltro)
        {
            List<Incidentes_Historial> ListaG = new List<Incidentes_Historial>();
            ListaG = CargoIncidentes(oFiltro.FecDesde, oFiltro.FecHasta, "0", "CA");
            return PartialView(ListaG);
        }

        public ActionResult _NovedadesGrilla(FiltroPeriodoFechas oFiltro)
        {
            object oResult = new object();
            try
            {
                ItemLoginUsuario oILUsuario = getUsuarioActual();
                Usuarios oUsuario = new UsuariosNegocio().Get_Usuario(oILUsuario.Id);
                if (oUsuario != null)
                {
                    List<ParteDiario_Incidentes> lNovedades = new List<ParteDiario_Incidentes>();
                    List<RolesUsuarios> lRolesUsuario = oUsuario.Roles;
                    bool puedeVerTodo = lRolesUsuario.Find(ru => ru.RolesId == ValoresUtiles.IdRol_AdminFull) != null ||
                        lRolesUsuario.Find(ru => ru.RolesId == ValoresUtiles.IdRol_DireccionEmpresa) != null;
                    if (puedeVerTodo)
                    {
                        lNovedades = new ParteDiario_IncidentesNegocio()
                            .Get_x_Periodo(oFiltro.FecDesde, oFiltro.FecHasta);
                    }
                    else if (lRolesUsuario.Find(ru => ru.RolesId == ValoresUtiles.IdRol_SeguridadHigiene) != null)
                    {
                        lNovedades = new ParteDiario_IncidentesNegocio()
                            .Get_x_Periodo(oFiltro.FecDesde, oFiltro.FecHasta)
                            .Where(n => n.IncidenteId == ValoresUtiles.IdIncidente_SeguridadHigiene_Nov)
                            .ToList();
                    }
                    lNovedades = lNovedades
                        .OrderByDescending(n => n.ParteDiario.Fecha_Creacion)
                        .ToList();
                    oResult = new
                    {
                        lNovedades
                    };
                }
                else
                {
                    oResult = new
                    {
                        error = true,
                        message = "Acceso denegado."
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    error = true,
                    message = "Error: IncidentesController: _NovedadesGrilla(FiltroPeriodoFechas): exception.",
                    ex = ex.InnerException
                };
            }
            return PartialView(oResult);
        }

        [HttpPost]
        public ReturnData IncidentesHistorialGrilla(FiltroPantallaGenerico pFiltro)
        {
            ReturnData d = new ReturnData();

            if (pFiltro != null)
            {
                d.data = CargoIncidentes(Convert.ToInt32(pFiltro.mfd), pFiltro.mfh, pFiltro.strFiltroTexto1);
            }
            else
            {
                d.isError = true;
                d.data = "Se produjo un error al buscar los Incidentes";

            }
            return d;
        }

        public ActionResult _IncidentesHistorialArchivos(int id)
        {
            List<Archivos_Adjuntos_Relacion> lAARelacion = new Archivos_Adjuntos_RelacionNegocio()
                .Get_Archivos_Adjuntos_Relacion("AIH", id)
                .OrderByDescending(aar => aar.Archivos_Adjuntos.Extencion)
                .ToList();
            return PartialView(lAARelacion);
        }

        public ActionResult _IncidentesHistorialArchivos_JPEG(int id)
        {
            List<Archivos_Adjuntos_Relacion> lAARelacion = new Archivos_Adjuntos_RelacionNegocio()
                .Get_Archivos_Adjuntos_Relacion("JPEG", id)
                .OrderByDescending(aar => aar.Archivos_Adjuntos.Extencion)
                .ToList();
            return PartialView(lAARelacion);
        }

        public ActionResult _NovedadesArchivos(int id)
        {
            List<Archivos_Adjuntos_Relacion> lAARelacion = new Archivos_Adjuntos_RelacionNegocio()
                .Get_Archivos_Adjuntos_Relacion("PINC", id)
                .OrderByDescending(aar => aar.Archivos_Adjuntos.Extencion)
                .ToList();
            return PartialView(lAARelacion);
        }
        private List<Incidentes_Historial> CargoIncidentes(int pDias, string pEstado, string pVisualizacion)
        {
            List<Incidentes_Historial> lIncHist = new List<Incidentes_Historial>();
            int mDias = pDias;
            DateTime mF = DateTime.Now.AddDays(-pDias);
            if (pDias == 0) mF = DateTime.MinValue;
            DateTime mFD = new DateTime(mF.Year, mF.Month, mF.Day, 0, 0, 0);

            #region 1 - Lista de Áreas a las que pertenece el usuario logueado
            int mIdUsuario = getUsuarioActual().Id;
            Usuarios oUsuario = new UsuariosNegocio().Get_Usuario(mIdUsuario);
            List<RolesUsuarios> lstRolesUsuario = oUsuario.Roles;
            List<Roles> lRoles_UsuarioLogueado = new List<Roles>();
            bool mMuestraTodo = false;
            foreach (var item in lstRolesUsuario)
            {
                Roles oRol = new RolesNegocio().Get_One_Roles(item.RolesId);
                if (oRol.Id == ValoresUtiles.IdRol_AdminFull || oRol.Id == ValoresUtiles.IdRol_DireccionEmpresa)
                    mMuestraTodo = true;
                lRoles_UsuarioLogueado.Add(oRol);
            }
            #endregion

            List<Incidentes_Historial> lIncHistAux = new Incidentes_HistorialNegocio()
                .Get_All_Incidentes_Historial()
                .Where(ih => (ih.EstadoId != ValoresUtiles.IdEstado_IncHist_Cerrado &&
                    ih.EstadoId != ValoresUtiles.IdEstado_IncHist_Cancelado) ||
                    (ih.Creacion_Fecha >= mFD && (ih.EstadoId == 50 || ih.EstadoId == 99)))
                .OrderByDescending(p => p.Creacion_Fecha)
                .ToList();

            List<int> lIdsIncidentes = lIncHistAux
                .Where(ih => ih.IncidenteId > 0)
                .GroupBy(ih => ih.IncidenteId)
                .Select(g => g.First().IncidenteId)
                .ToList();
            List<Incidentes> lIncidentes = new IncidentesNegocio()
                .Get_By_lIds(lIdsIncidentes);
            List<int> lIdsProyectos = lIncHistAux
                .Where(ih => ih.ProyectoId > 0)
                .GroupBy(ih => ih.ProyectoId)
                .Select(g => g.First().ProyectoId)
                .ToList();
            List<Proyecto> lProyectos = new ProyectoNegocio()
                .Get_By_lIds(lIdsProyectos);
            List<int> lIdsContratistas = lIncHistAux
                .Where(ih => ih.ContratistasId > 0)
                .GroupBy(ih => ih.ContratistasId)
                .Select(g => g.First().ContratistasId)
                .ToList();
            List<Contratistas> lContratistas = new ContratistasNegocio()
                .Get_x_lIds(lIdsContratistas);
            List<int> lIdsRoles = lIncHistAux
                .Where(ih => ih.AreaId > 0)
                .GroupBy(ih => ih.AreaId)
                .Select(g => g.First().AreaId)
                .ToList();
            List<Roles> lRoles = new RolesNegocio().Get_By_lIds(lIdsRoles);
            List<int> lIdsIncidentesHistorial = lIncHistAux
                .Select(ih => ih.Id)
                .ToList();
            List<IncidentesHistorial_Detalle> lIHDetalles = new IncidenteHistorial_DetalleNegocio()
                .Get_By_lIds(lIdsIncidentesHistorial);
            List<KeyValuePair<int, string>> lEstados = ValoresUtiles.Get_lEstados_IncidenteHistorial();

            foreach (var oIncidenteHistorial in lIncHistAux)
            {
                oIncidenteHistorial.Visualiza_AS = "N";
                oIncidenteHistorial.Visualiza_CA = "N";
                oIncidenteHistorial.Visualiza_CR = "N";
                oIncidenteHistorial.Visualiza_PA = "N";
                oIncidenteHistorial.Visualiza_SUP = "N";

                //Califico los Incidentes
                int mCant = 0;
                //Supervisor
                if (mMuestraTodo)
                {
                    oIncidenteHistorial.Visualiza_SUP = "S";
                }

                //Creados Por mi
                if (oIncidenteHistorial.Creacion_UsuarioId == mIdUsuario)
                {
                    mCant++;
                    oIncidenteHistorial.Visualiza_CR = "S";
                    oIncidenteHistorial.Visualiza_CA = "S";
                }

                //Verifico si este Incidente esta asignado a algun perfil del usuario logueado
                //Pero el parametro de visualizacion tiene que ser distinto a SOLO CREADO POR MI
                if (pVisualizacion != "CR")
                {
                    //Verifico si el areaAsignada es JEFE DE OBRA == 6 - Si es asi tengo que verificar que el PRoyecto Sea el del Jefe de Obra logueado
                    if (oIncidenteHistorial.AreaId == 6)
                    {
                        //VErifico que Proyecto es
                        Proyecto oP = lProyectos.Find(p => p.Id == oIncidenteHistorial.ProyectoId);
                        if (oP.UsuariosId == mIdUsuario)
                        {
                            mCant++;
                            oIncidenteHistorial.Visualiza_CA = "S";
                            oIncidenteHistorial.Visualiza_AS = "S";
                        }
                    }
                    else
                    {
                        var mCant2 = lRoles_UsuarioLogueado.Where(p => p.Id == oIncidenteHistorial.AreaId).Count();
                        if (mCant2 > 0)
                        {
                            mCant++;
                            oIncidenteHistorial.Visualiza_CA = "S";
                            oIncidenteHistorial.Visualiza_AS = "S";
                        }
                    }
                }
                List<IncidentesHistorial_Detalle> lstDetalle = lIHDetalles
                    .Where(p => p.IdIncidente_Historial == oIncidenteHistorial.Id &&
                        p.Modificacion_Usuario == oUsuario.ApellidoYNombre)
                    .ToList();
                if (lstDetalle.Count > 0)
                {
                    mCant++;
                    oIncidenteHistorial.Visualiza_PA = "S";
                }

                //Si tengo que mostrar en los que participe
                if (oIncidenteHistorial.Creacion_Descripcion != null)
                    oIncidenteHistorial.Creacion_Descripcion = oIncidenteHistorial.Creacion_Descripcion.Replace(@"\", "/");

                if (oIncidenteHistorial.sDetalleTratamiento != null)
                    oIncidenteHistorial.sDetalleTratamiento = oIncidenteHistorial.sDetalleTratamiento.Replace(@"\", "/");

                if (mCant > 0 || mMuestraTodo)

                {
                    #region Cargo los String de PRoyecto, Tipo Inciente, AreaAsiganada actual
                    Incidentes_Historial ih = new Incidentes_Historial();
                    ih = oIncidenteHistorial;
                    if (oIncidenteHistorial.ContratistasId > 0)
                        ih.sContratista = lContratistas.Find(c => c.Id == oIncidenteHistorial.ContratistasId).Nombre;
                    else
                        ih.sContratista = "";

                    if (oIncidenteHistorial.ProyectoId > 0)
                        ih.sProyecto = lProyectos.Find(p => p.Id == oIncidenteHistorial.ProyectoId).Nombre;
                    else
                        ih.sProyecto = "";

                    if (oIncidenteHistorial.IncidenteId > 0)

                        if (lIncidentes.Find(i => i.Id == oIncidenteHistorial.IncidenteId) != null)
                        {
                            ih.sIncidente = lIncidentes.Find(i => i.Id == oIncidenteHistorial.IncidenteId).Nombre;
                        }
                        else
                        {
                            ih.sIncidente = "";
                        }
                       
                    else
                        ih.sIncidente = "";

                    if (oIncidenteHistorial.AreaId > 0)
                        ih.sAreaActual = lRoles.Find(r => r.Id == oIncidenteHistorial.AreaId).Detalle;
                    else
                        ih.sAreaActual = "";

                    Usuarios oUsuarioCreacion = new UsuariosNegocio().Get_Usuario(oIncidenteHistorial.Creacion_UsuarioId);

                    if (oIncidenteHistorial.EstadoId == 4)
                    {
                        //Como esta en estado 4 pongo como Asignado a == al titular
                        oIncidenteHistorial.sAreaActual = oUsuarioCreacion.Apellido + ", " + oUsuarioCreacion.Nombre;
                    }
                    #endregion

                    #region Cargo el String con el nombre del estado
                    //Estado
                    ih.sEstado = lEstados.Find(e => e.Key == ih.EstadoId).Value;
                    #endregion

                    #region Defino el color del fondo de la grilla
                    //Azul si soy el Dueño
                    if (oIncidenteHistorial.Creacion_UsuarioId == mIdUsuario)
                        oIncidenteHistorial.sColorFondo = "Z";


                    //Verifico el Vencimiento
                    DateTime mFHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    DateTime mFFut = new DateTime(mFHoy.Year, mFHoy.Month, mFHoy.AddDays(2).Day, 0, 0, 0);

                    //Amarillo si esta proximo a vencerce 48 hrs
                    if (oIncidenteHistorial.Fecha_Cierre <= mFFut)
                        oIncidenteHistorial.sColorFondo = "A";
                    //Rojo si esta Vencido
                    if (oIncidenteHistorial.Fecha_Cierre < mFHoy)
                        oIncidenteHistorial.sColorFondo = "R";

                    //Defino si esta cerrado o anulado
                    if (oIncidenteHistorial.EstadoId == 50)
                        oIncidenteHistorial.sColorFondo = "V";
                    if (oIncidenteHistorial.EstadoId == 99)
                        oIncidenteHistorial.sColorFondo = "G";
                    #endregion

                    #region cargo el Nombre del Dueño
                    ih.sUsuarioDueño = oUsuarioCreacion.ApellidoYNombre;
                    #endregion

                    #region Saco el Ultimo Comentario que se incluyo

                    IncidentesHistorial_Detalle oIHD = lIHDetalles
                        .Where(p => p.IdIncidente_Historial == oIncidenteHistorial.Id)
                        .OrderByDescending(p => p.Id)
                        .FirstOrDefault();
                    if (oIHD != null)
                        ih.sUltimoComentarioAgregado = oIHD.Detalle + "(" + oIHD.Modificacion_Usuario + ")";
                    else
                    {
                        ih.sUltimoComentarioAgregado = "S/D";
                    }

                    #endregion

                    lIncHist.Add(ih);
                }

            }
            lIncHist = lIncHist.OrderByDescending(p => p.Id).ToList();
            return lIncHist;
        }

        private List<Incidentes_Historial> CargoIncidentes(DateTime fecDesde, DateTime fecHasta, string pEstado, string pVisualizacion)
        {
            List<Incidentes_Historial> lIncHist = new List<Incidentes_Historial>();

            #region 1 - Lista de Áreas a las que pertenece el usuario logueado
            int mIdUsuario = getUsuarioActual().Id;
            Usuarios oUsuario = new UsuariosNegocio().Get_Usuario(mIdUsuario);
            List<RolesUsuarios> lstRolesUsuario = oUsuario.Roles;
            List<Roles> lRoles_UsuarioLogueado = new List<Roles>();
            bool mMuestraTodo = false;
            foreach (var item in lstRolesUsuario)
            {
                Roles oRol = new RolesNegocio().Get_One_Roles(item.RolesId);
                if (oRol.Id == ValoresUtiles.IdRol_AdminFull || oRol.Id == ValoresUtiles.IdRol_DireccionEmpresa)
                    mMuestraTodo = true;
                lRoles_UsuarioLogueado.Add(oRol);
            }
            #endregion
            fecHasta = fecHasta.AddDays(1).AddTicks(-1);
            List<Incidentes_Historial> lIncHistAux = new Incidentes_HistorialNegocio()
                .Get_All_Incidentes_Historial()
                .Where(ih => (ih.EstadoId != ValoresUtiles.IdEstado_IncHist_Cerrado &&
                    ih.EstadoId != ValoresUtiles.IdEstado_IncHist_Cancelado) ||
                    (ih.Creacion_Fecha >= fecDesde &&
                    ih.Creacion_Fecha <= fecHasta &&
                    (ih.EstadoId == 50 || ih.EstadoId == 99)))
                .OrderByDescending(p => p.Creacion_Fecha)
                .ToList();

            // PREGUNTAMOS SI ES SOLO ROL DE SEGURIDAD E HIGIENE
            if (lRoles_UsuarioLogueado.Find(ru => ru.Id == ValoresUtiles.IdRol_AdminFull) == null &&
                lRoles_UsuarioLogueado.Find(ru => ru.Id == ValoresUtiles.IdRol_DireccionEmpresa) == null &&
                lRoles_UsuarioLogueado.Find(ru => ru.Id == ValoresUtiles.IdRol_SeguridadHigiene) != null)
            {
                lIncHistAux = lIncHistAux
                    .Where(ih => ih.IncidenteId == ValoresUtiles.IdIncidente_SeguridadHigiene_Inc_62 ||
                    ih.IncidenteId == ValoresUtiles.IdIncidente_SeguridadHigiene_Inc_64)
                    .ToList();
            }

            List<int> lIdsIncidentes = lIncHistAux
                .Where(ih => ih.IncidenteId > 0)
                .GroupBy(ih => ih.IncidenteId)
                .Select(g => g.First().IncidenteId)
                .ToList();
            List<Incidentes> lIncidentes = new IncidentesNegocio()
                .Get_By_lIds(lIdsIncidentes);
            List<int> lIdsProyectos = lIncHistAux
                .Where(ih => ih.ProyectoId > 0)
                .GroupBy(ih => ih.ProyectoId)
                .Select(g => g.First().ProyectoId)
                .ToList();
            List<Proyecto> lProyectos = new ProyectoNegocio()
                .Get_By_lIds(lIdsProyectos);
            List<int> lIdsContratistas = lIncHistAux
                .Where(ih => ih.ContratistasId > 0)
                .GroupBy(ih => ih.ContratistasId)
                .Select(g => g.First().ContratistasId)
                .ToList();
            List<Contratistas> lContratistas = new ContratistasNegocio()
                .Get_x_lIds(lIdsContratistas);
            List<int> lIdsRoles = lIncHistAux
                .Where(ih => ih.AreaId > 0)
                .GroupBy(ih => ih.AreaId)
                .Select(g => g.First().AreaId)
                .ToList();
            List<Roles> lRoles = new RolesNegocio().Get_By_lIds(lIdsRoles);
            List<int> lIdsIncidentesHistorial = lIncHistAux
                .Select(ih => ih.Id)
                .ToList();
            List<IncidentesHistorial_Detalle> lIHDetalles = new IncidenteHistorial_DetalleNegocio()
                .Get_By_lIds(lIdsIncidentesHistorial);
            List<KeyValuePair<int, string>> lEstados = ValoresUtiles.Get_lEstados_IncidenteHistorial();

            foreach (var oIncidenteHistorial in lIncHistAux)
            {
                oIncidenteHistorial.Visualiza_AS = "N";
                oIncidenteHistorial.Visualiza_CA = "N";
                oIncidenteHistorial.Visualiza_CR = "N";
                oIncidenteHistorial.Visualiza_PA = "N";
                oIncidenteHistorial.Visualiza_SUP = "N";

                //Califico los Incidentes
                int mCant = 0;
                //Supervisor
                if (mMuestraTodo)
                {
                    oIncidenteHistorial.Visualiza_SUP = "S";
                }

                //Creados Por mi
                if (oIncidenteHistorial.Creacion_UsuarioId == mIdUsuario)
                {
                    mCant++;
                    oIncidenteHistorial.Visualiza_CR = "S";
                    oIncidenteHistorial.Visualiza_CA = "S";
                }

                //Verifico si este Incidente esta asignado a algun perfil del usuario logueado
                //Pero el parametro de visualizacion tiene que ser distinto a SOLO CREADO POR MI
                if (pVisualizacion != "CR")
                {
                    //Verifico si el areaAsignada es JEFE DE OBRA == 6 - Si es asi tengo que verificar que el PRoyecto Sea el del Jefe de Obra logueado
                    if (oIncidenteHistorial.AreaId == 6)
                    {
                        //VErifico que Proyecto es
                        Proyecto oP = lProyectos.Find(p => p.Id == oIncidenteHistorial.ProyectoId);
                        if (oP.UsuariosId == mIdUsuario)
                        {
                            mCant++;
                            oIncidenteHistorial.Visualiza_CA = "S";
                            oIncidenteHistorial.Visualiza_AS = "S";
                        }
                    }
                    else
                    {
                        var mCant2 = lRoles_UsuarioLogueado.Where(p => p.Id == oIncidenteHistorial.AreaId).Count();
                        if (mCant2 > 0)
                        {
                            mCant++;
                            oIncidenteHistorial.Visualiza_CA = "S";
                            oIncidenteHistorial.Visualiza_AS = "S";
                        }
                    }
                }
                List<IncidentesHistorial_Detalle> lstDetalle = lIHDetalles
                    .Where(p => p.IdIncidente_Historial == oIncidenteHistorial.Id &&
                        p.Modificacion_Usuario == oUsuario.ApellidoYNombre)
                    .ToList();
                if (lstDetalle.Count > 0)
                {
                    mCant++;
                    oIncidenteHistorial.Visualiza_PA = "S";
                }

                //Si tengo que mostrar en los que participe
                if (oIncidenteHistorial.Creacion_Descripcion != null)
                    oIncidenteHistorial.Creacion_Descripcion = oIncidenteHistorial.Creacion_Descripcion.Replace(@"\", "/");

                if (oIncidenteHistorial.sDetalleTratamiento != null)
                    oIncidenteHistorial.sDetalleTratamiento = oIncidenteHistorial.sDetalleTratamiento.Replace(@"\", "/");

                if (mCant > 0 || mMuestraTodo)

                {
                    #region Cargo los String de PRoyecto, Tipo Inciente, AreaAsiganada actual
                    Incidentes_Historial ih = new Incidentes_Historial();
                    ih = oIncidenteHistorial;
                    if (oIncidenteHistorial.ContratistasId > 0)
                        ih.sContratista = lContratistas.Find(c => c.Id == oIncidenteHistorial.ContratistasId).Nombre;
                    else
                        ih.sContratista = "";

                    if (oIncidenteHistorial.ProyectoId > 0)
                        ih.sProyecto = lProyectos.Find(p => p.Id == oIncidenteHistorial.ProyectoId).Nombre;
                    else
                        ih.sProyecto = "";

                    if (oIncidenteHistorial.IncidenteId > 0)
                        ih.sIncidente = lIncidentes.Find(i => i.Id == oIncidenteHistorial.IncidenteId).Nombre;
                    else
                        ih.sIncidente = "";

                    if (oIncidenteHistorial.AreaId > 0)
                        ih.sAreaActual = lRoles.Find(r => r.Id == oIncidenteHistorial.AreaId).Detalle;
                    else
                        ih.sAreaActual = "";

                    Usuarios oUsuarioCreacion = new UsuariosNegocio().Get_Usuario(oIncidenteHistorial.Creacion_UsuarioId);

                    if (oIncidenteHistorial.EstadoId == 4)
                    {
                        //Como esta en estado 4 pongo como Asignado a == al titular
                        oIncidenteHistorial.sAreaActual = oUsuarioCreacion.Apellido + ", " + oUsuarioCreacion.Nombre;
                    }
                    #endregion

                    #region Cargo el String con el nombre del estado
                    //Estado
                    ih.sEstado = lEstados.Find(e => e.Key == ih.EstadoId).Value;
                    #endregion

                    #region Defino el color del fondo de la grilla
                    //Azul si soy el Dueño
                    if (oIncidenteHistorial.Creacion_UsuarioId == mIdUsuario)
                        oIncidenteHistorial.sColorFondo = "Z";


                    //Verifico el Vencimiento
                    DateTime mFHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    DateTime mFFut = new DateTime(mFHoy.Year, mFHoy.Month, mFHoy.AddDays(2).Day, 0, 0, 0);

                    //Amarillo si esta proximo a vencerce 48 hrs
                    if (oIncidenteHistorial.Fecha_Cierre <= mFFut)
                        oIncidenteHistorial.sColorFondo = "A";
                    //Rojo si esta Vencido
                    if (oIncidenteHistorial.Fecha_Cierre < mFHoy)
                        oIncidenteHistorial.sColorFondo = "R";

                    //Defino si esta cerrado o anulado
                    if (oIncidenteHistorial.EstadoId == 50)
                        oIncidenteHistorial.sColorFondo = "V";
                    if (oIncidenteHistorial.EstadoId == 99)
                        oIncidenteHistorial.sColorFondo = "G";
                    #endregion

                    #region cargo el Nombre del Dueño
                    ih.sUsuarioDueño = oUsuarioCreacion.ApellidoYNombre;
                    #endregion

                    #region Saco el Ultimo Comentario que se incluyo

                    IncidentesHistorial_Detalle oIHD = lIHDetalles
                        .Where(p => p.IdIncidente_Historial == oIncidenteHistorial.Id)
                        .OrderByDescending(p => p.Id)
                        .FirstOrDefault();
                    if (oIHD != null)
                        ih.sUltimoComentarioAgregado = oIHD.Detalle + "(" + oIHD.Modificacion_Usuario + ")";
                    else
                    {
                        ih.sUltimoComentarioAgregado = "S/D";
                    }

                    #endregion

                    lIncHist.Add(ih);
                }

            }
            lIncHist = lIncHist.OrderByDescending(p => p.Id).ToList();

           
            return lIncHist;
        }

        public ActionResult _IncidentesHistorialAbm(int id)
        {
            ItemIncidentesHistorial data = new ItemIncidentesHistorial();
            if (id != 0)
            {
                data.ih = new Incidentes_HistorialNegocio().Get_One_Incidentes_Historial(id);
                Usuarios oUsu = new UsuariosNegocio().Get_Usuario(data.ih.Creacion_UsuarioId);
                if (data.ih.EstadoId == 4 && data.ih.EstadoId == 50)
                {
                    //Como esta en estado 4 pongo como Asignado a == al titular

                    data.ih.sAreaActual = oUsu.ApellidoYNombre;
                }
                data.ih.sUsuarioDueño = oUsu.ApellidoYNombre;
            }
            else
            {
                data.ih = new Incidentes_Historial();
            }
            data.Proyecto = new ProyectoNegocio()
                .Get_All_Proyecto().OrderBy(p => p.Nombre)
                .ToList();
            data.Contratistas = new ContratistasNegocio()
                .Get_All_Contratistas().OrderBy(p => p.Nombre)
                .ToList();
            data.Incidentes = new IncidentesNegocio()
                .Get_All_Incidentes().OrderBy(p => p.Nombre)
                .ToList();
            data.Usuarios = new UsuariosNegocio()
                .Get_Usuarios()
                .OrderBy(p => p.ApellidoYNombre)
                .ToList();
            data.lstRoles = new RolesNegocio()
                .Get_All_Roles()
                .Where(p => p.Borrado == false && p.Activo == true && p.Detalle != "Admin Full")
                .OrderBy(p => p.Detalle)
                .ToList();
            data.lstHistorialDetalle = new IncidenteHistorial_DetalleNegocio()
                .Get_All_Incidentes_Historial()
                .Where(p => p.IdIncidente_Historial == id)
                .OrderBy(p => p.Asignacion_Fecha)
                .ToList();
            data.mUsuarioActual = getUsuarioActual().ApellidoNombre;
            data.mIdUsuarioActual = getUsuarioActual().Id;
            data.lArchivosAdjuntos = new Archivos_Adjuntos_RelacionNegocio()
                .Get_Archivos_Adjuntos_Relacion("AIH", id);
            return PartialView(data);
        }

        public ItemLoginUsuario getUsuarioActual()
        {
            ItemLoginUsuario d = new ItemLoginUsuario();
            string[] us = (HttpContext.User.Identity.Name).Split("#");

            d.Id = Convert.ToInt32(us[0]);
            d.Nombre = us[1];
            d.Apellido = us[2];
            d.Email = us[3];
            d.Tipo = us[4];
            d.ApellidoNombre = us[2] + ", " + us[1];
            d.Grupo = Convert.ToInt32(us[5]);

            return d;
        }

        [HttpPost]
        public ReturnData IncidentesHistorialGraba(Incidentes_Historial data)
        {
            ReturnData d = new ReturnData();
            if (data != null)
            {
                if (data.sDetalleTratamiento == "undefined")
                    data.sDetalleTratamiento = "";
                //Valido que el registro que viene este con los campos correctos
                d = this.ValidarIncidenteABM(data);
                if (d.isError == true)
                {
                    return d;
                }
                // Depuro los campos
                data.Creacion_Descripcion = ToolsNegocio.EliminaEnter(data.Creacion_Descripcion);
                int IdIncidenteHistorial;
                if (data.Id > 0)
                {
                    Incidentes_Historial oIncidente = new Incidentes_HistorialNegocio()
                        .Get_One_Incidentes_Historial(data.Id);
                    data.Creacion_UsuarioId = oIncidente.Creacion_UsuarioId;
                    data.Creacion_Fecha = oIncidente.Creacion_Fecha;
                    data.ParteDiarioId = oIncidente.ParteDiarioId;
                    IdIncidenteHistorial = new Incidentes_HistorialNegocio().Update(data);
                    this.GrabarIncidenteHistorial_Detalle(data);
                    this.EnvioMailIncidente(data, false);
                    if (data.lIdsArchivosAdjuntos != null)
                    {
                        data.lIdsArchivosAdjuntos.ForEach(id =>
                        {
                            Archivos_Adjuntos_Relacion oAARelacion = new Archivos_Adjuntos_Relacion();
                            oAARelacion.Archivos_AdjuntosId = id;
                            oAARelacion.Entidad = "AIH";
                            oAARelacion.IdEntidad = IdIncidenteHistorial;
                            new Archivos_Adjuntos_RelacionNegocio().Insert(oAARelacion);
                        //Movemos el archivo adjunto de la carpeta temporal a la carpeta que necesitamos
                        Archivos_Adjuntos oArchivoAdjunto = new Archivos_AdjuntosNegocio()
                                .Get_One_Archivos_Adjuntos(id);
                            string pathActual = Path.Combine(_env.WebRootPath, oArchivoAdjunto.URL + oArchivoAdjunto.Archivo);
                            string pathNuevo = Path.Combine(_env.WebRootPath, ValoresUtiles.PathArchivos_Incidentes + oArchivoAdjunto.Archivo);
                        //bool existeArchivo = System.IO.File.Exists(pathActual);
                        if (true)
                            {
                                bool archivoMovido = false;
                                try
                                {
                                    System.IO.File.Copy(pathActual, pathNuevo, true);
                                    archivoMovido = true;
                                }
                                catch (Exception ex)
                                {
                                    var a = ex.InnerException;
                                    archivoMovido = false;
                                }
                                if (archivoMovido)
                                {
                                    oArchivoAdjunto.URL = ValoresUtiles.PathArchivos_Incidentes;
                                    new Archivos_AdjuntosNegocio().Update(oArchivoAdjunto);
                                }
                            }
                        });
                    }
                }
                else
                {
                    IdIncidenteHistorial = new Incidentes_HistorialNegocio().Insert(data);
                    data.Id = IdIncidenteHistorial;
                    this.GrabarIncidenteHistorial_Detalle(data);
                    this.EnvioMailIncidente(data, true);
                    if (data.lIdsArchivosAdjuntos != null)
                    {
                        data.lIdsArchivosAdjuntos.ForEach(id =>
                    {
                        Archivos_Adjuntos_Relacion oAARelacion = new Archivos_Adjuntos_Relacion();
                        oAARelacion.Archivos_AdjuntosId = id;
                        oAARelacion.Entidad = "AIH";
                        oAARelacion.IdEntidad = IdIncidenteHistorial;
                        new Archivos_Adjuntos_RelacionNegocio().Insert(oAARelacion);
                        //Movemos el archivo adjunto de la carpeta temporal a la carpeta que necesitamos
                        Archivos_Adjuntos oArchivoAdjunto = new Archivos_AdjuntosNegocio()
                            .Get_One_Archivos_Adjuntos(id);
                        string pathActual = Path.Combine(_env.WebRootPath, oArchivoAdjunto.URL + oArchivoAdjunto.Archivo);
                        string pathNuevo = Path.Combine(_env.WebRootPath, ValoresUtiles.PathArchivos_Incidentes + oArchivoAdjunto.Archivo);
                        //bool existeArchivo = System.IO.File.Exists(pathActual);
                        if (true)
                        {
                            bool archivoMovido = false;
                            try
                            {
                                System.IO.File.Copy(pathActual, pathNuevo, true);
                                archivoMovido = true;
                            }
                            catch (Exception ex)
                            {
                                var a = ex.InnerException;
                                archivoMovido = false;
                            }
                            if (archivoMovido)
                            {
                                oArchivoAdjunto.URL = ValoresUtiles.PathArchivos_Incidentes;
                                new Archivos_AdjuntosNegocio().Update(oArchivoAdjunto);
                            }
                        }
                    });
                    }
                    
                    //08/08/2022 - PBERTI - Cambio para que cargue automaticamente los Incidentes, reutilizando la funcion ya existente
                    List<Incidentes_Historial> ListaRet = CargoIncidentes(DateTime.Now.AddMonths(-1), DateTime.Now, "0", "CA");
                    //List<Incidentes_Historial> ListaRet = new List<Incidentes_Historial>();
                    // List<Incidentes_Historial> hin = new Incidentes_HistorialNegocio().Get_All_Incidentes_Historial();
                    //foreach (var e in hin)
                    //{
                    //    Incidentes_Historial hi = new Incidentes_Historial();
                    //    hi = e;
                    //    if (e.ContratistasId != 0)
                    //        hi.sContratista = new ContratistasNegocio().Get_One_Contratistas(e.ContratistasId).Nombre;
                    //    else
                    //        hi.sContratista = "";
                    //    if (e.ProyectoId != 0)
                    //        hi.sProyecto = new ProyectoNegocio().Get_One_Proyecto(e.ProyectoId).Nombre;
                    //    else
                    //    {
                    //        hi.sProyecto = "";
                    //    }
                    //    hi.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;

                    //    if (e.AreaId != 0)
                    //        hi.sAreaActual = new RolesNegocio().Get_One_Roles(e.AreaId).Detalle;
                    //    else
                    //    {
                    //        hi.sAreaActual = "";
                    //    }

                    //    if (e.IncidenteId != 0)
                    //        hi.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;
                    //    else
                    //    {
                    //        hi.sIncidente = "ERROR - Sin Incidente";
                    //    }

                    //    ListaRet.Add(hi);
                    //}
                    //08/08/2022 - FIN
                    d.data = "Grabado ok";
                    d.data1 = ListaRet;
                    d.isError = false;
                }
                if (IdIncidenteHistorial > 0)
                {
                    d.isError = false;
                    d.data = "Se han grabado los datos OK.";
                }
                else
                {
                    d.isError = true;
                    d.data = "Error al Grabar en la base.";
                }
            }
            else
            {
                d.isError = true;
                d.data = "Error al Grabar";
            }
            return d;
        }

        [HttpGet]
        public ReturnData IncidentesHistorialArchivos2(int id)
        {
            ReturnData r = new ReturnData();
            List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio().Get_Archivos_Adjuntos_Relacion("PD", id);
            r.data = Laar;
            r.isError = false;

            return r;
        }

        private void GrabarIncidenteHistorial_Detalle(Incidentes_Historial data)
        {
            IncidentesHistorial_Detalle oIHDetalle = new IncidentesHistorial_Detalle();
            try
            {
                oIHDetalle.Asignacion_Area = data.sAreaActual;
                oIHDetalle.Asignacion_Fecha = DateTime.Now;
                oIHDetalle.Asignacion_IdArea = data.AreaId;
                if (data.EstadoId == 1)
                {
                    data.sDetalleTratamiento = data.Creacion_Descripcion;
                }
                if (data.sDetalleTratamiento != null)
                    oIHDetalle.Detalle = ToolsNegocio.EliminaEnter(data.sDetalleTratamiento);
                else
                    oIHDetalle.Detalle = "";
                oIHDetalle.Estado = data.sEstado;
                oIHDetalle.IdIncidente_Historial = data.Id;

                #region Busco el Usuario
                int mIdUsuario = getUsuarioActual().Id;
                oIHDetalle.Modificacion_Usuario = new UsuariosNegocio()
                    .Get_Usuario(mIdUsuario).ApellidoYNombre;
                #endregion

                #region Busco el Estado
                switch (data.EstadoId)
                {
                    case 1:
                        oIHDetalle.Estado = "Abierto";
                        break;
                    case 2:
                        oIHDetalle.Estado = "Tratamiento";
                        break;
                    case 3:
                        oIHDetalle.Estado = "Enviado";
                        break;
                    case 4:
                        oIHDetalle.Estado = "Validacion";
                        break;
                    case 50:
                        oIHDetalle.Estado = "Cerrado";
                        break;
                    case 99:
                        oIHDetalle.Estado = "Cancelado";
                        break;
                    default:
                        oIHDetalle.Estado = "Cancelado";
                        break;
                }
                #endregion

                #region Busco el Nombre del Area
                if (data.AreaId != 0)
                    oIHDetalle.Asignacion_Area = new RolesNegocio().Get_One_Roles(data.AreaId).Detalle;
                else
                {
                    oIHDetalle.Asignacion_Area = "";
                }
                #endregion
                var idc = new IncidenteHistorial_DetalleNegocio().Insert(oIHDetalle);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: IncidentesController: GrabarIncidenteHistorial_Detalle(Incidentes_Historial): exception.", ex);
            }
        }

        private ReturnData ValidarIncidenteABM(Incidentes_Historial data)
        {
            ReturnData d = new ReturnData();
            #region Validar Descripcion
            if (data.Creacion_Descripcion == null)
            {
                d.isError = true;
                d.data = "No se ha especificado ninguna descricion";
                return d;
            }
            if (data.Creacion_Descripcion.Trim().Length == 0)
            {
                d.isError = true;
                d.data = "No se ha especificado ninguna descricion";
                return d;
            }
            #endregion

            #region Validar Area Asignada
            if (data.AreaId == 0)
            {
                if (data.EstadoId != 99 && data.EstadoId != 2 && data.EstadoId != 4 && data.EstadoId != 1 && data.EstadoId != 50 && data.EstadoId != 5)
                {
                    d.isError = true;
                    d.data = "Debe asignarse el incidente a un area especifica";
                    return d;
                }
            }
            #endregion

            #region Validar que tenga Tipo de Incidente
            if (data.IncidenteId == 0)
            {
                d.isError = true;
                d.data = "Debe seleccionar un tipo de incidente especifico";
                return d;
            }
            #endregion

            return d;
        }

        [HttpPost]
        public ReturnData MostrarArchivoIncidenteHistorial(int IdIncidenteHistorial)
        {
            ReturnData d = new ReturnData();
       
            try
            {
              Archivos_Adjuntos_Relacion archivoRelacion = new Archivos_Adjuntos_RelacionNegocio().Get_All_Archivos_Adjuntos_Relacion()
                    .Where(s=> s.IdEntidad == IdIncidenteHistorial).FirstOrDefault();
                
                if (archivoRelacion != null)
                {
                    //Creamos la URL de la Imagen.
                    Archivos_Adjuntos archivo = new Archivos_AdjuntosNegocio()
                                .Get_One_Archivos_Adjuntos(archivoRelacion.Archivos_AdjuntosId);
                    
                    d.data2 = "/"+archivo.URL + archivo.Archivo;

                    //Asi la enviamos: "/archivos/AIH/2021721_9336_9_03d873ac-07e6-4c41-b8b0-cd1cc5bc99f2.jpeg"
                }
                else
                {
                    //Devolvemos la URL vacia
                    d.data2 = "";
                    d.data ="No tiene un archivo asociado.";
                }
            }
            catch (Exception)
            {

                d.isError = true;
                d.data2 = "Hubo un problema para realizar la operación.";
            }
            return d;
        }


        #endregion
        #endregion

        #region Data Select



        [HttpGet]
        public ReturnData GetProyecto()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new ProyectoNegocio().Get_All_Proyecto()
                .Where(s=>s.Estado != "Ejecutado")
                .OrderBy(p => p.Nombre).ToList();

            return d;
        }

        [HttpGet]
        public ReturnData GetContratistas()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new ContratistasNegocio().Get_All_Contratistas().OrderBy(p => p.Nombre).ToList();

            return d;
        }

        [HttpGet]
        public ReturnData GetIncidentes()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new IncidentesNegocio().Get_All_Incidentes().OrderBy(p => p.Nombre).ToList();
            return d;
        }

        [HttpGet]
        public ReturnData GetAreasEmpresa()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new RolesNegocio().Get_All_Roles().Where(p => p.Borrado == false && p.Activo == true && p.Detalle != "Admin Full").OrderBy(p => p.Detalle).ToList();
            return d;
        }
        #endregion

        #region Envio de Mails de los Incidentes
        private void EnvioMailIncidente(Incidentes_Historial data, bool pEsNuevo)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            bool EnviaEmail = Convert.ToBoolean(configuration["EnviaEmail"]);

            if (EnviaEmail)
            {
                Incidentes oIncidente = new IncidentesNegocio().Get_One_Incidentes(data.IncidenteId);
                Incidentes_Historial oHistorialIncidentes = new Incidentes_HistorialNegocio().Get_One_Incidentes_Historial(data.Id);
                #region Destinatarios
                string lstDestinatarios = "";
                //PRIMERO AGREGO AL CREADOR DEL INCIDENTE
                Usuarios oUsuario = new UsuariosNegocio().Get_Usuario(data.Creacion_UsuarioId);
                if (oUsuario != null)
                {
                    string MailCreador = oUsuario.Email;
                    if (MailCreador.Trim() != "")
                    {
                        if (NoExisteYaElDestinatario(lstDestinatarios, MailCreador))
                            lstDestinatarios = MailCreador;
                    }
                }

                //Verifico si es un INCIDETENE que se esta asignando a UNA JEFE DE OBRA. Si es asi solo le envio el mail a el y no a los demas jefes de obra
                bool mSoloJefeObra = false;
                if ((data.AreaId == 6) && (data.ProyectoId > 0))
                {
                    //Como es para jefe de obra solo vamos a mandar el mail al jefe de obra que corresponde
                    //Busco el proyecto y su jefe de Obra
                    Proyecto oProyecto = new ProyectoNegocio().Get_One_Proyecto(data.ProyectoId);
                    string mEmailJefeObra = new UsuariosNegocio().Get_Usuario(oProyecto.UsuariosId).Email;
                    lstDestinatarios = lstDestinatarios + "; " + mEmailJefeObra;
                    mSoloJefeObra = true;
                }

                if (pEsNuevo)
                {
                    // Si es nuevo, tengo que agregar los destinatario que estan configurados en el tipo de incidentes
                    if (NoExisteYaElDestinatario(lstDestinatarios, data.IngresarEmail) && data.IngresarEmail != null)
                        lstDestinatarios = lstDestinatarios + "; " + data.IngresarEmail;
                }
                // Si es una modificacion, tengo que verificar si se le esta asignando a una Area en particular y traer los integrantes y enviarles el mail.
                if (data.AreaId != 0)
                {
                    if (mSoloJefeObra == false)
                    {
                        //Busco los usuarios que tiene esta area y armo el listado
                        List<Usuarios> lstUsuarios = new UsuariosNegocio().Get_UsuariosWithRoles().Where(p => p.Roles.Any(x => x.RolesId == data.AreaId)).ToList();
                        if (lstUsuarios != null)
                        {
                            foreach (var item in lstUsuarios)
                            {
                                if (!lstDestinatarios.Contains(item.Email))
                                {
                                    if (NoExisteYaElDestinatario(lstDestinatarios, item.Email))
                                        lstDestinatarios = lstDestinatarios + "; " + item.Email;
                                }
                            }
                        }
                    }
                }

                //lstDestinatarios = "sezbustamante@gmail.com";
                #endregion

                #region Busco el Usuario que esta modificando
                int mIdUsuario = getUsuarioActual().Id;
                string mUsuarioModifico = new UsuariosNegocio().Get_Usuario(mIdUsuario).ApellidoYNombre;

                #endregion

                #region Armo el Asunto del Mail
                var Asunto = "";
                if (pEsNuevo)
                    Asunto = "Se ha generado un nuevo incidente del tipo:" + oIncidente.Nombre + " creado por:" + oUsuario.NombreYApellido;
                else
                    Asunto = "El Incidente Nro:" + data.Id + " ha sido modificado por:" + mUsuarioModifico;
                #endregion

                var Texto = "";
                Texto = TextoHtmlMail(oIncidente.Nombre, data, oUsuario.NombreYApellido, pEsNuevo, mUsuarioModifico);

                EnviaEmailNegocio.Enviar(lstDestinatarios, Asunto, Texto, true);

            }
        }

        //Funcion que sirve para saber si un nuevo destinatario de mail ya existe en la lista de mails a enviar
        private bool NoExisteYaElDestinatario(string lstDestinatarios, string pDestinatario)
        {
            if (lstDestinatarios.Trim().Length > 0 && pDestinatario != null)
            {
                if (lstDestinatarios.Contains(pDestinatario))
                    return false;
                else
                    return true;
            }
            else
            {
                return true;
            }
        }
        private string TextoHtmlMail(string pTipoIncidente, Incidentes_Historial data, string pCreador, bool pEsNuevo, string pUsuarioModifico)
        {
            string mTexto = "";
            mTexto = "<p><H3>Informacion de referencia</H3></p>";
            mTexto = mTexto + "<p><hr/></p>";
            mTexto = mTexto + "<p><strong> Tipo de Incidente:</strong>" + pTipoIncidente + "</p>";
            mTexto = mTexto + "<p><strong> Creado Por:</strong>" + pCreador + "</p>";
            mTexto = mTexto + "<p><strong> Fecha Vencimiento:</strong>" + data.Fecha_Cierre.ToShortDateString() + "</p>";

            #region Busco el Estado
            string mEstado = "";
            switch (data.EstadoId)
            {
                case 1:
                    mEstado = "Abierto";
                    break;
                case 2:
                    mEstado = "Tratamiento";
                    break;
                case 3:
                    mEstado = "Enviado";
                    break;
                case 4:
                    mEstado = "Validacion";
                    break;
                case 50:
                    mEstado = "Cerrado";
                    break;
                case 99:
                    mEstado = "Cancelado";
                    break;
                default:
                    mEstado = "Cancelado";
                    break;
            }

            mTexto = mTexto + "<p><strong>Estado Actual:</strong>" + mEstado + "</p>";
            #endregion
            #region Proyecto Relacionado
            if (data.ProyectoId != 0)
            {
                string mNombre = new ProyectoNegocio().Get_One_Proyecto(data.ProyectoId).Nombre;
                mTexto = mTexto + "<p><strong> Proyecto Relacionado:</strong>" + mNombre + "</p>";
            }
            #endregion

            #region Contratista Relacionado
            if (data.ContratistasId != 0)
            {
                string mNombre = new ContratistasNegocio().Get_One_Contratistas(data.ContratistasId).Nombre;
                mTexto = mTexto + "<p><strong> Contratista Relacionado:</strong>" + mNombre + "</p>";
            }
            #endregion

            mTexto = mTexto + "<p><strong> Descripcion del Incidente:</strong>" + data.Creacion_Descripcion + "</p>";
            if (data.AreaId != 0)
            {
                string mNombre = new RolesNegocio().Get_One_Roles(data.AreaId).Detalle;
                mTexto = mTexto + "<p><strong> Area Asignada:</strong>" + mNombre + "</p>";
            }
            mTexto = mTexto + "<hr/>";
            if (data.sDetalleTratamiento != null)
            {
                mTexto = mTexto + "<p><strong>Comentarios del Tratamiento:</strong>" + data.sDetalleTratamiento + "</p>";
                mTexto = mTexto + "<p><strong>Actualizado Por:</strong>" + pUsuarioModifico + "</p>";
                mTexto = mTexto + "<hr/>";
            }
            mTexto = mTexto + "<br>";
            mTexto = mTexto + "<H2>Para ingresar al sistema por favor haga <a href='https://pucheta.iotecnologias.com.ar/'> click aqui </a><H2>";
            mTexto = mTexto + "<br>";
            mTexto = mTexto + "<br>";
            mTexto = mTexto + "<p>Muchas gracias Saludos</p>";
            return mTexto;
        }
        #endregion

        #region Incidentes Indicadores Home
        [HttpPost]
        public Res_IncidentesIndicadoresHome IncidentesIndicadoresHome()
        {
            Res_IncidentesIndicadoresHome d = new Res_IncidentesIndicadoresHome();

            List<Incidentes_Historial> lstIncidentes = CargoIncidentes(0, null, null);
            d.indCreados = lstIncidentes.Where(p => p.Visualiza_CR == "S").Count();
            d.indAsignados = lstIncidentes.Where(p => p.Visualiza_AS == "S").Count();
            d.indPorVencer = lstIncidentes.Where(p => p.sColorFondo == "A" && p.Visualiza_CA == "S").Count();
            d.indVencidos = lstIncidentes.Where(p => p.sColorFondo == "R" && p.Visualiza_CA == "S").Count();

            return d;
        }
        #endregion

    }



}