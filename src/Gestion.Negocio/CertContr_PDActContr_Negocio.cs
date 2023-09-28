using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class CertContr_PDActContr_Negocio
    {
        #region ABM's

        public int Insert(ContrCert_PDActContr data)
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

        public int Update(ContrCert_PDActContr data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ContrCert_PDActContr data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ContrCert_PDActContr data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.CertContrs_PDActContrs.Attach(data);
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

        public List<ContrCert_PDActContr> Get_x_CertContrId(int certContrId, bool conIncludes)
        {
            List<ContrCert_PDActContr> lCertsContrs = new List<ContrCert_PDActContr>();
            try
            {
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        lCertsContrs = db.CertContrs_PDActContrs
                            .Include(ccpdac => ccpdac.PDActContr)
                            .Where(ccpdac => ccpdac.CertContrId == certContrId)
                            .ToList();
                    }
                    else
                    {
                        lCertsContrs = db.CertContrs_PDActContrs
                            .Where(ccpdac => ccpdac.CertContrId == certContrId)
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
