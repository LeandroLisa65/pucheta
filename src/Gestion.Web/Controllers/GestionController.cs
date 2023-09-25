using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestion.Web.Models;
using Gestion.EF;
using Gestion.EF.Models;
using Gestion.Negocio;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using static Gestion.Web.Controllers.PlanificacionController;

namespace Gestion.Web.Controllers
{
    [Authorize]
    public class GestionController : Controller
    {

        private readonly IWebHostEnvironment _env;

        public GestionController(IWebHostEnvironment env)
        {
            _env = env;
        }

        #region Controller Paginas

        public ActionResult Index(int id)
        {
            try
            {

                #region LLAMAMOS A LOS PROCESOS QUE DEBERÍAN EJECUTARSE AUTOMATICAMENTE

                // REPLANIFICACION ACTIVIDADES VENCIDAS
                ProcesoAutomatico oProcAut = new ProcesoAutomaticoNegocio()
                    .Get_x_Fecha_Motivo(DateTime.Today, ValoresUtiles.Motivo_ProcAut_ReplanificaionActividades);
                if (oProcAut == null)
                    this.Replanificar_PPAs_Vencidas();

                // EMAILS INFORMATIVOS
                this.EnviarMailsInformativos();

                #endregion

                return View(getDatosSeccionObras());
            }
            catch (Exception ex)
            {
                throw new Exception("Error: GestionController: Index(int): exception.", ex);
            }
        }

        public ActionResult Encriptar()
        {
            return View();
        }

        #region Pantalla Home - Datos para los Jefes de Obra

        public ItemCargaHome getDatosSeccionObras()
        {
            ItemCargaHome mResult = new ItemCargaHome();
            
            // Se comenta las siguientes lineas ya que se pidio que se sacaran los cuadros
            // de la pantalla de gestión apra agilizar la primer carga al ingresar al sistema.

            //1 - Busco la fehca del ultimo parte diario de cada obra
            //mResult.lstObrasParteDiarios = getUltimosPartesDiarios();

            //2 - BUscamos los ultinmos 20 incidendentes de criticidad media y alta
            mResult.lstIncidentes = getUltimosIncidentes();
            return mResult;
        }

        public List<ObrasParteDiario> getUltimosPartesDiarios()
        {
            List<ObrasParteDiario> lstOPD = new List<ObrasParteDiario>();

            //1 - Buscamos todos los proyectos que hay
            var lstProyectos = new ProyectoNegocio().Get_All_Proyecto().GroupBy(p => p.Id).Select(p => p.Key);

            var lstPartesDiarios = new ParteDiarioNegocio().Get_All_ParteDiario().GroupBy(p => p.ProyectoId);

            foreach (var item in lstProyectos)
            {
                //2 - Buscamos la fecha del ultimo parte diario de cada proyecto
                ParteDiario oPD = new ParteDiarioNegocio().Get_All_ParteDiario().Where(p => p.ProyectoId == item).OrderByDescending(p => p.Fecha_Creacion).FirstOrDefault();

                if (oPD != null)
                {
                    ObrasParteDiario oOPD = new ObrasParteDiario();
                    oOPD.fechaUltima = oPD.Fecha_Creacion;
                    oOPD.Proyecto = oPD.Proyecto.Nombre;

                    lstOPD.Add(oOPD);
                }

            }

            return lstOPD.OrderBy(p => p.Proyecto).ToList();
        }

