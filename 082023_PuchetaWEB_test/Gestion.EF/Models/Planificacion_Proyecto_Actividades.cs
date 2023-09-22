using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Gestion.EF.Models
{
    public class Planificacion_Proyecto_Actividades
    {
        public int Id { set; get; }
        public int IdPadre { set; get; }

        [DisplayName("Ubicaciones")]
        public int Proyecto_UbicacionesId { set; get; }
        public Proyecto_Ubicaciones Proyecto_Ubicaciones { set; get; }

        [DisplayName("Actividades")]
        public int Planificacion_ActividadesId { set; get; }
        public Planificacion_Actividades Planificacion_Actividades { set; get; }

        [DisplayName("Rubros")]
        public int Planificacion_Actividades_RubroId { set; get; }
        public Planificacion_Actividades_Rubro Planificacion_Actividades_Rubro { set; get; }

        [DisplayName("Fecha de Creación")]
        public DateTime Fecha_Creacion { get; set; }

        [DisplayName("Fecha de última modificación")]
        public DateTime Fecha_Ultima_Modificacion { get; set; }

        [DisplayName("Inicio Estimado")]
        public DateTime Fecha_Real_Incio { get; set; }
        [DisplayName("Fin Estimado")]
        public DateTime Fecha_Real_Fin { get; set; }

        [DisplayName("Responsable Ejecución")]
        public int UsuariosId { get; set; }
        public Usuarios Usuarios { get; set; }

        [DisplayName("Contratistas")]
        public int ContratistasId { get; set; }
        public Contratistas Contratistas { get; set; }

        [DisplayName("Comentario")]
        public String Detalle { set; get; }

        [DisplayName("Incidencia")]
        public float Incidencia { set; get; }

        [DisplayName("Es Retrabajo?")]
        public bool ReTrabajo { get; set; }

        [DisplayName("Es Adicional?")]
        public bool EsAdicional { get; set; }

        [DisplayName("Es Solapable?")]
        public bool Solapable { get; set; }

        /// <summary>
        /// Fecha_Est_Incio rango estimado para el Parte Diario
        /// </summary>
        [DisplayName("Fecha Estimada Inicio")]
        public DateTime Fecha_Est_Incio { get; set; }
        public DateTime FecEstInicio_Original { get; set; }
        /// <summary>
        /// Fecha_Est_Fin rango estimado para el Parte Diario
        /// </summary>
        [DisplayName("Fecha Estimada Fin")]
        public DateTime Fecha_Est_Fin { get; set; }
        public DateTime FecEstFin_Original { get; set; }
        /// <summary>
        /// Avance = carga avance desde Parte diario Activides
        /// </summary>
        public float Avance { set; get; }

        /// <summary>
        /// Cantidad Total de trabajo / relacionada con Parte diario actividades.
        /// </summary>
        [DisplayName("Cantidad")]
        public float Cantidad { set; get; }
        /// <summary>
        /// Monto a cobrar unidad de Medida
        /// </summary>
        [DisplayName("Monto")]
        public float Monto { set; get; }

        [DisplayName("Unidad de Medida")]
        [MaxLength(50)]
        public String UnidadMedida { set; get; }

        public bool Finalizados { set; get; }

        public List<Planificacion_Proyecto_Actividades_Log> Planificacion_Proyecto_Actividades_Log { get; set; }
        [NotMapped]
        public List<PlanProyAct_Dependencia> PlanProyAct_Dependencias { get; set; }


        [NotMapped]
        public float Cantidad_Asignada { set; get; }

        [NotMapped]
        public float Cantidad_Libre_Para_Asignar{ set; get; }

        [NotMapped]
        public bool PresentaPoliza { set; get; }
        [NotMapped]
        public string ComentarioPoliza { set; get; }

        #region PROPIEDADES DERIVADAS

        [NotMapped]
        public string FechaEstInicio
        {
            get
            {
                return Fecha_Est_Incio.ToString("dd/MM/yyyy");
            }
        }
        [NotMapped]
        public string FechaRealInicio
        {
            get
            {
                return Fecha_Real_Incio.ToString("dd/MM/yyyy");
            }
        }
        [NotMapped]
        public string FechaEstFin
        {
            get
            {
                return Fecha_Est_Fin.ToString("dd/MM/yyyy");
            }
        }
        [NotMapped]
        public string FechaRealFin
        {
            get
            {
                return Fecha_Real_Fin.ToString("dd/MM/yyyy");
            }
        }
        [NotMapped]
        public string sFecha_Real_Incio {
            get
            {

                return Fecha_Est_Incio.ToString("yyyy/MM/dd");
            }
        }
        [NotMapped]
        public string sFecha_Real_Fin {
            get
            {

                return Fecha_Est_Fin.ToString("yyyy/MM/dd");
            }
        }
        [NotMapped]
        public bool Vencida
        {
            get
            {

                return this.Finalizados == false &&
                    this.Fecha_Est_Fin <= DateTime.Now;
            }
        }
        [NotMapped]
        public bool ProximaVencer
        {
            get
            {

                return this.Fecha_Est_Fin > DateTime.Now &&
                    this.Fecha_Est_Fin <= DateTime.Now.AddDays(7);
            }
        }
        [NotMapped]
        public bool AlDia
        {
            get
            {
                return this.Fecha_Real_Incio > DateTime.MinValue &&
                    this.Vencida == false &&
                    this.Avance > 0;
            }
        }
        [NotMapped]
        public string EstadoAlternativo
        {
            get
            {
                return (this.Vencida ? ValoresUtiles.Estado_PPA_Vencida : string.Empty) + ", " +
                    (this.ProximaVencer ? ValoresUtiles.Estado_PPA_ProximaVencer : string.Empty) + ", " +
                    (this.AlDia ? ValoresUtiles.Estado_PPA_AlDia : string.Empty);
            }
        }
        [NotMapped]
        public bool TienePadre
        {
            get
            {
                return this.lPPADep_Padres.Count > 0;
            }
        }
        [NotMapped]
        public bool TieneHija
        {
            get
            {
                return this.lPPADep_Hijas.Count > 0;
            }
        }
        [NotMapped]
        public List<PlanProyAct_Dependencia> lPPADep_Padres
        {
            get
            {
                if (this.PlanProyAct_Dependencias != null)
                {
                    return this.PlanProyAct_Dependencias
                        .Where(ppad => ppad.PPAHijaId == this.Id)
                        .ToList();
                }
                else return new List<PlanProyAct_Dependencia>();
            }
        }
        [NotMapped]
        public List<PlanProyAct_Dependencia> lPPADep_Hijas
        {
            get
            {
                if (this.PlanProyAct_Dependencias != null)
                {
                    return this.PlanProyAct_Dependencias
                        .Where(ppad => ppad.PPAPadreId == this.Id)
                        .ToList();
                }
                else return new List<PlanProyAct_Dependencia>();
            }
        }
        [NotMapped]
        public PlanProyAct_Dependencia oPPADep_PadrePrimero
        {
            get
            {
                return lPPADep_Padres
                    .OrderBy(ppad => ppad.PPAPadre.Fecha_Est_Incio)
                    .FirstOrDefault();
            }
        }
        [NotMapped]
        public int IdPadrePrimero
        { 
            get
            {
                if (this.oPPADep_PadrePrimero != null)
                {
                    return this.oPPADep_PadrePrimero.PPAPadreId;
                }
                else return 0;
            }
        }
        [NotMapped]
        public string Rubro
        {
            get
            {
                if (this.Planificacion_Actividades_Rubro != null)
                {
                    return this.Planificacion_Actividades_Rubro.Nombre;
                }
                else return "Sin Rubro asignado.";
            }
        }
        [NotMapped]
        public string Actividad
        {
            get
            {
                if (this.Planificacion_Actividades != null)
                {
                    return this.Planificacion_Actividades.Nombre;
                }
                else return "Sin Actividad asignada.";
            }
        }
        [NotMapped]
        public string Ubicacion
        {
            get
            {
                if (this.Proyecto_Ubicaciones != null)
                {
                    return this.Proyecto_Ubicaciones.Nombre;
                }
                else return "Sin Ubicación asignada.";
            }
        }
        [NotMapped]
        public string Contratista
        {
            get
            {
                if (this.Contratistas != null)
                {
                    return this.Contratistas.Nombre;
                }
                else return "Sin Contratista asignada.";
            }
        }
        [NotMapped]
        public string Estado
        {
            get
            {
                string estado = ValoresUtiles.Estado_PPA_Programada;
                if (this.Finalizados)
                    estado = ValoresUtiles.Estado_PPA_Finalizada;
                else if (this.Vencida)
                    estado = ValoresUtiles.Estado_PPA_Vencida;
                else if (this.Fecha_Est_Incio < DateTime.Now &&
                    this.Fecha_Est_Fin > DateTime.Now)
                {
                    if (this.Avance == 0)
                        estado = ValoresUtiles.Estado_PPA_Atrasada;
                    else
                        estado = ValoresUtiles.Estado_PPA_EnCurso;
                }
                return estado;
            }
        }
        #endregion

        [NotMapped]
        public string _Monto_Formato
        {
            get
            {
                return "$ " + String.Format("{0:0.00}", Monto).Replace(',', '.');
            }
        }

        [NotMapped]
        public string _Monto_Formato2
        {
            get
            {
                return String.Format("{0:0.00}", Monto).Replace(',', '.');
            }
        }
        [NotMapped]
        public string _Cantidad_y_Medida
        {
            get
            {
                return (String.Format("{0:0.00}", Cantidad)).Replace(',','.') + " " + UnidadMedida; 
            }
        }

        [NotMapped]
        public string _Cantidad_Asignada
        {
            get
            {
                return (String.Format("{0:0.00}", Cantidad_Asignada)).Replace(',', '.') + " " + UnidadMedida;
            }
        }

        [NotMapped]
        public string _Nombre_Comentarios
        {
            get
            {
                return Actividad + " - " + Detalle;
            }
        }

    }
}
