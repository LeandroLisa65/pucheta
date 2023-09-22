using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class UsuariosNegocio
    {
        #region ABM's

        public int Insert(Usuarios data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public bool Inserts(List<Usuarios> data)
        {
            bool id = false;
            try
            {
                using (var db = new iotdbContext())
                {                    
                    db.AddRange(data);
                    db.SaveChanges();
                    id = true;
                }
            }
            catch (Exception)
            {
                id = false;
            }
            return id;
        }

        public int Update(Usuarios data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Usuarios data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        public bool Deletes(List<Usuarios> data)
        {
            bool id = false;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.RemoveRange(data);
                    db.SaveChanges();
                    id = true;
                }
            }
            catch (Exception)
            {
                id = false;
            }
            return id;
        }

        private int GrabarCambios(Usuarios data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {                    
                    db.Usuarios.Attach(data);
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

        #region Web

        public Usuarios Get_Usuario_Login(string NUsuario, string Password)
        {
            Usuarios usuario = new Usuarios();
            /*using (var ctx = new iotdbContext())
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }*/
            using (var db = new iotdbContext())
            {
                try
                {
                    var fake = db.Tests.FirstOrDefault();
                    var admin = db.Indices.ToList();
                    usuario = db.Usuarios
                        .Include(x => x.Roles)
                        .FirstOrDefault(u => u.NUsuario == NUsuario && u.Clave == Password &&
                            u.Activo && !u.Borrado);
                    if (usuario != null && usuario.Roles != null && usuario.Roles.Count > 0)
                    {
                        return usuario;
                    }
                    else
                    {
                        usuario = null;
                        return usuario;
                    }
                }
                catch (TypeInitializationException ex)
                {
                    usuario = null;
                    return usuario;
                }
            }            
        }

        public List<Usuarios> Get_Usuarios()
        {
            List<Usuarios> Lista = new List<Usuarios>();
            using (var db = new iotdbContext())
            {
                Lista = db.Usuarios.ToList();
            }
            return Lista;
        }

        public List<Usuarios> Get_UsuariosWithRoles()
        {
            List<Usuarios> Lista = new List<Usuarios>();
            using (var db = new iotdbContext())
            {
                Lista = db.Usuarios.Include(x => x.Roles).ToList();
            }
            return Lista;
        }

        public Usuarios Get_Usuario(int IdUsuario)
        {
            Usuarios usuario = new Usuarios();
            using (var db = new iotdbContext())
            {
                usuario = db.Usuarios.Include(x => x.UsuariosDireccion).Include(x => x.UsuariosDispositivo).Include(x=>x.Roles).FirstOrDefault(u => u.Id == IdUsuario);
            }
            return usuario;

        }
        #endregion

        #region Mobile


        #endregion







        #endregion
    }
}
