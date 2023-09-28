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
    public class CertificadoNuevoNegocioDetalle
    {

        #region ABM's

        public int Insert(Certificados_detalle_planificado data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(Certificados_detalle_planificado data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(Certificados_detalle_planificado data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(Certificados_detalle_planificado data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.certificados_detalle_planificado.Attach(data);
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


        public ReturnData Eliminacion_All_DetallesCertificados(Certificados_detalle_planificado data)
        {

            ReturnData Data = new ReturnData();
            int id=0;

                try
                {
                    using (var cn = new iotdbContext())
                    {
                            cn.certificados_detalle_planificado.Attach(data);
                            cn.Entry(data).State = EntityState.Deleted;
                            cn.SaveChanges();
                            Data.isError = false;
                    }
                }
                catch (Exception)
                {
                    Data.isError = true;
                }
            


            return Data;
        }



        #endregion
        #region Select's

        /// <summary>
        /// Método que devuelve una lista de Proyectos con las entidades relacionadas
        /// según indique el parámetro "conInclude"
        /// </summary>
        /// <param name="conIncludes">"True" para incluir entidades relacionadas, de lo contrario "False"</param>
        /// <returns>List de Proyecto</returns>
        public List<Certificados_detalle_planificado> Get_All_Ordenes()
        {
            try
            {
                List<Certificados_detalle_planificado> Ordenes = new List<Certificados_detalle_planificado>();
                using (var db = new iotdbContext())
                {
                    Ordenes = db.certificados_detalle_planificado.ToList();
                }
                return Ordenes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Ordenes_HeaderNegocio: Get_All_Proyecto(): exception.", ex);
            }
        }

        public Certificados_detalle_planificado Get_One_Orden(int Id)
        {
            Certificados_detalle_planificado accion = new Certificados_detalle_planificado();
            using (var db = new iotdbContext())
            {
                accion = db.certificados_detalle_planificado.FirstOrDefault(m => m.Id == Id);
            }

            return accion;
        }

        public Certificados_detalle_planificado Get_One_Orden_x_Certificado(int Id)
        {
            Certificados_detalle_planificado accion = new Certificados_detalle_planificado();
            using (var db = new iotdbContext())
            {
                accion = db.certificados_detalle_planificado.FirstOrDefault(m => m.IdCertificados == Id);
            }

            return accion;
        }

        public double CalcularCantidadPeriodo(DateTime pFechaDesde, DateTime pFechaHasta, int idNotaPedidoDetalle)
        {
            double CantidadPeriodo=0;
            try
            {


                    List<ParteDiario_Actividades_Contratista> listas = new ParteDiario_Actividades_ContratistaNegocio().Get_All_ParteDiario_Actividades_Contratistas()
                    .Where(s => s.NotaPedidoDetalleId == idNotaPedidoDetalle
                            && s.Fecha >= pFechaDesde
                            && s.Fecha <= pFechaHasta
                            && s.TrabajoHoy == true)
                    .ToList();
                    CantidadPeriodo = listas.Sum(x => x.Cantidad);

            }
            catch (Exception ex)
            {
                CantidadPeriodo = 0;

            }


            return CantidadPeriodo;
        }
        #endregion
    }
}
