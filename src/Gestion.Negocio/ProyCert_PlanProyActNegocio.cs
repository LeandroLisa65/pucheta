using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gestion.Negocio
{
    public class ProyCert_PlanProyActNegocio
    {
        #region ABM's

        public int Insert(ProyCert_PlanProyAct data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Added);
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        public int Update(ProyCert_PlanProyAct data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ProyCert_PlanProyAct data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ProyCert_PlanProyAct data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ProyCert_PlanProyActs.Attach(data);
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
        public ProyCert_PlanProyAct Get_x_Id(int Id)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_PlanProyActs
                        .FirstOrDefault(pcppa => pcppa.Id == Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PlanProyActNegocio: Get_x_Id(int): exception", ex);
            }
        }

        public ProyCert_PlanProyAct Get_x_ProyCertId_PlanProyActId(int ProyCertId, int PlanProyActId)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_PlanProyActs.FirstOrDefault(pcppa =>
                        pcppa.ProyCertificadoId == ProyCertId &&
                        pcppa.PlanProyActId == PlanProyActId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PlanProyActNegocio: Get_x_ProyCertId_PlanProyActId(int, int): exception", ex);
            }
        }

        public List<ProyCert_PlanProyAct> Get_x_ProyCertId(int ProyCertId)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_PlanProyActs
                        .Where(pcppa => pcppa.ProyCertificadoId == ProyCertId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PlanProyActNegocio: Get_x_ProyCertId(int): exception", ex);
            }
        }

        public List<ProyCert_PlanProyAct> Get_x_ProyectoId(List<int> lIdsProyCert)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCert_PlanProyActs
                        .Where(pcppa => lIdsProyCert.Contains(pcppa.ProyCertificadoId))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCert_PlanProyActNegocio: Get_x_ProyectoId(int): exception", ex);
            }
        }

        #endregion
    }
}
