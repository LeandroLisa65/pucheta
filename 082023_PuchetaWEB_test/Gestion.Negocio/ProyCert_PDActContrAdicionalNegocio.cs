using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gestion.Negocio
{
    public class ProyCert_PDActContr_AdicionalNegocio
    {
        #region ABM's

        public int Insert(ProyCert_PDActContr_Adicional data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyCert_PDActContr_AdicionalNegocio: Insert: exception", ex);
            }
        }

        public int Update(ProyCert_PDActContr_Adicional data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ProyCert_PDActContr_Adicional data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ProyCert_PDActContr_Adicional data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ProyCert_PDActContr_Adicionales.Attach(data);
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

        public List<ProyCert_PDActContr_Adicional> Get_x_ProyCertId(int ProyCertificadoId)
        {
            try
            {
                List<ProyCert_PDActContr_Adicional> lPCPDAC_As = new List<ProyCert_PDActContr_Adicional>();
                using (var db = new iotdbContext())
                {
                    lPCPDAC_As = db.ProyCert_PDActContr_Adicionales
                        .Where(cc => ProyCertificadoId == 0 || cc.ProyCertificadoId == ProyCertificadoId)
                        .ToList();
                }
                return lPCPDAC_As;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCertificadoNegocio: Get_UltimoCertificado_x_ProyectoId(int): exception.", ex);
            }
        }
        public List<ProyCert_PDActContr_Adicional> Get_x_ProyectoId_FecDesde_FecHasta(
            int ProyectoId, DateTime FecDesde, DateTime FecHasta)
        {
            try
            {
                List<ProyCertificado> lProyCertificados = new ProyCertificadoNegocio()
                    .Get_x_ProyectoId(ProyectoId);
                List<int> lIdsProyCertificados = lProyCertificados.Select(pc => pc.Id).ToList();
                List<ProyCert_PDActContr_Adicional> lPCPDAC_As = new List<ProyCert_PDActContr_Adicional>();
                using (var db = new iotdbContext())
                {
                    lPCPDAC_As = db.ProyCert_PDActContr_Adicionales
                        .Where(pcpdaca => 
                            lIdsProyCertificados.Contains(pcpdaca.ProyCertificadoId) == true &&
                            FecDesde <= pcpdaca.FecCreacion && pcpdaca.FecCreacion <= FecHasta)
                        .ToList();

                }
                return lPCPDAC_As;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PDActContr_AdicionalNegocio: Get_x_ProyectoId_FecDesde_FecHasta(int, DateTime, DateTime)", ex);
            }
        }

        public List<ProyCert_PDActContr_Adicional> Get_x_ProyectoId(List<int> lIdsProyCert)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_PDActContr_Adicionales
                        .Where(pcpdaca => lIdsProyCert.Contains(pcpdaca.ProyCertificadoId))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PDActContr_AdicionalNegocio: Get_x_ProyectoId(int): exception", ex);
            }
        }

        #endregion
    }
}
