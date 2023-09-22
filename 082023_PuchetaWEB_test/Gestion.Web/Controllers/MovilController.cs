using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.EF.Models;
using Gestion.EF;
using Gestion.Negocio;
using Gestion.Web.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using Microsoft.Extensions.Configuration;

namespace Gestion.Web.Controllers
{
    [ApiController]
    [Route("api/Movil/{action}/{id?}")]
    public class MovilController : Controller
    {

        #region VersionApp

        [HttpPost]
        public ReturnData Get_VersionApp()
        {
            ReturnData dataReturn = new ReturnData();
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            dataReturn.data = configuration["VersionApp"];
            return dataReturn;
        }

        #endregion

        #region Usuarios

        [HttpPost]
        public ReturnData Usuario_Login([FromBody] mobile_Usuarios data)
        {
            ReturnData dataReturn = new ReturnData();
            if (data.Email == null && data.Clave == null)
            {
                dataReturn.isError = true;
                dataReturn.data = "Usuario o Contraseña incorrecto/a";
                return dataReturn;
            }
            if (data.Email.Trim().Length > 0 && data.Clave.Trim().Length > 0)
            {
                Usuarios oUsr = new UsuariosNegocio().Get_Usuario_Login(data.Email, Security.Encrypt(data.Clave));
                List<RolesUsuarios> lRolesUsuarios = new RolesUsuariosNegocio().GetRolesUsuarios(oUsr.Id);
                List<int> lIdRoles = lRolesUsuarios.Select(ru => ru.RolesId).Distinct().ToList();
                List<Roles> lRoles = new RolesNegocio().Get_By_lIds(lIdRoles);
                List<AccionesRoles> lAccRoles = new AccionesRolesNegocio().Get_By_lIds(lIdRoles);
                List<Acciones> lAcciones = new AccionesNegocio().Get_By_lIds(lAccRoles.Select(ar => ar.AccionesID).Distinct().ToList());
                lAccRoles.ForEach(ar =>
                {
                    Acciones oAccion = lAcciones.Find(a => a.Id == ar.AccionesID);
                    ar.Acciones = oAccion;
                });
                dataReturn.isError = false;
                lRolesUsuarios.ForEach(ru =>
                {
                    Roles oRol = lRoles.Find(r => r.Id == ru.RolesId);
                    ru.Roles = oRol;
                    ru.Roles.Acciones = lAccRoles.Where(ar => ar.RolesId == ru.RolesId).ToList();
                });
                oUsr.Roles = lRolesUsuarios;
                dataReturn.data = oUsr;
            }
            else
            {
                dataReturn.isError = true;
                dataReturn.data = "Usuario o Contraseña incorrecto/a";
            }
            return dataReturn;
        }

        #endregion

        #region Incidentes

