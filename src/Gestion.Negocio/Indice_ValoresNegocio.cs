using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class Indice_ValoresNegocio
    {
        #region ABM's

        public int Insert(Indice_Valores data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Indice_ValoresNegocio: Insert(Indices): exception.", ex);
            }
        }

        public int Update(Indice_Valores data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Indice_ValoresNegocio: Update(Indices): exception.", ex);
            }
        }

        public int Delete(Indice_Valores data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Deleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Indice_ValoresNegocio: Delete(Indices): exception.", ex);
            }
        }

        private int GrabarCambios(Indice_Valores data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Indice_Valores.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Indice_ValoresNegocio: GrabarCambios(Indices, EntityState): exception.", ex);
            }
            return id;
        }

        #endregion

        #region Select's
        public List<Indice_Valores> Get_All_Indice_Valores()
        {
            List<Indice_Valores> Lista = new List<Indice_Valores>();
            using (var db = new iotdbContext())
            {
                Lista = db.Indice_Valores.OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }

        public Indice_Valores Get_One_Indice_Valores(long pId)
        {
            try
            {
                Indice_Valores oObj = new Indice_Valores();
                using (var db = new iotdbContext())
                {
                    oObj = db.Indice_Valores.FirstOrDefault(n => n.Id == pId);
                }
                return oObj;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Indice_ValoresNegocio: Get_One_indices(int): exception.", ex);
            }
        }
        #endregion
    }
}
