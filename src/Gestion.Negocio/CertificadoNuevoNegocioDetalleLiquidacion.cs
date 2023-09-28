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
    public class CertificadoNuevoNegocioDetalleLiquidacion
    {

        #region ABM's

        public int Insert(Certificados_detalle_liquidacion data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Certificados_detalle_liquidacion data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Certificados_detalle_liquidacion data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Certificados_detalle_liquidacion data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.certificados_detalle_liquidacion.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.cdl_Id;
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

        /// <summary>
        /// Método que devuelve una lista de Proyectos con las entidades relacionadas
        /// según indique el parámetro "conInclude"
        /// </summary>
        /// <param name="conIncludes">"True" para incluir entidades relacionadas, de lo contrario "False"</param>
        /// <returns>List de Proyecto</returns>
        public List<Certificados_detalle_liquidacion> Get_All_Ordenes()
        {
            try
            {
                List<Certificados_detalle_liquidacion> Ordenes = new List<Certificados_detalle_liquidacion>();
                using (var db = new iotdbContext())
                {
                    Ordenes = db.certificados_detalle_liquidacion.ToList();
                }
                return Ordenes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Ordenes_HeaderNegocio: Get_All_Proyecto(): exception.", ex);
            }
        }

        public Certificados_detalle_liquidacion Get_One_Orden(int Id)
        {
            Certificados_detalle_liquidacion accion = new Certificados_detalle_liquidacion();
            using (var db = new iotdbContext())
            {
                accion = db.certificados_detalle_liquidacion.FirstOrDefault(m => m.cdl_Id == Id);
            }

            return accion;
        }

        public Certificados_detalle_liquidacion Get_One_Orden_x_Certificado(int Id)
        {
            Certificados_detalle_liquidacion accion = new Certificados_detalle_liquidacion();
            using (var db = new iotdbContext())
            {
                accion = db.certificados_detalle_liquidacion.FirstOrDefault(m => m.cdl_IdCertificados == Id);
            }

            return accion;
        }

      
        #endregion
    }
}
