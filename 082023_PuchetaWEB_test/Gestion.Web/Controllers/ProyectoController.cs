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

namespace Gestion.Web.Controllers
{
    [Authorize]
    public class ProyectoController : Controller
    {
        // GET: Proyecto
        public ActionResult Index()
        {
            return View();            
        }

        #region Proyecto

        public ActionResult Proyecto()
        {
            try
            {
                List<Proyecto> lProyectos = new List<Proyecto>();
                ProyectoNegocio oProyNegocio = new ProyectoNegocio();
                lProyectos = new ProyectoNegocio().Get_All_Proyecto(false);
                lProyectos.ForEach(p =>
                {
                    if (p.Fecha_Fin == DateTime.MinValue)
                    {
                        p.Fecha_Fin = p.Fecha_Inicio.AddDays(p.Duracion);
                        oProyNegocio.Update(p);
                    }
                });
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyectoController: Index: exception.", ex);
            }
        }

        public ActionResult _ProyectoGrilla()
        {
            List<Proyecto> Lista = new List<Proyecto>();
            Lista = new ProyectoNegocio().Get_All_Proyecto();
            return PartialView(Lista);
        }

        public ActionResult _ProyectoAbm(int id)
        {
            ItemProyecto data = new ItemProyecto();
            if (id != 0)
            {
                data.pr = new ProyectoNegocio().Get_One_Proyecto(id);
            }
            else
            {
                data.pr = new Proyecto();
            }
            data.Usuarios = new UsuariosNegocio().Get_Usuarios().ToList();
            return PartialView(data);
        }

        [HttpPost]
        public ReturnData ProyectoGraba(Proyecto data)
        {
            ReturnData d = new ReturnData();
            try
            {
                if (data != null)
                {
                    int idc;
                    if (data.Id > 0)
                    {
                        idc = new ProyectoNegocio().Update(data);
                    }
                    else
                    {
                        idc = new ProyectoNegocio().Insert(data);
                    }

                    if (idc > 0)
                    {
                        d.isError = false;
                        d.data = "Se han grabado los datos OK.";
                    }
                    else
                    {
                        d.isError = true;
                        d.data = "Error al Grabar en la base.";
                    }

                    d.data1 = idc;
                }
                else
                {
                    d.isError = true;
                    d.data = "Error al Grabar";

                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyectoController: ProyectoGraba(Proyecto): exception.", ex);
            }
            return d;
        }

        public ReturnData GetProyectos()
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = GetProyectosMostrar(false);
            return data;
        }
        public ReturnData GetProyectosActivos()
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = GetProyectosMostrar(true);
            return data;
        }
        public List<Proyecto> GetProyectosMostrar(bool mSoloActivos)
        {
            Usuarios oUsuario = getUsuarioActual();

            List<Proyecto> lstProyectos = new List<Proyecto>();

            if (oUsuario.Roles.Any(s=> s.Id == 1))
            {
                lstProyectos = new ProyectoNegocio().Get_All_Proyecto().OrderBy(p => p.Nombre).ToList();
            }
            else
            {
                lstProyectos = new ProyectoNegocio().Get_All_Proyecto().Where(p => p.UsuariosId == oUsuario.Id).OrderBy(p => p.Nombre).ToList();
                if (lstProyectos.Count == 0)
                {
                    lstProyectos = new ProyectoNegocio().Get_All_Proyecto().OrderBy(p => p.Nombre).ToList();
                }

            }
           
           
            if (mSoloActivos)
                lstProyectos = lstProyectos.Where(p => p.Estado == null || p.Estado == "").ToList();

            return lstProyectos;
        }
        #endregion

        #region Proyecto Ubicacion

        public ActionResult Proyecto_Ubicaciones()
        {
            return View();
        }

        public ActionResult _Proyecto_UbicacionesGrilla()
        {
            List<Proyecto_Ubicaciones> Lista = new List<Proyecto_Ubicaciones>();
            Lista = new Proyecto_UbicacionesNegocio().Get_All_Proyecto_Ubicaciones();
            return PartialView(Lista);
        }

        public ActionResult _Proyecto_UbicacionesAbm(int id)
        {
            ItemProyecto_Ubicaciones data = new ItemProyecto_Ubicaciones();
            if (id != 0)
            {
                data.pu = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones(id);
            }
            data.Proyecto = new ProyectoNegocio().Get_All_Proyecto();
            return PartialView(data);
        }

