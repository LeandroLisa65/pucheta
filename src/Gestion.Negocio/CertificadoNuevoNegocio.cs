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
    public class CertificadoNuevoNegocio
    {

        #region ABM's

        public int Insert(Certificados data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Certificados data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Certificados data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Certificados data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.Certificados.Attach(data);
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
        public List<Certificados> Get_All_Ordenes()
        {
            try
            {
                List<Certificados> Ordenes = new List<Certificados>();
                using (var db = new iotdbContext())
                {
                    Ordenes = db.Certificados.ToList();
                }
                return Ordenes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Ordenes_HeaderNegocio: Get_All_Proyecto(): exception.", ex);
            }
        }

        public Certificados Get_One_Orden(int Id)
        {
            Certificados accion = new Certificados();
            using (var db = new iotdbContext())
            {
                accion = db.Certificados.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public Certificados Get_One_Orden_NroCertificado(int IdProyecto, int IdContratista, int Secuenciador)
        {
            Certificados accion = new Certificados();
            using (var db = new iotdbContext())
            {
                accion = db.Certificados.FirstOrDefault(m => m.Secuenciador == Secuenciador 
                                                    && m.IdProyecto== IdProyecto
                                                    && m.IdContratista==IdContratista);
            }

            return accion;
        }

        #endregion
    }
}
