using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gestion.Negocio
{
    public class Certificado_Contratista_Negocio
    {
        #region ABM's

        public int Insert(ContrCertificado data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(ContrCertificado data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ContrCertificado data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ContrCertificado data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Certificados_Contratistas.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception)
            {
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        public ContrCertificado Get_x_Id(int CertContrId, bool conIncludes)
        {
            ContrCertificado oCertContr = new ContrCertificado();
            try
            {
                using (var db = new iotdbContext())
                {
                    if(conIncludes)
                        oCertContr = db.Certificados_Contratistas
                            .Include(cc => cc.Contratista)
                            .Include(cc => cc.Proyecto)
                            .Include(cc => cc.lCertContrs_PDActContrs)
                                .ThenInclude(ccpdac => ccpdac.PDActContr)
                                .ThenInclude(pdac => pdac.ParteDiario_Actividades)
                            .Include(cc => cc.lCertContrs_PlanProyActs)
                                .ThenInclude(ccpdac => ccpdac.PlanProyAct)
                                .ThenInclude(ppa => ppa.Planificacion_Actividades)
                            .Include(cc => cc.lCertContrs_PlanProyActs)
                                .ThenInclude(ccpdac => ccpdac.PlanProyAct)
                                .ThenInclude(ppa => ppa.Proyecto_Ubicaciones)
                            .FirstOrDefault(cc => cc.Id == CertContrId);
                    else
                        oCertContr = db.Certificados_Contratistas
                            .FirstOrDefault(cc => cc.Id == CertContrId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ex.", ex);
            }
            return oCertContr;
        }

        public List<ContrCertificado> Get_x_Periodo(DateTime fecDesde, DateTime fecHasta, bool conInclude)
        {
            List<ContrCertificado> lCertContratistas = new List<ContrCertificado>();
            try
            {
                using (var db = new iotdbContext())
                {
                    lCertContratistas = this.Get_x_Periodo_ContrId_ProyId(
                         0, 0, DateTime.Now.AddMonths(-6), DateTime.Now, true);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error: exception.", ex);
            }
            return lCertContratistas;
        }

        public List<ContrCertificado> Get_x_Periodo_ContrId_ProyId(
            int ContratistaId, int ProyectoId, DateTime fecDesde, DateTime fecHasta, bool conInclude)
        {
            List<ContrCertificado> lCertContratistas = new List<ContrCertificado>();
            try
            {
                using (var db = new iotdbContext())
                {
                    if (conInclude)
                    {
                        lCertContratistas = db.Certificados_Contratistas
                            .Include(cc => cc.Contratista)
                            .Include(cc => cc.Proyecto)
                            .Where(cc => fecDesde <= cc.FecDesde && cc.FecHasta <= fecHasta &&
                                (ContratistaId == 0 || cc.ContratistaId == ContratistaId) &&
                                (ProyectoId == 0 || cc.ProyectoId == ProyectoId))
                            .OrderByDescending(cc => cc.FecRegistro)
                            .ThenByDescending(cc=>cc.Id)
                            .ToList();
                    }
                    else
                    {
                        lCertContratistas = db.Certificados_Contratistas
                            .Where(cc => fecDesde <= cc.FecDesde && cc.FecHasta <= fecHasta &&
                                (ContratistaId == 0 || cc.ContratistaId == ContratistaId) &&
                                (ProyectoId == 0 || cc.ProyectoId == ProyectoId))
                            .OrderByDescending(cc => cc.FechaRegistro)
                            .ThenByDescending(cc => cc.Id)
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: exception.", ex);
            }
            return lCertContratistas;
        }

        public ContrCertificado Get_Ultimo_x_ContratistaId_ProyectoId(
            int ContratistaId, int ProyectoId, bool conLista)
        {
            List<ContrCertificado> lCertContratistas = new List<ContrCertificado>();
            ContrCertificado oCertContr = new ContrCertificado();
            try
            {
                using (var db = new iotdbContext())
                {
                    lCertContratistas = db.Certificados_Contratistas
                        .Where(cc => cc.ContratistaId == ContratistaId && cc.ProyectoId == ProyectoId)
                        .ToList();
                }
                lCertContratistas = OrdenarLista(lCertContratistas, new List<ContrCertificado>());
                if (lCertContratistas.Count > 0)
                {
                    oCertContr = lCertContratistas.Last();
                    if (conLista)
                    {
                        oCertContr = this.Get_x_Id(oCertContr.Id, true);
                    }
                }
                else oCertContr = null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ex.", ex);
            }
            return oCertContr;
        }

        private List<ContrCertificado> OrdenarLista(
            List<ContrCertificado> lCertContratistas, List<ContrCertificado> lCertContrOrdenadas)
        {
            if (lCertContrOrdenadas.Count == 0)
            {
                ContrCertificado oCC = lCertContratistas.Find(cc => cc.CertContrAnteriorId == 0);
                if (oCC != null)
                {
                    lCertContrOrdenadas.Add(oCC);
                    return OrdenarLista(lCertContratistas, lCertContrOrdenadas);
                }
                else return new List<ContrCertificado>();
            }
            else
            {
                ContrCertificado oCCAnterior = lCertContrOrdenadas.Last();
                ContrCertificado oCC = lCertContratistas
                    .Find(cc => cc.CertContrAnteriorId == oCCAnterior.Id);
                if (oCC != null)
                {
                    lCertContrOrdenadas.Add(oCC);
                    return OrdenarLista(lCertContratistas, lCertContrOrdenadas);
                }
                else return lCertContrOrdenadas;
            }
        }

        public List<ParteDiario_Actividades> Get_lPDAs_x_CertContrId(int certContrId)
        {
            List<ParteDiario_Actividades> lPDAs = new List<ParteDiario_Actividades>();
            try
            {
                List<ContrCert_PDActContr> lCCPPAs = new CertContr_PDActContr_Negocio()
                    .Get_x_CertContrId(certContrId, true);
                List<int> lIdsPPAs = lCCPPAs
                    .Select(ccppa => ccppa.PDActContrId)
                    .ToList();
                lPDAs = new ParteDiario_ActividadesNegocio()
                    .Get_X_lIdsPlanProyActividades(lIdsPPAs);
                List<int> lIdsPDAs = lPDAs
                    .Select(pda => pda.Id)
                    .ToList();
                List<ParteDiario_Actividades_Contratista> lPDACs = new ParteDiario_Actividades_ContratistaNegocio()
                    .Get_X_lIdsPrtDiaActividades(lIdsPDAs);
                lPDAs.ForEach(pda => pda.ParteDiario_Actividades_Contratista = lPDACs
                    .Where(pdac => pdac.ParteDiario_ActividadesId == pda.Id)
                    .ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Error: exception.", ex);
            }
            return lPDAs;
        }

        #endregion
    }
}
