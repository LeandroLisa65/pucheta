using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Negocio
{
     public class ParteDiario_Sol_ObrasAdicNegocio
    {

        #region ABM's

        public int Insert(ParteDiario_Sol_ObrasAdic data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(ParteDiario_Sol_ObrasAdic data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ParteDiario_Sol_ObrasAdic data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ParteDiario_Sol_ObrasAdic data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ParteDiario_Sol_ObrasAdic.Attach(data);
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

        public List<ParteDiario_Sol_ObrasAdic> Get_All_ParteDiario_Sol_ObrasAdic()
        {
            List<ParteDiario_Sol_ObrasAdic> Lista = new List<ParteDiario_Sol_ObrasAdic>();
            using (var db = new iotdbContext())
            {
                Lista = db.ParteDiario_Sol_ObrasAdic.Include(x => x.ParteDiario).ToList();
            }

            return Lista;
        }


        public ParteDiario_Sol_ObrasAdic Get_One_ParteDiario_Sol_ObrasAdic(int Id)
        {
            ParteDiario_Sol_ObrasAdic accion = new ParteDiario_Sol_ObrasAdic();
            using (var db = new iotdbContext())
            {
                accion = db.ParteDiario_Sol_ObrasAdic.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        #endregion
    }
}
