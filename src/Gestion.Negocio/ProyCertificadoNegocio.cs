using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gestion.Negocio
{
    public class ProyCertificadoNegocio
    {
        #region ABM's

        public int Insert(ProyCertificado data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(ProyCertificado data)
        {
            return this.GrabarCambios(data, EntityState.Modified);
        }

        public int Delete(ProyCertificado data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        private int GrabarCambios(ProyCertificado data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.ProyCertificados.Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception)
            {
                id = 0;
            }
            return id;
        }

        #endregion

        #region Select's

        public List<ProyCertificado> Get_All()
        {
            try
            {
                using var db = new iotdbContext();
                return db.ProyCertificados.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ProyCertificadoNegocio: Get_All(): exception.", ex);
            }
        }

        /// <summary>
        /// Método que devuelve una lista de ProyCertificados según un periodo de fechas 
        /// (que contenga a los periodos de los PoryCertificados)
        /// </summary>
        /// <param name="fecDesde">Fecha inicio del perido</param>
        /// <param name="fecHasta">Fecha fin del periodo</param>
        /// <param name="conInclude">True: si necesita los objetos relaciones</param>
        /// <returns>List de ProyCertificado</returns>
        public List<ProyCertificado> Get_x_Periodo(DateTime fecDesde, DateTime fecHasta, bool conInclude)
        {
            List<ProyCertificado> lProyCertificados = new List<ProyCertificado>();
            try
            {
                using (var db = new iotdbContext())
                {
                    lProyCertificados = this.Get_x_ProyId_Periodo(
                         0, DateTime.Now.AddMonths(-6), DateTime.Now, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: exception.", ex);
            }
            return lProyCertificados;
        }

        /// <summary>
        /// Método que devuelve una lista de ProyCertificados según un periodo de fechas
        /// (que contenga a los periodos de los PoryCertificados)
        /// </summary>
        /// <param name="ProyectoId">Id del Proyecto Filtro (0 = todos)</param>
        /// <param name="fecDesde">Fecha inicio del perido</param>
        /// <param name="fecHasta">Fecha fin del periodo</param>
        /// <param name="conInclude">True: si necesita los objetos relaciones</param>
        /// <returns>List de ProyCertificado</returns>
        public List<ProyCertificado> Get_x_ProyId_Periodo(
            int ProyectoId, DateTime fecDesde, DateTime fecHasta, bool conInclude)
        {
            List<ProyCertificado> lProyCertificados = new List<ProyCertificado>();
            try
            {
                using (var db = new iotdbContext())
                {
                    if (conInclude)
                    {
                        lProyCertificados = db.ProyCertificados
                            .Include(pc => pc.Proyecto)
                            .Where(pc => fecDesde <= pc.FecDesde && pc.FecHasta <= fecHasta &&
                                (ProyectoId == 0 || pc.ProyectoId == ProyectoId))
                            .OrderByDescending(pc => pc.FecCreacion)
                            .ThenByDescending(pc => pc.Id)
                            .ToList();
                    }
                    else
                    {
                        lProyCertificados = db.ProyCertificados
                            .Where(pc => fecDesde <= pc.FecDesde && pc.FecHasta <= fecHasta &&
                                (ProyectoId == 0 || pc.ProyectoId == ProyectoId))
                            .OrderByDescending(pc => pc.FecCreacion)
                            .ThenByDescending(pc => pc.Id)
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Get_x_Periodo_ContrId_ProyId(int, DateTime, DateTime, bool): exception.", ex);
            }
            return lProyCertificados;
        }

        public List<ProyCertificado> Get_x_ProyectoId(int ProyectoId)
        {
            try
            {
                using (var db = new iotdbContext())
                {
                    return db.ProyCertificados.Where(pc=> 
                        ProyectoId == 0 || pc.ProyectoId == ProyectoId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ProyCertificadoNegocio: Get_x_ProyectoId(int): exception.", ex);
            }
        }

        public ProyCertificado Get_x_Id(int ProyCertificadoId, bool conIncludes)
        {
            ProyCertificado oProyCertificado = new ProyCertificado();
            try
            {
                using (var db = new iotdbContext())
                {
                    if (conIncludes)
                    {
                        oProyCertificado = db.ProyCertificados
                            .Include(pc => pc.Proyecto)
                            .Include(pc => pc.lProyCert_PDActContrs)
                                .ThenInclude(pcpdac => pcpdac.PDActContr)
                                .ThenInclude(pdac => pdac.ParteDiario_Actividades)
                            .Include(pc => pc.lProyCert_PDActContrs)
                                .ThenInclude(pcpdac => pcpdac.PDActContr)
                            .Include(pc => pc.lProyCert_PDActContrs)
                                .ThenInclude(pcpdac => pcpdac.Contratista)
                            .Include(pc => pc.lProyCert_PDActContrs)
                                .ThenInclude(pcpdac => pcpdac.PDActContr)
                                .ThenInclude(pdac => pdac.Contratistas)
                            .Include(pc => pc.lProyCert_PlanProyActs)
                                .ThenInclude(pcpdac => pcpdac.PlanProyAct)
                                .ThenInclude(ppa => ppa.Planificacion_Actividades)
                            .Include(pc => pc.lProyCert_PlanProyActs)
                                .ThenInclude(pcpdac => pcpdac.PlanProyAct)
                                .ThenInclude(ppa => ppa.Planificacion_Actividades)
                                .ThenInclude(pa => pa.PartidaPresupuestaria)
                            .Include(pc => pc.lProyCert_PlanProyActs)
                                .ThenInclude(pcpdac => pcpdac.PlanProyAct)
                                .ThenInclude(ppa => ppa.Proyecto_Ubicaciones)
                            .Include(pc => pc.lProyCert_Contratistas)
                                .ThenInclude(pcc => pcc.Contratista)
                            .Include(pc => pc.lProyCert_Empleados)
                                .ThenInclude(pce => pce.Contratista)
                            .FirstOrDefault(pc => pc.Id == ProyCertificadoId);
                        List<ProyCert_PDActContr> lPCPDACs = db.ProyCert_PDActContrs
                            .Include(pcpdac => pcpdac.Contratista)
                            .Where(pcpdac => pcpdac.ProyCertificadoId == ProyCertificadoId &&
                                pcpdac.PDActContrId == 0)
                            .ToList();
                        if(oProyCertificado.ProyCertAnteriorId > 0)
                        {
                            oProyCertificado.ProyCertAnterior = 
                                this.Get_x_Id(oProyCertificado.ProyCertAnteriorId, true);
                        }
                    }
                    else
                        oProyCertificado = db.ProyCertificados
                            .FirstOrDefault(cc => cc.Id == ProyCertificadoId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ex.", ex);
            }
            return oProyCertificado;
        }

        public ProyCertificado Get_x_Id_SQL(int ProyCertificadoId)
        {
            try
            {
                ProyCertificado oProyCertificado =
                    this.Get_x_Id(ProyCertificadoId, false);
                oProyCertificado.Proyecto = new ProyectoNegocio()
                    .Get_One_Proyecto(oProyCertificado.ProyectoId);
                oProyCertificado.lProyCert_PlanProyActs =
                    this.Get_PCPPAs_x_ProyCertificadoId_SQL(ProyCertificadoId);
                oProyCertificado.lProyCert_PDActContrs =
                    this.Get_PCPDACs_x_ProyCertificadoId_SQL(ProyCertificadoId);
                oProyCertificado.lProyCert_PDActContr_Adicionales =
                    this.Get_PCPDACsAdicionales_x_ProyCertificadoId_SQL(ProyCertificadoId);
                oProyCertificado.lProyCert_Contratistas =
                    this.Get_PCCs_x_ProyCertificadoId_SQL(ProyCertificadoId);
                oProyCertificado.lProyCert_Empleados =
                    this.Get_PCEs_x_ProyCertificadoId_SQL(ProyCertificadoId);
                if (oProyCertificado.ProyCertAnteriorId > 0)
                {
                    oProyCertificado.ProyCertAnterior =
                        this.Get_x_Id_SQL(oProyCertificado.ProyCertAnteriorId);
                }
                return oProyCertificado;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyCertificadoNegocio: Get_x_Id_SQL(int): exception.", ex);
            }
        }

        private List<ProyCert_PlanProyAct> Get_PCPPAs_x_ProyCertificadoId_SQL(int ProyCertificadoId)
        {
            try
            {
                List<ProyCert_PlanProyAct> lPCPPA = new List<ProyCert_PlanProyAct>();
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    string query = "select * from proycert_planproyacts pcppa " +
                        "inner join planificacion_proyecto_actividades ppa on ppa.id = pcppa.PlanProyActId " +
                        "inner join planificacion_actividades pa on pa.id = ppa.Planificacion_ActividadesId " +
                        "inner join partidaspresupuestarias pp on pp.id=pa.PartidaPresupuestariaId " +
                        "where pcppa.ProyCertificadoId = " + ProyCertificadoId + ";";
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProyCert_PlanProyAct oPCPPA = new ProyCert_PlanProyAct();
                        oPCPPA.Id = reader.GetInt32(0);
                        oPCPPA.ProyCertificadoId = reader.GetInt32(1);
                        oPCPPA.PlanProyActId = reader.GetInt32(2);
                        oPCPPA.MontoPlanificado = reader.GetFloat(3);
                        oPCPPA.CantidadPlanificada = reader.GetFloat(4);
                        oPCPPA.UnidadMedida = reader.GetString(5);
                        oPCPPA.Actividad = reader.GetString(6);
                        oPCPPA.Ubicacion = reader.GetString(7);
                        oPCPPA.FecCreacion = reader.GetDateTime(8);
                        oPCPPA.UsuarioCreoId = reader.GetInt32(9);
                        oPCPPA.ActividadDescripcion = reader.GetString(10);
                        oPCPPA.CodigoPartidaPresupuestaria = reader.GetString(11);
                        oPCPPA.ExcedenteAutorizado = reader.GetBoolean(12);
                        oPCPPA.CantidadAutorizadaExcedente = reader.GetFloat(13);
                        oPCPPA.PartidaPresupuestariaId = reader.GetInt32(14);
                        oPCPPA.NotificacionId = reader.GetInt32(15);
                        oPCPPA.DescripcionPartidaPresupuestaria = reader.GetString(16);
                        oPCPPA.Visada = reader.GetBoolean(17);

                        Planificacion_Proyecto_Actividades oPPA = new Planificacion_Proyecto_Actividades();
                        oPPA.Id = reader.GetInt32(18);
                        oPPA.Proyecto_UbicacionesId = reader.GetInt32(19);
                        oPPA.Planificacion_ActividadesId = reader.GetInt32(20);
                        oPPA.Cantidad = reader.GetFloat(34);
                        oPPA.UnidadMedida = reader.GetString(35);
                        oPPA.Monto = reader.GetFloat(39);
                        oPPA.EsAdicional = reader.GetBoolean(40);

                        Planificacion_Actividades oPA = new Planificacion_Actividades();
                        oPA.Id = reader.GetInt32(43);
                        oPA.PartidaPresupuestariaId = reader.GetInt32(49);

                        PartidaPresupuestaria oPP = new PartidaPresupuestaria();
                        oPP.Id = reader.GetInt32(50);
                        oPP.Codigo = reader.GetString(51);
                        oPP.Descripcion = reader.GetString(52);

                        oPA.PartidaPresupuestaria = oPP;
                        oPPA.Planificacion_Actividades = oPA;
                        oPCPPA.PlanProyAct = oPPA;

                        lPCPPA.Add(oPCPPA);
                    }
                }
                return lPCPPA;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCertificadoNegocio: Get_PCPPAs_x_ProyCertificadoId_SQL(int): exception.", ex);
            }
        }

        private List<ProyCert_PDActContr> Get_PCPDACs_x_ProyCertificadoId_SQL(int ProyCertificadoId)
        {
            try
            {
                List<ProyCert_PDActContr> lPCPDACs = new List<ProyCert_PDActContr>();
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    string query = "select * from proycert_pdactcontrs pcpdac " +
                        "inner join contratistas c on c.id = pcpdac.ContratistaId " +
                        "where pcpdac.ProyCertificadoId = " + ProyCertificadoId + ";";
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProyCert_PDActContr oPCPDAC = new ProyCert_PDActContr();
                        oPCPDAC.Id = reader.GetInt32(0);
                        oPCPDAC.ProyCertificadoId = reader.GetInt32(1);
                        oPCPDAC.PDActContrId = reader.GetInt32(2);
                        oPCPDAC.Cantidad = reader.GetFloat(3);
                        oPCPDAC.FecCreacion = reader.GetDateTime(4);
                        oPCPDAC.UsuarioCreoId = reader.GetInt32(5);
                        oPCPDAC.PlanProyActId = reader.GetInt32(6);
                        oPCPDAC.ContratistaId = reader.GetInt32(7);
                        oPCPDAC.Estado = reader.GetString(8);
                        oPCPDAC.ImporteAcumAnterior = reader.GetFloat(9);
                        oPCPDAC.MontosAjustables = reader.GetBoolean(10);

                        oPCPDAC.Contratista = new Contratistas();
                        oPCPDAC.Contratista.Id = reader.GetInt32(11);
                        oPCPDAC.Contratista.Nombre = reader.GetString(12);

                        lPCPDACs.Add(oPCPDAC);
                    }
                }
                List<int> lIdsPDACs = lPCPDACs
                    .Where(pcpdac => pcpdac.PDActContrId > 0)
                    .Select(pcpdac => pcpdac.PDActContrId)
                    .ToList();
                List<ParteDiario_Actividades_Contratista> lPDACs = new ParteDiario_Actividades_ContratistaNegocio()
                    .Get_x_lIDs(lIdsPDACs);
                lPCPDACs.ForEach(pcpdac =>
                {
                    pcpdac.PDActContr = lPDACs.Find(pdac => pcpdac.PDActContrId == pdac.Id);
                });
                return lPCPDACs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCertificadoNegocio: Get_PCPDACs_x_ProyCertificadoId_SQL(int): exception.", ex);
            }
        }

        private List<ProyCert_PDActContr_Adicional> Get_PCPDACsAdicionales_x_ProyCertificadoId_SQL(int ProyCertificadoId)
        {
            try
            {
                List<ProyCert_PDActContr_Adicional> lPCPDAC_Adicionales = new List<ProyCert_PDActContr_Adicional>();
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    string query = "select * from proycert_pdactcontr_adicionales pcpdaca " +
                        "inner join contratistas c on c.id=pcpdaca.ContratistaId " +
                        "where ProyCertificadoId = " + ProyCertificadoId + ";";
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProyCert_PDActContr_Adicional oPCPDAC_A = new ProyCert_PDActContr_Adicional();
                        oPCPDAC_A.Id = reader.GetInt32(0);
                        oPCPDAC_A.ProyCertificadoId = reader.GetInt32(1);
                        oPCPDAC_A.ContratistaId = reader.GetInt32(2);
                        oPCPDAC_A.Cantidad = reader.GetFloat(3);
                        oPCPDAC_A.Monto = reader.GetFloat(4);
                        oPCPDAC_A.Actividad = reader.GetString(5);
                        oPCPDAC_A.Ubicacion = reader.GetString(6);
                        oPCPDAC_A.UnidadMedida = reader.GetString(7);
                        oPCPDAC_A.CodigoPartidaPresupuestaria = reader.GetString(8);
                        oPCPDAC_A.FecCreacion = reader.GetDateTime(9);
                        oPCPDAC_A.UsuarioCreoId = reader.GetInt32(10);
                        oPCPDAC_A.MontosAjustables = reader.GetBoolean(11);
                        oPCPDAC_A.ActividadDescripcion = reader.GetString(12);

                        oPCPDAC_A.Contratista = new Contratistas();
                        oPCPDAC_A.Contratista.Id = reader.GetInt32(13);
                        oPCPDAC_A.Contratista.Nombre = reader.GetString(14);

                        lPCPDAC_Adicionales.Add(oPCPDAC_A);
                    }
                }
                return lPCPDAC_Adicionales;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCertificadoNegocio: Get_PCPDACsAdicionales_x_ProyCertificadoId_SQL(int): exception.", ex);
            }
        }

        private List<ProyCert_Contratista> Get_PCCs_x_ProyCertificadoId_SQL(int ProyCertificadoId)
        {
            try
            {
                List<ProyCert_Contratista> lPCContrs = new List<ProyCert_Contratista>();
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    string query = "select * from proycert_contratistas pcc " +
                        "inner join contratistas c on c.id = pcc.ContratistaId " +
                        "where pcc.ProyCertificadoId = " + ProyCertificadoId + "; ";
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProyCert_Contratista oPCC = new ProyCert_Contratista();
                        oPCC.Id = reader.GetInt32(0);
                        oPCC.ProyCertificadoId = reader.GetInt32(1);
                        oPCC.ContratistaId = reader.GetInt32(2);
                        oPCC.EmiteFactura = reader.GetBoolean(3);
                        oPCC.PorcentajeIVA = reader.GetFloat(4);
                        oPCC.IndiceBase = reader.GetFloat(5);
                        oPCC.IndiceActual = reader.GetFloat(6);
                        oPCC.PorcentajeActualizacion = reader.GetFloat(7);
                        oPCC.Adelanto = reader.GetFloat(8);
                        oPCC.PorcentajeDescuentoAnticipo = reader.GetFloat(9);
                        oPCC.PorcentajeFondoReparo = reader.GetFloat(10);
                        oPCC.NumeroCertificado = reader.GetInt32(11);

                        oPCC.Contratista = new Contratistas();
                        oPCC.Contratista.Id = reader.GetInt32(12);
                        oPCC.Contratista.Nombre = reader.GetString(13);

                        lPCContrs.Add(oPCC);
                    }
                }
                return lPCContrs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCertificadoNegocio: Get_PCCs_x_ProyCertificadoId_SQL(int): exception.", ex);
            }
        }

        private List<ProyCert_Empleado> Get_PCEs_x_ProyCertificadoId_SQL(int ProyCertificadoId)
        {
            try
            {
                List<ProyCert_Empleado> lPCEmpleados = new List<ProyCert_Empleado>();
                using (var db = new iotdbContext())
                {
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    var command = conn.CreateCommand();

                    string query = "select * from proycert_empleados pce " +
                        "inner join contratistas c on c.id = pce.ContratistaId " + 
                        "where pce.ProyCertificadoId = " + ProyCertificadoId + ";";
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProyCert_Empleado oPCE = new ProyCert_Empleado();
                        oPCE.Id = reader.GetInt32(0);
                        oPCE.ProyCertificadoId = reader.GetInt32(1);
                        oPCE.ApellidoNombre = reader.GetString(2);
                        oPCE.Monto = reader.GetFloat(3);
                        oPCE.ContratistaId = reader.GetInt32(4);

                        oPCE.Contratista = new Contratistas();
                        oPCE.Contratista.Id = reader.GetInt32(5);
                        oPCE.Contratista.Nombre = reader.GetString(6);

                        lPCEmpleados.Add(oPCE);
                    }
                }
                return lPCEmpleados;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ProyCertificadoNegocio: Get_PCCs_x_ProyCertificadoId_SQL(int): exception.", ex);
            }
        }

        public ProyCertificado Get_ProyCertificadoUltimo_x_ProyectoId(int ProyectoId, bool conIncludes)
        {
            try
            {
                List<ProyCertificado> lProyCertificados = new List<ProyCertificado>();
                using (var db = new iotdbContext())
                {
                    lProyCertificados = db.ProyCertificados
                        .Where(cc => ProyectoId == 0 || cc.ProyectoId == ProyectoId)
                        .OrderByDescending(cc => cc.FecHasta)
                        .ToList();
                }
                //lProyCertificados = OrdenarLista(lProyCertificados, new List<ProyCertificado>());
                ProyCertificado oPC = null;
                if (lProyCertificados.Count > 0)
                {
                    oPC = lProyCertificados.First();
                    if (conIncludes)
                    {
                        oPC = this.Get_x_Id_SQL(oPC.Id);
                    }
                }
                return oPC;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error: ProyCertificadoNegocio: Get_UltimoCertificado_x_ProyectoId(int): exception.", ex);
            }
        }

        private List<ProyCertificado> OrdenarLista(
           List<ProyCertificado> lPCs, List<ProyCertificado> lPCs_Ordenada)
        {
            if (lPCs_Ordenada.Count == 0)
            {
                ProyCertificado oPC = lPCs.Find(cc => cc.ProyCertAnteriorId == 0);
                if (oPC != null)
                {
                    lPCs_Ordenada.Add(oPC);
                    return OrdenarLista(lPCs, lPCs_Ordenada);
                }
                else return new List<ProyCertificado>();
            }
            else
            {
                ProyCertificado oPC_Anterior = lPCs_Ordenada.Last();
                ProyCertificado oPC = lPCs
                    .Find(cc => cc.ProyCertAnteriorId == oPC_Anterior.Id);
                if (oPC != null)
                {
                    lPCs_Ordenada.Add(oPC);
                    return OrdenarLista(lPCs, lPCs_Ordenada);
                }
                else return lPCs_Ordenada;
            }
        }

        public List<ProyCertificado> Get_x_lIdsProyectos(List<int> lIdsProyectos)
        {
            try
            {
                List<ProyCertificado> lProyCertificados = new List<ProyCertificado>();
                using (var db = new iotdbContext())
                {
                    lProyCertificados = db.ProyCertificados
                        .Where(c => lIdsProyectos.Contains(c.ProyectoId) == true)
                        .ToList();
                }
                return lProyCertificados;
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ProyCertificadoNegocio: Get_x_lIdsProyectos(List<int>): exception.", ex);
            }
        } 

        #endregion
    }
}
