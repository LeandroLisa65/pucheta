using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class IndicesNegocio
    {
        #region ABM's

        public int Insert(Indices data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: IndicesNegocio: Insert(Indices): exception.", ex);
            }
        }

        public int Update(Indices data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: IndicesNegocio: Update(Indices): exception.", ex);
            }
        }

        public int Delete(Indices data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Deleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: IndicesNegocio: Delete(Indices): exception.", ex);
            }
        }

        private int GrabarCambios(Indices data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Indices.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: IndicesNegocio: GrabarCambios(Indices, EntityState): exception.", ex);
            }
            return id;
        }

        #endregion

        #region Select's
        public List<Indices> Get_All_Indices()
        {
            List<Indices> Lista = new List<Indices>();
            using (var db = new iotdbContext())
            {
                Lista = db.Indices.OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }

        public Indices Get_One_indices(int pId, bool conIncludes)
        {
            try
            {
                Indices oIndice = new Indices();
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        oIndice = db.Indices.FirstOrDefault(n => n.Id == pId);
                    }
                    else
                    {
                        oIndice = db.Indices
                            .FirstOrDefault(n => n.Id == pId);
                    }
                }
                return oIndice;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: IndicesNegocio: Get_One_indices(int): exception.", ex);
            }
        }
        #endregion
    }
}
