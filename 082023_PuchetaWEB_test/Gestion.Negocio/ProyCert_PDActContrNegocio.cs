using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gestion.Negocio
{
    public class ProyCert_PDActContrNegocio
    {
        #region ABM's

        public int Insert(ProyCert_PDActContr data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }
                public int Update(ProyCert_PDActContr data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public int Delete(ProyCert_PDActContr data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ProyCert_PDActContr data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ProyCert_PDActContrs.Attach(data);
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

        #region Select's
        public ProyCert_PDActContr Get_x_Id(int ProyCert_PDActContrId)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_PDActContrs
                        .FirstOrDefault(pcpdac => pcpdac.Id == ProyCert_PDActContrId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PDActContrNegocio: Get_x_Id(int): exception.", ex);
            }
        }

        public List<ProyCert_PDActContr> Get_x_ProyCertificadoId_ContratistaId(int ProyCertificadoId, int ContratistaId)
        {
            try
            {
                List<ProyCert_PDActContr> lPCPDACs = new List<ProyCert_PDActContr>();
                using (var db = new iotdbContext())
                {
                    lPCPDACs = db.ProyCert_PDActContrs
                        .Where(pcpdac => pcpdac.ProyCertificadoId == ProyCertificadoId &&
                            pcpdac.ContratistaId == ContratistaId
                        )
                        .ToList();
                }
                return lPCPDACs;
            }
            catch(Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PDActContrNegocio: Get_x_ProyCertificadoId_ContratistaId(int, int): exception.", ex);
            }
        }
        
        public List<ProyCert_PDActContr> Get_x_ProyCertificadoId(int ProyCertificadoId)
        {
            try
            {
                List<ProyCert_PDActContr> lPCPDACs = new List<ProyCert_PDActContr>();
                using (var db = new iotdbContext())
                {
                    lPCPDACs = db.ProyCert_PDActContrs
                        .Where(pcpdac => pcpdac.ProyCertificadoId == ProyCertificadoId)
                        .ToList();
                }
                return lPCPDACs;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PDActContrNegocio: Get_x_ProyCertificadoId(int): exception.", ex);
            }
        }

        /// <summary>
        /// Método que devuelve una lista de ProyCertPDActContr según una lista de Ids de ParteDiario_Actividad_Contratista
        /// </summary>
        /// <param name="lIdsPDACs">Lista de IDs de ParteDiario_Actividad_Contratista</param>
        /// <returns>List de ProyCertPDActContr</returns>
        public List<ProyCert_PDActContr> Get_x_lIdsPDACs(List<int> lIdsPDACs)
        {
            List<ProyCert_PDActContr> lPCPDACs = new List<ProyCert_PDActContr>();
            using (var db = new iotdbContext())
            {
                lPCPDACs = db.ProyCert_PDActContrs
                    .Where(ihd => lIdsPDACs.Contains(ihd.PDActContrId) == true)
                    .ToList();
            }
            return lPCPDACs;
        }

        public List<ProyCert_PDActContr> Get_x_ProyCertificadoId_ContratistaId_PlanProyActId(
            int ProyCertificadoId, int ContratistaId, int PlanProyActId)
        {
            try
            {
                List<ProyCert_PDActContr> lPCPDACs = new List<ProyCert_PDActContr>();
                using (var db = new iotdbContext())
                {
                    lPCPDACs = db.ProyCert_PDActContrs
                        .Where(pcpdac => 
                            pcpdac.ProyCertificadoId == ProyCertificadoId &&
                            pcpdac.ContratistaId == ContratistaId && 
                            pcpdac.PlanProyActId == PlanProyActId
                        )
                        .ToList();
                }
                return lPCPDACs;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PDActContrNegocio: Get_x_ProyCertificadoId_ContratistaId_PlanProyActId(int, int, int): exception.", ex);
            }
        }
        
        public List<ProyCert_PDActContr> Get_x_ProyectoId(List<int> lIdsProyCert)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_PDActContrs
                        .Where(pcpdac => lIdsProyCert.Contains(pcpdac.ProyCertificadoId))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PDActContrNegocio: Get_x_ProyectoId(int): exception", ex);
            }
        }

        #endregion
    }
}
