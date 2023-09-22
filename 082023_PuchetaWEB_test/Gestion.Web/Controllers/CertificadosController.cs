using Gestion.EF;
using Gestion.EF.Migrations;
using Gestion.EF.Models;
using Gestion.Negocio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MySql.Data.MySqlClient;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;


namespace Gestion.Web.Controllers
{
    public class CertificadosController : Controller
    {

        #region INICIALIZACIÓN

        private readonly IWebHostEnvironment _env;
        public CertificadosController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public ActionResult index()
        {
            var oResult = new object();

            ReturnData mReturn = new ReturnData();
            var data = new Certificados();

            List<Certificados> ListaCertificados = new List<Certificados>();
                 List<Certificados> ListaCertificados2 = new List<Certificados>();
            Certificados Registro = new Certificados();
            ListaCertificados = new CertificadoNuevoNegocio().Get_All_Ordenes().ToList();


            List<Proyecto> lProyectos = GetProyectosMostrar().Where(p => p.Estado != "Ejecutado").OrderBy(p => p.Nombre).ToList();

            List<RolesUsuarios> ListaRoles = getUsuarioActual().Roles;

            int UsuarioId = getUsuarioActual().Id;




            var RolesUsuarioId = from s in ListaRoles
                                 where s.UsuariosId == UsuarioId
                                 select new
                                 {
                                     s.RolesId
                                 };


            /*
             Id=6 Jefe de Obra
             Id=11 Admnistracion
             Id=8 Coordinacion de Obra Pucheta
             */
            Roles NombreRol = new RolesNegocio().Get_One_Roles(RolesUsuarioId.OrderByDescending(s=>s.RolesId).FirstOrDefault().RolesId);




            mReturn.data = NombreRol.Detalle;
            mReturn.data1 = NombreRol.Id;
   

            foreach (var item in ListaCertificados)
            {
                if (item.NroCertificado ==null || item.NroCertificado == "")
                {
                    if (ListaCertificados.Where(x=> x.IdProyecto == item.IdProyecto && x.IdContratista==item.IdContratista).FirstOrDefault().Secuenciador == null) { item.Secuenciador = 1; }
                    else { item.Secuenciador = 1 + ListaCertificados.Where(x => x.IdProyecto == item.IdProyecto && x.IdContratista == item.IdContratista).FirstOrDefault().Secuenciador; }

                    String IdProyecto, IdContratista, SecuencialString;
                    switch (item.IdProyecto.ToString().Length)
                    {
                        case 1:
                            IdProyecto = "000" + item.IdProyecto.ToString();
                            break;
                        case 2:
                            IdProyecto = "00" + item.IdProyecto.ToString();
                            break;
                        case 3:
                            IdProyecto = "0" + item.IdProyecto.ToString();
                            break;
                        default:
                            IdProyecto = "" + item.IdProyecto.ToString();
                            break;
                    }
                    switch (item.IdContratista.ToString().Length)
                    {
                        case 1:
                            IdContratista = "000" + item.IdContratista.ToString();
                            break;
                        case 2:
                            IdContratista = "00" + item.IdContratista.ToString();
                            break;
                        case 3:
                            IdContratista = "0" + item.IdContratista.ToString();
                            break;
                        default:
                            IdContratista = "" + item.IdContratista.ToString();
                            break;
                    }
                    switch (item.Secuenciador.ToString().Length)
                    {
                        case 1:
                            SecuencialString = "000" + item.Secuenciador.ToString();
                            break;
                        case 2:
                            SecuencialString = "00" + item.Secuenciador.ToString();
                            break;
                        case 3:
                            SecuencialString = "0" + item.Secuenciador.ToString();
                            break;
                        default:
                            SecuencialString = "" + item.Secuenciador.ToString();
                            break;
                    }

                    item.NroCertificado = IdProyecto + '-' + IdContratista + '-' + SecuencialString;
                }
                

                try
                {
                    if (item.IdProyecto != 0)
                        item.sProyecto = lProyectos.Where(p => p.Id == Convert.ToInt32(item.IdProyecto)).FirstOrDefault().Nombre;
                }
                catch (Exception)
                {
                    item.sProyecto = "S/D";
                }

                try
                {
                    if (item.IdContratista != 0)
                       
                        item.sContratista = new ContratistasNegocio().Get_One_Contratistas((int)item.IdContratista).Nombre;
                }
                catch (Exception)
                {
                    item.sContratista = "S/D";
                }

                try
                {
                    if (item.IdUsuarioCreo != 0)
                        item.sUsuario = new UsuariosNegocio().Get_Usuario((int)item.IdUsuarioCreo).Nombre;
                }
                catch (Exception)
                {
                    item.sUsuario = "S/D";
                }
            }
            ListaCertificados = ListaCertificados.OrderByDescending(p => p.FechaCreacion).ToList();

            //Para cargar los combos del index
            if (RolesUsuarioId.Any(s => s.RolesId == 6 ))
            {
                lProyectos = GetProyectosMostrar().Where(p => p.Estado != "Ejecutado" && p.UsuariosId == UsuarioId).OrderBy(p => p.Nombre).ToList();
                foreach(var item in lProyectos)
                {
                    Registro = ListaCertificados.Where(p => p.IdProyecto == item.Id).FirstOrDefault();
                    if (Registro != null)
                    {
                        ListaCertificados2.Add(Registro);
                    }
                }          
              
                mReturn.data2 = ListaCertificados2;

            }
            else
            {
                mReturn.data2 = ListaCertificados;
            }
            mReturn.data3 = lProyectos;
           
            return View(mReturn);
        }
        #endregion


        #region Definiciones
        public ReturnData _CertificadosVisualizar()
        {
            ReturnData mReturn = new ReturnData();
            var data = new Certificados();

            List<Certificados> ListaCertificados = new List<Certificados>();
            ListaCertificados = new CertificadoNuevoNegocio().Get_All_Ordenes().ToList();
            List<Proyecto> lProyectos = GetProyectosMostrar().Where(p => p.Estado != "Ejecutado").OrderBy(p => p.Nombre).ToList();

            List<RolesUsuarios> ListaRoles = getUsuarioActual().Roles;
                
            int UsuarioId = getUsuarioActual().Id;


            

            var RolesUsuarioId = from s in ListaRoles
                                   where s.UsuariosId== UsuarioId
                                 select new
                                    {
                                       s.RolesId
                                    };
            

            /*
             Id=6 Jefe de Obra
             Id=11 Admnistracion
             Id=8 Coordinacion de Obra Pucheta
             */
            if (RolesUsuarioId.Any(s=> s.RolesId == 6))
              //  if (RolesUsuarioId.Any(s => s.RolesId == 1))
                {
                GetProyectosMostrar().Where(p => p.Estado != "Ejecutado" && p.UsuariosId==UsuarioId).OrderBy(p => p.Nombre).ToList();
            }
            mReturn.data = getUsuarioActual().Nombre;
            mReturn.data2 = ListaCertificados;
            mReturn.data3 = lProyectos;


            return mReturn;
        }
        #endregion

        #region ListadosCertificadosPrincipal
        public ActionResult _CertificadoGrilla()
        {
            var oResult = new object();
            List<Certificados> ListaCertificados = new List<Certificados>();
            try
            {

                
                ListaCertificados = new CertificadoNuevoNegocio().Get_All_Ordenes().ToList();

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
            return PartialView(ListaCertificados);
        }
        [HttpPost]
        public ReturnData CertificadoGrillaFiltro(FiltroBusquedaCertificado oFiltro)
        {
            ReturnData mReturn = new ReturnData();
            List<Certificados> Lista = new List<Certificados>();
            DateTime mFecHasta = DateTime.MinValue;
            DateTime mFecDesde= DateTime.MinValue;

            if (oFiltro.fechaDesde != null)
            {
               mFecDesde = Convert.ToDateTime(oFiltro.fechaDesde);
            
            };
            if (oFiltro.fechaHasta != null)
            {
                 mFecHasta = Convert.ToDateTime(oFiltro.fechaHasta);
            };

      


            int mIdProyecto = 0;
            int mIdContratista = 0;

            if(oFiltro.Idproyecto != null) { mIdProyecto = (int)oFiltro.Idproyecto;}
            if (oFiltro.IdContratista != null) { mIdContratista = (int)oFiltro.IdContratista; }
           


            if (mFecDesde == DateTime.MinValue)
            {
                mFecDesde = DateTime.Now.AddMonths(-1);
            }
            if (mFecHasta == DateTime.MinValue)
            {
                mFecHasta = DateTime.Now.AddDays(1);
            }

            Lista = new CertificadoNuevoNegocio().Get_All_Ordenes().Where(p => p.FechaCreacion >= mFecDesde && p.FechaCreacion <= mFecHasta).ToList();

            if (mIdProyecto != 0)
            { Lista = Lista.Where(p => p.IdProyecto == mIdProyecto).ToList(); }

            if (mIdContratista != 0)
            { Lista = Lista.Where(p => p.IdContratista == mIdContratista).ToList(); }

            List<Proyecto> lProyectos = GetProyectosMostrar().OrderBy(p => p.Nombre).ToList();

            mReturn.data = getUsuarioActual().Nombre;
            mReturn.data3 = lProyectos;


            foreach (var item in Lista)
            {
                if (item.NroCertificado == null || item.NroCertificado == "")
                {
                    if (Lista.Where(x => x.IdProyecto == item.IdProyecto && x.IdContratista == item.IdContratista).FirstOrDefault().Secuenciador == null) { item.Secuenciador = 1; }
                    else { item.Secuenciador = 1 + Lista.Where(x => x.IdProyecto == item.IdProyecto && x.IdContratista == item.IdContratista)
                                                        .OrderByDescending(s=> s.Secuenciador)
                                                        .FirstOrDefault().Secuenciador; 
                    }

                    String IdProyecto, IdContratista, SecuencialString;
                    switch (item.IdProyecto.ToString().Length)
                    {
                        case 1:
                            IdProyecto = "000" + item.IdProyecto.ToString();
                            break;
                        case 2:
                            IdProyecto = "00" + item.IdProyecto.ToString();
                            break;
                        case 3:
                            IdProyecto = "0" + item.IdProyecto.ToString();
                            break;
                        default:
                            IdProyecto = "" + item.IdProyecto.ToString();
                            break;
                    }
                    switch (item.IdContratista.ToString().Length)
                    {
                        case 1:
                            IdContratista = "000" + item.IdContratista.ToString();
                            break;
                        case 2:
                            IdContratista = "00" + item.IdContratista.ToString();
                            break;
                        case 3:
                            IdContratista = "0" + item.IdContratista.ToString();
                            break;
                        default:
                            IdContratista = "" + item.IdContratista.ToString();
                            break;
                    }
                    switch (item.Secuenciador.ToString().Length)
                    {
                        case 1:
                            SecuencialString = "000" + item.Secuenciador.ToString();
                            break;
                        case 2:
                            SecuencialString = "00" + item.Secuenciador.ToString();
                            break;
                        case 3:
                            SecuencialString = "0" + item.Secuenciador.ToString();
                            break;
                        default:
                            SecuencialString = "" + item.Secuenciador.ToString();
                            break;
                    }

                    item.NroCertificado = IdProyecto + '-' + IdContratista + '-' + SecuencialString;
                }


                try
                {
                    if (item.IdProyecto != 0)
                        item.sProyecto = lProyectos.Where(p => p.Id == Convert.ToInt32(item.IdProyecto)).FirstOrDefault().Nombre;
                }
                catch (Exception)
                {
                    item.sProyecto = "S/D";
                }

                try
                {
                    if (item.IdContratista != 0)

                        item.sContratista = new ContratistasNegocio().Get_One_Contratistas((int)item.IdContratista).Nombre;
                }
                catch (Exception)
                {
                    item.sContratista = "S/D";
                }

                try
                {
                    if (item.IdUsuarioCreo != 0)
                        item.sUsuario = new UsuariosNegocio().Get_Usuario((int)item.IdUsuarioCreo).Nombre;
                }
                catch (Exception)
                {
                    item.sUsuario = "S/D";
                }
            }
            if (oFiltro.nroCertificado != null) {
                Lista = Lista.Where(s => s.NroCertificado == oFiltro.nroCertificado).OrderByDescending(p => p.FechaCreacion).ToList();
            }
            else
            {
                Lista = Lista.OrderByDescending(p => p.FechaCreacion).ToList();
            }
           
            mReturn.data2 = Lista;


            return mReturn;
        }

        #endregion

        #region CertificadosAbm
        [HttpPost]
        public ReturnData ControrlExistenciaCertificado(int IdProyecto, int IdContratista)
        {
            ReturnData d = new ReturnData();

            List<Certificados> listaCertificadoAbierto = new CertificadoNuevoNegocio().Get_All_Ordenes()
                                          .Where(s => s.IdContratista == IdContratista && s.IdProyecto == IdProyecto
                                          && ( s.Estado=="Abierto" || s.Estado == "A pagar")
                                          ).ToList();

            if (listaCertificadoAbierto.Count>0)
            {
                d.isError=true;
                    d.data = "Ya existe el certificado abierto para ese proyecto y ese contratista.";
            }
            else
             {
                d.isError = false;
                List<Certificados> listaCertificadoFecha = new CertificadoNuevoNegocio().Get_All_Ordenes()
                                        .Where(s => s.IdContratista == IdContratista && s.IdProyecto == IdProyecto
                                        && s.FechaCreacion.Day == DateTime.Today.Day

                                        ).ToList();

                if (listaCertificadoFecha.Count > 0)
                {
                    d.isError = true;
                    d.data = "No puede crearse un certificado el mismo dia que otro Certificado para ese proyecto y ese contratista..";
                }
                else
                {
                    d.isError = false;
                }
            }
            

            //Consulto por el ParteDiario.
            return d;
        }

        public IActionResult _CertificadosAbm(int IdProyecto, int IdContratista, string Modo, int? IdCertificado)
        {
            #region 1)-Creacion/EdicionCertificado

            var d = new ReturnData();

            ReturnData mReturn = new ReturnData();

            List<Proyecto> lProyectos = GetProyectosMostrar().OrderBy(p => p.Nombre).ToList();
            Certificados Certificado = new Certificados();

            int IdCertificadoInsertado = 0;
            #endregion


            #region 2)-Creacion/EdicionCertificado

            try
                {

                if (Modo == "Agregar")
                {
                    Certificados CD = new Certificados();
                    CD.Estado = "Abierto";
                    CD.IdProyecto = IdProyecto;
                    CD.IdContratista = IdContratista;
                    CD.FechaCreacion = DateTime.Now;
                    CD.IdUsuarioCreo = getUsuarioActual().Id;

                    mReturn = getNroCertificadoNuevo(IdProyecto, IdContratista);
                    CD.Secuenciador = (long)mReturn.data1;
                    CD.NroCertificado = (string)mReturn.data;

                    IdCertificadoInsertado = new CertificadoNuevoNegocio().Insert(CD);
                }
                    //Cargamos el Detalle de Certificado
                    if (Modo == "Agregar")
                    {
                     Certificado = new CertificadoNuevoNegocio().Get_One_Orden((int)IdCertificadoInsertado);
                    d = _CargarNotaPedidoCertificadoDetalle(IdProyecto, IdContratista, (int)IdCertificadoInsertado);
                    }
                    else
                    {
                        Certificado = new CertificadoNuevoNegocio().Get_One_Orden((int)IdCertificado);
                    }
               
             
                }
                catch (Exception)
                {
                    d.isError=true;
                    d.data = "Hubo un Error para Crear el nuevo certificado";
                    throw;
                }

                
                    Certificado.sProyecto = lProyectos.Where(p => p.Id == Convert.ToInt32(Certificado.IdProyecto)).FirstOrDefault().Nombre;
                    Certificado.sContratista = new ContratistasNegocio().Get_One_Contratistas((int)Certificado.IdContratista).Nombre;
                
            
            #endregion
            
            //----------RETORNOS INFORMACION PARA PANTALLA------------//
            mReturn.isError = false;           
            mReturn.data2 = Modo;
            mReturn.data = Certificado;
           
            mReturn.data3 = Convert.ToString(((DateTime)Certificado.FechaCreacion).ToString("0:dd/MM/yyyy")).Remove(0, 2); ;
            return View(mReturn);
        }
        private ReturnData _CargarNotaPedidoCertificadoDetalle(int IdProyecto, int IdContratista, int IdCertificado)
        {
            ReturnData d = new ReturnData();

           
            int IdCertificadoInsertado = 0;
            

            try
            {
               
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                   
                    
                    MySqlConnection connection = new MySqlConnection(db.Database.GetDbConnection().ConnectionString);
                    connection.Open();
                    
                    MySqlCommand command = new MySqlCommand("ListadoNotaPedidos_Certificados", connection);

                    command.CommandType = CommandType.StoredProcedure;
                 
                    command.Parameters.AddWithValue("@Ent_IdProyecto", IdProyecto);
                    command.Parameters.AddWithValue("@Ent_IdContratista", IdContratista);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        Certificados_detalle_planificado CD = new Certificados_detalle_planificado();
                        CD.IdCertificados = IdCertificado;
                        CD.IdPA = (int)reader.GetInt32(0);
                        CD.RubroId = (int)reader.GetInt32(1);
                        CD.RubroNombre = (string)reader.GetString(2);
                        CD.ActividadNombre = (string)reader.GetString(3);
                        CD.PartidaCodigo = (string)reader.GetString(4);              
                        CD.Detalle = (string)reader.GetString(9);
                        CD.PartidaId= (int)reader.GetInt32(5);
                        CD.IdPPA = (int)reader.GetInt32(6);
                        CD.UbicacionId = (int)reader.GetInt32(7);
                        CD.UbicacionNombre= (string)reader.GetString(8);
                        CD.Detalle = (string)reader.GetString(9);                       
                        CD.IdNotaPedido= reader.GetInt32(10);
                        CD.NroNotaPedido= (string)reader.GetString(11);
                        CD.IdNotaPedidoDetalle = (int)reader.GetInt32(12);
                        CD.Cantidad_Asignada = (double)reader.GetDouble(13);
                        CD.Monto_Unitario= (double)reader.GetDouble(14);
                        CD.Monto_Total= (double)reader.GetDouble(15);
                       
                        //Por cada Registro, Inserto un Certificado Detalle


                        IdCertificadoInsertado = new CertificadoNuevoNegocioDetalle().Insert(CD);        

                        if(IdCertificadoInsertado > 0)
                        {
                            ReturnData valores = getActAcumuladoAnterior((int)CD.IdNotaPedidoDetalle, IdCertificadoInsertado);
                            CD.Act_Acum_Ant_Unid = (double)valores.data;
                            CD.Act_Acum_Total_Unid = (double)valores.data1;
                            CD.Act_Acum_Ant_Moneda= (double)valores.data2;
                            CD.Act_Acum_Actual_Moneda = (double)valores.data3;

                            new CertificadoNuevoNegocioDetalle().Update(CD);
                        }
                    }

                    connection.Close();
                }

               
                d.isError = false;
            }
            catch (Exception e)
            {
                d.isError = true;
                d.data = "Hubo un Error para insertar los registros en el certificado Detalle";
                throw;
            }


