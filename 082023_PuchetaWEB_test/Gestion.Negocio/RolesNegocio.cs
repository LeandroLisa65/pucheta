using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class RolesNegocio
    {
        #region ABM's

        public int Insert(Roles data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Roles data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Roles data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Roles data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Roles.Attach(data);
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

        public List<Roles> Get_All_Roles()  
        {
            List<Roles> Lista = new List<Roles>();

            try
            {
                using (var db = new iotdbContext())
                {
                    Lista = db.Roles.Include(x => x.Acciones).OrderBy(o => o.Id).ToList();
                }
            }
            catch (Exception err)
            {
                var s = err.ToString();
            }
            
            return Lista;
        }
       
        public Roles Get_One_Roles(int Id)
        {
            Roles rol = new Roles();
            using (var db = new iotdbContext())
            {
                rol = db.Roles.Include(x => x.Acciones).FirstOrDefault(m => m.Id == Id);
            }
            return rol;
        }

        public List<Roles> Get_By_lIds(List<int> lIdsRoles)
        {
            List<Roles> lRoles = new List<Roles>();
            using (var db = new iotdbContext())
            {
                lRoles = db.Roles
                    .Where(c => lIdsRoles.Contains(c.Id) == true)
                    .ToList();
            }
            return lRoles;
        }

        #endregion
    }
}