        private readonly IWebHostEnvironment _env;
        public MovilController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public partial class DataParteDiarioIncidente
        {
            public int UsuarioId { get; set; }
            public int ProyectoId { get; set; }
            public ItemParteDiarioIncidenteGraba oItemParteDiarioIncidente { get; set; }
        }
        [HttpPost]
        public ReturnData Grabar_ParteDiarioIncidente(DataParteDiarioIncidente data)
        {
            ReturnData oRD = new ReturnData();
            try
            {
                Usuarios oUsuarioLogueado = new UsuariosNegocio().Get_Usuario(data.UsuarioId);
                List<RolesUsuarios> lRolesUsuarios = new RolesUsuariosNegocio().GetRolesUsuarios(oUsuarioLogueado.Id);
                ItemParteDiarioIncidenteGraba oPDI = data.oItemParteDiarioIncidente;
                List<ParteDiario> lPartesDiarios = new ParteDiarioNegocio()
                    .Get_PartesDiarios_X_ProyectoId(data.ProyectoId)
                    .OrderByDescending(pd => pd.Fecha_Creacion)
                    .ToList();
                if (lPartesDiarios.Count > 0)
                {
                    ParteDiario oParteDiario = lPartesDiarios.First();
                    if (oPDI.Comentario != null)
                    {
                        oPDI.Comentario = ToolsNegocio.EliminaEnter(oPDI.Comentario);
                    }
                    ParteDiario_Incidentes oParteDiarioIncidente = new ParteDiario_Incidentes();
                    oParteDiarioIncidente.Comentario = oPDI.Comentario;
                    oParteDiarioIncidente.ContratistaId = oPDI.ContratistaId;
                    oParteDiarioIncidente.Id = oPDI.Id;
                    oParteDiarioIncidente.IncidenteId = oPDI.IncidenteId;
                    oParteDiarioIncidente.NoConformidadId = oPDI.NoConformidadId;
                    oParteDiarioIncidente.ParteDiarioId = oParteDiario.Id;
                    oParteDiarioIncidente.sContratista = oPDI.sContratista;
                    oParteDiarioIncidente.sIncidente = oPDI.sIncidente;
                    oParteDiarioIncidente.SolicitadoPor = oPDI.SolicitadoPor;
                    oParteDiarioIncidente.Criticidad = oPDI.Criticidad;

                    int IdPDI = new ParteDiario_IncidentesNegocio().Insert(oParteDiarioIncidente);

                    #region ARCHIVOS ADJUNTOS
                    //Cargamos FOTOS 
                    string Error = "";
                    if (oPDI.lFotos != null && oPDI.lFotos.Count > 0)
                    {
                        int contador = 1;
                        foreach (List<string> oFoto in oPDI.lFotos)
                        {
                            string folder = Path.Combine(_env.WebRootPath, ValoresUtiles.PathArchivos_Novedades);
                            string nombreFoto =
                                DateTime.Now.Year.ToString() +
                                DateTime.Now.Month.ToString() +
                                DateTime.Now.Day.ToString() +
                                DateTime.Now.Hour.ToString() +
                                DateTime.Now.Minute.ToString() +
                                DateTime.Now.Second.ToString() +
                                DateTime.Now.Millisecond.ToString() +
                                "_" + IdPDI.ToString() + "_" + contador + "." + oFoto[0];
                            contador++;
                            string GraboCorrectamente = this.Grabar_Foto(nombreFoto, oFoto[2]);
                            Error = GraboCorrectamente;
                            if (GraboCorrectamente == "OK")
                            {
                                try
                                {
                                    Archivos_Adjuntos oArchivoAdjunto = new Archivos_Adjuntos();
                                    oArchivoAdjunto.Archivo = nombreFoto;
                                    oArchivoAdjunto.Extencion = oFoto[0].ToUpper();
                                    oArchivoAdjunto.Fecha = DateTime.Now;
                                    oArchivoAdjunto.IdUsuario = oUsuarioLogueado.Id;
                                    oArchivoAdjunto.sUsuario = oUsuarioLogueado.Apellido + oUsuarioLogueado.Nombre;
                                    oArchivoAdjunto.URL = ValoresUtiles.PathArchivos_Novedades;
                                    int IdAA = new Archivos_AdjuntosNegocio().Insert(oArchivoAdjunto);

                                    Archivos_Adjuntos_Relacion oAARelacion = new Archivos_Adjuntos_Relacion();
                                    oAARelacion.Archivos_AdjuntosId = IdAA;
                                    oAARelacion.Entidad = "PINC";
                                    oAARelacion.IdEntidad = IdPDI;

                                    new Archivos_Adjuntos_RelacionNegocio().Insert(oAARelacion);
                                }
                                catch (Exception ex)
                                {
                                    oRD.isError = true;
                                    oRD.data1 = ex.InnerException.Message;
                                }
                            }
                        }
                    }
                    #endregion

                    oRD.isError = false;
                    oRD.data = new
                    {
                        ParteDiarioId = oParteDiario.Id,
                        FecCreacion = oParteDiario.Fecha_Creacion,
                        FechaCreacion = oParteDiario.Fecha_Creacion.ToString("dd/MM/yyyy")
                    };
                    oRD.data1 = Error;
                }
                else
                {
                    oRD.isError = true;
                    oRD.data = "No hay Parte Diario registrado.";
                }
            }
            catch (Exception)
            {
                oRD.isError = true;
                oRD.data = "Error";
            }
            return oRD;
        }