            return d;
        }

        [HttpPost]
        public ReturnData EliminarCertificados(int id)
        {
            int Id = 0;
            bool PuedeBorrar = true;

            ReturnData mReturn = new ReturnData();
            mReturn.data3 = "";
            Certificados Certificado = new CertificadoNuevoNegocio().Get_One_Orden(id);
            List<Certificados> ListaCertificados = new List<Certificados>();

            //Contralmos que no tenga adicionales.
            //Controlamos que no tenga avances cargados.
            #region Validaciones

            //Detalles Adicional
            List<Certificados_detalle_adicional> ExisteAdicionales = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                                                                           .Where(s => s.cda_IdCertificados == id)
                                                                           .ToList();
            //Detalles Actividades
            List<Certificados_detalle_planificado> ExisteActividadesConAvance = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
                                                                              .Where(s => s.Act_Avance_Periodo_Unid > 0
                                                                                && s.Act_Avance_Periodo_Unid != null
                                                                                && s.IdCertificados == Certificado.Id
                                                                              )
                                                                              .ToList();

            if (ExisteActividadesConAvance.Count > 0 || ExisteAdicionales.Count > 0)
            {
                PuedeBorrar = false;
                mReturn.data3 = "El certificado tiene creados Adicionales o Actividades con avance, por lo que no se puede borrar";
                mReturn.isError = true;
            }
            #endregion

            if (PuedeBorrar)
            {
                #region DetallesCertificado_Listados
                //Detalles Actividad
                List<Certificados_detalle_planificado> DetalleCertificadosActividades = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
                                                                               .Where(s => s.IdCertificados == id)
                                                                               .ToList();
                //Detalles Adicional
                List<Certificados_detalle_adicional> DetalleCertificadosAdicionales = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                                                                               .Where(s => s.cda_IdCertificados == id)
                                                                               .ToList();
                //Detalles Importe
                //Detalles Adicional
                List<Certificados_detalle_liquidacion> DetalleCertificadosLiquidaciones = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                                                                               .Where(s => s.cdl_IdCertificados == id)
                                                                               .ToList();
                #endregion

                #region  EliminarDetallesyCertificado
                try
                {
                    //Eliminar Detalle Planificado
                    if (DetalleCertificadosActividades.Count > 0)
                    {
                        foreach (var item in DetalleCertificadosActividades)
                        {
                            new CertificadoNuevoNegocioDetalle().Eliminacion_All_DetallesCertificados(item);
                        }
                    }


                    //Eliminar Detalle Adicional
                    if (DetalleCertificadosAdicionales.Count > 0)
                    {
                        foreach (var item in DetalleCertificadosAdicionales)
                        {
                            new CertificadoNuevooDetallAdicionalNegocio().Delete(item);
                        }
                    }
                    //Eliminar Importes
                    //Eliminar Detalle Adicional
                    if (DetalleCertificadosLiquidaciones.Count > 0)
                    {
                        foreach (var item in DetalleCertificadosLiquidaciones)
                        {
                            new CertificadoNuevoNegocioDetalleLiquidacion().Delete(item);
                        }
                    }
                    #region EliminarCertificado
                    if (PuedeBorrar)
                    {
                        Id = new CertificadoNuevoNegocio().Delete(Certificado);
                        if (Id > 0)
                        {

                            mReturn.isError = false;
                        }
                    }
                    #endregion
                }
                catch (Exception)
                {

                    PuedeBorrar = false;
                }



                #endregion
            }
            #region ListadoCertificado

            ListaCertificados = new CertificadoNuevoNegocio().Get_All_Ordenes().ToList();

           
            foreach (var item in ListaCertificados)
            {



                try
                {
                    if (item.IdProyecto != 0)
                        item.sProyecto = new ProyectoNegocio().Get_One_Proyecto((int)item.IdProyecto).Nombre;
                }
                catch (Exception)
                {
                    item.sProyecto = "S/D";
                }

                try
                {
                    if (item.IdContratista != 0)

                        item.sContratista = new ContratistasNegocio().Get_One_Contratistas((int)item.IdContratista).Nombre;
                }
                catch (Exception)
                {
                    item.sContratista = "S/D";
                }

                try
                {
                    if (item.IdUsuarioCreo != 0)
                        item.sUsuario = new UsuariosNegocio().Get_Usuario((int)item.IdUsuarioCreo).Nombre;
                }
                catch (Exception)
                {
                    item.sUsuario = "S/D";
                }
            }
         
            ListaCertificados = ListaCertificados.OrderByDescending(p => p.FechaCreacion).ToList();
            #endregion

            mReturn.data2 = ListaCertificados;
            return mReturn;
        }
        #endregion

        #region Pestañas

        #region Pestaña_Actividades

        public IActionResult _CertificadosActividadesOperaciones(int IdCertificado, string Modo)
            {
                ReturnData mReturn = new ReturnData();
            //Controlamos el Estado que tiene el Certificado.
           
            mReturn.data1 = IdCertificado;
                mReturn.data2 = Modo;
            mReturn.data3 = MostrarDetallesCertificadosActividades(IdCertificado);
                return PartialView(mReturn);
            }

        public List<Certificados_detalle_planificado> MostrarDetallesCertificadosActividades(int IdCertificado)
        {
            #region ListadoCertificadosDetalle
            List<Certificados_detalle_planificado> listaCertificadoDetalle = ListadoCertificadoCRUD(IdCertificado,0,0,"MOSTRAR");

            return listaCertificadoDetalle;
            #endregion
        }

        public List<Certificados_detalle_planificado> ListadoCertificadoCRUD(int IdCertificado, int? IdProyecto, int? IdContratista, string? Modo)
        {
            List<Certificados_detalle_planificado> listaCertificadoDetalle = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
                                        .Where(s => s.IdCertificados == IdCertificado).ToList();

            if (Modo =="MOSTRAR")
            {  
                foreach (var cer in listaCertificadoDetalle)
                {
                    //Fecha Desde
                    if (cer.Act_F_Desde != null){
                        cer.vis_Liquida_FDesde = Convert.ToString(((DateTime)cer.Act_F_Desde).ToString("0:dd/MM/yyyy")).Remove(0, 2);
                    }
                    //Fecha Hasta
                    if (cer.Act_F_Hasta != null){
                        cer.vis_Liquida_FHasta = Convert.ToString(((DateTime)cer.Act_F_Hasta).ToString("0:dd/MM/yyyy")).Remove(0,2);
                    }

                    if (cer.Act_Acum_Ant_Unid == null)
                    {
                        cer.Act_Acum_Ant_Unid = 0;
                    }
                    if (cer.Act_Acum_Total_Unid == null)
                    {
                        cer.Act_Acum_Total_Unid = 0;
                    }
                    if (cer.Act_Cert_Actual_Moneda == null)
                    {
                        cer.Act_Cert_Actual_Moneda = 0;
                    }
                    if (cer.Act_Acum_Ant_Moneda == null)
                    {
                        cer.Act_Acum_Ant_Moneda = 0;
                    }
                    if (cer.Act_Acum_Actual_Moneda == null)
                    {
                        cer.Act_Acum_Actual_Moneda = 0;
                    }
                    NotaPedido_Detalle npd = new NotaPedido_DetalleNegocio().Get_One_Orden((int)cer.IdNotaPedidoDetalle);
                    if (npd != null)
                    {
                   
                        if (npd._Cantidad_y_Medida != null)
                        {
                             cer.Cantidad_Asignada_UniMedida = npd._Cantidad_y_Medida;
                        }
                    }
                    cer.Act_Esta_Liquidado = getEstaLiquidado((int)cer.Id);
                        
                }                           
            }

            return listaCertificadoDetalle;
        }

