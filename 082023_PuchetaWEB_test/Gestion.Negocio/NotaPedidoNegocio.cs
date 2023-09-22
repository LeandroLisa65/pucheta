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
    public class NotaPedidoNegocio
    {

        #region ABM's

        public int Insert(NotaPedido data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(NotaPedido data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(NotaPedido data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(NotaPedido data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.NotaPedido.Attach(data);
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
        public List<NotaPedido> Get_All_Ordenes()
        {
            try
            {
                List<NotaPedido> lOrdenes = new List<NotaPedido>();
                using (var db = new iotdbContext())
                {
                    lOrdenes = db.NotaPedido.ToList();
                }
                return lOrdenes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Ordenes_HeaderNegocio: Get_All_Proyecto(): exception.", ex);
            }
        }

        public NotaPedido Get_One_Orden(int Id)
        {
            NotaPedido accion = new NotaPedido();
            using (var db = new iotdbContext())
            {
                accion = db.NotaPedido.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }


        #endregion
    }
}
