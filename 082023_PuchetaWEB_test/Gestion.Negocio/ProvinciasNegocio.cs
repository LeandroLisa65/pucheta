using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class ProvinciasNegocio
    {
        #region ABM's

        public int Insert(Provincias data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Provincias data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Provincias data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Provincias data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Provincias.Attach(data);
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

        public List<Provincias> Get_Provincias()
        {
            List<Provincias> Lista = new List<Provincias>();
            using (var db = new iotdbContext())
            {
                Lista = db.Provincias.OrderBy(o=>o.Provincia).Include(x => x.Localidades).ToList();
            }

            return Lista;
        }


        public Provincias Get_One_Acciones(int Id)
        {
            Provincias provincias = new Provincias();
            using (var db = new iotdbContext())
            {
                provincias = db.Provincias.FirstOrDefault(m => m.Id == Id);
            }

            return provincias;
        }



        #endregion
    }
}
