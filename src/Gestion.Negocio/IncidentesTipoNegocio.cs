using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
      public class IncidentesTipoNegocio
    {
        #region ABM's

        public int Insert(IncidentesTipo data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(IncidentesTipo data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(IncidentesTipo data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(IncidentesTipo data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.IncidentesTipo.Attach(data);
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

        public List<IncidentesTipo> Get_All_IncidentesTipo()
        {
            List<IncidentesTipo> Lista = new List<IncidentesTipo>();
            using (var db = new iotdbContext())
            {
                Lista = db.IncidentesTipo.OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }


        public IncidentesTipo Get_One_IncidentesTipo(int Id)
        {
            IncidentesTipo accion = new IncidentesTipo();
            using (var db = new iotdbContext())
            {
                accion = db.IncidentesTipo.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion



    }
}
