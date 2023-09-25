using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
    public class Archivos_AdjuntosNegocio
    {
        #region ABM's

        public int Insert(Archivos_Adjuntos data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Archivos_Adjuntos data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Archivos_Adjuntos data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Archivos_Adjuntos data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Archivos_Adjuntos.Attach(data);
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

        public List<Archivos_Adjuntos> Get_All_Archivos_Adjuntos()
        {
            List<Archivos_Adjuntos> Lista = new List<Archivos_Adjuntos>();
            using (var db = new iotdbContext())
            {
                Lista = db.Archivos_Adjuntos.OrderBy(o => o.Id).ToList();
            }

            return Lista;
        }


        public Archivos_Adjuntos Get_One_Archivos_Adjuntos(int Id)
        {
            Archivos_Adjuntos accion = new Archivos_Adjuntos();
            using (var db = new iotdbContext())
            {
                accion = db.Archivos_Adjuntos.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        

        #endregion
    }
}
