using Gestion.EF;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Negocio
{
    public class EmailNegocio
    {
        #region ABM's

        public int Insert(Email data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: EmailNegocio: Insert(Email): exception.", ex);
            }
        }

        public int Update(Email data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: EmailNegocio: Update(Email): exception.", ex);
            }
        }

        public int Delete(Email data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Deleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: EmailNegocio: Delete(Email): exception.", ex);
            }
        }

        private int GrabarCambios(Email data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Emails.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: EmailNegocio: GrabarCambios(Email, EntityState): exception.", ex);
            }
            return id;
        }

        #endregion

        #region Select's

        public Email Get_x_Id(int pId)
        {
            try
            {
                Email oEmail = new Email();
                using (var db = new iotdbContext())
                {
                    oEmail = db.Emails
                            .FirstOrDefault(n => n.Id == pId);
                }
                return oEmail;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: EmailNegocio: Get_x_Id(int): exception.", ex);
            }
        }

        public DateTime Get_MaxFechaCreacion()
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    if (db.Emails.Count() > 0)
                        return db.Emails.Max(e => e.FecCreacion);
                    else return DateTime.MinValue;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error: EmailNegocio: Get_MaxFecCreacion(): exception.", ex);
            }
        }

        #endregion
    }
}
