using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class ParteDiarioNegocio
    {

        #region ABM's

        public int Insert(ParteDiario data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(ParteDiario data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ParteDiario data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ParteDiario data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ParteDiario.Attach(data);
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

        public List<ParteDiario> Get_All_ParteDiario()
        {
            List<ParteDiario> Lista = new List<ParteDiario>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario
                    .Include(x => x.Proyecto)
                    .Include(x => x.ParteDiario_Actividades)  
                    .ToList();
            }

            return Lista;
        }

        public List<ParteDiario> Get_PartesDiarios_X_ProyectoId(int ProyectoId)
        {
            List<ParteDiario> Lista = new List<ParteDiario>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario
                    .Where(pd => pd.ProyectoId == ProyectoId)
                    .ToList();
            }
            return Lista;
        }

        public List<ParteDiario> Get_X_lIdsProyectos(List<int> lIdsProyectos)
        {
            try
            {
                List<ParteDiario> lPartesDiarios = new List<ParteDiario>();
                using (var db = new iotdbContext())
                {
                    lPartesDiarios = db.ParteDiario
                        .Where(pd => lIdsProyectos.Contains(pd.ProyectoId))
                        .ToList();
                }
                return lPartesDiarios;
            }
            catch(Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public ParteDiario Get_One_ParteDiario(int Id)
        {
            ParteDiario accion = new ParteDiario();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario
                    .Include(p => p.ParteDiario_Actividades)
                    .Include(p => p.ParteDiario_Contratista)
                    //.Include(p => p.ParteDiario_Sol_ObrasAdic)
                    .Include(p=> p.Proyecto)
                    .FirstOrDefault(m => m.Id == Id)
                    ;
            }

            return accion;
        }

        public ParteDiario Get_One_ParteDiario_Solo_Actividades(int Id)
        {
            ParteDiario accion = new ParteDiario();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario
                    .Include(p => p.ParteDiario_Actividades)
                    //.Include(p => p.ParteDiario_Contratista)
                    //.Include(p => p.ParteDiario_Sol_ObrasAdic)
                    //.Include(p => p.Proyecto)
                    .FirstOrDefault(m => m.Id == Id)
                    ;
            }

            return accion;
        }

        public ParteDiario Get_One_ParteDiario_SinInclude(int Id)
        {
            ParteDiario accion = new ParteDiario();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        /// <summary>
        /// Método que devuelve una lista de ParteDiario cuya fecha de registro es mayor o igual al parámentro pFecDesde
        /// </summary>
        /// <param name="pFecDesde">Fecha desde la cual se quiere traer los registros</param>
        /// <returns>List de ParteDiario</returns>
        public List<ParteDiario> Get_x_ProyectoId_FechaDesde(int pProyectoId, DateTime pFecDesde)
        {
            try
            {
                List<ParteDiario> lPartesDiarios = new List<ParteDiario>();
                using (var db = new iotdbContext())
                {
                    lPartesDiarios = db.ParteDiario
                        .Where(pd => pd.ProyectoId == pProyectoId && pd.Fecha_Creacion >= pFecDesde)
                        .ToList();
                }
                return lPartesDiarios;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ParteDiarioNegocio: Get_x_ProyectoId_FechaDesde(int, DateTime): exception.", ex);
            }
        }
        public List<ParteDiario> Get_x_ProyectoId(int pProyectoId)
        {
            try
            {
                List<ParteDiario> lPartesDiarios = new List<ParteDiario>();
                using (var db = new iotdbContext())
                {
                    lPartesDiarios = db.ParteDiario
                        .Where(pd => pd.ProyectoId == pProyectoId)
                        .ToList();
                }
                return lPartesDiarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ParteDiarioNegocio: Get_x_ProyectoId(int): exception.", ex);
            }
        }

        #endregion

        #region OTROS


        public void InformarProyectosSinParteDiario()
        {
            try
            {
                List<Proyecto> lProyectos = new ProyectoNegocio().Get_All_Proyecto();
                lProyectos = lProyectos.Where(p => p.Estado != ValoresUtiles.Estado_Proy_Ejecutado)
                    .ToList();
                lProyectos.ForEach(p =>
                {
                    int cantidadDias = -2;
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                        cantidadDias = -3;
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                        cantidadDias = -4;
                    DateTime fecDesde = DateTime.Now.AddDays(cantidadDias);
                    List<ParteDiario> lPartesDiarios = new ParteDiarioNegocio()
                        .Get_x_ProyectoId_FechaDesde(p.Id, fecDesde);
                    if (lPartesDiarios.Count == 0)
                    {
                        Usuarios oUsuarioJefeObra = new UsuariosNegocio().Get_Usuario(p.UsuariosId);
                        string asunto = "Aviso sobre Proyecto " + p.Nombre;
                        string mensaje = "No se ha registrado Parte Diario por mas de 48 horas.";
                        string emails = oUsuarioJefeObra.Email.Trim();
                        int RolId = 0;
                        if (p.Tipo == ValoresUtiles.EmailSeguimientoObra)
                            RolId = ValoresUtiles.IdRol_CoordinacionObra;
                        else RolId = ValoresUtiles.IdRol_CoordinacionEdificios;
                        if (RolId > 0)
                        {
                            List<Usuarios> lUsuarios = new RolesUsuariosNegocio().Get_Usuarios_x_RolId(RolId);
                            lUsuarios.ForEach(u => emails += ";" + u.Email.Trim());

                            EnviaEmailNegocio.EnviarEmail_Async(oUsuarioJefeObra.Email, asunto, mensaje, false)
                                .ConfigureAwait(false);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: Incidente_HistorialNegocio: GenerarIncidentesSegunProyectosSinPartesDiarios(): exception.", ex);
            }
        }

        #endregion

        #region Creadion de Actividades y de Actividades_Contratistas de un Parte Diario
        public ReturnData CreacionActividades_ActividadesContratistas(int pIdParteDiario)
        {
            ReturnData mReturn = new ReturnData();
            mReturn.isError = false;
            try
            {
                using (var db = new iotdbContext())
                {
                    #region 1-Busco el Parte Diario
                    ParteDiario oPD = db.ParteDiario.Where(p => p.Id == pIdParteDiario).FirstOrDefault();
                    #endregion

                    #region 2-Busca las Actividades a Insertar
                    List<NotaPedido_Detalle> lstNPD = new List<NotaPedido_Detalle>();
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    var query = "SELECT a.idppa as IDPPA, a.idpa as IDPA, c.Avance as Avance, b.IdContratista as IdContratista, b.Id as NotaPedidoId, a.Id as NotaPedidoDetalleId";
                    var mFrom = " FROM notapedido_detalle as a, notapedido as b, planificacion_proyecto_actividades as c ";
                    var mWhere = " WHERE a.IdNotaPedido = b.Id AND a.Finalizado = 0 AND b.Estado <> 'Finalizado' AND b.IdProyecto = " + oPD.ProyectoId + " AND c.id = a.IdPPA AND c.Finalizados = false AND date(b.fecha_creacion) <= date('" + oPD.Fecha_Creacion.ToString("yyyy-MM-dd") + "')";
                    var mOrder = " ORDER BY c.Fecha_Est_Incio, a.IdPPA, b.IdContratista";
                    command.CommandText = query + mFrom + mWhere + mOrder;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NotaPedido_Detalle oNPD = new NotaPedido_Detalle();
                        oNPD.IdPPA = reader.GetInt32(0);
                        oNPD.IdPA = reader.GetInt32(1);
                        oNPD.Avance_Actual = reader.GetDouble(2);
                        oNPD.sContratista = reader.GetInt32(3).ToString();
                        oNPD.Id = reader.GetInt32(5);
                        oNPD.IdNotaPedido = reader.GetInt32(4);
                        lstNPD.Add(oNPD);

                        #region 3-Inserto en la tabla ParteDiario_Actividades ParteDiarioActividades_Contratistas
                        ParteDiario_Actividades oPDA = new ParteDiario_Actividades();
                        oPDA.Avance = 0;
                        oPDA.ParteDiarioId = oPD.Id;
                        oPDA.Planificacion_ActividadesId = Convert.ToInt32(oNPD.IdPA);
                        oPDA.Planificacion_Proyecto_ActividadesId = Convert.ToInt32(oNPD.IdPPA);
                        oPDA.UsuariosId = 1;
                        #region 3.1-Verifico si ya existe antes
                        ParteDiario_Actividades pTempPDA = new ParteDiario_ActividadesNegocio().Get_All_ParteDiario_Actividades_SinInclude().Where(p => p.ParteDiarioId == oPDA.ParteDiarioId && p.Planificacion_ActividadesId == oPDA.Planificacion_ActividadesId && p.Planificacion_Proyecto_ActividadesId == oPDA.Planificacion_Proyecto_ActividadesId).FirstOrDefault();
                        #endregion
                        int mIdPDA = 0;
                        if (pTempPDA == null)
                        {
                            mIdPDA = new ParteDiario_ActividadesNegocio().Insert(oPDA);
                        }
                        else
                        {
                            mIdPDA = pTempPDA.Id;
                        }
                        ParteDiario_Actividades_Contratista oPDAC = new ParteDiario_Actividades_Contratista();
                        oPDAC.Avance = 0;
                        oPDAC.ParteDiarioId = oPD.Id;
                        oPDAC.ContratistasId = Convert.ToInt32(oNPD.sContratista);
                        oPDAC.ParteDiario_ActividadesId = mIdPDA;
                        oPDAC.Cantidad = 0;
                        oPDAC.AvanceActual = 0;
                        oPDAC.Fecha = oPD.Fecha_Creacion;
                        oPDAC.Finalizado = false;
                        oPDAC.TipoRegistro = "Manual";
                        oPDAC.NotaPedidoId = Convert.ToInt32(oNPD.IdNotaPedido);
                        oPDAC.NotaPedidoDetalleId = Convert.ToInt32(oNPD.Id);
                        oPDAC.TrabajoAyer = _ConsultarTrabajoAyer(oNPD.Id);
                        ParteDiario_Actividades_Contratista pTempPDAC = new ParteDiario_Actividades_ContratistaNegocio().Get_All_ParteDiario_Actividades_Contratistas_Solo().Where(p => p.ParteDiario_ActividadesId == oPDAC.ParteDiario_ActividadesId && p.Fecha == oPDAC.Fecha && p.ContratistasId == oPDAC.ContratistasId).FirstOrDefault();
                        if (pTempPDAC == null)
                        {
                            int mIdPDAC = new ParteDiario_Actividades_ContratistaNegocio().Insert(oPDAC);
                        }

                        #endregion
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                mReturn.isError = true;
                mReturn.data2 = ex.Message; 
                return mReturn;
            }
            mReturn.isError = false;
            return mReturn;
        }
        #endregion

        #region Creacion de Asistencia de un parte diario
        public ReturnData CreacionAsistencia(int pIdParteDiario)
        {
            ReturnData mReturn = new ReturnData();
            mReturn.isError = false;
            try
            {
                using (var db = new iotdbContext())
                {
                    #region 1-Busco el Parte Diario
                    ParteDiario oPD = db.ParteDiario.Where(p => p.Id == pIdParteDiario).FirstOrDefault();
                    #endregion

                    #region Busco en la tabla de ParteDiario contratista, lo que deberian trabajar ese dia
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    var query = "SELECT distinct(ContratistasId) ";
                    var mFrom = " FROM partediario_actividades_contratista ";
                    var mWhere = " WHERE PartediarioId = " + pIdParteDiario + " ";
                    var mOrder = " ORDER BY ContratistasId";
                    command.CommandText = query + mFrom + mWhere + mOrder;
                    var reader = command.ExecuteReader();
                    #endregion

                    while (reader.Read())
                    {
                        ParteDiario_Asistencia oObj = new ParteDiario_Asistencia();
                        oObj.ParteDiarioId = pIdParteDiario;
                        oObj.ContratistasId = reader.GetInt32(0);

                        //Verifico si existe ya ese registro
                        int mCount = new ParteDiario_AsistenciaNegocio().Get_All_ParteDiario_AsistenciaPA(pIdParteDiario).Where(p => p.ContratistasId == oObj.ContratistasId).Count();
                        if (mCount == 0)
                        {
                            oObj.Asig_Contratista = 0;
                            oObj.Asig_Contratista_Presentes = 0;
                            oObj.Asig_Propios = 0;
                            oObj.Asig_Propios_Presentes = 0;
                            oObj.Covid_Contratista = 0;
                            oObj.Covid_Propioa = 0;
                            mReturn.data = new ParteDiario_AsistenciaNegocio().Insert(oObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mReturn.isError = true;
                mReturn.data = ex.Message;
                return mReturn;
            }
            mReturn.isError = false;
            return mReturn;
        }
        #endregion

        private bool _ConsultarTrabajoAyer(long IdNotaPedidoDetalle)
        {
            bool encontro = false;
            DateTime today = DateTime.Today;
            DayOfWeek dayOfWeek = today.DayOfWeek;
            DateTime fechaBuscar= today;
            if (dayOfWeek == DayOfWeek.Monday)
            {
                fechaBuscar = DateTime.Today.AddHours(-48);
            }
            else
            {
                fechaBuscar = DateTime.Today.AddHours(-24);
            }
                ParteDiario_Actividades_Contratista pd = new ParteDiario_Actividades_ContratistaNegocio().Get_All_ParteDiario_Actividades_Contratistas()
                .Where(s => s.NotaPedidoId == IdNotaPedidoDetalle
                    && s.Fecha >= fechaBuscar
                    && s.TrabajoHoy==true
                ).FirstOrDefault();
            if (pd != null) { 
                encontro=true;
            
            }
            return encontro;
        }
    }
}
