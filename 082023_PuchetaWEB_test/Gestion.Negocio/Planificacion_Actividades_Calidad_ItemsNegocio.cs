using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gestion.Negocio
{
    public class Planificacion_Actividades_Calidad_ItemsNegocio
    {

        #region ABM's

        public int Insert(Planificacion_Actividades_Calidad_Items data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Planificacion_Actividades_Calidad_Items data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Planificacion_Actividades_Calidad_Items data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Planificacion_Actividades_Calidad_Items data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Planificacion_Actividades_Calidad_Items.Attach(data);
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

        public List<Planificacion_Actividades_Calidad_Items> Get_All_Planificacion_Actividades_Calidad_Items()
        {
            List<Planificacion_Actividades_Calidad_Items> Lista = new List<Planificacion_Actividades_Calidad_Items>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Actividades_Calidad_Items.ToList();
            }

            return Lista;
        }


        public List<Planificacion_Actividades_Calidad_Items> Get_All_Planificacion_Actividades_Calidad_ItemsId(int IdPLanificacionActividades)
        {
            List<Planificacion_Actividades_Calidad_Items> Lista = new List<Planificacion_Actividades_Calidad_Items>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Actividades_Calidad_Items.Where(x => x.IdPLanificacionActividades == IdPLanificacionActividades).ToList();
            }

            return Lista;
        }

        public List<Planificacion_Actividades_Calidad_Items> Get_All_Planificacion_Actividades_Calidad_ItemsIdAct(int id)
        {
            List<Planificacion_Actividades_Calidad_Items> Lista = new List<Planificacion_Actividades_Calidad_Items>();
            using (var db = new iotdbContext())
            {
                Lista = db.Planificacion_Actividades_Calidad_Items.Where(x => x.IdPLanificacionActividades == id).ToList();
            }

            return Lista;
        }


        public Planificacion_Actividades_Calidad_Items Get_One_Planificacion_Actividades_Calidad_Items(int Id)
        {
            Planificacion_Actividades_Calidad_Items accion = new Planificacion_Actividades_Calidad_Items();
            using (var db = new iotdbContext())
            {
                accion = db.Planificacion_Actividades_Calidad_Items.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion


    }
}
