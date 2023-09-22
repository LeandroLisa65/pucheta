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
    public class CertificadoNuevooDetallAdicionalNegocio
    {

        #region ABM's

        public int Insert(Certificados_detalle_adicional data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Certificados_detalle_adicional data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Certificados_detalle_adicional data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Certificados_detalle_adicional data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.certificados_detalle_adicional.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.cda_Id;
                }
            }
            catch (Exception err)
            {

                id = 0;
            }
            return id;
        }

        #endregion
        #region Select's

        public List<Certificados_detalle_adicional> Get_All_Ordenes()
        {
            try
            {
                List<Certificados_detalle_adicional> Ordenes = new List<Certificados_detalle_adicional>();
                using (var db = new iotdbContext())
                {
                    Ordenes = db.certificados_detalle_adicional.ToList();
                }
                return Ordenes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Ordenes_HeaderNegocio: Get_All_Proyecto(): exception.", ex);
            }
        }

        public Certificados_detalle_adicional Get_One_Orden(int Id)
        {
            Certificados_detalle_adicional accion = new Certificados_detalle_adicional();
            using (var db = new iotdbContext())
            {
                accion = db.certificados_detalle_adicional.FirstOrDefault(m => m.cda_Id == Id);
            }

            return accion;
        }

 
        #endregion
    }
}
