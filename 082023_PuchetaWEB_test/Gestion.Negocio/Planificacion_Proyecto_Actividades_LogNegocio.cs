using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
     public class Planificacion_Proyecto_Actividades_Log_Negocio
    {
        #region ABM's

        public int Insert(Planificacion_Proyecto_Actividades_Log data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Planificacion_Proyecto_Actividades_Log data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Planificacion_Proyecto_Actividades_Log data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Planificacion_Proyecto_Actividades_Log data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Planificacion_Proyecto_Actividades_Log.Attach(data);
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

        public List<Planificacion_Proyecto_Actividades_Log> Get_All_Planificacion_Proyecto_Actividades_Log()
        {
            List<Planificacion_Proyecto_Actividades_Log> Lista = new List<Planificacion_Proyecto_Actividades_Log>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades_Log.ToList();
            }

            return Lista;
        }

        public List<Planificacion_Proyecto_Actividades_Log> Get_All_Planificacion_Proyecto_Actividades_Log(int id)
        {
            List<Planificacion_Proyecto_Actividades_Log> Lista = new List<Planificacion_Proyecto_Actividades_Log>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Proyecto_Actividades_Log.Where(x=> x.Planificacion_Proyecto_ActividadesId==id).ToList();
            }

            return Lista;
        }


        public Planificacion_Proyecto_Actividades_Log Get_One_Planificacion_Proyecto_Actividades_Log(int Id)
        {
            Planificacion_Proyecto_Actividades_Log accion = new Planificacion_Proyecto_Actividades_Log();
            using (var db = new iotdbContext())
            {
                accion = db.Planificacion_Proyecto_Actividades_Log.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion

    }
}