        public List<Carga_Incidentes> getUltimosIncidentes() {

            List<Carga_Incidentes> lstCI = new List<Carga_Incidentes>();

            // 1- Busco los Partes Diarios de los ultimos 5 Dias 

            var lstPartesDiarios = new ParteDiarioNegocio().Get_All_ParteDiario().Where(p => p.Fecha_Creacion >= DateTime.Now.AddDays(-5)).OrderByDescending(p => p.Fecha_Creacion);


            foreach (var item in lstPartesDiarios)
            {
                // 2 - Recorro parte diario y veo los incidentes que tiene que sea distintos a Sin Criticidad
                var lstIncidentes = new ParteDiario_IncidentesNegocio().Get_All_ParteDiario_IncidentesPA(item.Id).Where(p => p.Criticidad != "Sin Criticidad");

                if (lstIncidentes != null)
                {
                    foreach (var oIncidente in lstIncidentes)
                    {
                        Carga_Incidentes oCI = new Carga_Incidentes();

                        if (oIncidente.ContratistaId != 0)
                        {
                            //Busco el contratista
                            string mNombre = new ContratistasNegocio().Get_One_Contratistas(oIncidente.ContratistaId).Nombre;
                            oCI.sComentario = "Contratista:" + mNombre + " - ";
                        }

                        oCI.sComentario = oCI.sComentario + oIncidente.Comentario;
                        oCI.sCriticidad = oIncidente.Criticidad;
                        oCI.IncidenteId = oIncidente.IncidenteId;
                        if (oIncidente.IncidenteId != 0)
                        {
                            oCI.sIncidente = new IncidentesNegocio().Get_One_Incidentes(oIncidente.IncidenteId).Nombre;
                        }
                        else
                        {
                            oCI.sIncidente = "";
                        }
                        oCI.sFecha = item.Fecha_Creacion;
                        oCI.sProyecto = item.Proyecto.Nombre;

                        lstCI.Add(oCI);
                    }
                }
            }

            return lstCI.OrderByDescending(p => p.sFecha).ToList();
        }

        #endregion

        #endregion

        #region Menu

        public ActionResult Menu()
        {
            // List<Menus> lMenus = new MenusNegocio().Get_All_Menus();
            // return View(lMenus);
            return View();
        }

        public ActionResult _MenuGrilla()
        {
            List<Menus> lMenus = new MenusNegocio().Get_All_Menus();
            return PartialView(lMenus);
        }

        public ActionResult _MenuAbm(int id)
        {
            try
            {
                MenuEdit rMenu = new MenuEdit();
                Menus menu = new Menus();

                List<ItemSelect> lista = new List<ItemSelect>();

                ItemSelect s1 = new ItemSelect();
                s1.nId = 0;
                s1.Detalle = "0 - Sin Padre. ";
                lista.Add(s1);

                List<Menus> lMenus = new MenusNegocio().Get_Menus_Padres();
                foreach (var item in lMenus)
                {
                    ItemSelect s = new ItemSelect();
                    s.nId = item.Id;
                    s.Detalle = item.Id + " - " + item.Nombre + " / " + item.Descripcion;
                    lista.Add(s);
                }

                if (id > 0)
                {
                    menu = new MenusNegocio().Get_One_Menu(id);
                }

                rMenu.Menu = menu;
                rMenu.ddlMenu = lista;

                return PartialView(rMenu);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return PartialView("Error");
            }
        }

