using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class Planificacion_Actividades_RubroNegocio
    {


        #region ABM's

        public int Insert(Planificacion_Actividades_Rubro data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Planificacion_Actividades_Rubro data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Planificacion_Actividades_Rubro data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Planificacion_Actividades_Rubro data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Planificacion_Actividades_Rubro.Attach(data);
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
        public List<Planificacion_Actividades_Rubro> Get_All_Planificacion_Actividades_Rubros()
        {
            List<Planificacion_Actividades_Rubro> Lista = new List<Planificacion_Actividades_Rubro>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Actividades_Rubro.OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }
        public List<Planificacion_Actividades_Rubro> Get_All_Planificacion_Actividades_Rubro()
        {
            List<Planificacion_Actividades_Rubro> Lista = new List<Planificacion_Actividades_Rubro>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Actividades_Rubro.ToList();
            }

            return Lista;
        }

        public Planificacion_Actividades_Rubro Get_One_Planificacion_Actividades_Rubro(int Id)
        {
            Planificacion_Actividades_Rubro accion = new Planificacion_Actividades_Rubro();
            using (var db = new iotdbContext())
            {
                accion = db.Planificacion_Actividades_Rubro.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public List<Planificacion_Actividades_Rubro> Get_X_lIds(List<int> lIds)
        {
            try
            {
                List<Planificacion_Actividades_Rubro> lPlanActRubros = new List<Planificacion_Actividades_Rubro>();
                using (var db = new iotdbContext())
                {
                    lPlanActRubros = db.Planificacion_Actividades_Rubro
                        .Where(x => lIds.Contains(x.Id) == true)
                        .ToList();
                }
                return lPlanActRubros;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        #endregion



    }
}