        [HttpPost]
        public ReturnData Update_Actividad(itemCertificado_Actividades data)
        {
            ReturnData mReturn = new ReturnData();
            mReturn.data2 = "";
            List<Certificados_detalle_planificado> listaActividades = new List<Certificados_detalle_planificado>();

            #region Definiciones

            Certificados_detalle_planificado oCertificadoDetalleActual = new CertificadoNuevoNegocioDetalle().Get_One_Orden(data.id);
            Certificados oCertificadoActual = new CertificadoNuevoNegocio().Get_One_Orden((int)oCertificadoDetalleActual.IdCertificados);

            NotaPedido NotaPedido = null;
            int IdCert_Detalle = 0;

            

            //Variables para calcular fechas
            ReturnData mFechas = new ReturnData();

        //Detalle Certificado Anterior
        Certificados_detalle_planificado oCertificadoDetalleAnterior = new Certificados_detalle_planificado();
            #endregion

        //BUSCAMOS EL CERTIFICADO ANTERIOR Y SU DETALLE        
            Certificados oCertificadoAnt = new CertificadoNuevoNegocio().Get_All_Ordenes()
                .Where(s=> s.IdContratista == oCertificadoActual.IdContratista
                        && s.IdProyecto== oCertificadoActual.IdProyecto
                        && s.Secuenciador == (oCertificadoActual.Secuenciador-1)).OrderByDescending(p=>p.Secuenciador).FirstOrDefault();

        if (oCertificadoAnt != null)
        {

            oCertificadoDetalleAnterior  = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
            .Where(s => s.IdNotaPedidoDetalle == oCertificadoDetalleActual.IdNotaPedidoDetalle
                && s.Id != oCertificadoDetalleActual.Id
                && s.Id < oCertificadoDetalleActual.Id)
            .OrderByDescending(p => p.Id)
            .FirstOrDefault();
        }

            //


            try
            {
                #region 2-Actualizacion de Datos
                if (data.act_Presenta_Liquidacion)
                {
                    #region BuscadoCertificadoAnterior
                    
                    //Buscamos certificado anterior para ese proyecto y contratista para calcular ACUMULADO anterior.   
                    if (oCertificadoAnt != null)
                    {
                        if (oCertificadoDetalleAnterior != null)
                        {
                                oCertificadoDetalleActual.Act_Acum_Ant_Unid = oCertificadoDetalleAnterior.Act_Acum_Total_Unid;
                                oCertificadoDetalleActual.Act_Acum_Ant_Moneda = oCertificadoDetalleAnterior.Act_Acum_Actual_Moneda;
                        }
                        else
                        {
                            //Acumu Anterior Unidad y Moneda no tiene.
                            oCertificadoDetalleActual.Act_Acum_Ant_Unid = 0;
                            oCertificadoDetalleActual.Act_Acum_Ant_Moneda = 0;
                        }

                    }
                    else
                    {
                        //Acumu Anterior  no tiene.
                        oCertificadoDetalleActual.Act_Acum_Ant_Unid = 0;
                        oCertificadoDetalleActual.Act_Acum_Ant_Moneda = 0;
                    }
                    #endregion
                    // 1)- Esta situacion es cuando edito por primera vez.---------------------------------------------------
                    #region GuardadoInicial

                    if (data.act_Presenta_Liquidacion == true && data.act_Avance_Periodo_Unid == null)
                    { 
                         

                            //Calculamos la fecha Desde  y  Hasta
                            mFechas = getDateLiquidacion((int)oCertificadoActual.Id, (int)oCertificadoDetalleActual.IdNotaPedidoDetalle);
                            //Fecha Desde            
                            oCertificadoDetalleActual.Act_F_Desde = (DateTime)mFechas.data1;
                            //Fecha Hasta            
                            oCertificadoDetalleActual.Act_F_Hasta = (DateTime)mFechas.data2;




                        //Avance Periodo Actual
                        oCertificadoDetalleActual.Act_Avance_Periodo_Unid = 0;
                        //Acumulado Total Unidades
                        oCertificadoDetalleActual.Act_Acum_Total_Unid = oCertificadoDetalleActual.Act_Acum_Ant_Unid + oCertificadoDetalleActual.Act_Avance_Periodo_Unid;
                       
                        //--//

                        //Cantidad Actual Moneda
                        oCertificadoDetalleActual.Act_Cert_Actual_Moneda = oCertificadoDetalleActual.Act_Avance_Periodo_Unid * oCertificadoDetalleActual.Monto_Unitario;
                        oCertificadoDetalleActual.Act_Cert_Actual_Moneda = (double)Math.Round((decimal)oCertificadoDetalleActual.Act_Cert_Actual_Moneda, 2);
                       //Acumulado Total Monedas
                       oCertificadoDetalleActual.Act_Acum_Actual_Moneda = oCertificadoDetalleActual.Act_Cert_Actual_Moneda + oCertificadoDetalleActual.Act_Acum_Ant_Moneda;


                        oCertificadoDetalleActual.Act_Presenta_Liquidacion = data.act_Presenta_Liquidacion;
                        oCertificadoDetalleActual.Act_Se_Modifico = true;

                        //--------------//
                        IdCert_Detalle = new CertificadoNuevoNegocioDetalle().Update(oCertificadoDetalleActual);
                    }
                    #endregion
                    // 3)- Guardamos la informacion y actualizamos si se realizo alguna Modificacion
                    #region GuardadoGenerico
                    
                    else if (data.act_Presenta_Liquidacion == true)
                    {
                        
                        //LIQUIDACION
                        oCertificadoDetalleActual.Act_Presenta_Liquidacion = data.act_Presenta_Liquidacion;

                        //UNIDADES
                        //Actualizamos Unidad  Anterior
                        if (oCertificadoDetalleActual.Act_Acum_Ant_Unid == null)
                        {
                            oCertificadoDetalleActual.Act_Acum_Ant_Unid = 0;
                        }

                        if (data.act_Avance_Periodo_Unid != 0 && data.act_Avance_Periodo_Unid != null)
                        {

                                //La cantidad asignada no puede ser menor al avance cargado + Acumulado anterior.
                                if (oCertificadoDetalleActual.Cantidad_Asignada < (data.act_Avance_Periodo_Unid + oCertificadoDetalleActual.Act_Acum_Ant_Unid))
                                {
                                    //El Avance = Cantidad Asignada - Acumulado Anterior
                                    oCertificadoDetalleActual.Act_Avance_Periodo_Unid = oCertificadoDetalleActual.Cantidad_Asignada - oCertificadoDetalleActual.Act_Acum_Ant_Unid;
                                    mReturn.data2 = "El Avance ingresado no puede ser mayor que la cantidad Asignada mas el Acumulado Anterior.";

                            }
                                else
                                {
                                    //Actualizamos Unidad Actual
                                    oCertificadoDetalleActual.Act_Avance_Periodo_Unid = data.act_Avance_Periodo_Unid;
                                }
                                
                                //Actualizamos Unidades Totales
                                oCertificadoDetalleActual.Act_Acum_Total_Unid = oCertificadoDetalleActual.Act_Acum_Ant_Unid + oCertificadoDetalleActual.Act_Avance_Periodo_Unid;
                                //Actualizamos Moneda Actual
                                oCertificadoDetalleActual.Act_Cert_Actual_Moneda = (oCertificadoDetalleActual.Act_Avance_Periodo_Unid * oCertificadoDetalleActual.Monto_Unitario);
                                oCertificadoDetalleActual.Act_Cert_Actual_Moneda = (double)Math.Round((decimal)oCertificadoDetalleActual.Act_Cert_Actual_Moneda, 2);



                        }
                        else
                        {
                            //Actualizamos Unidad  Actual
                            oCertificadoDetalleActual.Act_Avance_Periodo_Unid = 0;                       
                            //Actualizamos Unidades Totales                         
                            oCertificadoDetalleActual.Act_Acum_Total_Unid = oCertificadoDetalleActual.Act_Acum_Ant_Unid + oCertificadoDetalleActual.Act_Avance_Periodo_Unid;                  
                            //Actualizamos Moneda Actual
                            oCertificadoDetalleActual.Act_Cert_Actual_Moneda = 0;
                        }

                        //Seteamos el acumulado anterior moneda si es nulo.
                        if (oCertificadoDetalleActual.Act_Acum_Ant_Moneda == null)
                        {
                            oCertificadoDetalleActual.Act_Acum_Ant_Moneda = 0;
                        }
                        //MONEDA ToTal  
                        oCertificadoDetalleActual.Act_Acum_Actual_Moneda = oCertificadoDetalleActual.Act_Cert_Actual_Moneda + oCertificadoDetalleActual.Act_Acum_Ant_Moneda;

                        //CALCULAMOS LA FECHA DESDE Y FECHA HASTA

                        mFechas = getDateLiquidacion((int)oCertificadoActual.Id, (int)oCertificadoDetalleActual.IdNotaPedidoDetalle);
                        //Fecha Desde            
                        oCertificadoDetalleActual.Act_F_Desde = (DateTime)mFechas.data1;
                        //Fecha Hasta            
                        oCertificadoDetalleActual.Act_F_Hasta = (DateTime)mFechas.data2;

                        oCertificadoDetalleActual.Act_Se_Modifico = true;
                        //REALIZAMOS LA MODIFICACION
                        IdCert_Detalle = new CertificadoNuevoNegocioDetalle().Update(oCertificadoDetalleActual);

                        //Actualizamos el Resto del Proyecto

                        #region ActualizacionRestoProyecto
                        itemCertificadoDetalleActualizacionProyecto modelo = new itemCertificadoDetalleActualizacionProyecto();

                        modelo.AvanceCargado = (double)oCertificadoDetalleActual.Act_Avance_Periodo_Unid;
                        modelo.FechaHastaCertif = oCertificadoActual.FechaCreacion;
                        modelo.IdNotaPedidoDetalle = (int)oCertificadoDetalleActual.IdNotaPedidoDetalle;
                        modelo.IdContratista = (int)oCertificadoActual.IdContratista;
                        modelo.Planificacion_Proyecto_ActividadesId = (int)oCertificadoDetalleActual.IdPPA;
                        ReturnData mProyecto = ActualizarCantidadProyecto(modelo);

                        if (mProyecto.isError)
                        {
                            mReturn.data = "Hubo un problema para actualizar el resto del proyecto.";
                            mReturn.isError = true;
                        }


                        #endregion
                    }
                    #endregion
                }
                else 
                {
                    #region CuandoNoHayCheckSeleccionado

                    
                    //LIMPIAMOS TODOS LOS CAMPOS
                    oCertificadoDetalleActual.Act_Presenta_Liquidacion = false;
                    oCertificadoDetalleActual.Act_F_Desde= null;
                    oCertificadoDetalleActual.Act_F_Hasta = null;

                    //------------SECCION UNIDADES-----------------//
                    oCertificadoDetalleActual.Act_Avance_Periodo_Unid = null;

                    //Calculo de Unidades
                    if (oCertificadoDetalleAnterior.Act_Acum_Total_Unid != null)
                    { oCertificadoDetalleActual.Act_Acum_Ant_Unid = oCertificadoDetalleAnterior.Act_Acum_Total_Unid;}
                    else {oCertificadoDetalleActual.Act_Acum_Ant_Unid = 0;}

                     oCertificadoDetalleActual.Act_Acum_Total_Unid = oCertificadoDetalleActual.Act_Acum_Ant_Unid;

                    //-----------SECCION MONEDAS----------------//
                    oCertificadoDetalleActual.Act_Cert_Actual_Moneda = null;
                    if (oCertificadoDetalleActual.Act_Acum_Ant_Moneda == null)
                    {
                        oCertificadoDetalleActual.Act_Acum_Actual_Moneda = 0;
                    }
                    else
                    {
                        oCertificadoDetalleActual.Act_Acum_Actual_Moneda = oCertificadoDetalleActual.Act_Acum_Ant_Moneda;
                    }
                   
                    //REALIZAMOS LA MODIFICACION
                    IdCert_Detalle = new CertificadoNuevoNegocioDetalle().Update(oCertificadoDetalleActual);

                    #region ActualizacionRestoProyecto
                    itemCertificadoDetalleActualizacionProyecto modelo = new itemCertificadoDetalleActualizacionProyecto();

                    modelo.AvanceCargado = 0;
                    modelo.FechaHastaCertif = oCertificadoActual.FechaCreacion;
                    modelo.IdNotaPedidoDetalle = (int)oCertificadoDetalleActual.IdNotaPedidoDetalle;
                    modelo.IdContratista = (int)oCertificadoActual.IdContratista;
                    modelo.Planificacion_Proyecto_ActividadesId = (int)oCertificadoDetalleActual.IdPPA;
                    ReturnData mProyecto = ActualizarCantidadProyecto(modelo);

                    if (mProyecto.isError)
                    {
                        mReturn.data = "Hubo un problema para actualizar el resto del proyecto.";
                        mReturn.isError = true;
                    }


                    #endregion
                    #endregion
                }
                #endregion


                #region ReturnLista
                listaActividades = MostrarDetallesCertificadosActividades((int)oCertificadoDetalleActual.IdCertificados);

                mReturn.isError = false;
                mReturn.data = "Se modifico correctamente la Actividad.";
                mReturn.data3 = listaActividades;
                #endregion
            }

            catch (Exception ex)
            {
                mReturn.data = "Hubo un problema para modificar el registro.";
                mReturn.isError=true;
            }
            return mReturn;
    }

        #endregion

        #region Pestaña_Adicional
        public IActionResult _CertificadosAdicionales_Pestana(int IdCertificado, string Modo)
        {
            ReturnData mReturn = new ReturnData();
            Certificados Certificado = new CertificadoNuevoNegocio().Get_One_Orden(IdCertificado);
            mReturn.data = Certificado.IdProyecto;
            //Este Id nos sirve para buscar todos los detalles de certificado por la Grilla kendo.
            mReturn.data1 = IdCertificado;
            mReturn.data2 = Modo;
            
            return PartialView(mReturn);
        }


