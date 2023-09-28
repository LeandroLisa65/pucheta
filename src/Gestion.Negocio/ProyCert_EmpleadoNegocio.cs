using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
    public class ProyCert_EmpleadoNegocio
    {
        #region ABM's

        public int Insert(ProyCert_Empleado data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyCert_EmpleadoNegocio: Insert(ProyCert_Empleado): exception.", ex);
            }
        }

        public int Update(ProyCert_Empleado data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_EmpleadoNegocio: Update(ProyCert_Empleado): exception.", ex);
            }
        }

        public int Delete(ProyCert_Empleado data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Deleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_EmpleadoNegocio: Delete(ProyCert_Empleado): exception.", ex);
            }
        }

        private int GrabarCambios(ProyCert_Empleado data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ProyCert_Empleados.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_EmpleadoNegocio: GrabarCambios(ProyCert_Empleado, EntityState): exception.", ex);
            }
            return id;
        }

        #endregion

        #region Select's

        public ProyCert_Empleado Get_x_Id(int pId, bool conIncludes)
        {
            try
            {
                ProyCert_Empleado oProyCert_Empleado = new ProyCert_Empleado();
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        oProyCert_Empleado = db.ProyCert_Empleados
                            .Include(n => n.Contratista)
                            .FirstOrDefault(n => n.Id == pId);
                    }
                    else
                    {
                        oProyCert_Empleado = db.ProyCert_Empleados
                            .FirstOrDefault(n => n.Id == pId);
                    }
                }
                return oProyCert_Empleado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_EmpleadoNegocio: Get_x_Id(int): exception.", ex);
            }
        }

        public List<ProyCert_Empleado> Get_x_ProyCertificadoId(int ProyCertificadoId, int ContratistaId)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_Empleados
                        .Where(n => n.ProyCertificadoId == ProyCertificadoId && n.ContratistaId == ContratistaId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_EmpleadoNegocio: Get_x_ProyCertificadoId(int): exception.", ex);
            }
        }

        public List<ProyCert_Empleado> Get_x_ProyectoId(List<int> lIdsProyCert)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_Empleados
                        .Where(pce => lIdsProyCert.Contains(pce.ProyCertificadoId))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_EmpleadoNegocio: Get_x_ProyectoId(int): exception", ex);
            }
        }

        #endregion
    }
}
