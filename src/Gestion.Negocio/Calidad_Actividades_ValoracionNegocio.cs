using Gestion.EF;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySqlConnector;

namespace Gestion.Negocio
{
    public class Calidad_Actividades_ValoracionNegocio
    {
        #region ABM's

        public int Insert(Calidad_Actividades_Valoracion data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Calidad_Actividades_Valoracion data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Calidad_Actividades_Valoracion data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Calidad_Actividades_Valoracion data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Calidad_Actividades_Valoracion.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception e)
            {
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        public List<Calidad_Actividades_Valoracion> Get_All_Calidad_Actividades_Valoracion()
        {
            List<Calidad_Actividades_Valoracion> Lista = new List<Calidad_Actividades_Valoracion>();
            using (var db = new iotdbContext())
            {
                Lista = db.Calidad_Actividades_Valoracion.ToList();
            }

            return Lista;
        }


        public List<Calidad_Actividades_Valoracion> Get_All_Calidad_Actividades_ValoracionId(int IdActividad)
        {
            List<Calidad_Actividades_Valoracion> Lista = new List<Calidad_Actividades_Valoracion>();
            using (var db = new iotdbContext())
            {
                Lista = db.Calidad_Actividades_Valoracion.Where(x=> x.IdPlanificacion_Proyecto_ActividadId == IdActividad).ToList();
            }

            return Lista;
        }




        public Calidad_Actividades_Valoracion Get_One_Calidad_Actividades_Valoracion(int Id)
        {
            Calidad_Actividades_Valoracion accion = new Calidad_Actividades_Valoracion();
            using (var db = new iotdbContext())
            {
                accion = db.Calidad_Actividades_Valoracion.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion

        #region GEneracion de Listas de Itemas de Calidad para una Actividad en PArticular
        public List<Calidad_Actividades_Valoracion> Get_ItemCalidad_ParaActividad(int idPlanificacionProyectoActividad, string pCuando)
        {
            List<Calidad_Actividades_Valoracion> lstCalidadActividadesValoracion = new List<Calidad_Actividades_Valoracion>();

            bool mProcesa = true;

            //Recibi el IdPLanificacionProyectoActividad, con eso tengo que buscar el IdProyectoActividad
            Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(idPlanificacionProyectoActividad);
            int mIdPlanificacionActividad = oPPA.Planificacion_ActividadesId;


            if (oPPA.Avance == 0)
            {
                float mAcumulado = 0;
                //Verifico por la tabla de contratistas avances
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    var query = "SELECT sum(b.Cantidad) as Acumulado";
                    var mFrom = " FROM partediario_actividades as a, partediario_actividades_contratista as b ";
                    var mWhere = " where a.Planificacion_Proyecto_ActividadesId = " + oPPA.Id + " AND b.ParteDiario_ActividadesId = a.id;";
                    command.CommandText = query + mFrom + mWhere;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        try
                        {
                            mAcumulado = reader.GetFloat(0);
                        }
                        catch
                        {
                            mAcumulado = 0;
                        }
                    }
                }
                oPPA.Avance = mAcumulado;
            }

            //Con el Proyecto Actividad ahora voy a buscar los Item de Calidad que tiene esta Actividad -TABLA planificacion_actividades_calidad_items
            if (pCuando == "INICIAR" && oPPA.Avance > 0)
            {
                // si es INICIAR y tiene avance no verifico los items de calidad
                mProcesa = false;
            }
            else
            {
                
            }
            if (mProcesa)
            {
                List<Planificacion_Actividades_Calidad_Items> lstItemsCalidad = new Planificacion_Actividades_Calidad_ItemsNegocio().Get_All_Planificacion_Actividades_Calidad_ItemsIdAct(mIdPlanificacionActividad).Where(p => p.Se_Efectua.ToUpper().Contains(pCuando.ToUpper())).ToList();

                foreach (var item in lstItemsCalidad)
                {
                    Calidad_Actividades_Valoracion oObj = new Calidad_Actividades_Valoracion();

                    oObj.IdCalidad_Items = item.Id;
                    oObj.IdPlanificacion_Proyecto_ActividadId = idPlanificacionProyectoActividad;

                    string mDetalleTarea = item.Documentacion_Obra;

                    oObj.DetalleTareaCalidad = mDetalleTarea;

                    string mDetalleCompletaTarea = "REALIZA ACCION:" + item.Realiza_Accion;
                    mDetalleCompletaTarea = mDetalleCompletaTarea + " - TOLERANCIA:" + item.Tolerancia;
                    mDetalleCompletaTarea = mDetalleCompletaTarea + " - SE EFECTUA:" + item.Se_Efectua;
                    mDetalleCompletaTarea = mDetalleCompletaTarea + " - CONTROLAR:" + item.Controla;
                    mDetalleCompletaTarea = mDetalleCompletaTarea + " - COMO CONTROLAR:" + item.Como_Controlar;
                    mDetalleCompletaTarea = mDetalleCompletaTarea + " - CON ELEMENTO:" + item.Con_Elemento;
                    mDetalleCompletaTarea = mDetalleCompletaTarea + " - CON PLANO:" + item.Con_Plano;
                    if (item.Observaciones != null)
                        mDetalleCompletaTarea = mDetalleCompletaTarea + " - OBSERVACIONES:" + item.Observaciones;

                    oObj.DetalleCompletoTareaCalidad = mDetalleCompletaTarea;

                    //Verifico si ya existe un ITEM DE CALIDAD YA GENERADO
                    int mExiste  = new Calidad_Actividades_ValoracionNegocio().Get_All_Calidad_Actividades_Valoracion().Where(p => p.IdPlanificacion_Proyecto_ActividadId == item.IdPLanificacionActividades && p.IdCalidad_Items == item.Id).Count();

                    if (mExiste == 0)
                        lstCalidadActividadesValoracion.Add(oObj);

                }
            }
            return lstCalidadActividadesValoracion.ToList();
        }

        #endregion

        #region Procedimiento para Validar si se lleno correctamente los Item de Calidad de una Actividad antes de grabar
        public ReturnData ValidarItemCalidad(List<Calidad_Actividades_Valoracion> lstItemCalidad)
        {
            ReturnData d = new ReturnData();
            foreach (var item in lstItemCalidad)
            {
                //if (item.Avance != "0")
                //{

                //}
                //else
                //{
                    if (item.Valor == "0")
                    {
                        d.isError = true;
                        d.data = "<strong>ATENCION</strong> - Se debe completar la informacion de Calidad";
                        return d;
                    }
                    else
                    {
                        if (item.Valor == "N")
                        {
                            if (item.Descripcion == "" || item.Descripcion == null)
                            {
                                d.isError = true;
                                d.data = "<strong>ATENCION</strong> - Existe un Item de calidad <strong>NO APROBADO</strong> sin comentario relacionado";
                                return d;
                            }
                        }
                    }
                //}
            }
            return d;
        }
        #endregion

        #region Procedimeinto para GRABAR una lista de Items de Calidad y GEnerar el Incidente si corresponde
        public ReturnData GrabarItemCalidad_Incidentes(List<Calidad_Actividades_Valoracion> lstItemCalidad, int pIdUsuarioActual, int pParteDiarioId, int pPlanificacion_Proyecto_ActividadesId, int pProyecto)
        {
            ReturnData d = new ReturnData();
            try
            {
                foreach (var item in lstItemCalidad)
                {
                    Calidad_Actividades_Valoracion oObj = new Calidad_Actividades_Valoracion();

                    oObj.IdParteDiario = pParteDiarioId;
                    oObj.IdPlanificacion_Proyecto_ActividadId = pPlanificacion_Proyecto_ActividadesId;

                    oObj.Valor = item.Valor;
                    oObj.Descripcion = item.Descripcion;
                    oObj.IdCalidad_Items = item.IdCalidad_Items;
                    oObj.IdUsuario = pIdUsuarioActual;
                    oObj.IdIncidente = 0;
                    if (oObj.Valor == "N")
                    {
                        //Verifico si no PASO, por que tengo que generar un Incidente
                        Incidentes_Historial oI = new Incidentes_Historial();
                        oI.AreaId = 6; //Jefe de Obra
                        oI.ContratistasId = 0;
                        oI.Creacion_Descripcion = oObj.Descripcion;
                        oI.Creacion_Fecha = DateTime.Now;
                        oI.Creacion_UsuarioId = pIdUsuarioActual;
                        oI.EstadoId = 2;
                        oI.Fecha_Cierre = DateTime.Now.AddDays(4);
                        oI.ParteDiarioId = pParteDiarioId;
                        oI.ProyectoId = pProyecto; // Proyecto
                        oI.IncidenteId = 54; //Incidente de CALIDAD
                        //Grabo el Incidente
                        try
                        {
                            int mIdIncidente = new Incidentes_HistorialNegocio().Insert(oI);
                            oObj.IdIncidente = mIdIncidente;
                            d.isError = false;
                        }
                        catch (Exception e)
                        {
                            d.isError = true;
                            d.data = "Se produjo un error al generar el Incidente de CALIDAD - Error:" + e.Message;
                        }
                        
                    }
                    if (d.isError == false)
                    {
                        var mResp = new Calidad_Actividades_ValoracionNegocio().Insert(oObj);
                        d.isError = false;
                    }
                }
            }
            catch (Exception e)
            {
                d.isError = true;
                d.data = "Se produjo un error al grabar el Item de Calidad - Error:" + e.Message;
            }
            return d;
        }
        #endregion

        #region Armar lista con los Items de Calidad para armar una grilla
        public List<Calidad_Actividades_Valoracion> Get_ItemCalidadActividadValoracion_ParaGrilla_Distinto(long pIdParteDiario, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            List<Calidad_Actividades_Valoracion> oCAValoracion = new List<Calidad_Actividades_Valoracion>();

            string mFechaDesde = pFechaDesde.Year + "-" + pFechaDesde.Month + "-" + pFechaDesde.Day;
            string mfechaHasta = pFechaHasta.Year + "-" + pFechaHasta.Month + "-" + pFechaHasta.Day;

            #region Procedimiento Nuevo
            using (var db = new iotdbContext())
            {
                var conn = db.Database.GetDbConnection();


                MySqlConnection connection = new MySqlConnection(db.Database.GetDbConnection().ConnectionString);
                connection.Open();

                MySqlCommand command = new MySqlCommand("Get_ItemCalidadActividadValoracion", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Ent_FD", mFechaDesde);
                command.Parameters.AddWithValue("@Ent_FH", mfechaHasta);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        Calidad_Actividades_Valoracion oItem = new Calidad_Actividades_Valoracion();
                        oItem.Id = reader.GetInt32(0);
                        oItem.IdPlanificacion_Proyecto_ActividadId = reader.GetInt32(1); 
                        oItem.IdParteDiario = reader.GetInt32(2); 
                        oItem.IdCalidad_Items = reader.GetInt32(3);
                        oItem.Valor = reader.GetString(4);
                        oItem.sValor = reader.GetString(5);
                        oItem.Descripcion = reader.GetString(6);
                        oItem.IdIncidente = reader.GetInt32(7);
                        oItem.IdUsuario = reader.GetInt32(8);
                        oItem.sFecha = reader.GetDateTime(9).ToString("dd-MM-yyyy");
                        oItem.sProyecto = reader.GetString(10); 
                        oItem.sRubro = reader.GetString(11);
                        oItem.sActividad = reader.GetString(12);
                        oItem.sUbicacion = reader.GetString(13);
                        oItem.sUsuario = reader.GetString(14);
                        oItem.DetalleTareaCalidad = "";
                        oItem.DetalleCompletoTareaCalidad = "";
                        oItem.sCalidadNombre = reader.GetString(15);
                        oItem.sCalidadItemCompleto = reader.GetString(16);
                        
                        if (oItem.sValor == "No Paso")
                        {
                            Incidentes_Historial oIncidente = new Incidentes_HistorialNegocio()
                                .Get_One_Incidentes_Historial(oItem.IdIncidente);
                            if (oIncidente != null)
                            {
                                oItem.sIncidenteEstado = new Incidentes_HistorialNegocio()
                                    .IncidenteHistorial_EstadoNombre(oIncidente.EstadoId);
                            }
                            else
                            {
                                oItem.sIncidenteEstado = "S/D";
                            }
                        }
                        else
                        {
                            oItem.sIncidenteEstado = "No Corresponde";
                        }
                        oCAValoracion.Add(oItem);
                    }
                    catch
                    {
                        
                    }
                }
                 connection.Close();
            }

            #endregion


            return oCAValoracion;
        }