        [HttpPost]
        public ReturnData CargarNuevoAdicional(ItemNuevoAdicional omodel)
        {
            ReturnData d = new ReturnData();
            try
            {
                #region Definiciones
               
                int Id = 0;
                int IdRubro = Convert.ToInt16(omodel.IdRubro);
                int IdActividad = Convert.ToInt16(omodel.IdActividad);
                int IdUbicacion = Convert.ToInt16(omodel.IdUbicacion);
                int IdCertificado = Convert.ToInt16(omodel.IdCertificado);
                
                #endregion

                #region BusquedaInformacion

                Planificacion_Proyecto_Actividades PPA = new Planificacion_Proyecto_ActividadesNegocio()
                                                                        .Get_All_PPA_Filtro(IdRubro, IdActividad, IdUbicacion)
                                                                        .FirstOrDefault();

                Planificacion_Actividades PA = new Planificacion_ActividadesNegocio()
                                                .Get_All_Planificacion_Actividades()
                                                .Where(x => x.Id == IdActividad
                                                        && x.Planificacion_Actividades_RubroId == IdRubro)
                                                .FirstOrDefault();


                Certificados ocertificado= new CertificadoNuevoNegocio().Get_One_Orden((int)IdCertificado);
                #endregion
                Certificados_detalle_adicional DetalleAdicional = new Certificados_detalle_adicional();

                DetalleAdicional.cda_ActividadaId = (long)IdActividad;
                DetalleAdicional.cda_UbicacionId = (long)IdUbicacion;
                DetalleAdicional.cda_RubroId = (long)IdRubro;
                DetalleAdicional.cda_UnidadMedida = omodel.UnidadMedida;
                DetalleAdicional.cda_Importe_Cantidad_Asignada = (double)Convert.ToDouble(omodel.Cantidad);

                

                DetalleAdicional.cda_Importe_Monto_Unitario = double.Parse(omodel.MontoUnitario, new CultureInfo("en-US"));
                //omodel.MontoUnitario = omodel.MontoUnitario.Replace(".", ",");
                //DetalleAdicional.cda_Importe_Monto_Unitario = Math.Round(Convert.ToDouble(omodel.MontoUnitario), 2);

                DetalleAdicional.cda_Moneda_Certificado_Actual = DetalleAdicional.cda_Importe_Monto_Unitario * DetalleAdicional.cda_Importe_Cantidad_Asignada;
                DetalleAdicional.cda_Moneda_Certificado_Actual = (double)Math.Round((decimal)DetalleAdicional.cda_Moneda_Certificado_Actual, 2);


                DetalleAdicional.cda_IdCertificados = (long)Convert.ToInt32(omodel.IdCertificado);
                DetalleAdicional.cda_Importe_FHasta = ocertificado.FechaCreacion;

                if (PPA != null)
                {
                    if (PPA.Detalle != null)
                    {
                        DetalleAdicional.cda_Comentario = PPA.Detalle;
                    }
                   
                }
                if (PA != null)
                {
                    if (PA.PartidaPresupuestariaId != 0)
                    {
                        DetalleAdicional.cda_Partida_Id = PA.PartidaPresupuestariaId;
                    }
                    
                }
             
                Id = new CertificadoNuevooDetallAdicionalNegocio().Insert(DetalleAdicional);
                if (Id >0)
                {
                    d.isError = false;
                }
            }
            catch (Exception ex)
            {
               d.isError=true;
            }
            return d;
        }
        public List<Certificados_detalle_adicional> MostrarDetallesCertificadosAdicionales(int IdCertificado)
        {
            List<Certificados_detalle_adicional> listaCertificadoDetalle  = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                                        .Where(s => s.cda_IdCertificados == (long)IdCertificado).ToList();


            foreach (var cer in listaCertificadoDetalle)
            {
                    //Fecha Desde
                    
                    if (cer.cda_Importe_FHasta != null)
                    {
                        cer.vis_cda_Importe_FHasta = Convert.ToString(((DateTime)cer.cda_Importe_FHasta).ToString("0:dd/MM/yyyy")).Remove(0, 2);
                    }
                    if (cer.cda_RubroId != null)
                    {
                        cer.vis_cda_RubroNombre = new Planificacion_Actividades_RubroNegocio().Get_One_Planificacion_Actividades_Rubro((int)cer.cda_RubroId)
                                                                                      .Nombre;
                    }
                    if (cer.cda_UbicacionId != null)
                    {
                        cer.vis_cda_UbicacionNombre = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones((int)cer.cda_UbicacionId)
                                                                                      .Nombre;
                    }
                    if (cer.cda_ActividadaId != null)
                    {
                        Planificacion_Actividades PA = new Planificacion_ActividadesNegocio().Get_One_Planificacion_Actividades((int)cer.cda_ActividadaId);
                        cer.vis_cda_ActividadNombre = PA.Nombre;
                    PartidaPresupuestaria Partida = new PartidaPresupuestariaNegocio().Get_x_Id((int)cer.cda_Partida_Id);
                    cer.vis_cda_PartidaNombre = Partida.Codigo;
                    }
                    if (cer.cda_Importe_Monto_Unitario == null)
                    {
                        cer.cda_Importe_Monto_Unitario = 0;
                    }
                    if (cer.cda_Moneda_Certificado_Actual == null)
                    {
                        cer.cda_Moneda_Certificado_Actual = 0;
                    }
                cer.cda_Importe_Cantidad_Asignada_UnidadMedida = Convert.ToString(cer.cda_Importe_Cantidad_Asignada)+" " + cer.cda_UnidadMedida;

                if(cer.cda_EstaAprobada != null){
                    cer.vis_cda_EstaAprobada = (bool)new CertificadoNuevooDetallAdicionalNegocio().Get_One_Orden((int)cer.cda_Id).cda_EstaAprobada ? "SI" : "NO";
                }
                else
                {
                    cer.vis_cda_EstaAprobada = "NO";
                }
               



            }

            return listaCertificadoDetalle;
        }

        public List<Certificados_detalle_adicional> MostrarDetallesCertificadosAdicionalesHistorico(int IdCertificado)
        {
            Certificados _certificadoInfo = new CertificadoNuevoNegocio().Get_One_Orden(IdCertificado);
            List<Certificados_detalle_adicional> listaCertificadoDetalle = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                                        .Where(s => s.cda_IdCertificados < (long)IdCertificado
                                        ).ToList();

            if (listaCertificadoDetalle.Count > 0)
            {

           
            foreach (var cer in listaCertificadoDetalle)
            {
                //Fecha Desde

                if(cer.cda_IdCertificados != null)
                {

                    Certificados _certificado = new CertificadoNuevoNegocio().Get_One_Orden((int)cer.cda_IdCertificados);
                   cer.vis_nro_Certificado_Historico = _certificado.NroCertificado;
                    cer.vis_fecha_certificado_Historico = Convert.ToString(((DateTime)_certificado.FechaCreacion).ToString("0:dd/MM/yyyy")).Remove(0, 2);
                }
                if (cer.cda_Importe_FHasta != null)
                {
                    cer.vis_cda_Importe_FHasta = Convert.ToString(((DateTime)cer.cda_Importe_FHasta).ToString("0:dd/MM/yyyy")).Remove(0, 2);
                }
                if (cer.cda_RubroId != null)
                {
                    cer.vis_cda_RubroNombre = new Planificacion_Actividades_RubroNegocio().Get_One_Planificacion_Actividades_Rubro((int)cer.cda_RubroId)
                                                                                  .Nombre;
                }
                if (cer.cda_UbicacionId != null)
                {
                    cer.vis_cda_UbicacionNombre = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones((int)cer.cda_UbicacionId)
                                                                                  .Nombre;
                }
                if (cer.cda_ActividadaId != null)
                {
                    Planificacion_Actividades PA = new Planificacion_ActividadesNegocio().Get_One_Planificacion_Actividades((int)cer.cda_ActividadaId);
                    cer.vis_cda_ActividadNombre = PA.Nombre;
                    cer.vis_cda_PartidaNombre = PA.CodigoPartidaPresupuestaria;
                }
                if (cer.cda_Importe_Monto_Unitario == null)
                {
                    cer.cda_Importe_Monto_Unitario = 0;
                }
                if (cer.cda_Moneda_Certificado_Actual == null)
                {
                    cer.cda_Moneda_Certificado_Actual = 0;
                }
                cer.cda_Importe_Cantidad_Asignada_UnidadMedida = Convert.ToString(cer.cda_Importe_Cantidad_Asignada) + " " + cer.cda_UnidadMedida;

            }
              }
            return listaCertificadoDetalle;
        }

        [HttpPost]
        public ReturnData BorraAdicional(int id)
        {
            ReturnData d = new ReturnData();
            int IdEliminado = 0;
            try
            {
                Certificados_detalle_adicional adicional = new CertificadoNuevooDetallAdicionalNegocio().Get_One_Orden(id);
                IdEliminado = new CertificadoNuevooDetallAdicionalNegocio().Delete(adicional);
                
                Certificados_detalle_adicional adicionalAnterior = new CertificadoNuevooDetallAdicionalNegocio().Get_One_Orden((id-1));
                if(adicionalAnterior != null)
                {
                 
                    new CertificadoNuevooDetallAdicionalNegocio().Update(adicionalAnterior);
                }

                //Logica Cuando no tiene certificado Anterior.

                if (IdEliminado > 0)
                {
                    d.data2 = "Se han Borrado los datos OK.";
                    d.isError = false;
                }
                else
                {
                    d.data2 = "Error al Borrar el adicional.";
                    d.isError = false;
                }

            }
            catch (Exception)
            {

                d.data2 = "Error al Borrar el adicional.";
                d.isError = true;
            }
            return d;
        }

        [HttpPost]
        public ReturnData _AprobarAdicionales(itemCertificadoDetalleAdicionalAprobacion data)
        {
            ReturnData mReturn = new ReturnData();
            List<Certificados_detalle_adicional> listaCertificadoDetalle = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                                       .Where(s => s.cda_IdCertificados == (long)data.IdCertificado).ToList();

            List<RolesUsuarios> ListaRoles = getUsuarioActual().Roles;

            int UsuarioId = getUsuarioActual().Id;




            var RolesUsuarioId = from s in ListaRoles
                                 where s.UsuariosId == UsuarioId
                                 select new
                                 {
                                     s.RolesId
                                 };
            /*
             Id=6 Jefe de Obra
             Id=11 Admnistracion
             Id=8 Coordinacion de Obra Pucheta
             */




            try
            {
                //cambiar a 8

                if (!(RolesUsuarioId.Any(s => s.RolesId == 8 || s.RolesId==1)))
                {
                    mReturn.data = "El usuario con el que esta operando no presenta los permisos para aprobar adicionales.";
                    mReturn.isError = true;
                }
                else if(listaCertificadoDetalle.Any(s => s.cda_EstaAprobada == null || s.cda_EstaAprobada == false))
                {
                    foreach (var cer in listaCertificadoDetalle)
                    {
                        cer.cda_EstaAprobada = true;
                        new CertificadoNuevooDetallAdicionalNegocio().Update(cer);

                    }
                    mReturn.data2 = "Se aprobaron los Adicionales correctamente";
                }
                else
                {
                    mReturn.data2 = "No se encontraron adicionales pendientes de aprobar";
                }
            }

            catch (Exception ex)
            {
                mReturn.data = "Hubo un problema para aprobar los adicionales.";
                mReturn.isError = true;
            }
            return mReturn;
        }

        #endregion

        #region Pestaña_Liquidaciones
        /* //--> Esta funcion devuelve la vista de Liquidaciones<--// */
        public IActionResult _CertificadosLiquidaciones_Pestana(int IdCertificado, string Modo)
        {
            ReturnData mReturn = new ReturnData();

            //1)- Controlamos Si Presenta liquidacion o No y Se carga
            List<Certificados_detalle_liquidacion> listaCertificadoDetalleControl = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                                                 .Where(s => s.cdl_IdCertificados == IdCertificado && s.cdl_NroNotaPedido != "Adicional").ToList();
            if (listaCertificadoDetalleControl.Count == 0)
            { _CargadoInicialLiquidaciones(IdCertificado); }

            mReturn.data3 = IdCertificado;       
            //Controlamos el estado del Certificado
            mReturn.data5 = _ConsultarEstado(IdCertificado);

            return PartialView(mReturn);
        }

        [HttpPost]
        public ReturnData CargarGrillasLiquidaciones(int IdCertificado)
        {
            ReturnData mReturn = new ReturnData();
            try
            {
                //1)- Controlamos Si Presenta liquidacion o No y Se carga
                List<Certificados_detalle_liquidacion> listaCertificadoDetalleControl = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                                                                .Where(s => s.cdl_IdCertificados == IdCertificado  && s.cdl_NroNotaPedido != "Adicional").ToList();
                if (listaCertificadoDetalleControl.Count == 0)
                { _CargadoInicialLiquidaciones(IdCertificado); }


                //1)- Actualizamos la tabla de liquidaciones si hubo cambios.
                _ActualizacionTablaLiquidaciones(IdCertificado);

                //1)- Devolvemos la Lista de Partida Presupuestaria
                mReturn.data = _CargarPartidaPresupuestaria(IdCertificado).data1;

                //1)- Devolvemos la Lista de Aplicaciones Pagos.
                mReturn.data1 = new CertificadoNuevoNegocioDetalleAplicacionPago().Get_All_Ordenes().Where(s => s.cap_IdCertificados == IdCertificado).ToList();

                //1)- Devolvemos la Lista de Detalle de Liquidaciones.
                List<Certificados_detalle_liquidacion> listaLiquidaciones = MostrarDetallesCertificadosLiquidaciones((int)IdCertificado);
                mReturn.data2 = listaLiquidaciones;

                //1)- Calculamos el valor actualizado del total de Liquidacion.
                mReturn.data4 = _calcularTotalPestalaLiquidacion(IdCertificado);
            }
            catch (Exception ex)
            {
                mReturn.data = "Hubo un problema para modificar el registro.";
                mReturn.isError = true;
            }
                 
            return mReturn;
        }

