using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gestion.EF.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion.Negocio
{
     public class PlanProyAct_DependenciaNegocio
    {

        #region ABM's

        private int GrabarCambios(PlanProyAct_Dependencia data, EntityState estado)
        {
            int id = 0;
            try
            {
                using (var db = new iotdbContext())
                {
                    db.PlanProyAct_Dependencias .Attach(data);
                    db.Entry(data).State = estado;
                    db.SaveChanges();
                    id = (int)data.Id;
                }
            }
            catch (Exception e)
            {
                id = 0;
            }
            return id;
        }

        public int Insert(PlanProyAct_Dependencia data)
        {
            return this.GrabarCambios(data, EntityState.Added);
        }

        public int Update(PlanProyAct_Dependencia data)
        {
            try
            {
                return this.GrabarCambios(data, EntityState.Modified);
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        public int Delete(PlanProyAct_Dependencia  data)
        {
            return this.GrabarCambios(data, EntityState.Deleted);
        }

        #endregion

        #region Select's

        public List<PlanProyAct_Dependencia> Get_All()
        {
            List<PlanProyAct_Dependencia> Lista = new List<PlanProyAct_Dependencia>();
            using (var db = new iotdbContext())
            {
                Lista = db.PlanProyAct_Dependencias.ToList();
            }
            return Lista;
        }

        public PlanProyAct_Dependencia Get_x_Id(int Id)
        {
            PlanProyAct_Dependencia oPPADependencia = new PlanProyAct_Dependencia();
            using (var db = new iotdbContext())
            {
                oPPADependencia = db.PlanProyAct_Dependencias
                    .FirstOrDefault(ppad => ppad.Id == Id);
            }
            return oPPADependencia;
        }

        public PlanProyAct_Dependencia  Get_x_Ids (int PPAPadreId, int PPAHijaId)
        {
            PlanProyAct_Dependencia  oPPADependencia = new PlanProyAct_Dependencia ();
            using (var db = new iotdbContext())
            {
                oPPADependencia = db.PlanProyAct_Dependencias
                    .FirstOrDefault(ppad => ppad.PPAPadreId == PPAPadreId && ppad.PPAHijaId == PPAHijaId);
            }
            return oPPADependencia;
        }

        public List<PlanProyAct_Dependencia> Get_x_IdPPA(int IdPPA)
        {
            List<PlanProyAct_Dependencia> lPPA_Dependencias = new List<PlanProyAct_Dependencia>();
            using (var db = new iotdbContext())
            {
                lPPA_Dependencias = db.PlanProyAct_Dependencias
                    .Include(ppad => ppad.PPAPadre)
                    .Include(ppad => ppad.PPAHija)
                    .Where(ppad => ppad.PPAHijaId == IdPPA || ppad.PPAPadreId == IdPPA)
                    .ToList();
            }
            return lPPA_Dependencias;
        }

        public List<PlanProyAct_Dependencia> Get_x_IdPadre(int IdPPA)
        {
            List<PlanProyAct_Dependencia> lPPA_Dependencias = new List<PlanProyAct_Dependencia>();
            using (var db = new iotdbContext())
            {
                lPPA_Dependencias = db.PlanProyAct_Dependencias
                    .Include(ppad => ppad.PPAPadre)
                    .Include(ppad => ppad.PPAHija)
                    .Where(ppad => ppad.PPAPadreId == IdPPA)
                    .ToList();
            }
            return lPPA_Dependencias;
        }

        public List<PlanProyAct_Dependencia> Get_By_lIdsPPA_Hijas(List<int> lIdsPPAs_Hijas, bool conIncludes)
        {
            List<PlanProyAct_Dependencia> lPPA_Dependencias = new List<PlanProyAct_Dependencia>();
            using (var db = new iotdbContext())
            {
                if (conIncludes)
                {
                    lPPA_Dependencias = db.PlanProyAct_Dependencias
                        .Include(pp => pp.PPAPadre)
                        .Include(pp => pp.PPAPadre.Proyecto_Ubicaciones)
                        .Include(pp => pp.PPAPadre.Planificacion_Actividades)
                        .Include(pp => pp.PPAHija)
                        .Include(pp => pp.PPAHija.Proyecto_Ubicaciones)
                        .Include(pp => pp.PPAHija.Planificacion_Actividades)
                        .Where(ppad => lIdsPPAs_Hijas.Contains(ppad.PPAHijaId) == true)
                        .ToList();
                }
                else
                {
                    lPPA_Dependencias = db.PlanProyAct_Dependencias
                           .Where(ppad => lIdsPPAs_Hijas.Contains(ppad.PPAHijaId) == true)
                           .ToList();
                }
            }
            return lPPA_Dependencias;
        }

        public bool ConsultarTienePadre(int idPPA)
        {
            using (var db = new iotdbContext())
            {
                return db.PlanProyAct_Dependencias
                    .Where(ppad => ppad.PPAHijaId == idPPA)
                    .Count() > 0;
            }
        }

        #endregion

    }
}