        [HttpPost]
        public ReturnData MenuGraba(Menus menu)
        {
            ReturnData d = new ReturnData();
            if (menu != null)
            {
                int idc;
                if (menu.Id > 0)
                {
                    idc = new MenusNegocio().Update(menu);
                }
                else
                {
                    idc = new MenusNegocio().Insert(menu);
                }

                if (idc > 0)
                {
                    d.isError = false;
                    d.data = "Item Menu se ha grabado OK.";
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

        #endregion

        #region Usuarios

        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult _UsuariosGrilla()
        {
            List<EF.Models.Usuarios> usuarios = new UsuariosNegocio().Get_Usuarios();
            return PartialView(usuarios);
        }

        public ActionResult _UsuariosAbm(int id)
        {
            ItemUsuarios usuario = new ItemUsuarios();

            EF.Models.Usuarios user = new Usuarios();
            if (id > 0)
            {
                user = new UsuariosNegocio().Get_Usuario(id);
            }


            List<Roles> roles = new RolesNegocio().Get_All_Roles();

            List<ItemRoles> rRoles = new List<ItemRoles>();
            foreach (var item in roles)
            {
                ItemRoles ra = new ItemRoles();
                ra.Id = item.Id;
                ra.Detalle = item.Detalle;
                List<RolesUsuarios> rolUsuario = user.Roles;
                if (rolUsuario != null)
                {
                    foreach (var itemR in rolUsuario)
                    {
                        if (item.Id == itemR.RolesId)
                        {
                            ra.Activo = true;
                        }
                    }
                }

                rRoles.Add(ra);
            }

            usuario.Provincias = new ProvinciasNegocio().Get_Provincias();
            usuario.Usuarios = user;
            usuario.Roles = rRoles;
            usuario.Dispositivos = new DispositivosNegocio().Get_Dispositivos();

            return PartialView(usuario);
        }

        [HttpGet]
        public ReturnData getLocalidades(int id)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = new LocalidadesNegocio().Get_Localidades(id);

            return data;
        }

        [HttpPost]
        public ReturnData UsuariosGraba(UsuarioEdit usuario)
        {
            ReturnData data = new ReturnData();

            try
            {


                Gestion.EF.Models.Usuarios u = new Gestion.EF.Models.Usuarios();

                if (usuario.Id > 0)
                {
                    u = new UsuariosNegocio().Get_Usuario(usuario.Id);
                    u.Nombre = usuario.Nombre;
                    u.Apellido = usuario.Apellido;
                    u.NUsuario = usuario.NUsuario;
                    u.Email = usuario.Email;
                    if (usuario.Tipo == "")
                    {
                        u.Tipo = "O";
                    }
                    else
                    {
                        u.Tipo = usuario.Tipo;
                    }

                    if (u.Clave != usuario.Clave)
                    {
                        u.Clave = usuario.Clave;
                    }

                    u.Avatar = usuario.Avatar;
                    u.Activo = usuario.Activo;
                    u.Borrado = usuario.Borrado;
                    var idg = new UsuariosNegocio().Update(u);
                    if (idg > 0)
                    {
                        if (usuario.Roles != null)
                        {
                            if (new RolesUsuariosNegocio().ValidaExiste(usuario.Roles) > 0)
                            {
                                bool del = new RolesUsuariosNegocio().Deletes(usuario.Id);
                                if (del)
                                {
                                    foreach (var item in usuario.Roles)
                                    {
                                        new RolesUsuariosNegocio().Insert(item);
                                    }
                                    data.isError = false;
                                    data.data = "Los datos del usuario se actualizáron OK.";
                                }
                                else
                                {
                                    data.isError = true;
                                    data.data = "Error al actualizar los Roles.";
                                }
                            }
                            else
                            {
                                data.isError = false;
                                data.data = "Los datos del usuario se actualizáron OK.";
                            }
                        }
                        else
                        {
                            bool del = new RolesUsuariosNegocio().Deletes(usuario.Id);
                            data.isError = false;
                            data.data = "Los datos del usuario se actualizáron OK.";
                        }
                    }
                    else
                    {
                        data.isError = true;
                        data.data = "Error al actualizar los datos del usuario.";
                    }
                }
                else
                {
                    u.Nombre = usuario.Nombre;
                    u.Apellido = usuario.Apellido;
                    u.NUsuario = usuario.NUsuario;
                    u.Email = usuario.Email;
                    u.Clave = Security.Encrypt(usuario.Clave);

                    if (usuario.Avatar == null)
                    {
                        u.Avatar = "avatar15.png";
                    }
                    else
                    {
                        u.Avatar = usuario.Avatar;
                    }

                    u.Activo = usuario.Activo;
                    u.Borrado = usuario.Borrado;
                    if (usuario.Tipo == "")
                    {
                        u.Tipo = "O";
                    }
                    else
                    {
                        u.Tipo = usuario.Tipo;
                    }
                    u.Grupo = 0;
                    u.Rsid = "";
                    u.Rsn = "";
                    u.Fecha = DateTime.Now;

                    int addUsuarioID = new UsuariosNegocio().Insert(u);

                    foreach (var item in usuario.Roles)
                    {
                        item.UsuariosId = addUsuarioID;
                        new RolesUsuariosNegocio().Insert(item);
                    }

                    data.isError = false;
                    data.data = "Los datos del nuevo usuario se grabaron OK.";
                }

            }
            catch (Exception)
            {
                data.isError = true;
                data.data = "Error al grabar los datos";
            }

            return data;
        }

        [HttpPost]
        public ReturnData UsuariosDireccionGraba(UsuariosDireccion direccion)
        {

            ReturnData data = new ReturnData();
            try
            {
                int getId = 0;
                if (direccion.Id > 0)
                {
                    getId = new UsuariosDireccionNegocio().Update(direccion);
                }
                else
                {
                    getId = new UsuariosDireccionNegocio().Insert(direccion);
                }

                if (getId > 0)
                {
                    data.isError = false;
                    data.data = new UsuariosDireccionNegocio().Get_UsuariosDireccion(direccion.UsuariosId);

                }
                else
                {
                    data.isError = true;
                    data.data = "Error al grabar.";
                }

            }
            catch (Exception)
            {
                data.isError = true;
                data.data = "Error al grabar en la base.";
            }

            return data;
        }

        [HttpPost]
        public ReturnData UsuariosDispositivoGraba(ItemUsuarioDispositivoEdit dispositivo)
        {

            ReturnData data = new ReturnData();
            try
            {
                int getId = 0;
                UsuariosDispositivo dis = new UsuariosDispositivo();
                if (dispositivo.Id > 0)
                {
                    dis = new UsuariosDispositivoNegocios().Get_One_Dispositivo(dispositivo.Id);
                    dis.Activo = dispositivo.Activo;
                    dis.Borrado = dispositivo.Borrado;
                    dis.DispositivosId = dispositivo.DispositivosId;
                    dis.Empresa = dispositivo.Empresa;
                    dis.IMEI = dispositivo.IMEI;
                    dis.Numero = dispositivo.Numero;
                    dis.OS = dispositivo.OS;
                    dis.UsuariosId = dispositivo.UsuariosId;

                    getId = new UsuariosDispositivoNegocios().Update(dis);
                }
                else
                {
                    dis.Id = 0;
                    dis.Activo = dispositivo.Activo;
                    dis.Borrado = dispositivo.Borrado;
                    dis.DispositivosId = dispositivo.DispositivosId;
                    dis.Empresa = dispositivo.Empresa;
                    dis.IMEI = dispositivo.IMEI;
                    dis.Numero = dispositivo.Numero;
                    dis.OS = dispositivo.OS;
                    dis.UsuariosId = dispositivo.UsuariosId;

                    getId = new UsuariosDispositivoNegocios().Insert(dis);
                }

                if (getId > 0)
                {
                    data.isError = false;
                    data.data = new UsuariosDispositivoNegocios().Get_UsuariosDispositivo(dispositivo.UsuariosId);
                }
                else
                {
                    data.isError = true;
                    data.data = "Error al grabar.";
                }

            }
            catch (Exception)
            {
                data.isError = true;
                data.data = "Error al grabar en la base.";
            }

            return data;
        }

        [HttpGet]
        public ReturnData UsuariosResetClave(int id)
        {
            ReturnData data = new ReturnData();

            try
            {

                Usuarios usuario = new UsuariosNegocio().Get_Usuario(id);
                string nuevaClave = ToolsNegocio.CrearPassword(6);
                string nuevaClaveEncriptada = Security.Encrypt(nuevaClave);
                usuario.Clave = nuevaClaveEncriptada;

                new UsuariosNegocio().Update(usuario);

                string Mensaje = "Estimado Usuario " + usuario.Nombre + ", su clave ha sido resetada por el administrador.<br><br> Su nueva clave es : " + nuevaClave;
                bool envio = EnviaEmailNegocio.Enviar(usuario.Email, "Reset Clave", Mensaje, true);

                data.isError = false;
                data.data = "Clave Reseteada OK y se ha enviado mail al suario " + usuario.Nombre;
            
            }
            catch (Exception error)
            {
                Logger.Error(error.ToString());
                data.isError = true;
                data.data = "Error al gabar los datos.";
            }

            return data;
        }

        #endregion

        #region Roles

        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult _RolesGrilla()
        {
            List<Roles> Lista = new List<Roles>();
            List<Roles> lRoles = new RolesNegocio().Get_All_Roles();
            foreach (var item in lRoles)
            {
                Roles r = new Roles();
                r = item;
                foreach (var itemA in r.Acciones)
                {
                    itemA.Acciones = new AccionesNegocio().Get_One_Acciones(itemA.AccionesID);

                }


            }

            return PartialView(lRoles);
        }

        public ActionResult _RolesAbm(int id)
        {
            try
            {
                RolesEdit rRoles = new RolesEdit();

                Roles rol = new Roles();
                if (id > 0)
                {
                    rol = new RolesNegocio().Get_One_Roles(id);
                }

                List<Acciones> acciones = new List<Acciones>();
                acciones = new AccionesNegocio().Get_All_Acciones();

                List<ItemAcciones> racciones = new List<ItemAcciones>();
                foreach (var item in acciones)
                {
                    ItemAcciones ra = new ItemAcciones();
                    ra.Id = item.Id;
                    ra.Detalle = item.Detalle;
                    List<AccionesRoles> rolAcciones = rol.Acciones;
                    if (rolAcciones != null)
                    {
                        foreach (var itemR in rolAcciones)
                        {
                            if (item.Id == itemR.AccionesID)
                            {
                                ra.Activo = true;
                            }
                        }
                    }

                    racciones.Add(ra);
                }


                rRoles.Rol = rol;
                rRoles.Acciones = racciones;

                return PartialView(rRoles);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return PartialView("Error");
            }
        }

        [HttpPost]
        public ReturnData RolesGraba(Roles rol)
        {
            ReturnData d = new ReturnData();
            if (rol != null)
            {

                int idc = 0;
                if (rol.Id > 0)
                {
                    bool delete = new AccionesRolesNegocio().Deletes(rol.Id);
                    if (delete)
                    {
                        idc = new RolesNegocio().Update(rol);
                    }

                }
                else
                {
                    idc = new RolesNegocio().Insert(rol);
                }

                if (idc > 0)
                {
                    d.isError = false;
                    d.data = "Item Rol se ha grabado OK.";
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

        #endregion

        #region Acciones

        public ActionResult Acciones()
        {
            return View();
        }

        public ActionResult _AccionesGrilla()
        {
            List<Acciones> lAcciones = new AccionesNegocio().Get_All_Acciones();
            return PartialView(lAcciones);
        }

        public ActionResult _AccionesAbm(int id)
        {
            try
            {
                AccionesEdit rAcciones = new AccionesEdit();
                Menus menu = new Menus();
                Acciones acciones = new Acciones();

                List<ItemSelect> lista = new List<ItemSelect>();

                ItemSelect s1 = new ItemSelect();
                s1.nId = 0;
                s1.Detalle = "0 - Sin Menú asociado. ";
                lista.Add(s1);

                List<Menus> lMenus = new MenusNegocio().Get_Menus_link();
                foreach (var item in lMenus)
                {
                    ItemSelect s = new ItemSelect();
                    s.nId = item.Id;
                    s.Detalle = item.Id + " - " + item.Nombre + " / " + item.Descripcion;
                    lista.Add(s);
                }

                if (id > 0)
                {
                    acciones = new AccionesNegocio().Get_One_Acciones(id);
                }

                rAcciones.Accion = acciones;
                rAcciones.ddlMenu = lista;

                return PartialView(rAcciones);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return PartialView("Error");
            }
        }

        [HttpPost]
        public ReturnData AccionesGraba(Acciones acciones)
        {
            ReturnData d = new ReturnData();
            if (acciones != null)
            {
                int idc;
                if (acciones.Id > 0)
                {
                    idc = new AccionesNegocio().Update(acciones);
                }
                else
                {
                    idc = new AccionesNegocio().Insert(acciones);
                }

                if (idc > 0)
                {
                    d.isError = false;
                    d.data = "Item se ha grabado OK.";
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

        #endregion

        #region Perfil

        public ActionResult Perfil()
        {
            ItemUsuarioEditAvatar usuario = new ItemUsuarioEditAvatar();

            string[] us = (HttpContext.User.Identity.Name).Split("#");

            EF.Models.Usuarios user = new Usuarios();            
            user = new UsuariosNegocio().Get_Usuario(Convert.ToInt32(us[0]));


            usuario.Id = user.Id;
            usuario.Apellido = user.Apellido;
            usuario.Avatar = user.Avatar;
            usuario.Email = user.Email;
            usuario.Nombre = user.Nombre;
            usuario.NUsuario = user.NUsuario;

            usuario.UsuariosDireccion = new UsuariosDireccionNegocio().Get_UsuariosDireccion(user.Id);
            usuario.UsuariosDispositivo = new UsuariosDispositivoNegocios().Get_UsuariosDispositivo(user.Id);
            usuario.Provincias = new ProvinciasNegocio().Get_Provincias();            
            
            
            return View(usuario);
        }

        [HttpPost]
        public ReturnData ActualizaDatos(ItemUsuarioEditAvatar model)
        {
            ReturnData dReturn = new ReturnData();

            try
            {

                ItemUsuarios usuario = new ItemUsuarios();
                string[] us = (HttpContext.User.Identity.Name).Split("#");

                EF.Models.Usuarios user = new Usuarios();
                user = new UsuariosNegocio().Get_Usuario(Convert.ToInt32(us[0]));

                string fileName = null;
                if (model.FAvatar != null)
                {
                    string Folder = Path.Combine(_env.WebRootPath, "dist/avatar/");
                    fileName = user.Id + "_" + model.FAvatar.FileName;
                    string filePath = Path.Combine(Folder, fileName);
                    model.FAvatar.CopyTo(new FileStream(filePath, FileMode.Create));
                    user.Avatar = fileName;
                }

                user.Apellido = model.Apellido;
                user.Nombre = model.Nombre;
                user.NUsuario = model.NUsuario;
                user.Email = model.Email;

                int id = new UsuariosNegocio().Update(user);
                if(id > 0)
                {
                    dReturn.isError = false;
                    dReturn.data = "Los datos se actualizaron con éxito.";
                } else
                {
                    dReturn.isError = true;
                    dReturn.data = "Error al grabar los datos.";
                }
                
            }
            catch (Exception)
            {
                dReturn.isError = true;
                dReturn.data = "Error procesando los datos.";
            }

            return dReturn;
        }

        [HttpPost]
        public ReturnData ActualizaClave(ItemClaves model)
        {
            ReturnData dReturn = new ReturnData();

            ItemUsuarios usuario = new ItemUsuarios();
            string[] us = (HttpContext.User.Identity.Name).Split("#");

            EF.Models.Usuarios user = new Usuarios();
            user = new UsuariosNegocio().Get_Usuario(Convert.ToInt32(us[0]));
            if(user.Clave == Security.Encrypt(model.ClaveActual))
            {
                user.Clave = Security.Encrypt(model.ClaveNueva);
                int id = new UsuariosNegocio().Update(user);
                if (id > 0)
                {
                    string Mensaje = "Estimado Usuario " + user.Nombre + ", su clave ha sido modificada.<br><br> Su nueva clave es : " + model.ClaveNueva;
                    EnviaEmailNegocio.Enviar(user.Email, "Reset Clave", Mensaje, true);

                    dReturn.isError = false;
                    dReturn.data = "La Clave se ha actualizado.";
                } else
                {
                    dReturn.isError = true;
                    dReturn.data = "Error al intentar actualizar la clave.";
                }
            }
            else
            {
                dReturn.isError = true;
                dReturn.data = "La Clave Actual ingresasa no es correcta.";
            }



            return dReturn;
        }

        #endregion

        #region Metodos Varios

        [HttpPost]
        public UsuarioMenu_Login Get_User_Menu()
        {
            UsuarioMenu_Login user = new UsuarioMenu_Login();

            string[] us = (HttpContext.User.Identity.Name).Split("#");

            user.Idusuario = Convert.ToInt32(us[0]);
            user.Nombre = us[1];
            user.Apellido = us[2];
            user.Email = us[3];
            user.Tipo = us[4];
            user.Grupo = Convert.ToInt32(us[5]);
            user.Avatar = us[6];

            List<Menu_Login> getMenu = new List<Menu_Login>();
            /*List<Menus> lmenu = new MenusNegocio().Get_Menus(us[4]);
            foreach (var item in lmenu)
            {
                Models.Menu_Login pm = new Models.Menu_Login();
                pm.Menu = item;
                List<Menus> lmenusub = new MenusNegocio().Get_Menus_Sub(item.Id);
                List<Menus> menusub = new List<Menus>();
                foreach (var itemsub in lmenusub)
                {
                    Menus ms = itemsub;
                    menusub.Add(ms);
                }
                pm.MenuSub = menusub;

                getMenu.Add(pm);
            }*/

            user.gMenu = getMenu;

            return user;
        }

        [HttpPost]
        public ReturnData Get_encrip(ItemEncriptarGet gData)
        {
            ReturnData dReturn = new ReturnData();
            try
            {


                ItemReturnEncrip dr = new ItemReturnEncrip();


                if (gData.ncadena == 0)
                {
                    dr.Encriptado = Security.Encrypt(gData.scadena);
                    dr.Desencriptado = Security.Decrypt(dr.Encriptado);
                }
                else
                {
                    dr.Desencriptado = Security.Decrypt(gData.scadena);
                    dr.Encriptado = Security.Encrypt(dr.Desencriptado);
                }

                dReturn.isError = false;
                dReturn.data = dr;
            }
            catch (Exception)
            {
                dReturn.isError = true;
                dReturn.data = "Error;";
            }


            return dReturn;

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        #endregion

        #region Registros Automáticos

        private void EnviarMailsInformativos()
        {
            try
            {
                if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                {
                    DateTime maxFechaCreacion = new EmailNegocio().Get_MaxFechaCreacion();
                    if (maxFechaCreacion.Date < DateTime.Now.Date)
                    {
                        new ParteDiarioNegocio().InformarProyectosSinParteDiario();
                        new Incidentes_HistorialNegocio().InformarIncidentesVencidos();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error: GestionController: EjecutarRegistrosAutomaticos: exception.", ex);
            }
        }

        private void Replanificar_PPAs_Vencidas()
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> lPPAs = new Planificacion_Proyecto_ActividadesNegocio()
                    .Get_All()
                    .Where(ppa => ppa.Fecha_Est_Fin.Date <= DateTime.Now.AddDays(-2) &&
                        ppa.Finalizados == false)
                    .ToList();
                lPPAs.ForEach(ppa =>
                {
                    this.Replanificar_FecEstFin_PPAsHijas(ppa, 1);
                });
                ProcesoAutomatico oProcAut = new ProcesoAutomatico();
                oProcAut.FecCreacion = DateTime.Now;
                oProcAut.Motivo = ValoresUtiles.Motivo_ProcAut_ReplanificaionActividades;
                oProcAut.Entidad = new PlanProyActividad().GetType().Name;
                new ProcesoAutomaticoNegocio().Insert(oProcAut);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: PlanificacionController: ReplanificarAutomaticamente(): exception.", ex);
            }
        }

        private void Replanificar_FecEstFin_PPAsHijas(Planificacion_Proyecto_Actividades oPPA, int Nivel)
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> lPPAs_Hijas = new Planificacion_Proyecto_ActividadesNegocio()
                    .Get_X_IdPadre(oPPA.Id);
                if (!oPPA.Finalizados)
                {
                    //  SI ES MAYOR QUE 2 ENTONCES ESTAMOS RECORRIENDO LAS PPAs NIETAS
                    if (Nivel > 2)
                    {
                        if (oPPA.Fecha_Real_Incio == DateTime.MinValue)
                        {
                            double cantidaDias = (DateTime.Now.AddDays(-1) - oPPA.Fecha_Est_Fin).TotalDays;
                            DateTime nuevaFecEstInicio = oPPA.Fecha_Est_Incio.AddDays(cantidaDias);
                            if (nuevaFecEstInicio > oPPA.Fecha_Est_Incio)
                                oPPA.Fecha_Est_Incio = oPPA.Fecha_Est_Incio.AddDays(cantidaDias);
                        }
                    }
                    DateTime nuevaFecEstFin = DateTime.Now.AddDays(-1);
                    if (nuevaFecEstFin > oPPA.Fecha_Est_Fin && nuevaFecEstFin > oPPA.Fecha_Est_Incio)
                        oPPA.Fecha_Est_Fin = DateTime.Now.AddDays(-1);

                    new Planificacion_Proyecto_ActividadesNegocio().Update(oPPA);
                }
                lPPAs_Hijas.ForEach(ppa =>
                {
                    this.Replanificar_FecEstFin_PPAsHijas(ppa, Nivel + 1);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: PlanificacionController: ReplanificarAutomaticamente(Planificacion_Proyecto_Actividades, int): exception.", ex);
            }
        }

        #endregion

        #region PROYECTOS

        [HttpGet]
        public object Get_PlazosPrevistos()
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuario = this.getUsuarioActual();
                if(oUsuario != null)
                {
                    List<dynamic> lPlazosPrevistos = new List<dynamic>();
                    List<Proyecto> lProyectos = new List<Proyecto>();
                    List<Roles> lRoles = new RolesUsuariosNegocio().Get_Roles_x_UsuarioId(oUsuario.Id);
                    if(lRoles.Find(r => r.Id == ValoresUtiles.IdRol_CoordinacionObra ||
                        r.Id == ValoresUtiles.IdRol_DireccionEmpresa ||
                        r.Id == ValoresUtiles.IdRol_AdminFull) != null)
                        lProyectos = new ProyectoNegocio().Get_x_Estado(string.Empty, false);
                    else
                        lProyectos = new ProyectoNegocio().Get_x_UsuarioId(oUsuario.Id);
                    lProyectos.ForEach(p =>
                    {
                        List<Planificacion_Proyecto_Actividades> lPPA = new Planificacion_Proyecto_ActividadesNegocio()
                            .Get_X_IdProyecto(p.Id);
                        DateTime fechaPlanificadaFin = DateTime.MinValue;
                        int diasDiferencia = 0;
                        if(lPPA.Count > 0)
                        {
                            fechaPlanificadaFin = lPPA.Max(ppa => ppa.Fecha_Est_Fin);
                            diasDiferencia = (int)(fechaPlanificadaFin - p.Fecha_Fin).TotalDays;
                        }
                        lPlazosPrevistos.Add(new
                        {
                            p.Id,
                            p.Nombre,
                            FechaFinPrevista = p.FechaFin,
                            fechaPlanificadaFin = fechaPlanificadaFin.ToString(ValoresUtiles.Formato_dd_MM_yyyy),
                            diasDiferencia
                        });
                        lPlazosPrevistos = lPlazosPrevistos
                            .OrderByDescending(pp => pp.diasDiferencia).ToList();
                    });
                    oResult = new
                    {
                        lPlazosPrevistos
                    };
                }
            }
            catch(Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    message = ValoresUtiles.Msj_Error,
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #endregion

        #region PARTIDAS PRESUPUESTARIAS

        public ActionResult PartidasPresupuestarias()
        {
            return View();
        }

        public ActionResult _PartidasPresupuestariasGrilla()
        {
            try
            {
                List<PartidaPresupuestaria> lPartidasPresupuestarias = new PartidaPresupuestariaNegocio()
                    .GetAll()
                    .OrderBy(pp => pp.Codigo)
                    .ToList();
                return PartialView(lPartidasPresupuestarias);
            }
            catch(Exception ex)
            {
                throw new Exception("Error: GestionController: _PartidasPresupuestariasGrilla: exception.", ex);
            }
        }

        public ActionResult _PartidasPresupuestariasAbm(int id)
        {
            try
            {
                PartidaPresupuestaria oPartidaPresupuestaria = new PartidaPresupuestaria();
                if (id > 0)
                    oPartidaPresupuestaria = new PartidaPresupuestariaNegocio().Get_x_Id(id);
                return PartialView(oPartidaPresupuestaria);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return PartialView("Error");
            }
        }

        [HttpPost]
        public ReturnData GrabarPartidaPresupuestaria(PartidaPresupuestaria oPartidaPresupuestaria)
        {
            ReturnData d = new ReturnData();
            if (oPartidaPresupuestaria != null)
            {
                int idc = 0;
                if (oPartidaPresupuestaria.Id > 0)
                    idc = new PartidaPresupuestariaNegocio().Update(oPartidaPresupuestaria);
                else
                    idc = new PartidaPresupuestariaNegocio().Insert(oPartidaPresupuestaria);
                if (idc > 0)
                {
                    d.isError = false;
                    d.data = "Partida Presupuestaria registrada.";
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

        #endregion

        #region PRIVADOS

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