        /* //--> Esta funcion es para calcular la Partida Presupuestaria x Codigo de Partida de Actividades y Adicionales <--// */
        private ReturnData _CargarPartidaPresupuestaria(int IdCertificado)
        {
            ReturnData mReturn = new ReturnData();
            //Esta lista es la que devolveremos con la informacion cargada
            List<itemCertificado_CertificadoLiquidacionPPresupuestaria> Lista = new List<itemCertificado_CertificadoLiquidacionPPresupuestaria>();
            //Listado de Adicionales
            List<Certificados_detalle_adicional> listaCertificadoDetalleAdicional = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                                  .Where(s => s.cda_IdCertificados == (long)IdCertificado).ToList();
            //Listado de Actividades Planificadas
            List<Certificados_detalle_planificado> listaCertificadoDetallePlanificado = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
                                  .Where(s => s.IdCertificados == (long)IdCertificado
                                    && s.Act_Presenta_Liquidacion==true
                                  ).ToList();

            // Recorremos todas las Partidas Presupuestarias viendo cuales son las que estan en Adicionales y Liquidaciones
            List <PartidaPresupuestaria> PartidasPresupuestarias = new PartidaPresupuestariaNegocio().GetAll();
            foreach (var par in PartidasPresupuestarias)
            {             
                #region Adicionales
                    itemCertificado_CertificadoLiquidacionPPresupuestaria valueAdicional = new itemCertificado_CertificadoLiquidacionPPresupuestaria();
                    valueAdicional._certificadoMonto = 0;
                    valueAdicional._fondoReparo = 0;
                    valueAdicional._codigoPartida = par.Codigo;
                    valueAdicional._nombrePartida = par.Descripcion;    
                    foreach (var item in listaCertificadoDetalleAdicional)
                    { 
                        if(item.cda_Partida_Id == par.Id)
                        {
                            valueAdicional._certificadoMonto= valueAdicional._certificadoMonto  + (double)item.cda_Moneda_Certificado_Actual;
                           
                        }
                    }
                    //Usamos esta funcion para calcular el monto correspondiente al fondo reparo de Adicionales
                        //(Usando los montos de adicionales por el porcentaje que le corresponda. Agrupado por Partida)
                    valueAdicional._fondoReparo = _calcularMontoReparo_Adicional(valueAdicional._certificadoMonto, IdCertificado);

                #endregion
                #region Actividades
                foreach (var planif in listaCertificadoDetallePlanificado)
                {
                    //TIene el mismo codigo de Partida y Esta Liquidado
                    if (planif.PartidaId == par.Id && planif.Act_Presenta_Liquidacion == true)
                    {
                        //Buscamos el monto del certificado actual
                        double montoActualActividad = planif.Act_Cert_Actual_Moneda > 0 ? (double)planif.Act_Cert_Actual_Moneda : 0;

                        //Buscamos el Registro de Liquidaciones por Nota de Pedido.
                        Certificados_detalle_liquidacion certificados_Liquidacion = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                                                                .Where(s => s.cdl_NroNotaPedido == planif.NroNotaPedido
                                                                && s.cdl_IdCertificados == IdCertificado).FirstOrDefault();

                        if (certificados_Liquidacion != null)
                        {                   
                            //Buscamos el TOTAL de Actividades Liquidadas (puede que sean varias actividades con la misma Nota de Pedido)
                            double montoActividadesLiquidado = (double)certificados_Liquidacion.cdl_Acumulado_Actividades_Planificadas;
                            //Calculamos que porcentaje de ese TOTAL liquidado le corresponde a esta actividad.
                            double porcentajeCorrespondiente = (montoActualActividad / montoActividadesLiquidado) * 100;
                            //Calculamos el Neto que le corresponde a esa actividad
                            double NetoaFacturarActividad = (double)((porcentajeCorrespondiente * certificados_Liquidacion.cdl_Neto_Facturar)/100);
                            if(NetoaFacturarActividad > 0)
                            {
                                valueAdicional._certificadoMonto = valueAdicional._certificadoMonto + NetoaFacturarActividad;
                                //No estamos calculando el Fondo Reparo para Actividades.
                                valueAdicional._fondoReparo = valueAdicional._fondoReparo + 0;
                                //oCDL.cdl_Fondo_Reparo_Monto = ((oCDL.cdl_Fondo_Reparo_Porcentaje + oCDL.cdl_Ajuste_Actualizacion + oCDL.cdl_Acumulado_Actividades_Planificadas) * (oCDL.cdl_Fondo_Reparo_Porcentaje / 100));
                            }
                        } 
                    }
                    
                }
                if (valueAdicional._certificadoMonto > 0)
                {
                    valueAdicional._fondoReparo = Math.Round(valueAdicional._fondoReparo, 2);
                    Lista.Add(valueAdicional);
                }
                #endregion             
            }
            mReturn.data1 = Lista;

            return mReturn;
        }

        /* //--> Esta funcion es para calcular el fondo Reparo para los Adicionales de Partida Presupuestaria <--// */
        private double _calcularMontoReparo_Adicional(double montoAdicional, int idCertificado)
        {
            double montoReparo = 0;
            Certificados_detalle_liquidacion DetalleLiquidar = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                .Where(s => s.cdl_NroNotaPedido == "Adicional"
                    && s.cdl_IdCertificados == idCertificado
                ).FirstOrDefault();

            if(DetalleLiquidar != null && (DetalleLiquidar.cdl_Fondo_Reparo_Porcentaje  != null))
            {
                if (DetalleLiquidar.cdl_Ajuste_Actualizacion == null)
                {
                    DetalleLiquidar.cdl_Ajuste_Actualizacion = 0;
                } 
                montoReparo=  (double)((DetalleLiquidar.cdl_Fondo_Reparo_Porcentaje + DetalleLiquidar.cdl_Ajuste_Actualizacion + montoAdicional ) * (DetalleLiquidar.cdl_Fondo_Reparo_Porcentaje / 100));

            }

            return montoReparo;
        }

        /* //--> Devuelve el lista de Registros de la tabla liquidaciones x IdCertificado <--// */
        public List<Certificados_detalle_liquidacion> MostrarDetallesCertificadosLiquidaciones(int IdCertificado)
        {

            
            #region ListadoCertificadosDetalle
            List<Certificados_detalle_liquidacion> listaCertificadoDetalle = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                                                            .Where(s=> s.cdl_IdCertificados==IdCertificado).ToList();

            return listaCertificadoDetalle;
            #endregion
        }

        /* //--> Esta funcion se utiliza cuando se crea por primera vez los registros de Liquidaciones <--// */
        private ReturnData _CargadoInicialLiquidaciones( int IdCertificado)
        {
            ReturnData d = new ReturnData();
            int IdUlt = 0;

            try
            {
                #region Planificacion
                List<Certificados_detalle_planificado> listaCertificadoDetalleControl2 = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
                                                        .Where(s => s.Act_Acum_Actual_Moneda > 0
                                                        && s.Act_Presenta_Liquidacion == true
                                                        && s.IdCertificados == IdCertificado
                                                        )
                                                        .ToList();
                if (listaCertificadoDetalleControl2.Count > 0)
                {

                    using (var db = new iotdbContext())
                    {
                        var conn = db.Database.GetDbConnection();


                        MySqlConnection connection = new MySqlConnection(db.Database.GetDbConnection().ConnectionString);
                        connection.Open();

                        MySqlCommand command = new MySqlCommand("ListadoCertificados_Liquidaciones", connection);

                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Ent_IdCertificados", IdCertificado);

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            Certificados_detalle_liquidacion CD = new Certificados_detalle_liquidacion();
                            CD.cdl_IdCertificados = IdCertificado;
                            CD.cdl_NroNotaPedido = (string)reader.GetString(0);
                            CD.cdl_Acumulado_Actividades_Planificadas = (double)reader.GetDouble(1);
                            CD.cdl_Descuento_Por_Anticipo_Monto = 0;
                            CD.cdl_Descuento_Por_Anticipo_Porcentaje = 0;
                            CD.cdl_Descuento_Por_Adelanto = 0;
                            CD.cdl_Total_Sujeto_Ajuste = CD.cdl_Acumulado_Actividades_Planificadas;
                           
                            CD.cdl_Ajuste_Ind_Base = (double)reader.GetDouble(2);
                            CD.cdl_Ajuste_Ind_Actual = _calcularIndiceActual((int)reader.GetInt32(3));

                            //Puede tener nulos los indices,y si es asi el coef es 0
                            if(CD.cdl_Ajuste_Ind_Base > 0 && CD.cdl_Ajuste_Ind_Actual > 0){
                                    CD.cdl_Ajuste_Porc_Coeficiente = ( ( (CD.cdl_Ajuste_Ind_Actual / CD.cdl_Ajuste_Ind_Base) -1) * 100 );
                                    CD.cdl_Ajuste_Porc_Coeficiente = (double)Math.Round((decimal)CD.cdl_Ajuste_Porc_Coeficiente, 2);
                            }
                            else
                            {
                                CD.cdl_Ajuste_Porc_Coeficiente = 0;
                            }
                             
                            CD.cdl_Ajuste_Actualizacion = 0;

                            CD.cdl_Actividades_Adicionales = 0;

                            CD.cdl_Neto_Facturar = CD.cdl_Actividades_Adicionales + CD.cdl_Total_Sujeto_Ajuste + CD.cdl_Ajuste_Actualizacion;
                            CD.cdl_Iva_Porcentaje = 0;
                            CD.cdl_Iva_Monto = 0;
                            CD.cdl_Total_A_Facturar = CD.cdl_Neto_Facturar;
                            CD.cdl_Nro_Poliza = (string)reader.GetString(4);
                            CD.cdl_Fondo_Reparo_Monto = 0;
                            CD.cdl_Fondo_Reparo_Porcentaje = (double)reader.GetInt32(5);
                            CD.cdl_Total_A_Pagar = CD.cdl_Total_A_Facturar;

                            IdUlt = new CertificadoNuevoNegocioDetalleLiquidacion().Insert(CD);

                        }
                        connection.Close();
                        //Certificados_detalle_liquidacion Cer = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                        //                                        .Where(s => s.cdl_Id == IdUlt).FirstOrDefault();
                        
                    }
                }
                #endregion
                #region Adicionales
                List<Certificados_detalle_liquidacion> listaCertificadoDetalleLiquidacion = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                                                       .Where(s => s.cdl_IdCertificados == IdCertificado
                                                       && s.cdl_NroNotaPedido == "Adicional")
                                                       .ToList();
                if (listaCertificadoDetalleLiquidacion.Count == 0 )
                {

               
                List<Certificados_detalle_adicional> Lis = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                                                   .Where(s => s.cda_IdCertificados == IdCertificado).ToList();


                /*Nueva Adicionales*/
                Certificados_detalle_liquidacion Liquidacion_Nuevo = new Certificados_detalle_liquidacion();

                if (Lis.Count > 0)
                {
                    Liquidacion_Nuevo.cdl_Actividades_Adicionales = Lis.Sum(p => p.cda_Moneda_Certificado_Actual);
                    Liquidacion_Nuevo.cdl_Neto_Facturar = Liquidacion_Nuevo.cdl_Actividades_Adicionales;
                    Liquidacion_Nuevo.cdl_Total_A_Facturar = Liquidacion_Nuevo.cdl_Neto_Facturar;
                    Liquidacion_Nuevo.cdl_Total_A_Pagar = Liquidacion_Nuevo.cdl_Total_A_Facturar;

                }
                else
                {
                    Liquidacion_Nuevo.cdl_Actividades_Adicionales = 0;
                    Liquidacion_Nuevo.cdl_Neto_Facturar = Liquidacion_Nuevo.cdl_Actividades_Adicionales;
                    Liquidacion_Nuevo.cdl_Total_A_Facturar = Liquidacion_Nuevo.cdl_Neto_Facturar;
                    Liquidacion_Nuevo.cdl_Total_A_Pagar = Liquidacion_Nuevo.cdl_Total_A_Facturar;
                }
                Liquidacion_Nuevo.cdl_IdCertificados = IdCertificado;
                Liquidacion_Nuevo.cdl_Descuento_Por_Anticipo_Porcentaje = 0;
                Liquidacion_Nuevo.cdl_Total_Sujeto_Ajuste = 0;
                Liquidacion_Nuevo.cdl_Ajuste_Porc_Coeficiente = 0;
                Liquidacion_Nuevo.cdl_Iva_Porcentaje = 0;
                Liquidacion_Nuevo.cdl_Fondo_Reparo_Porcentaje = 0;
                Liquidacion_Nuevo.cdl_NroNotaPedido = "Adicional";

                new CertificadoNuevoNegocioDetalleLiquidacion().Insert(Liquidacion_Nuevo);
                }
                #endregion
                d.isError = false;
            }
            catch (Exception e)
            {
                d.isError = true;
                d.data = "Hubo un Error para insertar los registros en el certificado Detalle";
                throw;
            }


