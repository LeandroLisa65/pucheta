using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
     public class ParteDiario_ContratistaNegocio
    {

        #region ABM's

        public int Insert(ParteDiario_Contratista data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(ParteDiario_Contratista data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ParteDiario_Contratista data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ParteDiario_Contratista data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ParteDiario_Contratista.Attach(data);
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

        public List<ParteDiario_Contratista> Get_All_ParteDiario_Contratista()
        {
            List<ParteDiario_Contratista> Lista = new List<ParteDiario_Contratista>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Contratista.Include(x => x.Usuarios).ToList();
            }

            return Lista;
        }


        public ParteDiario_Contratista Get_One_ParteDiario_Contratista(int Id)
        {
            ParteDiario_Contratista accion = new ParteDiario_Contratista();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario_Contratista.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion
    }
}
