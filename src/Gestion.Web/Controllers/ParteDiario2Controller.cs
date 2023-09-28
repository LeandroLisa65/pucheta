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
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Gestion.Web.Controllers
{
    public class ParteDiario2Controller : Controller
    {
        private readonly IWebHostEnvironment _env;

        public ParteDiario2Controller(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ParteDiario()
        {
            return View();
        }

        #region Parte Diario GRILLA
        public ActionResult _ParteDiarioGrilla()
        {
            List<ParteDiario> Lista = new List<ParteDiario>();
            DateTime mFechaInicial = DateTime.Now.AddDays(-30);
            Lista = new ParteDiarioNegocio().Get_All_ParteDiario().Where(p => p.Fecha_Creacion >= mFechaInicial).OrderByDescending(x => x.Fecha_Creacion).ThenBy(p => p.ProyectoId).ToList();

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
            

            if (data.proyecto != 0)
            {
                Lista = new ParteDiarioNegocio().Get_All_ParteDiario()
                .Where(x => x.Fecha_Creacion >= data.fechaDesde && x.Fecha_Creacion <= data.fechaHasta && x.ProyectoId == data.proyecto)
                .OrderByDescending(x => x.Fecha_Creacion).ToList();
            }
            else
            {
                Lista = new ParteDiarioNegocio().Get_All_ParteDiario()
                .Where(x => x.Fecha_Creacion >= data.fechaDesde && x.Fecha_Creacion <= data.fechaHasta)
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
            //r.data = Lista;
            r.isError = false;

            return r;
        }
        #endregion

        #region Creacion Parte Diario
        [HttpPost]
        public ReturnData InicializacionPD(ItemPDFiltro data)
        {
            ReturnData r = new ReturnData();
            int mIdParteDiario = 0;

            r = ValidoExistaPD(data);

            if (r.isError == false)
            {
                if (r.data.ToString() == "0")
                {
                    # region 1-Es un PD NUEVO
                    r = CreoParteDiario(data); 
                    mIdParteDiario = Convert.ToInt32(r.data);
                    #endregion
                }
                else
                {
                    //Es un PD que ya existe
                    mIdParteDiario = Convert.ToInt32(r.data);
                }

                #region 2-Creo las Actividades
                r = CreoActividadesPD(mIdParteDiario);
                #endregion

                #region 3-Creo la Asistencia
                r = CreoAsistenciaPD(mIdParteDiario);
                #endregion
            }
            else
            {
                //Devuelvo con el error
                return r;
            }
            r.data = mIdParteDiario;
            r.isError = false;

            return r;
        }
        #region Validacion de los datos recibidos para Inicializar el PD
        private ReturnData ValidoExistaPD(ItemPDFiltro data)
        {
            ReturnData r = new ReturnData();

            r.isError = false;
            r.data = "0";
            int mIdParteDiario = 0;

            #region 1-Valido que venga un Proyecto seleccionado
            if (data.proyecto == 0)
            {
                r.data = "Debe seleccionar un proyecto valido";
                r.isError = true;
                return r;
            }
            #endregion

            #region 2-Valido si existen parte diarios para eso proyecto para esa fecha o posterior
            List<ParteDiario> lstPartesDiarios = new ParteDiarioNegocio().Get_x_ProyectoId_FechaDesde(data.proyecto, data.fechaDesde).ToList();
            if (lstPartesDiarios.Count > 0)
            {
                foreach (var item in lstPartesDiarios)
                {
                    if (item.Fecha_Creacion == data.fechaDesde)
                    {
                        mIdParteDiario = item.Id;
                    }
                }
                if (mIdParteDiario == 0)
                {
                    r.data = "Existen uno o mas partes diarios posteriores a la fecha seleccionada";
                    r.isError = true;
                    return r;
                }
                else
                {
                    r.data = mIdParteDiario;
                    r.isError = false;
                    return r;
                }
            }
            #endregion

            return r;
        }
        #endregion

        #region Creacion del Parte Diario
        private ReturnData CreoParteDiario(ItemPDFiltro data)
        {
            ReturnData mReturn = new ReturnData();
            ParteDiario oPD = new ParteDiario();
            oPD.Fecha_Creacion = data.fechaDesde;
            oPD.ProyectoId = data.proyecto;
            mReturn.data = new ParteDiarioNegocio().Insert(oPD).ToString();
            return mReturn;
        }

        private ReturnData CreoActividadesPD(int pIdPD)
        {
            ReturnData mReturn = new ReturnData();
            var mResult = new ParteDiarioNegocio().CreacionActividades_ActividadesContratistas(pIdPD);
            return mReturn;
        }
        #endregion

        #region Creacion de la Tabla Asistencia
        private ReturnData CreoAsistenciaPD(int pIdPD)
        {
            ReturnData mReturn = new ReturnData();
            var mResult = new ParteDiarioNegocio().CreacionAsistencia(pIdPD);
            return mReturn;
        }
        #endregion
        #endregion

        #region Pantalla de Asistencia
        public ActionResult _ParteDiarioAsistencia(int id)
        {
            ReturnData mReturn = new ReturnData();
            mReturn.data = id.ToString();
            return PartialView(mReturn);
        }

        [HttpPost]
        public List<ParteDiario_Asistencia> Buscar_PDAsistencia(int id)
        {
            #region 1-Busco la Asistencia
            List<ParteDiario_Asistencia> lstAsistencia = new ParteDiario_AsistenciaNegocio().Get_All_ParteDiario_AsistenciaPA(id, true).ToList();
            return lstAsistencia;
            #endregion
        }

        [HttpPost]
        public void Update_PDAsistencia(itemParteDiario_Asistencia data)
        {
            ParteDiario_Asistencia oPDA = new ParteDiario_AsistenciaNegocio().Get_One_ParteDiario_Asistencia(data.id);
            oPDA.Asig_Propios = data.asig_Propios;
            oPDA.Asig_Propios_Presentes = data.asig_Propios_Presentes;
            new ParteDiario_AsistenciaNegocio().Update(oPDA);
        }
        #endregion

        #region Pantalla de Actividades

        #region FiltroActividades
        [HttpPost]
        public ReturnData cargarComboFiltroUbicacion(int IdParteDiario)
        {
            ReturnData data = new ReturnData();

            ParteDiario PD = new ParteDiarioNegocio().Get_One_ParteDiario(IdParteDiario);

            data.isError = false;
            data.data = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(PD.ProyectoId);
            return data;
        }
        [HttpPost]
        public ReturnData cargarComboFiltroActividad(int IdParteDiario)
        {
            ReturnData data = new ReturnData();
            ParteDiario PD = new ParteDiarioNegocio().Get_One_ParteDiario(IdParteDiario);

            data.isError = false;
            List<int> ListaActividadesId= new ParteDiario_ActividadesNegocio().Get_x_ParteDiarioId(PD.Id).Select(p=>p.Planificacion_ActividadesId).ToList();

            List<Planificacion_Actividades> ListaActividades = new List<Planificacion_Actividades>();

            foreach(var i in ListaActividadesId)
            {
                Planificacion_Actividades item = new Planificacion_ActividadesNegocio().Get_One_Planificacion_Actividades(i);

                ListaActividades.Add(item);
            }
            ListaActividades = ListaActividades.OrderBy(s=>s.Nombre).ToList();

            data.data = ListaActividades;
            return data;
        }

        [HttpPost]
        public ReturnData ListFiltroGrillaActividades(itemParteDiario_FiltroActividades item)
        {
            ReturnData mReturn = new ReturnData();
            DateTime fechaActual = DateTime.Today;
            DateTime fechaAyer = fechaActual.AddDays(-1);
            DateTime fechaAVencer = fechaActual.AddDays(10);
            string dateHoy = fechaActual.ToString("dd/MM/aaaa");
            string dateAyer = fechaAyer.ToString("dd/MM/aaaa");
            #region 1-Busco las Actividades
            List<ItemParteDiario_Actividades2> lstActividades = new ParteDiario_ActividadesNegocio().BuscarParteDiario_Actividades(item.IdParteDiario);      
            #endregion
         
            //Filtro Ubicacion
            if (item.FiltroUbicacion != null && item.FiltroUbicacion != "")
            {
                lstActividades = lstActividades.Where(s=> s.sUbicacion == item.FiltroUbicacion).ToList();
            }

            //Filtro Actividades
            if (item.IdFiltroActividad != 0) 
            {
                lstActividades = lstActividades.Where(s => s.Proyecto_ActividadesId == item.IdFiltroActividad).ToList();
            }
            //Filtro Avance Hoy
            if (item.FiltroAvanceActualHoy != null && item.FiltroAvanceActualHoy != "")
            {
                // Buscar_Parte_Diario trae todo lo del partediario
                lstActividades = item.FiltroAvanceActualHoy == "SI" ? lstActividades.Where(s => s.Trabajo == true).ToList() : lstActividades.Where(s => s.Trabajo == false).ToList();
            }
            //Filtro Avance Ayer
            if (item.FiltroAvanceActualAyer != null && item.FiltroAvanceActualAyer != "")
            {
                lstActividades = item.FiltroAvanceActualAyer == "SI" ? lstActividades.Where(s => s.TrabajoAyer == true).ToList() : lstActividades.Where(s => s.TrabajoAyer == false).ToList();
            }
            if (item.FiltroEstado != null && item.FiltroEstado != "")
            {
                if (item.FiltroEstado == "Todas")
                {                   
                }
                else if (item.FiltroEstado == "Vencida")
                {
                    lstActividades = lstActividades.Where(s => s.Estado=="Vencida").ToList();
                }
                else if (item.FiltroEstado == "Prox_Vencer")
                {
                    lstActividades = lstActividades.Where(s => Convert.ToDateTime(s.FechaFin) > fechaActual && Convert.ToDateTime(s.FechaFin) < fechaAVencer).ToList();
                }
                else
                {
                    lstActividades = lstActividades.Where(s => Convert.ToDateTime(s.FechaFin) < fechaActual ).ToList();
                }
            }

            mReturn.data = lstActividades;
            return mReturn;
        }
        #endregion



        public ActionResult _ParteDiarioActividades(int id)
        {
            ReturnData mReturn = new ReturnData();
            mReturn.data = id.ToString();
            return PartialView(mReturn);
        }

        [HttpPost]
        public ReturnData Buscar_PDActividades(int id)
        {
            ReturnData mReturn = new ReturnData();

            #region 1-Busco las Actividades
            List<ItemParteDiario_Actividades2> lstActividades = new ParteDiario_ActividadesNegocio().BuscarParteDiario_Actividades(id);
            mReturn.data = lstActividades;
            #endregion

            //Busco el IdProyecto
            //ParteDiario oPD = new ParteDiarioNegocio().Get_One_ParteDiario(lstActividades[0].ParteDiarioId);
            //Proyecto oPro = new ProyectoNegocio().Get_One_Proyecto(oPD.ProyectoId);

            //#region 2-Busco las Ubicaciones
            //List<Proyecto_Ubicaciones> lstUbicaciones = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(oPro.Id).OrderBy(p => p.Nombre).ToList();
            //mReturn.data1 = lstUbicaciones;
            //#endregion

            //#region 3-Actividades

            //#endregion
            return mReturn;
        }

        [HttpPost]
        public ReturnData Update_PDActividades(ItemParteDiario_Actividades2 data)
        {
            //Validar
            ReturnData mReturn = new ReturnData();
            mReturn.isError = false;
            //Primero busco el objeto como esta hoy
            ParteDiario_Actividades_Contratista oPDAC = new ParteDiario_Actividades_ContratistaNegocio().Get_x_Id(data.Id);
            NotaPedido_Detalle oNotaPedidoDetalle = new NotaPedido_DetalleNegocio().Get_One_Orden(data.NotaPedidoDetalleId);

            try
            {
                #region 1-Validaciones
                mReturn = Valida_PDActividades(data, oPDAC, oNotaPedidoDetalle);
                if (mReturn.isError)
                    return mReturn;
                #endregion

                #region 2-Items de Calidad
                if (data.ListaItems != null)
                {
                    if (data.ListaItems.Count > 0)
                    {
                        //Recorremos todos los items de Calidad Asociados a esa Actividad
                        foreach (var item in data.ListaItems)
                        {
                            int id_IncidenteHistorial = 0;
                            //Vemos Si se Genera o No un Incidente
                            if (item.Valor == "N")
                            {
                                //Creamos el Incidente y su Incidente Historial

                                #region Seccion Historial Incidente
                                Incidentes_Historial incidentes_Historial = new Incidentes_Historial();
                                incidentes_Historial.IncidenteId = 60;
                                incidentes_Historial.Creacion_Fecha= DateTime.Now;
                                incidentes_Historial.Creacion_UsuarioId = getUsuarioActual().Id;
                                //Busar la descripcion de la actividad y la ubicacion.
                                Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_Actividades();
                                oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(data.Planificacion_Proyecto_ActividadesId);
                                Planificacion_Actividades oPA = new Planificacion_Actividades();
                                oPA = new Planificacion_ActividadesNegocio().Get_One_Planificacion_Actividades(oPPA.Planificacion_ActividadesId);
                                incidentes_Historial.Creacion_Descripcion = "Incidente Historial de un Item de Calidad. Nombre Actividad: " + oPA.Nombre + " Descripcion del JO: "+ item.Descripcion;
                                incidentes_Historial.ProyectoId = (int)new NotaPedidoNegocio().Get_One_Orden((int)oNotaPedidoDetalle.IdNotaPedido).IdProyecto;
                                incidentes_Historial.EstadoId = 2;
                                incidentes_Historial.AreaId=17;
                                incidentes_Historial.Fecha_Cierre= DateTime.Now.AddDays(5);
                                incidentes_Historial.ParteDiarioId = data.ParteDiarioId;
                                incidentes_Historial.ContratistasId = data.ContratistasId;
                                id_IncidenteHistorial = new Incidentes_HistorialNegocio().Insert(incidentes_Historial);
                                #endregion

                                //Cargamos las imagenes asociadas al incidente
                                #region Imagenes-ItemCalidad
                                //Guardamos el listado de Imagenes Cargadas
                                if (item.lIdsArchivosAdjuntos != null)
                                {
                                    item.lIdsArchivosAdjuntos.ForEach(line =>
                                    {
                                        if(line.IdCalidad == item.IdCalidadItem)
                                         {
                                            Archivos_Adjuntos_Relacion oAARelacion = new Archivos_Adjuntos_Relacion();
                                            oAARelacion.Archivos_AdjuntosId = line.ImagenId;
                                            oAARelacion.Entidad = "AIH";
                                            oAARelacion.IdEntidad = id_IncidenteHistorial;
                                            new Archivos_Adjuntos_RelacionNegocio().Insert(oAARelacion);
                                            //Movemos el archivo adjunto de la carpeta temporal a la carpeta que necesitamos
                                            Archivos_Adjuntos oArchivoAdjunto = new Archivos_AdjuntosNegocio()
                                                    .Get_One_Archivos_Adjuntos(line.ImagenId);
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
                                        }

                                    });
                                }
                                #endregion
                            }

                            #region Insert Item Calidad

                            //Cargamos todas las propiedades para Insertar el Item de Calidad

                            Calidad_Actividades_Valoracion itemCalidad = new Calidad_Actividades_Valoracion();
                            itemCalidad.IdPlanificacion_Proyecto_ActividadId = data.Planificacion_Proyecto_ActividadesId;
                            itemCalidad.IdParteDiario = data.ParteDiarioId;
                            itemCalidad.IdCalidad_Items = item.IdCalidadItem;
                            itemCalidad.Valor = item.Valor;
                            itemCalidad.Descripcion = item.Descripcion;
                            itemCalidad.IdUsuario = getUsuarioActual().Id;
                            itemCalidad.IdIncidente = id_IncidenteHistorial > 0 ? id_IncidenteHistorial : 0;

                            new Calidad_Actividades_ValoracionNegocio().Insert(itemCalidad);
                            #endregion
                        }
                    }
                }
                #endregion

                #region 3-Actualizacion de Datos
                if (mReturn.isError == false)
                {
                    //Verifica si se cambio algun dato del registro original para realizar las actualizaciones
                    
            
                        if (data.FinalizadaActividadNP == true)
                        {
                            data.Trabajo = true;
                            oPDAC.Finalizado = data.FinalizadaActividadNP;
                        }
                        #region 1-Actualizar Parte Diario Actividad Contratista
                        oPDAC.TrabajoHoy = data.Trabajo;
                        oPDAC.AvanceActual = Convert.ToDecimal(data._AvanceActual.Replace(".", ","));
                        oPDAC.Finalizado = data.FinalizadaActividadNP;
                        if (oPDAC.AvanceActual > 0 && oPDAC.TrabajoHoy == false)
                        {
                            oPDAC.TrabajoHoy = true;
                        }
                        if (oPDAC.TrabajoHoy)
                        {
                            oPDAC.Cantidad = 1;
                        }
                        else
                        {
                            oPDAC.Cantidad = 0;
                        }
                        var updPDAC = new ParteDiario_Actividades_ContratistaNegocio().Update(oPDAC);
                        #endregion

                        #region 2-Actualizar Nota Pedido y Nota Pedido Detalle - Avance y Estado

                        #region 2.1-Nota Pedido Detalle
                        decimal mSumaCantidad = new ParteDiario_Actividades_ContratistaNegocio().SumaAvanceActual_PDAC(data.Planificacion_Proyecto_ActividadesId, data.ContratistasId, 0, oPDAC.NotaPedidoId);
                        oNotaPedidoDetalle.Avance_Actual = Convert.ToDouble(mSumaCantidad);
                        oNotaPedidoDetalle.Finalizado = data.FinalizadaActividadNP;
                        var mUpdate = new NotaPedido_DetalleNegocio().Update(oNotaPedidoDetalle);
                        #endregion

                        #region 2.2 Nota Pedido
                        NotaPedido oNP = new NotaPedidoNegocio().Get_One_Orden(oPDAC.NotaPedidoId);

                        if (data.FinalizadaActividadNP)
                        {
                            //Verifico si se debe cerrar la nota de pedido. Esto pasa cuando se finalizan todas las nota de pedido detalle que tiene.
                            int mNPD_SinFinalizar = new NotaPedido_DetalleNegocio().Get_All_Ordenes().Where(p => p.IdNotaPedido == oPDAC.NotaPedidoId && p.Finalizado == true && p.IdPPA != data.Planificacion_Proyecto_ActividadesId).Count();
                            if (mNPD_SinFinalizar == 0)
                            {
                                //Finalizar la Nota de Pedido
                                oNP.Estado = "Finalizado";
                            }
                        }
                        else
                        {
                            //Si la nota de pedido esta abierta y se declara que se trabajo, se cambia el estado a En Ejecucion
                            if (oNP.Estado == "Abierto" && oPDAC.TrabajoHoy)
                            {
                                oNP.Estado = "Ejecucion";
                            }
                        }
                        mUpdate = new NotaPedidoNegocio().Update(oNP);
                        #endregion

                        #endregion

                        #region 3- Planificacion Proyecto Actividades - Avance global y si esta o no finalizada
                        Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(data.Planificacion_Proyecto_ActividadesId);

                        #region 3.1 - Actualizacion de la Fecha Real de Inicio
                        if (oPPA.Fecha_Real_Incio == DateTime.MinValue && data.Trabajo)
                        {
                            oPPA.Fecha_Real_Incio = oPDAC.Fecha;
                        }
                        #endregion

                        #region 3.2 - Finalizacion de la Actividad
                        //1 Verificar que se haya asignado por Nota de Pedido el Total de lo Planificado para esa actividad
                        decimal mCantidadPlanificada = Convert.ToDecimal(oPPA.Cantidad);
                        double mCantidadAsignada =  new NotaPedido_DetalleNegocio().Get_Orden_PPA(oPPA.Id).Sum(p => p.Cantidad);
                        decimal mCantidadAsignada2 =  Convert.ToDecimal(Convert.ToString(mCantidadAsignada).Replace(".", ","));

                        if (mCantidadPlanificada == mCantidadAsignada2)
                        {
                            //Como esta Asignada al 100% entonces verifico si esta PPA esta finalizada en todas las Notas de Pedido Detalle
                            var mCount = new NotaPedido_DetalleNegocio().Get_Orden_PPA(oPPA.Id).Where(p => p.Finalizado == false).Count();
                            if (mCount == 0)
                            {
                                oPPA.Finalizados = true;
                            }
                            else
                            {
                                oPPA.Finalizados = false;
                                
                            }
                        }
                        //Calculo el Avance en Porcentaje
                        decimal mCantidadTotalAvance = new ParteDiario_Actividades_ContratistaNegocio().SumaAvanceActual_PDAC(data.Planificacion_Proyecto_ActividadesId, 0, 0, 0);
                        decimal mAvacenTotalPorcentaje = 0;
                        if (mCantidadTotalAvance > 0)
                        {
                            mAvacenTotalPorcentaje = (mCantidadTotalAvance / mCantidadPlanificada) * 100;
                        }
                        
                        oPPA.Avance = float.Parse(mAvacenTotalPorcentaje.ToString("#0.00"));

                        #endregion
                        
                        var mResult = new Planificacion_Proyecto_ActividadesNegocio().Update(oPPA);
                        #endregion

                        #region 4-Actulizar Parte Diario Actividad
                        ParteDiario_Actividades oPDA = new ParteDiario_Actividades();
                        oPDA = new ParteDiario_ActividadesNegocio().Get_One_ParteDiario_Actividades(oPDAC.ParteDiario_ActividadesId);
                        oPDA.Avance = (float)mAvacenTotalPorcentaje;
                        oPDA.Finalizados = oPPA.Finalizados;
                        mResult = new ParteDiario_ActividadesNegocio().Update(oPDA);
                        #endregion
                    
                }

                if (data.FinalizadaActividadNP == true)
                {
                    oPDAC.TrabajoHoy = true;
                    oPDAC.AvanceActual = Convert.ToDecimal(data._AvanceActual.Replace(".", ","));
                    oPDAC.Finalizado = data.FinalizadaActividadNP;
                }
                #endregion

                #region 4-Devuelvo las Actividades Actualizadas
                mReturn = Buscar_PDActividades(oPDAC.ParteDiarioId);
                mReturn.isError = false;
                #endregion
            }
            catch (Exception ex)
            {
                mReturn.data = "Se produjo un error: " + ex.Message;
                mReturn.isError = true;
            }
            return mReturn;
        }

        [HttpPost]
        public ReturnData ControlItemCalidadParteDiario(ItemParteDiario_Actividades2 data)
        {
            
            ReturnData mReturn = new ReturnData();
          
            try
            {
                //Parte Diario Id
                int IdPD = data.Id;
                int IdPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(data.Planificacion_Proyecto_ActividadesId).Planificacion_ActividadesId;

                //1.0 Si es Finalizar la Actividad
                if (data.FinalizadaActividadNP == true)
                {
                    //1.1 Buscamos Si tiene un item de Calidad para Finalizar x Planificacion Actividad Id.
                    List<Calidad_Actividades_Valoracion> listaItemsCalidadFinal = new Calidad_Actividades_ValoracionNegocio().Get_ItemCalidad_ParaActividad(data.Planificacion_Proyecto_ActividadesId, "FINALIZAR");
                    if (listaItemsCalidadFinal.Count > 0)
                    {
                        mReturn.data = "Control";
                        mReturn.data1= listaItemsCalidadFinal;
                    }
                    else
                    {
                        //Si no tiene item puede Avanzar
                        mReturn.data = "Ok";
                    }
                }
                else
                {
                    //2.0 Buscamos si es la primera vez que trabaja.
                    bool trabajoAntes = new ParteDiario_Actividades_ContratistaNegocio().ControlTrabajoHoy_PD(data.NotaPedidoId, IdPD).Any();
                    if (!trabajoAntes)
                    {
                        // 2.1 Buscamos si tiene asociado un item de Calidad asociado al iniciar x Planificacion Actividad Id.
                       
                        List<Calidad_Actividades_Valoracion> listaItemsCalidadInicial = new Calidad_Actividades_ValoracionNegocio().Get_ItemCalidad_ParaActividad(data.Planificacion_Proyecto_ActividadesId, "INICIAR");
                        if (listaItemsCalidadInicial.Count > 0)
                        {
                            mReturn.data = "Control";
                            mReturn.data1 = listaItemsCalidadInicial;
                        }
                        else
                        {
                            //Si no tiene item de calidad asociado
                            mReturn.data = "Ok";
                        }
                    }
                    else
                    {
                        //Si no es la primera vez
                        mReturn.data = "Ok";
                    }
                    

                }
                    
                }
            catch (Exception ex)
            {
                mReturn.data = "Se produjo un error: " + ex.Message;
                mReturn.isError = true;
            }
            return mReturn;
        }

        private ReturnData Valida_PDActividades(ItemParteDiario_Actividades2 data, ParteDiario_Actividades_Contratista oPDAC,  NotaPedido_Detalle oNotaPedidoDetalle)
        {
            ReturnData mReturn = new ReturnData();
            mReturn.isError = false;
            mReturn.data = "";

            #region 1-Validaciones
            #region Validar 1.1 - Cantidad a cargar no puede superar carntidad asignada + cantidad actual
            decimal mSumaCantidadAcum_SinEstePD = new ParteDiario_Actividades_ContratistaNegocio().SumaAvanceActual_PDAC(data.Planificacion_Proyecto_ActividadesId, data.ContratistasId, oPDAC.ParteDiarioId, oPDAC.NotaPedidoId);
            decimal mNuevoAcumulado = (Convert.ToDecimal(data._AvanceActual.Replace(".", ",")) + mSumaCantidadAcum_SinEstePD);
            //if (Convert.ToDecimal(data._AvanceActual.Replace(".", ",")) + Convert.ToDecimal(oNotaPedidoDetalle.Avance_Actual) > Convert.ToDecimal(oNotaPedidoDetalle.Cantidad))
            if (mNuevoAcumulado > Convert.ToDecimal(oNotaPedidoDetalle.Cantidad))
            {
                mReturn.isError = true;
                mReturn.data = "Cantidad supera la cantidad asigada.";
            }
            #endregion

            #region Validar 1.2 - Si esta Finalizada
            if (oPDAC.Finalizado)
            {
                mReturn.isError = true;
                mReturn.data = "Activdad finalizada";
            }
            #endregion

            #region Validar 1.3 - Haya cargado Asistentes a este contratistas
            ParteDiario_Asistencia oPDAsist = new ParteDiario_AsistenciaNegocio().Get_All_ParteDiario_AsistenciaPA(oPDAC.ParteDiarioId).Where(p => p.ContratistasId == oPDAC.ContratistasId).FirstOrDefault();
            if (oPDAsist != null)
            {
                if (oPDAsist.Asig_Propios_Presentes == 0)
                {
                    mReturn.isError = true;
                    mReturn.data = "El contratista no posee empleados asignados presentes, complete la planilla de asistencia primero";
                }
            }
            else
            {
                mReturn.isError = true;
                mReturn.data = "El contratista no posee empleados asignados presentes, complete la planilla de asistencia primero";
            }
            #endregion

            #endregion

            return mReturn;
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
            //List<Calidad_Actividades_Valoracion> lstCalidad = new Calidad_Actividades_ValoracionNegocio()
            //    .Get_ItemCalidadActividadValoracion_ParaGrilla(idParteDiario, DateTime.MinValue, DateTime.MaxValue)
            //    .OrderByDescending(s => Convert.ToDateTime(s.sFecha))
            //    .ToList();
            oPD_R.lstItemCalidadValoracion = null;
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
                .ThenBy(p => p.Planificacion_Proyecto_ActividadesId)
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

        #region Pantalla Parte Diario Novedades
        public ActionResult _ParteDiarioNovedades(int id,int id2)
        {
            ReturnData mReturn = new ReturnData();
            var data = new ItemParteDiarioIncidenteGraba();
            mReturn.data = id.ToString();
            mReturn.data1 = id2.ToString();
            List<ParteDiario_Incidentes> ListaReturn = new List<ParteDiario_Incidentes>();
            List<ParteDiario_Incidentes> pdi = new ParteDiario_IncidentesNegocio().Get_All_ParteDiario_IncidentesPA(id2);
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
                     var Contratista = new ContratistasNegocio().Get_One_Contratistas(e.ContratistaId);
                    
                    if(Contratista != null) { pa.sContratista = Contratista.Nombre; }
                    else { pa.sContratista = ""; }
                    
                }
                else
                {
                    pa.sContratista = "";
                }
                #endregion




                ListaReturn.Add(pa);
            }
            mReturn.data2 = ListaReturn;
            return PartialView(mReturn);
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

                        ParteDiario ControlParteDiario = new ParteDiarioNegocio().Get_One_ParteDiario(oParteDiario.Id);

                        List<ParteDiario_Actividades> ListaPD = new ParteDiario_ActividadesNegocio().Get_All_ParteDiario_Actividades().Where(s => s.ParteDiarioId == oParteDiario.Id).ToList();
                        
                        if (ExisteAvance(ListaPD))
                        {
                            oResult = new
                            {
                                isError = true,
                                mensaje = "No se puede eliminar un parte diario que tenga cargado avances."
                            };
                        }
                        else
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


        private bool ExisteAvance(List<ParteDiario_Actividades> lista)
        {
            bool _tieneAvance=false;

            foreach( var item in lista)
            {

                List<ParteDiario_Actividades_Contratista> ListaContratistaa = new ParteDiario_Actividades_ContratistaNegocio().Get_All_ParteDiario_Actividades_Contratistas()
                    .Where(p => p.ParteDiario_ActividadesId == item.Id).ToList();
                foreach (var item2 in ListaContratistaa)
                {
                    NotaPedido_Detalle onpd = new NotaPedido_DetalleNegocio().Get_One_Orden(item2.NotaPedidoDetalleId);
                    if (onpd.Avance_Actual > 0)
                    {
                        _tieneAvance = true;
                        break;
                    }

                }                
                if (_tieneAvance)
                {               
                    break;
                }
            }
            return _tieneAvance;
        }
        #endregion


        #region  Novedades

        public ReturnData GetNovedades()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new IncidentesNegocio().Get_All_Incidentes().Where(x=> x.Rectype == "Novedad");
            d.data1 = new UsuariosNegocio().Get_Usuario(UsuarioLogin().Id).NombreYApellido;
            return d;
        }
        #endregion

        #region Contratista
        [HttpGet]
        public ReturnData GetContratistas(int id)
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new Proyecto_ContratistaNegocio().Get_All_Proyecto_Contratista(id);
            return d;
        }

        [HttpGet]
        public ReturnData CargarUsuario()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new UsuariosNegocio().Get_Usuario(UsuarioLogin().Id).NombreYApellido;
            return d;
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
        #region CRUD_ParteDiario
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
        public ReturnData IncidentesBorra(int id)
        {
            ReturnData d = new ReturnData();
            int idc;
            try
            {
                ParteDiario_Incidentes pdi = new ParteDiario_Incidentes();

                ParteDiario_Incidentes pdib = new ParteDiario_IncidentesNegocio().Get_One_ParteDiario_Incidentes(id);
                idc = new ParteDiario_IncidentesNegocio().Delete(pdib);

                if (pdib is null) {
                    d.isError = true;
                    d.data = "Error al Borrar";
                }
                else { 
                d.isError = false;
                d.data = "Se han Borrado los datos OK.";
               }
            }
            catch (Exception)
            {
                d.isError = true;
                d.data = "Error al Borrar";

            }
            return d;
        }

        [HttpGet]
        public ReturnData ParteDiarioArchivosDelete(int id)
        {
            ReturnData r = new Archivos_Adjuntos_RelacionNegocio().Eliminacion_Archivo_y_Relacion(id);
           
            return r;
        }
        #endregion
        #region Listado
        public ActionResult _GetArchivos_ParteDiarioNovedades(int id)
        {
            try
            {
               
                List<Archivos_Adjuntos_Relacion> lAARelacion = new Archivos_Adjuntos_RelacionNegocio()
                .Get_Archivos_Adjuntos_Relacion("PINC", id)
                .OrderByDescending(aar => aar.Archivos_Adjuntos.Extencion)
                .ToList();
                return PartialView(lAARelacion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ParteDiarioController: GetArchivos_ParteDiarioIncidente: exception.", ex);
            }
        }

        [HttpGet]
        public ReturnData ListadoGrilla(int ParteDiarioId )
        {
            try
            {
                var data = new ItemParteDiarioIncidenteGraba();
                ReturnData d = new ReturnData();

                List<ParteDiario_Incidentes> ListaReturn = new List<ParteDiario_Incidentes>();
                List<ParteDiario_Incidentes> pdi = new ParteDiario_IncidentesNegocio().Get_All_ParteDiario_IncidentesPA(ParteDiarioId);
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
                d.data1 = ListaReturn;
                return d;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ParteDiarioController: GetArchivos_ParteDiarioIncidente: exception.", ex);
            }
        }
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
        #endregion

        #region Incidentes

        #region CargarCombos
        [HttpGet]
        public ReturnData GetIncidente()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new IncidentesNegocio().Get_All_Incidentes();
            
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
        #endregion

        #region CRUD

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
                    Ih.sUsuarioDueño = UsuarioLogin().ApellidoNombre;
                    Ih.Creacion_UsuarioId = UsuarioLogin().Id;
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
                        
                        //if (e.IncidenteId != 0)
                        //    hi.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;
                        //else
                        //{
                        //    hi.sIncidente = "";
                        //}

                        //hi.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;
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

                        if (data.Id != 0) 
                        {
                            List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio().Get_Archivos_Adjuntos_Relacion("PINC", data.Id);
                            if (Laar.Count > 0)
                            {
                                data.IsArchivosIncidentes = true;
                            }
                            else
                            {
                                data.IsArchivosIncidentes = false;
                            }

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

        #endregion


        #region Pantalla Parte Diario Incidentes
        public ActionResult _ParteDiarioIncidentes(int id, int id2)
        {
            ReturnData mReturn = new ReturnData();
            var data = new ItemParteDiarioIncidenteGraba();
            mReturn.data = id.ToString();
            mReturn.data1 = id2.ToString();
            List<ParteDiario_Incidentes> ListaReturn = new List<ParteDiario_Incidentes>();
            List<ParteDiario_Incidentes> pdi = new ParteDiario_IncidentesNegocio().Get_All_ParteDiario_IncidentesPA(id2);
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
                    var Contratista = new ContratistasNegocio().Get_One_Contratistas(e.ContratistaId);

                    if (Contratista != null) { pa.sContratista = Contratista.Nombre; }
                    else { pa.sContratista = ""; }
                }
                else
                {
                    pa.sContratista = "";
                }
                #endregion




                ListaReturn.Add(pa);
            }
            mReturn.data2 = ListaReturn;
            return PartialView(mReturn);
        }
        #endregion

        #region MostrarDatos
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
                
                if (e.IncidenteId != 0)
                {
                    pa.sIncidente = new IncidentesNegocio().Get_One_Incidentes(e.IncidenteId).Nombre;
                }
                else
                {
                    pa.sIncidente = "";
                }

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

        public ActionResult _GetArchivos_ParteDiarioIncidentes(int id)
        {
            try
            {

                List<Archivos_Adjuntos_Relacion> lAARelacion = new Archivos_Adjuntos_RelacionNegocio()
                .Get_Archivos_Adjuntos_Relacion("PINC", id)
                .OrderByDescending(aar => aar.Archivos_Adjuntos.Extencion)
                .ToList();
                return PartialView(lAARelacion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ParteDiarioController: GetArchivos_ParteDiarioIncidente: exception.", ex);
            }
        }
        #endregion

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
                int mIdUsuario = UsuarioLogin().Id;
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
    }
}
