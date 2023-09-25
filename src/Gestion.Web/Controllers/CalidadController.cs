using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestion.Web.Models;
using Gestion.EF.Models;
using Gestion.Negocio;
using Gestion.EF;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Gestion.Web.Controllers
{
    public class CalidadController : Controller
    {
        // GET: Incidentes
        private readonly IWebHostEnvironment _env;

        public CalidadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CalidadObra()
        {
            Res_CalidadObra Result = new Res_CalidadObra();
             
            Result = CargoDatosCalidadObra(DateTime.Now.AddMonths(-1), DateTime.Now);
            // return PartialView(Result);
            return View(Result);
        }

        [HttpPost]
        public Res_CalidadObra _CalidadObra(FiltroPeriodoFechas oFiltro)
        {
            Res_CalidadObra r = new Res_CalidadObra();
            Res_CalidadObra Result = new Res_CalidadObra();
            r = CargoDatosCalidadObra(oFiltro.FecDesde, oFiltro.FecHasta);
            return r;

            //List<Incidentes_Historial> ListaG = new List<Incidentes_Historial>();
            //ListaG = CargoIncidentes(oFiltro.FecDesde, oFiltro.FecHasta, "0", "CA");
            //return PartialView(ListaG);
        }

        #region Armado de datos para la Pantalla de CalidadObra Grilla
        private Res_CalidadObra CargoDatosCalidadObra(DateTime pFechaDesde, DateTime pFechaHasta)
        {
            Res_CalidadObra Result = new Res_CalidadObra();

            #region 1- Traigo todos los item de calidad que estan grabados y los completo con toda la info que necesito
            List<Calidad_Actividades_Valoracion> lstCalidad = new Calidad_Actividades_ValoracionNegocio().Get_ItemCalidadActividadValoracion_ParaGrilla(0, pFechaDesde, pFechaHasta).OrderByDescending(s => Convert.ToDateTime(s.sFecha)).ToList();
            Result.lstItemCalidad = lstCalidad;
            #endregion

            #region 2-Distintos Proyectos
            Result.lstProyectos = lstCalidad.OrderBy(p => p.sProyecto).Select(o => o.sProyecto).Distinct().ToList();
            Result.lstRubros = lstCalidad.OrderBy(p => p.sRubro).Select(o => o.sRubro).Distinct().ToList();
            Result.lstActividades = lstCalidad.OrderBy(p => p.sActividad).Select(o => o.sActividad).Distinct().ToList();
            #endregion

            return Result;
        }

        #endregion

        public partial class FiltroPeriodoFechas
        {
            public string FechaDesde { get; set; }
            public string FechaHasta { get; set; }
            public DateTime FecDesde
            {
                get
                {
                    DateTime fecDesde = new DateTime();
                    try
                    {
                        if (!string.IsNullOrEmpty(this.FechaDesde))
                            fecDesde = DateTime.Parse(this.FechaDesde);
                        else
                            fecDesde = DateTime.Today.AddMonths(-1);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: FiltroPeriodoFechas: FecDesde: exception.", ex);
                    }
                    return fecDesde;
                }
            }
            public DateTime FecHasta
            {
                get
                {
                    DateTime fecHasta = new DateTime();
                    try
                    {
                        if (!string.IsNullOrEmpty(this.FechaHasta))
                            fecHasta = DateTime.Parse(this.FechaHasta);
                        else
                            fecHasta = DateTime.Today;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: FiltroPeriodoFechas: FecHasta: exception.", ex);
                    }
                    return fecHasta;
                }
            }
        }

    }
}
