using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class UsuariosDireccionNegocio
    {
        #region ABM's

        public int Insert(UsuariosDireccion data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(UsuariosDireccion data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(UsuariosDireccion data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(UsuariosDireccion data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.UsuariosDireccion.Attach(data);
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

        public List<UsuariosDireccion> Get_UsuariosDireccion(int UsuariosId)
        {
            List<UsuariosDireccion> Lista = new List<UsuariosDireccion>();
            using (var db = new iotdbContext())
            {
                Lista = db.UsuariosDireccion.Where(d => d.UsuariosId == UsuariosId).ToList();
            }

            return Lista;
        }


        public UsuariosDireccion Get_One_Acciones(int Id)
        {
            UsuariosDireccion usuariosDispositivo = new UsuariosDireccion();
            using (var db = new iotdbContext())
            {
                usuariosDispositivo = db.UsuariosDireccion.FirstOrDefault(m => m.Id == Id);
            }

            return usuariosDispositivo;
        }



        #endregion
    }
}
