using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class Calidad_ItemsNegocio
    {
        #region ABM's

        public int Insert(Calidad_Items data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Calidad_Items data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Calidad_Items data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Calidad_Items data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Calidad_Items.Attach(data);
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

        public List<Calidad_Items> Get_All_Calidad_Items()
        {
            List<Calidad_Items> Lista = new List<Calidad_Items>();
            using (var db = new iotdbContext())
            {
                Lista = db.Calidad_Items.ToList();
            }

            return Lista;
        }

        public List<Calidad_Items> Get_All_Calidad_ItemsSelect( int Id)
        {
            List<Calidad_Items> Lista = new List<Calidad_Items>();
            using (var db = new iotdbContext())
            {
                Lista = db.Calidad_Items.Where(x => x.Id == Id).ToList();
            }

            return Lista;
        }


        public Calidad_Items Get_One_Calidad_Items(int Id)
        {
            Calidad_Items accion = new Calidad_Items();
            using (var db = new iotdbContext())
            {
                accion = db.Calidad_Items.FirstOrDefault(m => m.Id == Id) ;
            }

            return accion;
        }

        #endregion
    }
}
