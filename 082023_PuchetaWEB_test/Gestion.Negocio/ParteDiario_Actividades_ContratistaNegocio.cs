using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class ParteDiario_Actividades_ContratistaNegocio
    {
        #region ABM's

        public int Insert(ParteDiario_Actividades_Contratista data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch(Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public int Update(ParteDiario_Actividades_Contratista data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public int Delete(ParteDiario_Actividades_Contratista data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ParteDiario_Actividades_Contratista data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ParteDiario_Actividades_Contratista.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception error)
            {
                var err = error.ToString();
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        public List<ParteDiario_Actividades_Contratista> Get_All_ParteDiario_Actividades_Contratistas()
        {
            List<ParteDiario_Actividades_Contratista> Lista = new List<ParteDiario_Actividades_Contratista>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Actividades_Contratista
                    .Include(x => x.ParteDiario_Actividades)
                    .Include(x => x.Contratistas)
                    .OrderByDescending(x=> x.Fecha)
                    .ToList();
            }

            return Lista;
        }

        public ParteDiario_Actividades_Contratista Get_One_ParteDiario_Contratista(int Id)
        {
            ParteDiario_Actividades_Contratista PDA = new ParteDiario_Actividades_Contratista();
            using (var db = new iotdbContext())
            {
                PDA = db.ParteDiario_Actividades_Contratista
                    .FirstOrDefault(s => s.Id == Id);
            }

            return PDA;
        }

        public List<ParteDiario_Actividades_Contratista> Get_All_ParteDiario_Actividades_Contratistas_Solo()
        {
            List<ParteDiario_Actividades_Contratista> Lista = new List<ParteDiario_Actividades_Contratista>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Actividades_Contratista
                    //.Include(x => x.ParteDiario_Actividades)
                    //.Include(x => x.Contratistas)
                    .OrderByDescending(x => x.Fecha)
                    .ToList();
            }

            return Lista;
        }

        public ParteDiario_Actividades_Contratista Get_x_Id(int Id)
        {
            return this.Get_x_Id(Id, false);
        }

        public List<ParteDiario_Actividades_Contratista> Get_x_lIDs(List<int> lIDs)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ParteDiario_Actividades_Contratista
                        .Where(pdac => lIDs.Contains(pdac.Id) == true)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ParteDiario_Actividades_ContratistaNegocio: Get_x_lIDs(List<int>): exception.", ex);
            }
        }

        public ParteDiario_Actividades_Contratista Get_x_Id(int Id, bool conIncludes)
        {
            ParteDiario_Actividades_Contratista accion = new ParteDiario_Actividades_Contratista();
            using (var db = new iotdbContext())
            {
                if (conIncludes)
                    accion = db.ParteDiario_Actividades_Contratista
                        .Include(pdac => pdac.ParteDiario_Actividades)
                        .Include(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_Actividades)
                        .Include(pdac => pdac.ParteDiario_Actividades.ParteDiario)
                        .FirstOrDefault(m => m.Id == Id);
                else
                    accion = db.ParteDiario_Actividades_Contratista
                        .FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public ParteDiario_Actividades_Contratista get_one_Contratista_Actividad( 
            int IdContratista, int IdParteActividad)
        {
            ParteDiario_Actividades_Contratista pac = new ParteDiario_Actividades_Contratista();
            ParteDiario_Actividades_Contratista ac = new ParteDiario_Actividades_Contratista();
            using (var db = new iotdbContext())
            {
                ac = db.ParteDiario_Actividades_Contratista.FirstOrDefault(m => m.ContratistasId == IdContratista && m.ParteDiario_ActividadesId == IdParteActividad);
            }

            return ac;
        }

        /// <summary>
        /// Se usa este procedmiento para traer el detalle de las Actividades Contrastistas de un actividad con el id de Planificacion_Proyecto_ActividadesId
        /// </summary>
        /// <param name="Planificacion_Proyecto_ActividadesId"></param>
        /// <returns></returns>
        public List<Actividades_Contratista> Get_All_ParteDiario_Actividades_Contratistas_PorActividad(
            long mPPA_Id, DateTime mFechaParteDiario)
        {
            List<Actividades_Contratista> Lista = new List<Actividades_Contratista>();
            using (var db = new iotdbContext())
            {
                var conn = db.Database.GetDbConnection();
                conn.Open();
                var command = conn.CreateCommand();

                var query = "SELECT b.id as id, a.Id as ParteDiario_ActividadesId, a.Planificacion_Proyecto_ActividadesId as Planificacion_Proyecto_ActividadesId, b.contratistasid as ContratistasId, c.Nombre as sContratistas, b.cantidad as Cantidad, b.fecha as Fecha, 0 as PermitoBorar, 0 as isArchivoContratistas, b.TipoRegistro as TipoRegistro ";
                var mFrom = " FROM partediario_actividades_contratista as b left join archivos_adjuntos_relacion as d on(d.identidad = b.id and d.entidad = 'PD') , partediario_actividades as a , contratistas as c ";
                var mWhere = " WHERE a.Planificacion_Proyecto_ActividadesId = " + mPPA_Id + " AND a.id = b.ParteDiario_ActividadesId AND c.id = b.contratistasid";
                var mOrder = " ORDER BY b.fecha desc, b.contratistasid desc";
                command.CommandText = query + mFrom + mWhere + mOrder;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Actividades_Contratista o = new Actividades_Contratista();

                    o.id = reader.GetInt32(0);
                    o.ParteDiario_ActividadesId = reader.GetInt32(1);
                    o.Planificacion_Proyecto_ActividadesId = reader.GetInt32(2);
                    o.ContratistasId = reader.GetInt32(3);
                    o.sContratistas = reader.GetString(4);
                    o.Cantidad = reader.GetFloat(5);
                    o.Fecha = reader.GetDateTime(6);
                    o.isArchivoContratistas = reader.GetBoolean(8);
                    o.TipoRegistro = reader.GetString(9);


                    if (o.Fecha == mFechaParteDiario)
                        o.PermitoBorar = true;
                    else
                        o.PermitoBorar = false;
                    Lista.Add(o);

                }
            }

            return Lista;
        }

        /// <summary>
        /// Se usa este procedimiento para llegar las lista de datos que se muestra en el resumen del parte diario de cada proyecto
        /// </summary>
        /// <param name="Planificacion_Proyecto_ActividadesId"></param>
        /// <returns></returns>
        public List<ItemResumen_Actividades_Contratista> Get_All_ParteDiario_Actividades_Contratistas_Resumen(
            long mIdParteDiario, long mIdParteDiarioAyer)
        {
            List<ItemResumen_Actividades_Contratista> lstActividadesContratista = new List<ItemResumen_Actividades_Contratista>();

            using (var db = new iotdbContext())
            {
                var conn = db.Database.GetDbConnection();
                conn.Open();
                var command = conn.CreateCommand();

                //var query = "SELECT e.Nombre as Nombre_Ubicacion, g.Nombre as Nombre_Rubro, f.Nombre as Nombre_Actividad, IFNULL(d.detalle, '') as Comentario, c.id as ContratistasId, c.Nombre as sContratistas, b.cantidad, d.Avance as Avance, a.Planificacion_Proyecto_ActividadesId as Planificacion_Proyecto_ActividadesId, h.Fecha_Creacion as Fecha_ParteDiario, d.Fecha_Est_Fin as  Fecha_Estimada_Fin, d.Fecha_Est_Incio as Fecha_Estimada_Inicio, NPD.Finalizado, NP.NroNP as NroNotaPedido, b.AvanceActual as AvanceDelDia, NPD.Avance_Actual as AvanceNotaPedidoDetalle, NPD.Cantidad as NPD_CantidadAsignada ";
                //var mFrom = " FROM partediario_actividades as a,  partediario_actividades_contratista as b, contratistas as c, planificacion_proyecto_actividades as d, proyecto_ubicaciones as e, planificacion_actividades as f, planificacion_actividades_rubro as g, partediario as h, NotaPedido as NP, notapedido_detalle as NPD ";
                //var mWhere = " WHERE a.ParteDiarioId = "+ mIdParteDiario + " AND a.id = b.ParteDiario_ActividadesId AND c.id = b.contratistasid AND d.id = a.Planificacion_Proyecto_ActividadesId AND e.id = d.Proyecto_UbicacionesId AND f.id = d.Planificacion_ActividadesId AND g.id = d.Planificacion_Actividades_RubroId AND h.id = a.ParteDiarioId AND NP.id = b.NotaPedidoId AND NPD.Id = b.NotaPedidoDetalleId";
                var query = "SELECT e.Nombre as Nombre_Ubicacion, g.Nombre as Nombre_Rubro, f.Nombre as Nombre_Actividad, IFNULL(d.detalle, '') as Comentario, c.id as ContratistasId, c.Nombre as sContratistas, b.cantidad, d.Avance as Avance, a.Planificacion_Proyecto_ActividadesId as Planificacion_Proyecto_ActividadesId, h.Fecha_Creacion as Fecha_ParteDiario, d.Fecha_Est_Fin as  Fecha_Estimada_Fin, d.Fecha_Est_Incio as Fecha_Estimada_Inicio, b.Finalizado, b.AvanceActual as AvanceDelDia, b.NotapedidoId as NotaPedidoId, b.TrabajoHoy ";
                var mFrom = " FROM partediario_actividades as a,  partediario_actividades_contratista as b, contratistas as c, planificacion_proyecto_actividades as d, proyecto_ubicaciones as e, planificacion_actividades as f, planificacion_actividades_rubro as g, partediario as h ";
                var mWhere = " WHERE a.ParteDiarioId = " + mIdParteDiario + " AND a.id = b.ParteDiario_ActividadesId AND c.id = b.contratistasid AND d.id = a.Planificacion_Proyecto_ActividadesId AND e.id = d.Proyecto_UbicacionesId AND f.id = d.Planificacion_ActividadesId AND g.id = d.Planificacion_Actividades_RubroId AND h.id = a.ParteDiarioId ";
                var mOrder = " ORDER BY e.Nombre, g.Nombre, f.Nombre, a.Planificacion_Proyecto_ActividadesId,c.Nombre ";
                command.CommandText = query + mFrom + mWhere + mOrder;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ItemResumen_Actividades_Contratista oPDRA = new ItemResumen_Actividades_Contratista();

                    oPDRA.sUbicacion = reader.GetString(0);
                    oPDRA.sRubro = reader.GetString(1);
                    oPDRA.sActividad = reader.GetString(2);
                    oPDRA.sComentario = reader.GetString(3);
                    oPDRA.ContratistasId = reader.GetInt32(4);
                    oPDRA.sContratistas = reader.GetString(5);
                    //oPDRA.Cantidad = reader.GetFloat(6).ToString();
                    oPDRA.Cantidad = reader.GetDecimal(13).ToString(); //Va la Cantidad que se agrego en este parte diario.
                    decimal mAvanceNP = reader.GetDecimal(13);
                    float mAvance = reader.GetFloat(7);
                    oPDRA.Avance = Math.Round(reader.GetFloat(7), 2).ToString() + "%";
                    oPDRA.Planificacion_Proyecto_ActividadesId = reader.GetInt32(8);
                    oPDRA.f_Avance = mAvance;
                    oPDRA.ColorFondo = "PONER";
                    oPDRA.CantidadAyer = "0";
                    if (reader.GetInt32(15) == 1)
                    {
                        oPDRA.TrabajoHoy = "SI";
                    }
                    else
                    {
                        oPDRA.TrabajoHoy = "NO";
                    }
                   

                    #region Busco los Datos de la Nota de Pedido Detalle y Nota de Pedido
                    int mNotaPedidoId = reader.GetInt32(14);
                    NotaPedido_Detalle oNPD = new NotaPedido_Detalle();
                    NotaPedido oNP = new NotaPedido();
                    if (mNotaPedidoId != 0)
                    {
                        //NPD.Avance_Actual as AvanceNotaPedidoDetalle, oNPD.Cantidad as NPD_CantidadAsignada
                        oNPD = new NotaPedido_DetalleNegocio().Get_One_Orden(mNotaPedidoId);
                        if(oNPD != null)
                        {
                            oPDRA._CantidadAcumulada = oNPD.Avance_Actual.ToString();
                            oPDRA._CantidadAsignadaNP = oNPD.Cantidad.ToString();
                            oNP = new NotaPedidoNegocio().Get_One_Orden(Convert.ToInt32(oNPD.IdNotaPedido));
                            oPDRA.NotaPedido = oNP.NroNP + "-" + oNP.Comentario;
                        }
                        else 
                        {
                            oPDRA._CantidadAcumulada = "0";
                            oPDRA._CantidadAsignadaNP = "0";
                            oPDRA.NotaPedido = "S/D";

                        }   
                        
                    }
                    else
                    {
                        oPDRA._CantidadAcumulada = "0";
                        oPDRA._CantidadAsignadaNP = "0";
                        oPDRA.NotaPedido = "S/D";
                    }
                    #endregion

                    #region  ARCHIVOS ADJUNTOS
                    //var mIdPDAC = reader.GetInt32(13);  //id de la tablapartediario_actividades_contratista
                    //oPDRA.IsArchivo = false;
                    //List<Archivos_Adjuntos_Relacion> Laar = new Archivos_Adjuntos_RelacionNegocio().Get_Archivos_Adjuntos_Relacion("PD", mIdPDAC);
                    //if (Laar.Count > 0)
                    //{
                    //    oPDRA.IsArchivo = true;
                    //}
                    //else
                    //{
                    //    oPDRA.IsArchivo = false;
                    //}
                    #endregion

                    #region  COLOR DE FONDO
                    //1- Verifico si la fecha estimada de fin es anterior a la fecha del partediario

                    DateTime mFechaFin = reader.GetDateTime(10);
                    DateTime mFechaPA = reader.GetDateTime(9);
                    DateTime mFechaEstInicio = reader.GetDateTime(11);
                    Boolean mFinalizado = reader.GetBoolean(12);

                    //oPDRA.FechaEstInicio = mFechaEstInicio.ToShortDateString();
                    //oPDRA.FechaEstFin = mFechaFin.ToShortDateString();
                    oPDRA.FechaEstInicio = mFechaEstInicio.ToShortDateString();
                    oPDRA.FechaEstFin = mFechaFin.ToShortDateString();
                    oPDRA.FechaRealInicio = "";


                    if (mFinalizado)
                    {
                        oPDRA.ColorFondo = "V";
                    }
                    else
                    {
                        if (mFechaFin < mFechaPA)
                        {
                            oPDRA.ColorFondo = "R";
                        }
                        else
                        {
                            //if (mFechaEstInicio < mFechaPA && mAvance == 0)
                            if (mFechaEstInicio < mFechaPA && mAvanceNP == 0)
                            {
                                oPDRA.ColorFondo = "A";
                            }
                        }
                    }
                    #endregion

                    

                    #region Busco el Avance que se cargo en el parte diario anterior
                    //if (oPDRA.f_Avance > 0)
                    //{
                    //    using (var db2 = new iotdbContext())
                    //    {
                    //        var conn2 = db2.Database.GetDbConnection();
                    //        conn2.Open();
                    //        var command2 = conn2.CreateCommand();

                    //        var query2 = "SELECT sum(cantidad) as Cantidad FROM partediario_actividades as a , partediario_actividades_contratista as b ";
                    //        var query2Where = "where a.ParteDiarioId = " + mIdParteDiarioAyer + " AND a.Planificacion_Proyecto_ActividadesId = " + oPDRA.Planificacion_Proyecto_ActividadesId + " and b.ParteDiario_ActividadesId = a.id;";
                    //        command2.CommandText = query2 + query2Where;
                    //        var reader2 = command2.ExecuteReader();

                    //        while (reader2.Read())
                    //        {
                    //            try
                    //            {
                    //                oPDRA.CantidadAyer = reader2.GetFloat(0).ToString();
                    //            }
                    //            catch
                    //            {
                    //                oPDRA.CantidadAyer = "0";
                    //            }
                    //        }
                    //    }
                    //    //ANULADO POR QUE ANDA MUY LENTO
                    //    //ParteDiario_Actividades oPDA_A = new ParteDiario_ActividadesNegocio().Get_All_ParteDiario_Actividades_SinInclude().Where(p => p.ParteDiarioId == mIdParteDiarioAyer && p.Planificacion_Proyecto_ActividadesId == oPDRA.Planificacion_Proyecto_ActividadesId).FirstOrDefault();

                    //    //if (oPDA_A != null)
                    //    //{
                    //    //    //Busco en el parte diario Actividades Contratistas, para esa actividad y ese contratista

                    //    //    ParteDiario_Actividades_Contratista oPDAC_A = new ParteDiario_Actividades_ContratistaNegocio().Get_All_ParteDiario_Actividades_Contratistas_Solo().Where(p => p.ContratistasId == oPDRA.ContratistasId && p.ParteDiario_ActividadesId == oPDA_A.Id).FirstOrDefault();
                    //    //    if (oPDAC_A != null)
                    //    //    {
                    //    //        oPDRA.CantidadAyer = oPDAC_A.Cantidad.ToString(); ;
                    //    //    }
                    //    //}
                    //}
                    #endregion

                    lstActividadesContratista.Add(oPDRA);
                }
            }

            return lstActividadesContratista;

        }

        public List<ParteDiario_Actividades_Contratista> Get_PDActContratistas_x_IdPPA(int IdPPA)
        {
            try
            {
                List<ParteDiario_Actividades_Contratista> lPDACs = 
                    new List<ParteDiario_Actividades_Contratista>();
                List<ParteDiario_Actividades> lPDA = new ParteDiario_ActividadesNegocio()
                    .Get_X_IdPlanProyActividad(IdPPA);
                List<int> lIdsPDAs = lPDA.Select(pda => pda.Id).ToList();
                lPDACs = this.Get_X_lIdsPrtDiaActividades(lIdsPDAs);
                return lPDACs;
            }
            catch(Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<ParteDiario_Actividades_Contratista> Get_X_lIdsPrtDiaActividades(List<int> lIdsPDAs)
        {
            try
            {
                List<ParteDiario_Actividades_Contratista> lPrtDiaActContratistas = new List<ParteDiario_Actividades_Contratista>();
                using (var db = new iotdbContext())
                {
                    lPrtDiaActContratistas = db.ParteDiario_Actividades_Contratista
                        .Where(pdac => lIdsPDAs.Contains(pdac.ParteDiario_ActividadesId))
                        .ToList();
                }
                return lPrtDiaActContratistas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<ParteDiario_Actividades_Contratista> Get_x_ParteDiarioActividadId(int parteDiarioActividadId)
        {
            try
            {
                List<ParteDiario_Actividades_Contratista> lPrtDiaActContratistas = new List<ParteDiario_Actividades_Contratista>();
                using (var db = new iotdbContext())
                {
                    lPrtDiaActContratistas = db.ParteDiario_Actividades_Contratista
                        .Where(pdac => pdac.ParteDiario_ActividadesId == parteDiarioActividadId)
                        .ToList();
                }
                return lPrtDiaActContratistas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public DateTime Get_Fecha_Ult_PD_con_AvanceCargado(long mPPA_Id)
        {
            try
            {
                DateTime mFechaUltPd = DateTime.Now;

                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    var query = "SELECT DATE_FORMAT(b.fecha , '%Y/%m/%d') ";
                    var mFrom = " FROM partediario_actividades as a, partediario_actividades_contratista as b ";
                    var mWhere = " where a.Planificacion_Proyecto_ActividadesId = " + mPPA_Id + " and b.ParteDiario_ActividadesId = a.id and b.cantidad > 0 ";
                    var mOrder = " order by b.fecha desc;";
                    command.CommandText = query + mFrom + mWhere + mOrder;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(reader.GetString(0)))
                        {
                            return mFechaUltPd = Convert.ToDateTime(reader.GetString(0));
                        }
                        else
                        {
                            return mFechaUltPd = DateTime.Now;
                        }
                    }
                }

                return mFechaUltPd;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<ParteDiario_Actividades_Contratista> Get_x_ProyectoId_FecDesde_FecHasta(
            int ProyectoId, DateTime FecDesde, DateTime FecHasta, bool conIncludes)
        {
            try
            {
                FecDesde = new DateTime(FecDesde.Year, FecDesde.Month, FecDesde.Day);
                FecHasta = new DateTime(FecHasta.Year, FecHasta.Month, FecHasta.Day);
                FecHasta = FecHasta.AddDays(1).AddTicks(-1);
                List<ParteDiario_Actividades_Contratista> lPDACs = new List<ParteDiario_Actividades_Contratista>();
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        lPDACs = db.ParteDiario_Actividades_Contratista
                            .Include(pdac => pdac.ParteDiario_Actividades)
                            .Include(pdac => pdac.ParteDiario_Actividades.ParteDiario)
                            .Include(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_Actividades)
                            .Include(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_Actividades.Planificacion_Actividades)
                            .Include(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_Actividades.Planificacion_Actividades.PartidaPresupuestaria)
                            .Include(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_Actividades.Proyecto_Ubicaciones)
                            .Include(pdac => pdac.Contratistas)
                            .Where(pdac => FecDesde <= pdac.Fecha && pdac.Fecha <= FecHasta &&
                                (ProyectoId == 0 || pdac.ParteDiario_Actividades.ParteDiario.ProyectoId == ProyectoId))
                            .OrderByDescending(pdac => pdac.Fecha)
                            .ToList();
                    }
                    else
                    {
                        lPDACs = db.ParteDiario_Actividades_Contratista
                            .Where(pdac => FecDesde <= pdac.Fecha && pdac.Fecha <= FecHasta &&
                                (ProyectoId == 0 || pdac.ParteDiario_Actividades.ParteDiario.ProyectoId == ProyectoId))
                            .OrderByDescending(pdac => pdac.Fecha)
                            .ToList();
                    }
                }

                return lPDACs;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ParteDiario_ActividadNegocio: Get_x_ContratistaId_ProyectoId_FechaHasta(int, DateTime, DateTime).", ex);
            }
        }

        public List<ParteDiario_Actividades_Contratista> Get_x_ContratistaId_ProyectoId_FechaHasta(
            int ContratistaId, int ProyectoId, DateTime FecDesde, DateTime FecHasta)
        {
            List<ParteDiario_Actividades_Contratista> lPDACs = new List<ParteDiario_Actividades_Contratista>();
            FecDesde = new DateTime(FecDesde.Year, FecDesde.Month, FecDesde.Day);
            FecDesde = FecDesde.AddTicks(-1);
            FecHasta = new DateTime(FecHasta.Year, FecHasta.Month, FecHasta.Day);
            FecHasta = FecHasta.AddDays(1);
            try
            {
                using (var db = new iotdbContext())
                {
                    lPDACs = db.ParteDiario_Actividades_Contratista
                        .Include(pdac => pdac.ParteDiario_Actividades)
                        .Include(pdac => pdac.ParteDiario_Actividades.ParteDiario)
                        .Include(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_Actividades)
                        .Include(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_Actividades.Planificacion_Actividades)
                        .Include(pdac => pdac.ParteDiario_Actividades.Planificacion_Proyecto_Actividades.Proyecto_Ubicaciones)
                        .Include(pdac => pdac.Contratistas)
                        .Where(pdac => FecDesde < pdac.Fecha && pdac.Fecha < FecHasta &&
                            pdac.ContratistasId == ContratistaId &&
                            pdac.ParteDiario_Actividades.ParteDiario.ProyectoId == ProyectoId)
                        .OrderByDescending(pdac => pdac.Fecha)
                        .ToList();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error: exception.", ex);
            }
            return lPDACs;
        }

        /// <summary>
        /// Esta funcion devuelve para una nota de Pedido, todos los parte diarios distintos al actual (IdPD)
        /// </summary>
        /// <param name="IdNP"></param>
        /// <param name="IdPD"></param>
        /// <returns></returns>
        
        public List<ParteDiario_Actividades_Contratista> ControlTrabajoHoy_PD(int IdNP, int IdPD)
        {
            List<ParteDiario_Actividades_Contratista> oPDA = new List<ParteDiario_Actividades_Contratista>();
            using (var db = new iotdbContext())
            {
                oPDA = db.ParteDiario_Actividades_Contratista         
                     .Where(x => x.NotaPedidoId == IdNP && x.ParteDiarioId != IdPD && x.TrabajoHoy == true)
                    .ToList();
            }
            return oPDA;
        }

        #endregion

        #region Suma Avance acumulado de una actividad por contratista
        public decimal SumaAvanceActual_PDAC(int pPPA, int pIdContratista, int pIdPD, int pNotaPedidoId)
        {
            decimal mReturn = 0;

            using (var db = new iotdbContext())
            {
                var conn = db.Database.GetDbConnection();
                conn.Open();
                var command = conn.CreateCommand();
                var mWhere = "";
                var query = "SELECT COALESCE(sum(avanceActual),0) ";
                var mFrom = " FROM partediario_actividades_contratista as a, partediario_actividades as b ";
                mWhere = " WHERE b.id = a.ParteDiario_ActividadesId AND b.Planificacion_Proyecto_ActividadesId =" + pPPA + " ";
                if (pIdContratista != 0)
                {
                    //Si viene en 0 no tiene en cuenta alcontratista
                    mWhere = mWhere + " AND a.ContratistasId = " + pIdContratista + " AND a.NotaPedidoId =  " + pNotaPedidoId + " ";
                }
                if (pIdPD != 0)
                {
                    //No tiene en cuenta el ultimo Parte diario
                    mWhere = mWhere + " AND a.ParteDiarioId <> " + pIdPD + " ";
                }
                
                command.CommandText = query + mFrom + mWhere;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    return mReturn = reader.GetDecimal(0);
                }
            }

            return mReturn;
        }
        #endregion
    }
}