        private bool NoExisteYaElDestinatario(string lstDestinatarios, string pDestinatario)
        {
            if (lstDestinatarios.Trim().Length > 0 && pDestinatario != null)
            {
                if (lstDestinatarios.Contains(pDestinatario))
                    return false;
                else
                    return true;
            }
            else
            {
                return true;
            }
        }
        private string TextoHtmlMail(string pTipoIncidente, Incidentes_Historial data, string pCreador, bool pEsNuevo, string pUsuarioModifico)
        {
            string mTexto = "";
            mTexto = "<p><H3>Informacion de referencia</H3></p>";
            mTexto = mTexto + "<p><hr/></p>";
            mTexto = mTexto + "<p><strong> Tipo de Incidente:</strong>" + pTipoIncidente + "</p>";
            mTexto = mTexto + "<p><strong> Creado Por:</strong>" + pCreador + "</p>";
            mTexto = mTexto + "<p><strong> Fecha Vencimiento:</strong>" + data.Fecha_Cierre.ToShortDateString() + "</p>";

            #region Busco el Estado
            string mEstado = "";
            switch (data.EstadoId)
            {
                case 1:
                    mEstado = "Abierto";
                    break;
                case 2:
                    mEstado = "Tratamiento";
                    break;
                case 3:
                    mEstado = "Enviado";
                    break;
                case 4:
                    mEstado = "Validacion";
                    break;
                case 50:
                    mEstado = "Cerrado";
                    break;
                case 99:
                    mEstado = "Cancelado";
                    break;
                default:
                    mEstado = "Cancelado";
                    break;
            }

            mTexto = mTexto + "<p><strong>Estado Actual:</strong>" + mEstado + "</p>";
            #endregion
            #region Proyecto Relacionado
            if (data.ProyectoId != 0)
            {
                string mNombre = new ProyectoNegocio().Get_One_Proyecto(data.ProyectoId).Nombre;
                mTexto = mTexto + "<p><strong> Proyecto Relacionado:</strong>" + mNombre + "</p>";
            }
            #endregion

            #region Contratista Relacionado
            if (data.ContratistasId != 0)
            {
                string mNombre = new ContratistasNegocio().Get_One_Contratistas(data.ContratistasId).Nombre;
                mTexto = mTexto + "<p><strong> Contratista Relacionado:</strong>" + mNombre + "</p>";
            }
            #endregion

            mTexto = mTexto + "<p><strong> Descripcion del Incidente:</strong>" + data.Creacion_Descripcion + "</p>";
            if (data.AreaId != 0)
            {
                string mNombre = new RolesNegocio().Get_One_Roles(data.AreaId).Detalle;
                mTexto = mTexto + "<p><strong> Area Asignada:</strong>" + mNombre + "</p>";
            }
            mTexto = mTexto + "<hr/>";
            if (data.sDetalleTratamiento != null)
            {
                mTexto = mTexto + "<p><strong>Comentarios del Tratamiento:</strong>" + data.sDetalleTratamiento + "</p>";
                mTexto = mTexto + "<p><strong>Actualizado Por:</strong>" + pUsuarioModifico + "</p>";
                mTexto = mTexto + "<hr/>";
            }
            mTexto = mTexto + "<br>";
            mTexto = mTexto + "<H2>Para ingresar al sistema por favor haga <a href='https://pucheta.iotecnologias.com.ar/'> click aqui </a><H2>";
            mTexto = mTexto + "<br>";
            mTexto = mTexto + "<br>";
            mTexto = mTexto + "<p>Muchas gracias Saludos</p>";
            return mTexto;
        }
        private void EnvioMailIncidente(Incidentes_Historial oIncidenteHistoria, bool pEsNuevo)
        {
            try
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json", optional: false);
                var configuration = builder.Build();
                bool EnviaEmail = Convert.ToBoolean(configuration["EnviaEmail"]);
                if (EnviaEmail)
                {
                    Incidentes oIncidente = new IncidentesNegocio().Get_One_Incidentes(oIncidenteHistoria.IncidenteId);

                    #region Destinatarios

                    string lstDestinatarios = "";
                    //PRIMERO AGREGO AL CREADOR DEL INCIDENTE
                    Usuarios oUsuario = new UsuariosNegocio().Get_Usuario(oIncidenteHistoria.Creacion_UsuarioId);
                    if (oUsuario != null)
                    {
                        string MailCreador = oUsuario.Email;
                        if (MailCreador.Trim() != "")
                        {
                            if (NoExisteYaElDestinatario(lstDestinatarios, MailCreador))
                                lstDestinatarios = MailCreador;
                        }
                    }
                    if (pEsNuevo)
                    {
                        // Si es nuevo, tengo que agregar los destinatario que estan configurados en el tipo de incidentes
                        if (NoExisteYaElDestinatario(lstDestinatarios, oIncidente.ListaEmail) && oIncidente.ListaEmail != null)
                            lstDestinatarios = lstDestinatarios + "; " + oIncidente.ListaEmail;
                    }
                    // Si es una modificacion, tengo que verificar si se le esta asignando a una Area en particular y traer los integrantes y enviarles el mail.
                    if (oIncidenteHistoria.AreaId != 0)
                    {
                        //Busco los usuarios que tiene esta area y armo el listado
                        List<Usuarios> lstUsuarios = new UsuariosNegocio().Get_UsuariosWithRoles().Where(p => p.Roles.Any(x => x.RolesId == oIncidenteHistoria.AreaId)).ToList();
                        if (lstUsuarios != null)
                        {
                            foreach (var item in lstUsuarios)
                            {
                                if (!lstDestinatarios.Contains(item.Email))
                                {
                                    if (NoExisteYaElDestinatario(lstDestinatarios, item.Email))
                                        lstDestinatarios = lstDestinatarios + "; " + item.Email;
                                }
                            }
                        }
                    }

                    //lstDestinatarios = "sezbustamante@gmail.com";
                    #endregion

                    #region Busco el Usuario que esta modificando
                    int mIdUsuario = oUsuario.Id;
                    string mUsuarioModifico = new UsuariosNegocio().Get_Usuario(mIdUsuario).ApellidoYNombre;

                    #endregion

                    #region Armo el Asunto del Mail
                    var Asunto = "";
                    if (pEsNuevo)
                        Asunto = "Se ha generado un nuevo incidente del tipo:" + oIncidente.Nombre + " creado por:" + oUsuario.NombreYApellido;
                    else
                        Asunto = "El Incidente Nro:" + oIncidenteHistoria.Id + " ha sido modificado por:" + mUsuarioModifico;
                    #endregion

                    var Texto = "";
                    Texto = TextoHtmlMail(oIncidente.Nombre, oIncidenteHistoria, oUsuario.NombreYApellido, pEsNuevo, mUsuarioModifico);

                    EnviaEmailNegocio.Enviar(lstDestinatarios, Asunto, Texto, true);
                    //EnviaEmailNegocio.Enviar("lfe.2.fe@gmail.com", Asunto, Texto, true);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception", ex);
            }
        }

