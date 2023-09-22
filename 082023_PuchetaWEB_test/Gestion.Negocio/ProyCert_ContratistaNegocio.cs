using Gestion.EF;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gestion.Negocio
{
    public class ProyCert_ContratistaNegocio
    {
        #region ABM's

        public int Insert(ProyCert_Contratista data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyCert_ContratistaNegocio: Insert(ProyCert_Contratista): exception.", ex);
            }
        }

        public int Update(ProyCert_Contratista data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_ContratistaNegocio: Update(ProyCert_Contratista): exception.", ex);
            }
        }

        public int Delete(ProyCert_Contratista data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Deleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_ContratistaNegocio: Delete(ProyCert_Contratista): exception.", ex);
            }
        }

        private int GrabarCambios(ProyCert_Contratista data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ProyCert_Contratistas.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_ContratistaNegocio: GrabarCambios(ProyCert_Contratista, EntityState): exception.", ex);
            }
            return id;
        }

        #endregion

        #region Select's

        public ProyCert_Contratista Get_x_Id(int pId, bool conIncludes)
        {
            try
            {
                ProyCert_Contratista oProyCert_Contratista = new ProyCert_Contratista();
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        oProyCert_Contratista = db.ProyCert_Contratistas
                            .Include(n => n.Contratista)
                            .FirstOrDefault(n => n.Id == pId);
                    }
                    else
                    {
                        oProyCert_Contratista = db.ProyCert_Contratistas
                            .FirstOrDefault(n => n.Id == pId);
                    }
                }
                return oProyCert_Contratista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_ContratistaNegocio: Get_x_Id(int): exception.", ex);
            }
        }

        public List<ProyCert_Contratista> Get_All()
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_Contratistas
                        .Include(pcc => pcc.ProyCertificado)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_ContratistaNegocio: Get_All(): exception.", ex);
            }
        }

        public List<ProyCert_Contratista> Get_x_lIdsProyectos_lIdsContratistas(List<int> lIdsProyectos, List<int> lIdsContratistas)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_Contratistas
                        .Include(pcc => pcc.ProyCertificado)
                        .Include(pcc => pcc.ProyCertificado.Proyecto)
                        .Include(pcc => pcc.Contratista)
                        .Where(pcc => (lIdsProyectos.Count == 0 || lIdsProyectos.Contains(pcc.ProyCertificado.ProyectoId)) &&
                            (lIdsContratistas.Count == 0 || lIdsContratistas.Contains(pcc.ContratistaId)))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCert_ContratistaNegocio: Get_x_Periodo(DateTime, DateTime): exception.", ex);
            }
        }

        public string ConsultarEstado(int ProyCertificadoId, int ContratistaId)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    int cantidadAbiertos = db.ProyCert_PDActContrs
                        .Count(pcc => pcc.ProyCertificadoId == ProyCertificadoId &&
                            pcc.ContratistaId == ContratistaId &&
                            pcc.Estado == ValoresUtiles.Estado_PCPDAC_Abierto);
                    return cantidadAbiertos > 0 ? ValoresUtiles.Estado_PCPDAC_Abierto : ValoresUtiles.Estado_PCPDAC_Cerrado;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyCert_ContratistaNegocio: ConsultarEstado(int, int): exception.", ex);
            }
        }

        public List<ProyCert_Contratista> Get_x_lIdsProyCertificados(List<int> lIdsProyCertificados)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_Contratistas
                        .Where(pcc => lIdsProyCertificados.Contains(pcc.ProyCertificadoId))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_ContratistaNegocio: Get_x_lIdsProyCertificados(List<int>): exception", ex);
            }
        }

        public ProyCert_Contratista Get_x_ProyCertId_ContratistaId(int ProyCertificadoId, int ContratistaId)
        {
            try
            {
                using var db = new iotdbContext();
                return db.ProyCert_Contratistas
                    .FirstOrDefault(pcc => pcc.ProyCertificadoId == ProyCertificadoId &&
                        pcc.ContratistaId == ContratistaId);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_ContratistaNegocio: Get_x_ProyCertId_ContratistaId(int, int): exception", ex);
            }
        }

        public List<ProyCert_Contratista> Get_x_ProyCertificadoId(int ProyCertificadoId)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_Contratistas
                        .Where(pcc => pcc.ProyCertificadoId == ProyCertificadoId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_ContratistaNegocio: Get_x_ProyCertificadoId(int): exception", ex);
            }
        }

        public List<Contratistas> GetContratistas_x_ProyectoId_CertCerrados(int ProyectoId)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_Contratistas
                        .Include(pcc => pcc.ProyCertificado)
                        .Include(pcc => pcc.Contratista)
                        .Where(pcc => pcc.ProyCertificado.ProyectoId == ProyectoId)
                        .ToList()
                        .GroupBy(pcc => pcc.ContratistaId)
                        .Select(g => g.First().Contratista)
                        .OrderBy(c => c.Nombre)
                        .ToList();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_ContratistaNegocio: GetContratistas_x_ProyectoId_CertCerrados(int): exception", ex);
            }
        }

        public int GetNuevoNumCertificado(int ProyectoId, int ContratistaId)
        {
            try
            {
                using var db = new iotdbContext();
                List<ProyCert_Contratista> lPCC = db.ProyCert_Contratistas
                    .Include(pcc => pcc.ProyCertificado)
                    .Where(pcc => pcc.ProyCertificado.ProyectoId == ProyectoId &&
                        pcc.ContratistaId == ContratistaId)
                    .ToList();
                if (lPCC.Count > 0) return lPCC.Max(pcc => pcc.NumeroCertificado) + 1;
                else return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_ContratistaNegocio: GetNuevoNumCertificado(int, int): exception", ex);
            }
        }

        #endregion
    }
}
