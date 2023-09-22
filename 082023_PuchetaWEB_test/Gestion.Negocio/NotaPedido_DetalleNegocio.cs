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
    public class NotaPedido_DetalleNegocio
    {

        #region ABM's

        public int Insert(NotaPedido_Detalle data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(NotaPedido_Detalle data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(NotaPedido_Detalle data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(NotaPedido_Detalle data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.NotaPedido_Detalle.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
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
        public List<NotaPedido_Detalle> Get_All_Ordenes()
        {
            try
            {
                List<NotaPedido_Detalle> lOrdenes = new List<NotaPedido_Detalle>();
                using (var db = new iotdbContext())
                {
                    lOrdenes = db.NotaPedido_Detalle.ToList();
                }
                return lOrdenes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Ordenes_HeaderNegocio: Get_All_Proyecto(): exception.", ex);
            }
        }

        public NotaPedido_Detalle Get_One_Orden(int Id)
        {
            NotaPedido_Detalle accion = new NotaPedido_Detalle();
            using (var db = new iotdbContext())
            {
                accion = db.NotaPedido_Detalle.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public List<NotaPedido_Detalle> Get_Orden_PPA(int IdPPA)
        {
            List<NotaPedido_Detalle> accion = new List<NotaPedido_Detalle>();
            using (var db = new iotdbContext())
            {
                accion = db.NotaPedido_Detalle.Where(m => m.IdPPA == IdPPA).ToList();
            }

            return accion;
        }

      

        public List<NotaPedido_Detalle> Get_Orden_By_IdPA(int IdPA)
        {
            List<NotaPedido_Detalle> accion = new List<NotaPedido_Detalle>();
            using (var db = new iotdbContext())
            {
                accion = db.NotaPedido_Detalle.Where(m => m.IdPA == IdPA).ToList();
            }

            return accion;
        }

        #endregion
    }
}
