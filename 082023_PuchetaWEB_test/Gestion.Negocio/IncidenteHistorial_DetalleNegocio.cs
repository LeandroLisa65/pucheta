using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class IncidenteHistorial_DetalleNegocio
    {
        #region ABM's

        public int Insert(IncidentesHistorial_Detalle data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(IncidentesHistorial_Detalle data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(IncidentesHistorial_Detalle data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(IncidentesHistorial_Detalle data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.IncidentesHistorial_Detalle.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception e)
            {
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        public List<IncidentesHistorial_Detalle> Get_All_Incidentes_Historial()
        {
            List<IncidentesHistorial_Detalle> Lista = new List<IncidentesHistorial_Detalle>();
            using (var db = new iotdbContext())
            {
                Lista = db.IncidentesHistorial_Detalle.ToList();
            }

            return Lista;
        }

        public IncidentesHistorial_Detalle Get_One_Incidentes_Historial(int Id)
        {
            IncidentesHistorial_Detalle accion = new IncidentesHistorial_Detalle();
            using (var db = new iotdbContext())
            {
                accion = db.IncidentesHistorial_Detalle.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        /// <summary>
        /// Método que devuelve una lista de IncidenteHistorial_Detalle según una lista de Ids de Incidente_Historial
        /// </summary>
        /// <param name="lIdsIncidentesHistorial">Lista de IDs de InicidenteHistorial</param>
        /// <returns>List de IncidentesHistorial_Detalle</returns>
        public List<IncidentesHistorial_Detalle> Get_By_lIds(List<int> lIdsIncidentesHistorial)
        {
            List<IncidentesHistorial_Detalle> lHIDetalles = new List<IncidentesHistorial_Detalle>();
            using (var db = new iotdbContext())
            {
                lHIDetalles = db.IncidentesHistorial_Detalle
                    .Where(ihd => lIdsIncidentesHistorial.Contains(ihd.IdIncidente_Historial) == true)
                    .ToList();
            }
            return lHIDetalles;
        }

        #endregion
    }
}
