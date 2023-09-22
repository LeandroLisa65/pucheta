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
using System.Collections;
using Microsoft.EntityFrameworkCore.Internal;
using System.Runtime.ConstrainedExecution;

namespace Gestion.Web.Controllers
{
    [Authorize]
    public class NotaPedidoController : Controller
    {
        // GET: OrdenesController
        public ActionResult Index()
        {
            return View();
        }


       #region Nota de Pedido - Cabecera

        //Trae las Notas de Pedidos
        public ActionResult _NotaPedidoGrilla()
        {
            DateTime mFecDesde = DateTime.Now.AddMonths(-2);
            DateTime mFecHasta = DateTime.Now;
            List<NotaPedido> Lista = new List<NotaPedido>();
            Lista = new NotaPedidoNegocio().Get_All_Ordenes().Where(p => p.Fecha_Creacion >= mFecDesde
                                                //&& p.Fecha_Creacion <= mFecHasta
                                                && p.Estado!= "Finalizado"
                                                )
                .ToList();
            List<Proyecto> lstProyectos = GetProyectosMostrar();
            //            customers.Any(cus => names.Contains(cus.FirstName));
            // result = lista.Where(a => listb.Any(b => a.ToLower() == b.ToLower()));

            Lista = Lista.Where(a => lstProyectos.Any(b => a.IdProyecto == b.Id)).ToList();


            List<Contratistas> lstContratistas = new ContratistasNegocio().Get_All_Contratistas();
            List<Usuarios> lstUsuarios = new UsuariosNegocio().Get_Usuarios();

            foreach (var item in Lista)
            {
                try
                {
                    if (item.IdProyecto != 0)
                        item.sProyecto = lstProyectos.Where(p => p.Id == Convert.ToInt32(item.IdProyecto)).FirstOrDefault().Nombre;
                }
                catch (Exception)
                {
                    item.sProyecto = "S/D";
                }

                try
                {
                    if (item.IdContratista != 0)
                        item.sContratista = lstContratistas.Where(p => p.Id == item.IdContratista).FirstOrDefault().Nombre;
                }
                catch (Exception)
                {
                    item.sContratista = "S/D";
                }

                try
                {
                    if (item.IdUsuario_Creacion != 0)
                        item.sUsuarioCreo = lstUsuarios.Where(p => p.Id == item.IdUsuario_Creacion).FirstOrDefault().NombreYApellido;
                }
                catch (Exception)
                {
                    item.sUsuarioCreo = "S/D";
                }
                
            }
            Lista = Lista.OrderByDescending(p => p.Fecha_Creacion).ToList();
            return PartialView(Lista);
        }
       
