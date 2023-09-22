using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using Gestion.EF;

namespace Gestion.Negocio
{
    public class ProyContr_DescuentoNegocio
    {
        #region ABM's

        public int Insert(ProyContr_Descuento data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyContr_DescuentoNegocio: Insert(ProyContr_Descuento): exception.", ex);
            }
        }

        public int Update(ProyContr_Descuento data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyContr_DescuentoNegocio: Update(ProyContr_Descuento): exception.", ex);
            }
        }

        public int Delete(ProyContr_Descuento data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Deleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyContr_DescuentoNegocio: Delete(ProyContr_Descuento): exception.", ex);
            }
        }

        private int GrabarCambios(ProyContr_Descuento data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ProyContr_Descuentos.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyContr_DescuentoNegocio: GrabarCambios(ProyContr_Descuento, EntityState): exception.", ex);
            }
            return id;
        }

        #endregion

        #region Select's

        public List<ProyContr_Descuento> Get_x_ProyectoId_ContratistaId(int ProyectoId, int ContratistaId)
        {
            try
            {
                List<ProyContr_Descuento> lProyContr_Descuentos = new List<ProyContr_Descuento>();
                using (var db = new iotdbContext())
                {
                    lProyContr_Descuentos = db.ProyContr_Descuentos
                            .Where(p => p.ProyectoId == ProyectoId &&
                                p.ContratistaId == ContratistaId)
                            .ToList();
                }
                return lProyContr_Descuentos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyContr_DescuentoNegocio: Get_x_ProyectoId(int): exception.", ex);
            }
        }

        #endregion
    }
}
