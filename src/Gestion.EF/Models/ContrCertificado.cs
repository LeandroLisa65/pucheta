using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion.EF.Models
{
    public class ContrCertificado
    {

        #region PROPIEDADES

        public int Id { get; set; }
        public int ContratistaId { get; set; }
        public Contratistas Contratista { get; set; }
        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
        public DateTime FecDesde { get; set; }
        public DateTime FecHasta { get; set; }
        public int CertContrAnteriorId { get; set; }
        public DateTime FecRegistro { get; set; }
        public int UsuarioRegistroId { get; set; }
        public ContrCertificado CertContrAnterior { get; set; }
        public List<ContrCert_PDActContr> lCertContrs_PDActContrs { get; set; }
        public List<CertContr_PlanProyAct> lCertContrs_PlanProyActs { get; set; }

        #endregion

        #region PROPIEDADES DERIVADAS

        public string FechaRegistro
        {
            get
            {
                return this.FecRegistro.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }

        public string FechaDesde
        {
            get
            {
                return this.FecDesde.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }

        public string FechaHasta
        {
            get
            {
                return this.FecHasta.ToString(ValoresUtiles.Formato_dd_MM_yyyy);
            }
        }

        public string Periodo
        {
            get
            {
                return this.FechaDesde + " - " + this.FechaHasta;
            }
        }

        public string ContratistaNombre
        {
            get
            {
                string nombre = "Sin Contratista Asignado";
                if (this.Contratista != null)
                    nombre= this.Contratista.Nombre;
                return nombre;
            }
        }

        public string ProyectoNombre
        {
            get
            {
                string nombre = "Sin Proyecto Asignado";
                if (this.Proyecto != null)
                    nombre = this.Proyecto.Nombre;
                return nombre;
            }
        }

        public string Detalle
        {
            get
            {
                return this.ContratistaNombre + " - " + this.ProyectoNombre + 
                    " - Periodo: " + this.FechaDesde + " - " + this.FechaHasta;
            }
        }

        public object AsData()
        {
            object oData = new object();
            try
            {
                oData = new
                {
                    this.Detalle,
                    lCertContrs_PlanProyActs = this.lCertContrs_PlanProyActs
                        .Select(ccpdac => new
                        {
                            ccpdac.Id,
                            ccpdac.NroItem,
                            ccpdac.Ubicacion,
                            ccpdac.Actividad,
                            ccpdac.UnidadMedida,
                            ccpdac.MontoPlanificado,
                            ccpdac.Partida,
                            ccpdac.CantidadPlanificada,
                            ccpdac.CantidadAcumAnterior,
                            ccpdac.CantidadAjuste,
                            ccpdac.CantidadActual,
                            ccpdac.CantidadAcumulada,
                            ccpdac.ImporteAcumAnterior,
                            ccpdac.ImporteActual,
                            ccpdac.ImporteAcumulado
                        })
                        .ToList()
                };
            }
            catch(Exception ex)
            {
                throw new Exception("Error: Certificado_Contratista: AsData(): exception.", ex);
            }
            return oData;
        }

        #endregion

    }
}
