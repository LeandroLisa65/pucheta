using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
     public class ParteDiario_IncidentesNegocio
    {

        #region ABM's

        public int Insert(ParteDiario_Incidentes data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(ParteDiario_Incidentes data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ParteDiario_Incidentes data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ParteDiario_Incidentes data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ParteDiario_Incidentes.Attach(data);
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

        public List<ParteDiario_Incidentes> Get_All_ParteDiario_Incidentes()
        {
            List<ParteDiario_Incidentes> Lista = new List<ParteDiario_Incidentes>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Incidentes.OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }

        public List<ParteDiario_Incidentes> Get_All_ParteDiario_IncidentesPA(int idParteDiario)
        {
            List<ParteDiario_Incidentes> Lista = new List<ParteDiario_Incidentes>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Incidentes.Where(x => x.ParteDiarioId == idParteDiario).ToList();
            }

            return Lista;
        }

        public ParteDiario_Incidentes Get_One_ParteDiario_Incidentes(int Id)
        {
            ParteDiario_Incidentes accion = new ParteDiario_Incidentes();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario_Incidentes.FirstOrDefault(m => m.Id == Id);
            }
            return accion;
        }

        public List<ParteDiario_Incidentes> Get_x_Periodo(DateTime fecDesde, DateTime fecHasta)
        {
            try
            {
                List<ParteDiario_Incidentes> lPDIncidentes = new List<ParteDiario_Incidentes>();
                using (var db = new iotdbContext())
                {
                    lPDIncidentes = db.ParteDiario_Incidentes
                        .Include(pdi => pdi.ParteDiario)
                        .Include(pdi => pdi.ParteDiario.Proyecto)
                        .Include(pdi => pdi.Incidente)
                        .Include(pdi => pdi.Contratista)
                        .Where(pdi => pdi.ParteDiario.Fecha_Creacion >= fecDesde &&
                            pdi.ParteDiario.Fecha_Creacion <= fecHasta)
                        .ToList();
                }
                return lPDIncidentes;
            }
            catch(Exception ex)
            {
                throw new Exception(
                    "Error: ParteDiario_IncidentesNegocio: Get_x_Periodo(DateTime, DateTime) exception.", ex);
            }
        }

        #endregion






    }
}
