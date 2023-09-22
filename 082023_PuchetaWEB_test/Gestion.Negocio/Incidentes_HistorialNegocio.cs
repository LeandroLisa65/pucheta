using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class Incidentes_HistorialNegocio
    {
        #region ABM's

        public int Insert(Incidentes_Historial data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Incidentes_Historial data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Incidentes_Historial data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Incidentes_Historial data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Incidentes_Historial.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception err)
            {

                id = 0;
            }
            return id;
        }

        public void InformarIncidentesVencidos()
        {
            try
            {
                List<Incidentes_Historial> lIncidentes = new Incidentes_HistorialNegocio()
                    .Get_Vencidos();
                if (lIncidentes.Count > 0)
                {
                    lIncidentes.ForEach(i =>
                    {
                        List<Usuarios> lUsuarios = new RolesUsuariosNegocio().Get_Usuarios_x_RolId(i.AreaId);
                        string emails = string.Empty;
                        lUsuarios.ForEach(u =>
                        {
                            if (!emails.Contains(u.Email.Trim()))
                            {
                                emails += u.Email + ";";
                            }
                        });
                        emails.Trim(';');
                        string Estado = ValoresUtiles.Get_lEstados_IncidenteHistorial()
                            .Find(e => e.Key == i.EstadoId).Value;
                        string mensaje = "Incidente ID " + i.Id + " se encuentra en estado " +
                            Estado + " y su fecha de cierre debía ser el día " +
                            i.Fecha_Cierre.ToString(ValoresUtiles.Formato_dd_MM_yyyy) + ".";
                        EnviaEmailNegocio.EnviarEmail_Async(emails, "Incidente Vencido", mensaje, false)
                        .ConfigureAwait(false);
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Incidente_HistorialNegocio: exception.", ex);
            }
        }

        #endregion

        #region Select's

        public List<Incidentes_Historial> Get_All_Incidentes_Historial()
        {
            List<Incidentes_Historial> Lista = new List<Incidentes_Historial>();
            using (var db = new iotdbContext())
            {
                Lista = db.Incidentes_Historial.ToList();
            }

            return Lista;
        }

        public List<Incidentes_Historial> Get_All_Incidentes_HistorialPD(int IdParteDiario)
        {
            List<Incidentes_Historial> Lista = new List<Incidentes_Historial>();
            using (var db = new iotdbContext())
            {
                Lista = db.Incidentes_Historial.Where(x => x.ParteDiarioId == IdParteDiario).ToList();
            }

            return Lista;
        }

        public Incidentes_Historial Get_One_Incidentes_Historial(int Id)
        {
            Incidentes_Historial accion = new Incidentes_Historial();
            using (var db = new iotdbContext())
            {
                accion = db.Incidentes_Historial.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public List<Incidentes_Historial> Get_Vencidos()
        {
            try
            {
                List<Incidentes_Historial> Lista = new List<Incidentes_Historial>();
                using (var db = new iotdbContext())
                {
                    Lista = db.Incidentes_Historial
                        .Where(i => i.Fecha_Cierre <= DateTime.Now &&
                            i.EstadoId != ValoresUtiles.IdEstado_IncHist_Cerrado &&
                            i.EstadoId != ValoresUtiles.IdEstado_IncHist_Cancelado)
                        .ToList();
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Incidente_HistorialNegocio: Get_Vencidos: exception.", ex);
            }
        }

        /// <summary>
        /// Método que devuelve una lista de Incidentes_Historial según un periodo (FecDesde y FecHasta)
        /// y un ProyectoId determinados.
        /// </summary>
        /// <param name="FecDesde">Fecha Desde</param>
        /// <param name="FecHasta">Fecha Hasta</param>
        /// <param name="ProyectoId">Id del Proyecto</param>
        /// <returns>List Incidentes_Historial</returns>
        public List<Incidentes_Historial> Get_x_Periodo_ProyectoId(DateTime FecDesde, DateTime FecHasta, int ProyectoId)
        {
            try 
            {
                FecDesde = new DateTime(FecDesde.Year, FecDesde.Month, FecDesde.Day);
                FecHasta = new DateTime(FecHasta.Year, FecHasta.Month, FecHasta.Day);
                FecHasta = FecHasta.AddDays(1).AddTicks(-1);
                using (var db = new iotdbContext())
                {
                    return db.Incidentes_Historial
                        .Where(i => FecDesde <= i.Creacion_Fecha &&
                        i.Creacion_Fecha <= FecHasta &&
                        i.ProyectoId == ProyectoId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        #endregion

        #region IncidentesHistorial Estado

        public string IncidenteHistorial_EstadoNombre(int mId)
        {
            string mEstado = "";
            switch (mId)
            {
                case 1:
                    mEstado = "Abierto";
                    break;
                case 2:
                    mEstado = "Tratamiento";
                    break;
                case 3:
                    mEstado = "Sol Propuesta";
                    break;
                case 4:
                    mEstado = "Validacion";
                    break;
                case 50:
                    mEstado = "Cerrado";
                    break;
                case 99:
                    mEstado = "Cancelado";
                    break;
                default:
                    mEstado = "Cancelado";
                    break;
            }
            return mEstado;
        }

        #endregion

    }
}
