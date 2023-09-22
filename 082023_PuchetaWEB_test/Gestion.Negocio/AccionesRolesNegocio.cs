using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class AccionesRolesNegocio
    {

        #region ABM's

        public int Insert(AccionesRoles data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(AccionesRoles data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(AccionesRoles data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(AccionesRoles data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.AccionesRoles.Attach(data);
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

        public bool Deletes(int IdRol)
        {
            bool id = false;
            try
            {
                List<AccionesRoles> accrol = new List<AccionesRoles>();
                using (var db = new iotdbContext())
                {
                    accrol = db.AccionesRoles.Where(a => a.RolesId == IdRol).Include(x => x.Acciones).ToList();
                }

                if (accrol != null)
                {
                    using (var db = new iotdbContext())
                    {
                        db.AccionesRoles.RemoveRange(accrol);
                        db.SaveChanges();
                        id = true;
                    }
                }
            }
            catch (Exception)
            {
                id = false;
            }
            return id;
        }

        public List<AccionesRoles> Get_AccionesRoles(int RolesId)
        {
            List<AccionesRoles> Lista = new List<AccionesRoles>();
            using (var db = new iotdbContext())
            {
                Lista = db.AccionesRoles.Where(a => a.RolesId == RolesId).ToList();
            }

            return Lista;
        }

        public List<AccionesRoles> Get_By_lIds(List<int> lIdsRoles)
        {
            List<AccionesRoles> lAccRoles = new List<AccionesRoles>();
            using (var db = new iotdbContext())
            {
                lAccRoles = db.AccionesRoles
                    .Where(c => lIdsRoles.Contains(c.RolesId) == true)
                    .ToList();
            }
            return lAccRoles;
        }


    }
}