        public List<Calidad_Actividades_Valoracion> Get_ItemCalidadActividadValoracion_ParaGrilla(long pIdParteDiario, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            List<Calidad_Actividades_Valoracion> oCAValoracion = new List<Calidad_Actividades_Valoracion>();

            string mFechaDesde = pFechaDesde.Year + "-" + pFechaDesde.Month + "-" + pFechaDesde.Day;
            string mfechaHasta = pFechaHasta.Year + "-" + pFechaHasta.Month + "-" + pFechaHasta.Day;

            #region Procedimiento Nuevo
            using (var db = new iotdbContext())
            {
                var conn = db.Database.GetDbConnection();
                conn.Open();
                var command = conn.CreateCommand();

                var query = "";
                query = query + "SELECT a.id, a.IdPlanificacion_Proyecto_ActividadId, a.IdParteDiario, a.IdCalidad_Items";
                query = query + " ,a.Valor, CASE WHEN a.Valor = 'S' THEN 'Paso' ELSE 'No Paso' END as Valor_Detalle";
                query = query + " , a.Descripcion, a.IdIncidente, a.IdUsuario , b.Fecha_Creacion as Fecha ";
                query = query + " ,c.Nombre as Proyecto_Nombre  ,e.Nombre as Actividad_Nombre  ,f.Nombre as Rubro_Nombre ";
                query = query + " ,g.Nombre as Ubicacion_Nombre  , CONCAT(h.Nombre, ' ', h.Apellido) as Usuario_Nombre, i.Documentacion_Obra as Calidad_Nombre ";
                query = query + " ,CONCAT('Realiza Accion:', IFNULL(i.Realiza_Accion,' ') ,' Tolerancia:', IFNULL(i.Tolerancia,' ') ,' Se Efectua:', IFNULL(i.Se_Efectua,' ') ,' Controla:', IFNULL(i.Controla,' ') ,' Como Controlar:', IFNULL(i.Como_Controlar,' ') ,' Con Elemento:', IFNULL(i.Con_Elemento,'') ,' Con Plano:', IFNULL(i.Con_Plano,'') ,' Observaciones:', IFNULL(i.Observaciones,'') ) as Detalle_Tarea";
                var mFrom = "";
                mFrom = " FROM calidad_actividades_valoracion as a, partediario as b, proyecto as c, planificacion_proyecto_actividades as d, planificacion_actividades as e,";
                mFrom = mFrom + " planificacion_actividades_rubro as f, proyecto_ubicaciones as g, usuarios as h, planificacion_actividades_calidad_items as i";

                var mWhere = "";
                mWhere = mWhere + " WHERE a.IdParteDiario = b.Id ";
                mWhere = mWhere + " AND ( date(b.Fecha_Creacion) >= date('" + mFechaDesde + "') AND date(b.Fecha_Creacion) <= date('" + mfechaHasta + "') )";
                mWhere = mWhere + " AND b.ProyectoId = c.Id";
                mWhere = mWhere + " AND d.id = a.IdPlanificacion_Proyecto_ActividadId";
                mWhere = mWhere + " AND d.planificacion_actividadesid = e.id";
                mWhere = mWhere + " AND e.Planificacion_Actividades_RubroId = f.id";
                mWhere = mWhere + " AND d.Proyecto_UbicacionesId = g.id";
                mWhere = mWhere + " AND a.IdUsuario = h.Id";
                mWhere = mWhere + " AND a.IdCalidad_Items = i.id";

                command.CommandText = query + mFrom + mWhere;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        Calidad_Actividades_Valoracion oItem = new Calidad_Actividades_Valoracion();
                        oItem.Id = reader.GetInt32(0);
                        oItem.IdPlanificacion_Proyecto_ActividadId = reader.GetInt32(1);
                        oItem.IdParteDiario = reader.GetInt32(2);
                        oItem.IdCalidad_Items = reader.GetInt32(3);
                        oItem.Valor = reader.GetString(4);
                        oItem.sValor = reader.GetString(5);
                        oItem.Descripcion = reader.GetString(6);
                        oItem.IdIncidente = reader.GetInt32(7);
                        oItem.IdUsuario = reader.GetInt32(8);
                        oItem.sFecha = reader.GetDateTime(9).ToString("dd-MM-yyyy");
                        oItem.sProyecto = reader.GetString(10);
                        oItem.sRubro = reader.GetString(11);
                        oItem.sActividad = reader.GetString(12);
                        oItem.sUbicacion = reader.GetString(13);
                        oItem.sUsuario = reader.GetString(14);
                        oItem.DetalleTareaCalidad = "";
                        oItem.DetalleCompletoTareaCalidad = "";
                        oItem.sCalidadNombre = reader.GetString(15);
                        oItem.sCalidadItemCompleto = reader.GetString(16);

                        if (oItem.sValor == "No Paso")
                        {
                            Incidentes_Historial oIncidente = new Incidentes_HistorialNegocio()
                                .Get_One_Incidentes_Historial(oItem.IdIncidente);
                            if (oIncidente != null)
                            {
                                oItem.sIncidenteEstado = new Incidentes_HistorialNegocio()
                                    .IncidenteHistorial_EstadoNombre(oIncidente.EstadoId);
                            }
                            else
                            {
                                oItem.sIncidenteEstado = "S/D";
                            }
                        }
                        else
                        {
                            oItem.sIncidenteEstado = "No Corresponde";
                        }
                        oCAValoracion.Add(oItem);
                    }
                    catch
                    {

                    }
                }
            }