        [HttpGet]
        public ReturnData getUbicaciones(int id)
        {
            ReturnData r = new ReturnData();
            List<Proyecto_Ubicaciones> Lista = new List<Proyecto_Ubicaciones>();
            try
            {
                Lista = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(id);
                r.data = Lista;
                r.isError = false;
            }
            catch (Exception)
            {
                r.data = Lista;
                r.isError = false;                
            }

            return r;
        }

        [HttpGet]
        public ReturnData getUbicacion(int id)
        {
            ReturnData r = new ReturnData();
            List<Proyecto_Ubicaciones> Lista = new List<Proyecto_Ubicaciones>();
            try
            {
                Lista = new Proyecto_UbicacionesNegocio().Get_All_Ubicaciones(id);
                r.data = Lista;
                r.isError = false;
            }
            catch (Exception)
            {
                r.data = Lista;
                r.isError = false;
            }

            return r;
        }

        [HttpPost]
        public ReturnData Proyecto_UbicacionesGraba(Proyecto_Ubicaciones data)
        {
            ReturnData d = new ReturnData();

            List<Proyecto_Ubicaciones> lpu = new List<Proyecto_Ubicaciones>();

            if (data != null)
            {
                int idc;
                if (data.Id > 0)
                {
                    idc = new Proyecto_UbicacionesNegocio().Update(data);
                }
                else
                {
                    idc = new Proyecto_UbicacionesNegocio().Insert(data);
                }

                if (idc > 0)
                {
                    d.isError = false;
                    d.data = "Se han grabado los datos OK.";
                    lpu = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(data.ProyectoId);
                    d.data1 = lpu;
                }
                else
                {
                    d.isError = true;
                    d.data = "Error al Grabar en la base.";
                }


            }
            else
            {
                d.isError = true;
                d.data = "Error al Grabar";

            }

            return d;
        }

        [HttpPost]
        public ReturnData ControlyEliminacionUbicacion(int IdUbicacion)
        {
            ReturnData d = new ReturnData();

            Proyecto_Ubicaciones UbicacionSeleccionada = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones(IdUbicacion);
            
            try
            {
                int IdProyecto = UbicacionSeleccionada.ProyectoId;
                //Controlamos que la ubicacion ingresada no tenga un PPA asociado
                List<Planificacion_Proyecto_Actividades> PPA = new Planificacion_Proyecto_ActividadesNegocio().Get_All()
                                                                .Where(s=>
                                                                s.Proyecto_UbicacionesId == UbicacionSeleccionada.Id)
                                                                .ToList();
                //Si existe algun registro, devolvemos error.
                if(PPA.Count > 0 )
                {
                    d.isError = true;
                    d.data2 = "No se puede eliminar la ubicación porque tiene una actividad asociada.";
                }
                else
                {
                    //Si no tiene asociado una PPA, la eliminamos.
                    int idEliminado = new Proyecto_UbicacionesNegocio().Delete(UbicacionSeleccionada);
                    if(idEliminado > 0 ) { 
                        d.isError = false;
                        d.data = "Se elimino correctamente la Ubicación.";
                    }

                    //Devolvemos la nueva lista de Ubicaciones
                    d.data3 = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(IdProyecto);
                }

            }
            catch (Exception)
            {

                d.isError = true;
                d.data2 = "Hubo un problema para realizar la operación.";
            }
            return d;
        }
        #endregion

        #region Proyecto Contratista

        public ActionResult _Proyecto_ContratistasAbm(int id)
        {
            ItemListaContratista data = new ItemListaContratista();
            List<Proyecto_Contratista> lista = new List<Proyecto_Contratista>();
            if (id != 0)
            {
                 lista = new Proyecto_ContratistaNegocio().Get_All_Proyecto_Contratista(id);
                
            }

            data.Id = id;
            data.Lista = lista;

            return PartialView(data);
        }

