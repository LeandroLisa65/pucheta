using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
     public class ParteDiario_ActividadesNegocio
    {
        #region ABM's

        public int Insert(ParteDiario_Actividades data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(ParteDiario_Actividades data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ParteDiario_Actividades data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ParteDiario_Actividades data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    //db.ParteDiario_Actividades.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception err)
            {
                var e = err.ToString();
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        public List<ParteDiario_Actividades> Get_All_ParteDiario_Actividades()
        {
            List<ParteDiario_Actividades> Lista = new List<ParteDiario_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Actividades.Include(x=> x.Planificacion_Proyecto_Actividades)
                    .Include(x=> x.Planificacion_Proyecto_Actividades)
                    .Include(x => x.Planificacion_Proyecto_Actividades.Planificacion_Actividades_Rubro)
                    .Include(x => x.Planificacion_Proyecto_Actividades.Proyecto_Ubicaciones)
                    .Include(x => x.Planificacion_Proyecto_Actividades.Planificacion_Actividades)
                    .Include(x => x.ParteDiario.Proyecto)
                    .Include(x=> x.ParteDiario).Include(x => x.Usuarios).ToList();
            }

            return Lista;
        }

        public List<ParteDiario_Actividades> Get_All_ParteDiario_Actividades_Lite()
        {
            List<ParteDiario_Actividades> Lista = new List<ParteDiario_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Actividades.ToList();
            }

            return Lista;
        }

        public bool Existe_ParteDiarioActividad(long pParteDiarioId, long pPlanificacion_Proyecto_ActividadesId)
        {
            using (var db = new iotdbContext())
            {
                if (db.ParteDiario_Actividades.Where(pda => pda.ParteDiarioId == pParteDiarioId && pda.Planificacion_Proyecto_ActividadesId == pPlanificacion_Proyecto_ActividadesId).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<ParteDiario_Actividades> Get_x_ParteDiarioId(int pParteDiarioId)
        {
            try
            {
                List<ParteDiario_Actividades> lPDAs = new List<ParteDiario_Actividades>();
                using (var db = new iotdbContext())
                {
                    lPDAs = db.ParteDiario_Actividades.Where(pda => pda.ParteDiarioId == pParteDiarioId)
                        .ToList();
                }
                return lPDAs;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ParteDiario_ActividadNegocio: Get_x_ParteDiarioId: exception.", ex);
            }
        }

        public List<ParteDiario_Actividades> Get_All_ParteDiario_Actividades_SinInclude()
        {
            List<ParteDiario_Actividades> Lista = new List<ParteDiario_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Actividades.ToList();
            }

            return Lista;
        }

        public List<ParteDiario_Actividades> Get_X_IdPlanProyActividad(int IdPlanProyActividad)
        {
            List<ParteDiario_Actividades> Lista = new List<ParteDiario_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Actividades
                    .Include(pda => pda.ParteDiario)
                    .Where(x => x.Planificacion_Proyecto_ActividadesId == IdPlanProyActividad)
                    .ToList();
            }
            return Lista;
        }

        public ParteDiario_Actividades Get_One_ParteDiario_Actividades(int Id)
        {
            ParteDiario_Actividades accion = new ParteDiario_Actividades();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario_Actividades.Include(x => x.Planificacion_Proyecto_Actividades)
                    .Include(x => x.Planificacion_Proyecto_Actividades.Planificacion_Actividades_Rubro)
                    .Include(x => x.Planificacion_Proyecto_Actividades.Proyecto_Ubicaciones)
                    .Include(x => x.Planificacion_Proyecto_Actividades.Planificacion_Actividades)
                    .Include(x => x.ParteDiario).Include(x => x.Usuarios).FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public ParteDiario_Actividades Get_One_ParteDiario_Actividades_ValidacionParteDiario(int Id)
        {
            ParteDiario_Actividades accion = new ParteDiario_Actividades();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario_Actividades
                    //.Include(x => x.Planificacion_Proyecto_Actividades)
                    //.Include(x => x.Planificacion_Proyecto_Actividades.Planificacion_Actividades_Rubro)
                    //.Include(x => x.Planificacion_Proyecto_Actividades.Proyecto_Ubicaciones)
                    //.Include(x => x.Planificacion_Proyecto_Actividades.Planificacion_Actividades)
                    .Include(x => x.Usuarios).FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public List<ParteDiario_Actividades> Get_x_lIDs(List<int> lIDs)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ParteDiario_Actividades
                        .Where(pdac => lIDs.Contains(pdac.Id) == true)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ParteDiario_ActividadesNegocio: Get_x_lIDs(List<int>): exception.", ex);
            }
        }

        public List<ParteDiario_Actividades> Get_X_lIdsPlanProyActividades(List<int> lIdsPDAs)
        {
            try
            {
                List<ParteDiario_Actividades> lPartesDiariosActividades = new List<ParteDiario_Actividades>();
                using (var db = new iotdbContext())
                {
                    lPartesDiariosActividades = db.ParteDiario_Actividades
                        .Where(pda => lIdsPDAs.Contains(pda.Planificacion_Proyecto_ActividadesId))
                        .ToList();
                }
                return lPartesDiariosActividades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public ParteDiario_Actividades Get_X_IdPlanProyActvidad_PrimeroConAvance(int IdPlanProyActividad)
        {
            ParteDiario_Actividades oPDA = new ParteDiario_Actividades();
            using (var db = new iotdbContext())
            {
                oPDA = db.ParteDiario_Actividades
                    .Include(x => x.ParteDiario)
                    .Where(x => x.Planificacion_Proyecto_ActividadesId == IdPlanProyActividad && x.Avance > 0)
                    .OrderBy(pda => pda.ParteDiario.Fecha_Creacion)
                    .ThenBy(pda => pda.Id)
                    .FirstOrDefault();
            }
            return oPDA;
        }

        public ParteDiario_Actividades Get_X_IdPlanProyActvidad_UltimoConAvance(int IdPlanProyActividad)
        {
            ParteDiario_Actividades oPDA = new ParteDiario_Actividades();
            using (var db = new iotdbContext())
            {
                oPDA = db.ParteDiario_Actividades
                    .Include(x => x.ParteDiario)
                    .Where(x => x.Planificacion_Proyecto_ActividadesId == IdPlanProyActividad && x.Avance > 0)
                    .OrderByDescending(pda => pda.ParteDiario.Fecha_Creacion)
                    .ThenByDescending(pda => pda.Id)
                    .FirstOrDefault();
            }
            return oPDA;
        }    

        #endregion

        #region Cargar Actividades de un Parte Diario - V2.0
        public List<ItemParteDiario_Actividades2> BuscarParteDiario_Actividades(int pIdParteDiario)
        {
            List<ItemParteDiario_Actividades2> lstPDActividades = new List<ItemParteDiario_Actividades2>();
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
                    var query = "SELECT a.ParteDiarioId as PDA_ParteDiarioId, a.Planificacion_ActividadesId as PDA_Planificacion_ActividadesId, a.Planificacion_Proyecto_ActividadesId as PDA_Planificacion_Proyecto_ActividadesId, a.Avance as PDA_Avance, a.Finalizados as PDA_Finalizados "; //Parte Diario Actividades
                    query = query + " , b.ContratistasId as PDAC_ContratistaId, b.TrabajoHoy as PDAC_Trabajo, b.Finalizado as PDAC_Finalizado, b.Cantidad as PDAC_Cantidad "; //Parte Diario Actividad Contratista
                    query = query + " , d.Nombre as Contratista_Nombre "; //Contratista
                    query = query + " , ppa.Fecha_Est_Fin as PPA_FechaEstFin, ppa.Fecha_Est_Incio as PPA_FechaEstInicio, ppa.Fecha_Real_Incio as PPA_FechaRealInicio, COALESCE(ppa.Detalle,'') as PPA_Comentario"; //Planificacion PRoyecto Actividad
                    query = query + " , pu.Nombre as Ubicacion_Nombre "; //Ubicacion
                    query = query + " , par.Nombre as Rubro_Nombre "; //Rubro
                    query = query + ", pa.Nombre"; //Nombre de Actividad
                    query = query + ", np.NroNP"; // Numero de Nota de pedido
                    query = query + ", c.Fecha_Creacion as PD_FechaCreacion "; //Fecha Creacion Parte Diario
                    query = query + ", b.id as PDAC_Id ";
                    query = query + ", npd.Avance_Actual as NPD_Avance, npd.Cantidad as NPD_CantidadAdjudicada, npd.UnidadMedida as NPD_UnidadMedidaAdjudicada, npd.id as NPD_Id";
                    query = query + ", b.AvanceActual as ParteDiario_Actividades_AvanceActual, b.TrabajoAyer as TrabajoAyer";
                    var mFrom = " FROM partediario_actividades as a, partediario_actividades_contratista as b, partediario as c, contratistas as d , planificacion_proyecto_actividades as ppa, planificacion_actividades_rubro as par, proyecto_ubicaciones as pu, planificacion_actividades pa, notapedido as np, notapedido_detalle as npd ";
                    var mWhere = " WHERE a.ParteDiarioId = " + pIdParteDiario + " ";
                    mWhere = mWhere + " AND c.id = a.ParteDiarioId";
                    mWhere = mWhere + " AND a.id = b.ParteDiario_ActividadesId "; //Relacion con Actividad_Contratista
                    mWhere = mWhere + " AND b.ContratistasId = d.id "; //Tabla ContratistasId
                    mWhere = mWhere + " AND a.Planificacion_Proyecto_ActividadesId = ppa.Id ";
                    mWhere = mWhere + " AND par.Id = ppa.Planificacion_Actividades_RubroId "; //Rubro
                    mWhere = mWhere + " AND ppa.Proyecto_UbicacionesId = pu.Id "; //Ubicaciones
                    mWhere = mWhere + " AND pa.Id = a.Planificacion_ActividadesId"; //Sacar nombre de la Actividad ";
                    mWhere = mWhere + " AND np.Id = b.notapedidoId ";
                    mWhere = mWhere + " AND npd.Id = b.notapedidodetalleid ";
                    var mOrder = " ORDER BY pu.Nombre, par.Nombre, ppa.Fecha_Est_Incio, a.Planificacion_Proyecto_ActividadesId, d.Nombre";
                    command.CommandText = query + mFrom + mWhere + mOrder;
                    var reader = command.ExecuteReader();
                    #endregion

                    while (reader.Read())
                    {
                        ItemParteDiario_Actividades2 oObj = new ItemParteDiario_Actividades2();

                        
                        oObj.ParteDiarioId = reader.GetInt32(1);
                        oObj.Proyecto_ActividadesId = reader.GetInt32(1);
                        oObj.Planificacion_Proyecto_ActividadesId = reader.GetInt32(2);
                        oObj.ContratistasId = reader.GetInt32(5);
                        oObj.Trabajo = reader.GetBoolean(6);
                        oObj.FinalizadaActividadNP = reader.GetBoolean(7);
                        oObj.Cantidad = reader.GetFloat(8);
                        
                        oObj.sContratistas = reader.GetString(9);
                        oObj.FechaFin = reader.GetDateTime(10).ToString();
                        oObj.FechaInicio = reader.GetDateTime(11).ToString();
                        oObj.FechaRealInicio = reader.GetDateTime(12).ToString("dd-MM-yyyy");
                        oObj.sComentario = reader.GetString(13);
                        oObj.sUbicacion = reader.GetString(14);
                        oObj.sRubro = reader.GetString(15);
                        oObj.sActividad = reader.GetString(16);
                        
                        oObj.NotaPedidoNro = reader.GetString(17);

                        oObj.FechaCreacion = reader.GetDateTime(18);
                        oObj.Id = reader.GetInt32(19);
                        oObj.AvanceNP = reader.GetDouble(20);
                        oObj.AdjudicadoNP = reader.GetDouble(21);
                        oObj.AdjudicadoMedidaNP = reader.GetString(22);
                        oObj.NotaPedidoDetalleId = reader.GetInt32(23);
                        oObj.AvanceActual = reader.GetDecimal(24);
                        oObj.TrabajoAyer = reader.GetBoolean(25);
                        oObj._AvanceActual = oObj.AvanceActual.ToString("#0.00").Replace(",",".");

                        #region 1-Color del fondo de este registro para la grilla segun el estado
                        oObj.ColorFondo = "B";
                        DateTime mFechaFin = oObj.FecEstFin;
                        DateTime mFechaPA = oObj.FechaCreacion;
                        DateTime mFechaEstInicio = oObj.FecEstInicio;
                        //1- Verifico si la fecha estimada de fin es anterior a la fecha del partediario
                        if (oObj.FinalizadaActividadNP)
                        {
                            oObj.ColorFondo = "V";
                        }
                        else
                        {
                            if (mFechaFin < mFechaPA)
                            {
                                oObj.ColorFondo = "R";
                            }
                            else
                            {
                                if (mFechaEstInicio < mFechaPA && oObj.AvanceNP == 0)
                                {
                                    oObj.ColorFondo = "A";
                                }
                            }
                        }
                        #endregion

                        lstPDActividades.Add(oObj);
                    }
                }
            }
            catch (Exception ex)
            {
                lstPDActividades = null;
            }
            return lstPDActividades;
        }
        #endregion

    }
}
