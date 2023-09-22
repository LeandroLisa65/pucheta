using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
      public class ParteDiario_AsistenciaNegocio
    {
        #region ABM's

        public int Insert(ParteDiario_Asistencia data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(ParteDiario_Asistencia data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ParteDiario_Asistencia data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ParteDiario_Asistencia data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ParteDiario_Asistencia.Attach(data);
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

        public List<ParteDiario_Asistencia> Get_All_ParteDiario_Asistencia()
        {
            List<ParteDiario_Asistencia> Lista = new List<ParteDiario_Asistencia>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Asistencia
                    .Include(x=> x.ContratistasId)
                    .Include(x => x.ParteDiarioId)
                    .ToList();
            }

            return Lista;
        }


        public List<ParteDiario_Asistencia> Get_All_ParteDiario_AsistenciaPA(int idParteDiario)
        {
            List<ParteDiario_Asistencia> Lista = new List<ParteDiario_Asistencia>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Asistencia.Where(x => x.ParteDiarioId == idParteDiario).ToList();
            }

            return Lista;
        }

        public List<ParteDiario_Asistencia> Get_All_ParteDiario_AsistenciaPA(int idParteDiario, bool mCompletaNombre)
        {
            List<ParteDiario_Asistencia> Lista = new List<ParteDiario_Asistencia>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Asistencia.Where(x => x.ParteDiarioId == idParteDiario).ToList();

                if (mCompletaNombre)
                {
                    foreach (var item in Lista)
                    {
                        item.SContratista = new ContratistasNegocio().Get_One_Contratistas(item.ContratistasId).Nombre;
                    }
                }
            }

            return Lista;
        }
        public ParteDiario_Asistencia Get_One_ParteDiario_Asistencia(int Id)
        {
            ParteDiario_Asistencia accion = new ParteDiario_Asistencia();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario_Asistencia.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion





    }
}
