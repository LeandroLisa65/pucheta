using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class RolesUsuariosNegocio
    {
        #region ABM's

        public int Insert(RolesUsuarios data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(RolesUsuarios data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public bool Deletes(int UsuriosID)
        {
            bool id = false;
            try
            {
                List<RolesUsuarios> roluser = new List<RolesUsuarios>();
                using (var db = new iotdbContext())
                {
                    roluser = db.RolesUsuarios.Where(a => a.UsuariosId == UsuriosID).ToList();
                }

                if (roluser != null)
                {
                    using (var db = new iotdbContext())
                    {
                        db.RolesUsuarios.RemoveRange(roluser);
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

        public int Delete(RolesUsuarios data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(RolesUsuarios data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.RolesUsuarios.Attach(data);
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


        public int ValidaExiste(List<RolesUsuarios> roles)
        {
            int cont = 0; 
            try
            {
                List<RolesUsuarios> roluser = new List<RolesUsuarios>();
                using (var db = new iotdbContext())
                {
                    roluser = db.RolesUsuarios.Where(a => a.UsuariosId == roles[0].UsuariosId).ToList();
                }

                cont = roles.Except(roluser).ToList().Count();
               
            }
            catch (Exception)
            {
                cont = 0;
            }
            return cont;
        }

        public List<RolesUsuarios> GetRolesUsuarios(int UsuariosId)
        {
            List<RolesUsuarios> roluser = new List<RolesUsuarios>();
            using (var db = new iotdbContext())
            {
                roluser = db.RolesUsuarios.Include(x => x.Roles).Where(a => a.UsuariosId == UsuariosId).ToList();
            }

            return roluser;

        }

        public List<RolesUsuarios> Get_x_RolId(int pRolId)
        {
            List<RolesUsuarios> roluser = new List<RolesUsuarios>();
            using (var db = new iotdbContext())
            {
                roluser = db.RolesUsuarios
                    .Include(x => x.Roles)
                    .Include(x => x.Usuarios)
                    .Where(a => a.RolesId == pRolId).ToList();
            }
            return roluser;

        }

        public List<Usuarios> Get_Usuarios_x_RolId(int RolId)
        {
            try
            {
                List<RolesUsuarios> lRolesUsuarios = new List<RolesUsuarios>();
                using (var db = new iotdbContext())
                {
                    lRolesUsuarios = db.RolesUsuarios
                        .Include(ru => ru.Usuarios)
                        .Where(ru => ru.RolesId == RolId)
                        .ToList();
                }
                return lRolesUsuarios
                    .Select(ru => ru.Usuarios)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: RolesUsuariosNegocio: Get_Usuarios_x_RolId(int): exception.", ex);
            }
        }

        public List<Roles> Get_Roles_x_UsuarioId(int UsuarioId)
        {
            try
            {
                List<RolesUsuarios> lRolesUsuarios = new List<RolesUsuarios>();
                using (var db = new iotdbContext())
                {
                    lRolesUsuarios = db.RolesUsuarios
                        .Include(ru => ru.Roles)
                        .Where(ru => ru.UsuariosId == UsuarioId)
                        .ToList();
                }
                return lRolesUsuarios
                    .Select(ru => ru.Roles)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: RolesUsuariosNegocio: Get_Roles_x_UsuarioId(int): exception.", ex);
            }
        }

    }
}
