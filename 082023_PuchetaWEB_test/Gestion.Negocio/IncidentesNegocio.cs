using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
     public class IncidentesNegocio
    {

        #region ABM's

        public int Insert(Incidentes  data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Incidentes  data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        public int Delete(Incidentes  data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Incidentes  data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Incidentes .Attach(data);
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

        public List<Incidentes > Get_All_Incidentes ()
        {
            List<Incidentes > Lista = new List<Incidentes >();
            using (var db = new iotdbContext())
            {
                Lista = db.Incidentes
                .OrderBy(o => o.Nombre).ToList();
            }

            return Lista;
        }

        public Incidentes  Get_One_Incidentes (int Id)
        {
            Incidentes  accion = new Incidentes ();
            using (var db = new iotdbContext())
            {
                accion = db.Incidentes
                    .FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public List<Incidentes> Get_By_lIds(List<int> lIdsIncidentes)
        {
            List<Incidentes> lIncidentes = new List<Incidentes>();
            using (var db = new iotdbContext())
            {
                lIncidentes = db.Incidentes
                    .Where(c => lIdsIncidentes.Contains(c.Id) == true)
                    .ToList();
            }
            return lIncidentes;
        }

        #endregion

        #region Creacion de Incidentes para cuando una Actividad no tiene Item de Calidad
        public int Crear_Incidente_ItemCalidad(Incidentes oIncidente)
        {



            return 0;
        }

        #endregion
    }
}
