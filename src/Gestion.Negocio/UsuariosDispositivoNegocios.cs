using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class UsuariosDispositivoNegocios
    {
        #region ABM's

        public int Insert(UsuariosDispositivo data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(UsuariosDispositivo data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(UsuariosDispositivo data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(UsuariosDispositivo data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.UsuariosDispositivo.Attach(data);
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

        public List<UsuariosDispositivo> Get_UsuariosDispositivo(int UsuariosId)
        {
            List<UsuariosDispositivo> Lista = new List<UsuariosDispositivo>();
            using (var db = new iotdbContext())
            {
                Lista = db.UsuariosDispositivo.Where(d => d.UsuariosId == UsuariosId).ToList();
            }

            return Lista;
        }


        public UsuariosDispositivo Get_One_Dispositivo(int Id)
        {
            UsuariosDispositivo usuariosDispositivo = new UsuariosDispositivo();
            using (var db = new iotdbContext())
            {
                usuariosDispositivo = db.UsuariosDispositivo.FirstOrDefault(m => m.Id == Id);
            }

            return usuariosDispositivo;
        }



        #endregion
    }
}