            #endregion

            #region Procedimiento Viejo
            //if (pIdParteDiario != 0)
            //    oCAValoracion = new Calidad_Actividades_ValoracionNegocio()
            //        .Get_All_Calidad_Actividades_Valoracion()
            //        .Where(p => p.IdParteDiario == pIdParteDiario)
            //        .ToList();
            //else
            //    oCAValoracion = new Calidad_Actividades_ValoracionNegocio()
            //        .Get_All_Calidad_Actividades_Valoracion().ToList();
            //        //.Get_All_Calidad_Actividades_Valoracion()
            //        //.Where(p => Convert.ToDateTime(p.sFecha) >= Convert.ToDateTime(pFechaDesde) && Convert.ToDateTime(p.sFecha) <= Convert.ToDateTime(pFechaHasta)).ToList();
            //foreach (var item in oCAValoracion)
            //{
            //    ParteDiario oParteDiario = new ParteDiarioNegocio()
            //        .Get_One_ParteDiario(item.IdParteDiario);
            //    // este if se tuvo que agregar ya que hay registros PD borrados 
            //    // y consecuentemente inconsistencia de datos con las registros relacionados.
            //    if (oParteDiario != null)
            //    {
            //        item.sFecha = oParteDiario.Fecha_Creacion.ToShortDateString();
            //        //1-Proyecto y Ubicacion
            //        item.sProyecto = new ProyectoNegocio()
            //            .Get_One_Proyecto(oParteDiario.ProyectoId).Nombre;

