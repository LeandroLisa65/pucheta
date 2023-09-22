using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;


namespace Gestion.Negocio
{
     public class Proyecto_UbicacionesNegocio
    {

        #region ABM's

        public int Insert(Proyecto_Ubicaciones data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Proyecto_Ubicaciones data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Proyecto_Ubicaciones data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Proyecto_Ubicaciones data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Proyecto_Ubicaciones.Attach(data);
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

        public List<Proyecto_Ubicaciones> Get_All_Proyecto_Ubicaciones()
        {
            List<Proyecto_Ubicaciones> Lista = new List<Proyecto_Ubicaciones>();
            using (var db = new iotdbContext())
            {
                Lista = db.Proyecto_Ubicaciones.Include(x=> x.Proyecto).ToList();
            }

            return Lista;
        }

        public List<Proyecto_Ubicaciones> Get_X_IdProyecto(int IdProyecto)
        {
            List<Proyecto_Ubicaciones> Lista = new List<Proyecto_Ubicaciones>();
            using (var db = new iotdbContext())
            {
                Lista = db.Proyecto_Ubicaciones.Where(x => x.ProyectoId == IdProyecto).ToList();
            }

            return Lista;
        }

        public List<Proyecto_Ubicaciones> Get_X_lIds(List<int> lIdsProyUbicaciones)
        {
            try
            {
                List<Proyecto_Ubicaciones> lProyUbicaciones = new List<Proyecto_Ubicaciones>();
                using (var db = new iotdbContext())
                {
                    lProyUbicaciones = db.Proyecto_Ubicaciones
                        .Where(x => lIdsProyUbicaciones.Contains(x.Id) == true)
                        .ToList();
                }
                return lProyUbicaciones;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<Proyecto_Ubicaciones> Get_X_lIdsProyectos(List<int> lIdProyectos)
        {
            try
            {
                List<Proyecto_Ubicaciones> lProyUbicaciones = new List<Proyecto_Ubicaciones>();
                using (var db = new iotdbContext())
                {
                    lProyUbicaciones = db.Proyecto_Ubicaciones
                        .Where(x => lIdProyectos.Contains(x.ProyectoId) == true)
                        .ToList();
                }
                return lProyUbicaciones;
            }
            catch(Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public List<Proyecto_Ubicaciones> Get_All_Ubicaciones(int id)
        {
            List<Proyecto_Ubicaciones> Lista = new List<Proyecto_Ubicaciones>();
            using (var db = new iotdbContext())
            {
                Lista = db.Proyecto_Ubicaciones.Where(x => x.Id == id).ToList();
            }

            return Lista;
        }

        public Proyecto_Ubicaciones Get_One_Proyecto_Ubicaciones(int Id)
        {
            Proyecto_Ubicaciones accion = new Proyecto_Ubicaciones();
            using (var db = new iotdbContext())
            {
                accion = db.Proyecto_Ubicaciones.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion

    }
}
