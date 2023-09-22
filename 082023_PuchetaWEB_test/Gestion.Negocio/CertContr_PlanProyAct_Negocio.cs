using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class CertContr_PLanProyAct_Negocio
    {
        #region ABM's

        public int Insert(CertContr_PlanProyAct data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public int Update(CertContr_PlanProyAct data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(CertContr_PlanProyAct data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(CertContr_PlanProyAct data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.CertContrs_PlanProyActs.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }
            return id;
        }

        #endregion

        #region SELECTS

        public List<CertContr_PlanProyAct> Get_x_CertContrId(int certContrId, bool conIncludes)
        {
            List<CertContr_PlanProyAct> lCertsContrs = new List<CertContr_PlanProyAct>();
            try
            {
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        lCertsContrs = db.CertContrs_PlanProyActs
                            .Include(ccppa => ccppa.PlanProyAct)
                            .Where(ccppa => ccppa.CertContrId == certContrId)
                            .ToList();
                    }
                    else
                    {
                        lCertsContrs = db.CertContrs_PlanProyActs
                            .Where(ccppa => ccppa.CertContrId == certContrId)
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: exception.", ex);
            }
            return lCertsContrs;
        }

        #endregion

    }
}