        public partial class DataIncidenteHistorial
        {
            public int UsuarioId { get; set; }
            public ItemParteDiarioGrabaIncidente oItemIncidenteHistorial { get; set; }
        }
        [HttpPost]
        public ReturnData Grabar_IncidenteHistorial(DataIncidenteHistorial data)
        {
            ReturnData oRD = new ReturnData();
            try
            {
                Usuarios oUsuarioLogueado = new UsuariosNegocio().Get_Usuario(data.UsuarioId);
                List<RolesUsuarios> lRolesUsuarios = new RolesUsuariosNegocio().GetRolesUsuarios(oUsuarioLogueado.Id);
                ItemParteDiarioGrabaIncidente oIIH = data.oItemIncidenteHistorial;
                List<ParteDiario> lPartesDiarios = new ParteDiarioNegocio()
                    .Get_PartesDiarios_X_ProyectoId(oIIH.ProyectoId)
                    .OrderByDescending(pd => pd.Fecha_Creacion)
                    .ToList();
                if (lPartesDiarios.Count > 0)
                {
                    ParteDiario oParteDiario = lPartesDiarios.First();
                    if (oIIH.ComentarioHI != null)
                    {
                        oIIH.ComentarioHI = ToolsNegocio.EliminaEnter(oIIH.ComentarioHI);
                    }
                    Incidentes_Historial oIncidenteHistorial = new Incidentes_Historial();
                    oIncidenteHistorial.Creacion_Descripcion = oIIH.ComentarioHI;
                    oIncidenteHistorial.ContratistasId = oIIH.ContratistaId;
                    oIncidenteHistorial.Id = oIIH.Id;
                    oIncidenteHistorial.IncidenteId = oIIH.IncidenteId;
                    oIncidenteHistorial.ParteDiarioId = oParteDiario.Id;
                    oIncidenteHistorial.sContratista = oIIH.sContratista;
                    oIncidenteHistorial.sIncidente = oIIH.sIncidente;
                    oIncidenteHistorial.sUsuarioDueño = oUsuarioLogueado.ApellidoYNombre;
                    oIncidenteHistorial.Creacion_UsuarioId = oUsuarioLogueado.Id;
                    oIncidenteHistorial.Creacion_Fecha = DateTime.Now;
                    oIncidenteHistorial.Fecha_Cierre = oIIH.FechaCierre;
                    oIncidenteHistorial.sAreaActual = oIIH.sAreaActual;
                    oIncidenteHistorial.AreaId = oIIH.AreaId;
                    oIncidenteHistorial.ProyectoId = oIIH.ProyectoId;
                    oIncidenteHistorial.sProyecto = oIIH.sProyecto;
                    oIncidenteHistorial.EstadoId = 2;
                    //TODO: desde el controller de la web app se envia mail aca también?
                    //EnvioMailIncidente(oIncidenteHistorial, true);

                    int IdIH = new Incidentes_HistorialNegocio().Insert(oIncidenteHistorial);
                    EnvioMailIncidente(oIncidenteHistorial, true);

                    #region ARCHIVOS ADJUNTOS
                    //Cargamos FOTOS 
                    string Error = "";
                    if (oIIH.lFotos != null && oIIH.lFotos.Count > 0)
                    {
                        int contador = 1;
                        foreach (List<string> oFoto in oIIH.lFotos)
                        {
                            string folder = Path.Combine(_env.WebRootPath, ValoresUtiles.PathArchivos_Incidentes);
                            string nombreFoto =
                                DateTime.Now.Year.ToString() +
                                DateTime.Now.Month.ToString() +
                                DateTime.Now.Day.ToString() +
                                DateTime.Now.Hour.ToString() +
                                DateTime.Now.Minute.ToString() +
                                DateTime.Now.Second.ToString() +
                                DateTime.Now.Millisecond.ToString() +
                                "_" + IdIH.ToString() + "_" + contador + "." + oFoto[0];
                            contador++;
                            string GraboCorrectamente = this.Grabar_Foto(nombreFoto, oFoto[2]);
                            Error = GraboCorrectamente;
                            if (GraboCorrectamente == "OK")
                            {
                                try
                                {
                                    Archivos_Adjuntos oArchivoAdjunto = new Archivos_Adjuntos();
                                    oArchivoAdjunto.Archivo = nombreFoto;
                                    oArchivoAdjunto.Extencion = oFoto[0].ToUpper();
                                    oArchivoAdjunto.Fecha = DateTime.Now;
                                    oArchivoAdjunto.IdUsuario = oUsuarioLogueado.Id;
                                    oArchivoAdjunto.sUsuario = oUsuarioLogueado.Apellido + oUsuarioLogueado.Nombre;
                                    oArchivoAdjunto.URL = ValoresUtiles.PathArchivos_Incidentes;
                                    int IdAA = new Archivos_AdjuntosNegocio().Insert(oArchivoAdjunto);

                                    Archivos_Adjuntos_Relacion oAARelacion = new Archivos_Adjuntos_Relacion();
                                    oAARelacion.Archivos_AdjuntosId = IdAA;
                                    oAARelacion.Entidad = "AIH";
                                    oAARelacion.IdEntidad = IdIH;

                                    new Archivos_Adjuntos_RelacionNegocio().Insert(oAARelacion);
                                }
                                catch (Exception ex)
                                {
                                    oRD.isError = true;
                                    oRD.data1 = ex.InnerException.Message;
                                }
                            }
                        }
                    }
                    #endregion

                    oRD.isError = false;
                    oRD.data = new
                    {
                        ParteDiarioId = oParteDiario.Id,
                        FecCreacion = oParteDiario.Fecha_Creacion,
                        FechaCreacion = oParteDiario.Fecha_Creacion.ToString("dd/MM/yyyy")
                    };
                    oRD.data1 = Error;
                }
                else
                {
                    oRD.isError = true;
                    oRD.data = "No hay Parte Diario registrado.";
                }
            }
            catch (Exception)
            {
                oRD.data = "Error al grabar";
                oRD.isError = true;
            }
            return oRD;
        }

