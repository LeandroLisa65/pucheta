using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestion.Web.Models;
using Gestion.EF.Models;
using Gestion.Negocio;
using Gestion.EF;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using static Gestion.Negocio.Planificacion_Proyecto_ActividadesNegocio;

namespace Gestion.Web.Controllers
{
    public class ParteDiarioController : Controller
    {

        private readonly IWebHostEnvironment _env;

        public ParteDiarioController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET: ParteDiario
        public ActionResult Index()
        {
            if(this.getUsuarioActual().Id == 1)
            {
                //this.CocinarAvancesPDAs();
            }
            return View();
        }

        #region Parte Diario
        public ActionResult ParteDiario()
        {
            return View();
        }

        public ActionResult _ParteDiarioGrilla()
        {
            List<ParteDiario> Lista = new List<ParteDiario>();
            Lista = new ParteDiarioNegocio().Get_All_ParteDiario().OrderByDescending(x => x.Fecha_Creacion).ToList();

            List<Res_PD> listRes_PD = new List<Res_PD>();
            foreach (var item in Lista)
            {
                Res_PD cPD = new Res_PD();
                cPD.Id = item.Id;
                cPD.ProyectoId = item.ProyectoId;
                cPD.ProyectoNombre = item.Proyecto.Nombre;
                cPD.Asig_Comentario = item.Asig_Comentario;
                cPD.Asig_Contratista = item.Asig_Contratista;
                cPD.Asig_Contratista_Presentes = item.Asig_Contratista_Presentes;
                cPD.Asig_Propios = item.Asig_Propios;
                cPD.Asig_Propios_Presentes = item.Asig_Propios_Presentes;
                cPD.Covid_Comentario = item.Covid_Comentario;
                cPD.Covid_Contratista = item.Covid_Contratista;
                cPD.Covid_Propioa = item.Covid_Propioa;
                if (item.Fecha_Creacion != null)
                {
                    cPD.Fecha_Creacion = item.Fecha_Creacion.ToString("dd/MM/yyyy");
                }
                else
                {
                    cPD.Fecha_Creacion = "";
                }
                listRes_PD.Add(cPD);
            }
            return PartialView(listRes_PD);
        }

        [HttpPost]
        public ReturnData ParteDiarioGrillaFiltro(ItemPDFiltro data)
        {
            ReturnData r = new ReturnData();
            List<ParteDiario> Lista = new List<ParteDiario>();
            Lista = new ParteDiarioNegocio().Get_All_ParteDiario()
                .Where(x => x.Fecha_Creacion >= data.fechaDesde && x.Fecha_Creacion <= data.fechaHasta)
                .OrderByDescending(x => x.Fecha_Creacion).ToList();

            if (data.proyecto != 0)
            {
                Lista = Lista.Where(x => x.ProyectoId == data.proyecto)
                    .OrderByDescending(x => x.Fecha_Creacion).ToList();
            }
            List<Res_PD> listRes_PD = new List<Res_PD>();
            foreach (var item in Lista)
            {
                Res_PD cPD = new Res_PD();
                cPD.Id = item.Id;
                cPD.ProyectoId = item.ProyectoId;
                cPD.ProyectoNombre = item.Proyecto.Nombre;
                cPD.Asig_Comentario = item.Asig_Comentario;
                cPD.Asig_Contratista = item.Asig_Contratista;
                cPD.Asig_Contratista_Presentes = item.Asig_Contratista_Presentes;
                cPD.Asig_Propios = item.Asig_Propios;
                cPD.Asig_Propios_Presentes = item.Asig_Propios_Presentes;
                cPD.Covid_Comentario = item.Covid_Comentario;
                cPD.Covid_Contratista = item.Covid_Contratista;
                cPD.Covid_Propioa = item.Covid_Propioa;
                if (item.Fecha_Creacion != null)
                {
                    cPD.Fecha_Creacion = item.Fecha_Creacion.ToString("dd/MM/yyyy");
                }
                else
                {
                    cPD.Fecha_Creacion = "";
                }
                listRes_PD.Add(cPD);
            }
            r.data = listRes_PD;
            r.isError = false;

            return r;
        }

        [HttpPost]
        public ReturnData ParteDiarioUbicacionFiltro(ItemUbicacioFiltro data)
        {
            ReturnData r = new ReturnData();
            List<ParteDiario> Lista = new List<ParteDiario>();
            if (data.ubicacion != 0)
            {
                Lista = Lista.Where(x => x.ProyectoId == data.ubicacion)
                    .OrderByDescending(x => x.Fecha_Creacion).ToList();
            }
            List<Res_PD> listRes_PD = new List<Res_PD>();
            foreach (var item in Lista)
            {
                Res_PD cPD = new Res_PD();
                cPD.Id = item.Id;
                cPD.ProyectoId = item.ProyectoId;
                cPD.ProyectoNombre = item.Proyecto.Nombre;
                cPD.Asig_Comentario = item.Asig_Comentario;
                cPD.Asig_Contratista = item.Asig_Contratista;
                cPD.Asig_Contratista_Presentes = item.Asig_Contratista_Presentes;
                cPD.Asig_Propios = item.Asig_Propios;
                cPD.Asig_Propios_Presentes = item.Asig_Propios_Presentes;
                cPD.Covid_Comentario = item.Covid_Comentario;
                cPD.Covid_Contratista = item.Covid_Contratista;
                cPD.Covid_Propioa = item.Covid_Propioa;
                if (item.Fecha_Creacion != null)
                {
                    cPD.Fecha_Creacion = item.Fecha_Creacion.ToString("dd/MM/yyyy");
                }
                else
                {
                    cPD.Fecha_Creacion = "";
                }
                listRes_PD.Add(cPD);
            }
            r.data = listRes_PD;
            r.isError = false;

            return r;
        }

        public ActionResult _ParteDiarioAbm(int id)
        {
            List<Proyecto> data = new ProyectoNegocio().Get_All_Proyecto_Parte_Diario();
            ItemProyectoFile gData = new ItemProyectoFile();
            gData.proyecto = data;
            gData.mUsuarioActual = getUsuarioActual().ApellidoNombre;
            gData.mIdUsuarioActual = getUsuarioActual().Id;
            return PartialView(gData);
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
            d.ApellidoNombre = us[1] + ", " + us[2];
            d.Grupo = Convert.ToInt32(us[5]);
            return d;
        }

        /// <summary>
        /// Valida los Parte Diarios Activos.
        /// </summary>
        /// <param name="itemGetParteDiario"></param>
        /// <returns></returns>
        [HttpPost]
        public ReturnData ValidaParteDiario(itemGetParteDiario data)
        {
            ReturnData oReturnData = new ReturnData();
            //Item de Retorno
            ItemParteDiarioRetornoGlobal retornoGlobal = new ItemParteDiarioRetornoGlobal();
            List<ItemParteDiarioRetorno> lIPDR = new List<ItemParteDiarioRetorno>();
            try
            {
                int mIdP = 0;
                //Verifico si ya existe un parte diario para este proyecto este dia
                ParteDiario oParteDiarioActual = new ParteDiarioNegocio().Get_All_ParteDiario()
                    .Where(x => x.ProyectoId == data.ProyectoId && x.Fecha_Creacion == data.Fecha_Creacion)
                    .FirstOrDefault();
                if (oParteDiarioActual != null)
                {
                    data.Editar = false;
                    data.ProyectoId = oParteDiarioActual.Id;
                }

                if (data.Editar)
                {

                    oReturnData = ValidaParteDiario_P(data);

                    if (oReturnData.isError == false)
                    {
                        mIdP = Convert.ToInt32(oReturnData.data);
                    }
                    else
                    {
                        return oReturnData;
                    }
                }
                else
                {
                    mIdP = data.ProyectoId;
                }

                ParteDiario oPD = new ParteDiarioNegocio().Get_One_ParteDiario_Solo_Actividades(mIdP);
                //BUSCO LOS DATOS DEL PROYECTO
                Proyecto oProyecto = new ProyectoNegocio().Get_One_Proyecto(oPD.ProyectoId);
                retornoGlobal.ParteDiario = null;

                foreach (var item in oPD.ParteDiario_Actividades)
                {
                    // ParteDiario_Actividades oObj = new ParteDiario_ActividadesNegocio().Get_All_ParteDiario_Actividades().Where(p => p.ParteDiarioId == item.ParteDiarioId && p.Planificacion_Proyecto_ActividadesId == item.Planificacion_Proyecto_ActividadesId).FirstOrDefault();
                    ParteDiario_Actividades oObj = new ParteDiario_ActividadesNegocio()
                        .Get_One_ParteDiario_Actividades_ValidacionParteDiario(item.Id);

                    ItemParteDiarioRetorno oIPDR = new ItemParteDiarioRetorno();
                    oIPDR.ParteDiario_ActividadesId = oObj.Id;
                    oIPDR.Finalizado = oObj.Finalizados;

                    oIPDR.Actividades_Contratista = null;
                    var mAvance = Math.Round(oObj.Avance, 2);
                    oIPDR.Avance = (float)mAvance;
                    oIPDR.Finalizado = oObj.Finalizados;

                    //Planificacion_Proyecto_Actividades
                    #region Datos de las Actividades del Parte diario - Planificacion_Proyecto_Actividades
                    oIPDR.ParteDiario_PPA = oObj.Planificacion_Proyecto_ActividadesId;
                    Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                        .Get_X_Id(oIPDR.ParteDiario_PPA);

                    oIPDR.CantidadPPA = oPPA.Cantidad;
                    oIPDR.sUnidad = " " + oPPA.UnidadMedida;
                    oIPDR.FechaFin = oPPA.Fecha_Est_Fin.ToString("dd/MM/yyyy");
                    oIPDR.FechaIncio = oPPA.Fecha_Est_Incio.ToString("dd/MM/yyyy");
                    oIPDR.FechaRealFin = oPPA.Fecha_Real_Fin.ToString("dd/MM/yyyy");

                    oIPDR.sComentario = oPPA.Detalle;
                    oIPDR.UbicacionId = oPPA.Proyecto_UbicacionesId;
                    oIPDR.sDetalle = oPPA.Detalle;
                    oIPDR.DiasDiferencia = (oPPA.Fecha_Est_Fin - oPD.Fecha_Creacion).TotalDays;
                    oIPDR.DiasDiferenciaActual = (oPPA.Fecha_Est_Fin - DateTime.Now).TotalDays;


                    oIPDR.RubroId = oPPA.Planificacion_Actividades_RubroId;
                    string mRubro = new Planificacion_Actividades_RubroNegocio().Get_One_Planificacion_Actividades_Rubro(oIPDR.RubroId).Nombre;
                    oIPDR.sRubro = mRubro;

                    string mPanificacionActividad_Nombre = new Planificacion_ActividadesNegocio().Get_One_Planificacion_Actividades(oPPA.Planificacion_ActividadesId).Nombre;
                    oIPDR.sActividades = mPanificacionActividad_Nombre;

                    string mUbicacionNombre = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones(oPPA.Proyecto_UbicacionesId).Nombre;
                    oIPDR.sUbiucacion = mUbicacionNombre;

                    if (oPPA.Fecha_Real_Incio != null)
                    {
                        string mFecha = oPPA.Fecha_Real_Incio.ToString("dd/MM/yyyy");
                        if (mFecha == "01/01/0001")
                            oIPDR.FechaRealInicio = "";
                        else
                            oIPDR.FechaRealInicio = mFecha;
                    }
                    else
                    {
                        oIPDR.FechaRealInicio = "";
                    }
                    #endregion

                    //DATOS DEL PARTE DIARIO
                    #region Datos del Parte Diario que se muestran en la Actividad
                    oIPDR.ParteDiario_Id = oObj.ParteDiarioId;
                    oIPDR.ProyectoId = oPD.ProyectoId; // oObj.ParteDiario.ProyectoId;

                    //BUSCO LOS DATOS DEL PROYECTO

                    oIPDR.ProyectoNombre = oProyecto.Nombre; // oObj.ParteDiario.Proyecto.Nombre;
                    oIPDR.Fecha_Creacion = oPD.Fecha_Creacion.ToString("dd/MM/yyyy"); // oObj.ParteDiario.Fecha_Creacion.ToString("dd/MM/yyyy");
                    oIPDR.FechaInicioProyecto = oProyecto.Fecha_Inicio.ToString("dd/MM/yyyy"); // oObj.ParteDiario.Proyecto.Fecha_Inicio.ToString("dd/MM/yyyy");
                    oIPDR.Duracion = oProyecto.Duracion;  // oObj.ParteDiario.Proyecto.Duracion;
                    oIPDR.DiasFinalizaObra = oProyecto.Duracion - (oProyecto.Fecha_Inicio - DateTime.Now).TotalDays; //  oObj.ParteDiario.Proyecto.Duracion - (oObj.ParteDiario.Proyecto.Fecha_Inicio - DateTime.Now).TotalDays;
                    #endregion

                    #region Calculo de Acumulado de Cantidad Realizada y Listado de Contratistas y el avance de cada uno por Parte diario
                    oIPDR.CantidadAcum = 0;
                    float mAcumulado = 0;
                    oIPDR.Actividades_Contratista = new List<Actividades_Contratista>();

                    List<Actividades_Contratista> lstActividadesContrastistas = new ParteDiario_Actividades_ContratistaNegocio()
                        .Get_All_ParteDiario_Actividades_Contratistas_PorActividad(
                            oIPDR.ParteDiario_PPA, oPD.Fecha_Creacion).ToList();

                    if (lstActividadesContrastistas.Count() > 0)
                    {
                        mAcumulado = lstActividadesContrastistas.Sum(p => p.Cantidad);

                        //Verifico el Avance de Ayer
                        // Si el acumulado es mayor a 0, quiere decir que puede tener avancer ayer
                        if (mAcumulado > 0)
                        {
                            //Avance de hoy
                            oIPDR.CantidadHoy = lstActividadesContrastistas.Where(p => p.Fecha == oPD.Fecha_Creacion).Sum(p => p.Cantidad);

                            //Acance de Ayer
                            DateTime mDiaAyer = new DateTime();
                            if (oPD.Fecha_Creacion.DayOfWeek == DayOfWeek.Monday)
                                mDiaAyer = oPD.Fecha_Creacion.AddDays(-2);
                            else
                                mDiaAyer = oPD.Fecha_Creacion.AddDays(-1);

                            oIPDR.CantidadAyer = lstActividadesContrastistas.Where(p => p.Fecha == mDiaAyer).Sum(p => p.Cantidad);
                        }
                        else
                        {
                            oIPDR.CantidadAyer = 0;
                            oIPDR.CantidadHoy = 0;
                        }

                    }

                    oIPDR.CantidadAcum = mAcumulado;
                    oIPDR.Actividades_Contratista = lstActividadesContrastistas.OrderByDescending(p => p.Fecha).ToList();
                    #endregion

                    #region Defino el color del fondo de este registro segun si esta vencida o no
                    oIPDR.ColorFondo = "B";

                    DateTime mFechaFin = oPPA.Fecha_Est_Fin;
                    DateTime mFechaPA = oPD.Fecha_Creacion;
                    DateTime mFechaEstInicio = oPPA.Fecha_Est_Incio;

                    //1- Verifico si la fecha estimada de fin es anterior a la fecha del partediario
                    if (oObj.Finalizados)
                    {
                        oIPDR.ColorFondo = "V";
                    }
                    else
                    {
                        if (mFechaFin < mFechaPA)
                        {
                            oIPDR.ColorFondo = "R";
                        }
                        else
                        {
                            if (mFechaEstInicio < mFechaPA && oObj.Avance == 0)
                            {
                                oIPDR.ColorFondo = "A";
                            }
                        }
                    }
                    #endregion

                    #region Defino el color del boton para identificar si cargamos alguna actividad-contratista con cantidad
                    //if (oIPDR.Actividades_Contratista.Where(p => p.Cantidad > 0 && p.Fecha == DateTime.Today).Count() > 0)
                    if (oIPDR.Actividades_Contratista.Where(p => p.Cantidad > 0 && p.Fecha == oPD.Fecha_Creacion).Count() > 0)
                    {
                        oIPDR.ColorBoton = "V";
                    }
                    else
                    {
                        oIPDR.ColorBoton = "B";
                    }
                    #endregion

                    lIPDR.Add(oIPDR);

                }

                retornoGlobal.ItemParteDiarioRetorno = lIPDR.OrderBy(x => x.FechaIncio).ToList();
                retornoGlobal.ParteDiario = new ParteDiario();
                retornoGlobal.ParteDiario.Id = mIdP;

                #region Cargos Dinstintos Actividades
                retornoGlobal.lstDistinctActividades = lIPDR.OrderBy(p => p.sActividades).Select(o => o.sActividades).Distinct().ToList();
                #endregion

                #region Cargos Distintas Ubicaciones
                retornoGlobal.lstDistinctUbicaciones = lIPDR.OrderBy(p => p.sUbiucacion).Select(o => o.sUbiucacion).Distinct().ToList();
                #endregion

                oReturnData.data = retornoGlobal;

                oReturnData.isError = false;

            }
            catch (Exception ex)
            {
                oReturnData.data = "ERROR PROCESO.";
                oReturnData.isError = true;
            }
            return oReturnData;
        }