        [HttpPost]
        public ReturnData Proyecto_ContratistasGraba(Proyecto_Contratista data)
        {
            ReturnData d = new ReturnData();
            if (data != null)
            {
                try
                {
                    Proyecto_Contratista pcg = new Proyecto_ContratistaNegocio().Get_All_Proyecto_Contratista(data.ProyectoId).FirstOrDefault(x => x.ContratistaId == data.ContratistaId);
                    if (pcg == null)
                    {

                        data.sContratistas = new ContratistasNegocio().Get_One_Contratistas(data.ContratistaId).Nombre;
                        new Proyecto_ContratistaNegocio().Insert(data);

                        List<Proyecto_Contratista> ListaRet = new Proyecto_ContratistaNegocio().Get_All_Proyecto_Contratista(data.ProyectoId);
                       

                        d.data = "Grabado ok";
                        d.data1 = ListaRet;
                        d.isError = false;
                    }
                    else
                    {
                        d.data = "Ya existe un registros de Contratista para este Proyecto.";
                        d.isError = true;
                    }
                }
                catch (Exception err)
                {
                    d.data = err.ToString() + "Error al grabar";
                    d.isError = true;
                }
            }
            else
            {
                d.isError = true;
                d.data = "Error al Grabar";
            }
            return d;
        }

        [HttpPost]
        public ReturnData PContratistaBorra(int id)
        {
            ReturnData d = new ReturnData();
            int idc;
            try
            {
                Proyecto_Contratista pcb = new Proyecto_Contratista();

                Proyecto_Contratista pcbi = new Proyecto_ContratistaNegocio().Get_One_Proyecto_Contratista(id);
                idc = new Proyecto_ContratistaNegocio().Delete(pcbi);


                d.isError = false;
                d.data = "Se han Borrado los datos OK.";
            }
            catch (Exception)
            {
                d.isError = true;
                d.data = "Error al Borrar";

            }
            return d;
        }

        [HttpGet]
        public object GetProyectos_x_IdContratista(int idContratista)
        {
            var oResult = new object();
            try
            {
                Usuarios oUsuarioLogueado = this.getUsuarioActual();
                if (oUsuarioLogueado != null)
                {
                    List<Proyecto_Contratista> lProyectosContratistas = new Proyecto_ContratistaNegocio()
                        .Get_x_IdContratista(idContratista, true);
                    List<Proyecto> lProyectos = lProyectosContratistas
                        .Select(pc => pc.Proyecto)
                        .ToList();
                    if (idContratista == 0)
                        lProyectos = lProyectos
                            .GroupBy(p => p.Id)
                            .Select(g=>g.First())
                            .ToList();
                    oResult = new
                    {
                        lProyectos = lProyectos.Select(p => new { p.Id, p.Nombre })
                    };
                }
                else
                {
                    oResult = new
                    {
                        isError = true,
                        mensaje = ValoresUtiles.Msj_AccesoDenegado,
                    };
                }
            }
            catch (Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    mensaje = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #endregion

        #region DataSelect

        [HttpGet]
        public ReturnData getActividades(int id)
        {
            ReturnData r = new ReturnData();
            List<Planificacion_Proyecto_Actividades> Lista = new List<Planificacion_Proyecto_Actividades>();
            try
            {
                Lista = new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades(id);
                r.data = Lista;
                r.isError = false;
            }
            catch (Exception)
            {
                r.data = Lista;
                r.isError = false;
            }

            return r;
        }


        [HttpGet]
        public ReturnData getActividadeslog(int id)
        {
            ReturnData r = new ReturnData();
            List<Planificacion_Proyecto_Actividades_Log> Lista = new List<Planificacion_Proyecto_Actividades_Log>();
            try
            {
                Lista = new Planificacion_Proyecto_Actividades_Log_Negocio().Get_All_Planificacion_Proyecto_Actividades_Log(id);
                r.data = Lista;
                r.isError = false;
            }
            catch (Exception)
            {
                r.data = Lista;
                r.isError = false;
            }

            return r;
        }

        [HttpGet]
        public ReturnData GetContratistas()
        {
            ReturnData d = new ReturnData();
            d.isError = false;
            d.data = new ContratistasNegocio().Get_All_Contratistas();

            return d;
        }


        #endregion

        #region USUARIO LOGUEADO

        private ItemLoginUsuario getItemLoguinUsuarioActual()
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

        private Usuarios getUsuarioActual()
        {
            Usuarios oUsuario = new Usuarios();
            try
            {
                ItemLoginUsuario oILUsuario = this.getItemLoguinUsuarioActual();
                oUsuario = new UsuariosNegocio().Get_Usuario(oILUsuario.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: getUsuarioActual(): exception.", ex);
            }
            return oUsuario;
        }

        #endregion
    }
}