        public ReturnData _NotaPedidoGrillaFiltro(FiltroPantallaGenerico oFiltro)
        {
            ReturnData oReturn = new ReturnData();
            List<NotaPedido> Lista = new List<NotaPedido>();
            //Desglose del Filtro
            DateTime mFecDesde = Convert.ToDateTime(oFiltro.mfd);
            DateTime mFecHasta = Convert.ToDateTime(oFiltro.mfh);
            int mIdProyecto = 0;
            int mIdContratista = 0;
            string mEstado = "";
            if (oFiltro.strFiltroTexto1 != "")
            {
                var mIndices = oFiltro.strFiltroTexto1.Split(',');
                if (mIndices[0].Trim().Length == 0)
                {
                    mIndices[0] = "0";
                }
                mIdProyecto = Convert.ToInt32(mIndices[0]);
                if (mIndices[1].Trim().Length == 0)
                {
                    mIndices[1] = "0";
                }
                mIdContratista = Convert.ToInt32(mIndices[1]);
                mEstado = mIndices[2];
            }

            //if (mFecDesde == DateTime.MinValue)
            //{
            //    mFecDesde = DateTime.Now.AddMonths(-1);
            //}
            //if (mFecHasta == DateTime.MinValue)
            //{
            //    mFecHasta = DateTime.Now.AddDays(1);
            //}

            Lista = new NotaPedidoNegocio().Get_All_Ordenes().ToList();

            if (mFecDesde > DateTime.MinValue)
            {
                Lista = Lista.Where(p => p.Fecha_Creacion >= mFecDesde).ToList();
            }

            if (mFecHasta > DateTime.MinValue)
            {
                Lista = Lista.Where(p => p.Fecha_Creacion <= mFecHasta).ToList();
            }


           // Lista = new NotaPedidoNegocio().Get_All_Ordenes().Where(p => p.Fecha_Creacion >= mFecDesde && p.Fecha_Creacion <= mFecHasta).ToList();

            if (mIdProyecto != 0)
                Lista = Lista.Where(p => p.IdProyecto == mIdProyecto).ToList();

            if(mIdContratista != 0)
                Lista = Lista.Where(p => p.IdContratista == mIdContratista).ToList();

            if (mEstado != "0")
                Lista = Lista.Where(p => p.Estado == mEstado).ToList();

            List <Proyecto> lstProyectos = new ProyectoNegocio().Get_All_Proyecto();
            List<Contratistas> lstContratistas = new ContratistasNegocio().Get_All_Contratistas();
            List<Usuarios> lstUsuarios = new UsuariosNegocio().Get_Usuarios();

            foreach (var item in Lista)
            {
                try
                {
                    if (item.IdProyecto != 0)
                        item.sProyecto = lstProyectos.Where(p => p.Id == Convert.ToInt32(item.IdProyecto)).FirstOrDefault().Nombre;
                }
                catch (Exception)
                {
                    item.sProyecto = "S/D";
                }

                try
                {
                    if (item.IdContratista != 0)
                        item.sContratista = lstContratistas.Where(p => p.Id == item.IdContratista).FirstOrDefault().Nombre;
                }
                catch (Exception)
                {
                    item.sContratista = "S/D";
                }

                try
                {
                    if (item.IdUsuario_Creacion != 0)
                        item.sUsuarioCreo = lstUsuarios.Where(p => p.Id == item.IdUsuario_Creacion).FirstOrDefault().NombreYApellido;
                }
                catch (Exception)
                {
                    item.sUsuarioCreo = "S/D";
                }

            }
            Lista = Lista.OrderByDescending(p => p.Fecha_Creacion).ToList();
            oReturn.data = Lista;
            //return PartialView(oResult);
            return oReturn;
        }
        public ActionResult _NotaPedidoAbm(int id)
        {

            
            ItemNotaPedido data = new ItemNotaPedido();
            data.Id = id;
            data.idProyecto = 0;
            List <Proyecto> lProyectos = GetProyectosMostrar().Where(p => p.Estado != "Ejecutado").OrderBy(p => p.Nombre).ToList();
            data.lstProyectos = lProyectos;
            
            List<Indices> Indices = new IndicesNegocio().Get_All_Indices();
            data.lstIndices = Indices;
     
            if (id != 0)
            {
                data.oNP = new NotaPedidoNegocio().Get_One_Orden(id);
                data.idProyecto = data.oNP.IdProyecto;
              
          
                List<NotaPedido_Detalle> lstNP_Det = CargoNotaPedido_Detalles(Convert.ToInt32(id));
                data.lstNP_det = lstNP_Det;
            }
            return PartialView(data);
        }
       
        public ReturnData GetContratistaProyecto(int IdProyecto)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            if (IdProyecto != 0)
            {
                List<Proyecto_Contratista> lstProyCont = new Proyecto_ContratistaNegocio().Get_All_Proyecto_Contratista(IdProyecto);
                data.data = lstProyCont.OrderBy(p => p.sContratistas).ToList();
            }
            else
            {
                data.data = new ContratistasNegocio().Get_All_Contratistas().OrderBy(p => p.Nombre).ToList();
            }
            return data;
        }