        #region Hecho por Pablo
        [HttpPost]
        public ReturnData ValidaParteDiario_P(itemGetParteDiario data)
        {
            ReturnData r = new ReturnData();
            try
            {
                DateTime mFC = data.Fecha_Creacion;
                data.Fecha_Creacion = new DateTime(mFC.Year, mFC.Month, mFC.Day, 0, 0, 0);

                #region 1-Valido si existe una Parte diario para esa Obra en el Futuro
                ParteDiario lPD_Fut = new ParteDiarioNegocio().Get_All_ParteDiario()
                    .Where(x => x.ProyectoId == data.ProyectoId && x.Fecha_Creacion > data.Fecha_Creacion)
                    .OrderByDescending(x => x.Fecha_Creacion)
                    .FirstOrDefault();
                if (lPD_Fut != null)
                {
                    r.data = "Ya existe un parte diario para el proyecto '" + lPD_Fut.Proyecto.Nombre +
                        "' con fecha '" + lPD_Fut.Fecha_Creacion.ToShortDateString() + "'.";
                    r.isError = true;
                    return r;
                }
                #endregion

                #region 2-Valido si existe una Parte diario para esa Obra y ese Dia

                ParteDiario lPD = new ParteDiarioNegocio().Get_All_ParteDiario()
                    .Where(x => x.ProyectoId == data.ProyectoId && x.Fecha_Creacion == data.Fecha_Creacion)
                    .FirstOrDefault();

                if (lPD == null)
                {
                    // SI no existe lo tengo que crear
                    ParteDiario oParteDiario = new ParteDiario();
                    oParteDiario.Fecha_Creacion = data.Fecha_Creacion;
                    oParteDiario.ProyectoId = data.ProyectoId;
                    r = ParteDiarioGraba(oParteDiario);
                    if (r.isError == false)
                    {
                        data.Id = Convert.ToInt32(r.data);
                    }
                    else
                    {
                        r.data = "ERROR PROCESO.";
                        r.isError = true;
                        return r;
                    }
                }
                else
                {
                    data.Id = lPD.Id;
                }
                #endregion

                #region 3-Verifico las Actividades que tiene ese Parte Diario

                DateTime mFechaHoy = new DateTime(data.Fecha_Creacion.Year, data.Fecha_Creacion.Month, data.Fecha_Creacion.Day, 0, 0, 0);
                DateTime mFecha10Dias = mFechaHoy.AddDays(10);
                List<ParteDiario_Actividades> lstPD_Act_Temp = new List<ParteDiario_Actividades>(); //Lista Temporal con las ParteDiario_Actividades que corresponden a este Parte Diario

                #region 3.1 Verifico si el proyecto tiene actividades anteriores que no esten finalizadas, en el ultimo parte diario disponible
                ParteDiario oParteDiarioAnterior = new ParteDiarioNegocio().Get_All_ParteDiario()
                    .Where(x => x.ProyectoId == data.ProyectoId && x.Fecha_Creacion < data.Fecha_Creacion)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();



                #region 3.1.1 Datos del ParteDiario anterior de este proyecto
                //03/05/2022 - Se anula esta parte por que esta duplicada, ya se busca el Parte diario antes oParteDiarioAnterior
                //Traigo el ultimo parte diario que tengo de ese lugar
                //ParteDiario oPD_Anterior = new ParteDiarioNegocio().Get_All_ParteDiario()
                //    .Where(x => x.ProyectoId == data.ProyectoId && x.Fecha_Creacion < data.Fecha_Creacion)
                //    .OrderByDescending(p => p.Fecha_Creacion)
                //    .FirstOrDefault();

                //if (oPD_Anterior == null)
                //{
                //    oPD_Anterior = new ParteDiario();
                //    oPD_Anterior.Id = 0;
                //}
                #endregion

                if (oParteDiarioAnterior != null)
                {
                    //Traigo las Actividades que tiene ese parte diario anterior a HOY y que NO esten finalizadas
                    List<ParteDiario_Actividades> lstPD_Act = new ParteDiario_ActividadesNegocio().Get_All_ParteDiario_Actividades_Lite()
                        .Where(p => p.ParteDiarioId == oParteDiarioAnterior.Id && p.Finalizados == false)
                        .OrderBy(x => x.Id)
                        .ToList();
                    foreach (var oPD_Actividad in lstPD_Act)
                    {
                        if (oPD_Actividad.Finalizados == false)
                        {
                            var moPD_Actividad_Id = oPD_Actividad.Id;
                            oPD_Actividad.ParteDiarioId = data.Id;
                            oPD_Actividad.Id = 0;
                            lstPD_Act_Temp.Add(oPD_Actividad);
                        }
                    }
                }
                #endregion

                #region 3.2-Verifico en la Planificacion del Proyecto si hay alguna actividad que tenga que agregar actualmente
                //List<Planificacion_Proyecto_Actividades> lst_PPA = new Planificacion_Proyecto_ActividadesNegocio()
                //    .Get_All_Planificacion_Proyecto_Actividades()
                //    .Where(x => x.Proyecto_Ubicaciones.ProyectoId == data.ProyectoId &&
                //        x.Finalizados == false && x.Fecha_Est_Incio <= mFecha10Dias)
                //    .ToList();
                List<Planificacion_Proyecto_Actividades> lst_PPA = new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades_GeneraPD(data.ProyectoId, false, mFecha10Dias).ToList();

                var noExisten = (from t in lst_PPA  where !lstPD_Act_Temp.Any(x => t.Id == x.Planificacion_Proyecto_ActividadesId) select t).ToList();

                //lst_PPA = lst_PPA.OrderBy(x => x.Fecha_Est_Incio).ToList();
                //if (lst_PPA.Count() > 0)
                noExisten = noExisten.OrderBy(x => x.Fecha_Est_Incio).ToList();
                if (noExisten.Count() > 0)
                {
                    //
                    //foreach (var oPlanificacion_Activdad in lst_PPA)
                    foreach (var oPlanificacion_Activdad in noExisten)
                    {
                        //Verifico que no este esta ACTIVIDAD ya en la lista
                        if (lstPD_Act_Temp.Where(x => x.Planificacion_Proyecto_ActividadesId == oPlanificacion_Activdad.Id).Count() == 0)
                        {
                            ParteDiario_Actividades oPD_A = new ParteDiario_Actividades();
                            oPD_A.Avance = oPlanificacion_Activdad.Avance;
                            oPD_A.Finalizados = oPlanificacion_Activdad.Finalizados;
                            oPD_A.ParteDiarioId = data.Id;
                            oPD_A.Planificacion_Proyecto_ActividadesId = oPlanificacion_Activdad.Id;
                            oPD_A.Id = 0;
                            lstPD_Act_Temp.Add(oPD_A);
                        }

                    }
                }
                #endregion

                #region 3.3-Grabo las Actividades de este parte diario en la tabla ParteDiario_Actividades
                if (lstPD_Act_Temp.Count() > 0)
                {
                    //Borro la Infor de la tabla informes_actividades_vencidas para el proyecto
                    new Informe_Actividad_VencidaNegocio().Borrar_Informes_Proyecto(data.ProyectoId);

                    foreach (var item in lstPD_Act_Temp)
                    {
                        if (item.Planificacion_Proyecto_ActividadesId == 2254)
                        {
                            int m = 2;
                        }

                        item.UsuariosId = 1;
                        //Verifico si ya existe esa Actividad para ese Parte diario.
                        //int mCant = 0; 
                        //int mCant = new ParteDiario_ActividadesNegocio().Get_All_ParteDiario_Actividades()
                        //.Where(p => p.ParteDiarioId == data.Id && p.Planificacion_Proyecto_ActividadesId == item.Planificacion_Proyecto_ActividadesId)
                        //.Count();

                        bool mExiste = new ParteDiario_ActividadesNegocio().Existe_ParteDiarioActividad(data.Id, item.Planificacion_Proyecto_ActividadesId);
                        if (!mExiste)
                        {
                            #region 3.3.1 Grabar ParteDiario_Actividades
                            item.Id = 0;
                            item.ParteDiarioId = data.Id;
                            int idc = new ParteDiario_ActividadesNegocio().Insert(item);

                            CalcularAvanceActividad(idc, item.Planificacion_Proyecto_ActividadesId, true);
                            #endregion

                            #region 3.3.2 Grabar ParteDiario_Actividades Contratista
                            //03/05/2022 - PB - Para mejorar la performance se anula esta parte que creaba registros por cada Actividad en la parte diario actividad contratista segun la info del dia anterior
                            //var mId_ActividadAnterior = 0;
                            //try
                            //{
                            //    ParteDiario_Actividades oPDA_Aux = oPD_Anterior.ParteDiario_Actividades
                            //        .Where(p => p.Planificacion_Proyecto_ActividadesId == item.Planificacion_Proyecto_ActividadesId)
                            //        .FirstOrDefault();
                            //    if (oPDA_Aux != null)
                            //        mId_ActividadAnterior = oPDA_Aux.Id;
                            //    //Busco si esta actividad tiene alguna Actividad COntratista
                            //    List<ParteDiario_Actividades_Contratista> lstPD_Act_Cont_Temp = new List<ParteDiario_Actividades_Contratista>();
                            //    lstPD_Act_Cont_Temp = new ParteDiario_Actividades_ContratistaNegocio()
                            //        .Get_All_ParteDiario_Actividades_Contratistas()
                            //        .Where(p => p.ParteDiario_ActividadesId == mId_ActividadAnterior)
                            //        .ToList();

                            //    long mIdContratistaAnterior = 0;
                            //    long midActividadAnterior = 0;

                            //    foreach (var item_AC in lstPD_Act_Cont_Temp)
                            //    {
                            //        if (item_AC.Fecha == oPD_Anterior.Fecha_Creacion)
                            //        {


                            //            if ((mIdContratistaAnterior == item_AC.ContratistasId) && (midActividadAnterior == idc))
                            //            {
                            //                mIdContratistaAnterior = item_AC.ContratistasId;
                            //                midActividadAnterior = idc;
                            //            }
                            //            else
                            //            {
                            //                mIdContratistaAnterior = item_AC.ContratistasId;
                            //                midActividadAnterior = idc;

                            //                ParteDiario_Actividades_Contratista oPDAC = new ParteDiario_Actividades_Contratista();
                            //                oPDAC.ContratistasId = item_AC.ContratistasId;
                            //                oPDAC.Cantidad = 0;
                            //                oPDAC.Fecha = mFechaHoy;
                            //                oPDAC.Id = 0;
                            //                oPDAC.ParteDiario_ActividadesId = idc;
                            //                oPDAC.TipoRegistro = ValoresUtiles.TipoRegistro_PDAC_Manual;

                            //                new ParteDiario_Actividades_ContratistaNegocio().Insert(oPDAC);

                            //            }
                            //        }
                            //    }
                            //}
                            //catch (Exception)
                            //{
                            //}
                            #endregion

                            #region Verifico Si esta vencidad, y si corresponde agregar a la tabla de actividades vencidas con mas de 48 horas sin avance
                            if (item.Planificacion_Proyecto_Actividades == null)
                            {
                                item.Planificacion_Proyecto_Actividades = new Planificacion_Proyecto_ActividadesNegocio()
                                    .Get_X_Id(item.Planificacion_Proyecto_ActividadesId);
                            }

                            if (item.Planificacion_Proyecto_Actividades.Fecha_Est_Fin < mFechaHoy && item.Finalizados == false)
                            {
                                //Esta Vencida
                                DateTime mFechaUltPd_ConAvance = new ParteDiario_Actividades_ContratistaNegocio()
                                    .Get_Fecha_Ult_PD_con_AvanceCargado(item.Planificacion_Proyecto_ActividadesId);

                                //Verifico si tiene mas de 3 dias de
                                int mDiasVencidos = (mFechaHoy - mFechaUltPd_ConAvance).Days;
                                if (mDiasVencidos >= 3)
                                {
                                    //Grabo los esa actividad en la tabla que corresponde
                                    Informe_Actividad_Vencida oInfActVencida = new Informe_Actividad_Vencida();
                                    oInfActVencida.ProyectoId = data.ProyectoId;
                                    oInfActVencida.ProyectoUbicacionId = item.Planificacion_Proyecto_Actividades.Proyecto_UbicacionesId;
                                    oInfActVencida.PlanActividadId = item.Planificacion_Proyecto_Actividades.Planificacion_ActividadesId;
                                    oInfActVencida.PlanificacionProyectoActividadId = item.Planificacion_Proyecto_ActividadesId;
                                    oInfActVencida.Fecha_Est_Fin = item.Planificacion_Proyecto_Actividades.Fecha_Est_Fin;
                                    oInfActVencida.DiasVencida = mDiasVencidos;
                                    oInfActVencida.FecUltimoAvance = mFechaUltPd_ConAvance;
                                    oInfActVencida.PorcAvanceObra = item.Planificacion_Proyecto_Actividades.Avance;
                                    oInfActVencida.FecCreacion = DateTime.Now;
                                    oInfActVencida.Id = new Informe_Actividad_VencidaNegocio().Insert(oInfActVencida);

                                }
                            }
                            #endregion
                        }
                    }
                }
                #endregion



                r.data = data.Id;
                r.isError = false;

                #endregion
            }
            catch (Exception e)
            {
                r.data = "ERROR PROCESO." + e.Message;
                r.isError = true;
            }
            return r;
        }


