using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class Planificacion_Proyecto_ActividadesNegocio
    {
        #region ABM's

        public int Insert(Planificacion_Proyecto_Actividades data)
        {
            data.FecEstInicio_Original = data.Fecha_Est_Incio;
            data.FecEstFin_Original = data.Fecha_Est_Fin;
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Planificacion_Proyecto_Actividades data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Planificacion_Proyecto_Actividades data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Planificacion_Proyecto_Actividades data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Planificacion_Proyecto_Actividades.Attach(data);
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

        public partial class ResultActualizacionArbolPPA
        {
            /// <summary>
            /// Propiedad utilizada para resguardar el id de la PPA orignel del cambio recursivo aplicado a las hijas.
            /// </summary>
            public int IdPlanProyOrigenMovimiento { get; set; }
            public List<Planificacion_Proyecto_Actividades> lPlanProyActividades_Resultante { get; set; }
            public int IdPlanProyActividad { get; set; }
            public DateTime FecEstInicio_Nueva { get; set; }
            public DateTime FecEstFin_Nueva { get; set; }
            public DateTime FecEstFin_Nueva_Original { get; set; }
            public bool CambioAplicable { get; set; }
            public string message { get; set; }
        }

        public ResultActualizacionArbolPPA AplicarActualizacionCascada(ResultActualizacionArbolPPA oRAAPPA, bool DiscriminaAvance)
        {
            try
            {
                if (oRAAPPA.CambioAplicable)
                {
                    Planificacion_Proyecto_Actividades oPPA_AModificar = new Planificacion_Proyecto_ActividadesNegocio()
                        .Get_X_Id(oRAAPPA.IdPlanProyActividad, true);
                    bool puedeModificar = true;
                    if (oPPA_AModificar.Id != oRAAPPA.IdPlanProyOrigenMovimiento && oPPA_AModificar.lPPADep_Padres.Count > 0)
                    {
                        puedeModificar = oRAAPPA.FecEstFin_Nueva_Original > oPPA_AModificar.lPPADep_Padres
                            .Max(ppad => ppad.PPAPadre.Fecha_Est_Fin);
                    }
                    if (puedeModificar || oRAAPPA.IdPlanProyOrigenMovimiento == oRAAPPA.IdPlanProyActividad)
                    {
                        DateTime fecEstInicio_Original = oPPA_AModificar.Fecha_Est_Incio;
                        DateTime fecEstFin_Original = oPPA_AModificar.Fecha_Est_Fin;
                        // preparamos la actividad a mover con la nuevas fechas
                        if (DiscriminaAvance == true || oPPA_AModificar.Avance == 0)
                            oPPA_AModificar.Fecha_Est_Incio = oRAAPPA.FecEstInicio_Nueva;
                        oPPA_AModificar.Fecha_Est_Fin = oRAAPPA.FecEstFin_Nueva;
                        oRAAPPA.lPlanProyActividades_Resultante.Add(oPPA_AModificar);

                        #region SI TIENE PADRE

                        #region CODIGO SUSPENDIDO
                        //if (oRAAPPA.lPlanProyActividades_Resultante.Find(ppar => ppar.Id == oPPA_AModificar.IdPadre) == null
                        //    && oPPA_AModificar.IdPadre > 0)
                        //{
                        //    if (oPPA_AModificar.Fecha_Est_Incio < fecEstInicio_Original)
                        //    {
                        //        Planificacion_Proyecto_Actividades oPPA_Padre = new Planificacion_Proyecto_ActividadesNegocio()
                        //            .Get_X_Id(oPPA_AModificar.IdPadre);
                        //        // TODO: esta condición se irá ya que la idea es que se pregunte
                        //        // si distintas condiciones del padre permiten el movimiento.
                        //        if (!oPPA_Padre.Solapable)
                        //        {
                        //            // ¿se solapan?
                        //            if (oPPA_AModificar.Fecha_Est_Incio < oPPA_Padre.Fecha_Est_Fin)
                        //            {
                        //                oRAAPPA.CambioAplicable = false;
                        //                oRAAPPA.message += "Actividad padre no permite solapamiento.\n\r";
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion

                        #endregion

                        #region SI TIENE HIJAS
                        if (oRAAPPA.FecEstFin_Nueva > fecEstFin_Original)
                        {
                            List<Planificacion_Proyecto_Actividades> lPPAs_hijas = new Planificacion_Proyecto_ActividadesNegocio()
                                .Get_X_IdPadre(oPPA_AModificar.Id);
                            if (lPPAs_hijas.Count > 0)
                            {
                                int cantidadDiasMovimiento =
                                    (int)(oPPA_AModificar.Fecha_Est_Fin - fecEstFin_Original).TotalDays;
                                if (cantidadDiasMovimiento != 0)
                                {
                                    lPPAs_hijas.ForEach(ppa =>
                                    {
                                        oRAAPPA.IdPlanProyActividad = ppa.Id;
                                        oRAAPPA.FecEstInicio_Nueva = ppa.Fecha_Est_Incio.AddDays(cantidadDiasMovimiento);
                                        oRAAPPA.FecEstFin_Nueva = ppa.Fecha_Est_Fin.AddDays(cantidadDiasMovimiento);
                                        this.AplicarActualizacionCascada(oRAAPPA, false);
                                    });
                                }
                            }
                        }
                        #endregion

                        if (oRAAPPA.CambioAplicable)
                        {
                            oPPA_AModificar.FecEstInicio_Original = oPPA_AModificar.Fecha_Est_Incio;
                            oPPA_AModificar.FecEstFin_Original = oPPA_AModificar.Fecha_Est_Fin;
                            new Planificacion_Proyecto_ActividadesNegocio().Update(oPPA_AModificar);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: PlanificacionController: AplicarActualizacionCascada(ResultActualizacionArbolPPA): exception.", ex);
            }
            return oRAAPPA;
        }

        #endregion

        #region Select's

        public List<Planificacion_Proyecto_Actividades> Get_All_ubi_Proyecto_Actividades(int id)
        {
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades.Where(x => x.Proyecto_UbicacionesId == id).ToList();
            }

            return Lista;
        }

        public List<Planificacion_Proyecto_Actividades> Get_All_Planificacion_Proyecto_Actividades_SinIncludes()
        {
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades.ToList();
            }

            return Lista;
        }

        public List<Planificacion_Proyecto_Actividades> Get_All()
        {
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades.ToList();
            }
            return Lista;
        }

        public List<Planificacion_Proyecto_Actividades> Get_All_Planificacion_Proyecto_Actividades()
        {
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades
                    .Include(x => x.Proyecto_Ubicaciones)
                    .Include(x => x.Planificacion_Actividades)
                    .Include(x => x.Usuarios)
                    .Include(x => x.Planificacion_Actividades_Rubro).ToList();
            }

            return Lista;
        }

        public List<Planificacion_Proyecto_Actividades> Get_All_Planificacion_Proyecto_Actividades_PA()
        {
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades
                    .Include(x => x.Planificacion_Actividades).ToList();
            }

            return Lista;
        }

        public List<Planificacion_Proyecto_Actividades> Get_All_Planificacion_Proyecto_Actividades_GeneraPD(long pProyectoId, bool pFinalizados, DateTime pFecha)
        {
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades.Include(x => x.Proyecto_Ubicaciones).Where(p => p.Proyecto_Ubicaciones.ProyectoId == pProyectoId && p.Finalizados == pFinalizados && p.Fecha_Est_Incio < pFecha).ToList();
            }

            return Lista;
        }

        public List<Planificacion_Proyecto_Actividades> Get_All_Planificacion_Proyecto_Actividades(int IdProyecto)
        {
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            List<Planificacion_Proyecto_Actividades> ListaReturn = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades
                   .Include(x => x.Proyecto_Ubicaciones).Where(p => p.Proyecto_Ubicaciones.ProyectoId == IdProyecto)
                   .Include(x => x.Planificacion_Actividades)
                   .Include(x => x.Contratistas)
                   .Include(x => x.Planificacion_Actividades_Rubro)
                   .Include(x => x.Planificacion_Proyecto_Actividades_Log).Where(p => p.Proyecto_Ubicaciones.ProyectoId == IdProyecto).ToList();
                foreach (var item in Lista)
                {
                    Planificacion_Proyecto_Actividades pa = new Planificacion_Proyecto_Actividades();
                    pa = item;
                    pa.Planificacion_Proyecto_Actividades_Log = item.Planificacion_Proyecto_Actividades_Log.OrderByDescending(o => o.Version).ToList();
                    ListaReturn.Add(pa);
                }

            }
            ListaReturn = ListaReturn.OrderBy(p => p.Proyecto_UbicacionesId).ToList();


            return ListaReturn;
        }

        public Planificacion_Proyecto_Actividades Get_X_Id(int Id, bool ConListas)
        {
            return this.Get_X_Id(Id, ConListas, false);
        }

        public Planificacion_Proyecto_Actividades Get_X_Id(int Id, bool ConListas, bool ConIncludes)
        {
            if (ConListas)
            {
                Planificacion_Proyecto_Actividades oPlanProyActividad = new Planificacion_Proyecto_Actividades();
                using (var db = new iotdbContext())
                {
                    if (ConIncludes)
                    {
                        oPlanProyActividad = db.Planificacion_Proyecto_Actividades
                            .Include(ppa => ppa.Planificacion_Actividades_Rubro)
                            .Include(ppa => ppa.Planificacion_Actividades)
                            .Include(ppa => ppa.Proyecto_Ubicaciones)
                            .Include(ppa => ppa.Contratistas)
                            .FirstOrDefault(m => m.Id == Id);
                    }
                    else
                    {
                        oPlanProyActividad = db.Planificacion_Proyecto_Actividades
                            .FirstOrDefault(m => m.Id == Id);
                    }
                    List<PlanProyAct_Dependencia> lPPA_Dependencias = new PlanProyAct_DependenciaNegocio()
                        .Get_x_IdPPA(Id);
                    if (oPlanProyActividad != null)
                    {
                        oPlanProyActividad.PlanProyAct_Dependencias = new List<PlanProyAct_Dependencia>();
                        oPlanProyActividad.PlanProyAct_Dependencias = lPPA_Dependencias;
                    }
                        
                }
                return oPlanProyActividad;
            }
            else
            {
                return this.Get_X_Id(Id);
            }
        }

        public Planificacion_Proyecto_Actividades Get_X_Id(int Id)
        {
            Planificacion_Proyecto_Actividades oPlanProyActividad = new Planificacion_Proyecto_Actividades();
            using (var db = new iotdbContext())
            {
                oPlanProyActividad = db.Planificacion_Proyecto_Actividades.FirstOrDefault(m => m.Id == Id);
            }

            return oPlanProyActividad;
        }

        public List<Planificacion_Proyecto_Actividades> Get_X_Ids(int IdPlanActRubro, int IdPlanActividad, int IdProyUbicacion)
        {
            List<Planificacion_Proyecto_Actividades> lPlanProyActividades = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                lPlanProyActividades = db.Planificacion_Proyecto_Actividades
                    .Where(ppa =>
                    (IdPlanActRubro == 0 || ppa.Planificacion_Actividades_RubroId == IdPlanActRubro) &&
                    (IdPlanActividad == 0 || ppa.Planificacion_ActividadesId == IdPlanActividad) &&
                    (IdProyUbicacion == 0 || ppa.Proyecto_UbicacionesId == IdProyUbicacion))
                    .ToList();
            }
            return lPlanProyActividades;
        }

        public List<Planificacion_Proyecto_Actividades> Get_X_Ids_Corregido(int IdPlanActRubro, int IdPlanActividad, int IdProyUbicacion, int IdActActual)
        {
            List<Planificacion_Proyecto_Actividades> lPlanProyActividades;
            using (var db = new iotdbContext())
            {
                Planificacion_Proyecto_Actividades ppa = Get_X_Id(IdPlanActividad);
                var IdActividad = ppa.Planificacion_ActividadesId;
                //Buscamos el proyecto que corresponde
                var IdProyecto = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones(ppa.Proyecto_UbicacionesId).ProyectoId;
                //Buscamos todas las ubicaciones para ese proyecto,
                List<int> Ubicaciones = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(IdProyecto).Select(s=>s.Id).ToList();
                if (IdProyUbicacion != 0)
                {
                    lPlanProyActividades = db.Planificacion_Proyecto_Actividades
                        .Where(ppa => ppa.Proyecto_UbicacionesId == IdProyUbicacion
                            && ppa.Planificacion_Actividades_RubroId == IdPlanActRubro
                            && ppa.Planificacion_ActividadesId == IdActividad
                            && ppa.Id!=IdActActual
                        )
                        .ToList();
                }
                else
                {
                    lPlanProyActividades = db.Planificacion_Proyecto_Actividades
                        .Where(ppa => ppa.Planificacion_Actividades_RubroId == IdPlanActRubro
                            && ppa.Planificacion_ActividadesId == IdActividad
                            &&  Ubicaciones.Contains(ppa.Proyecto_UbicacionesId)
                            && ppa.Id != IdActActual
                        )
                        .ToList();
                }
            }
            return lPlanProyActividades;
        }

        /// <summary>
        /// Método que devuelve una lista de PPAs según el IdPadre
        /// </summary>
        /// <param name="IdPadre"></param>
        /// <returns></returns>
        public List<Planificacion_Proyecto_Actividades> Get_X_IdPadre(int IdPadre)
        {
            List<Planificacion_Proyecto_Actividades> lPPA_Hijas = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                List<PlanProyAct_Dependencia> lPPADep_Hijas = new PlanProyAct_DependenciaNegocio()
                    .Get_x_IdPadre(IdPadre);
                lPPA_Hijas = lPPADep_Hijas.Select(ppad => ppad.PPAHija).ToList();
            }
            return lPPA_Hijas;
        }

        public List<Planificacion_Proyecto_Actividades> Get_All_Planificacion_Ubicaciones_Actividades()
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
                using (var db = new iotdbContext())
                {
                    Lista = db.Planificacion_Proyecto_Actividades
                        .Include(x => x.Planificacion_Actividades_Rubro)
                        .Include(x => x.Planificacion_Actividades)
                        .Include(x => x.Proyecto_Ubicaciones)
                        .Include(x => x.Proyecto_Ubicaciones.Proyecto).ToList();
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<Planificacion_Proyecto_Actividades> Get_X_IdProyecto(int idProyecto)
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
                using (var db = new iotdbContext())
                {
                    Lista = db.Planificacion_Proyecto_Actividades
                        .Include(x => x.Planificacion_Actividades_Rubro)
                        .Include(x => x.Planificacion_Actividades)
                        .Include(x => x.Proyecto_Ubicaciones)
                        .Include(x => x.Proyecto_Ubicaciones.Proyecto)
                        .Where(ppa => ppa.Proyecto_Ubicaciones.ProyectoId == idProyecto)
                        .ToList();
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<Planificacion_Proyecto_Actividades> Get_X_IdProyecto_IdUbicacion(int idProyecto, int idUbicacion)
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
                using (var db = new iotdbContext())
                {
                    Lista = db.Planificacion_Proyecto_Actividades
                        .Include(x => x.Planificacion_Actividades_Rubro)
                        .Include(x => x.Planificacion_Actividades)
                        .Include(x => x.Proyecto_Ubicaciones)
                        .Include(x => x.Proyecto_Ubicaciones.Proyecto)
                        .Where(ppa => ppa.Proyecto_Ubicaciones.ProyectoId == idProyecto)
                        .Where(ppa => ppa.Proyecto_Ubicaciones.Id == idUbicacion)
                        .ToList();
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }
        public List<Planificacion_Proyecto_Actividades> Get_Act_IdProyecto_X_Rubro(int idProyecto)
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();
                    //var query = $@" SELECT PPA.Planificacion_Actividades_RubroId, PPA.Proyecto_UbicacionesId, PPA.Planificacion_ActividadesId,
                    //            MIN(PPA.Fecha_Est_Incio) AS Fecha_Est_Incio, MAX(PPA.Fecha_Est_Fin) AS Fecha_Est_Fin , MIN(PPA.Fecha_Real_Incio) AS Fecha_Real_Incio ,MAX(PPA.Fecha_Real_Fin) AS Fecha_Real_Fin,
                    //            MIN(PPA.FecEstInicio_Original) AS FecEstInicio_Original, MAX(PPA.FecEstFin_Original) AS FecEstFin_Original, PU.ProyectoId, PPA.Detalle,
                    //            PPA.ContratistasId, PPA.Incidencia, PPA.Finalizados, PAR.Nombre, PPA.IdPadre, PU.Nombre AS Ubicacion, MIN(PPA.Avance) AS Avance
                    //            FROM puchetadb.planificacion_proyecto_actividades PPA
                    //            INNER JOIN puchetadb.Planificacion_Actividades_Rubro PAR ON PAR.Id = PPA.Planificacion_Actividades_RubroId
                    //            INNER JOIN puchetadb.Planificacion_Actividades PA on PPA.Planificacion_ActividadesId = PA.Id
                    //            INNER JOIN puchetadb.Proyecto_Ubicaciones PU on PU.Id = PPA.Proyecto_UbicacionesId
                    //            WHERE PU.ProyectoId = {idProyecto} and PPA.Avance > 0
                    //            GROUP BY PPA.Planificacion_Actividades_RubroId, PPA.Proyecto_UbicacionesId";
                    var query = $@" SELECT PPA.Planificacion_Actividades_RubroId, PPA.Proyecto_UbicacionesId, PPA.Planificacion_ActividadesId,
                                MIN(PPA.Fecha_Est_Incio) AS Fecha_Est_Incio, MAX(PPA.Fecha_Est_Fin) AS Fecha_Est_Fin , MIN(PPA.Fecha_Real_Incio) AS Fecha_Real_Incio ,MAX(PPA.Fecha_Real_Fin) AS Fecha_Real_Fin,
                                MIN(PPA.FecEstInicio_Original) AS FecEstInicio_Original, MAX(PPA.FecEstFin_Original) AS FecEstFin_Original, PU.ProyectoId, PPA.Detalle,
                                PPA.ContratistasId, PPA.Incidencia, PPA.Finalizados, PAR.Nombre, PPA.IdPadre, PU.Nombre AS Ubicacion, MIN(PPA.Avance) AS Avance
                                FROM planificacion_proyecto_actividades PPA
                                INNER JOIN Planificacion_Actividades_Rubro PAR ON PAR.Id = PPA.Planificacion_Actividades_RubroId
                                INNER JOIN Planificacion_Actividades PA on PPA.Planificacion_ActividadesId = PA.Id
                                INNER JOIN Proyecto_Ubicaciones PU on PU.Id = PPA.Proyecto_UbicacionesId
                                WHERE PU.ProyectoId = {idProyecto} and PPA.Avance > 0
                                GROUP BY PPA.Planificacion_Actividades_RubroId, PPA.Proyecto_UbicacionesId, PPA.Planificacion_ActividadesId,PPA.ContratistasId , PPA.Detalle, PPA.Incidencia, PPA.Finalizados, PAR.Nombre, PPA.IdPadre";
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Planificacion_Proyecto_Actividades item = new Planificacion_Proyecto_Actividades();
                        item.Planificacion_Actividades_RubroId = Convert.ToInt32(reader["Planificacion_Actividades_RubroId"]);
                        item.Fecha_Est_Incio = Convert.ToDateTime(reader["Fecha_Est_Incio"]);
                        item.Fecha_Est_Fin = Convert.ToDateTime(reader["Fecha_Est_Fin"]);
                        item.Fecha_Real_Incio = Convert.ToDateTime(reader["Fecha_Real_Incio"]);
                        item.Fecha_Real_Fin = Convert.ToDateTime(reader["Fecha_Real_Fin"]);
                        item.FecEstInicio_Original = Convert.ToDateTime(reader["FecEstInicio_Original"]);
                        item.FecEstFin_Original = Convert.ToDateTime(reader["FecEstFin_Original"]);
                        item.Planificacion_Actividades_RubroId = Convert.ToInt32(reader["Planificacion_Actividades_RubroId"]);
                        item.Planificacion_Actividades_Rubro = new Planificacion_Actividades_Rubro();
                        item.Planificacion_Actividades_Rubro.Id = Convert.ToInt32(reader["Planificacion_Actividades_RubroId"]);
                        item.Planificacion_Actividades_Rubro.Nombre = Convert.ToString(reader["Nombre"]);
                        item.Detalle = Convert.ToString(reader["Detalle"]);
                        //item.Finalizados = Convert.ToBoolean(reader["Finalizados"]);
                        item.Proyecto_Ubicaciones = new Proyecto_Ubicaciones();
                        item.Proyecto_Ubicaciones.Id = Convert.ToInt32(reader["Proyecto_UbicacionesId"]);
                        item.Proyecto_UbicacionesId = Convert.ToInt32(reader["Proyecto_UbicacionesId"]);
                        item.Proyecto_Ubicaciones.Nombre = Convert.ToString(reader["Ubicacion"]);
                        item.Proyecto_Ubicaciones.ProyectoId = Convert.ToInt32(reader["ProyectoId"]);
                        string avance = Convert.ToString(reader["Avance"]);
                        if (avance != "100")
                        {
                            avance = "0";
                        }
                        item.Avance = (float)Convert.ToDecimal(avance);
                        Lista.Add(item);
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<Planificacion_Proyecto_Actividades> Get_All_Planificacion_Ubicaciones_Actividades(int id)
        {
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades
                    .Include(x => x.Proyecto_Ubicaciones)
                    .Include(x => x.Proyecto_Ubicaciones.Proyecto)
                    .Include(x => x.Planificacion_Actividades_Rubro)
                    .Include(x => x.Planificacion_Actividades)
                    .Where(x => x.Proyecto_UbicacionesId == id).ToList();
            }

            return Lista;
        }

        public List<Planificacion_Proyecto_Actividades> Get_All_PPA_Filtro(int IdRubro, int IdActividad, int IdUbicacion)
        {
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades
                    .Where(x => x.Proyecto_UbicacionesId == IdUbicacion
                        && x.Planificacion_ActividadesId == IdActividad
                        && x.Planificacion_Actividades_RubroId == IdRubro
                    )
                    .ToList();
            }

            return Lista;
        }

        public List<Planificacion_Proyecto_Actividades> Get_X_lIdsProyUbicaciones(List<int> lIdsProyUbicaciones)
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> lPlanProyActividades = new List<Planificacion_Proyecto_Actividades>();
                using (var db = new iotdbContext())
                {
                    lPlanProyActividades = db.Planificacion_Proyecto_Actividades
                        .Where(ppa => lIdsProyUbicaciones.Contains(ppa.Proyecto_UbicacionesId))
                        .ToList();
                }
                return lPlanProyActividades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<Planificacion_Proyecto_Actividades> Get_X_lIds(List<int> lIdsPPAs, bool conIncludes)
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> lPlanProyActividades = new List<Planificacion_Proyecto_Actividades>();
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        lPlanProyActividades = db.Planificacion_Proyecto_Actividades
                            .Include(ppa => ppa.Planificacion_Actividades)
                            .Include(ppa => ppa.Proyecto_Ubicaciones)
                            .Where(ppa => lIdsPPAs.Contains(ppa.Id))
                            .ToList();
                    }
                    else
                    {
                        lPlanProyActividades = db.Planificacion_Proyecto_Actividades
                            .Where(ppa => lIdsPPAs.Contains(ppa.Id))
                            .ToList();
                    }
                }
                return lPlanProyActividades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<Proyecto> GetProyecto_x_lIdsContratistas(List<int> lIdsContratistas)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.Planificacion_Proyecto_Actividades
                        .Include(ppa => ppa.Proyecto_Ubicaciones)
                        .Include(ppa => ppa.Proyecto_Ubicaciones.Proyecto)
                        .Where(ppa => lIdsContratistas.Contains(ppa.ContratistasId))
                        .ToList()
                        .GroupBy(ppa => ppa.Proyecto_Ubicaciones.ProyectoId)
                        .Select(g => g.First().Proyecto_Ubicaciones.Proyecto)
                        .ToList();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error: exception.", ex);
            }
        }

        public Planificacion_Proyecto_Actividades GetRubroByActividad_Id(int IdActividad)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.Planificacion_Proyecto_Actividades
                        .Where(s=>s.Planificacion_ActividadesId == IdActividad).First();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: exception.", ex);
            }
        }

        public List<Planificacion_Proyecto_Actividades> Get_Atrasadas(int ProyectoId, bool conIncludes)
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> lPPAs = new List<Planificacion_Proyecto_Actividades>();
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        lPPAs = db.Planificacion_Proyecto_Actividades
                            .Include(ppa => ppa.Proyecto_Ubicaciones)
                            .Include(ppa => ppa.Proyecto_Ubicaciones.Proyecto)
                            .Include(ppa => ppa.Planificacion_Actividades)
                            .Where(ppa => ppa.Fecha_Est_Incio < DateTime.Now &&
                                ppa.Fecha_Real_Incio == DateTime.MinValue &&
                                (ProyectoId == 0 || ppa.Proyecto_Ubicaciones.ProyectoId == ProyectoId))
                            .ToList();
                    }
                    else
                    {
                        lPPAs = db.Planificacion_Proyecto_Actividades
                           .Where(ppa => ppa.Fecha_Est_Incio < DateTime.Now &&
                                ppa.Fecha_Real_Incio == DateTime.MinValue)
                           .ToList();
                    }
                }
                return lPPAs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: exception.", ex);
            }
        }

        public float GetCantidadAcumulada(int PlanProyActividadId)
        {
            try
            {
                using var db = new iotdbContext();
                List<ParteDiario_Actividades> lPDAs = db.ParteDiario_Actividades
                    .Where(pda => pda.Planificacion_Proyecto_ActividadesId == PlanProyActividadId)
                    .ToList();
                List<int> lIdsPDAs = lPDAs.Select(pda => pda.Id).ToList();
                List<ParteDiario_Actividades_Contratista> lPDACs = db.ParteDiario_Actividades_Contratista
                    .Where(pdac => lIdsPDAs.Contains(pdac.ParteDiario_ActividadesId))
                    .ToList();
                return lPDACs.Sum(pdac => pdac.Cantidad);
            }
            catch(Exception ex)
            {
                throw new Exception("");
            }
        }

        #endregion
    }
}
