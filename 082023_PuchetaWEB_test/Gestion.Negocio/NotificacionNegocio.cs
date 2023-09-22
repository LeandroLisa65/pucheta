using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
    public class NotificacionNegocio
    {
        #region ABM's

        public int Insert(Notificacion data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch(Exception ex)
            {
                throw new Exception("Error: NotificacionNegocio: Insert(Notificacion): exception.", ex);
            }
        }

        public int Update(Notificacion data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: NotificacionNegocio: Update(Notificacion): exception.", ex);
            }
        }

        public int Delete(Notificacion data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Deleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: NotificacionNegocio: Delete(Notificacion): exception.", ex);
            }
        }

        private int GrabarCambios(Notificacion data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Notificaciones.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: NotificacionNegocio: GrabarCambios(Notificacion, EntityState): exception.", ex);
            }
            return id;
        }

        #endregion

        #region Select's

        public Notificacion Get_x_Id(int pId, bool conIncludes)
        {
            try
            {
                Notificacion oNotificacion = new Notificacion();
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        oNotificacion = db.Notificaciones
                            .Include(n => n.UsuarioEmisor)
                            .Include(n => n.UsuarioReceptor)
                            .FirstOrDefault(n => n.Id == pId);
                    }
                    else
                    {
                        oNotificacion = db.Notificaciones
                            .FirstOrDefault(n => n.Id == pId);
                    }
                }
                return oNotificacion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: NotificacionNegocio: Get_x_Id(int): exception.", ex);
            }
        }

        public List<Notificacion> GetUltimas_x_UsuarioReceptorId(int pUsuarioReceptorId, bool conIncludes)
        {
            try
            {
                List<Notificacion> lNotificaciones = new List<Notificacion>();
                DateTime fechaLimite = DateTime.Now.AddDays(-7);
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        lNotificaciones = db.Notificaciones
                            .Include(n => n.UsuarioEmisor)
                            .Include(n => n.UsuarioReceptor)
                            .Where(n => n.UsuarioReceptorId == pUsuarioReceptorId &&
                                n.FecEmision >= fechaLimite
                            )
                            .ToList();
                    }
                    else
                    {
                        lNotificaciones = db.Notificaciones
                            .Where(n => n.UsuarioReceptorId == pUsuarioReceptorId &&
                                n.FecEmision >= fechaLimite
                            )
                            .ToList();
                    }
                }
                return lNotificaciones;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: NotificacionNegocio: GetUltimas_x_UsuarioReceptorId(int): exception.", ex);
            }
        }

        #endregion
    }
}
