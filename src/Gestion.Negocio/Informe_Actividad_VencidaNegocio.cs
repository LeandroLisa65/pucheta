using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
     public class Informe_Actividad_VencidaNegocio
    {

        #region ABM's

        public int Insert(Informe_Actividad_Vencida data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Informe_Actividad_Vencida data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Informe_Actividad_Vencida data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        public void Borrar_Informes_Proyecto(int pIdProyecto)
        {
            try
            {
                List<Informe_Actividad_Vencida> lstInformes = new List<Informe_Actividad_Vencida>();
                using (var db = new iotdbContext())
                {
                    lstInformes = db.Informes_Actividades_Vencidas
                        .Where(iav =>
                            (pIdProyecto == 0 || iav.ProyectoId == pIdProyecto))
                        .ToList();
                }
                foreach (var item in lstInformes)
                {
                    Delete(item);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private int GrabarCambios(Informe_Actividad_Vencida data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Informes_Actividades_Vencidas.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception)
            {
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        public Informe_Actividad_Vencida Get_Unico_X_Ids(int Planificacion_Proyecto_ActividadId, 
            int Proyecto_UbicacionId, int Planificacion_ActividadId)
        {
            Informe_Actividad_Vencida oInfActVencida = new Informe_Actividad_Vencida();
            using (var db = new iotdbContext())
            {
                oInfActVencida = db.Informes_Actividades_Vencidas
                    .FirstOrDefault(iav => 
                        iav.PlanificacionProyectoActividadId == Planificacion_Proyecto_ActividadId &&
                        iav.ProyectoUbicacionId == Proyecto_UbicacionId &&
                        iav.PlanActividadId == Planificacion_ActividadId);
            }
            return oInfActVencida;
        }
        public List<Informe_Actividad_Vencida> Get_Varios_X_Ids(int IdProyecto,
            int IdUbicacion, int IdActividad)
        {
            List<Informe_Actividad_Vencida> lInfActVencidas = new List<Informe_Actividad_Vencida>();
            using (var db = new iotdbContext())
            {
                lInfActVencidas = db.Informes_Actividades_Vencidas
                    .Where(iav =>
                        (IdProyecto == 0 || iav.ProyectoId == IdProyecto) &&
                        (IdUbicacion == 0 || iav.ProyectoUbicacionId == IdUbicacion) &&
                        (IdActividad == 0 || iav.PlanActividadId == IdActividad))
                    .ToList();
            }
            return lInfActVencidas;
        }

        public DateTime Get_Max_FecCreacion()
        {
            DateTime maxFecCreacion = DateTime.MinValue;
            using (var db = new iotdbContext())
            {
                maxFecCreacion = db.Informes_Actividades_Vencidas
                    .Max(iav => iav.FecCreacion);
            }
            return maxFecCreacion;
        }



        public List<Informe_Actividad_Vencida> Get_Informe_Completo_Por_Proyecto(int pIdProyecto)
        {
            List<Informe_Actividad_Vencida> lstInformes = new List<Informe_Actividad_Vencida>();
            using (var db = new iotdbContext())
            {
                if (pIdProyecto != 0)
                {
                    lstInformes = db.Informes_Actividades_Vencidas
                       .Where(iav =>
                           (pIdProyecto == 0 || iav.ProyectoId == pIdProyecto))
                       .ToList();
                }
                else
                {
                    lstInformes = db.Informes_Actividades_Vencidas.ToList();
                }
                
            }
            foreach (var item in lstInformes)
            {
                item.sPlanActividad = new Planificacion_ActividadesNegocio()
                    .Get_One_Planificacion_Actividades(item.PlanActividadId).Nombre;
                item.sProyecto = new ProyectoNegocio()
                    .Get_One_Proyecto(item.ProyectoId).Nombre;
                item.sProyUbicacion = new Proyecto_UbicacionesNegocio()
                    .Get_One_Proyecto_Ubicaciones(item.ProyectoUbicacionId).Nombre;
            }
            return lstInformes.OrderByDescending(p => p.DiasVencida).ToList();
        }
        #endregion

    }
}