        public string Grabar_Foto(string nombreFoto, string foto)
        {
            string todoOk = "";
            string Folder = Path.Combine(_env.WebRootPath, "archivos/PINC/");
            try
            {
                string[] f = foto.Split(',');
                byte[] bytes = Convert.FromBase64String(f[1]);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (Bitmap bm = new Bitmap(ms))
                    {
                        bm.Save(Folder + nombreFoto);
                    }
                }
                todoOk = "OK";
            }
            catch (Exception e)
            {
                todoOk = "Error: " + e.InnerException;
            }
            return todoOk;
        }

        [HttpPost]
        public ReturnData Get_Incidentes([FromBody] getDataIncidentes data)
        {
            ReturnData I = new ReturnData();

            try
            {
                Usuarios u = new UsuariosNegocio().Get_Usuario(data.id);
                List<ItemIncidentesHistorialMovil> lista = new List<ItemIncidentesHistorialMovil>();
                var IncidentesH = new Incidentes_HistorialNegocio().Get_All_Incidentes_Historial();

                foreach (var item in IncidentesH)
                {
                    ItemIncidentesHistorialMovil Ih = new ItemIncidentesHistorialMovil();
                    Ih.Id = item.Id;
                    Ih.creacion_Fecha = item.Creacion_Fecha;
                    Ih.Comentario = item.Creacion_Descripcion;
                    if (item.ContratistasId != 0)
                        Ih.sContratista = new ContratistasNegocio().Get_One_Contratistas(item.ContratistasId).Nombre;
                    else
                        Ih.sContratista = "";
                    //Ih.sContratista = new ContratistasNegocio().Get_One_Contratistas(item.ContratistasId).Nombre;
                    Ih.sIncidente = new IncidentesNegocio().Get_One_Incidentes(item.IncidenteId).Nombre;
                    if (item.ProyectoId != 0)
                        Ih.sProyecto = new ProyectoNegocio().Get_One_Proyecto(item.ProyectoId).Nombre;
                    else
                        Ih.sProyecto = "";
                    //Ih.sProyecto = new ProyectoNegocio().Get_One_Proyecto(item.ProyectoId).Nombre;
                    Ih.sAreaActual = item.sAreaActual;
                    Ih.sEstado = item.sEstado;
                    Ih.sUsuarioDueño = item.sUsuarioDueño;
                    lista.Add(Ih);
                }
                I.data = lista;
                I.isError = false;
            }
            catch (Exception)
            {
                I.data = "Error";
                I.isError = true;
            }

            return I;

        }

