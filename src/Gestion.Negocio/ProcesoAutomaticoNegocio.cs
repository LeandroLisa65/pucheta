using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
    public class ProcesoAutomaticoNegocio
    {
        #region ABM's

        public int Insert(ProcesoAutomatico data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch(Exception ex)
            {
                throw new Exception("Error: RegistroAutomaticoNegocio: Insert(RegistroAutomatico): exception.", ex);
            }
        }

        public int Update(ProcesoAutomatico data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: RegistroAutomaticoNegocio: Update(RegistroAutomatico): exception.", ex);
            }
        }

        public int Delete(ProcesoAutomatico data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Deleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: RegistroAutomaticoNegocio: Delete(RegistroAutomatico): exception.", ex);
            }
        }

        private int GrabarCambios(ProcesoAutomatico data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ProcesosAutomaticos.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: RegistroAutomaticoNegocio: GrabarCambios(RegistroAutomatico, EntityState): exception.", ex);
            }
            return id;
        }

        #endregion

        #region Select's

        public ProcesoAutomatico Get_x_Id(int pId, bool conIncludes)
        {
            try
            {
                ProcesoAutomatico oRegistroAutomatico = new ProcesoAutomatico();
                using (var db = new iotdbContext())
                {
                    oRegistroAutomatico = db.ProcesosAutomaticos
                        .FirstOrDefault(n => n.Id == pId);
                }
                return oRegistroAutomatico;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: RegistroAutomaticoNegocio: Get_x_Id(int): exception.", ex);
            }
        }

        public ProcesoAutomatico Get_x_Fecha_Motivo(DateTime Fecha, string Motivo)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProcesosAutomaticos
                        .Where(pa => pa.FecCreacion.Date == Fecha.Date && pa.Motivo == Motivo)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: RegistroAutomaticoNegocio: Get_x_Fecha_Motivo(DateTime, string): exception.", ex);
            }
        }

        #endregion
    }
}
