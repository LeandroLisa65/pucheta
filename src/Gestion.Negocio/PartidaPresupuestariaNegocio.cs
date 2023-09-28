using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
    public class PartidaPresupuestariaNegocio
    {
        #region ABM's

        public int Insert(PartidaPresupuestaria data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch(Exception ex)
            {
                throw new Exception("Error: PartidaPresupuestariaNegocio: Insert(PartidaPresupuestaria): exception.", ex);
            }
        }

        public int Update(PartidaPresupuestaria data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: PartidaPresupuestariaNegocio: Update(PartidaPresupuestaria): exception.", ex);
            }
        }

        public int Delete(PartidaPresupuestaria data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Deleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: PartidaPresupuestariaNegocio: Delete(PartidaPresupuestaria): exception.", ex);
            }
        }

        private int GrabarCambios(PartidaPresupuestaria data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.PartidasPresupuestarias.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: PartidaPresupuestariaNegocio: GrabarCambios(PartidaPresupuestaria, EntityState): exception.", ex);
            }
            return id;
        }

        #endregion

        #region Select's

        public PartidaPresupuestaria Get_x_Id(int pId)
        {
            try
            {
                PartidaPresupuestaria oPartidaPresupuestaria = new PartidaPresupuestaria();
                using (var db = new iotdbContext())
                {
                    oPartidaPresupuestaria = db.PartidasPresupuestarias
                        .FirstOrDefault(n => n.Id == pId);
                }
                return oPartidaPresupuestaria;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: PartidaPresupuestariaNegocio: Get_x_Id(int): exception.", ex);
            }
        }

        public List<PartidaPresupuestaria> GetAll()
        {
            try
            {
                List<PartidaPresupuestaria> lPartidasPresupuestarias = new List<PartidaPresupuestaria>();
                using (var db = new iotdbContext())
                {
                    lPartidasPresupuestarias = db.PartidasPresupuestarias
                        .OrderBy(pp => pp.Codigo)
                        .ToList();
                }
                return lPartidasPresupuestarias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: PartidaPresupuestariaNegocio: Get_x_Id(int): exception.", ex);
            }
        }

        #endregion
    }
}
