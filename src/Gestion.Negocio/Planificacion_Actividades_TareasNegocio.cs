using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
     public class Planificacion_Actividades_TareasNegocio
    {
        #region ABM's

        public int Insert(Planificacion_Actividades_Tareas data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Planificacion_Actividades_Tareas data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Planificacion_Actividades_Tareas data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Planificacion_Actividades_Tareas data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Planificacion_Actividades_Tareas.Attach(data);
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

        public List<Planificacion_Actividades_Tareas> Get_All_Planificacion_Actividades_Tareas()
        {
            List<Planificacion_Actividades_Tareas> Lista = new List<Planificacion_Actividades_Tareas>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Actividades_Tareas.Include(x => x.Planificacion_Actividades).ToList();
            }

            return Lista;
        }


        public Planificacion_Actividades_Tareas Get_One_Planificacion_Actividades_Tareas(int Id)
        {
            Planificacion_Actividades_Tareas accion = new Planificacion_Actividades_Tareas();
            using (var db = new iotdbContext())
            {
                accion = db.Planificacion_Actividades_Tareas.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion

    }
}
