using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class UsuariosLogNegocio
    {
        #region ABM's

        public void Insert(int IdUsuario, string Detalle, int IdAccion, string IP )
        {

            UsuariosLog log = new UsuariosLog();
            log.Id = 0;
            log.IdUsuarios = IdUsuario;
            log.Ip = IP;
            log.Fecha = DateTime.Now;
            log.Detalle = Detalle;
            log.AccionesId = IdAccion;
            InsertData(log);
            
        }

        private int InsertData(UsuariosLog data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        private int GrabarCambios(UsuariosLog data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.UsuariosLog.Attach(data);
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

        public List<UsuariosLog> Get_UsuariosLog(int IdUsuario)
        {
            List<UsuariosLog> Lista = new List<UsuariosLog>();

            using (var db = new iotdbContext())
            {
                Lista = db.UsuariosLog.Where(u => u.IdUsuarios == IdUsuario).ToList();
            }
            return Lista;
        }


        #endregion
    }
}
