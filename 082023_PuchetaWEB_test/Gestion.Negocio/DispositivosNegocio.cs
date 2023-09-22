using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
    public class DispositivosNegocio
    {
        #region ABM's

        public int Insert(Dispositivos data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Dispositivos data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Dispositivos data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Dispositivos data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Dispositivos.Attach(data);
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

        public List<Dispositivos> Get_Dispositivos()
        {
            List<Dispositivos> Lista = new List<Dispositivos>();
            using (var db = new iotdbContext())
            {
                Lista = db.Dispositivos.OrderBy(o=>o.Id).ToList();
            }

            return Lista;
        }


        public Dispositivos Get_One_Acciones(int Id)
        {
            Dispositivos dispositivos = new Dispositivos();
            using (var db = new iotdbContext())
            {
                dispositivos = db.Dispositivos.FirstOrDefault(m => m.Id == Id);
            }

            return dispositivos;
        }



        #endregion
    }
}