        #endregion

        #region LlenarCombos

        /// <summary>
        /// Método que devuelve un objeto de tipo anónimo con listas internas. Cada una con un tipo distinto de datos.
        /// Los mismo se utilizan para cargar los controlles inciales.
        /// </summary>
        /// <returns>object</returns>
        [HttpPost]
        public object Get_DatosIniciales()
        {
            object oResult = new object();
            try
            {
                List<string> lEstadosPPA = new List<string>()
                {
                    ValoresUtiles.Estado_PPA_Vencida, ValoresUtiles.Estado_PPA_Atrasada
                };
                List<Incidentes> lTiposIncidentes = new IncidentesNegocio().Get_All_Incidentes();
                List<Proyecto> lProyectos = new ProyectoNegocio().Get_All_Proyecto();
                List<Contratistas> lContratistas = new ContratistasNegocio().Get_All_Contratistas();
                oResult = new
                {
                    lEstadosPPA,
                    lTiposIncidentes,
                    lProyectos,
                    lContratistas
                };
            }
            catch(Exception ex)
            {
                oResult = new
                {
                    isError = true,
                    message = "Error: MovilController: Get_DatosIniciales: exception.",
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #endregion

        #region Pantalla ABM Novedades

        [HttpPost]
        public object Get_Datos_ABM_Novedades([FromBody] ItemLoginIONIC data)
        {
            object result = new object();
            try
            {
                Usuarios oUsuario = new UsuariosNegocio().Get_Usuario(data.id);
                List<Contratistas> lContratistas = new ContratistasNegocio().Get_All_Contratistas();
                List<Proyecto> lProyectosT = new ProyectoNegocio().Get_All_Proyecto();
                List<Proyecto> lProyectos = new ProyectoNegocio().Get_x_UsuarioId(oUsuario.Id);
                List<Roles> lRoles = new RolesNegocio().Get_All_Roles()
                    .Where(p => p.Borrado == false &&
                        p.Activo == true &&
                        p.Id != ValoresUtiles.IdRol_AdminFull)
                    .OrderBy(p => p.Detalle)
                    .ToList();
                List<Incidentes> lIncidentes = new IncidentesNegocio().Get_All_Incidentes();
                List<string> lCriticidades = ValoresUtiles.Get_lCriticidades_ParteDiarioIncidente();
                List<string> lEstadosPPA = new List<string>()
                {
                    ValoresUtiles.Estado_PPA_Vencida, ValoresUtiles.Estado_PPA_Atrasada
                };
                result = new
                {
                    lTiposIncidentes = lIncidentes
                        .Where(i => i.Rectype == ValoresUtiles.Rectype_Incidente_IncidenteAccidente)
                        .Select(p => new { p.Id, p.Nombre }).ToList(),
                    lTiposNovedades = lIncidentes
                        .Where(i => i.Rectype == ValoresUtiles.Rectype_Incidente_Novedad)
                        .Select(p => new { p.Id, p.Nombre }).ToList(),
                    lContratistas = lContratistas.Select(c => new { c.Id, c.Nombre }),
                    lProyectos = lProyectos.Select(p => new { p.Id, p.Nombre }),
                    lRoles = lRoles.Select(r => new { r.Id, r.Detalle }),
                    lProyectosT = lProyectosT.Select(p => new { p.Id, p.Nombre }),
                    lCriticidades,
                    lEstadosPPA
                };
            }
            catch (Exception)
            {
                result = new
                {
                    isError = true,
                    data = "Error"
                };
            }
            return result;
        }

        #endregion

        #region PLANIFICACION PROYECTO ACTIVIDADES

        /// <summary>
        /// Método que devuelve una lista de Planificacion_Proyecto_Actividades (como objetos anónimos)
        /// cuyos estados sean "Atrasada".
        /// </summary>
        /// <returns>Lista de Planificacion_Proyecto_Actividades</returns>
        public List<Planificacion_Proyecto_Actividades> Get_ActividadesAtrasadas_x_ProyectoId(int ProyectoId)
        {
            try
            {
                List<Planificacion_Proyecto_Actividades> lPPAs = new List<Planificacion_Proyecto_Actividades>();
                lPPAs = new Planificacion_Proyecto_ActividadesNegocio()
                    .Get_Atrasadas(ProyectoId, true);
                return lPPAs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: MovilController: Get_ActividadesAtrasadas_x_ProyectoId: exception.", ex);
            }
        }

        /// <summary>
        /// Método que devuelve una lista de Informe_Actividad_Vencida
        /// cuyos proyecto sean igual a ProyectoId
        /// </summary>
        /// <param name="ProyectoId">Id del Proyecto</param>
        /// <returns>List de Informe_Actividad_Vencida</returns>
        private List<Informe_Actividad_Vencida> Get_Informe_Actividades_Vencidas(int ProyectoId)
        {
            try
            {
                List<Informe_Actividad_Vencida> lIAVencidas = new List<Informe_Actividad_Vencida>();
                lIAVencidas = new Informe_Actividad_VencidaNegocio()
                    .Get_Informe_Completo_Por_Proyecto(ProyectoId);
                return lIAVencidas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: MovilController: Get_Informe_Actividades_Vencidas: exception.", ex);
            }
        }

        public partial class FiltroPPA
        {
            public int IdProyecto { get; set; }
            public string Estado { get; set; }
        }
        [HttpPost]
        public object Get_Actividades_x_oFiltro([FromBody] FiltroPPA oFiltro)
        {
            object oResult = new object();
            try
            {
                if(oFiltro.Estado == ValoresUtiles.Estado_PPA_Vencida)
                {
                    List<Informe_Actividad_Vencida> lIAVencidas = 
                        this.Get_Informe_Actividades_Vencidas(oFiltro.IdProyecto);
                    oResult = new
                    {
                        lIAVencidas = lIAVencidas.Select(iav => new {
                            oFiltro.Estado,
                            iav.sProyUbicacion,
                            iav.sPlanActividad,
                            iav.PorcAvanceObra,
                            iav.FechaUltimoAvance,
                            iav.FechaEstimadaFin,
                            iav.DiasVencida,
                            iav.FechaCreacion
                        }).ToList()
                    };
                }
                else if(oFiltro.Estado == ValoresUtiles.Estado_PPA_Atrasada)
                {
                    List<Planificacion_Proyecto_Actividades> lPPAs =
                        this.Get_ActividadesAtrasadas_x_ProyectoId(oFiltro.IdProyecto);
                    oResult = new
                    {
                        lPPA_Atrasadas = lPPAs.Select(ppa => new
                        {
                            ppa.Estado,
                            ppa.Ubicacion,
                            ppa.Actividad,
                            ppa.Avance,
                            ppa.FechaEstInicio,
                            DiasAtrasada = (int)(DateTime.Now - ppa.Fecha_Est_Incio).TotalDays,
                            ppa.FechaRealInicio,
                            ppa.FechaEstFin,
                            ppa.FechaRealFin
                        }).ToList()
                    };
                }
            }
            catch(Exception ex)
            {
                oResult = new
                {
                    error = true,
                    message = "Error: MovilController: Get_Actividades_x_oFiltro: exception.",
                    ex = ex.InnerException
                };
            }
            return oResult;
        }

        #endregion

        #region PROYECTOS

        [HttpGet]
        public object Get_PlazosPrevistos(int Id)
        {
            object oResult = new object();
            try
            {
                Usuarios oUsuario = new UsuariosNegocio().Get_Usuario(Id);
                if (oUsuario != null)
                {
                    List<dynamic> lPlazosPrevistos = new List<dynamic>();
                    List<Proyecto> lProyectos = new List<Proyecto>();
                    List<Roles> lRoles = new RolesUsuariosNegocio().Get_Roles_x_UsuarioId(oUsuario.Id);
                    if (lRoles.Find(r => r.Id == ValoresUtiles.IdRol_CoordinacionObra ||
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
                        if (lPPA.Count > 0)
                        {
                            fechaPlanificadaFin = lPPA.Max(ppa => ppa.Fecha_Est_Fin);
                            diasDiferencia = (int)(fechaPlanificadaFin - p.Fecha_Fin).TotalDays;
                        }
                        lPlazosPrevistos.Add(new
                        {
                            p.Id,
                            p.Nombre,
                            FechaFinPrevista = p.FechaFin,
                            FechaPlanificadaFin = fechaPlanificadaFin.ToString(ValoresUtiles.Formato_dd_MM_yyyy),
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
                else
                {
                    oResult = new
                    {
                        isError = true,
                        message = "Acceso denegado"
                    };
                }
            }
            catch (Exception ex)
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

    }
}