            return d;
        }

        /* //--> Esta funcion se utiliza cuando se edita algun valor de la tabla de Liquidaciones <--// */
        [HttpPost]
        public ReturnData Update_Liquidacion(itemCertificado_Liquidaciones data)
        {
            ReturnData mReturn = new ReturnData();
            mReturn.data2 = "";

            if (data.cdl_Descuento_Por_Anticipo_Porcentaje == null)
            {
                data.cdl_Descuento_Por_Anticipo_Porcentaje = 0;
            }

            if (data.cdl_Descuento_Por_Anticipo_Monto == null)
            {
                data.cdl_Descuento_Por_Anticipo_Monto = 0;
            }
            if (data.cdl_Descuento_Por_Adelanto == null)
            {
                data.cdl_Descuento_Por_Adelanto = 0;
            }
            try
            {
                #region 2-Actualizacion de Datos

                Certificados_detalle_liquidacion oCDL = new CertificadoNuevoNegocioDetalleLiquidacion().Get_One_Orden(data.cdl_Id);

                // 3)- Guardamos la informacion y actualizamos si se realizo alguna Modificacion

                if (data.cdl_Acumulado_Actividades_Planificadas != null)
                {

                    oCDL.cdl_Acumulado_Actividades_Planificadas = data.cdl_Acumulado_Actividades_Planificadas;
                }

                if (data.cdl_Descuento_Por_Anticipo_Porcentaje != null) {

                    oCDL.cdl_Descuento_Por_Anticipo_Porcentaje = data.cdl_Descuento_Por_Anticipo_Porcentaje;
                    oCDL.cdl_Descuento_Por_Anticipo_Monto = oCDL.cdl_Acumulado_Actividades_Planificadas *(oCDL.cdl_Descuento_Por_Anticipo_Porcentaje/100);
                }

                if(data.cdl_Descuento_Por_Adelanto != null)
                {
                    oCDL.cdl_Descuento_Por_Adelanto = data.cdl_Descuento_Por_Adelanto;
                    oCDL.cdl_Total_Sujeto_Ajuste = oCDL.cdl_Acumulado_Actividades_Planificadas - oCDL.cdl_Descuento_Por_Anticipo_Monto - oCDL.cdl_Descuento_Por_Adelanto;
                }
                else
                {
                    oCDL.cdl_Descuento_Por_Adelanto = 0;
                    oCDL.cdl_Total_Sujeto_Ajuste = oCDL.cdl_Acumulado_Actividades_Planificadas - oCDL.cdl_Descuento_Por_Anticipo_Monto;
                }
                //Columna Ajuste-Actualizacion (las otras columnas se calculan cuando se crea)

                oCDL.cdl_Ajuste_Actualizacion = oCDL.cdl_Ajuste_Porc_Coeficiente * oCDL.cdl_Total_Sujeto_Ajuste;

                //Columna Neto a Facturar
                if (oCDL.cdl_Ajuste_Actualizacion == null)
                {
                    oCDL.cdl_Ajuste_Actualizacion = 0;
                }
                if (oCDL.cdl_Total_Sujeto_Ajuste == null)
                {
                    oCDL.cdl_Total_Sujeto_Ajuste = 0;
                }
                oCDL.cdl_Neto_Facturar = oCDL.cdl_Ajuste_Actualizacion + oCDL.cdl_Total_Sujeto_Ajuste + oCDL.cdl_Actividades_Adicionales;
                oCDL.cdl_Neto_Facturar = (double)Math.Round((decimal)oCDL.cdl_Neto_Facturar, 2);

                //Columna Iva
                if (data.cdl_Iva_Porcentaje != null)
                {
                    oCDL.cdl_Iva_Porcentaje = data.cdl_Iva_Porcentaje;
                    oCDL.cdl_Iva_Monto = oCDL.cdl_Neto_Facturar * (oCDL.cdl_Iva_Porcentaje / 100);
                }
                else
                {
                    oCDL.cdl_Iva_Porcentaje = 0;
                    oCDL.cdl_Iva_Monto = 0;
                }

                //Columna Total a Facturar
                oCDL.cdl_Total_A_Facturar = oCDL.cdl_Iva_Monto + oCDL.cdl_Neto_Facturar;
                oCDL.cdl_Total_A_Facturar =(double)Math.Round((decimal)oCDL.cdl_Total_A_Facturar,2);

                //Columna Fondo Reparo
                if (data.cdl_Fondo_Reparo_Porcentaje != null)
                {
                    //if(data.cdl_Fondo_Reparo_Porcentaje < 5)
                    //{
                    //    oCDL.cdl_Fondo_Reparo_Porcentaje = 5;
                    //}
                   // else
                    //{
                        oCDL.cdl_Fondo_Reparo_Porcentaje = data.cdl_Fondo_Reparo_Porcentaje;
                    //}

                    if (oCDL.cdl_Acumulado_Actividades_Planificadas == null)
                    {
                        oCDL.cdl_Acumulado_Actividades_Planificadas = 0;
                    }
                    //Diferenciamos el Calculo si es Adicional o Acumulado de Actividades
                    if (oCDL.cdl_NroNotaPedido == "Adicional")
                    {
                        oCDL.cdl_Fondo_Reparo_Monto = ((oCDL.cdl_Fondo_Reparo_Porcentaje + oCDL.cdl_Ajuste_Actualizacion + oCDL.cdl_Actividades_Adicionales ) * (oCDL.cdl_Fondo_Reparo_Porcentaje / 100));

                    }
                    else
                    {
                        oCDL.cdl_Fondo_Reparo_Monto = ((oCDL.cdl_Fondo_Reparo_Porcentaje + oCDL.cdl_Ajuste_Actualizacion + oCDL.cdl_Acumulado_Actividades_Planificadas) * (oCDL.cdl_Fondo_Reparo_Porcentaje / 100));

                    }

                    if (oCDL.cdl_Ajuste_Actualizacion == 0 && oCDL.cdl_Acumulado_Actividades_Planificadas == 0 && oCDL.cdl_NroNotaPedido != "Adicional") {
                        oCDL.cdl_Fondo_Reparo_Monto = 0;
                    }
                }
                else
                {
                    oCDL.cdl_Fondo_Reparo_Monto = 0;
                }



                if (oCDL.cdl_Fondo_Reparo_Monto == null)
                {
                    oCDL.cdl_Fondo_Reparo_Monto = 0;
                }
                oCDL.cdl_Total_A_Pagar = oCDL.cdl_Total_A_Facturar - oCDL.cdl_Fondo_Reparo_Monto;
                oCDL.cdl_Total_A_Pagar = (double)Math.Round((decimal)oCDL.cdl_Total_A_Pagar, 2);

                //REALIZAMOS LA MODIFICACION
                int IdCertificadoInsertado = new CertificadoNuevoNegocioDetalleLiquidacion().Update(oCDL);








                #endregion
               

                #region ReturnLista
                List<Certificados_detalle_liquidacion> listaLiquidacion = MostrarDetallesCertificadosLiquidaciones((int)data.cdl_IdCertificados);


                mReturn.isError = false;
                
                mReturn.data3 = listaLiquidacion;

                ReturnData dRet = new ReturnData();
               
             
                mReturn.data4 = _CargarPartidaPresupuestaria((int)data.cdl_IdCertificados).data1;
                mReturn.data = "Se modifico correctamente la Liquidación.";
                //Devolvemos el Total de Liquidacion - Total de Operarios.
                mReturn.data2 = _calcularTotalPestalaLiquidacion(data.cdl_IdCertificados);
                #endregion
            }

            catch (Exception ex)
            {
                mReturn.data = "Hubo un problema para modificar el registro.";
                mReturn.isError = true;
            }
            return mReturn;
        }

        /* //--> Metodo llamado cuando se hace Click en la Pestaña Liquidaciones <--// */
        [HttpPost]
        public ReturnData PresentaModificacion(ItemCertificadoModificacion data)
        {
            ReturnData mReturn = new ReturnData();
           
            try
            {

                //Actualiza los registros de Liquidaciones por los cambios de Actividades y/o Adicionales.
                _SeModificoPestanas((int)data.IdCertificado);
                mReturn.data2 = _calcularTotalPestalaLiquidacion((int)data.IdCertificado);
                //Mostramos el Listado Liquidaciones
                mReturn.data3 = MostrarDetallesCertificadosLiquidaciones((int)data.IdCertificado);
                //Mostramos el Listado de Partida Presupuestaria  (ultima Grilla)
                mReturn.data= _CargarPartidaPresupuestaria((int)data.IdCertificado).data1;
            }

            catch (Exception ex)
            {
                mReturn.data = "Hubo un problema para modificar el registro.";
                mReturn.isError = true;
            }
            return mReturn;
        }

        /* //--> Metodo ABM para la grilla de Aplicacion de Pagos en Pestaña Liquidaciones. <--// */
        [HttpPost]
        public ReturnData ABMAplicacionPagos_V2(itemCertificado_AplicacionesPago data)
        {
            ReturnData mReturn = new ReturnData();
            mReturn.data2 = "";
            mReturn.data1 = "";

            Certificados_detalle_aplicaciones_pagos oCDAPValidacion = new Certificados_detalle_aplicaciones_pagos();

            oCDAPValidacion = new CertificadoNuevoNegocioDetalleAplicacionPago().Get_One_Orden(data.cap_Id);

            if (oCDAPValidacion == null){data.cap_operacion = "I";}
            if (data.cap_Id == 0 || data.cap_Id <0) {data.cap_Id = -1;}

            int IdCertificadoInsertado = 0;

            try
            {

                #region Actualizacion de Datos
                if (data.cap_operacion == "I" || data.cap_operacion == "U")
                {
                    if (PuedeCargarAplicacionPagos(data.cap_IdCertificados, Math.Round(Convert.ToDouble(data.cap_Monto), 2), data.cap_Id))
                    {
                        //MODIFICACION
                        if (data.cap_operacion == "U")
                        {
                            if (data.cap_Id > 0)
                            {
                                Certificados_detalle_aplicaciones_pagos oCDAP = new Certificados_detalle_aplicaciones_pagos();
                                oCDAP = new CertificadoNuevoNegocioDetalleAplicacionPago().Get_One_Orden(data.cap_Id);

                                oCDAP.cap_NombreApellido = data.cap_NombreApellido;
                                data.cap_Monto = data.cap_Monto.Replace(".", ",");
                                oCDAP.cap_Monto = Math.Round(Convert.ToDouble(data.cap_Monto), 2);

                                IdCertificadoInsertado = new CertificadoNuevoNegocioDetalleAplicacionPago().Update(oCDAP);

                            }
                        }
                        if (data.cap_operacion == "I" || (data.cap_operacion == "U" && (data.cap_Id < 0)))
                        {
                            Certificados_detalle_aplicaciones_pagos oCDAP = new Certificados_detalle_aplicaciones_pagos();
                            oCDAP.cap_NombreApellido = data.cap_NombreApellido;
                            oCDAP.cap_Monto = Math.Round(Convert.ToDouble(data.cap_Monto), 2);
                            oCDAP.cap_IdCertificados = data.cap_IdCertificados;

                            //REALIZAMOS LA INSERCCION
                            IdCertificadoInsertado = new CertificadoNuevoNegocioDetalleAplicacionPago().Insert(oCDAP);
                        }
                    }
                    else
                    {
                        mReturn.data1 = "No se puede superar el Total a Pagar.";
                    }
                }
                else
                {
                        Certificados_detalle_aplicaciones_pagos oCDAP = new Certificados_detalle_aplicaciones_pagos();
                        oCDAP = new CertificadoNuevoNegocioDetalleAplicacionPago().Get_One_Orden(data.cap_Id);
                        IdCertificadoInsertado = new CertificadoNuevoNegocioDetalleAplicacionPago().Delete(oCDAP);

                }

                #endregion
                #region Mensajes Salida
                
                if (mReturn.data1.ToString() != "")
                {
                    mReturn.isError = true;
                    mReturn.data = "No se puede superar el Total a Pagar.";
                }
                else
                { 
                    if (IdCertificadoInsertado > 0) 
                    {
                        if (data.cap_operacion == "I") {
                            mReturn.data = "Se Inserto Correctamente el registro.";

                        }
                        else if (data.cap_operacion == "U")
                        {
                            mReturn.data = "Se Modifico Correctamente el registro.";
                        }
                        else
                        {
                            mReturn.data = "Se Elimino Correctamente el registro.";
                        }
                    
                    }
                    else
                    {
                        mReturn.isError = true;
                        mReturn.data = "Hubo un Problema para realizar la operacion.";
                    }
                }
                #endregion
                #region ReturnLista
                List<Certificados_detalle_aplicaciones_pagos> listaAplicacionPago = new CertificadoNuevoNegocioDetalleAplicacionPago().Get_All_Ordenes()
                                                                                        .Where(s => s.cap_IdCertificados == data.cap_IdCertificados)
                                                                                        .ToList();

                    mReturn.data3 = listaAplicacionPago;
                    //Devolvemos el Total de Liquidaciones - Total de Operarios
                    mReturn.data2 = _calcularTotalPestalaLiquidacion(data.cap_IdCertificados);
                    #endregion
            }

            catch (Exception ex)
            {
                mReturn.data = "Hubo un problema para modificar el registro.";
                mReturn.isError = true;
            }
            return mReturn;
        }

        /* //--> Metodo para aprobar el certificado.  <--// */
        [HttpPost]
        public ReturnData _AprobarCertificado(itemCertificadoDetalleAdicionalAprobacion data)
        {
            ReturnData mReturn = new ReturnData();
            List<Certificados_detalle_adicional> listaCertificadoDetalle = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                                       .Where(s => s.cda_IdCertificados == (long)data.IdCertificado).ToList();
            Certificados Certificado = new CertificadoNuevoNegocio().Get_One_Orden(data.IdCertificado);


            List<RolesUsuarios> ListaRoles = getUsuarioActual().Roles;

            int UsuarioId = getUsuarioActual().Id;




            var RolesUsuarioId = from s in ListaRoles
                                 where s.UsuariosId == UsuarioId
                                 select new
                                 {
                                     s.RolesId
                                 };
            /*
             Id=6 Jefe de Obra
             Id=11 Admnistracion
             Id=8 Coordinacion de Obra Pucheta
             */


            //cambiar a 8

            try
            {
                if (listaCertificadoDetalle.Any(s => s.cda_EstaAprobada == null || s.cda_EstaAprobada == false))
                {
                    mReturn.data = "No se puede firmar el Certificado, porque existen adicionales no aprobados.";
                    mReturn.isError = true;
                }
                else if (!(RolesUsuarioId.Any(s => s.RolesId == 8 || s.RolesId == 1)))
                // else if (!(RolesUsuarioId.Any(s => s.RolesId == 1)))
                {
                    mReturn.data = "El usuario con el que esta operando no presenta los permisos para firmar el certificado.";
                    mReturn.isError = true;
                }
                else
                {
                    Certificado.Estado = "A Pagar";
                    new CertificadoNuevoNegocio().Update(Certificado);
                    mReturn.data = "Se Firmo Correctamente el Certificado.";
                    mReturn.isError = false;

                }
            }

            catch (Exception ex)
            {
                mReturn.data = "Hubo un problema para aprobar los adicionales.";
                mReturn.isError = true;
            }
            return mReturn;
        }
        /* //--> Metodo para cerrar el certificado.  <--// */
        [HttpPost]
        public ReturnData _CerrarCertificado(itemCertificadoDetalleAdicionalAprobacion data)
        {
            ReturnData mReturn = new ReturnData();
            Certificados Certificado = new CertificadoNuevoNegocio().Get_One_Orden(data.IdCertificado);
            List<RolesUsuarios> ListaRoles = getUsuarioActual().Roles;

            int UsuarioId = getUsuarioActual().Id;




            var RolesUsuarioId = from s in ListaRoles
                                 where s.UsuariosId == UsuarioId
                                 select new
                                 {
                                     s.RolesId
                                 };
            /*
             Id=6 Jefe de Obra
             Id=11 Admnistracion
             Id=8 Coordinacion de Obra Pucheta
             */




            try
            {
                if (Certificado.Estado != "A Pagar")
                {
                    mReturn.data = "No se puede cerrar el Certificado, porque no esta aprobado el Certificado.";
                    mReturn.isError = true;
                }
                else if (!(RolesUsuarioId.Any(s => s.RolesId == 11)))
                {
                    mReturn.data = "El usuario con el que esta operando no presenta los permisos para firmar el certificado.";
                    mReturn.isError = true;
                }
                else
                {
                    Certificado.Estado = "Finalizado";
                    new CertificadoNuevoNegocio().Update(Certificado);
                    mReturn.data = "Se Firmo Correctamente el Certificado.";
                    mReturn.isError = false;

                }
            }

            catch (Exception ex)
            {
                mReturn.data = "Hubo un problema para aprobar los adicionales.";
                mReturn.isError = true;
            }
            return mReturn;
        }
        #endregion

        #endregion

        #region FuncionesGenericas
        private bool PuedeCargarAplicacionPagos(int idCertificado, double montoNuevo, int IdModificado)
        {
            bool PuedeCargar = true;
            List<Certificados_detalle_liquidacion> listaLiquidacion = MostrarDetallesCertificadosLiquidaciones(idCertificado);
            double TotalActividadesLiquidadas = 0;
            TotalActividadesLiquidadas = (double)listaLiquidacion.Sum(s => s.cdl_Total_A_Pagar);

            double? TotalPagos = 0;
            if(IdModificado != -1)
            {
                TotalPagos = new CertificadoNuevoNegocioDetalleAplicacionPago().Get_All_Ordenes().Where(s => 
                                    s.cap_IdCertificados == idCertificado
                                    && s.cap_Id != IdModificado
                                    )
                                    .Sum(p => p.cap_Monto);
            }
            else
            {
                TotalPagos = new CertificadoNuevoNegocioDetalleAplicacionPago().Get_All_Ordenes().Where(s =>
                                   s.cap_IdCertificados == idCertificado)
                                   .Sum(p => p.cap_Monto);
            }
           

            if ((TotalActividadesLiquidadas - (TotalPagos+montoNuevo)) < 0)
            {
                PuedeCargar = false;
            };

            return PuedeCargar;
        }
        private ReturnData getDateLiquidacion(int CertificadoActual, int IdNotaPedidoDetalle)
        {
            #region Definiciones

            ReturnData d = new ReturnData();
            Certificados Certificado = new CertificadoNuevoNegocio().Get_One_Orden(CertificadoActual);
            
           
         
            DateTime? FechaDesde ;
            DateTime FechaHasta ;
            #endregion

            try
            {
                 
			   //Buscamos la fecha mas reciente, que tenga aprobado la liquidacion.
			   DateTime? _fechaNotaPedido_Liquidada_Ult = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
																			.Where(s=>s.Act_Presenta_Liquidacion == true 
																							   && s.IdNotaPedidoDetalle == IdNotaPedidoDetalle
																							   && s.IdCertificados != CertificadoActual)
																			.OrderByDescending(p=>p.Act_F_Hasta).Select(f=> f.Act_F_Hasta).FirstOrDefault();

					if (_fechaNotaPedido_Liquidada_Ult != null)
					{
						//Fecha Desde de un Registro de DetalleCertificado x NotaPedidoDetalle. 
						FechaDesde = ((DateTime)_fechaNotaPedido_Liquidada_Ult).AddDays(1);
					}
					else
					{
                    //Fecha Parte Diario de cuando se trabajo por Primera vez
                    FechaDesde = new ParteDiario_Actividades_ContratistaNegocio().Get_All_ParteDiario_Actividades_Contratistas()
                                                             .Where(s => s.TrabajoHoy == true
                                                             && s.NotaPedidoDetalleId == IdNotaPedidoDetalle)
                                                             .OrderBy(f => f.Fecha)
                                                             .Select(s => s.Fecha).FirstOrDefault();

                }

				//Si es nulo,devolvemos un valor por Defecto.
				if (FechaDesde == null)
				{
					
					FechaDesde = new DateTime(1990, 01, 01, 7, 0, 0); //   -->  01/01/1990 7:00:00 AM
				}

				//Fecha Hasta de un Registro de DetalleCertificado, cuando se creo el Certificado 
				 FechaHasta = Certificado.FechaCreacion;

                if (FechaDesde > FechaHasta)
                {
                    FechaDesde = FechaHasta;
                }

                //Cargamos las Fechas a Devolver
                d.data1 = FechaDesde;
				d.data2 = FechaHasta;

               

            }
            catch (Exception ex)
            {
                throw new Exception("Error: getDateLiquidacion(): exception.", ex);
            }
            return d;
        }
        
        private ReturnData getNroCertificadoNuevo(int IdProyEnc, int IdContratEnc)
        {
            ReturnData d = new ReturnData();
            string nroCertificadoNuevo = "";
            long SecuencialActual = 0; 
            String IdProyecto, IdContratista, SecuencialString="";
            try

            {
             
                List<Certificados> lista = new CertificadoNuevoNegocio().Get_All_Ordenes()
                    .Where(p => p.IdProyecto == (long)IdProyEnc 
                             && p.IdContratista == (long)IdContratEnc)
                    .OrderByDescending(p => p.Secuenciador).ToList();

                if (lista.Count > 0) { SecuencialActual = 1 + (long)lista.FirstOrDefault().Secuenciador; }

                if (SecuencialActual == 0 ) { SecuencialActual = 1; }

               
                switch (IdProyEnc.ToString().Length)
                {
                    case 1:
                        IdProyecto = "000" + IdProyEnc.ToString();
                        break;
                    case 2:
                        IdProyecto = "00" + IdProyEnc.ToString();
                        break;
                    case 3:
                        IdProyecto = "0" + IdProyEnc.ToString();
                        break;
                    default:
                        IdProyecto = "" + IdProyEnc.ToString();
                        break;
                }
                switch (IdContratEnc.ToString().Length)
                {
                    case 1:
                        IdContratista = "000" + IdContratEnc.ToString();
                        break;
                    case 2:
                        IdContratista = "00" + IdContratEnc.ToString();
                        break;
                    case 3:
                        IdContratista = "0" + IdContratEnc.ToString();
                        break;
                    default:
                        IdContratista = "" + IdContratEnc.ToString();
                        break;
                }
                switch (SecuencialActual.ToString().Length)
                {
                    case 1:
                        SecuencialString = "000" + SecuencialActual.ToString();
                        break;
                    case 2:
                        SecuencialString = "00" + SecuencialActual.ToString();
                        break;
                    case 3:
                        SecuencialString = "0" + SecuencialActual.ToString();
                        break;
                    default:
                        SecuencialString = "" + SecuencialActual.ToString();
                        break;
                }

                nroCertificadoNuevo = IdProyecto + '-' + IdContratista + '-' + SecuencialString;
                d.data= nroCertificadoNuevo;
                d.data1 = SecuencialActual;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: getUsuarioActual(): exception.", ex);
            }
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
        public List<Proyecto> GetProyectosMostrar()
        {
            Usuarios oUsuario = getUsuarioActual();
            List<Proyecto> lstProyectos = new List<Proyecto>();

            if (oUsuario.Roles.Any(s => s.Id == 1))
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
            return lstProyectos;
        }
        public ReturnData GetProyectos()
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = GetProyectosMostrar();
            return data;
        }

        [HttpGet]
        public ReturnData GetContratistasxCertificado(int id)
        {
            ReturnData d = new ReturnData();

            List<NotaPedido> listaContratistas = new NotaPedidoNegocio().Get_All_Ordenes().Where(p => p.IdProyecto == id)
                                                            .OrderByDescending(p => p.Secuencial).ToList();
            List<Proyecto_Contratista> listaContratistaProyecto = new Proyecto_ContratistaNegocio().Get_All_Proyecto_Contratista(id);
            

            var ContratistasLista = from s in listaContratistas
                                    join a in listaContratistaProyecto
                                    on s.IdContratista equals a.ContratistaId

                                    select new {
                                        contratistaId = s.IdContratista,
                                        sContratistas = a.sContratistas
                                    };


            d.isError = false;
            d.data = ContratistasLista.Distinct();
            d.data2 = ContratistasLista.Distinct().LongCount();
            return d;
        }

        [HttpGet]
        public ReturnData GetContratistas_En_Certificado(int id)
        {
            ReturnData d = new ReturnData();

            List<int> IdContratistas = new CertificadoNuevoNegocio().Get_All_Ordenes().Select(s=>(int)s.IdContratista).Distinct().ToList();

            List<Proyecto_Contratista> listaContratistaProyecto = new List<Proyecto_Contratista>();
            foreach (var item in IdContratistas)
            {
                Proyecto_Contratista Contratista = new Proyecto_Contratista
                {
                    ContratistaId = item,
                    sContratistas = new ContratistasNegocio().Get_One_Contratistas(item).Nombre
                };
                listaContratistaProyecto.Add(Contratista);
            }
           
            d.isError = false;
            d.data = listaContratistaProyecto;
            return d;
        }

        [HttpGet]
        public ReturnData GetContratistas(int id)
        {
            ReturnData d = new ReturnData();
            List<Proyecto_Contratista> listaContratistaProyecto = new Proyecto_ContratistaNegocio().Get_All_Proyecto_Contratista(id);


            d.isError = false;
            d.data = listaContratistaProyecto;
            return d;
        }
        private bool getEstaLiquidado(int pIdCertificadoDetalle)
        {
            
            bool EstaLiquidado = false;

            //BUSCAMOS SI EL CERTIFICADO ANTERIOR
            Certificados_detalle_planificado CertificadoDetalleActual = new CertificadoNuevoNegocioDetalle()
                                                                            .Get_One_Orden(pIdCertificadoDetalle);
            
            Certificados_detalle_planificado oCertificadoDetalle = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
                .Where(s => s.IdNotaPedidoDetalle == CertificadoDetalleActual.IdNotaPedidoDetalle
                        && s.Cantidad_Asignada==s.Act_Acum_Total_Unid
                        && s.Cantidad_Asignada !=0 && s.Cantidad_Asignada != null
                        && s.Id != pIdCertificadoDetalle
                        && s.Id < pIdCertificadoDetalle)
                .OrderByDescending(p=> p.Id)
                .FirstOrDefault();
                        
            if (oCertificadoDetalle != null )
            {  EstaLiquidado = true;}




                    
            return EstaLiquidado;
        }
        private double _calcularTotalPestalaLiquidacion(int IdCertificado)
        {
           

            List<Certificados_detalle_liquidacion> listaLiquidacion = MostrarDetallesCertificadosLiquidaciones(IdCertificado);

            List<Certificados_detalle_aplicaciones_pagos> listaAplicacionPago = new CertificadoNuevoNegocioDetalleAplicacionPago().Get_All_Ordenes()
                                                                                .Where(s => s.cap_IdCertificados == IdCertificado)
                                                                                .ToList();
            double TotalOperarios = 0;
            double TotalActividades = 0;
            double total = 0;


            if (listaAplicacionPago.Count > 0)
            {
                TotalOperarios = (double)listaAplicacionPago.Sum(s => s.cap_Monto);
            }
            else
            {
                TotalOperarios = 0;
            }
            TotalActividades = (double)listaLiquidacion.Sum(s => s.cdl_Total_A_Pagar);

            total = TotalActividades - TotalOperarios;
            total = (double)Math.Round((decimal)total, 2);

            return total;
        }
        private double _calcularIndiceActual(int IdIndice)
        {

            double total = 0;
            DateTime fechaAhora = new DateTime();
            fechaAhora = DateTime.Now;
            List<Indice_Valores> Indices = new Indice_ValoresNegocio().Get_All_Indice_Valores()
                .Where(s=>s.IdIndice == IdIndice).ToList()
                  .OrderBy(s => s.Ano)
                  .ThenByDescending(s => s.Mes)
                  .ToList();

            if(Indices.Count>0) { 
                 total = Indices.FirstOrDefault().Valor;
            }
           

            return total;
        }
        private ReturnData getActAcumuladoAnterior(int pIdNotaPedidoDetalle, int pIdCertificadoDetalle)
        {
            ReturnData d = new ReturnData();
            double AcumuladoAnterior = 0;
            double AcumuladoTotalUnidad = 0;
            double AcumuladoAnteriorMoneda = 0;
            double AcumuladoTotalMoneda = 0;

            Certificados_detalle_planificado oCertificadoDetalle = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
                .Where(s => s.IdNotaPedidoDetalle == pIdNotaPedidoDetalle
                       // && s.Cantidad_Asignada == s.Act_Acum_Total_Unid
                        && s.Cantidad_Asignada != 0 && s.Cantidad_Asignada != null
                        && s.Id != pIdCertificadoDetalle
                        && s.Id < pIdCertificadoDetalle)
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            if (oCertificadoDetalle != null)
            {
                if (oCertificadoDetalle.Act_Acum_Total_Unid != null && oCertificadoDetalle.Act_Acum_Total_Unid != 0)
                {
                    AcumuladoAnterior = (double)oCertificadoDetalle.Act_Acum_Total_Unid;
                    AcumuladoTotalUnidad = AcumuladoAnterior;

                }

                if (oCertificadoDetalle.Act_Acum_Actual_Moneda != null && oCertificadoDetalle.Act_Acum_Actual_Moneda !=0)
                {
                    AcumuladoAnteriorMoneda = (double)oCertificadoDetalle.Act_Acum_Actual_Moneda;
                    AcumuladoTotalMoneda = AcumuladoAnteriorMoneda;
                }
                
            }

            d.data= AcumuladoAnterior;
            d.data1 = AcumuladoTotalUnidad;
            d.data2 = AcumuladoAnteriorMoneda;
            d.data3 = AcumuladoTotalMoneda;
            return d;
        }

        public ReturnData ActualizarCantidadProyecto(itemCertificadoDetalleActualizacionProyecto omodel)
        {
            ReturnData data = new ReturnData();
            int IdCorregido_PD = 0;

            #region Actualizar_ParteDiario_ActividadContratista
            ParteDiario_Actividades_Contratista oPD = new ParteDiario_Actividades_ContratistaNegocio().Get_All_ParteDiario_Actividades_Contratistas()
                                                                  .Where(s => s.NotaPedidoDetalleId == omodel.IdNotaPedidoDetalle
                                                                  && s.Fecha <= omodel.FechaHastaCertif
                                                                    && s.ContratistasId == omodel.IdContratista
                                                                  && s.TrabajoHoy==true
                                                                  )
                                                                  .OrderBy(p => p.Id)
                                                                  .FirstOrDefault();

            if(oPD != null)
            {
                oPD.TrabajoHoy = true;
                oPD.AvanceActual = (decimal)omodel.AvanceCargado;
                oPD.TipoRegistro = "Certificado-Automatico";
                IdCorregido_PD = new ParteDiario_Actividades_ContratistaNegocio().Update(oPD);

            }
            //Modificamos el Avance

            #endregion
            #region Actualizar_NotaPedidoDetalle
            //Actualizamos la nota de Pedido Detalle
            double AvanceAcumulado = 0;
            if (IdCorregido_PD > 0)
            {
                List<ParteDiario_Actividades_Contratista> listas = new ParteDiario_Actividades_ContratistaNegocio().Get_All_ParteDiario_Actividades_Contratistas()
                 .Where(s => s.NotaPedidoDetalleId == omodel.IdNotaPedidoDetalle
                         && s.Fecha <= omodel.FechaHastaCertif
                         && s.ContratistasId == omodel.IdContratista
                         )
                 .ToList();
                AvanceAcumulado = (double)listas.Sum(x => x.AvanceActual);
            }

            NotaPedido_Detalle oNPD = new NotaPedido_DetalleNegocio().Get_One_Orden(omodel.IdNotaPedidoDetalle);
            if (oNPD != null)
            {

            
            oNPD.Avance_Actual = AvanceAcumulado;

            int Id_Npd =new NotaPedido_DetalleNegocio().Update(oNPD);
}
            #endregion
            #region ActualizamosPorcentaje_IDPPA
            Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(omodel.Planificacion_Proyecto_ActividadesId);
            decimal mCantidadPlanificada = Convert.ToDecimal(oPPA.Cantidad);
            decimal mCantidadTotalAvance = (decimal)new NotaPedido_DetalleNegocio().Get_All_Ordenes()
                                            .Where(s=> s.IdPPA== omodel.Planificacion_Proyecto_ActividadesId).Sum(p=>p.Avance_Actual);
            decimal mAvacenTotalPorcentaje = 0;

            if (mCantidadTotalAvance > 0)
            {
                mAvacenTotalPorcentaje = (mCantidadTotalAvance / mCantidadPlanificada) * 100;
            }

            oPPA.Avance = float.Parse(mAvacenTotalPorcentaje.ToString("#0.00"));

            int mResult = new Planificacion_Proyecto_ActividadesNegocio().Update(oPPA);

            #endregion

            if(mResult > 0)
            {
                data.isError = false;
                //data.data=;

            }else
            {
                data.isError = true;
            }
            
            return data;
        }
        private string _ConsultarEstado(int IdCertificado)
        {
            string d = "";

            try
            {
                 d = new CertificadoNuevoNegocio().Get_One_Orden(IdCertificado).Estado;
            }
            catch (Exception e)
            {
                throw;
            }

            return d;
        }
        private bool _SeModificoPestanas(int IdCertificado)
        {
            bool d =false;

            try
            {
                Certificados_detalle_planificado ocdp = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
                    .Where(s =>  s.IdCertificados == IdCertificado
                    
                    ).FirstOrDefault();


                Certificados_detalle_adicional ocda = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                   .Where(s =>  s.cda_IdCertificados == IdCertificado).FirstOrDefault();

                
                if(ocda != null || ocdp != null) {

                    d = true;
                    _ActualizacionTablaLiquidaciones(IdCertificado);
                }

            }
            catch (Exception e)
            {
               
                throw;
            }


            return d;
        }
        private void _ActualizacionTablaLiquidaciones(int IdCertificado)
        {
            ReturnData d = new ReturnData();
            int Id_Npd = 0;
            try
            {

                List<Certificados_detalle_liquidacion> oListaLiquidaciones = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                .Where(s => s.cdl_IdCertificados == IdCertificado).ToList();

                if (oListaLiquidaciones.Count >0)
                {
                    #region Actualizar_Liquidacion
               
                    foreach (var item in oListaLiquidaciones)
                {
                     List<Certificados_detalle_planificado> ListaActividades = new CertificadoNuevoNegocioDetalle().Get_All_Ordenes()
                   .Where(s => s.NroNotaPedido == item.cdl_NroNotaPedido && s.IdCertificados == IdCertificado).ToList();
                        if(ListaActividades.Count> 0)
                        {
                             itemCertificado_Liquidaciones data = new itemCertificado_Liquidaciones();
                            data.cdl_Acumulado_Actividades_Planificadas= ListaActividades.Sum(s => s.Act_Cert_Actual_Moneda);
                            data.cdl_IdCertificados= IdCertificado;
                            data.cdl_Id = item.cdl_Id;
                            data.cdl_Fondo_Reparo_Porcentaje = item.cdl_Fondo_Reparo_Porcentaje;
                            data.cdl_Descuento_Por_Adelanto = item.cdl_Descuento_Por_Adelanto;
                            data.cdl_Descuento_Por_Anticipo_Porcentaje = item.cdl_Descuento_Por_Anticipo_Porcentaje;
                            data.cdl_Iva_Porcentaje = item.cdl_Iva_Porcentaje;
                            data.cdl_Total_A_Pagar = item.cdl_Total_A_Pagar;

                            Update_Liquidacion(data);
                            Id_Npd= item.cdl_Id;
                           

                        }
                       
                        
                }


                    #endregion
                    #region Actualizar_Adicional
                            

                            //Vemos si Existe el registro adicional sin el cambio de Nro Nota Pedido
                            if (new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                                                                   .Where(s => s.cdl_NroNotaPedido == null  && s.cdl_IdCertificados == IdCertificado)
                                                                   .FirstOrDefault() != null) 
                            {
                                        //lo buscamos y lo actualizamos.
                                        Certificados_detalle_liquidacion CertificadoLiquidacion = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                                                   .Where(s => s.cdl_NroNotaPedido == null&& s.cdl_IdCertificados == IdCertificado)
                                                   .FirstOrDefault();
                                        CertificadoLiquidacion.cdl_NroNotaPedido = "Adicional";
                                        new CertificadoNuevoNegocioDetalleLiquidacion().Update(CertificadoLiquidacion);


                            }    
                            
                            Certificados_detalle_liquidacion Cer = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                                                                   .Where(s =>  s.cdl_NroNotaPedido == "Adicional"
                                                                   && s.cdl_IdCertificados== IdCertificado
                                                                   ).FirstOrDefault();
                        if(Cer != null )
                            {
                                //Actualizamos el registro que se utiliza para ADICIONALES.
                                Cer.cdl_Actividades_Adicionales = new CertificadoNuevooDetallAdicionalNegocio().Get_All_Ordenes()
                                            .Where(s => s.cda_IdCertificados == IdCertificado).Sum(p => p.cda_Moneda_Certificado_Actual);
                                //Re-Calculo de Totales:
                                Cer.cdl_Neto_Facturar = Cer.cdl_Actividades_Adicionales;
                                Cer.cdl_Total_A_Facturar = Cer.cdl_Neto_Facturar;
                     
                                
                                if (Cer.cdl_Fondo_Reparo_Monto == null)
                                {
                                    Cer.cdl_Fondo_Reparo_Monto = 0;
                                }
                                
                                //Fondo Reparo
                                               
                                Cer.cdl_Fondo_Reparo_Monto = (
                                            (Cer.cdl_Fondo_Reparo_Porcentaje + Cer.cdl_Actividades_Adicionales) * (Cer.cdl_Fondo_Reparo_Porcentaje / 100)
                                            
                                            );
                                //Calculo Total a Pagar
                                Cer.cdl_Total_A_Pagar = Cer.cdl_Total_A_Facturar - Cer.cdl_Fondo_Reparo_Monto;

                        new CertificadoNuevoNegocioDetalleLiquidacion().Update(Cer);
                            }
                    #endregion
                    #region Limpieza
                    List<Certificados_detalle_liquidacion> oListaLiquidacionesLimpieza = new CertificadoNuevoNegocioDetalleLiquidacion().Get_All_Ordenes()
                    .Where(s => s.cdl_IdCertificados == IdCertificado).ToList();
                    foreach(var item in oListaLiquidacionesLimpieza)
                    {
                        if(item.cdl_Acumulado_Actividades_Planificadas == 0)
                        {
                            new CertificadoNuevoNegocioDetalleLiquidacion().Delete(item);
                        }
                    }
                    #endregion
                }
                else
                {
                    _CargadoInicialLiquidaciones(IdCertificado);
                }



            }
            catch (Exception e)
            {

                throw;
            }
        }

        #region Combos
            public ReturnData GetAllRubros()
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = new Planificacion_Actividades_RubroNegocio().Get_All_Planificacion_Actividades_Rubros();
            return data;
        }

        public ReturnData GetAllActividadXRubro(int IdRubro)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = new Planificacion_ActividadesNegocio().Get_All_Planificacion_Actividades(Convert.ToInt16(IdRubro));
            return data;
        }
        public ReturnData GetAllUbicaciones(int IdProyecto)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(IdProyecto);
            return data;
        }
        #endregion

        #endregion

        #region PDFs
        public IActionResult _GenerarPDFCertificados(int IdCertificado)
        {
            ItemCertificadoPDFVM CertificadoPDFVM = new ItemCertificadoPDFVM();
            CertificadoPDFVM.FechaCreacion = DateTime.Now;
            CertificadoPDFVM._NombreUsuario = getUsuarioActual().NombreYApellido;
            CertificadoPDFVM._FechaCreacion = Convert.ToString(((DateTime)CertificadoPDFVM.FechaCreacion).ToString("0:dd/MM/yyyy")).Remove(0, 2);

            Certificados Certificado = new CertificadoNuevoNegocio().Get_One_Orden((int)IdCertificado);

            CertificadoPDFVM._NotadePedido = Certificado.NroCertificado;
            CertificadoPDFVM._ContratistaCertificado = new ContratistasNegocio().Get_One_Contratistas((int)Certificado.IdContratista).Nombre;
            CertificadoPDFVM._ProyectoCertificado = new ProyectoNegocio().Get_One_Proyecto((int)Certificado.IdProyecto).Nombre; ;
            CertificadoPDFVM._FechaCertificado = Convert.ToString(((DateTime)Certificado.FechaCreacion).ToString("0:dd/MM/yyyy")).Remove(0, 2);
            CertificadoPDFVM.ListaActividades = MostrarDetallesCertificadosActividades(IdCertificado)
                                                                    .FindAll(s => s.Act_Avance_Periodo_Unid != 0
                                                                    && s.Act_Avance_Periodo_Unid != null
                                                                    );
            CertificadoPDFVM.ListaAdicionales = MostrarDetallesCertificadosAdicionales(IdCertificado);
            _SeModificoPestanas(IdCertificado);
            CertificadoPDFVM.Listaliquidaciones = MostrarDetallesCertificadosLiquidaciones(IdCertificado);
            CertificadoPDFVM.ListaPartidaPresupuestaria = (List<itemCertificado_CertificadoLiquidacionPPresupuestaria>)_CargarPartidaPresupuestaria(IdCertificado).data1;

            return new ViewAsPdf("_GenerarPDFCertificados", CertificadoPDFVM)
            {

                FileName = $"Certificado_{CertificadoPDFVM._ProyectoCertificado}-{CertificadoPDFVM._ContratistaCertificado}-{CertificadoPDFVM._FechaCreacion}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageSize = Rotativa.AspNetCore.Options.Size.A4

            };

        }
        #endregion
    }

}