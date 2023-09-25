using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
     public class Planificacion_ActividadesNegocio
    {

        #region ABM's

        public int Insert(Planificacion_Actividades data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Planificacion_Actividades data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Planificacion_Actividades data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Planificacion_Actividades data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Planificacion_Actividades.Attach(data);
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

        public List<Planificacion_Actividades> Get_All_Planificacion_Actividades()
        {
            List<Planificacion_Actividades> Lista = new List<Planificacion_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Actividades
                    .Include(x => x.PartidaPresupuestaria)
                    .Include(x=> x.Planificacion_Actividades_Rubro).ToList();
            }

            return Lista;
        }

        public List<Planificacion_Actividades> Get_All_Planificacion_Actividades(int id)
        {
            List<Planificacion_Actividades> Lista = new List<Planificacion_Actividades>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Actividades.Where(x=> x.Planificacion_Actividades_RubroId == id).OrderBy(x => x.Nombre).ToList();
            }

            return Lista;
        }

        public Planificacion_Actividades Get_One_Planificacion_Actividades(int Id)
        {
            Planificacion_Actividades accion = new Planificacion_Actividades();
            using (var db = new iotdbContext())
            {
                accion = db.Planificacion_Actividades.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public List<Planificacion_Actividades> Get_X_lIds(List<int> lIdsPlanActividades)
        {
            try
            {
                List<Planificacion_Actividades> lPlanActividades = new List<Planificacion_Actividades>();
                using (var db = new iotdbContext())
                {
                    lPlanActividades = db.Planificacion_Actividades
                        .Where(pa => lIdsPlanActividades.Contains(pa.Id))
                        .ToList();
                }
                return lPlanActividades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<Planificacion_Actividades> Get_X_lIdsPlanActividades(List<int> lIdsPlanActividades)
        {
            try
            {
                List<Planificacion_Actividades> lPlanActividades = new List<Planificacion_Actividades>();
                using (var db = new iotdbContext())
                {
                    lPlanActividades = db.Planificacion_Actividades
                        .Where(pa => lIdsPlanActividades.Contains(pa.Id))
                        .ToList();
                }
                return lPlanActividades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        #endregion

    }
}
