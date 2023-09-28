using Gestion.EF;
using Gestion.EF.Models;
using Gestion.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Web.Controllers
{
    public class InformeActividadesVencidasController : Controller
    {
        // GET: InformeActividadesVencidas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InformeActividadesVencidas()
        {
            return View();
        }

        public ItemLoginUsuario GetUsuarioLogueado()
        {
            ItemLoginUsuario d = new ItemLoginUsuario();
            string[] us = (HttpContext.User.Identity.Name).Split("#");

            d.Id = Convert.ToInt32(us[0]);
            d.Nombre = us[1];
            d.Apellido = us[2];
            d.Email = us[3];
            d.Tipo = us[4];
            d.ApellidoNombre = us[1] + ", " + us[2];
            d.Grupo = Convert.ToInt32(us[5]);

            return d;
        }

        public object Get_InformeActividadesVencidas()
        {
            var result = new object();
            try
            {
                ItemLoginUsuario oItemUsuario = GetUsuarioLogueado();
                Usuarios oUsuarioLogueado = new UsuariosNegocio().Get_Usuario(oItemUsuario.Id);
                if(oUsuarioLogueado != null)
                {
                    List<Proyecto> lProyectos = new ProyectoNegocio()
                        .Get_x_UsuarioId(oUsuarioLogueado.Id);
                    List<int> lIdsProyectos = lProyectos
                        .Select(p => p.Id).ToList();
                    List<Proyecto_Ubicaciones> lProyUbicaciones = new Proyecto_UbicacionesNegocio()
                        .Get_X_lIdsProyectos(lIdsProyectos);
                    List<int> lIdsProyUbicaciones = lProyUbicaciones
                        .Select(pu => pu.Id).ToList();
                    List<Planificacion_Proyecto_Actividades> lPlanProyActividades = 
                        new Planificacion_Proyecto_ActividadesNegocio().Get_X_lIdsProyUbicaciones(lIdsProyUbicaciones);
                    List<int> lIdsPlanProyActividades = lPlanProyActividades
                        .Select(ppa => ppa.Id).ToList();
                    List<ParteDiario_Actividades> lPrtDiaActividades = new ParteDiario_ActividadesNegocio()
                        .Get_X_lIdsPlanProyActividades(lIdsPlanProyActividades);
                    List<int> lIdsPlanActividades = lPlanProyActividades
                        .Select(ppa => ppa.Planificacion_ActividadesId).ToList();
                    List<Planificacion_Actividades> lPlanActividades = new Planificacion_ActividadesNegocio()
                        .Get_X_lIdsPlanActividades(lIdsPlanActividades);
                    List<int> lIdsPrtDiaActividades = lPrtDiaActividades
                        .Select(pda => pda.Id).ToList();
                    List<ParteDiario_Actividades_Contratista> lPrtDiaActContratistas = new ParteDiario_Actividades_ContratistaNegocio()
                        .Get_X_lIdsPrtDiaActividades(lIdsPrtDiaActividades);

                    List<Informe_Actividad_Vencida> lInfActVencidas = new List<Informe_Actividad_Vencida>();
                    lPlanProyActividades.ForEach(ppa =>
                    {
                        if(ppa.Fecha_Est_Fin < DateTime.Now)
                        {
                            List<ParteDiario_Actividades> lPrtDiaAct_Aux = lPrtDiaActividades
                                .Where(pda => pda.Planificacion_Proyecto_ActividadesId == ppa.Id)
                                .ToList();
                            List<ParteDiario_Actividades_Contratista> lPrtDiaActCont_Aux = new List<ParteDiario_Actividades_Contratista>();
                            lPrtDiaAct_Aux.ForEach(pda =>
                            {
                                lPrtDiaActCont_Aux.AddRange(lPrtDiaActContratistas
                                    .Where(pdac => pdac.ParteDiario_ActividadesId == pda.Id).ToList());
                            });

                            // ¿Hubo avaces en las ultimas 72 hr?
                            bool tuvoAvances = lPrtDiaActCont_Aux.Where(pdac => pdac.Fecha >= DateTime.Now.AddDays(-3)).Count() > 0;

                            var avanceReal = lPrtDiaActCont_Aux.Sum(pdac => pdac.Cantidad);
                            if (avanceReal < ppa.Avance)
                            {
                                // TODO: aca debo preguntar si ya existe un Informe con IdPPA == ppa.id
                                Proyecto_Ubicaciones oProyUbicacion = lProyUbicaciones
                                    .Find(pu => pu.Id == ppa.Proyecto_UbicacionesId);
                                Proyecto oProyecto = lProyectos
                                    .Find(p => p.Id == oProyUbicacion.ProyectoId);
                                Planificacion_Actividades oPlanActividad = lPlanActividades
                                    .Find(pa => pa.Id == ppa.Planificacion_ActividadesId);
                                Informe_Actividad_Vencida oInfActVencida = new Informe_Actividad_VencidaNegocio()
                                    .Get_Unico_X_Ids(ppa.Id, oProyUbicacion.Id, oPlanActividad.Id);
                                if (oInfActVencida == null) oInfActVencida = new Informe_Actividad_Vencida();
                                oInfActVencida = new Informe_Actividad_Vencida();
                                oInfActVencida.ProyectoId = oProyUbicacion.ProyectoId;
                                oInfActVencida.sProyecto = oProyecto.Nombre;
                                oInfActVencida.ProyectoUbicacionId = oProyUbicacion.Id;
                                oInfActVencida.sProyUbicacion = oProyUbicacion.Nombre;
                                oInfActVencida.PlanActividadId = oPlanActividad.Id;
                                oInfActVencida.sPlanActividad = oPlanActividad.Nombre;
                                oInfActVencida.PlanificacionProyectoActividadId = ppa.Id;
                                oInfActVencida.Fecha_Est_Fin = ppa.Fecha_Est_Fin;
                                oInfActVencida.DiasVencida = (int)(DateTime.Now - ppa.Fecha_Est_Fin).TotalDays;
                                if (oInfActVencida.Id == 0)
                                    oInfActVencida.Id = new Informe_Actividad_VencidaNegocio().Insert(oInfActVencida);
                                lInfActVencidas.Add(oInfActVencida);
                            }
                        }
                    });
                    result = new
                    {
                        lInfActVencidas
                    };
                }
                else
                {
                    result = new
                    {
                        error = true,
                        message = "Acceso denegado."
                    };
                }
            }
            catch
            {
                result = new
                {
                    error = true,
                    message = "Error: InformeActividadesVencidasController: GetInformeActividadesVencidas: exception."
                };
            }
            return result;
        }

    }
}
