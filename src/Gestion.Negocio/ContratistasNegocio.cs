using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
     public class ContratistasNegocio
    {

        #region ABM's

        public int Insert(Contratistas data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Contratistas data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Contratistas data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Contratistas data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Contratistas.Attach(data);
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

        public List<Contratistas> Get_All_Contratistas()
        {
            List<Contratistas> Lista = new List<Contratistas>();
            using (var db = new iotdbContext())
            {
                Lista = db.Contratistas.OrderBy(x=> x.Nombre).ToList();
            }

            return Lista;
        }

        public Contratistas Get_One_Contratistas(int Id)
        {
            Contratistas accion = new Contratistas();
            using (var db = new iotdbContext())
            {
                accion = db.Contratistas.FirstOrDefault(m => m.Id == Id);
            }
            return accion;
        }

        public List<Contratistas> Get_x_lIds(List<int> lIdsContratistas)
        {
            List<Contratistas> lContratistas = new List<Contratistas>();
            using (var db = new iotdbContext())
            {
                lContratistas = db.Contratistas
                    .Where(c => lIdsContratistas.Contains(c.Id) == true)
                    .ToList();
            }
            return lContratistas;
        }

        #endregion


    }
}