        #endregion

        [HttpPost]
        public ReturnData ParteDiarioGraba(ParteDiario data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new ParteDiarioNegocio().Update(data);
                }
                else
                {
                    idc = new ParteDiarioNegocio().Insert(data);
                }

                if (idc > 0)
                {
                    d.isError = false;
                    d.data = idc.ToString();
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

        #endregion

        #region Parte Diario Actividades

        public ActionResult ParteDiario_Actividades()
        {
            return View();
        }

        public ActionResult _ParteDiario_ActividadesGrilla()
        {
            List<ParteDiario_Actividades> lPDAs = new List<ParteDiario_Actividades>();
            lPDAs = new ParteDiario_ActividadesNegocio().Get_All_ParteDiario_Actividades();
            List<Res_PDA> lResPDA = new List<Res_PDA>();
            foreach (var oPDA in lPDAs)
            {
                Res_PDA oResPDA = new Res_PDA();
                oResPDA.Id = oPDA.Id;
                oResPDA.ParteDiarioId = oPDA.ParteDiarioId;
                oResPDA.ParteDiarioNombre = oPDA.ParteDiario.Proyecto.Nombre;
                oResPDA.Planificacion_Proyecto_ActividadesId = oPDA.Planificacion_Proyecto_ActividadesId;
                oResPDA.Planificacion_Proyecto_ActividadesNombre = oPDA.Planificacion_Proyecto_Actividades.Planificacion_Actividades_Rubro.Nombre;
                oResPDA.Planificacion_Proyecto_ActividadesNombre = oPDA.Planificacion_Proyecto_Actividades.Planificacion_Actividades.Nombre;
                oResPDA.Planificacion_Proyecto_ActividadesNombre = oPDA.Planificacion_Proyecto_Actividades.Proyecto_Ubicaciones.Nombre;
                oResPDA.Planificacion_Proyecto_ActividadesNombre = oPDA.Planificacion_Proyecto_Actividades.Fecha_Est_Incio.ToString("dd/MM/yyyy");
                oResPDA.Planificacion_Proyecto_ActividadesNombre = oPDA.Planificacion_Proyecto_Actividades.Fecha_Est_Fin.ToString("dd/MM/yyyy");
                oResPDA.Planificacion_Proyecto_ActividadesNombre = oPDA.Planificacion_Proyecto_Actividades.Cantidad.ToString("0");
                oResPDA.Avance = oPDA.Avance;
                oResPDA.Comentarios = oPDA.Comentarios;
                oResPDA.Finalizados = oPDA.Finalizados;
                oResPDA.UsuariosId = oPDA.UsuariosId;
                oResPDA.UsuariosNombre = oPDA.Usuarios.Nombre;
                lResPDA.Add(oResPDA);
            }
            return PartialView(lResPDA);
        }

        public ActionResult _ParteDiario_ActividadesAbm(int id)
        {
            ItemParteDiario_Actividades data = new ItemParteDiario_Actividades();
            if (id != 0)
            {
                data.pa = new ParteDiario_ActividadesNegocio().Get_One_ParteDiario_Actividades(id);
            }
            else
            {
                data.pa = new ParteDiario_Actividades();
            }

            data.Planificacion_Proyecto_Actividades = new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades();
            data.ParteDiario = new ParteDiarioNegocio().Get_All_ParteDiario();
            data.Usuarios = new UsuariosNegocio().Get_Usuarios().Where(u => u.Tipo == "J").ToList();

            return PartialView(data);
        }

        [HttpPost]
        public ReturnData ParteDiario_ActividadesGraba(ParteDiario_Actividades data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new ParteDiario_ActividadesNegocio().Update(data);
                }
                else
                {
                    idc = new ParteDiario_ActividadesNegocio().Insert(data);
                }

                if (idc > 0)
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
        public ReturnData GetProActividades(int id)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = new Planificacion_Proyecto_ActividadesNegocio().Get_All_ubi_Proyecto_Actividades(id);
            return data;
        }

        #endregion

        #region Parte Diario Finaliza

        public ActionResult ParteDiario_Finaliza_Actividad(int id)
        {
            Res_ParteDiario_ActividadFinaliza data = new Res_ParteDiario_ActividadFinaliza();
            ParteDiario_Actividades oParteDiarioActividades = new ParteDiario_ActividadesNegocio().Get_One_ParteDiario_Actividades(id);

            data.IdParteDiario = oParteDiarioActividades.ParteDiarioId;
            data.Planificacion_Proyecto_ActividadId = oParteDiarioActividades.Planificacion_Proyecto_ActividadesId;

            data.ProyectoId = oParteDiarioActividades.ParteDiario.ProyectoId;
            data.IdPlanificacionActividades = oParteDiarioActividades.Planificacion_Proyecto_Actividades.Planificacion_ActividadesId;
            data.parteDiario_ActividadesId = id;
            data.lstItemCalidad = new Calidad_Actividades_ValoracionNegocio().Get_ItemCalidad_ParaActividad(data.Planificacion_Proyecto_ActividadId, "FINALIZA");

            if (data.lstItemCalidad.Count > 0)
                data.MostarItemCalidad = true;
            else
                data.MostarItemCalidad = false;
            return PartialView(data);
        }

        [HttpPost]
        public ReturnData CalidadValoracionGraba(ItemFinalizaActividad data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {

                EF.Models.Calidad_Actividades_Valoracion cav = new EF.Models.Calidad_Actividades_Valoracion();
                cav.Id = data.Id;
                cav.IdParteDiario = data.IdParteDiario;
                cav.IdPlanificacion_Proyecto_ActividadId = data.IdPlanificacion_Proyecto_Actividad;
                cav.IdCalidad_Items = data.IdCalidad_Item;
                cav.Valor = data.Valor;
                cav.Descripcion = data.Descripcion;
                int idc;

                idc = new Calidad_Actividades_ValoracionNegocio().Insert(cav);
                //Cargamos FOTOS 
                if (data.ArchivosF != null && data.ArchivosF.Count > 0)
                {

                    foreach (IFormFile photo in data.ArchivosF)
                    {
                        string Folder = Path.Combine(_env.WebRootPath, "archivos/Calidad_Obra/");

                        string nf = photo.FileName;
                        var nf1 = nf.Split(".");
                        var nl = nf1.Length;
                        string nfe = nf1[(nl - 1)];
                        string f = ToolsNegocio.CrearAlfaNumerico(4);
                        string mNombreSinExtencion = nf.Replace(nfe, "");
                        mNombreSinExtencion = mNombreSinExtencion.Replace(' ', '_');
                        mNombreSinExtencion = mNombreSinExtencion.Replace('.', '_');
                        string mName = mNombreSinExtencion;
                        string fileName1 = "calidad_obra_" + mName + "_" + idc + "." + nfe;
                        string filePath = Path.Combine(Folder, fileName1);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));

                        Archivos_Adjuntos aa = new Archivos_Adjuntos();
                        aa.Archivo = fileName1;
                        aa.Extencion = nfe.ToUpper();
                        aa.Fecha = DateTime.Now;
                        aa.IdUsuario = UsuarioLogin().Id;
                        aa.sUsuario = UsuarioLogin().Nombre + " " + UsuarioLogin().Apellido;
                        aa.URL = "../archivos/Calidad_Obra/";
                        int IdAA = new Archivos_AdjuntosNegocio().Insert(aa);

                        Archivos_Adjuntos_Relacion aar = new Archivos_Adjuntos_Relacion();
                        aar.Archivos_AdjuntosId = IdAA;
                        aar.Entidad = "Calidad_Obra";
                        aar.IdEntidad = idc;

                        new Archivos_Adjuntos_RelacionNegocio().Insert(aar);
                    }
                }


                #region devuelvo datos

                List<Calidad_Actividades_Valoracion> ListaRet = new List<Calidad_Actividades_Valoracion>();
                List<Calidad_Actividades_Valoracion> cavg = new Calidad_Actividades_ValoracionNegocio().Get_All_Calidad_Actividades_ValoracionId(data.IdPlanificacion_Proyecto_Actividad);
                foreach (var e in cavg)
                {
                    Calidad_Actividades_Valoracion ca = new Calidad_Actividades_Valoracion();
                    ca = e;


                    ListaRet.Add(ca);
                }
                d.data = "Grabado ok";
                d.data1 = ListaRet;
                d.isError = false;

                #endregion

            }
            else
            {
                d.isError = true;
                d.data = "Error al Grabar";

            }

