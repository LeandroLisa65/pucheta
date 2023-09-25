using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
     public  class ParteDiario_Actividades_FotosNegocio
    {


        #region ABM's

        public int Insert(ParteDiario_Actividades_Fotos data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(ParteDiario_Actividades_Fotos data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ParteDiario_Actividades_Fotos data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ParteDiario_Actividades_Fotos data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ParteDiario_Actividades_Fotos.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception err)
            {
                string e = err.ToString();
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        public List<ParteDiario_Actividades_Fotos> Get_All_ParteDiario_Actividades_Fotos()
        {
            List<ParteDiario_Actividades_Fotos> Lista = new List<ParteDiario_Actividades_Fotos>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Actividades_Fotos.Include(x => x.ParteDiario_Actividades).ToList();
            }

            return Lista;
        }


        public ParteDiario_Actividades_Fotos Get_One_ParteDiario_Actividades_Fotos(int Id)
        {
            ParteDiario_Actividades_Fotos accion = new ParteDiario_Actividades_Fotos();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario_Actividades_Fotos.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion
    }
}
