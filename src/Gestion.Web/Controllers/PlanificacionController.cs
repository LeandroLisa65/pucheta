using Gestion.EF;
using Gestion.EF.Models;
using Gestion.Negocio;
using Gestion.Web.Models;

using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using static Gestion.Negocio.Planificacion_Proyecto_ActividadesNegocio;
using MySqlConnector;

namespace Gestion.Web.Controllers
{
    public class PlanificacionController : Controller
    {
        // GET: Planificacion _Proyecto_ActividadesAbm
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: PlanificacionController: Index(): exception.", ex);
            }
        }

        #region Actividades Rubros

        public ActionResult Actividades_Rubros()
        {
            return View();
        }

        public ActionResult _Actividades_RubrosGrilla()
        {
            List<Planificacion_Actividades_Rubro> Lista = new List<Planificacion_Actividades_Rubro>();
            Lista = new Planificacion_Actividades_RubroNegocio().Get_All_Planificacion_Actividades_Rubro();
            return PartialView(Lista);
        }

        public ActionResult _Actividades_RubrosAbm(int id)
        {
            Planificacion_Actividades_Rubro lista = new Planificacion_Actividades_Rubro();
            if (id != 0)
            {
                lista = new Planificacion_Actividades_RubroNegocio().Get_One_Planificacion_Actividades_Rubro(id);
            }
            return PartialView(lista);
        }

        [HttpPost]
        public ReturnData Actividades_RubrosGraba(Planificacion_Actividades_Rubro data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new Planificacion_Actividades_RubroNegocio().Update(data);
                }
                else
                {
                    idc = new Planificacion_Actividades_RubroNegocio().Insert(data);
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

        #region Actividades

        public ActionResult Actividades()
        {
            return View();
        }

        public ActionResult _ActividadesGrilla()
        {
            List<Planificacion_Actividades> Lista = new List<Planificacion_Actividades>();
            Lista = new Planificacion_ActividadesNegocio().Get_All_Planificacion_Actividades();

            //REcorro la lista para ver si tiene ITEMS de CALIDAD Asociados
            if (Lista != null)
            {
                foreach (var item in Lista)
                {
                    var mTieneItemCalidad = new Planificacion_Actividades_Calidad_ItemsNegocio().Get_All_Planificacion_Actividades_Calidad_Items().Where(p => p.Inactivo == false && p.IdPLanificacionActividades == item.Id).Count();
                    if (mTieneItemCalidad > 0)
                        item.Tiene_ItemsCalidad = 1;
                    else
                        item.Tiene_ItemsCalidad = 0;

                }
            }
            return PartialView(Lista);
        }

        public ActionResult _ActividadesAbm(int id)
        {
            ItemPlanificacion_Actividades data = new ItemPlanificacion_Actividades();
            if (id != 0)
            {
                data.pa = new Planificacion_ActividadesNegocio().Get_One_Planificacion_Actividades(id);
            }
            else
            {
                data.pa = new Planificacion_Actividades();
            }

            data.Planificacion_Actividades_Rubro = new Planificacion_Actividades_RubroNegocio().Get_All_Planificacion_Actividades_Rubro();

            return PartialView(data);
        }

        [HttpPost]
        public ReturnData ActividadesGraba(Planificacion_Actividades data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new Planificacion_ActividadesNegocio().Update(data);
                }
                else
                {
                    idc = new Planificacion_ActividadesNegocio().Insert(data);
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

        #region  Tareas

        public ActionResult Actividades_Tareas()
        {
            return View();
        }

        public ActionResult _Actividades_TareasGrilla()
        {
            List<Planificacion_Actividades_Tareas> Lista = new List<Planificacion_Actividades_Tareas>();
            Lista = new Planificacion_Actividades_TareasNegocio().Get_All_Planificacion_Actividades_Tareas();
            return PartialView(Lista);
        }

        public ActionResult _Actividades_TareasAbm(int id)
        {
            ItemActividades_Tareas data = new ItemActividades_Tareas();
            if (id != 0)
            {
                data.pt = new Planificacion_Actividades_TareasNegocio().Get_One_Planificacion_Actividades_Tareas(id);
            }

            data.Planificacion_Actividades = new Planificacion_ActividadesNegocio().Get_All_Planificacion_Actividades();

            return PartialView(data);
        }

        [HttpPost]
        public ReturnData Actividades_TareasGraba(Planificacion_Actividades_Tareas data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new Planificacion_Actividades_TareasNegocio().Update(data);
                }
                else
                {
                    idc = new Planificacion_Actividades_TareasNegocio().Insert(data);
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

        #region Proyecto Actividades

        public ActionResult Proyecto_Actividades(int id)
        {
            ItemPUA data = new ItemPUA();
            try
            {
                //data.proyecto_ubicaciones = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones(id);
                data.proyecto = new ProyectoNegocio().Get_One_Proyecto(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Proyecto", "Proyecto");
            }

            return View(data);
        }

        public ActionResult _Proyecto_ActividadesGrilla(int id)
        {

            Res_PlanificacionProyectoActividades_Grilla mReturn = new Res_PlanificacionProyectoActividades_Grilla();

            List<Planificacion_Proyecto_Actividades> lPlanProyActividades = new List<Planificacion_Proyecto_Actividades>();
            lPlanProyActividades = new Planificacion_Proyecto_ActividadesNegocio()
                .Get_All_Planificacion_Proyecto_Actividades(id)
                .OrderBy(x => x.Fecha_Est_Incio)
                .ToList();



            List<Res_PPA> lResPPAs = new List<Res_PPA>();
            //Recorro la Lista y armo la lista con los datos basicos
            foreach (var oPAA in lPlanProyActividades)
            {
                Res_PPA oResPPa = new Res_PPA();
                oResPPa.Avance = oPAA.Avance;
                oResPPa.Cantidad = oPAA.Cantidad;
                oResPPa.Detalle = oPAA.Detalle;
                oResPPa.Fecha_Creacion = oPAA.Fecha_Creacion.ToString("dd/MM/yyyy");
                oResPPa.Fecha_Est_Fin = oPAA.Fecha_Est_Fin.ToString("dd/MM/yyyy");
                oResPPa.Fecha_Est_Incio = oPAA.Fecha_Est_Incio.ToString("dd/MM/yyyy");
                oResPPa.Fecha_Real_Fin = oPAA.Fecha_Real_Fin.ToString("dd/MM/yyyy");
                oResPPa.Fecha_Real_Incio = oPAA.Fecha_Real_Incio.ToString("dd/MM/yyyy");
                oResPPa.Fecha_Ultima_Modificacion = oPAA.Fecha_Ultima_Modificacion.ToString("dd/MM/yyyy");
                oResPPa.Id = oPAA.Id;
                oResPPa.Incidencia = oPAA.Incidencia;
                oResPPa.Planificacion_ActividadesId = oPAA.Planificacion_ActividadesId;
                oResPPa.Planificacion_ActividadesNombre = oPAA.Planificacion_Actividades.Nombre;
                oResPPa.Planificacion_Actividades_RubroId = oPAA.Planificacion_Actividades_RubroId;
                oResPPa.Planificacion_Actividades_RubroNombre = oPAA.Planificacion_Actividades_Rubro.Nombre;
                oResPPa.Proyecto_UbicacionesId = oPAA.Proyecto_UbicacionesId;
                oResPPa.Proyecto_UbicacionesNombre = oPAA.Proyecto_Ubicaciones.Nombre;
                oResPPa.ContratistasNombre = oPAA.Contratistas.Nombre;
                oResPPa.ContratistasId = oPAA.ContratistasId;
                oResPPa.EsAdicional = oPAA.EsAdicional;
                oResPPa.UnidadMedida = oPAA.UnidadMedida;
                oResPPa.UsuariosId = oPAA.UsuariosId;
                oResPPa.Finalizados = oPAA.Finalizados;
               
                oResPPa.TienePadre = new PlanProyAct_DependenciaNegocio().ConsultarTienePadre(oPAA.Id) ? "Si" : "No";
                //oPPA.UsuariosNombre = item.Usuarios.Nombre + " " + item.Usuarios.Nombre;

                //Nota de Pedidos
                double mCantidadAsignada = new NotaPedido_DetalleNegocio().Get_Orden_PPA(oPAA.Id).Sum(p => p.Cantidad);
                oResPPa.AsignadoNP = (String.Format("{0:0.00}", mCantidadAsignada)).Replace(',', '.') + " " + oResPPa.UnidadMedida;
                oResPPa.DisponibleCantidad = Math.Round((Convert.ToDouble(oResPPa.Cantidad) - mCantidadAsignada),2);
                if (mCantidadAsignada > 0)
                {
                    oResPPa.OcultarNP = false;
                }
                else
                {
                    oResPPa.OcultarNP = true;
                }
                lResPPAs.Add(oResPPa);
            }
            lResPPAs = lResPPAs.OrderBy(x => x.Proyecto_UbicacionesNombre)
                .ThenBy(x => x.Planificacion_Actividades_RubroNombre)
                .ThenBy(x => x.Planificacion_ActividadesNombre)
                .ThenBy(x => x.Fecha_Est_Incio).ToList();

            mReturn.lResPPAs = lResPPAs;

            #region 2 - Carga de los Filtros

            #region 2.1 - Cargo las Ubicaciones de ese proyecto para le filtro
            mReturn.lstDistinctUbicacion = lResPPAs.OrderBy(p => p.Proyecto_UbicacionesNombre).Select(o => o.Proyecto_UbicacionesNombre).Distinct().ToList();
            #endregion

            #region 2.2 - Cargos Distintos Contratistas
            mReturn.lstDistinctContratistas = lResPPAs.OrderBy(p => p.ContratistasNombre).Select(o => o.ContratistasNombre).Distinct().ToList();
            #endregion

            #region 2.3 - Cargos Distintos Actividades
            mReturn.lstDistinctActividades = lResPPAs.OrderBy(p => p.Planificacion_ActividadesNombre).Select(o => o.Planificacion_ActividadesNombre).Distinct().ToList();
            #endregion

            #region 2.4 - Cargos Distintos Rubros
            mReturn.lstDistinctRubro = lResPPAs.OrderBy(p => p.Planificacion_Actividades_RubroNombre).Select(o => o.Planificacion_Actividades_RubroNombre).Distinct().ToList();
            #endregion

            mReturn.lstOrden = new List<string>();
            mReturn.lstOrden.Add("Ubicacion");
            mReturn.lstOrden.Add("Rubro");
            mReturn.lstOrden.Add("Actividad");


            #endregion

            return PartialView(mReturn);
            //return PartialView(lResPPAs);
        }

        /// <summary>
        /// Procedimiento que carga los datos para la pantalla _Proyecto_ActividadesAbm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IdActividad"></param>
        /// <returns></returns>
        public ActionResult _Proyecto_ActividadesAbm(int id, int IdActividad)
        {
            ItemProyecto_Actividades oIProyActividad = new ItemProyecto_Actividades();
            if (IdActividad != 0)
            {
                oIProyActividad.pp = new Planificacion_Proyecto_ActividadesNegocio()
                    .Get_X_Id(IdActividad, true);


                if (oIProyActividad.pp.TienePadre)
                {
                    oIProyActividad.pp.lPPADep_Padres.ForEach(ppad =>
                    {
                        Planificacion_Proyecto_Actividades oPAA_Padre = new Planificacion_Proyecto_ActividadesNegocio()
                            .Get_X_Id(ppad.PPAPadreId);
                        Planificacion_Actividades_Rubro oPAR = new Planificacion_Actividades_RubroNegocio()
                            .Get_One_Planificacion_Actividades_Rubro(oPAA_Padre.Planificacion_Actividades_RubroId);
                        Planificacion_Actividades oPA = new Planificacion_ActividadesNegocio()
                            .Get_One_Planificacion_Actividades(oPAA_Padre.Planificacion_ActividadesId);
                        Proyecto_Ubicaciones oPU = new Proyecto_UbicacionesNegocio()
                            .Get_One_Proyecto_Ubicaciones(oPAA_Padre.Proyecto_UbicacionesId);
                        oPAA_Padre.Planificacion_Actividades_Rubro = oPAR;
                        oPAA_Padre.Planificacion_Actividades = oPA;
                        oPAA_Padre.Proyecto_Ubicaciones = oPU;
                        oIProyActividad.lPPA_Padres.Add(oPAA_Padre);
                    });
                }
            }
            else
            {
                oIProyActividad.pp = new Planificacion_Proyecto_Actividades();
            }
            oIProyActividad.IdProyecto = id;
            oIProyActividad.Planificacion_Actividades = new Planificacion_ActividadesNegocio()
                .Get_All_Planificacion_Actividades();

            oIProyActividad.Proyecto_Ubicaciones = new Proyecto_UbicacionesNegocio()
                .Get_X_IdProyecto(id).Distinct().ToList();
            Proyecto_Ubicaciones itemUbicaciones = new Proyecto_Ubicaciones
            {
                Id = 0,
                Nombre = "Todas las Ubicaciones"
            };
            oIProyActividad.Proyecto_Ubicaciones.Add(itemUbicaciones);
            oIProyActividad.Proyecto_Ubicaciones.OrderByDescending(x => x.Id).ToList();

            oIProyActividad.Planificacion_Actividades_Rubro = new Planificacion_Actividades_RubroNegocio()
                .Get_All_Planificacion_Actividades_Rubro();

            oIProyActividad.Planificacion_Actividades_Rubro_Antecedente = _CargadoRubrosPrecedentes(id);
            oIProyActividad.Contratistas = new ContratistasNegocio()
                .Get_All_Contratistas();

            oIProyActividad.FechaInicioProyecto = new ProyectoNegocio()
                    .Get_One_Proyecto((int)oIProyActividad.IdProyecto).Fecha_Inicio;

            oIProyActividad.Indices = new IndicesNegocio().Get_All_Indices();

            return PartialView(oIProyActividad);
        }

        private List<Planificacion_Actividades_Rubro> _CargadoRubrosPrecedentes(int IdProyecto)
        {
            List<Planificacion_Actividades_Rubro> ListaRubrosPrecedente = new List<Planificacion_Actividades_Rubro>();

            try
            {
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    MySqlConnection connection = new MySqlConnection(db.Database.GetDbConnection().ConnectionString);
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("ListadoRubros_Precedente", connection);

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Ent_IdProyecto", IdProyecto);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Planificacion_Actividades_Rubro Rub = new Planificacion_Actividades_Rubro();
                        Rub.Id = reader.GetInt32(0);
                        Rub.Nombre= (string)reader.GetString(1);
                        ListaRubrosPrecedente.Add(Rub);
                    }
                    connection.Close();           
                }
                
                Planificacion_Actividades_Rubro RubroPrimero = new Planificacion_Actividades_Rubro
                {
                    Id = 0,
                    Nombre = "Seleccione Rubro"
                };
                ListaRubrosPrecedente.Add(RubroPrimero);
                ListaRubrosPrecedente = ListaRubrosPrecedente.OrderBy(x => x.Id).ToList();


            }
            catch (Exception e)
            {
                throw;
            }

            return ListaRubrosPrecedente;
        }

        public partial class PPA_Y_Padres
        {
            public Planificacion_Proyecto_Actividades oPPA { get; set; }
            public List<Planificacion_Proyecto_Actividades> lPPA_Padres { get; set; }
        }
        [HttpPost]
        public ReturnData Proyecto_ActividadesGraba(PPA_Y_Padres data)
        {
            ReturnData oReturnData = new ReturnData();
            Usuarios oUsuarioLogueado = this.getUsuarioActual();
            if (oUsuarioLogueado != null && oUsuarioLogueado.Id > 0)
            {
                if (data != null)
                {
                    Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_Actividades();
                    if (data.oPPA.Id > 0)
                        oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                            .Get_X_Id(data.oPPA.Id, true);

                    #region VALIDACIÓN
                    string mensajeValidacion = "";
                    mensajeValidacion = data.oPPA.Fecha_Est_Incio < data.oPPA.Fecha_Est_Fin ?
                        "" : "- La fecha estimada de inicio debe ser menor que la fecha estimada de fin.";
                    data.lPPA_Padres = data.lPPA_Padres == null ? new List<Planificacion_Proyecto_Actividades>() :
                        data.lPPA_Padres;
                    data.lPPA_Padres.ForEach(ppa =>
                    {
                        if (string.IsNullOrEmpty(mensajeValidacion))
                        {
                            Planificacion_Proyecto_Actividades oPPA_Padre = new Planificacion_Proyecto_ActividadesNegocio()
                                .Get_X_Id(ppa.Id);
                            mensajeValidacion += oPPA_Padre.Fecha_Est_Incio <= data.oPPA.Fecha_Est_Incio ? "" :
                                "- La fecha estimada de inicio no puede ser menor que la fecha estimada de inicio de la actividad precedente.";
                        }
                    });
                    #endregion

                    if (string.IsNullOrEmpty(mensajeValidacion))
                    {
                        int idPPA;
                        if (data.oPPA.Id > 0)
                        {
                            if (data.oPPA.Detalle != null)
                            {
                                data.oPPA.Detalle = ToolsNegocio.EliminaEnter(data.oPPA.Detalle);
                            }
                            if (oPPA.Fecha_Est_Incio != data.oPPA.Fecha_Est_Incio || oPPA.Fecha_Est_Fin != data.oPPA.Fecha_Est_Fin)
                            {
                                ResultActualizacionArbolPPA oRAAPPA = new ResultActualizacionArbolPPA();
                                oRAAPPA.IdPlanProyActividad = oPPA.Id;
                                oRAAPPA.FecEstInicio_Nueva = data.oPPA.Fecha_Est_Incio;
                                oRAAPPA.FecEstFin_Nueva = data.oPPA.Fecha_Est_Fin;
                                oRAAPPA.FecEstFin_Nueva_Original = data.oPPA.Fecha_Est_Fin;
                                oRAAPPA.CambioAplicable = true;
                                oRAAPPA.message = string.Empty;
                                oRAAPPA.lPlanProyActividades_Resultante = new List<Planificacion_Proyecto_Actividades>();
                                oRAAPPA = new Planificacion_Proyecto_ActividadesNegocio()
                                    .AplicarActualizacionCascada(oRAAPPA, false);
                            }
                            data.oPPA.Avance = oPPA.Avance;
                            idPPA = new Planificacion_Proyecto_ActividadesNegocio().Update(data.oPPA);
                        }
                        else
                        {
                            if (data.oPPA.Detalle != null)
                            {
                                data.oPPA.Detalle = ToolsNegocio.EliminaEnter(data.oPPA.Detalle);
                            }
                            oPPA.Id = 0;
                            oPPA.IdPadre = data.oPPA.IdPadre;
                            oPPA.Detalle = data.oPPA.Detalle;
                            oPPA.Fecha_Real_Incio = data.oPPA.Fecha_Real_Incio;
                            oPPA.Fecha_Creacion = DateTime.Now;
                            oPPA.Fecha_Real_Fin = data.oPPA.Fecha_Real_Fin;
                            oPPA.Fecha_Est_Incio = data.oPPA.Fecha_Est_Incio;
                            oPPA.Fecha_Est_Fin = data.oPPA.Fecha_Est_Fin;
                            oPPA.Avance = data.oPPA.Avance = 0;
                            oPPA.Fecha_Ultima_Modificacion = DateTime.Now;
                            oPPA.Planificacion_ActividadesId = data.oPPA.Planificacion_ActividadesId;

                            oPPA.PresentaPoliza = data.oPPA.PresentaPoliza;
                            oPPA.ComentarioPoliza = data.oPPA.ComentarioPoliza;

                            oPPA.Planificacion_Actividades_RubroId = data.oPPA.Planificacion_Actividades_RubroId;
                            oPPA.Proyecto_UbicacionesId = data.oPPA.Proyecto_UbicacionesId;
                            oPPA.UsuariosId = data.oPPA.UsuariosId;
                            oPPA.EsAdicional = data.oPPA.EsAdicional;
                            oPPA.Solapable = data.oPPA.Solapable;
                            oPPA.ContratistasId = data.oPPA.ContratistasId;
                            oPPA.Incidencia = data.oPPA.Incidencia;
                            oPPA.Monto = data.oPPA.Monto;
                            oPPA.Cantidad = data.oPPA.Cantidad;
                            oPPA.UnidadMedida = data.oPPA.UnidadMedida;
                            oPPA.Finalizados = data.oPPA.Finalizados;

                           // oPPA.ContratistasId = 1; //este es el problema, esta llegando nulo
                            idPPA = new Planificacion_Proyecto_ActividadesNegocio().Insert(oPPA);
                        }

                        List<PlanProyAct_Dependencia> lPPAD_PadresExistentes = oPPA.lPPADep_Padres;
                        data.lPPA_Padres.ForEach(ppa =>
                        {
                            PlanProyAct_Dependencia oPPAD = new PlanProyAct_DependenciaNegocio()
                                .Get_x_Ids(ppa.Id, idPPA);
                            if (oPPAD == null)
                            {
                                oPPAD = new PlanProyAct_Dependencia();
                                oPPAD.PPAPadreId = ppa.Id;
                                oPPAD.PPAHijaId = idPPA;
                                oPPAD.FecCreacion = DateTime.Now;
                                oPPAD.IdUsuarioCreo = oUsuarioLogueado.Id;
                                new PlanProyAct_DependenciaNegocio().Insert(oPPAD);
                            }
                        });
                        lPPAD_PadresExistentes.ForEach(ppad =>
                        {
                            bool debeBorrar = data.lPPA_Padres.Find(ppa => ppa.Id == ppad.PPAPadreId) == null;
                            if (debeBorrar)
                            {
                                new PlanProyAct_DependenciaNegocio().Delete(ppad);
                            }
                        });

                        int nVersion = new Planificacion_Proyecto_Actividades_Log_Negocio()
                            .Get_All_Planificacion_Proyecto_Actividades_Log(idPPA).Count();
                        Planificacion_Proyecto_Actividades_Log log = new Planificacion_Proyecto_Actividades_Log();
                        log.Fecha_Est_Incio = data.oPPA.Fecha_Est_Incio;
                        log.Fecha_Est_Fin = data.oPPA.Fecha_Est_Fin;
                        log.Version = nVersion + 1;
                        log.Planificacion_Proyecto_ActividadesId = idPPA;
                        new Planificacion_Proyecto_Actividades_Log_Negocio().Insert(log);
                        if (idPPA > 0)
                        {
                            oReturnData.isError = false;
                            oReturnData.data = idPPA.ToString();
                            oReturnData.data1 = idPPA.ToString();
                        }
                        else
                        {
                            oReturnData.isError = true;
                            oReturnData.data = "Error al Grabar en la base.";
                        }
                    }
                    else
                    {
                        oReturnData.isError = true;
                        oReturnData.data = mensajeValidacion;
                    }
                }
                else
                {
                    oReturnData.isError = true;
                    oReturnData.data = "Error al Grabar";
                }
            }
            else
            {
                oReturnData.isError = true;
                oReturnData.data = "Acceso denegado.";
            }
         
            return oReturnData;
        }

        [HttpPost]
        public ReturnData ActividadesBorra(int id)
        {
            ReturnData d = new ReturnData();
            int idc;
            try
            {
       

                Planificacion_Proyecto_Actividades ppab = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(id);

                List<NotaPedido_Detalle> lstNPD = new NotaPedido_DetalleNegocio().Get_Orden_PPA(ppab.Id);

                if (lstNPD.Count>0)
                {
                    d.isError = true;
                    d.data = "No se puede eliminar porque tiene nota de pedido asociada. Por favor borrarla manualmente.";
                }
                else
                {
                    idc = new Planificacion_Proyecto_ActividadesNegocio().Delete(ppab);


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

        public partial class PlanProyActividad
        {
            public int Id { get; set; }
            public int IdProyecto { get; set; }
            public string Fecha_Est_Incio { get; set; }
            public string Fecha_Est_Fin { get; set; }
            public string Cantidad { get; set; }
        }
        [HttpPost]
        public object ActualizarPlanProyActividad(PlanProyActividad oPlanProyActividad)
        {
            var result = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null && oUsuarioLogueado.Id > 0)
                {
                    DateTime fecEstInicio = new DateTime();
                    DateTime fecEstFin = new DateTime();
                    if (!string.IsNullOrEmpty(oPlanProyActividad.Fecha_Est_Incio))
                        fecEstInicio = DateTime.Parse(oPlanProyActividad.Fecha_Est_Incio);
                    if (!string.IsNullOrEmpty(oPlanProyActividad.Fecha_Est_Fin))
                        fecEstFin = DateTime.Parse(oPlanProyActividad.Fecha_Est_Fin);
                    string mensajeValidacion = "";
                    mensajeValidacion = fecEstInicio < fecEstFin ?
                        "" : "- La fecha estimada de inicio debe ser menor que la fecha estimada de fin.";
                    Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                        .Get_X_Id(oPlanProyActividad.Id, true);
                    if (oPPA.TienePadre)
                    {
                        DateTime fecEstInicioMayor_Padre = oPPA.lPPADep_Padres
                            .Max(ppad => ppad.PPAPadre.Fecha_Est_Incio);
                        mensajeValidacion += fecEstInicioMayor_Padre <= fecEstInicio ? "" :
                            "- La fecha estimada de inicio no puede ser menor que la " +
                            "fecha estimada de inicio de la actividad precedente.";
                        if (string.IsNullOrEmpty(mensajeValidacion))
                        {
                            if (!oPPA.oPPADep_PadrePrimero.PPAPadre.Solapable &&
                                oPPA.Fecha_Est_Incio > oPPA.oPPADep_PadrePrimero.PPAPadre.Fecha_Est_Incio &&
                                oPPA.Fecha_Est_Incio < oPPA.oPPADep_PadrePrimero.PPAPadre.Fecha_Est_Fin)
                            {
                                mensajeValidacion += "- La actividad padre (primera) no permite solapamiento.";
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(mensajeValidacion))
                    {
                        if (oPPA.Finalizados == false)
                        {
                            if (fecEstInicio < fecEstFin)
                            {
                                if (fecEstInicio != oPPA.Fecha_Est_Incio && oPPA.Avance > 0)
                                {
                                    result = new
                                    {
                                        error = true,
                                        message = "La fecha estimada de inicio no puede modificarse cuando " +
                                        "la tarea ya ha iniciado. Se cancela la acción."
                                    };
                                }
                                else
                                {
                                    ResultActualizacionArbolPPA oRAAPPA = new ResultActualizacionArbolPPA();
                                    oRAAPPA.IdPlanProyOrigenMovimiento = oPlanProyActividad.Id;
                                    oRAAPPA.IdPlanProyActividad = oPlanProyActividad.Id;
                                    oRAAPPA.FecEstInicio_Nueva = fecEstInicio;
                                    oRAAPPA.FecEstFin_Nueva_Original = fecEstFin;
                                    oRAAPPA.FecEstFin_Nueva = fecEstFin;
                                    oRAAPPA.CambioAplicable = true;
                                    oRAAPPA.message = string.Empty;
                                    oRAAPPA.lPlanProyActividades_Resultante = new List<Planificacion_Proyecto_Actividades>();
                                    oRAAPPA = new Planificacion_Proyecto_ActividadesNegocio()
                                        .AplicarActualizacionCascada(oRAAPPA, false);
                                    List<Planificacion_Proyecto_Actividades> lPPAs = new Planificacion_Proyecto_ActividadesNegocio()
                                        .Get_X_IdProyecto(oPlanProyActividad.IdProyecto);
                                    DateTime fecEstFinProyecto = lPPAs.Max(ppa => ppa.Fecha_Est_Fin);
                                    result = new
                                    {
                                        oResult = oRAAPPA,
                                        FecEstimadaFin = fecEstFinProyecto.ToString("dd/MM/yyyy")
                                    };
                                }
                            }
                            else
                            {
                                result = new
                                {
                                    error = true,
                                    message = "Fechas incorrectas."
                                };
                            }
                        }
                        else
                        {
                            result = new
                            {
                                error = true,
                                message = "No se puede modificar una Actividad Finalizada."
                            };
                        }
                    }
                    else
                    {
                        result = new
                        {
                            error = true,
                            message = mensajeValidacion
                        };
                    }
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
                    message = "Error: PlanificacionController: ActualizarPlanProyActividad(Planificacion_Proyecto_Actividades): exception.",
                    ex = ex
                };
            }
            return result;
        }

        [HttpPost]
        public object AgregarDepedencia(PlanProyAct_Dependencia oPPA_Dependencia)
        {
            object result = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null && oUsuarioLogueado.Id > 0)
                {
                    if (oPPA_Dependencia.PPAHijaId > 0 && oPPA_Dependencia.PPAPadreId > 0)
                    {
                        PlanProyAct_Dependencia oPPA_DepOriginal = new PlanProyAct_DependenciaNegocio()
                            .Get_x_Ids(oPPA_Dependencia.PPAPadreId, oPPA_Dependencia.PPAHijaId);
                        if (oPPA_DepOriginal == null)
                        {
                            oPPA_DepOriginal = new PlanProyAct_Dependencia();
                            oPPA_DepOriginal.PPAPadreId = oPPA_Dependencia.PPAPadreId;
                            oPPA_DepOriginal.PPAHijaId = oPPA_Dependencia.PPAHijaId;
                            oPPA_DepOriginal.FecCreacion = DateTime.Now;
                            oPPA_DepOriginal.IdUsuarioCreo = oUsuarioLogueado.Id;
                            new PlanProyAct_DependenciaNegocio().Insert(oPPA_DepOriginal);
                        }
                        result = new
                        {
                            message = "Dependencia generada con éxito."
                        };
                    }
                    else
                    {
                        result = new
                        {
                            error = true,
                            message = "Datos incompletos."
                        };
                    }
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
                    message = "Error: PlanificacionController: AgregaDepedencia(Dependencia): exception.",
                    ex = ex
                };
            }
            return result;
        }

        [HttpPost]
        public object BorrarDepedencia(PlanProyAct_Dependencia oPPA_Dependencia)
        {
            object result = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null && oUsuarioLogueado.Id > 0)
                {
                    PlanProyAct_Dependencia oPPA_DepOriginal = new PlanProyAct_DependenciaNegocio()
                        .Get_x_Ids(oPPA_Dependencia.PPAPadreId, oPPA_Dependencia.PPAHijaId);
                    if (oPPA_Dependencia != null)
                        new PlanProyAct_DependenciaNegocio().Delete(oPPA_DepOriginal);
                    result = new
                    {
                        message = "Dependencia eliminada con éxito."
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
                    message = "Error: PlanificacionController: BorrarDepedencia(Dependencia): exception.",
                    ex = ex
                };
            }
            return result;
        }

        public KeyValuePair<bool, string> ValidarRestricionesArbol(PlanProyActividad oPlanProyActividad)
        {
            var result = new KeyValuePair<bool, string>();
            try
            {
                Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_Actividades();
                if (oPlanProyActividad.Id > 0)
                    oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                        .Get_X_Id(oPlanProyActividad.Id);
                Planificacion_Proyecto_Actividades oPPA_Padre = new Planificacion_Proyecto_Actividades();
                if (oPPA.IdPadre > 0)
                    oPPA_Padre = new Planificacion_Proyecto_ActividadesNegocio()
                        .Get_X_Id(oPPA.IdPadre);
                List<Planificacion_Proyecto_Actividades> lPAA_Hijas = new Planificacion_Proyecto_ActividadesNegocio()
                    .Get_X_IdPadre(oPPA.Id);
                bool cambioAplicable = true;
                string message = "";

                #region VERFICAMOS QUE NO SE SOLAPE CON PADRE
                if (oPPA_Padre.Id > 0 && !oPPA.Solapable)
                {
                    if (!string.IsNullOrEmpty(oPlanProyActividad.Fecha_Est_Incio))
                    {
                        DateTime fecEstInicio = DateTime.Parse(oPlanProyActividad.Fecha_Est_Incio);
                        cambioAplicable = oPPA_Padre.Fecha_Est_Fin < fecEstInicio;
                        if (!cambioAplicable)
                            message = "- No se puede aplicar el cambios ya que la actividad " +
                                "que se intenta modificar no es solapable. Las fechas elegidas " +
                                " la solaparían con su actividad precedente.";
                    }
                }

                #endregion

                #region VERIFICAMOS QUE NO SE SOLAPE CON LAS HIJAS
                //if (!oPPA.Solapable)
                //{
                //    if (!string.IsNullOrEmpty(oPlanProyActividad.Fecha_Est_Fin))
                //    {
                //        DateTime fecEstFin = DateTime.Parse(oPlanProyActividad.Fecha_Est_Fin);
                //        cambioAplicable = !cambioAplicable ?
                //            cambioAplicable :
                //            lPAA_Hijas.Where(ppah => ppah.Fecha_Est_Incio < fecEstFin).Count() == 0;
                //        if (!cambioAplicable)
                //            message += "- No se puede aplicar el cambios ya que la actividad " +
                //                "que se intenta modificar no es solapable. Las fechas elegidas " +
                //                " la solaparían con alguna de sus actividades hijas.";
                //    }
                //}
                #endregion

                result = new KeyValuePair<bool, string>(cambioAplicable, message);

            }
            catch (Exception ex)
            {
                throw new Exception("Error: PlanificacionController: ValidarRestricionesArbol(PlanProyActividad): exception.", ex);
            }
            return result;
        }

        [HttpGet]
        public object Get_PlanPoryAct_Padres_Id(int id)
        {
            var result = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null && oUsuarioLogueado.Id > 0)
                {
                    Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio()
                        .Get_X_Id(id, true);
                    oPPA.lPPADep_Padres.ForEach(ppad =>
                    {
                        ppad.PPAPadre = new Planificacion_Proyecto_ActividadesNegocio()
                            .Get_X_Id(ppad.PPAPadre.Id, true, true);
                    });
                    result = new
                    {
                        lPPA_Padres = oPPA.lPPADep_Padres.Select(ppad => ppad.PPAPadre).ToList()
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
                    message = "Error: PlanificacionController: GetPlanProyActividad_X_Id(int): exception.",
                    ex = ex.InnerException
                };
            }
            return result;
        }

        #endregion

        #region Actividades Logisticas

        public ActionResult Actividades_Log()
        {
            return View();
        }

        public ActionResult _Actividades_LogGrilla()
        {
            List<Planificacion_Proyecto_Actividades_Log> Lista = new List<Planificacion_Proyecto_Actividades_Log>();
            Lista = new Planificacion_Proyecto_Actividades_Log_Negocio().Get_All_Planificacion_Proyecto_Actividades_Log();
            return PartialView(Lista);
        }

        public ActionResult _Actividades_LogAbm(int id)
        {
            ItemActividades_Log data = new ItemActividades_Log();
            if (id != 0)
            {
                data.pp = new Planificacion_Proyecto_Actividades_Log_Negocio().Get_One_Planificacion_Proyecto_Actividades_Log(id);
            }

            data.Planificacion_Proyecto_Actividades = new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades();

            return PartialView(data);
        }

        [HttpPost]
        public ReturnData Actividades_LogGraba(Planificacion_Proyecto_Actividades_Log data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new Planificacion_Proyecto_Actividades_Log_Negocio().Update(data);
                }
                else
                {
                    idc = new Planificacion_Proyecto_Actividades_Log_Negocio().Insert(data);
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
        public ReturnData GetActividades(int id)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = new Planificacion_ActividadesNegocio().Get_All_Planificacion_Actividades(id);

            return data;
        }

        [HttpGet]
        public ReturnData GetActividadesFiltradas( int IdProyecto, int IdRubro)
        {
            ReturnData data = new ReturnData();
           List<int> ListaUbicacionId  = new Proyecto_UbicacionesNegocio()
                .Get_X_IdProyecto(IdProyecto).Select(s=>s.Id).ToList();
            List<Planificacion_Actividades> Lista = new List<Planificacion_Actividades>();

            List < Planificacion_Proyecto_Actividades > ListaActividades= new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades()
                .Where(a => ListaUbicacionId.Contains(a.Proyecto_UbicacionesId) 
                && a.Planificacion_Actividades_RubroId == IdRubro)
                .ToList();

            foreach(var item in ListaActividades)
            {
                Planificacion_Actividades value = new Planificacion_Actividades();

                value.Id = item.Id;
                value.Nombre = item.Actividad;
                Lista.Add(value);
            }
            data.data = Lista;
            data.isError = false;
            return data;
        }

        public partial class Filtro_PlanProyAct
        {
            public int IdProyecto { get; set; }
            public int IdPlanActRubro { get; set; }
            public int IdProyUbicacion { get; set; }
            public List<int> lIdsProyUbicaciones { get; set; }
            public int IdPlanActividad { get; set; }
            public List<int> lIdsPlanActividades { get; set; }
            public string Estado { get; set; }
            public List<string> lEstados { get; set; }
            public List<string> lBusqueda { get; set; }
            public string FechaDesde { get; set; }
            public string FechaHasta { get; set; }
            public bool SoloPlanificadas { get; set; }
            public int IdActividadActual { get; set; }
        }
        [HttpPost]
        public object Get_PlanProyActividades_X_oFiltro(Filtro_PlanProyAct oFiltro)
        {
            var oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null && oUsuarioLogueado.Id > 0)
                {
                    List<Planificacion_Proyecto_Actividades> lPlanProyActividades = new Planificacion_Proyecto_ActividadesNegocio()
                        .Get_X_Ids_Corregido(oFiltro.IdPlanActRubro, oFiltro.IdPlanActividad, oFiltro.IdProyUbicacion, oFiltro.IdActividadActual);

                    List<int> lIdsPlanActRubros = lPlanProyActividades
                        .GroupBy(ppa => ppa.Planificacion_Actividades_RubroId)
                        .Select(g => g.First().Planificacion_Actividades_RubroId)
                        .ToList();
                    List<int> lIdsPlanActividades = lPlanProyActividades
                        .GroupBy(ppa => ppa.Planificacion_ActividadesId)
                        .Select(g => g.First().Planificacion_ActividadesId)
                        .ToList();
                    List<int> lIdsProyUbicaciones = lPlanProyActividades
                        .GroupBy(ppa => ppa.Proyecto_UbicacionesId)
                        .Select(g => g.First().Proyecto_UbicacionesId)
                        .ToList();
                    List<Planificacion_Actividades_Rubro> lPlanActRubros = new Planificacion_Actividades_RubroNegocio()
                        .Get_X_lIds(lIdsPlanActRubros);
                    List<Planificacion_Actividades> lPlanActividades = new Planificacion_ActividadesNegocio()
                        .Get_X_lIds(lIdsPlanActividades);
                    List<Proyecto_Ubicaciones> lProyUbicaciones = new Proyecto_UbicacionesNegocio()
                        .Get_X_lIds(lIdsProyUbicaciones);
                    lPlanProyActividades.ForEach(ppa =>
                    {
                        ppa.Planificacion_Actividades_Rubro = lPlanActRubros
                            .Find(par => par.Id == ppa.Planificacion_Actividades_RubroId);
                        ppa.Planificacion_Actividades = lPlanActividades
                            .Find(pa => pa.Id == ppa.Planificacion_ActividadesId);
                        ppa.Proyecto_Ubicaciones = lProyUbicaciones
                        .Find(pu => pu.Id == ppa.Proyecto_UbicacionesId);
                    });
                    oResult = new
                    {
                        lPlanProyActividades = lPlanProyActividades
                            .Select(ppa => new
                            {
                                ppa.Id,
                                ppa.IdPadre,
                                Rubro = ppa.Planificacion_Actividades_Rubro.Nombre,
                                Actividad = ppa.Planificacion_Actividades.Nombre,
                                Ubicacion = ppa.Proyecto_Ubicaciones.Nombre,
                                FechaEstInicio = ppa.Fecha_Est_Incio.ToString("dd-MM-yyyy"),
                                FechaEstFin = ppa.Fecha_Est_Fin.ToString("dd-MM-yyyy"),
                                ppa.TienePadre,
                                ppa.Detalle
                            })
                            .ToList()
                    };
                }
                else
                {
                    oResult = new
                    {
                        error = true,
                        message = "Acceso denegado"
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    erro = true,
                    message = "Error: PlanificacionController: Get_PlanProyActividades_X_oFiltro(Filtro_PlanProyAct): exception",
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #endregion

        #region Plan de Avance

        public ActionResult Plan_de_Avance(int id)
        {
            Filtro_PlanProyAct oFiltro = new Filtro_PlanProyAct()
            {
                IdProyecto = id,
                lIdsPlanActividades = new List<int>(),
                lIdsProyUbicaciones = new List<int>(),
                lEstados = new List<string>() { "Solo Activas" }
            };
            return View(getAvances(oFiltro));
        }
        public ActionResult Plan_de_Avance_Rubro(int id)
        {
            Filtro_PlanProyAct oFiltro = new Filtro_PlanProyAct()
            {
                IdProyecto = id,
                lIdsPlanActividades = new List<int>(),
                lIdsProyUbicaciones = new List<int>(),
                lEstados = new List<string>() { "Solo Activas" },
                lBusqueda = new List<string>() { "Por Defecto" }
            };
            return View(getAvancesRubro(oFiltro));
        }
        private Res_Gantt getAvances(Filtro_PlanProyAct oFiltro)
        {
            if (oFiltro.lIdsProyUbicaciones == null) oFiltro.lIdsProyUbicaciones = new List<int>();
            if (oFiltro.lIdsPlanActividades == null) oFiltro.lIdsPlanActividades = new List<int>();
            if (oFiltro.lEstados == null) oFiltro.lEstados = new List<string>();
            Res_Gantt oResGantt = new Res_Gantt();
            try
            {
                #region Lista de Actividades con sus Avances para armar el Gantt
                List<ItemAvences> lItemAvances = new List<ItemAvences>();
                Proyecto oProyecto = new ProyectoNegocio().Get_One_Proyecto(oFiltro.IdProyecto);
                oResGantt.IdProyecto = oFiltro.IdProyecto;
                oResGantt.NombreProyecto = oProyecto.Nombre;
                DateTime fecDesde = DateTime.MinValue;
                DateTime fecHasta = DateTime.MinValue;
                if (!string.IsNullOrEmpty(oFiltro.FechaDesde) && !string.IsNullOrEmpty(oFiltro.FechaHasta))
                {
                    fecDesde = DateTime.Parse(oFiltro.FechaDesde);
                    fecHasta = DateTime.Parse(oFiltro.FechaHasta);
                }
                List<Planificacion_Proyecto_Actividades> lPlanProyActividades = new Planificacion_Proyecto_ActividadesNegocio()
                    .Get_X_IdProyecto(oFiltro.IdProyecto);
                List<Planificacion_Proyecto_Actividades> lPlanProyActividades_Filtradas = lPlanProyActividades
                    .Where(ppa => ppa.Proyecto_Ubicaciones.Proyecto.Id == oFiltro.IdProyecto &&
                        (oFiltro.lIdsProyUbicaciones.Count == 0 || oFiltro.lIdsProyUbicaciones.Contains(ppa.Proyecto_UbicacionesId)) &&
                        (oFiltro.lIdsPlanActividades.Count == 0 || oFiltro.lIdsPlanActividades.Contains(ppa.Planificacion_ActividadesId)) &&
                        ((fecDesde == DateTime.MinValue && fecHasta == DateTime.MinValue) ||
                        (ppa.Fecha_Est_Incio >= fecDesde && ppa.Fecha_Est_Fin <= fecHasta)))
                    .OrderBy(o => o.Proyecto_UbicacionesId)
                    .ThenBy(o => o.Fecha_Est_Incio)
                    .ToList();
                DateTime fecProgFin = oProyecto.Fecha_Inicio.AddDays(oProyecto.Duracion);
                DateTime fecEstFin = fecProgFin;
                if (lPlanProyActividades_Filtradas.Count > 0)
                    fecEstFin = lPlanProyActividades_Filtradas.Max(ppa => ppa.Fecha_Est_Fin);
                oResGantt.FecProgramadaFin = fecProgFin.ToString("dd/MM/yyyy");
                oResGantt.FecEstimadaFin = fecEstFin.ToString("dd/MM/yyyy");
                oResGantt.DiasDiferencia = (int)(fecEstFin - fecProgFin).TotalDays;
                int mCont = 0;
                foreach (var oPlanProyActividad in lPlanProyActividades_Filtradas)
                {
                    if (oPlanProyActividad.Fecha_Est_Incio <= oPlanProyActividad.Fecha_Est_Fin)
                    {
                        ////REgistro Estimado
                        ItemAvences oIAvanceEstimado = new ItemAvences();

                        oIAvanceEstimado.type = "Estimado";
                        oIAvanceEstimado.ubicacion = oPlanProyActividad.Proyecto_Ubicaciones.Nombre;
                        oIAvanceEstimado.id = oPlanProyActividad.Id;
                        oIAvanceEstimado.parentID = oPlanProyActividad.IdPadre;
                        oIAvanceEstimado.title = oPlanProyActividad.Planificacion_Actividades.Nombre;
                        oIAvanceEstimado.rubro_nombre = oPlanProyActividad.Planificacion_Actividades_Rubro.Nombre;
                        oIAvanceEstimado.IdPlanActividad = oPlanProyActividad.Planificacion_ActividadesId;
                        oIAvanceEstimado.actividades_nombre = oPlanProyActividad.Planificacion_Actividades.Nombre;
                        if (oPlanProyActividad.Detalle != null)
                        {
                            oIAvanceEstimado.comentario = oPlanProyActividad.Detalle;
                        }
                        else
                        {
                            oIAvanceEstimado.comentario = "Sin Comentario";
                        }
                        oIAvanceEstimado.comentario = oPlanProyActividad.Detalle;
                        // a.orderID = act.Proyecto_UbicacionesId;
                        oIAvanceEstimado.orderID = mCont++;
                        oIAvanceEstimado.start = oPlanProyActividad.Fecha_Est_Incio.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fechaEstInicio = oPlanProyActividad.Fecha_Est_Incio.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fechaEstInicioOriginal = oPlanProyActividad.FecEstInicio_Original.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fecInicioReal = oPlanProyActividad.Fecha_Real_Incio == DateTime.MinValue ?
                            "Sin Registrar" : oPlanProyActividad.Fecha_Real_Incio.ToString("dd-MM-yyyy");
                        oIAvanceEstimado.end = oPlanProyActividad.Fecha_Est_Fin.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fechaEstFin = oPlanProyActividad.Fecha_Est_Fin.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fechaEstFinOriginal = oPlanProyActividad.FecEstFin_Original.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fecFinReal = oPlanProyActividad.Fecha_Real_Fin == DateTime.MinValue ?
                            "Sin Registrar" : oPlanProyActividad.Fecha_Real_Fin.ToString("dd-MM-yyyy");
                        oIAvanceEstimado.percentComplete = 0;
                        oIAvanceEstimado.avance = (float)Convert.ToDecimal(oPlanProyActividad.Avance.ToString("##0.00")) / 100;
                        oIAvanceEstimado.summary = false;
                        oIAvanceEstimado.expanded = false;

                        //Registro Real
                        ItemAvences oIAvanceReal = new ItemAvences();
                        oIAvanceReal.type = "Real";
                        oIAvanceReal.id = oPlanProyActividad.Id;
                        oIAvanceReal.IdProyUbicacion = oPlanProyActividad.Proyecto_UbicacionesId;
                        oIAvanceReal.ubicacion = oPlanProyActividad.Proyecto_Ubicaciones.Nombre;
                        oIAvanceReal.title = oPlanProyActividad.Proyecto_Ubicaciones.Nombre + "-" +
                            oPlanProyActividad.Planificacion_Actividades.Nombre;
                        oIAvanceReal.rubro_nombre = oPlanProyActividad.Planificacion_Actividades_Rubro.Nombre;
                        oIAvanceReal.IdPlanActividad = oPlanProyActividad.Planificacion_ActividadesId;
                        oIAvanceReal.actividades_nombre = oPlanProyActividad.Planificacion_Actividades.Nombre;

                        if (oPlanProyActividad.Detalle != null)
                        {
                            oIAvanceReal.comentario = oPlanProyActividad.Detalle;
                        }
                        else
                        {
                            oIAvanceReal.comentario = "Sin Comentario";
                        }
                      
                        oIAvanceReal.orderID = mCont++;

                        oIAvanceReal.fechaEstInicio = oPlanProyActividad.Fecha_Est_Incio.ToString("yyyy/MM/dd");
                        oIAvanceReal.fechaEstInicioOriginal = oPlanProyActividad.FecEstInicio_Original.ToString("yyyy/MM/dd");
                        oIAvanceReal.fechaEstFin = oPlanProyActividad.Fecha_Est_Fin.ToString("yyyy/MM/dd");
                        oIAvanceReal.fechaEstFinOriginal = oPlanProyActividad.FecEstFin_Original.ToString("yyyy/MM/dd");

                        if (oPlanProyActividad.Fecha_Real_Incio > DateTime.MinValue)
                            oIAvanceReal.start = oPlanProyActividad.Fecha_Real_Incio.ToString("yyyy/MM/dd");
                        else
                            oIAvanceReal.start = oPlanProyActividad.Fecha_Est_Incio.ToString("yyyy/MM/dd");

                        if (oPlanProyActividad.Fecha_Real_Fin > DateTime.MinValue)
                            oIAvanceReal.end = oPlanProyActividad.Fecha_Real_Fin.ToString("yyyy/MM/dd");
                        else
                        {
                            if (oPlanProyActividad.Fecha_Real_Incio != DateTime.MinValue)
                            {
                                // obtenemos el ultimo registro de avance
                                ParteDiario_Actividades oPDactividad = new ParteDiario_ActividadesNegocio()
                                    .Get_X_IdPlanProyActvidad_UltimoConAvance(oPlanProyActividad.Id);
                                if (oPDactividad != null)
                                    oIAvanceReal.end = oPDactividad.ParteDiario.Fecha_Creacion.ToString("yyyy/MM/dd");
                                else
                                    oIAvanceReal.end = DateTime.Now.ToString("yyyy/MM/dd");
                            }
                            else oIAvanceReal.end = oIAvanceReal.start;
                        }
                        oIAvanceReal.percentComplete = (float)Convert.ToDecimal(oPlanProyActividad.Avance.ToString("##0.00")) / 100;
                        oIAvanceReal.avance = (float)Convert.ToDecimal(oPlanProyActividad.Avance.ToString("##0.00")) / 100;
                        oIAvanceReal.summary = true;
                        oIAvanceReal.expanded = false;
                        oIAvanceReal.Finalizado = oPlanProyActividad.Finalizados ? true : false;

                        //Defino si esta o no vencido
                        oIAvanceReal.Vencido = false;
                        //1-No debe estar finalizado
                        if (oIAvanceReal.Finalizado != true)
                        {
                            //2-Verifico que tenga fecha estimada de FIN
                            if (oPlanProyActividad.Fecha_Real_Fin != DateTime.MinValue)
                            {
                                //3-Verifico que la estimada de finalizacion sea menor a la de hoy
                                if (oPlanProyActividad.Fecha_Est_Fin <= DateTime.Now)
                                {
                                    oIAvanceReal.Vencido = true;
                                }
                            }
                            else
                            {
                                //No tiene fecha estimada de FIN, tomo la fecha de hoy
                                if (oPlanProyActividad.Fecha_Est_Fin <= DateTime.Now)
                                {
                                    oIAvanceReal.Vencido = true;
                                }
                            }
                        }
                        oIAvanceReal.Estado = Math.Round(oIAvanceReal.percentComplete * 100, 2).ToString() + "%";
                        oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_Programada;
                        oIAvanceEstimado.Finalizado = oIAvanceReal.Finalizado;
                        oIAvanceEstimado.Vencido = oIAvanceReal.Vencido;
                        if (oIAvanceEstimado.Finalizado)
                            oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_Finalizada;
                        else if (oIAvanceEstimado.Vencido)
                            oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_Vencida;
                        else if (oPlanProyActividad.Fecha_Est_Incio < DateTime.Now &&
                            oPlanProyActividad.Fecha_Est_Fin > DateTime.Now)
                        {
                            if (oPlanProyActividad.Avance == 0)
                                oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_Atrasada;
                            else
                                oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_EnCurso;
                        }

                        #region FILTRAMOS SEGÚN LOS ESTADOS SELECCIONADOS COMO FILTROS
                        if (oFiltro.lEstados.Count == 0)
                        {
                            lItemAvances.Add(oIAvanceEstimado);
                            if (!oFiltro.SoloPlanificadas) lItemAvances.Add(oIAvanceReal);
                        }
                        else if (oFiltro.lEstados.Contains("Solo Activas"))
                        {
                            if (oIAvanceEstimado.Estado != ValoresUtiles.Estado_PPA_Finalizada)
                            {
                                lItemAvances.Add(oIAvanceEstimado);
                                if (!oFiltro.SoloPlanificadas) lItemAvances.Add(oIAvanceReal);
                            }
                        }
                        else if (oFiltro.lEstados.Contains(oIAvanceEstimado.Estado))
                        {
                            lItemAvances.Add(oIAvanceEstimado);
                            if (!oFiltro.SoloPlanificadas) lItemAvances.Add(oIAvanceReal);
                        }
                        #endregion
                    }
                }

                //var newList = list.OrderByDescending(x => x.Product.Name)
                //  .ThenBy(x => x.Product.Price)
                //  .ToList();

                lItemAvances = lItemAvances.OrderBy(s=>s.fechaEstInicio).ToList();  
                oResGantt.lAvances = lItemAvances;
                #endregion

                #region Listas adicionales
                List<int> lIdsPPAs = oResGantt.lAvances
                        .Where(ia => ia.type == "Estimado")
                        .Select(ia => ia.id)
                        .ToList();
                List<PlanProyAct_Dependencia> lPPA_Dependencias = new PlanProyAct_DependenciaNegocio()
                    .Get_By_lIdsPPA_Hijas(lIdsPPAs, true);
                oResGantt.lPPA_Dependencias = lPPA_Dependencias.Select(ppad => new
                {
                    ppad.Id,
                    ppad.PredecessorId,
                    ppad.SuccessorId,
                    ppad.Type
                }).ToList();
                oResGantt.lAvances.ForEach(a =>
                {
                    List<PlanProyAct_Dependencia> lPPADependenciasPadres = lPPA_Dependencias
                        .Where(ppad => ppad.PPAHijaId == a.id)
                        .ToList();
                    if (lPPADependenciasPadres.Count == 0)
                        a.DetalleActPadres = "Sin actividades padres.";
                    else
                    {
                        lPPADependenciasPadres.ForEach(ppad =>
                        {
                            a.DetalleActPadres += "- " + ppad.PPAPadre.Ubicacion + " - " + ppad.PPAPadre.Actividad + "<br/>";
                        });
                    }
                    List<PlanProyAct_Dependencia> lPPADependenciasHijas = lPPA_Dependencias
                        .Where(ppad => ppad.PPAPadreId == a.id)
                        .ToList();
                    if (lPPADependenciasHijas.Count == 0)
                        a.DetalleActHijas = "Sin actividades hijas.";
                    else
                    {
                        lPPADependenciasHijas.ForEach(ppad =>
                        {
                            a.DetalleActHijas += "- " + ppad.PPAHija.Ubicacion + " - " + ppad.PPAHija.Actividad + "<br/>";
                        });
                    }
                });
                oResGantt.lActividades = lItemAvances
                    .GroupBy(ia => ia.IdPlanActividad)
                    .Select(g => g.First())
                    .Select(ia => new { id = ia.IdPlanActividad, nombre = ia.actividades_nombre })
                    .OrderBy(ia => ia.nombre)
                    .ToList();
                oResGantt.lUbicaciones = lItemAvances
                    .GroupBy(ia => ia.IdProyUbicacion)
                    .Select(g => g.First())
                    .Select(ia => new { id = ia.IdProyUbicacion, nombre = ia.ubicacion })
                    .ToList();
                List<string> lEstados = new List<string>() { "Todas", "Solo Activas" };
                lEstados.AddRange(ValoresUtiles.Get_lEstados_PPA());
                oResGantt.lEstados = lEstados;
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error: PlanificacionController: getAvances(Filtro_PlanProyAct): exception.", ex);
            }
             return oResGantt;
        }

        public List<Planificacion_Proyecto_Actividades> Actividades_Filtradas(List<Planificacion_Proyecto_Actividades> lPlanProyActividades, Filtro_PlanProyAct oFiltro)
        {
            List<Planificacion_Proyecto_Actividades> lPlanProyActividades_Filtradas = new List<Planificacion_Proyecto_Actividades>();
            if ((oFiltro.lBusqueda.Count() == 3 || oFiltro.lBusqueda.Count() == 0 || oFiltro.lBusqueda[0].ToString() == "Por Defecto") || (oFiltro.lBusqueda.Count() == 1 && oFiltro.lBusqueda[0].ToString() == "Cronologicamente") ||
                (oFiltro.lBusqueda.Count() == 2 && (oFiltro.lBusqueda.Contains("Por Ubicacion") && oFiltro.lBusqueda.Contains("Cronologicamente"))))
            {
                lPlanProyActividades_Filtradas = lPlanProyActividades
                    .OrderBy(o => o.Fecha_Est_Incio)
                    .ThenBy(o =>o.Proyecto_UbicacionesId)
                    .ThenBy(o => o.Planificacion_Actividades_RubroId)
                    .ToList();
            }
            else if (oFiltro.lBusqueda.Count() == 1 && oFiltro.lBusqueda[0].ToString() == "Por Rubro")
            {
                lPlanProyActividades_Filtradas = lPlanProyActividades
                    .OrderBy(o => o.Planificacion_Actividades_RubroId)
                    .ThenBy(o => o.Fecha_Est_Incio)
                    .ThenBy(o => o.Proyecto_UbicacionesId)
                    .ToList();
            }
            else if (oFiltro.lBusqueda.Count() == 1 && oFiltro.lBusqueda[0].ToString() == "Por Ubicacion")
            {
                lPlanProyActividades_Filtradas = lPlanProyActividades
                    .OrderBy(o => o.Proyecto_UbicacionesId)
                    .ThenBy(o => o.Fecha_Est_Incio)
                    .ThenBy(o => o.Planificacion_Actividades_RubroId)
                    .ToList();
            }
            else if (oFiltro.lBusqueda.Count() == 2 && (oFiltro.lBusqueda.Contains("Por Ubicacion") && oFiltro.lBusqueda.Contains("Por Rubro")))
            {
                lPlanProyActividades_Filtradas = lPlanProyActividades
                    .OrderBy(o => o.Proyecto_UbicacionesId)
                    .ThenBy(o => o.Planificacion_Actividades_RubroId)
                    .ThenBy(o => o.Fecha_Est_Incio)
                    .ToList();
            }
            else if (oFiltro.lBusqueda.Count() == 2 && (oFiltro.lBusqueda.Contains("Por Rubro") && oFiltro.lBusqueda.Contains("Cronologicamente")))
            {
                lPlanProyActividades_Filtradas = lPlanProyActividades
                    .OrderBy(o => o.Fecha_Est_Incio)
                    .ThenBy(o => o.Planificacion_Actividades_RubroId)
                    .ThenBy(o => o.Proyecto_UbicacionesId)
                    .ToList();
            }
            return lPlanProyActividades_Filtradas;
        }

        private Res_Gantt getAvancesRubro(Filtro_PlanProyAct oFiltro)
        {
            if (oFiltro.lIdsProyUbicaciones == null) oFiltro.lIdsProyUbicaciones = new List<int>();
            if (oFiltro.lIdsPlanActividades == null) oFiltro.lIdsPlanActividades = new List<int>();
            if (oFiltro.lEstados == null) oFiltro.lEstados = new List<string>();
            if (oFiltro.lBusqueda == null) oFiltro.lBusqueda = new List<string>();
            Res_Gantt oResGantt = new Res_Gantt();
            try
            {
                #region Lista de Actividades con sus Avances para armar el Gantt
                List<ItemAvences> lItemAvances = new List<ItemAvences>();
                Proyecto oProyecto = new ProyectoNegocio().Get_One_Proyecto(oFiltro.IdProyecto);
                oResGantt.IdProyecto = oFiltro.IdProyecto;
                oResGantt.NombreProyecto = oProyecto.Nombre;
                DateTime fecDesde = DateTime.MinValue;
                DateTime fecHasta = DateTime.MinValue;
                if (!string.IsNullOrEmpty(oFiltro.FechaDesde))
                {
                    fecDesde = DateTime.Parse(oFiltro.FechaDesde);
                }
                else
                {
                    fecDesde = DateTime.MinValue;
                }
                if (!string.IsNullOrEmpty(oFiltro.FechaHasta))
                {
                    fecHasta = DateTime.Parse(oFiltro.FechaHasta);
                }
                else
                {
                    fecHasta = fecDesde;
                }

                List<Planificacion_Proyecto_Actividades> lPlanProyActividades = new Planificacion_Proyecto_ActividadesNegocio()
                    .Get_Act_IdProyecto_X_Rubro(oFiltro.IdProyecto);

                //Metodo para ordenar segun busqueda

                List<Planificacion_Proyecto_Actividades> lPlanProyActividades_Filtrada = lPlanProyActividades
                    .Where(ppa => ppa.Proyecto_Ubicaciones.ProyectoId == oFiltro.IdProyecto &&
                        (oFiltro.lIdsProyUbicaciones.Count == 0 || oFiltro.lIdsProyUbicaciones.Contains(ppa.Proyecto_UbicacionesId)) &&
                        ((fecDesde == DateTime.MinValue && fecHasta == DateTime.MinValue) ||
                        (ppa.Fecha_Est_Incio <= fecDesde && ppa.Fecha_Est_Fin >= fecDesde) || (ppa.Fecha_Est_Incio <= fecHasta && ppa.Fecha_Est_Fin >= fecHasta)))
                    .ToList();

                List<Planificacion_Proyecto_Actividades> lPlanProyActividades_Filtradas = Actividades_Filtradas(lPlanProyActividades_Filtrada, oFiltro);

                DateTime fecProgFin = oProyecto.Fecha_Inicio.AddDays(oProyecto.Duracion);
                DateTime fecEstFin = fecProgFin;
                if (lPlanProyActividades_Filtradas.Count > 0)
                    fecEstFin = lPlanProyActividades_Filtradas.Max(ppa => ppa.Fecha_Est_Fin);
                oResGantt.FecProgramadaFin = fecProgFin.ToString("dd/MM/yyyy");
                oResGantt.FecEstimadaFin = fecEstFin.ToString("dd/MM/yyyy");
                oResGantt.DiasDiferencia = (int)(fecEstFin - fecProgFin).TotalDays;
                int mCont = 0;
                foreach (var oPlanProyActividad in lPlanProyActividades_Filtradas)
                {
                    if (oPlanProyActividad.Fecha_Est_Incio <= oPlanProyActividad.Fecha_Est_Fin)
                    {
                        ////REgistro Estimado
                        ItemAvences oIAvanceEstimado = new ItemAvences();

                        oIAvanceEstimado.type = "Estimado";
                        oIAvanceEstimado.ubicacion = oPlanProyActividad.Proyecto_Ubicaciones.Nombre;
                        oIAvanceEstimado.id = oPlanProyActividad.Id;
                        oIAvanceEstimado.parentID = oPlanProyActividad.IdPadre;
                        oIAvanceEstimado.title = oPlanProyActividad.Planificacion_Actividades_Rubro.Nombre;
                        //oIAvanceEstimado.title = oPlanProyActividad.Proyecto_Ubicaciones.Nombre;
                        oIAvanceEstimado.rubro_nombre = oPlanProyActividad.Planificacion_Actividades_Rubro.Nombre;
                        oIAvanceEstimado.IdPlanActividad = oPlanProyActividad.Planificacion_ActividadesId;
                       // oIAvanceEstimado.actividades_nombre = oPlanProyActividad.Planificacion_Actividades.Nombre;
                        oIAvanceEstimado.comentario = oPlanProyActividad.Detalle;
                        //a.orderID = act.Proyecto_UbicacionesId;
                        oIAvanceEstimado.orderID = mCont++;

                        //22/08/2022 - VAmos aponer que la fecha de inicio es la fecha inicial del FILTRO que se envio, asi aparece de "hoy" en adelante
                        if (fecDesde >= oPlanProyActividad.Fecha_Est_Incio)
                        {
                            oIAvanceEstimado.start = fecDesde.ToString("yyyy/MM/dd");
                        }
                        else
                        {
                            oIAvanceEstimado.start = oPlanProyActividad.Fecha_Est_Incio.ToString("yyyy/MM/dd");
                        }
                        

                        oIAvanceEstimado.fechaEstInicio = oPlanProyActividad.Fecha_Est_Incio.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fechaEstInicioOriginal = oPlanProyActividad.FecEstInicio_Original.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fecInicioReal = oPlanProyActividad.Fecha_Real_Incio == DateTime.MinValue ?
                            "Sin Registrar" : oPlanProyActividad.Fecha_Real_Incio.ToString("dd-MM-yyyy");
                        oIAvanceEstimado.end = oPlanProyActividad.Fecha_Est_Fin.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fechaEstFin = oPlanProyActividad.Fecha_Est_Fin.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fechaEstFinOriginal = oPlanProyActividad.FecEstFin_Original.ToString("yyyy/MM/dd");
                        oIAvanceEstimado.fecFinReal = oPlanProyActividad.Fecha_Real_Fin == DateTime.MinValue ?
                            "Sin Registrar" : oPlanProyActividad.Fecha_Real_Fin.ToString("dd-MM-yyyy");
                        oIAvanceEstimado.percentComplete = 0;
                        oIAvanceEstimado.avance = (float)Convert.ToDecimal(oPlanProyActividad.Avance.ToString("##0.00")) / 100;
                        oIAvanceEstimado.summary = false;
                        oIAvanceEstimado.expanded = false;

                        //Registro Real
                        //Si la fecha desde es a partir de HOY, no mostramos nada del real por que es a futuro
                        ItemAvences oIAvanceReal = new ItemAvences();
                        if (fecDesde.Date != DateTime.Now.Date)
                        {
                            oIAvanceReal.type = "Real";
                            oIAvanceReal.id = oPlanProyActividad.Id;
                            oIAvanceReal.IdProyUbicacion = oPlanProyActividad.Proyecto_UbicacionesId;
                            oIAvanceReal.ubicacion = oPlanProyActividad.Proyecto_Ubicaciones.Nombre;
                            oIAvanceReal.title = oPlanProyActividad.Planificacion_Actividades_Rubro.Nombre;
                            //oIAvanceReal.title = oPlanProyActividad.Proyecto_Ubicaciones.Nombre;
                            //+ "-" + oPlanProyActividad.Planificacion_Actividades.Nombre;
                            oIAvanceReal.rubro_nombre = oPlanProyActividad.Planificacion_Actividades_Rubro.Nombre;
                            oIAvanceReal.IdPlanActividad = oPlanProyActividad.Planificacion_ActividadesId;
                            //oIAvanceReal.actividades_nombre = oPlanProyActividad.Planificacion_Actividades.Nombre;
                            oIAvanceReal.comentario = oPlanProyActividad.Detalle;
                            oIAvanceReal.orderID = mCont++;

                            oIAvanceReal.fechaEstInicio = oPlanProyActividad.Fecha_Est_Incio.ToString("yyyy/MM/dd");
                            oIAvanceReal.fechaEstInicioOriginal = oPlanProyActividad.FecEstInicio_Original.ToString("yyyy/MM/dd");
                            oIAvanceReal.fechaEstFin = oPlanProyActividad.Fecha_Est_Fin.ToString("yyyy/MM/dd");
                            oIAvanceReal.fechaEstFinOriginal = oPlanProyActividad.FecEstFin_Original.ToString("yyyy/MM/dd");

                            if (oPlanProyActividad.Fecha_Real_Incio > DateTime.MinValue)
                                oIAvanceReal.start = oPlanProyActividad.Fecha_Real_Incio.ToString("yyyy/MM/dd");
                            else
                                oIAvanceReal.start = oPlanProyActividad.Fecha_Est_Incio.ToString("yyyy/MM/dd");

                            if (oPlanProyActividad.Fecha_Real_Fin > DateTime.MinValue)
                                oIAvanceReal.end = oPlanProyActividad.Fecha_Real_Fin.ToString("yyyy/MM/dd");
                            else
                            {
                                if (oPlanProyActividad.Fecha_Real_Incio != DateTime.MinValue)
                                {
                                    // obtenemos el ultimo registro de avance
                                    ParteDiario_Actividades oPDactividad = new ParteDiario_ActividadesNegocio()
                                        .Get_X_IdPlanProyActvidad_UltimoConAvance(oPlanProyActividad.Id);
                                    if (oPDactividad != null)
                                        oIAvanceReal.end = oPDactividad.ParteDiario.Fecha_Creacion.ToString("yyyy/MM/dd");
                                    else
                                        oIAvanceReal.end = DateTime.Now.ToString("yyyy/MM/dd");
                                }
                                else oIAvanceReal.end = oIAvanceReal.start;
                            }
                            oIAvanceReal.percentComplete = (float)Convert.ToDecimal(oPlanProyActividad.Avance.ToString("##0.00")) / 100;
                            oIAvanceReal.avance = (float)Convert.ToDecimal(oPlanProyActividad.Avance.ToString("##0.00")) / 100;
                            oIAvanceReal.summary = true;
                            oIAvanceReal.expanded = false;
                            oIAvanceReal.Finalizado = oPlanProyActividad.Finalizados ? true : false;

                            //Defino si esta o no vencido
                            oIAvanceReal.Vencido = false;
                            //1-No debe estar finalizado
                            if (oIAvanceReal.Finalizado != true)
                            {
                                //2-Verifico que tenga fecha estimada de FIN
                                if (oPlanProyActividad.Fecha_Real_Fin != DateTime.MinValue)
                                {
                                    //3-Verifico que la estimada de finalizacion sea menor a la de hoy
                                    if (oPlanProyActividad.Fecha_Est_Fin <= DateTime.Now)
                                    {
                                        oIAvanceReal.Vencido = true;
                                    }
                                }
                                else
                                {
                                    //No tiene fecha estimada de FIN, tomo la fecha de hoy
                                    if (oPlanProyActividad.Fecha_Est_Fin <= DateTime.Now)
                                    {
                                        oIAvanceReal.Vencido = true;
                                    }
                                }
                            }
                            oIAvanceReal.Estado = Math.Round(oIAvanceReal.percentComplete * 100, 2).ToString() + "%";
                        }

                        oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_Programada;
                        oIAvanceEstimado.Finalizado = oIAvanceReal.Finalizado;
                        oIAvanceEstimado.Vencido = oIAvanceReal.Vencido;
                        if (oIAvanceEstimado.Finalizado)
                            oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_Finalizada;
                        else if (oIAvanceEstimado.Vencido)
                            oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_Vencida;
                        else if (oPlanProyActividad.Fecha_Est_Incio < DateTime.Now &&
                            oPlanProyActividad.Fecha_Est_Fin > DateTime.Now)
                        {
                            if (oPlanProyActividad.Avance == 0)
                                oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_Atrasada;
                            else
                                oIAvanceEstimado.Estado = ValoresUtiles.Estado_PPA_EnCurso;
                        }

                        #region FILTRAMOS SEGÚN LOS ESTADOS SELECCIONADOS COMO FILTROS
                        if (oFiltro.lEstados.Count == 0)
                        {
                            lItemAvances.Add(oIAvanceEstimado);
                            if (!oFiltro.SoloPlanificadas) lItemAvances.Add(oIAvanceReal);
                        }
                        else if (oFiltro.lEstados.Contains("Solo Activas"))
                        {
                            if (oIAvanceEstimado.Estado != ValoresUtiles.Estado_PPA_Finalizada)
                            {
                                lItemAvances.Add(oIAvanceEstimado);
                                if (!oFiltro.SoloPlanificadas) lItemAvances.Add(oIAvanceReal);
                            }
                        }
                        else if (oFiltro.lEstados.Contains(oIAvanceEstimado.Estado))
                        {
                            lItemAvances.Add(oIAvanceEstimado);
                            if (!oFiltro.SoloPlanificadas) lItemAvances.Add(oIAvanceReal);
                        }
                        #endregion
                    }
                }

                oResGantt.lAvances = lItemAvances;
                #endregion

                #region Listas adicionales
                List<int> lIdsPPAs = oResGantt.lAvances
                        .Where(ia => ia.type == "Estimado")
                        .Select(ia => ia.id)
                        .ToList();
                List<PlanProyAct_Dependencia> lPPA_Dependencias = new PlanProyAct_DependenciaNegocio()
                    .Get_By_lIdsPPA_Hijas(lIdsPPAs, true);
                oResGantt.lPPA_Dependencias = lPPA_Dependencias.Select(ppad => new
                {
                    ppad.Id,
                    ppad.PredecessorId,
                    ppad.SuccessorId,
                    ppad.Type
                }).ToList();
                oResGantt.lAvances.ForEach(a =>
                {
                    List<PlanProyAct_Dependencia> lPPADependenciasPadres = lPPA_Dependencias
                        .Where(ppad => ppad.PPAHijaId == a.id)
                        .ToList();
                    if (lPPADependenciasPadres.Count == 0)
                        a.DetalleActPadres = "Sin actividades padres.";
                    else
                    {
                        lPPADependenciasPadres.ForEach(ppad =>
                        {
                            a.DetalleActPadres += "- " + ppad.PPAPadre.Ubicacion + " - " + ppad.PPAPadre.Actividad + "<br/>";
                        });
                    }
                    List<PlanProyAct_Dependencia> lPPADependenciasHijas = lPPA_Dependencias
                        .Where(ppad => ppad.PPAPadreId == a.id)
                        .ToList();
                    if (lPPADependenciasHijas.Count == 0)
                        a.DetalleActHijas = "Sin actividades hijas.";
                    else
                    {
                        lPPADependenciasHijas.ForEach(ppad =>
                        {
                            a.DetalleActHijas += "- " + ppad.PPAHija.Ubicacion + " - " + ppad.PPAHija.Actividad + "<br/>";
                        });
                    }
                });
                oResGantt.lActividades = lItemAvances
                    .GroupBy(ia => ia.IdPlanActividad)
                    .Select(g => g.First())
                    .Select(ia => new { id = ia.IdPlanActividad, nombre = ia.actividades_nombre })
                    .OrderBy(ia => ia.nombre)
                    .ToList();
                oResGantt.lUbicaciones = lItemAvances
                    .GroupBy(ia => ia.IdProyUbicacion)
                    .Select(g => g.First())
                    .Select(ia => new { id = ia.IdProyUbicacion, nombre = ia.ubicacion })
                    .ToList();
                //List<string> lEstados = new List<string>() { "Todas", "Solo Activas" };
                //lEstados.AddRange(ValoresUtiles.Get_lEstados_PPA());
                //oResGantt.lEstados = lEstados;
                List<string> lBusqueda = new List<string>() { "Por Defecto"};
                lBusqueda.AddRange(ValoresUtiles.Get_lBusqueda_Filtros());
                oResGantt.lBusqueda = lBusqueda;
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error: PlanificacionController: getAvances(Filtro_PlanProyAct): exception.", ex);
            }
            return oResGantt;
        }
        [HttpPost]
        public object GetAvances(Filtro_PlanProyAct oFiltro)
        {
            var oResult = new object();
            try
            {
                Res_Gantt oResGantt = this.getAvances(oFiltro);
                oResult = new
                {
                    data = oResGantt
                };

            }
            catch (Exception ex)
            {
                oResult = new
                {
                    error = true,
                    message = "Error: PlanificacionController: GetAvances(int): exception.",
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        [HttpPost]
        public object GetAvances_Rubro(Filtro_PlanProyAct oFiltro)
        {
            var oResult = new object();
            try
            {
                Res_Gantt oResGantt = this.getAvancesRubro(oFiltro);
                oResult = new
                {
                    data = oResGantt
                };

            }
            catch (Exception ex)
            {
                oResult = new
                {
                    error = true,
                    message = "Error: PlanificacionController: GetAvances(int): exception.",
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #endregion

        #region Copiar una Planificacion a otra Ubicacion

        [HttpPost]
        public ReturnData Planificacion_CopiarActividad_EntreUbicaciones(int pIdUbicacionOrigen, int pIdUbicacionFInal)
        {
            ReturnData d = new ReturnData();
            try
            {
                //1 - Recorro todas las actividades planificadas para la ubicacion origen
                // este dato sale de la tabla planificacion_proyecto_actividades

                List<Planificacion_Proyecto_Actividades> lstActividades = new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades_SinIncludes().Where(p => p.Proyecto_UbicacionesId == pIdUbicacionOrigen).OrderBy(p => p.Planificacion_ActividadesId).ToList();

                foreach (var oAct in lstActividades)
                {
                    //2-Por cada actividad genero una nueva y la grabo con la nueva ubicacion, verifico antes si ya existe, no hago nada
                    //2.1- Verifico si ya existe esa actividad para esta ubicacion

                    Planificacion_Proyecto_Actividades mExiste = new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades_SinIncludes().Where(p => p.Proyecto_UbicacionesId == pIdUbicacionFInal && p.Planificacion_ActividadesId == oAct.Planificacion_ActividadesId).FirstOrDefault();

                    if (mExiste == null)
                    {
                        //2.2 - Como no existe creo el nuevo objeto y lo grabo
                        Planificacion_Proyecto_Actividades oNewAct = new Planificacion_Proyecto_Actividades();


                        oNewAct.Cantidad = oAct.Cantidad;
                        oNewAct.Detalle = oAct.Detalle;
                        oNewAct.Planificacion_ActividadesId = oAct.Planificacion_ActividadesId;
                        oNewAct.Planificacion_Actividades_RubroId = oAct.Planificacion_Actividades_RubroId;
                        oNewAct.Proyecto_UbicacionesId = pIdUbicacionFInal;
                        oNewAct.UnidadMedida = oAct.UnidadMedida;
                        oNewAct.UsuariosId = oAct.UsuariosId;
                        oNewAct.Id = 0;
                        oNewAct.Avance = 0;
                        oNewAct.ContratistasId = 1;
                        oNewAct.Fecha_Creacion = DateTime.Now;
                        oNewAct.Fecha_Est_Fin = DateTime.MinValue;
                        oNewAct.Fecha_Est_Incio = DateTime.MinValue;
                        oNewAct.Fecha_Real_Fin = DateTime.MinValue;
                        oNewAct.Fecha_Real_Incio = DateTime.MinValue;
                        oNewAct.Fecha_Ultima_Modificacion = DateTime.Now;
                        oNewAct.Finalizados = false;
                        oNewAct.Id = 0;
                        oNewAct.Incidencia = 0;
                        oNewAct.EsAdicional = false;

                        //2.3 - Grabo el registro
                        try
                        {
                            var mGrabo = new Planificacion_Proyecto_ActividadesNegocio().Insert(oNewAct);
                        }
                        catch (Exception)
                        {
                            d.isError = true;
                            d.data = "Se produjo un error al copiar la Actividad" + oAct.Planificacion_ActividadesId;
                        }
                    }
                }
            }
            catch (Exception)
            {
                d.isError = true;
                d.data = "Se produjo un error al copiar";
            }

            d.isError = false;
            d.data = "La copia de las Actividades se realizo con exito";

            return d;
        }

        public ActionResult CopiaActividadesUbicacion(int id)
        {
            ItemCopiaActividadUbicacion data = new ItemCopiaActividadUbicacion();

            data.Proyecto_Ubicaciones = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(id);

            return PartialView(data);
        }

        [HttpGet]
        public ReturnData GetUbicaciones(int id)
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(id);

            return d;
        }

        #endregion

        #region Actividad Calidad

        public ActionResult Planificacion_Actividades_Calidad_Items(int id)
        {
            Planificacion_Actividades_Calidad_ItemsItem data = new Planificacion_Actividades_Calidad_ItemsItem();

            return PartialView(data);
        }

        [HttpPost]
        public ReturnData CalidadGraba(Planificacion_Actividades_Calidad_ItemsItem data)
        {
            ReturnData d = new ReturnData();

            if (data != null)
            {

                EF.Models.Planificacion_Actividades_Calidad_Items paci = new EF.Models.Planificacion_Actividades_Calidad_Items();
                paci.Id = data.Id;
                paci.IdPLanificacionActividades = data.IdPLanificacionActividades;
                paci.Realiza_Accion = data.Realiza_Accion;
                paci.Se_Efectua = data.Se_Efectua;
                paci.Perioricidad = data.Perioricidad;
                paci.Tolerancia = data.Tolerancia;
                paci.Como_Controlar = data.Como_Controlar;
                paci.Controla = data.Controla;
                paci.Con_Elemento = data.Con_Elemento;
                paci.Con_Plano = data.Con_Plano;
                paci.Documentacion_Obra = data.Documentacion_Obra;
                paci.Observaciones = data.Observaciones;
                paci.Donde = data.Donde;
                paci.Fecha_Ult_Modificacion = DateTime.Now;
                paci.Inactivo = data.Inactivo;
                paci.IdUsuarioMOdifico = getItemLoguinUsuarioActual().Id;
                paci.sUsuarioMOdifico = getItemLoguinUsuarioActual().ApellidoNombre;
                int idc;
                try
                {

                    if (data.Id > 0)
                    {
                        idc = new Planificacion_Actividades_Calidad_ItemsNegocio().Update(paci);
                    }
                    else
                    {
                        idc = new Planificacion_Actividades_Calidad_ItemsNegocio().Insert(paci);
                    }
                }
                catch (Exception e)
                {

                    throw;
                }

                #region devuelvo datos

                List<Planificacion_Actividades_Calidad_Items> ListaRet = new List<Planificacion_Actividades_Calidad_Items>();
                List<Planificacion_Actividades_Calidad_Items> placig = new Planificacion_Actividades_Calidad_ItemsNegocio().Get_All_Planificacion_Actividades_Calidad_ItemsId(data.IdPLanificacionActividades);
                foreach (var e in placig)
                {
                    Planificacion_Actividades_Calidad_Items pacic = new Planificacion_Actividades_Calidad_Items();
                    pacic = e;


                    ListaRet.Add(pacic);
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

        [HttpGet]
        public ReturnData CalidadGet(int Id)
        {

            ReturnData d = new ReturnData();
            List<Planificacion_Actividades_Calidad_Items> ListaReturn = new List<Planificacion_Actividades_Calidad_Items>();
            List<Planificacion_Actividades_Calidad_Items> placig = new Planificacion_Actividades_Calidad_ItemsNegocio().Get_All_Planificacion_Actividades_Calidad_ItemsId(Id);
            foreach (var e in placig)
            {
                Planificacion_Actividades_Calidad_Items cav = new Planificacion_Actividades_Calidad_Items();
                cav = e;

                ListaReturn.Add(cav);
            }

            d.data = ListaReturn;
            d.isError = false;
            return d;
        }

        #endregion

        #region PRIVADOS

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

        #region COCINADAS

        private void CocinarDatos()
        {
            try
            {
                bool noExistenRelaciones = new PlanProyAct_DependenciaNegocio().Get_All().Count() == 0;
                if (noExistenRelaciones)
                {
                    List<Planificacion_Proyecto_Actividades> lPPAs = new Planificacion_Proyecto_ActividadesNegocio()
                       .Get_All()
                       .Where(ppa => ppa.IdPadre > 0)
                       .ToList();
                    PlanProyAct_DependenciaNegocio oPPADep_Negocio = new PlanProyAct_DependenciaNegocio();

                    lPPAs.ForEach(ppa =>
                    {
                        PlanProyAct_Dependencia oPPADependencia = new PlanProyAct_Dependencia();
                        oPPADependencia.PPAPadreId = ppa.IdPadre;
                        oPPADependencia.PPAHijaId = ppa.Id;
                        oPPADependencia.FecCreacion = DateTime.Now;
                        oPPADependencia.IdUsuarioCreo = 1;
                        oPPADep_Negocio.Insert(oPPADependencia);
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        #endregion

        #region CopiarPlanificacion
        public  ActionResult CopiaActividadesPlanificacion(int IdProyecto, int IdRegistro)
        {
            ReturnData mReturn = new ReturnData();
            mReturn.data = IdProyecto;
            ItemActividadPlanificacionCopiaActividad item = new ItemActividadPlanificacionCopiaActividad();
            item.IdPlanificacionProyectoActividad = IdRegistro;

            Planificacion_Proyecto_Actividades ppa = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(IdRegistro);
            Planificacion_Actividades pa = new Planificacion_ActividadesNegocio().Get_One_Planificacion_Actividades(ppa.Planificacion_ActividadesId);

            item.vis_NombreActividad = pa.Nombre;
            item.vis_NombreUbicacion = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones(ppa.Proyecto_UbicacionesId).Nombre;
            item.vis_FechaInicio= Convert.ToString(((DateTime)ppa.Fecha_Est_Incio).ToString("0:dd/MM/yyyy")).Remove(0, 2);
            item.vis_FechaHasta = Convert.ToString(((DateTime)ppa.Fecha_Est_Fin).ToString("0:dd/MM/yyyy")).Remove(0, 2);
            item.vis_Detalle = ppa.Detalle;

            item.IdProyecto = IdProyecto;
            //estas actividades esta de mas.
            item.actividadesSeleccionada = ppa.Planificacion_ActividadesId;

            item.Proyecto_Ubicaciones = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(IdProyecto);
            //Seteamos estos valores temporalmente para cargarlos en el popap
            item.pp=null;
           
            return PartialView(item);
        }
      
        public  ReturnData cargarComboActividadPlanificacion(int IdProyecto)
        {
            ReturnData data = new ReturnData();

            List<int> PPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_IdProyecto(IdProyecto).Select(s=>s.Planificacion_ActividadesId).ToList();

            List<ItemActividadNombre> ListaActividades = new List<ItemActividadNombre>();

           
            foreach (var i in PPA)
            {
                ItemActividadNombre val = new ItemActividadNombre();
                Planificacion_Actividades item = new Planificacion_ActividadesNegocio().Get_One_Planificacion_Actividades(i);
                val.Id = item.Id;
                val.Nombre = item.Nombre + " - " + item.Descripción;
                ListaActividades.Add(val);
            }
            ListaActividades = ListaActividades.OrderBy(s => s.Nombre).ToList();

            data.data = ListaActividades;
            return data;
        }

        [HttpPost]
        public ReturnData CopiarPlanificacionActividad(ItemActividadPlanificacionCopiar data)
        {
            ReturnData oReturnData = new ReturnData();
            Usuarios oUsuarioLogueado = this.getUsuarioActual();
            Planificacion_Proyecto_Actividades _PPA = new Planificacion_Proyecto_Actividades();
            Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(data.IdPlanificacionProyectoActividad);

            //Recorremos el array de ubicaciones y vamos insertando
            try
            {

                if (Convert.ToDateTime(data.Fecha_Est_Incio)> Convert.ToDateTime(data.Fecha_Est_Fin))
                {
                    throw new Exception("La fecha de Inicio no puede ser mayor a la fecha de Fin.");
                }

                foreach (var item in data.Proyecto_UbicacionesId)
                {
                    _PPA = oPPA;
                    _PPA.Id = 0;
                    _PPA.Fecha_Creacion = DateTime.Now;
                    _PPA.Fecha_Real_Fin = Convert.ToDateTime(data.Fecha_Est_Fin);
                    _PPA.Fecha_Est_Incio = Convert.ToDateTime(data.Fecha_Est_Incio);
                    _PPA.Fecha_Est_Fin = Convert.ToDateTime(data.Fecha_Est_Fin);
                    
                    _PPA.Planificacion_ActividadesId = data.Planificacion_ActividadesId;
                    _PPA.Planificacion_Actividades_RubroId = new Planificacion_Proyecto_ActividadesNegocio().GetRubroByActividad_Id(data.Planificacion_ActividadesId).Planificacion_Actividades_RubroId;
                    _PPA.Proyecto_UbicacionesId = item;

                    new Planificacion_Proyecto_ActividadesNegocio().Insert(_PPA);
                }

                oReturnData.isError = false;
                oReturnData.data = "Se dieron de alta las actividades para las ubicaciones seleccionadas";
            }
            catch (Exception e)
            {
                oReturnData.isError = true;
                if (e.Message.ToString() != null) {
                    oReturnData.data = e.Message.ToString();
                }
                else
                {
                    oReturnData.data = "Problema ar crear la actividad";
                }
               
            }
            

            return oReturnData;
        }

        #endregion
    }
}