            return d;
        }

        [HttpPost]
        public ReturnData ActividadesFinaliza(Res_ParteDiario_ActividadFinaliza oResParteDiarioFinaliza)
        {
            ReturnData d = new ReturnData();
            int idc;
            try
            {
                //Este procedimiento consta de 2 partes
                //1-Validad que si tenia algun Item de Calidad lo haya completado
                //2-Actualizar la Actividad como Finalizada
                //3-Grabar los items de calidad
                //4-Verifico si no tiene item de calidad, verifico si se puso algo en el TEXTBOX para crear un incidente para la creacion correspondiente
                if (oResParteDiarioFinaliza.parteDiario_ActividadesId > 0)
                {
                    #region Verifico que se hayan completado los valores de ITem de Calidad
                    if (oResParteDiarioFinaliza.lstItemCalidad != null)
                    {
                        if (oResParteDiarioFinaliza.lstItemCalidad.Count() > 0)
                        {
                            d = new Calidad_Actividades_ValoracionNegocio().ValidarItemCalidad(oResParteDiarioFinaliza.lstItemCalidad);
                            if (d.isError == true)
                            {
                                return d;
                            }
                        }
                    }
                    #endregion

                    #region Finalizar la Actividad

                    ParteDiario_Actividades oPDA = new ParteDiario_ActividadesNegocio()
                        .Get_One_ParteDiario_Actividades(oResParteDiarioFinaliza.parteDiario_ActividadesId);
                    oPDA.Finalizados = true;
                    idc = new ParteDiario_ActividadesNegocio().Update(oPDA);

                    //Se actualiza la fecha real de Finalizacion
                    Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                        .Get_X_Id(oPDA.Planificacion_Proyecto_ActividadesId);
                    if (oPPA.Fecha_Real_Incio <= oPDA.ParteDiario.Fecha_Creacion)
                    {
                        if (oPPA.Fecha_Est_Fin.Day != oPDA.ParteDiario.Fecha_Creacion.Day ||
                            oPPA.Fecha_Est_Fin.Month != oPDA.ParteDiario.Fecha_Creacion.Month ||
                            oPPA.Fecha_Est_Fin.Year != oPDA.ParteDiario.Fecha_Creacion.Year)
                        {
                            ResultActualizacionArbolPPA oRAAPPA = new ResultActualizacionArbolPPA();
                            oRAAPPA.IdPlanProyActividad = oPPA.Id;
                            oRAAPPA.IdPlanProyOrigenMovimiento = oPPA.Id;
                            oRAAPPA.FecEstInicio_Nueva = oPPA.Fecha_Est_Incio;
                            oRAAPPA.FecEstFin_Nueva_Original = oPDA.ParteDiario.Fecha_Creacion;
                            oRAAPPA.FecEstFin_Nueva = oPDA.ParteDiario.Fecha_Creacion;
                            oRAAPPA.CambioAplicable = true;
                            oRAAPPA.message = string.Empty;
                            oRAAPPA.lPlanProyActividades_Resultante = new List<Planificacion_Proyecto_Actividades>();
                            oRAAPPA = new Planificacion_Proyecto_ActividadesNegocio()
                                .AplicarActualizacionCascada(oRAAPPA, false);
                        }
                        oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                            .Get_X_Id(oPDA.Planificacion_Proyecto_ActividadesId);
                        oPPA.Finalizados = true;
                        oPPA.Fecha_Real_Fin = oPDA.ParteDiario.Fecha_Creacion;
                        oPPA.Fecha_Ultima_Modificacion = DateTime.Now;
                        idc = new Planificacion_Proyecto_ActividadesNegocio().Update(oPPA);

                        #region Grabo los Item de Calidad
                        int mIdProyecto = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones(oPPA.Proyecto_UbicacionesId).ProyectoId;
                        if (oResParteDiarioFinaliza.lstItemCalidad != null)
                        {
                            if (oResParteDiarioFinaliza.lstItemCalidad.Count() > 0)
                            {
                                //Busco el ProyectoID

                                d = new Calidad_Actividades_ValoracionNegocio().GrabarItemCalidad_Incidentes(oResParteDiarioFinaliza.lstItemCalidad, getUsuarioActual().Id, oPDA.ParteDiarioId, oPDA.Planificacion_Proyecto_ActividadesId, mIdProyecto);
                                if (d.isError == true)
                                {
                                    return d;
                                }
                            }
                        }
                        if (oResParteDiarioFinaliza.CrearItemCalidad == null)
                        {
                            oResParteDiarioFinaliza.CrearItemCalidad = "";
                        }
                        if (oResParteDiarioFinaliza.MostarItemCalidad == false && oResParteDiarioFinaliza.CrearItemCalidad.Trim().Length > 0)
                        {
                            //Verifico si se puso algun comentario en el text para crear un nuevo incidente
                            Incidentes_Historial oIncidente = new Incidentes_Historial();
                            Planificacion_Actividades oPA = new Planificacion_ActividadesNegocio()
                                .Get_One_Planificacion_Actividades(oPPA.Planificacion_ActividadesId);
                            string mTexto = "El usuario " + getUsuarioActual().ApellidoNombre + 
                                " ha solicitado la creacion de un Item de calidad para la actividad: "+
                                "(ID:" + oPDA.Planificacion_Proyecto_ActividadesId + ") " + oPA.Nombre +
                                " - DETALLE: " + oPDA.Planificacion_Proyecto_Actividades.Detalle + ", cuando se  *FINALIZA* la misma";
                            mTexto = mTexto + " - El usuario tambien ha agregado el siguiente comentario:" + oResParteDiarioFinaliza.CrearItemCalidad;

                            oIncidente.Creacion_Descripcion = mTexto;
                            oIncidente.AreaId = 17;
                            oIncidente.Creacion_Fecha = DateTime.Now;
                            oIncidente.Creacion_UsuarioId = getUsuarioActual().Id;
                            oIncidente.ProyectoId = mIdProyecto;
                            oIncidente.EstadoId = 2;
                            oIncidente.Fecha_Cierre = DateTime.Now.AddDays(5);
                            oIncidente.Id = 0;
                            oIncidente.IncidenteId = 60;
                            oIncidente.ParteDiarioId = oPDA.ParteDiarioId;

                            new Incidentes_HistorialNegocio().Insert(oIncidente);
                        }
                        #endregion
                        d.isError = false;
                        d.data = "Se han Grabado los datos OK.";
                    }
                    else
                    {
                        d.isError = false;
                        d.data = "No se puede finalizar con un parte diario cuya" +
                            "fecha es menor a la fecha de inicio real de la actividad.";
                    }

                    #endregion
                }

            }
            catch (Exception)
            {
                d.isError = true;
                d.data = "Error al Grabar";
            }
            return d;
        }

        #endregion

        #region Actividades Fotos

        public ActionResult Actividades_Foto()
        {
            return View();
        }

        public ActionResult _Actividades_FotoGrilla()
        {
            List<ParteDiario_Actividades_Fotos> Lista = new List<ParteDiario_Actividades_Fotos>();
            Lista = new ParteDiario_Actividades_FotosNegocio().Get_All_ParteDiario_Actividades_Fotos();
            return PartialView(Lista);
        }

        public ActionResult _Actividades_FotoAbm(int id)
        {
            ItemActividades_Foto data = new ItemActividades_Foto();
            if (id != 0)
            {
                ParteDiario_Actividades_Fotos pdaf = new ParteDiario_Actividades_FotosNegocio().Get_One_ParteDiario_Actividades_Fotos(id);
                data.Id = pdaf.Id;
                data.URL_Foto = pdaf.URL_Foto;
                data.ParteDiario_ActividadesId = pdaf.ParteDiario_ActividadesId;
            }
            else
            {
                data.Id = 0;
                data.URL_Foto = "";
                data.ParteDiario_ActividadesId = 0;
            }

            data.ParteDiario_Actividades = new ParteDiario_ActividadesNegocio().Get_All_ParteDiario_Actividades();

            return PartialView(data);
        }

        [HttpGet]
        public ReturnData getParteDiarioActividades()
        {
            ReturnData d = new ReturnData();
            try
            {
                d.data = new ParteDiario_ActividadesNegocio().Get_All_ParteDiario_Actividades();
                d.isError = false;
            }
            catch (Exception)
            {
                d.data = null;
                d.isError = true;
            }
            return d;
        }

        [HttpPost]
        public ReturnData Actividades_FotoAbm_Graba(ItemActividades_Foto data)
        {
            ReturnData d = new ReturnData();

            ParteDiario_Actividades_Fotos pdaf = new ParteDiario_Actividades_Fotos();

            try
            {
                if (data.Id > 0)
                {
                    pdaf = new ParteDiario_Actividades_FotosNegocio().Get_One_ParteDiario_Actividades_Fotos(data.Id);
                    pdaf.ParteDiario_ActividadesId = data.ParteDiario_ActividadesId;
                    if (data.File01 != null)
                        pdaf.URL_Foto = pdaf.URL_Foto + ";" + addFoto(data.File01, data.Id);

                    int g = new ParteDiario_Actividades_FotosNegocio().Update(pdaf);

                    if (g > 0)
                    {
                        d.data = "Proceso Ok";
                        d.isError = false;
                    }
                    else
                    {
                        d.data = "Error Proceso";
                        d.isError = true;
                    }
                }
                else
                {
                    pdaf.Id = 0;
                    pdaf.ParteDiario_ActividadesId = data.ParteDiario_ActividadesId;
                    if (data.File01 != null)
                        pdaf.URL_Foto = addFoto(data.File01, data.Id);
                    else
                        pdaf.URL_Foto = "";

                    int g = new ParteDiario_Actividades_FotosNegocio().Insert(pdaf);

                    if (g > 0)
                    {
                        d.data = "Proceso Ok";
                        d.isError = false;
                    }
                    else
                    {
                        d.data = "Error Proceso";
                        d.isError = true;
                    }
                }


            }
            catch (Exception)
            {
                d.data = "Error Proceso";
                d.isError = true;
            }

            return d;

        }

        private string addFoto(IFormFile archivo, int Id)
        {
            string fileName1 = "";
            string mensajeFile = "";
            if (archivo != null)
            {
                if (validaFile(archivo.ContentType, archivo.Length))
                {
                    string Folder = Path.Combine(_env.WebRootPath, "dist/Actividades_Fotos/");

                    string nf = archivo.FileName;
                    var nf1 = nf.Split(".");
                    var nl = nf1.Length;
                    string nfe = nf1[(nl - 1)];

                    DateTime hoy = DateTime.Now;

                    fileName1 = Id + "_" + hoy.Minute + "." + nfe;
                    string filePath = Path.Combine(Folder, fileName1);
                    archivo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                else
                {
                    fileName1 = "";
                }
            }
            else
            {
                fileName1 = "";
            }

            return fileName1;
        }


        private bool validaFile(string name, long peso)
        {
            bool OK = true;

            switch (name)
            {
                case "image/jpeg":
                    OK = true;
                    break;
                case "image/JPEG":
                    OK = true;
                    break;
                case "image/png":
                    OK = true;
                    break;
                case "image/gif":
                    OK = true;
                    break;
                case "image/pdf":
                    OK = true;
                    break;
                default:
                    OK = false;
                    break;
            }

            if (peso > 3110007)
            {
                OK = false;
            }

            return OK;
        }

        [HttpPost]
        public ReturnData Actividades_FotoGraba(ParteDiario_Actividades_Fotos data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new ParteDiario_Actividades_FotosNegocio().Update(data);
                }
                else
                {
                    idc = new ParteDiario_Actividades_FotosNegocio().Insert(data);
                }

                if (idc > 0)
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


        #endregion

        #region Obra Adic
        public ActionResult ObraAdic()
        {
            return View();
        }

        public ActionResult _ObraAdicGrilla()
        {
            List<ParteDiario_Sol_ObrasAdic> Lista = new List<ParteDiario_Sol_ObrasAdic>();
            Lista = new ParteDiario_Sol_ObrasAdicNegocio().Get_All_ParteDiario_Sol_ObrasAdic();
            return PartialView(Lista);
        }

        public ActionResult _ObraAdicAbm(int id)
        {
            ItemObraAdic data = new ItemObraAdic();
            if (id != 0)
            {
                data.oa = new ParteDiario_Sol_ObrasAdicNegocio().Get_One_ParteDiario_Sol_ObrasAdic(id);
            }
            data.ParteDiario = new ParteDiarioNegocio().Get_All_ParteDiario();

            return PartialView(data);
        }

        [HttpPost]
        public ReturnData ObraAdicGraba(ParteDiario_Sol_ObrasAdic data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new ParteDiario_Sol_ObrasAdicNegocio().Update(data);
                }
                else
                {
                    idc = new ParteDiario_Sol_ObrasAdicNegocio().Insert(data);
                }

                if (idc > 0)
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


        #endregion

        #region Carga ParteDiario
        public ActionResult ParteDiarioAct(int id)
        {
            ReturnData r = new ReturnData();
            r.data = id;
            r.isError = false;
            return View(r);
        }

        #endregion

        #region Usuarios Login
        /// <summary>
        /// Obtiene el Usuario Logiado
        /// </summary>
        /// <returns></returns>
        private ItemLoginUsuario UsuarioLogin()
        {
            ItemLoginUsuario d = new ItemLoginUsuario();
            string[] us = (HttpContext.User.Identity.Name).Split("#");

            d.Id = Convert.ToInt32(us[0]);
            d.Nombre = us[1];
            d.Apellido = us[2];
            d.Email = us[3];
            d.Tipo = us[4];
            d.Grupo = Convert.ToInt32(us[5]);

            return d;
        }

        #endregion

        #region Parte Diario Contratistas

        public ActionResult ParteDiario_Contratista_Abm(int id)
        {
            ItemContratistasFile data = new ItemContratistasFile();
            ParteDiario_Actividades oContratistasActividades = new ParteDiario_ActividadesNegocio().Get_One_ParteDiario_Actividades(id);
            data.Contratistas = new ContratistasNegocio().Get_All_Contratistas();
            data.IdParteDiario = oContratistasActividades.ParteDiarioId;
            data.Planificacion_Proyecto_ActividadId = oContratistasActividades.Planificacion_Proyecto_ActividadesId;
            data.ProyectoId = oContratistasActividades.ParteDiario.ProyectoId;
            data.IdPlanificacionActividades = oContratistasActividades.Planificacion_Proyecto_Actividades.Planificacion_ActividadesId;
            data.parteDiario_ActividadesId = id;
            data.lstItemCalidad = new Calidad_Actividades_ValoracionNegocio()
                .Get_ItemCalidad_ParaActividad(data.Planificacion_Proyecto_ActividadId, "INICIAR");
            if (data.lstItemCalidad.Count > 0)
                data.MostarItemCalidad = true;
            else
                data.MostarItemCalidad = false;
            return PartialView(data);
        }

        [HttpPost]
        public ReturnData ParteDiario_ContratistasGraba(ItemContratistasGraba data)
        {
            ReturnData oReturnData = new ReturnData();
            KeyValuePair<bool, string> kvpValidCompPrecedencia = this.ValidarCompatibilidadPrecedencia(
                data.Cantidad, data.parteDiario_ActividadesId);
            if (kvpValidCompPrecedencia.Key)
            {
                int idPDAC;
                try
                {
                    if (data.lstItemCalidad2 != null)
                    {
                        if (data.lstItemCalidad2 != "")
                        {
                            List<Calidad_Actividades_Valoracion> ListaCalidad = new List<Calidad_Actividades_Valoracion>();
                            string[] lc = data.lstItemCalidad2.Split("-*");
                            foreach (var e in lc)
                            {
                                if (e != "")
                                {
                                    string[] ed = e.Split("||");
                                    Calidad_Actividades_Valoracion cav = new Calidad_Actividades_Valoracion();
                                    cav.Valor = ed[0];
                                    cav.Descripcion = ed[1];
                                    cav.IdCalidad_Items = Convert.ToInt32(ed[2]);
                                    ListaCalidad.Add(cav);
                                }
                            }
                            data.lstItemCalidad = ListaCalidad;
                        }
                    }
                    //Este procedimiento consta de 2 partes
                    //1-Validad que si tenia algun Item de Calidad lo haya completado
                    //2-Actualizar la Actividad como Finalizada
                    //3-Grabar los items de calidad
                    if (data.parteDiario_ActividadesId > 0)
                    {
                        ParteDiario_Actividades oPDA = new ParteDiario_ActividadesNegocio()
                            .Get_One_ParteDiario_Actividades(data.ParteDiario_ActividadesId);

                        #region Verifico que se hayan completado los valores de ITem de Calidad
                        if (data.MostarItemCalidad == true)
                        {
                            if (data.lstItemCalidad != null)
                            {
                                if (data.lstItemCalidad.Count() > 0)
                                {
                                    oReturnData = new Calidad_Actividades_ValoracionNegocio().ValidarItemCalidad(data.lstItemCalidad);
                                    if (oReturnData.isError == true)
                                    {
                                        return oReturnData;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Graba act Contratistas
                        ParteDiario_Actividades oPDA_f = new ParteDiario_ActividadesNegocio()
                            .Get_One_ParteDiario_Actividades(data.ParteDiario_ActividadesId);
                        if (data != null)
                        {
                            ParteDiario_Actividades_Contratista oPDAC = new ParteDiario_Actividades_Contratista();
                            oPDAC.Cantidad = data.Cantidad;
                            oPDAC.TipoRegistro = ValoresUtiles.TipoRegistro_PDAC_Manual;
                            oPDAC.ContratistasId = data.ContratistasId;
                            oPDAC.ParteDiario_ActividadesId = data.ParteDiario_ActividadesId;
                            //Necesita conocer la Fecha de Ese parte diario, para ello desde la Actividad voy buscando
                            oPDAC.Fecha = oPDA.ParteDiario.Fecha_Creacion;
                            List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio()
                                .Get_Archivos_Adjuntos_Relacion("PD", data.ParteDiario_ActividadesId);
                            if (Laar.Count > 0)
                            {
                                data.IsArchivoContratistas = true;
                            }
                            else
                            {
                                data.IsArchivoContratistas = false;
                            }

                            //Busco si ya hay un parte diario actividad para esa actividad y para ese contratista
                            ParteDiario_Actividades_Contratista oPDACExistente = new ParteDiario_Actividades_ContratistaNegocio()
                                .get_one_Contratista_Actividad(data.ContratistasId, data.ParteDiario_ActividadesId);

                            if (oPDACExistente != null)
                            {
                                if (oPDACExistente.Cantidad > 0)
                                {
                                    idPDAC = new ParteDiario_Actividades_ContratistaNegocio().Insert(oPDAC);
                                }
                                else
                                {
                                    oPDAC.Id = oPDACExistente.Id;
                                    idPDAC = new ParteDiario_Actividades_ContratistaNegocio().Update(oPDAC);
                                }

                            }
                            else
                            {
                                idPDAC = new ParteDiario_Actividades_ContratistaNegocio().Insert(oPDAC);
                            }


                            /// Generar Calculo de Avance
                            /// Llega el acance Actual data.Avance
                            CalcularAvanceActividad(oPDA.Id, oPDA.Planificacion_Proyecto_ActividadesId, true);
                            //idPDAC = 0;
                            //TODO: esto nunca se ejecuta pues idc siempre será 0 ¿?
                            if (idPDAC > 0)
                            {
                                //Cargamos FOTOS 
                                if (data.ArchivosPD != null && data.ArchivosPD.Count > 0)
                                {
                                    foreach (IFormFile photo in data.ArchivosPD)
                                    {
                                        string Folder = Path.Combine(_env.WebRootPath, "archivos/PD_Contratistas/");

                                        string nf = photo.FileName;
                                        var nf1 = nf.Split(".");
                                        var nl = nf1.Length;
                                        string nfe = nf1[(nl - 1)];
                                        string f = ToolsNegocio.CrearAlfaNumerico(4);
                                        string mNombreSinExtencion = nf.Replace(nfe, "");
                                        mNombreSinExtencion = mNombreSinExtencion.Replace(' ', '_');
                                        mNombreSinExtencion = mNombreSinExtencion.Replace('.', '_');
                                        string mName = mNombreSinExtencion;
                                        string fileName1 = "PD_" + mName + "_" + idPDAC + "." + nfe;
                                        string filePath = Path.Combine(Folder, fileName1);
                                        photo.CopyTo(new FileStream(filePath, FileMode.Create));

                                        Archivos_Adjuntos aa = new Archivos_Adjuntos();
                                        aa.Archivo = fileName1;
                                        aa.Extencion = nfe.ToUpper();
                                        aa.Fecha = DateTime.Now;
                                        aa.IdUsuario = UsuarioLogin().Id;
                                        aa.sUsuario = UsuarioLogin().Nombre + " " + UsuarioLogin().Apellido;
                                        aa.URL = "../archivos/PD_Contratistas/";
                                        int IdAA = new Archivos_AdjuntosNegocio().Insert(aa);

                                        Archivos_Adjuntos_Relacion aar = new Archivos_Adjuntos_Relacion();
                                        aar.Archivos_AdjuntosId = IdAA;
                                        aar.Entidad = "PD";
                                        aar.IdEntidad = idPDAC;

                                        new Archivos_Adjuntos_RelacionNegocio().Insert(aar);
                                    }
                                }
                                oReturnData.isError = false;
                                oReturnData.data = "Se han grabado los datos OK.";
                            }
                        }
                        else
                        {
                            oReturnData.isError = true;
                            oReturnData.data = "Error al Grabar";

                        }
                        #endregion

                        #region Grabo los Item de Calidad
                        ParteDiario oPD = new ParteDiarioNegocio().Get_One_ParteDiario_SinInclude(oPDA_f.ParteDiarioId);
                        if (data.lstItemCalidad != null)
                        {
                            if (data.lstItemCalidad.Count() > 0)
                            {
                                //Busco el PRoyecto

                                oReturnData = new Calidad_Actividades_ValoracionNegocio().GrabarItemCalidad_Incidentes(data.lstItemCalidad, getUsuarioActual().Id, oPDA_f.ParteDiarioId, oPDA_f.Planificacion_Proyecto_ActividadesId, oPD.ProyectoId);
                                if (oReturnData.isError == true)
                                {
                                    return oReturnData;

                                }
                            }
                        }
                        if (data.CrearItemCalidad == null)
                        {
                            data.CrearItemCalidad = "";
                        }
                        if (data.MostarItemCalidad == false && data.CrearItemCalidad.Trim().Length > 0)
                        {
                            //Verifico si se puso algun comentario en el text para crear un nuevo incidente
                            Incidentes_Historial oIncidente = new Incidentes_Historial();
                            string mTexto = "El usuario " + getUsuarioActual().ApellidoNombre + " ha solicitado la creacion de un Item de calidad para la actividad: ID:" + oPDA_f.Planificacion_Proyecto_ActividadesId + " - DETALLE: " + oPDA_f.Planificacion_Proyecto_Actividades.Detalle + ", cuando se  *INICIALIZA* la misma";
                            mTexto = mTexto + " - El usuario agrego el siguiente comentario:" + data.CrearItemCalidad;

                            oIncidente.Creacion_Descripcion = mTexto;
                            oIncidente.AreaId = 17;
                            oIncidente.Creacion_Fecha = DateTime.Now;
                            oIncidente.Creacion_UsuarioId = getUsuarioActual().Id;
                            oIncidente.ProyectoId = oPD.ProyectoId;
                            oIncidente.EstadoId = 2;
                            oIncidente.Fecha_Cierre = DateTime.Now.AddDays(5);
                            oIncidente.Id = 0;
                            oIncidente.IncidenteId = 60;
                            oIncidente.ParteDiarioId = oPDA_f.ParteDiarioId;

                            new Incidentes_HistorialNegocio().Insert(oIncidente);
                        }
                        #endregion

                        oReturnData.isError = false;
                        oReturnData.data = "Se han Grabado los datos OK.";
                    }

                }
                catch (Exception e)
                {
                    oReturnData.isError = true;
                    oReturnData.data = "Error al Grabar";

                }
            }
            else
            {
                oReturnData.data = new
                {
                    error = true,
                    message = kvpValidCompPrecedencia.Value
                };
            }
            return oReturnData;
        }

        /// <summary>
        /// Método que verifica para un PPA que no tiene avances y que se le está por registrar el primero
        /// y por tanto asiganar una fecha real de inicio, que esa misma fecha no sea menor a la fecha de inicio del padre
        /// </summary>
        /// <param name="cantidad">Cantidad de avance</param>
        /// <param name="idPDA">Id Parte Diario desde el cual se intenta registrar el avance</param>
        /// <param name="idPPA">Id De PlanificacionProyectoActividad</param>
        /// <returns></returns>
        private KeyValuePair<bool, string> ValidarCompatibilidadPrecedencia(float cantidad, int idPDA)
        {
            KeyValuePair<bool, string> kvpResult = new KeyValuePair<bool, string>(true, "Todo Ok");
            try
            {
                ParteDiario_Actividades oPDA = new ParteDiario_ActividadesNegocio()
                    .Get_One_ParteDiario_Actividades(idPDA);
                ParteDiario oPD = new ParteDiarioNegocio().Get_One_ParteDiario(oPDA.ParteDiarioId);
                Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                    .Get_X_Id(oPDA.Planificacion_Proyecto_ActividadesId, true);
                if (cantidad > 0 && oPPA.Avance == 0 && oPPA.TienePadre)
                {
                    if (oPPA.oPPADep_PadrePrimero.PPAPadre.Avance == 0)
                    {
                        kvpResult = new KeyValuePair<bool, string>(false, "El avance que intenta registrar actualizaría la actividad " +
                            "asignándole una fecha real de inicio (la misma del parte diario), pero la actividad padre no ha iniciado aún. " +
                            "Se restringe la aplicación del cambio hasta que se inicie la actividad padre.");
                    }
                    else if (oPPA.oPPADep_PadrePrimero.PPAPadre.Fecha_Real_Incio > oPD.Fecha_Creacion)
                    {
                        kvpResult = new KeyValuePair<bool, string>(false,
                            "Al ser el primer avance, la fecha de inicio real de la actividad será asignada " +
                            "según la fecha del parte diario (" + oPPA.Fecha_Real_Incio.ToString("dd-MM-yyyy") +
                            ") y la misma es menor a la fecha real de inicio (" + oPPA.oPPADep_PadrePrimero.PPAPadre.Fecha_Real_Incio.ToString("dd-MM-yyyy") +
                            ") de la actividad padre. " +
                            "Esto es incorrecto, por favor realice la carga del avance desde un parte diario con " +
                            "fecha mayor a la fecha real de inicio de la actividad padre");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ParteDiarioController: VerificarFechasPPAPadre(float, int, int): exception.", ex);
            }
            return kvpResult;
        }

        [HttpGet]
        public ReturnData getParteDiarioContratistas(int id)
        {
            ReturnData r = new ReturnData();
            List<ParteDiario_Actividades_Contratista> Lista = new List<ParteDiario_Actividades_Contratista>();
            try
            {
                Lista = new ParteDiario_Actividades_ContratistaNegocio().Get_All_ParteDiario_Actividades_Contratistas();
                r.data = Lista;
                r.isError = false;
            }
            catch (Exception)
            {
                r.data = Lista;
                r.isError = false;
            }

            return r;
        }


        //Borra Contratista


        [HttpPost]
        public ReturnData ContratistaBorra(int id)
        {
            ReturnData d = new ReturnData();
            int idc;
            try
            {
                ParteDiario_Actividades_Contratista pdac = new ParteDiario_Actividades_Contratista();

                ParteDiario_Actividades_Contratista pdcb = new ParteDiario_Actividades_ContratistaNegocio().Get_x_Id(id);
                int mPartediario_Actividadesid = pdcb.ParteDiario_ActividadesId;
                //Buscar el Parte Diario Actividades
                ParteDiario_Actividades oPDA = new ParteDiario_ActividadesNegocio().Get_One_ParteDiario_Actividades(mPartediario_Actividadesid);
                int mPPA_id = oPDA.Planificacion_Proyecto_ActividadesId;

                //Verifico si la fecha del registro a BORRAR es igual a la del parte diario, si es distinto no lo hago
                DateTime mFechaParteDiario = oPDA.ParteDiario.Fecha_Creacion;
                DateTime mFechaRegistroBorrar = pdcb.Fecha;

                if (mFechaRegistroBorrar == mFechaParteDiario)
                {
                    idc = new ParteDiario_Actividades_ContratistaNegocio().Delete(pdcb);
                    CalcularAvanceActividad(mPartediario_Actividadesid, mPPA_id, true);

                    d.isError = false;
                    d.data = "Se han Borrado los datos OK.";
                }
                else
                {
                    d.isError = true;
                    d.data = "No se puede borrar registros con fecha diferente a la fecha del Parte Diario";
                }
            }
            catch (Exception e)
            {
                d.isError = true;
                d.data = "Error al Borrar";

            }
            return d;
        }

        #endregion

        #region Calculo de Avance de una Actividad
        /// <summary>
        /// Se calcula el avance de una tarea, como el acumulado, segun la ultima modificacion de un parte diario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ReturnData CalcularAvanceActividad(int id, int pPlanificacion_Proyecto_ActividadesId, bool pGrabar)
        {
            //Para el calcular el acumulado de las cantidad y el % de Avance
            //debo buscar todos los registros que se cargaron en la tabla partediario_actividades_contratista
            ReturnData r = new ReturnData();
            List<ParteDiario_Actividades_Contratista> Lista = new List<ParteDiario_Actividades_Contratista>();
            try
            {
                float mTotal = 0;
                float mAvance = 0;
                float mAcumulado = 0;
                int mCantidadRegMayores0 = 0;

                //27/04/2021 NUEVA FORMA DE CALCULAR EL AVANCE
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    var query = "SELECT sum(b.Cantidad) as Acumulado, sum( if (b.cantidad = 0, 0, 1)) as CANTIDAD  ";
                    var mFrom = " FROM partediario_actividades as a, partediario_actividades_contratista as b ";
                    var mWhere = " where a.Planificacion_Proyecto_ActividadesId = " +
                        pPlanificacion_Proyecto_ActividadesId + " AND b.ParteDiario_ActividadesId = a.id;";
                    command.CommandText = query + mFrom + mWhere;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        mAcumulado = reader.GetFloat(0);
                        mCantidadRegMayores0 = reader.GetInt32(1);
                    }
                }

                //Busco cuánto es la Cantidad que tenia planificada
                ParteDiario_Actividades oPDA = new ParteDiario_ActividadesNegocio()
                    .Get_One_ParteDiario_Actividades(id);
                if (oPDA != null)
                {
                    mTotal = oPDA.Planificacion_Proyecto_Actividades.Cantidad;
                    //FORMULA DEL AVANCE Acumulado/Total*100
                    mAvance = mAcumulado / mTotal * 100;
                    oPDA.Avance = mAvance;

                    if (pGrabar)
                    {
                        //Actualizo la tabla de partediario_actividades
                        var IdPDA = new ParteDiario_ActividadesNegocio().Update(oPDA);
                        //Actualizo la tabla de planificacion_proyecto_actividades
                        Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                            .Get_X_Id(oPDA.Planificacion_Proyecto_ActividadesId);
                        oPPA.Avance = mAvance;
                        //Calculo cuantos registros tengo de esa actividad con avance mayor a 0,
                        //para de esta forma saber si es la primera vez que se le carga avance
                        int mRegConAvance = mCantidadRegMayores0;  //lstPDA.Where(p => p.Avance > 0).Count();
                        //Si la lista tiene un solo registro, quiere decir que es el primero,
                        //asi que actualizo tambien la fecha real de Inicio de la Actividad
                        bool discriminaAvance = mAvance == 0 ? false : true;
                        if (mRegConAvance == 1 && oPPA.Fecha_Real_Incio == DateTime.MinValue)
                            oPPA.Fecha_Real_Incio = oPDA.ParteDiario.Fecha_Creacion;
                        // 20212806: como se identificaron registros con avance pero sin fecha_real_inicio,
                        // mediante el siguiente IF se procede a la corrección de los mismo.
                        else if (mRegConAvance > 1 && oPPA.Fecha_Real_Incio == DateTime.MinValue)
                        {
                            ParteDiario_Actividades oPDA_PrimeroConAvance = new ParteDiario_ActividadesNegocio()
                                .Get_X_IdPlanProyActvidad_PrimeroConAvance(oPPA.Id);
                            oPPA.Fecha_Real_Incio = oPDA_PrimeroConAvance.ParteDiario.Fecha_Creacion;
                            discriminaAvance = true;
                        }
                        //Si la lista no tiene registros, actualizo tambien la fecha real
                        //de Inicio de la Actividad en minvalue (Esto puede ser que este borrando una actividad contratista.
                        if (mAvance == 0)
                            oPPA.Fecha_Real_Incio = DateTime.MinValue;
                        new Planificacion_Proyecto_ActividadesNegocio().Update(oPPA);
                        if (oPPA.Fecha_Real_Incio > DateTime.MinValue && oPPA.Fecha_Est_Incio != oPPA.Fecha_Real_Incio)
                        {
                            int cantidadDiasMovimiento = (int)(oPPA.Fecha_Real_Incio - oPPA.Fecha_Est_Incio).TotalDays;
                            ResultActualizacionArbolPPA oRAAPPA = new ResultActualizacionArbolPPA();
                            oRAAPPA.IdPlanProyActividad = oPPA.Id;
                            oRAAPPA.IdPlanProyOrigenMovimiento = oPPA.Id;
                            oRAAPPA.FecEstInicio_Nueva = oPPA.Fecha_Real_Incio;
                            oRAAPPA.FecEstFin_Nueva_Original = oPPA.Fecha_Est_Fin.AddDays(cantidadDiasMovimiento);
                            oRAAPPA.FecEstFin_Nueva = oPPA.Fecha_Est_Fin.AddDays(cantidadDiasMovimiento);
                            oRAAPPA.CambioAplicable = true;
                            oRAAPPA.message = string.Empty;
                            oRAAPPA.lPlanProyActividades_Resultante = new List<Planificacion_Proyecto_Actividades>();
                            oRAAPPA = new Planificacion_Proyecto_ActividadesNegocio()
                                .AplicarActualizacionCascada(oRAAPPA, discriminaAvance);
                        }
                        else if (oPDA.ParteDiario.Fecha_Creacion > oPPA.Fecha_Est_Fin)
                        {
                            ResultActualizacionArbolPPA oRAAPPA = new ResultActualizacionArbolPPA();
                            oRAAPPA.IdPlanProyActividad = oPPA.Id;
                            oRAAPPA.IdPlanProyOrigenMovimiento = oPPA.Id;
                            oRAAPPA.FecEstInicio_Nueva = oPPA.Fecha_Est_Incio;
                            oRAAPPA.FecEstFin_Nueva_Original = oPDA.ParteDiario.Fecha_Creacion;
                            oRAAPPA.FecEstFin_Nueva = oPDA.ParteDiario.Fecha_Creacion;
                            oRAAPPA.CambioAplicable = true;
                            oRAAPPA.message = string.Empty;
                            oRAAPPA.lPlanProyActividades_Resultante = new List<Planificacion_Proyecto_Actividades>();
                            oRAAPPA = new Planificacion_Proyecto_ActividadesNegocio()
                                .AplicarActualizacionCascada(oRAAPPA, discriminaAvance);
                        }
                    }
                }

                r.data = mAcumulado;
                r.isError = false;
            }
            catch (Exception e)
            {
                r.data = Lista;
                r.isError = false;
            }
            return r;
        }
        #endregion

        #region Incidentes

        public ActionResult Incidentes()
        {
            return View();
        }
        public ActionResult _IncidentesGrilla()
        {
            List<Incidentes> Lista = new List<Incidentes>();
            Lista = new IncidentesNegocio().Get_All_Incidentes();
            return PartialView(Lista);
        }

        public ActionResult _IncidentesAbm(int id)
        {
            ItemIncidentesFile data = new ItemIncidentesFile();
            if (id != 0)
            {
                data.inc = new IncidentesNegocio().Get_One_Incidentes(id);
            }
            else
            {
                data.inc = new Incidentes();
            }
            data.IncidentesTipo = new IncidentesTipoNegocio().Get_All_IncidentesTipo();

            return PartialView(data);
        }

        public ActionResult _IncidentesArchivos(int id)
        {
            List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio()
                .Get_Archivos_Adjuntos_Relacion("INC", id);
            return PartialView(Laar);
        }

        public ActionResult _ParteDiarioArchivos(int id)
        {
            List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio()
                .Get_Archivos_Adjuntos_Relacion("PINC", id);
            return PartialView(Laar);
        }

        public ActionResult _GetArchivos_IncidenteHistorial(int id)
        {
            try
            {
                List<Archivos_Adjuntos_Relacion> lAARelacion = new Archivos_Adjuntos_RelacionNegocio()
                    .Get_Archivos_Adjuntos_Relacion("AIH", id);
                return PartialView(lAARelacion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ParteDiarioController: _GetArchivos_IncidenteHistorial: exception.", ex);
            }
        }

        public ActionResult _GetArchivos_ParteDiarioIncidente(int id)
        {
            try
            {
                List<Archivos_Adjuntos_Relacion> lAARelacion = new Archivos_Adjuntos_RelacionNegocio()
                    .Get_Archivos_Adjuntos_Relacion("PINC", id);
                return PartialView(lAARelacion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ParteDiarioController: GetArchivos_ParteDiarioIncidente: exception.", ex);
            }
        }

        public ActionResult _ParteDiarioArchivosContratistas(int id)
        {
            List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio()
                .Get_Archivos_Adjuntos_Relacion("PD", id);
            return PartialView(Laar);
        }

        [HttpGet]
        public ReturnData ParteDiarioArchivosContratistas2(int id)
        {
            ReturnData r = new ReturnData();
            List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio().Get_Archivos_Adjuntos_Relacion("PD", id);
            r.data = Laar;
            r.isError = false;

            return r;
        }

        [HttpGet]
        public ReturnData ParteDiarioArchivos2(int id)
        {
            ReturnData r = new ReturnData();
            List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio().Get_Archivos_Adjuntos_Relacion("PINC", id);
            r.data = Laar;
            r.isError = false;

            return r;
        }

        [HttpGet]
        public ReturnData ParteDiarioArchivosDelete(int id)
        {
            ReturnData r = new ReturnData();
            Archivos_Adjuntos_Relacion Laar = new Archivos_Adjuntos_RelacionNegocio()
                .Get_One_Archivos_Adjuntos_Relacion(id);
            new Archivos_Adjuntos_RelacionNegocio().Delete(Laar);
            r.data = Laar;
            r.isError = false;
            return r;
        }

        [HttpPost]
        public ReturnData IncidenteGraba(ItemIncidentesGraba data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                if (data.inc.Descripcion != null)
                {
                    data.inc.Descripcion = ToolsNegocio.EliminaEnter(data.inc.Descripcion);
                }
                EF.Models.Incidentes inc = new EF.Models.Incidentes();
                inc.Criticidad = data.inc.Criticidad;
                inc.Descripcion = data.inc.Descripcion;
                inc.GeneraEmail = data.inc.GeneraEmail;
                inc.Id = data.inc.Id;
                inc.ListaEmail = data.inc.ListaEmail;
                inc.Nombre = data.inc.Nombre;
                //inc.RolResponsable = data.inc.RolResponsable;
                inc.TipoIncidenteId = data.inc.TipoIncidenteId;
                inc.GeneraSegIncidente = data.inc.GeneraSegIncidente;
                inc.Rectype = data.inc.Rectype;
                inc.RolResponsable = data.RolResponsable2;
                int idc;
                try
                {

                    if (data.inc.Id > 0)
                    {
                        idc = new IncidentesNegocio().Update(inc);
                    }
                    else
                    {
                        idc = new IncidentesNegocio().Insert(inc);
                    }
                }
                catch (Exception e)
                {

                    throw;
                }


                if (idc > 0)
                {
                    //Cargamos FOTOS 
                    if (data.Archivos != null && data.Archivos.Count > 0)
                    {

                        foreach (IFormFile photo in data.Archivos)
                        {
                            string Folder = Path.Combine(_env.WebRootPath, "archivos/INC/");

                            string nf = photo.FileName;
                            var nf1 = nf.Split(".");
                            var nl = nf1.Length;
                            string nfe = nf1[(nl - 1)];
                            string f = ToolsNegocio.CrearAlfaNumerico(4);
                            string mNombreSinExtencion = nf.Replace(nfe, "");
                            mNombreSinExtencion = mNombreSinExtencion.Replace(' ', '_');
                            mNombreSinExtencion = mNombreSinExtencion.Replace('.', '_');
                            string mName = mNombreSinExtencion;
                            string fileName1 = "inc_" + mName + "_" + idc + "." + nfe;
                            string filePath = Path.Combine(Folder, fileName1);
                            photo.CopyTo(new FileStream(filePath, FileMode.Create));

                            Archivos_Adjuntos aa = new Archivos_Adjuntos();
                            aa.Archivo = fileName1;
                            aa.Extencion = nfe.ToUpper();
                            aa.Fecha = DateTime.Now;
                            aa.IdUsuario = UsuarioLogin().Id;
                            aa.sUsuario = UsuarioLogin().Nombre + " " + UsuarioLogin().Apellido;
                            aa.URL = "../archivos/INC/";
                            int IdAA = new Archivos_AdjuntosNegocio().Insert(aa);

                            Archivos_Adjuntos_Relacion aar = new Archivos_Adjuntos_Relacion();
                            aar.Archivos_AdjuntosId = IdAA;
                            aar.Entidad = "INC";
                            aar.IdEntidad = idc;

                            new Archivos_Adjuntos_RelacionNegocio().Insert(aar);
                        }
                    }

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



        #endregion

        #region Graba asistencia y Parte Diario Incidentes

        [HttpGet]
        public ReturnData ParteDiarioAsistenciaGet(int id)
        {
            ReturnData d = new ReturnData();
            List<ParteDiario_Asistencia> ListaRet = new List<ParteDiario_Asistencia>();
            List<ParteDiario_Asistencia> ppa = new ParteDiario_AsistenciaNegocio().Get_All_ParteDiario_AsistenciaPA(id);
            foreach (var e in ppa)
            {
                ParteDiario_Asistencia pa = new ParteDiario_Asistencia();
                pa = e;
                pa.SContratista = new ContratistasNegocio().Get_One_Contratistas(e.ContratistasId).Nombre;
                ListaRet.Add(pa);
            }

            //Calcular Asistencia Pucheta
            var mCantAsig = ListaRet.Where(p => p.ContratistasId == 1).Sum(p => p.Asig_Propios).ToString();
            var mCantPres = ListaRet.Where(p => p.ContratistasId == 1).Sum(p => p.Asig_Propios_Presentes).ToString();


            string mAsistPucheta = "<b>Empleados Propios:</b>" + mCantAsig + "/" + mCantPres;

            string mAsistOtros = "<b>Empleados Terceros:</b>" + ListaRet.Where(p => p.ContratistasId != 1).Sum(x => x.Asig_Propios).ToString() + "/" + ListaRet.Where(p => p.ContratistasId != 1).Sum(x => x.Asig_Propios_Presentes).ToString();

            d.data1 = mAsistPucheta + " - " + mAsistOtros;
            d.data = ListaRet;
            d.isError = false;
            return d;
        }



        [HttpPost]
        public ReturnData ParteDiarioAsistenciaGraba(ParteDiario_Asistencia data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {

                try
                {
                    ParteDiario_Asistencia pdac = new ParteDiario_AsistenciaNegocio()
                        .Get_All_ParteDiario_AsistenciaPA(data.ParteDiarioId)
                        .FirstOrDefault(x => x.ContratistasId == data.ContratistasId);
                    if (pdac == null)
                    {
                        if (data.ContratistasId > 0)
                        {
                            if (data.Asig_Comentario != null)
                            {
                                data.Asig_Comentario = ToolsNegocio.EliminaEnter(data.Asig_Comentario);
                            }
                            new ParteDiario_AsistenciaNegocio().Insert(data);

                            List<ParteDiario_Asistencia> ListaRet = new List<ParteDiario_Asistencia>();
                            List<ParteDiario_Asistencia> ppa = new ParteDiario_AsistenciaNegocio().Get_All_ParteDiario_AsistenciaPA(data.ParteDiarioId);
                            foreach (var e in ppa)
                            {
                                ParteDiario_Asistencia pa = new ParteDiario_Asistencia();
                                pa = e;
                                pa.SContratista = new ContratistasNegocio().Get_One_Contratistas(e.ContratistasId).Nombre;
                                pa.Asig_Contratista = (data.Asig_Propios - data.Asig_Propios_Presentes);
                                ListaRet.Add(pa);
                            }
                            d.data = "Grabado ok";
                            d.data1 = ListaRet;
                            d.isError = false;
                        }
                        else
                        {
                            d.data = "Debe Asignar un contratista.";
                            d.isError = true;
                        }
                    }
                    else
                    {
                        d.data = "Ya existe un registros de asistencia para este Contratista.";
                        d.isError = true;
                    }
                }
                catch (Exception err)
                {
                    d.data = err.ToString() + "Error al grabar";
                    d.isError = true;
                }
            }
            else
            {
                d.isError = true;
                d.data = "Error al Grabar";
            }
            return d;
        }


        #region PArte Diario Incidentes

        [HttpGet]
        public ReturnData ParteDiarioIncidentesGet(int id)
        {

            ReturnData d = new ReturnData();
            List<ParteDiario_Incidentes> ListaReturn = new List<ParteDiario_Incidentes>();
            List<ParteDiario_Incidentes> pdi = new ParteDiario_IncidentesNegocio().Get_All_ParteDiario_IncidentesPA(id);
            foreach (var e in pdi)
            {
                ParteDiario_Incidentes pa = new ParteDiario_Incidentes();
                pa = e;

                if (e.IncidenteId > 0)
                    pa.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;
                else pa.sIncidente = "Sin Incidente asignado";

                #region Cargar los datos de la NoConformidad para Saber tambien los datos del Contratista
                if (e.ContratistaId != 0)
                {
                    pa.sContratista = new ContratistasNegocio().Get_One_Contratistas(e.ContratistaId).Nombre;
                }
                else
                {
                    pa.sContratista = "";
                }
                #endregion

                ListaReturn.Add(pa);
            }

            d.data = ListaReturn;
            #region Solicitado Por
            d.data1 = new UsuariosNegocio().Get_Usuario(UsuarioLogin().Id).NombreYApellido;
            #endregion
            d.isError = false;
            return d;
        }


        [HttpGet]
        public ReturnData ParteDiarioIncidentesHistorialGet(int id)
        {

            ReturnData d = new ReturnData();
            List<Incidentes_Historial> ListaReturn = new List<Incidentes_Historial>();
            List<Incidentes_Historial> IHPD = new Incidentes_HistorialNegocio().Get_All_Incidentes_HistorialPD(id);
            foreach (var e in IHPD)
            {
                Incidentes_Historial pa = new Incidentes_Historial();
                pa = e;
                pa.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;
                pa.sUsuarioDueño = new UsuariosNegocio().Get_Usuario(pa.Creacion_UsuarioId).ApellidoYNombre;

                if (e.ProyectoId != 0)
                {
                    pa.sProyecto = new ProyectoNegocio().Get_One_Proyecto(e.ProyectoId).Nombre;
                }
                else
                {
                    pa.sProyecto = "";
                }

                if (e.ContratistasId != 0)
                {
                    pa.sContratista = new ContratistasNegocio().Get_One_Contratistas(e.ContratistasId).Nombre;
                }
                else
                {
                    pa.sContratista = "";
                }
                if (e.AreaId != 0)
                    pa.sAreaActual = new RolesNegocio().Get_One_Roles(e.AreaId).Detalle;
                else
                {
                    pa.sAreaActual = "";
                }

                ListaReturn.Add(pa);
            }

            d.data = ListaReturn;
            #region Solicitado Por
            d.data1 = new UsuariosNegocio().Get_Usuario(UsuarioLogin().Id).NombreYApellido;
            #endregion
            d.isError = false;
            return d;
        }

        [HttpPost]
        public ReturnData ParteDiarioIncidentesGraba(ItemParteDiarioIncidenteGraba data)
        {
            ReturnData d = new ReturnData();
            if (data != null)
            {
                try
                {
                    #region 1-Genero el Registro en IncidentesHistorial
                    //Verifico si el Incidente tiene que generar un registro en Incidente_Historial
                    Incidentes oIncidente = new IncidentesNegocio().Get_One_Incidentes(data.IncidenteId);
                    if (oIncidente != null)
                    {
                        if (oIncidente.GeneraSegIncidente)
                        {
                            #region 1.1-Busco el Dato del Parte Diario
                            ParteDiario oPD = new ParteDiarioNegocio().Get_One_ParteDiario(data.ParteDiarioId);
                            #endregion

                            #region 1.2-Grabo el registro en la tabla de Incidentes HIstorial
                            Incidentes_Historial oIH = new Incidentes_Historial();
                            oIH.ContratistasId = data.ContratistaId;
                            oIH.Creacion_Descripcion = data.Comentario;
                            oIH.Creacion_Fecha = oPD.Fecha_Creacion;
                            oIH.EstadoId = 1;
                            oIH.IncidenteId = data.IncidenteId;
                            oIH.ProyectoId = oPD.ProyectoId;
                            try
                            {
                                var mIH_ID = new Incidentes_HistorialNegocio().Insert(oIH);
                                data.NoConformidadId = mIH_ID;
                            }
                            catch (Exception)
                            {
                                data.NoConformidadId = 0;
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region 2-Grabo el Registro del Incidente de la Obra

                    if (data.Comentario != null)
                    {
                        data.Comentario = ToolsNegocio.EliminaEnter(data.Comentario);
                    }
                    ParteDiario_Incidentes oPDI = new ParteDiario_Incidentes();
                    oPDI.Comentario = data.Comentario;
                    oPDI.ContratistaId = data.ContratistaId;
                    oPDI.Id = data.Id;
                    oPDI.IncidenteId = data.IncidenteId;
                    oPDI.NoConformidadId = data.NoConformidadId;
                    oPDI.ParteDiarioId = data.ParteDiarioId;
                    oPDI.sContratista = data.sContratista;
                    oPDI.sIncidente = data.sIncidente;
                    oPDI.SolicitadoPor = data.SolicitadoPor;
                    oPDI.Criticidad = data.Criticidad;
                    int IdPI = new ParteDiario_IncidentesNegocio().Insert(oPDI);

                    #region CODIGO SUSPENDIDO
                    //if (data.Archivos != null && data.Archivos.Count > 0)
                    //{
                    //    foreach (IFormFile photo in data.Archivos)
                    //    {
                    //        string Folder = Path.Combine(_env.WebRootPath, "archivos/PINC/");

                    //        string nf = photo.FileName;
                    //        var nf1 = nf.Split(".");
                    //        var nl = nf1.Length;
                    //        string nfe = nf1[(nl - 1)];
                    //        string f = ToolsNegocio.CrearAlfaNumerico(4);

                    //        string mNombreSinExtencion = nf.Replace(nfe, "");
                    //        mNombreSinExtencion = mNombreSinExtencion.Replace(' ', '_');
                    //        mNombreSinExtencion = mNombreSinExtencion.Replace('.', '_');
                    //        string mName = mNombreSinExtencion;
                    //        string fileName1 = "pinc_" + mName + "_" + IdPI + "." + nfe;
                    //        string filePath = Path.Combine(Folder, fileName1);
                    //        photo.CopyTo(new FileStream(filePath, FileMode.Create));

                    //        Archivos_Adjuntos aa = new Archivos_Adjuntos();
                    //        aa.Archivo = fileName1;
                    //        aa.Extencion = nfe.ToUpper();
                    //        aa.Fecha = DateTime.Now;
                    //        aa.IdUsuario = UsuarioLogin().Id;
                    //        aa.sUsuario = UsuarioLogin().Nombre + " " + UsuarioLogin().Apellido;
                    //        aa.URL = "../archivos/PINC/";
                    //        int IdAA = new Archivos_AdjuntosNegocio().Insert(aa);

                    //        Archivos_Adjuntos_Relacion aar = new Archivos_Adjuntos_Relacion();
                    //        aar.Archivos_AdjuntosId = IdAA;
                    //        aar.Entidad = "PINC";
                    //        aar.IdEntidad = IdPI;

                    //        new Archivos_Adjuntos_RelacionNegocio().Insert(aar);
                    //    }
                    //}
                    #endregion
                    if (data.lIdsArchivosAdjuntos != null)
                    {
                        data.lIdsArchivosAdjuntos.ForEach(id =>
                        {
                            Archivos_Adjuntos_Relacion oAARelacion = new Archivos_Adjuntos_Relacion();
                            oAARelacion.Archivos_AdjuntosId = id;
                            oAARelacion.Entidad = "PINC";
                            oAARelacion.IdEntidad = IdPI;
                            new Archivos_Adjuntos_RelacionNegocio().Insert(oAARelacion);
                            //Movemos el archivo adjunto de la carpeta temporal a la carpeta que necesitamos
                            Archivos_Adjuntos oArchivoAdjunto = new Archivos_AdjuntosNegocio()
                                    .Get_One_Archivos_Adjuntos(id);
                            string pathActual = Path.Combine(_env.WebRootPath, oArchivoAdjunto.URL + oArchivoAdjunto.Archivo);
                            string pathNuevo = Path.Combine(_env.WebRootPath, ValoresUtiles.PathArchivos_Novedades + oArchivoAdjunto.Archivo);
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
                                    oArchivoAdjunto.URL = ValoresUtiles.PathArchivos_Novedades;
                                    new Archivos_AdjuntosNegocio().Update(oArchivoAdjunto);
                                }
                            }
                        });
                    }

                    #endregion

                    #region 3-Genero una lista de Resiltado para devolver
                    List<ParteDiario_Incidentes> ListaReturn = new List<ParteDiario_Incidentes>();
                    List<ParteDiario_Incidentes> pdi = new ParteDiario_IncidentesNegocio().Get_All_ParteDiario_IncidentesPA(data.ParteDiarioId);
                    foreach (var e in pdi)
                    {
                        ParteDiario_Incidentes pa = new ParteDiario_Incidentes();
                        pa = e;
                        if (e.IncidenteId > 0)
                            pa.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;
                        else pa.sIncidente = "Sin Incidente asignado.";

                        List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio().Get_Archivos_Adjuntos_Relacion("PINC", data.Id);
                        if (Laar.Count > 0)
                        {
                            data.IsArchivosIncidentes = true;
                        }
                        else
                        {
                            data.IsArchivosIncidentes = false;
                        }

                        #region Cargar los datos de la NoConformidad para Saber tambien los datos del Contratista
                        if (e.ContratistaId != 0)
                        {
                            pa.sContratista = new ContratistasNegocio().Get_One_Contratistas(e.ContratistaId).Nombre;
                        }
                        else
                        {
                            pa.sContratista = "";
                        }



                        #endregion

                        ListaReturn.Add(pa);
                    }
                    d.data = "Grabado ok";
                    d.data1 = ListaReturn;
                    d.isError = false;
                    #endregion
                }
                catch (Exception e)
                {

                    d.data = "Error al grabar";
                    d.isError = true;
                }
            }
            else
            {
                d.isError = true;
                d.data = "Error al Grabar";
            }
            return d;
        }

        [HttpPost]
        public ReturnData ParteDiarioGrabaIncidentes(ItemParteDiarioGrabaIncidente data)
        {
            ReturnData d = new ReturnData();
            if (data != null)
            {
                try
                {
                    if (data.ComentarioHI != null)
                    {
                        data.ComentarioHI = ToolsNegocio.EliminaEnter(data.ComentarioHI);
                    }
                    Incidentes_Historial Ih = new Incidentes_Historial();
                    Ih.Creacion_Descripcion = data.ComentarioHI;
                    Ih.ContratistasId = data.ContratistaId;
                    Ih.Id = data.Id;
                    Ih.IncidenteId = data.IncidenteId;
                    Ih.ParteDiarioId = data.ParteDiarioId;
                    Ih.sContratista = data.sContratista;
                    Ih.sIncidente = data.sIncidente;
                    Ih.sUsuarioDueño = getUsuarioActual().ApellidoNombre;
                    Ih.Creacion_UsuarioId = getUsuarioActual().Id;
                    Ih.Creacion_Fecha = DateTime.Now;
                    Ih.Fecha_Cierre = data.FechaCierre;
                    Ih.sAreaActual = data.sAreaActual;
                    Ih.AreaId = data.AreaId;
                    Ih.ProyectoId = data.ProyectoId;
                    Ih.sProyecto = data.sProyecto;
                    Ih.EstadoId = 2;
                    //ENVIO DE MAIL
                    EnvioMailIncidente(Ih, true);

                    int IdPI = new Incidentes_HistorialNegocio().Insert(Ih);

                    #region CODIGO SUSPENDIDO
                    ////Cargamos FOTOS 
                    //if (data.Archivos != null && data.Archivos.Count > 0)
                    //{

                    //    foreach (IFormFile photo in data.Archivos)
                    //    {
                    //        string Folder = Path.Combine(_env.WebRootPath, "archivos/PINC/");

                    //        string nf = photo.FileName;
                    //        var nf1 = nf.Split(".");
                    //        var nl = nf1.Length;
                    //        string nfe = nf1[(nl - 1)];
                    //        string f = ToolsNegocio.CrearAlfaNumerico(4);
                    //        string mNombreSinExtencion = nf.Replace(nfe, "");
                    //        mNombreSinExtencion = mNombreSinExtencion.Replace(' ', '_');
                    //        mNombreSinExtencion = mNombreSinExtencion.Replace('.', '_');
                    //        string mName = mNombreSinExtencion;
                    //        string fileName1 = "pinc_" + mName + "_" + IdPI + "." + nfe;
                    //        string filePath = Path.Combine(Folder, fileName1);
                    //        photo.CopyTo(new FileStream(filePath, FileMode.Create));

                    //        Archivos_Adjuntos aa = new Archivos_Adjuntos();
                    //        aa.Archivo = fileName1;
                    //        aa.Extencion = nfe.ToUpper();
                    //        aa.Fecha = DateTime.Now;
                    //        aa.IdUsuario = UsuarioLogin().Id;
                    //        aa.sUsuario = UsuarioLogin().Nombre + " " + UsuarioLogin().Apellido;
                    //        aa.URL = "../archivos/PINC/";
                    //        int IdAA = new Archivos_AdjuntosNegocio().Insert(aa);

                    //        Archivos_Adjuntos_Relacion aar = new Archivos_Adjuntos_Relacion();
                    //        aar.Archivos_AdjuntosId = IdAA;
                    //        aar.Entidad = "AIH";
                    //        aar.IdEntidad = IdPI;

                    //        new Archivos_Adjuntos_RelacionNegocio().Insert(aar);
                    //    }
                    //}
                    #endregion
                    if (data.lIdsArchivosAdjuntos != null)
                    {
                        data.lIdsArchivosAdjuntos.ForEach(id =>
                        {
                            Archivos_Adjuntos_Relacion oAARelacion = new Archivos_Adjuntos_Relacion();
                            oAARelacion.Archivos_AdjuntosId = id;
                            oAARelacion.Entidad = "AIH";
                            oAARelacion.IdEntidad = IdPI;
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
                    #region devuelvo datos

                    List<Incidentes_Historial> ListaRet = new List<Incidentes_Historial>();
                    List<Incidentes_Historial> hid = new Incidentes_HistorialNegocio().Get_All_Incidentes_HistorialPD(data.ParteDiarioId);
                    foreach (var e in hid)
                    {
                        Incidentes_Historial hi = new Incidentes_Historial();
                        hi = e;
                        hi.sUsuarioDueño = new UsuariosNegocio().Get_Usuario(hi.Creacion_UsuarioId).ApellidoYNombre;

                        if (e.ContratistasId != 0)
                            hi.sContratista = new ContratistasNegocio().Get_One_Contratistas(e.ContratistasId).Nombre;
                        else
                            hi.sContratista = "";
                        if (e.ProyectoId != 0)
                            hi.sProyecto = new ProyectoNegocio().Get_One_Proyecto(e.ProyectoId).Nombre;
                        else
                        {
                            hi.sProyecto = "";
                        }
                        hi.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;

                        if (e.AreaId != 0)
                            hi.sAreaActual = new RolesNegocio().Get_One_Roles(e.AreaId).Detalle;
                        else
                        {
                            hi.sAreaActual = "";
                        }

                        if (e.IncidenteId != 0)
                            hi.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;
                        else
                        {
                            hi.sIncidente = "ERROR - Sin Incidente";
                        }

                        List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio().Get_Archivos_Adjuntos_Relacion("PINC", data.Id);
                        if (Laar.Count > 0)
                        {
                            data.IsArchivosIncidentes = true;
                        }
                        else
                        {
                            data.IsArchivosIncidentes = false;
                        }

                        ListaRet.Add(hi);
                    }
                    d.data = "Grabado ok";
                    d.data1 = ListaRet;
                    d.isError = false;

                    #endregion

                    #region GEnero Lista para devolver
                    //d = ParteDiarioIncidentesHistorialGet(data.Id);
                    #endregion
                }
                catch (Exception)
                {
                    d.data = "Error al grabar";
                    d.isError = true;
                }
            }
            else
            {
                d.isError = true;
                d.data = "Error al Grabar";
            }
            return d;
        }

        [HttpPost]
        public ReturnData IncidentesHistorialBorra(int id)
        {
            ReturnData d = new ReturnData();
            int idc;
            try
            {
                ParteDiario_Incidentes mPDI = new ParteDiario_IncidentesNegocio().Get_One_ParteDiario_Incidentes(id);

                if (mPDI != null)
                    idc = new ParteDiario_IncidentesNegocio().Delete(mPDI);

                d.isError = false;
                d.data = "Se han Borrado los datos OK.";
            }
            catch (Exception)
            {
                d.isError = true;
                d.data = "Error al Borrar";

            }
            return d;
        }

        #endregion
        //BorraAsistencia

        [HttpPost]
        public ReturnData AsistenciaBorra(int id)
        {
            ReturnData d = new ReturnData();
            int idc;
            try
            {
                ParteDiario_Asistencia pda = new ParteDiario_Asistencia();

                ParteDiario_Asistencia pdab = new ParteDiario_AsistenciaNegocio().Get_One_ParteDiario_Asistencia(id);
                idc = new ParteDiario_AsistenciaNegocio().Delete(pdab);


                d.isError = false;
                d.data = "Se han Borrado los datos OK.";
            }
            catch (Exception)
            {
                d.isError = true;
                d.data = "Error al Borrar";

            }
            return d;
        }

        #endregion

        #region DataSelect

        [HttpGet]
        public ReturnData GetIncidente()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new IncidentesNegocio().Get_All_Incidentes();

            return d;
        }


        [HttpGet]
        public ReturnData GetContratistas(int id)
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new Proyecto_ContratistaNegocio().Get_All_Proyecto_Contratista(id);
            return d;
        }

        [HttpGet]
        public ReturnData GetCalidad()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new Calidad_ItemsNegocio().Get_All_Calidad_Items();

            return d;
        }


        [HttpGet]
        public ReturnData GetRubros()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new Planificacion_Actividades_RubroNegocio().Get_All_Planificacion_Actividades_Rubro();
            return d;
        }

        [HttpGet]
        public ReturnData GetUbicaciones()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new Proyecto_UbicacionesNegocio().Get_All_Proyecto_Ubicaciones();
            return d;
        }


        [HttpGet]

        public ReturnData GetProyecto()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new ProyectoNegocio().Get_All_Proyecto();

            return d;
        }


        [HttpGet]
        public ReturnData GetAreasEmpresa()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new RolesNegocio().Get_All_Roles()
                .Where(p => p.Borrado == false && p.Activo == true && p.Id != ValoresUtiles.IdRol_AdminFull)
                .OrderBy(p => p.Detalle)
                .ToList();
            return d;
        }


        [HttpPost]
        public ReturnData IncidentesBorra(int id)
        {
            ReturnData d = new ReturnData();
            int idc;
            try
            {
                ParteDiario_Incidentes pdi = new ParteDiario_Incidentes();

                ParteDiario_Incidentes pdib = new ParteDiario_IncidentesNegocio().Get_One_ParteDiario_Incidentes(id);
                idc = new ParteDiario_IncidentesNegocio().Delete(pdib);


                d.isError = false;
                d.data = "Se han Borrado los datos OK.";
            }
            catch (Exception)
            {
                d.isError = true;
                d.data = "Error al Borrar";

            }
            return d;
        }


        #endregion

        #region Parte Diario Resumen
        public ActionResult _ParteDiarioResumen(int id)
        {
            int idParteDiario = id;
            Res_PD_Resumen oPD_R = new Res_PD_Resumen();
            oPD_R.ParteDiarioId = idParteDiario;

            #region 1-Busco el Parte Diario y Cargo los datos del Proyecto
            //ParteDiario oPD = new ParteDiarioNegocio().Get_One_ParteDiario(id);
            ParteDiario oPD = new ParteDiarioNegocio().Get_One_ParteDiario_SinInclude(idParteDiario);


            oPD_R.ParteDiario_Fecha = oPD.Fecha_Creacion.ToShortDateString();
            oPD_R.ProyectoId = oPD.ProyectoId;
            Proyecto oProyecto = new ProyectoNegocio().Get_One_Proyecto(oPD.ProyectoId);
            oPD_R.ProyectoNombre = oProyecto.Nombre;

            #endregion

            #region 2-Cargo las Actividades de Contratistas que se cargaron ese dia
            oPD_R.lstActContratistas = ParteDiario_Resumen_Actividad(oPD, oPD.ProyectoId);
            #endregion

            #region 3-Cargo la Asistencia del Dia
            oPD_R.lstAsistContratista = ParteDiario_Resumen_Asistencia(oPD);
            #endregion

            #region 4-Cargo las Novedades
            oPD_R.lstIncidentes = ParteDiario_Resumen_Incidentes(oPD);

            #endregion

            #region 5-Cargo los Incidentes Historial
            oPD_R.lstIncidentesHistorial = ParteDiario_Resumen_incidentesH(oPD);

            #endregion

            #region 6 - Cargo las Ubicaciones de ese proyecto para le filtro
            oPD_R.lstDistinctUbicacion = oPD_R.lstActContratistas.OrderBy(p => p.sUbicacion).Select(o => o.sUbicacion).Distinct().ToList();
            #endregion

            #region 7 - Cargos Dinstintos Contratistas
            oPD_R.lstDistinctContratistas = oPD_R.lstActContratistas.OrderBy(p => p.sContratistas).Select(o => o.sContratistas).Distinct().ToList();
            #endregion

            #region 8 - Cargos Dinstintos Actividades
            oPD_R.lstDistinctActividades = oPD_R.lstActContratistas.OrderBy(p => p.sActividad).Select(o => o.sActividad).Distinct().ToList();
            #endregion

            #region 9 - Cargo Detalle de los Item de Calidad Verificados
            List<Calidad_Actividades_Valoracion> lstCalidad = new Calidad_Actividades_ValoracionNegocio()
                .Get_ItemCalidadActividadValoracion_ParaGrilla(idParteDiario, DateTime.MinValue, DateTime.MaxValue)
                .OrderByDescending(s => Convert.ToDateTime(s.sFecha))
                .ToList();
            oPD_R.lstItemCalidadValoracion = lstCalidad;
            #endregion

            return PartialView(oPD_R);
        }

        public List<ItemResumen_Actividades_Contratista> ParteDiario_Resumen_Actividad(ParteDiario oPD, int mProyecto)
        {
            // 1- Busco el IdDel ParteDiario anterior a este
            ParteDiario oPD_Anterior = new ParteDiarioNegocio().Get_All_ParteDiario()
                .Where(p => p.ProyectoId == mProyecto && p.Id < oPD.Id)
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();
            int ParteDiarioAnteriorId = 0;
            if (oPD_Anterior != null) ParteDiarioAnteriorId = oPD_Anterior.Id;

            // 2 - Con el idParte diario traigo las actividades
            List<ItemResumen_Actividades_Contratista> lstActividadesContratista =
                new List<ItemResumen_Actividades_Contratista>();

            lstActividadesContratista = new ParteDiario_Actividades_ContratistaNegocio()
                .Get_All_ParteDiario_Actividades_Contratistas_Resumen(oPD.Id, ParteDiarioAnteriorId)
                .ToList();
            List<int> lIdsPPAs = lstActividadesContratista
                .GroupBy(irac => irac.Planificacion_Proyecto_ActividadesId)
                .Select(g => g.First().Planificacion_Proyecto_ActividadesId)
                .ToList();
            List<Planificacion_Proyecto_Actividades> lPPAs = new Planificacion_Proyecto_ActividadesNegocio()
                .Get_X_lIds(lIdsPPAs, false);
            lstActividadesContratista.ForEach(ac =>
                ac.EstadoAltPPA = lPPAs.Find(ppa => ppa.Id == ac.Planificacion_Proyecto_ActividadesId).EstadoAlternativo);
            return lstActividadesContratista.OrderBy(p => p.sUbicacion)
                .ThenBy(p => p.sRubro)
                .ThenBy(p => p.sActividad)
                .ThenBy(p => p.sContratistas)
                .ToList();
        }

        public List<PDResumen_Asistencia_Contratista> ParteDiario_Resumen_Asistencia(ParteDiario oPD)
        {
            List<PDResumen_Asistencia_Contratista> lstAsistenciaContratista = new List<PDResumen_Asistencia_Contratista>();

            List<ParteDiario_Asistencia> lstAsistencia = new ParteDiario_AsistenciaNegocio().Get_All_ParteDiario_AsistenciaPA(oPD.Id).ToList();
            foreach (var item in lstAsistencia)
            {
                PDResumen_Asistencia_Contratista oAsist = new PDResumen_Asistencia_Contratista();

                oAsist.Asignados = item.Asig_Propios.ToString();
                oAsist.Asig_Presentes = item.Asig_Propios_Presentes.ToString();
                oAsist.Comentario = item.Asig_Comentario;
                oAsist.ContratistasId = item.ContratistasId;
                oAsist.Covid = item.Covid_Propioa.ToString();
                Contratistas oC = new ContratistasNegocio().Get_One_Contratistas(item.ContratistasId);
                oAsist.sContratistas = oC.Nombre;

                lstAsistenciaContratista.Add(oAsist);
            }


            return lstAsistenciaContratista.OrderBy(p => p.sContratistas).ToList();
        }

        public List<PDResumen_IncidentesHitorial> ParteDiario_Resumen_incidentesH(ParteDiario oPD)
        {
            List<PDResumen_IncidentesHitorial> lstIncidentesHistorial = new List<PDResumen_IncidentesHitorial>();

            List<Incidentes_Historial> lstHistorial = new Incidentes_HistorialNegocio().Get_All_Incidentes_HistorialPD(oPD.Id).ToList();
            foreach (var item in lstHistorial)
            {
                PDResumen_IncidentesHitorial rih = new PDResumen_IncidentesHitorial();
                rih.Comentario = item.Creacion_Descripcion;
                rih.ContratistaId = item.ContratistasId;
                rih.FechaCierre = item.Fecha_Cierre;
                rih.Id = item.Id;
                rih.sContratista = item.sContratista;
                rih.sIncidente = new IncidentesNegocio().Get_One_Incidentes(item.IncidenteId).Nombre;
                if (item.ContratistasId != 0)
                    rih.sContratista = new ContratistasNegocio().Get_One_Contratistas(item.ContratistasId).Nombre;
                else
                    rih.sContratista = "";
                rih.IncidenteId = item.IncidenteId;
                rih.ParteDiarioId = item.ParteDiarioId;
                rih.mUsuarioActual = new UsuariosNegocio().Get_Usuario(item.Creacion_UsuarioId).ApellidoYNombre;

                lstIncidentesHistorial.Add(rih);
            }


            return lstIncidentesHistorial.OrderBy(p => p.sContratista).ToList();
        }

        public List<PDResumen_Incidentes> ParteDiario_Resumen_Incidentes(ParteDiario oPD)
        {
            List<PDResumen_Incidentes> lstIncidentes = new List<PDResumen_Incidentes>();
            List<ParteDiario_Incidentes> lstPDI = new ParteDiario_IncidentesNegocio().Get_All_ParteDiario_IncidentesPA(oPD.Id);

            if (lstPDI.Count > 0)
            {
                foreach (var item in lstPDI)
                {
                    PDResumen_Incidentes oIn = new PDResumen_Incidentes();
                    oIn.ContratistasId = item.ContratistaId;
                    if (item.ContratistaId != 0)
                        oIn.sContratistas = new ContratistasNegocio().Get_One_Contratistas(item.ContratistaId).Nombre;
                    else
                        oIn.sContratistas = "";
                    oIn.IncidenteId = item.IncidenteId;
                    if (item.IncidenteId != 0)
                    {
                        oIn.sIncidente = new IncidentesNegocio().Get_One_Incidentes(item.IncidenteId).Nombre;
                    }
                    else
                    {
                        oIn.sIncidente = "Error al grabar Incidente";
                    }
                    oIn.sComentario = item.Comentario;
                    oIn.sSolicitadoPor = item.SolicitadoPor;
                    oIn.sCriticidad = item.Criticidad;
                    oIn.ParteDiario_IncidentesId = item.Id;

                    List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio().Get_Archivos_Adjuntos_Relacion("PINC", item.Id);
                    if (Laar.Count > 0)
                    {
                        oIn.IsArchivos = true;
                    }
                    else
                    {
                        oIn.IsArchivos = false;
                    }


                    lstIncidentes.Add(oIn);
                }
            }
            return lstIncidentes.OrderBy(p => p.sIncidente).ToList();
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
                if (pEsNuevo)
                {
                    // Si es nuevo, tengo que agregar los destinatario que estan configurados en el tipo de incidentes
                    if (NoExisteYaElDestinatario(lstDestinatarios, oIncidente.ListaEmail) && oIncidente.ListaEmail != null)
                        lstDestinatarios = lstDestinatarios + "; " + oIncidente.ListaEmail;
                }
                // Si es una modificacion, tengo que verificar si se le esta asignando a una Area en particular y traer los integrantes y enviarles el mail.
                if (data.AreaId != 0)
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

        public ItemLoginUsuario GetUsuarioLogueado()
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

        public List<Informe_Actividad_Vencida> Get_InformeActividadesVencidas()
        {
            try
            {
                ItemLoginUsuario oItemUsuario = GetUsuarioLogueado();
                Usuarios oUsuarioLogueado = new UsuariosNegocio().Get_Usuario(oItemUsuario.Id);
                if (oUsuarioLogueado != null)
                {
                    List<Informe_Actividad_Vencida> lInfActVencidas = new Informe_Actividad_VencidaNegocio()
                        .Get_Informe_Completo_Por_Proyecto(0);
                    return lInfActVencidas;
                }
                else
                {
                    return new List<Informe_Actividad_Vencida>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public ActionResult _GetInformesActividadesVencidas()
        {
            try
            {
                List<Informe_Actividad_Vencida> lInfActVencidas = Get_InformeActividadesVencidas();
                return PartialView(lInfActVencidas);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public partial class Filtro
        {
            public int IdProyecto { get; set; }
            public int IdUbicacion { get; set; }
            public int IdActividad { get; set; }
        }
        [HttpPost]
        public object Get_InformeActividadesVencidas_X_oFiltro(Filtro oFiltro)
        {
            var result = new object();
            try
            {
                ItemLoginUsuario oItemUsuario = GetUsuarioLogueado();
                Usuarios oUsuarioLogueado = new UsuariosNegocio().Get_Usuario(oItemUsuario.Id);
                if (oUsuarioLogueado != null)
                {
                    List<RolesUsuarios> lRolesUsuario = new RolesUsuariosNegocio().GetRolesUsuarios(oUsuarioLogueado.Id);
                    bool esAdmin = false;
                    lRolesUsuario.ForEach(ru => esAdmin = esAdmin ? true : ValoresUtiles.Get_lRolesAdmin().Contains(ru.RolesId));
                    List<Informe_Actividad_Vencida> lInfActVencidas = new Informe_Actividad_VencidaNegocio()
                        .Get_Varios_X_Ids(oFiltro.IdProyecto, oFiltro.IdUbicacion, oFiltro.IdActividad)
                        .OrderBy(iav => iav.ProyectoId)
                        .ThenBy(iav => iav.ProyectoUbicacionId)
                        .ThenBy(iav => iav.PlanActividadId)
                        .ToList();

                    List<int> lIdsProyectos = lInfActVencidas.GroupBy(iav => iav.ProyectoId)
                        .Select(g => g.First().ProyectoId)
                        .ToList();
                    List<int> lIdsUbicaciones = lInfActVencidas.GroupBy(iav => iav.ProyectoUbicacionId)
                       .Select(g => g.First().ProyectoUbicacionId)
                       .ToList();
                    List<int> lIdsActividades = lInfActVencidas.GroupBy(iav => iav.PlanActividadId)
                       .Select(g => g.First().PlanActividadId)
                       .ToList();

                    List<Proyecto> lProyectos = new ProyectoNegocio()
                        .Get_By_lIds(lIdsProyectos);
                    List<Proyecto_Ubicaciones> lProyUbicaciones = new Proyecto_UbicacionesNegocio()
                        .Get_X_lIds(lIdsUbicaciones);
                    List<Planificacion_Actividades> lPlanActividades = new Planificacion_ActividadesNegocio()
                        .Get_X_lIds(lIdsActividades);

                    lInfActVencidas.ForEach(iac =>
                    {
                        Proyecto oProyecto = lProyectos.Find(p => p.Id == iac.ProyectoId);
                        iac.sProyecto = oProyecto.Nombre;
                        Planificacion_Actividades oPlanActividad = lPlanActividades.Find(a => a.Id == iac.PlanActividadId);
                        iac.sPlanActividad = oPlanActividad.Nombre;
                        Proyecto_Ubicaciones oProyUbicacion = lProyUbicaciones.Find(pu => pu.Id == iac.ProyectoUbicacionId);
                        iac.sProyUbicacion = oProyUbicacion.Nombre;
                    });

                    result = new
                    {
                        lInfActVencidas
                    };
                }
                else
                {
                    result = new
                    {
                        error = true,
                        message = "Acceso denegado"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new
                {
                    error = true,
                    message = "Exception",
                    ex = ex.InnerException
                };
            }
            return result;
        }

        [HttpPost]
        public object BorrarParteDiario(ParteDiario oParteDiario)
        {
            var oResult = new object();
            try
            {
                ItemLoginUsuario oItemUsuario = GetUsuarioLogueado();
                Usuarios oUsuarioLogueado = new UsuariosNegocio().Get_Usuario(oItemUsuario.Id);
                if (oUsuarioLogueado != null)
                {
                    ParteDiario_ActividadesNegocio oPDANeg = new ParteDiario_ActividadesNegocio();
                    ParteDiario_Actividades_ContratistaNegocio oPDACNeg = new ParteDiario_Actividades_ContratistaNegocio();
                    ParteDiarioNegocio oPDNeg = new ParteDiarioNegocio();

                    ParteDiario oPD = oPDNeg.Get_One_ParteDiario(oParteDiario.Id);
                    Proyecto oProyecto = new ProyectoNegocio().Get_One_Proyecto(oPD.ProyectoId);
                    if (oUsuarioLogueado.Id == oProyecto.UsuariosId)
                    {
                        List<ParteDiario_Actividades> lPDAs = oPDANeg.Get_x_ParteDiarioId(oPD.Id);
                        lPDAs.ForEach(pda =>
                        {
                            List<ParteDiario_Actividades_Contratista> lPDACs = new ParteDiario_Actividades_ContratistaNegocio()
                                .Get_x_ParteDiarioActividadId(pda.Id);
                            lPDACs.ForEach(pdac => oPDACNeg.Delete(pdac));
                            oPDANeg.Delete(pda);
                        });
                        oPDNeg.Delete(oPD);
                        oResult = new
                        {
                            borrado = true,
                            mensaje = "Parte diario eliminado correctamente."
                        };
                    }
                    else
                    {
                        oResult = new
                        {
                            isError = true,
                            mensaje = "No tiene permisos para realizar esta acción."
                        };
                    }
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = "Acceso denegado"
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = "Exception",
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #region COCINADAS

        private void CocinarAvancesPDAs()
        {
            try
            {
                List<Proyecto> lProyectos = new ProyectoNegocio().Get_All_Proyecto();
                lProyectos.ForEach(p =>
                {
                    List<ParteDiario> lPDs = new ParteDiarioNegocio().Get_x_ProyectoId(p.Id);
                    ParteDiario oPD = lPDs.OrderByDescending(pd=>pd.Fecha_Creacion).FirstOrDefault();
                    if(oPD != null)
                    {
                        List<ParteDiario_Actividades> lPDAs = new ParteDiario_ActividadesNegocio()
                            .Get_x_ParteDiarioId(oPD.Id);
                        lPDAs.ForEach(pda =>
                        {
                            List<ParteDiario_Actividades_Contratista> lPDACs = new ParteDiario_Actividades_ContratistaNegocio()
                                .Get_x_ParteDiarioActividadId(pda.Id);
                            if(lPDACs.Count(pdac => pdac.TipoRegistro == ValoresUtiles.TipoRegistro_PDAC_Automatico) > 0)
                            {
                                Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                                    .Get_X_Id(pda.Planificacion_Proyecto_ActividadesId);
                                float cantPlanificada = oPPA.Cantidad;
                                if (cantPlanificada == 0) cantPlanificada = 1;
                                float acumulado = new Planificacion_Proyecto_ActividadesNegocio()
                                    .GetCantidadAcumulada(oPPA.Id);
                                float avance = acumulado / cantPlanificada * 100;

                                pda.Avance = avance;
                                new ParteDiario_ActividadesNegocio().Update(pda);
                                oPPA.Avance = avance;
                                new Planificacion_Proyecto_ActividadesNegocio().Update(oPPA);
                            }
                        });
                    }
                });
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ParteDiarioController: CocinarAvancesPDAs(): exception.", ex);
            }
        }


        #endregion

    }
}