            //        //2-Nombre del Rubro y Actividad
            //        Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio()
            //            .Get_X_Id(item.IdPlanificacion_Proyecto_ActividadId);
            //        // este if se tuvo que agregar ya que hay registros PPA borrados 
            //        // y consecuentemente inconsistencia de datos con las registros relacionados.
            //        if (oPPA != null)
            //        {
            //            Planificacion_Actividades oActividad = new Planificacion_ActividadesNegocio()
            //                .Get_One_Planificacion_Actividades(oPPA.Planificacion_ActividadesId);
            //            item.sActividad = oActividad.Nombre;
            //            Planificacion_Actividades_Rubro oRubro = new Planificacion_Actividades_RubroNegocio()
            //                .Get_One_Planificacion_Actividades_Rubro(oPPA.Planificacion_Actividades_RubroId);
            //            item.sRubro = oRubro.Nombre;
            //            item.sUbicacion = new Proyecto_UbicacionesNegocio()
            //                .Get_One_Proyecto_Ubicaciones(oPPA.Proyecto_UbicacionesId).Nombre;
            //            //3-Usuario
            //            item.sUsuario = new UsuariosNegocio().Get_Usuario(item.IdUsuario).NombreYApellido;
            //            //4-Calidad Item
            //            Planificacion_Actividades_Calidad_Items oItemCalidad = new Planificacion_Actividades_Calidad_ItemsNegocio()
            //                .Get_One_Planificacion_Actividades_Calidad_Items(item.IdCalidad_Items);
            //            item.sCalidadNombre = oItemCalidad.Documentacion_Obra;
            //            string mDetalleCompletaTarea = "Realiza Accion:" + oItemCalidad.Realiza_Accion;
            //            mDetalleCompletaTarea = mDetalleCompletaTarea + " * Tolerancia:" + oItemCalidad.Tolerancia;
            //            mDetalleCompletaTarea = mDetalleCompletaTarea + " * Se Efectua:" + oItemCalidad.Se_Efectua;
            //            mDetalleCompletaTarea = mDetalleCompletaTarea + " * Controla:" + oItemCalidad.Controla;
            //            mDetalleCompletaTarea = mDetalleCompletaTarea + " * Como Controlar:" + oItemCalidad.Como_Controlar;
            //            mDetalleCompletaTarea = mDetalleCompletaTarea + " * Con Elemento:" + oItemCalidad.Con_Elemento;
            //            mDetalleCompletaTarea = mDetalleCompletaTarea + " * Con Plano:" + oItemCalidad.Con_Plano;
            //            if (oItemCalidad.Observaciones != null)
            //                mDetalleCompletaTarea = mDetalleCompletaTarea + " Observaciones:" + oItemCalidad.Observaciones;
            //            item.sCalidadItemCompleto = mDetalleCompletaTarea;
            //            //5-Datos de la declaraion de calidad
            //            if (item.Valor == "S")
            //                item.sValor = "Paso";
            //            else
            //                item.sValor = "No Paso";
            //            //Si no paso verifico que tenga IdIncidente y busco el estado del mismo
            //            if (item.sValor == "No Paso")
            //            {
            //                Incidentes_Historial oIncidente = new Incidentes_HistorialNegocio()
            //                    .Get_One_Incidentes_Historial(item.IdIncidente);
            //                if (oIncidente != null)
            //                {
            //                    item.sIncidenteEstado = new Incidentes_HistorialNegocio()
            //                        .IncidenteHistorial_EstadoNombre(oIncidente.EstadoId);
            //                }
            //                else
            //                {
            //                    item.sIncidenteEstado = "S/D";
            //                }
            //            }
            //            else
            //            {
            //                item.sIncidenteEstado = "No Corresponde";
            //            }
            //        }
            //    }
            //}
            #endregion

            return oCAValoracion;
        }
        #endregion
    }
}