        public ReturnData GetProyectos()
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = GetProyectosMostrar();
            return data;
        }

        public List<Proyecto> GetProyectosMostrar()
        {

            Usuarios oUsuario = getUsuarioActual();
            List<Proyecto> lstProyectos = new List<Proyecto>();

            if (oUsuario.Id == 1)
            {
                lstProyectos = new ProyectoNegocio().Get_All_Proyecto().OrderBy(p => p.Nombre).ToList();
            }
           else 
            {
                lstProyectos = new ProyectoNegocio().Get_All_Proyecto().Where(p => p.UsuariosId == oUsuario.Id).OrderBy(p => p.Nombre).ToList();

            }
          
            if (lstProyectos.Count == 0)
            {
                lstProyectos = new ProyectoNegocio().Get_All_Proyecto().OrderBy(p => p.Nombre).ToList();
            }
            return lstProyectos;
        }

        [HttpPost]
        public ReturnData NotaPedidoGraba(NotaPedido data)
        {
            
            ReturnData d = new ReturnData();
           d = ValidoNotaPedido(data);
           d.isError = false;
            if (d.isError == false)
            {
                Usuarios oUsuario = getUsuarioActual();
                data.IdUsuario_Creacion = oUsuario.Id;
                data.Estado = "Abierto";
               
                   
                if (data != null)
                {
                    int idc;
                    if (data.Id > 0)
                    {
                        List<NotaPedido> Registro = new NotaPedidoNegocio().Get_All_Ordenes().Where(p => p.Id == data.Id)
                                                           .ToList();
                        
                        if (Registro.FirstOrDefault().Secuencial == null) 
                        {
                            List<NotaPedido> listaGeneral = new NotaPedidoNegocio().Get_All_Ordenes().Where(p => p.IdProyecto == data.IdProyecto && p.IdContratista == data.IdContratista)
                                                            .OrderByDescending(p => p.Secuencial).ToList();

                            if (listaGeneral.Count > 0) { data.Secuencial = 1 + listaGeneral.FirstOrDefault().Secuencial; }
                            if (data.Secuencial == null || data.Secuencial == 0) { data.Secuencial = 1; }
                            String IdProyecto, IdContratista, SecuencialString;
                            switch (data.IdProyecto.ToString().Length)
                            {
                                case 1:
                                    IdProyecto = "000" + data.IdProyecto.ToString();
                                    break;
                                case 2:
                                    IdProyecto = "00" + data.IdProyecto.ToString();
                                    break;
                                case 3:
                                    IdProyecto = "0" + data.IdProyecto.ToString();
                                    break;
                                default:
                                    IdProyecto = "" + data.IdProyecto.ToString();
                                    break;
                            }
                            switch (data.IdContratista.ToString().Length)
                            {
                                case 1:
                                    IdContratista = "000" + data.IdContratista.ToString();
                                    break;
                                case 2:
                                    IdContratista = "00" + data.IdContratista.ToString();
                                    break;
                                case 3:
                                    IdContratista = "0" + data.IdContratista.ToString();
                                    break;
                                default:
                                    IdContratista = "" + data.IdContratista.ToString();
                                    break;
                            }
                            switch (data.Secuencial.ToString().Length)
                            {
                                case 1:
                                    SecuencialString = "000" + data.Secuencial.ToString();
                                    break;
                                case 2:
                                    SecuencialString = "00" + data.Secuencial.ToString();
                                    break;
                                case 3:
                                    SecuencialString = "0" + data.Secuencial.ToString();
                                    break;
                                default:
                                    SecuencialString = "" + data.Secuencial.ToString();
                                    break;
                            }

                            data.NroNP = IdProyecto + '-' + IdContratista + '-' + SecuencialString;
                            

                        }  
                               

                        idc = new NotaPedidoNegocio().Update(data);
                        d.data2 = data.NroNP;
                    }
                    else
                    {
                        List<NotaPedido> lista = new NotaPedidoNegocio().Get_All_Ordenes().Where(p => p.IdProyecto == data.IdProyecto && p.IdContratista == data.IdContratista)
                                                            .OrderByDescending(p => p.Secuencial).ToList();

                        if (lista.Count > 0) { data.Secuencial = 1 + lista.FirstOrDefault().Secuencial; }
                        if (data.Secuencial == null || data.Secuencial == 0) { data.Secuencial = 1; }

                        String IdProyecto, IdContratista, SecuencialString;
                        switch (data.IdProyecto.ToString().Length)
                        {
                            case 1:
                                IdProyecto = "000" + data.IdProyecto.ToString();
                                break;
                            case 2:
                                IdProyecto = "00" + data.IdProyecto.ToString();
                                break;
                            case 3:
                                IdProyecto = "0" + data.IdProyecto.ToString();
                                break;
                            default:
                                IdProyecto = "" + data.IdProyecto.ToString();
                                break;
                        }
                        switch (data.IdContratista.ToString().Length)
                        {
                            case 1:
                                IdContratista = "000" + data.IdContratista.ToString();
                                break;
                            case 2:
                                IdContratista = "00" + data.IdContratista.ToString();
                                break;
                            case 3:
                                IdContratista = "0" + data.IdContratista.ToString();
                                break;
                            default:
                                IdContratista = "" + data.IdContratista.ToString();
                                break;
                        }
                        switch (data.Secuencial.ToString().Length)
                        {
                            case 1:
                                SecuencialString = "000" + data.Secuencial.ToString();
                                break;
                            case 2:
                                SecuencialString = "00" + data.Secuencial.ToString();
                                break;
                            case 3:
                                SecuencialString = "0" + data.Secuencial.ToString();
                                break;
                            default:
                                SecuencialString = "" + data.Secuencial.ToString();
                                break;
                        }

                        data.NroNP = IdProyecto + '-' + IdContratista + '-' + SecuencialString;
                        d.data2 = data.NroNP;
                        if (data.NroNP.Length > 0)
                        {
                            idc = new NotaPedidoNegocio().Insert(data);
                        }
                        else
                        {
                            idc = 0;
                        }

                      
                    }
                    if (idc > 0)
                    {
                        d.isError = false;
                        d.data = "Se han grabado los datos OK.";
                        d.data1 = idc;
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
            }
            return d;
        }

        private ReturnData ValidoNotaPedido(NotaPedido oNP)
        {
            ReturnData oReturn = new ReturnData();
            oReturn.isError = false;
            oReturn.data = "";
            //Validaciones a hacer
            #region 1 - Campos Completos
            if (oNP.IdContratista == 0)
            {
                oReturn.isError = true;
                oReturn.data = "Debe seleccionar un contratista";
            }
            if (oNP.IdProyecto == 0)
            {
                oReturn.isError = true;
                oReturn.data = "Debe seleccionar un proyecto";
            }
           /* if (oNP.NroNP.Trim().Length == 0)
            {
                oReturn.isError = true;
                oReturn.data = "Debe ingresar un numero de nota de pedido valida";
            }
           
            //2 - Que no exista ya el mismo nro de nota de pedido.
            var mCount = new NotaPedidoNegocio().Get_All_Ordenes().Where(p => p.NroNP.ToUpper() == oNP.NroNP.Trim().ToUpper()).Count();
            if (mCount > 0)
            {
                oReturn.isError = true;
                oReturn.data = "Ya existe una nota de pedido con ese nro de nota de pedido";
            }*/
            //3 - Fecha de Nota de Pedido a Futuro
            #endregion
            if (oNP.Fecha_Creacion >= DateTime.Now.AddHours(10))
            {
                oReturn.isError = true;
                oReturn.data = "Fecha de nota de pedido no puede ser mayor a hot";
            }
            return oReturn;
        }
        
        #endregion

        #region Nota de Pedido - Detalle
        public ReturnData GetUbicacionesProyecto(int IdProyecto)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = new Proyecto_UbicacionesNegocio().Get_X_IdProyecto(IdProyecto);
            return data;
        }

        public ReturnData GetActividadesUbicacionesProyecto(long id)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            //data.data = new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades().Where(p => p.Finalizados == false && p.Proyecto_UbicacionesId == id).ToList();
            data.data = new Planificacion_Proyecto_ActividadesNegocio().Get_All_Planificacion_Proyecto_Actividades_PA().Where(p => p.Finalizados == false && p.Proyecto_UbicacionesId == id).OrderBy(p => p.Actividad).ToList();
            return data;
        }

        public ReturnData GetDatosActividad(int id)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(id, false);


            //Tengo que calcular cuanto ya esta asignado - Para eso busco las notas de pedidos que ya estan
            double mCantidad_Asignada = 0;
            if (oPPA != null)
            {
                mCantidad_Asignada = new NotaPedido_DetalleNegocio().Get_Orden_PPA(oPPA.Id).Sum(P => P.Avance_Actual);
                oPPA.Cantidad_Libre_Para_Asignar = (float)(oPPA.Cantidad - mCantidad_Asignada);
                oPPA.Cantidad_Asignada = (float)mCantidad_Asignada;
                data.data = oPPA;
            }
            else
            {
                data.isError = true;
                data.data = "No se encontraron actividades";
            }
            return data;
        }

        public ReturnData GetDatosActividad_V2(int IdPPA, int? IdNP=0)
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(IdPPA, false);


            //Tengo que calcular cuanto ya esta asignado - Para eso busco las notas de pedidos que ya estan
            double mCantidad_Asignada = 0;
            if (oPPA != null)
            {
                mCantidad_Asignada = new NotaPedido_DetalleNegocio().Get_Orden_PPA(oPPA.Id).Where(s=>s.IdNotaPedido != IdNP).Sum(P => P.Cantidad);
                oPPA.Cantidad_Libre_Para_Asignar = (float)(oPPA.Cantidad - mCantidad_Asignada);
                oPPA.Cantidad_Asignada = (float)mCantidad_Asignada;
                data.data = oPPA;
            }
            else
            {
                data.isError = true;
                data.data = "No se encontraron actividades";
            }
            return data;
        }

        public ReturnData GetIndices()
        {
            ReturnData data = new ReturnData();
            data.isError = false;
            data.data = new IndicesNegocio().Get_All_Indices();
            return data;
        }

        [HttpPost]
        public ReturnData NotaPedido_DetalleGraba(NotaPedido_Detalle data)
        {
           
            ReturnData d = new ReturnData();
            NotaPedido_Detalle npd = new NotaPedido_Detalle(); 

            //Existe la Nota de Pedido Detalle
            if (data.Id > 0)
            {
                npd = new NotaPedido_DetalleNegocio().Get_One_Orden((int)data.Id);
                npd.Cantidad = Convert.ToDouble(data._Ori_Cantidad.Replace('.', ','));
                npd.Precio_Contradado = Convert.ToDouble(data._Ori_Precio_Contradado.Replace('.', ','));
            }
            else
            {
                data.Precio_Contradado = Convert.ToDouble(data._Ori_Precio_Contradado.Replace('.', ','));
                data.Cantidad = Convert.ToDouble(data._Ori_Cantidad.Replace('.', ','));               
                data.Avance_Actual = 0;
            }

          
            Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(Convert.ToInt32(data.IdPPA));

            if (oPPA != null)
            {
                data.IdPA = oPPA.Planificacion_ActividadesId;
                data.IdUbicacion = oPPA.Proyecto_UbicacionesId;
                data.UnidadMedida = oPPA.UnidadMedida;
            }


            if (data.Id > 0)
            {
                d = ValidoNotaPedido_Detalle(npd, oPPA);
            }
            else
            {
                d = ValidoNotaPedido_Detalle(data, oPPA);
            }
              


            

            if (d.isError == false)
            {
                    int idc;
                    if (data.Id > 0)
                    {
                        idc = new NotaPedido_DetalleNegocio().Update(npd);
                    }
                    else
                    {
                        idc = new NotaPedido_DetalleNegocio().Insert(data);
                    }

                    List<NotaPedido_Detalle> lstNP_Det = CargoNotaPedido_Detalles(Convert.ToInt32(data.IdNotaPedido));
                    d.isError = false;
                    d.data2 = "Se ha Grabado exitosamente la Operación.";
                    d.data = lstNP_Det;
                    d.data1 = idc;
            }
            return d;
        }

        [HttpPost]
        public ReturnData Listar_DetallePedido(NotaPedido_Detalle data)
        {
            ReturnData d = new ReturnData();

            List<NotaPedido_Detalle> lstNP_Det = CargoNotaPedido_Detalles(Convert.ToInt32(data.IdNotaPedido));
                    d.isError = false;
                    d.data = lstNP_Det;
            return d;
        }

        private ReturnData ValidoNotaPedido_Detalle(NotaPedido_Detalle oObj, Planificacion_Proyecto_Actividades oPPA)
        {
            ReturnData oReturn = new ReturnData();
            oReturn.isError = false;
            oReturn.data = "";
            //Validaciones a hacer
            #region 1 - Campos Completos
            if (oObj.Cantidad == 0)
            {
                oReturn.isError = true;
                oReturn.data = "Debe ingresar una cantidad valida";
            }
           
            if (oObj.IdNotaPedido == 0)
            {
                oReturn.isError = true;
                oReturn.data = "Nota de pedido no valida, reintente";
            }
            if (oObj.IdPPA == 0)
            {
                oReturn.isError = true;
                oReturn.data = "Debe seleccionar una actividad";
            }
            if (oObj.Precio_Contradado == 0)
            {
                oReturn.isError = true;
                oReturn.data = "Debe ingresar un precio de contratacion";
            }
            if (!Valida_NotaPedido_Habilitada(Convert.ToInt32(oObj.IdNotaPedido)))
            {
                oReturn.isError = true;
                oReturn.data = "Estado de la Nota de Pedido no valido para esta accion, verifique que la misma este es estado ABIERTA";
            }

            #endregion
            #region 2 - Que no exista la misma actividad en la misma nota de pedido.

            var mCount = new NotaPedido_DetalleNegocio().Get_All_Ordenes().Where(p => p.IdPPA == oObj.IdPPA && p.IdNotaPedido == oObj.IdNotaPedido && p.Id != oObj.Id).Count();
            if (mCount > 0)
            {
                oReturn.isError = true;
                oReturn.data = "Ya existe para esta nota de pedido la actividad " + oObj.sActividad;
            }
            #endregion
            //#region 3 - Verfico que la cantidad que estoy mandando no supere la estimacion original.
            ////Tengo que calcular cuanto ya esta asignado - Para eso busco las notas de pedidos que ya estan
            //double mCantidad_Asignada = new NotaPedido_DetalleNegocio().Get_Orden_PPA(oPPA.Id).Sum(P => P.Cantidad);
            //mCantidad_Asignada = mCantidad_Asignada + oObj.Avance_Actual;
            //double mPPA_Cantidad = Math.Round(oPPA.Cantidad, 2);

            //if (mCantidad_Asignada > mPPA_Cantidad)
            //{
            //    oReturn.isError = true;
            //    oReturn.data = "La cantidad asignada supera a la cantidad planificada, debe modificar la planificacion de esta actividad";
            //}
            //#endregion
            #region 3B - Verfico que la cantidad que estoy mandando no supere la estimacion original.

            //Tengo que calcular cuanto ya esta asignado - Para eso busco las notas de pedidos que ya estan cargadas distinto del actual.
            double mCantidad_AsignadaTotal_Cargada = new NotaPedido_DetalleNegocio().Get_Orden_PPA(oPPA.Id).Where(s=>s.IdNotaPedido != oObj.IdNotaPedido).Sum(P => P.Cantidad);
            mCantidad_AsignadaTotal_Cargada = mCantidad_AsignadaTotal_Cargada + oObj.Cantidad;

            //Esto es lo planificado
            double mPPA_Cantidad_Planificada = Math.Round(oPPA.Cantidad, 2);

            if (mCantidad_AsignadaTotal_Cargada > mPPA_Cantidad_Planificada)
            {
                oReturn.isError = true;
                oReturn.data = "La cantidad asignada supera a la cantidad planificada, debe modificar la planificacion de esta actividad";
            }
            #endregion

            return oReturn;
        }


        private List<NotaPedido_Detalle> CargoNotaPedido_Detalles(int pIdNotaPedido)
        {
            List<NotaPedido_Detalle> lstReturn = new List<NotaPedido_Detalle>();
            lstReturn = new NotaPedido_DetalleNegocio().Get_All_Ordenes().Where(p => p.IdNotaPedido == pIdNotaPedido).ToList();
            double mCantidad_Asignada = 0;
            Planificacion_Proyecto_Actividades oPPA= new Planificacion_Proyecto_Actividades();  
            foreach (var item in lstReturn)
            {
                oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id((int)item.IdPPA, false);
                //1-Busco el nombre de la actividad
                item.sActividad = new Planificacion_ActividadesNegocio().Get_One_Planificacion_Actividades(Convert.ToInt32(item.IdPA)).Nombre;
                //2-Busco el nombre de la Ubicacion
                item.sUbicacion = new Proyecto_UbicacionesNegocio().Get_One_Proyecto_Ubicaciones(Convert.ToInt32(item.IdUbicacion)).Nombre;
                item._Cantidad_Planificada = Convert.ToString(oPPA.Cantidad);
                
               // mCantidad_Asignada = new NotaPedido_DetalleNegocio().Get_Orden_PPA(oPPA.Id).Sum(P => P.Avance_Actual);
               // oPPA.Cantidad_Libre_Para_Asignar = (float)(oPPA.Cantidad - mCantidad_Asignada);
               // oPPA.Cantidad_Asignada = (float)mCantidad_Asignada;
               //item.

            }
            return lstReturn;
        }

        [HttpPost]
        public ReturnData BorraNotaPedido_Detalle(int id)
        {
            ReturnData d = new ReturnData();
            int idc = 0;
            int pIdNotaPedido = 0;
            try
            {
                NotaPedido_Detalle npd = new NotaPedido_DetalleNegocio().Get_One_Orden(id);

                pIdNotaPedido = Convert.ToInt32(npd.IdNotaPedido);
                List<NotaPedido_Detalle> lstNP_Det = CargoNotaPedido_Detalles(Convert.ToInt32(pIdNotaPedido));
                d.data = lstNP_Det;
                d.data1 = idc;
                //Valido el estado de la Nota de Pedido para borrar

                bool mValido = Valida_NotaPedido_Habilitada(pIdNotaPedido);
                if (mValido)
                {
                    idc = new NotaPedido_DetalleNegocio().Delete(npd);
                    d.data2 = "Se han Borrado los datos OK.";
                    d.isError = false;
                }
                else
                {
                    d.data2 = "Estado de la Nota de Pedido no valido para esta accion";
                    d.isError = true;
                }
                
            }
            catch (Exception)
            {
                d.data = null;
                d.data1 = null;
                d.data2 = "Error al Borrar el detalle de la nota de pedido";
                d.isError = true;
            }
            return d;
        }

        private bool Valida_NotaPedido_Habilitada(int pIdNotaPedido)
        {
            NotaPedido oNP = new NotaPedidoNegocio().Get_One_Orden(pIdNotaPedido);
            if (oNP != null)
            {
                if (oNP.Estado == "Abierto")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        #endregion

        #region NotaPedidoCompleta
        [HttpPost]
        public ReturnData ControlExistenciaNotaPedido(ItemNotaPedidoActualizar data)
        {
            ReturnData d = new ReturnData();

            NotaPedido np = new NotaPedido();
            NotaPedido_Detalle npd = new NotaPedido_Detalle();
            using (var db = new iotdbContext())
            {
               // np = db.NotaPedido.get(m => m.IdProyecto == data.IdProyecto && m.IdContratista == data.IdContratista);
                
                List<NotaPedido> lista = new NotaPedidoNegocio().Get_All_Ordenes().Where(m => m.IdProyecto == data.IdProyecto && m.IdContratista == data.IdContratista)
                                                            .ToList();
                if (lista != null)
                {
                    foreach (var item in lista)
                    { 
                         npd = db.NotaPedido_Detalle.FirstOrDefault(m => m.IdPA == data.IdPA && m.IdNotaPedido == item.Id);

                        if (npd != null)
                        {  
                            d.isError = true;
                            d.data2 = "Ya existe una nota de Pedido Creada para esa Actividad y para el Contratista seleccionado.";
                            break;
                        }
                    
                    }
    
                }
                else
                {
                    d.isError = false;
                    d.data2 = "";
                }
            }
            return d;
        }


        [HttpPost]
        public ReturnData NotaPedidoGrabaCompleta(ItemNotaPedidoActualizar data)
        {
        
            ReturnData d = new ReturnData();
            NotaPedido oNP = new NotaPedido();
            NotaPedido_Detalle oNP_det = new NotaPedido_Detalle();


            oNP.IdProyecto = data.IdProyecto;
            oNP.IdContratista = data.IdContratista;
            oNP.Fecha_Creacion = data.Fecha_Creacion;
            oNP.Comentario = data.Comentario;
            oNP.IdIndiceAjuste = data.IdIndiceAjuste;
            
             
            oNP.ComentarioPoliza= data.ComentarioPoliza;  
            oNP.PresentaPoliza = data.PresentaPoliza;   
            //CREACION NOTA PEDIDO
            d = ValidoNotaPedido(oNP);
            d.isError = false;

            if (d.isError == false)
            {
                Usuarios oUsuario = getUsuarioActual();
                oNP.IdUsuario_Creacion = oUsuario.Id;
                oNP.Estado = "Abierto";

                if (oNP != null)
                {
                    int iDNotaPedido;
                    
                    List<NotaPedido> lista = new NotaPedidoNegocio().Get_All_Ordenes().Where(p => p.IdProyecto == oNP.IdProyecto && p.IdContratista == oNP.IdContratista)
                                                            .OrderByDescending(p => p.Secuencial).ToList();
                    if (lista.Count > 0) { oNP.Secuencial = 1 + lista.FirstOrDefault().Secuencial; }
                    if (oNP.Secuencial == null || oNP.Secuencial == 0) { oNP.Secuencial = 1; }

                    String IdProyecto, IdContratista, SecuencialString;
                    switch (oNP.IdProyecto.ToString().Length)
                    {
                        case 1:
                            IdProyecto = "000" + oNP.IdProyecto.ToString();
                            break;
                        case 2:
                            IdProyecto = "00" + oNP.IdProyecto.ToString();
                            break;
                        case 3:
                            IdProyecto = "0" + oNP.IdProyecto.ToString();
                            break;
                        default:
                            IdProyecto = "" + oNP.IdProyecto.ToString();
                            break;
                    }
                    switch (oNP.IdContratista.ToString().Length)
                    {
                        case 1:
                            IdContratista = "000" + oNP.IdContratista.ToString();
                            break;
                        case 2:
                            IdContratista = "00" + oNP.IdContratista.ToString();
                            break;
                        case 3:
                            IdContratista = "0" + oNP.IdContratista.ToString();
                            break;
                        default:
                            IdContratista = "" + oNP.IdContratista.ToString();
                            break;
                    }
                    switch (oNP.Secuencial.ToString().Length)
                    {
                        case 1:
                            SecuencialString = "000" + oNP.Secuencial.ToString();
                            break;
                        case 2:
                            SecuencialString = "00" + oNP.Secuencial.ToString();
                            break;
                        case 3:
                            SecuencialString = "0" + oNP.Secuencial.ToString();
                            break;
                        default:
                            SecuencialString = "" + oNP.Secuencial.ToString();
                            break;
                    }

                    oNP.NroNP = IdProyecto + '-' + IdContratista + '-' + SecuencialString;

                    if (oNP.NroNP.Length > 0)
                    {
                        iDNotaPedido = new NotaPedidoNegocio().Insert(oNP);
                    }
                    else
                    {
                        iDNotaPedido = 0;
                    }
            

                    /*
                     
                     iDNotaPedido --> 
                    
                    idPA
                     
                     */

                    if (iDNotaPedido > 0)
                    {
                        d.isError = false;

                        //INSERTAMOS LA NOTA PEDIDO DETALLE
                        oNP_det.Precio_Contradado = Convert.ToDouble(data._Ori_Precio_Contradado.Replace('.', ','));
                        oNP_det.Cantidad = Convert.ToDouble(data._Ori_Cantidad.Replace('.', ','));
             
                        oNP_det.IdNotaPedido = iDNotaPedido;

                        oNP_det.IdPPA = data.IdPPA;//combo actividades
                        oNP_det.IdPA = data.IdPA;
                        Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_ActividadesNegocio().Get_X_Id(Convert.ToInt32(oNP_det.IdPPA));

                        if (oPPA != null)
                        {
                            oNP_det.IdPA = oPPA.Planificacion_ActividadesId;
                            oNP_det.IdUbicacion = oPPA.Proyecto_UbicacionesId;
                            oNP_det.UnidadMedida = oPPA.UnidadMedida;
                        }


                        d = ValidoNotaPedido_Detalle(oNP_det, oPPA);
                        
                        if (d.isError == false)
                        {
                            if (oNP_det != null)
                            {
                                int iDNotaPedidoDetalle;

                                iDNotaPedidoDetalle = new NotaPedido_DetalleNegocio().Insert(oNP_det);
                               

                               if (iDNotaPedidoDetalle > 0)
                                {
                                    d.isError = false;
                                    d.data2 = "Se ha Grabado exitosamente la Nota de Pedido y un Detalle de Nota de Pedido.";
                                }
                                else
                                {
                                    d.data2 = "Error al Grabar el Detalle de la Nota de Pedido.";
                                }

                            }
                            else
                            {
                                d.isError = true;
                                d.data2 = "No se encontro información para grabar el Detalle de la nota de Pedido.";
                            }
                        }

                    }
                    else
                    {
                        d.isError = true;
                        d.data = "Error al Grabar en la base la Nota de Pedido.";
                    }
                }
            }
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

        public Usuarios getUsuarioActual()
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

        #region Pantalla de Planificacion - Carga de grilla con el detalle de las asignaciones de una Actividad
        [HttpPost]
        public ReturnData NotaPedido_PPA(int id)
        {
            ReturnData d = new ReturnData();
            try
            {
                List<NotaPedido_Detalle> lstNPD = new NotaPedido_DetalleNegocio().Get_Orden_PPA(id);
                List<Indices> Indices = new IndicesNegocio().Get_All_Indices();
                foreach (var item in lstNPD)
                {
                    NotaPedido oNP = new NotaPedidoNegocio().Get_One_Orden(Convert.ToInt32(item.IdNotaPedido));
                   
                    item.sFechaNotaPedido = Convert.ToString(((DateTime)oNP.Fecha_Creacion).ToString("0:dd/MM/yyyy")).Remove(0, 2);
                    item.sNotaPedido = oNP.NroNP;
                    item.sEstadoPedido = oNP.Estado;
                    item.sComentarioPedido = oNP.Comentario;
                    Contratistas oContratista = new ContratistasNegocio().Get_One_Contratistas(Convert.ToInt32(oNP.IdContratista));
                    item.sContratista = oContratista.Nombre;
                    item.sUsuarioCreo = new UsuariosNegocio().Get_Usuario(Convert.ToInt32(oNP.IdUsuario_Creacion)).NombreYApellido;
                    
                    item.sIndice = Indices.FirstOrDefault(a=> a.Id == oNP.IdIndiceAjuste).Nombre;
                    if ((bool)oNP.PresentaPoliza) { item.PresentaPoliza = "Tiene Poliza"; item.ComentarioPoliza = oNP.ComentarioPoliza; }
                    else
                    {
                        item.PresentaPoliza = "No Tiene Poliza";
                        item.ComentarioPoliza = "";
                    }
                    
                }
                d.data = lstNPD;
                d.isError = false;
            }
            catch (Exception)
            {
                d.data = null;
                d.data1 = "Error al buscar las Asignacion de esta Actividad";
                d.isError = true;
            }
            return d;
        }

        #endregion
    }
}
