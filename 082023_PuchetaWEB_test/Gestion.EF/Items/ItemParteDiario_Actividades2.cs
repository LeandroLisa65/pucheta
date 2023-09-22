using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItemParteDiario_Actividades2
    {
        public int Id { set; get; } //Id del Part Diario Actividad Contratista
        public int ParteDiarioId { set; get; }

        //datos de la Nota de Pedido
        public int NotaPedidoId { get; set; }
        public string NotaPedidoNro { get; set; }
        public int NotaPedidoDetalleId { get; set; }

        //datos del Contratista
        public int ContratistasId { get; set; }
        public string sContratistas { get; set; }

        //Datos de la Actividad
        public int Planificacion_Proyecto_ActividadesId { set; get; } //PPA
        public int Proyecto_ActividadesId { set; get; } //PA
        public string sActividad { get; set; }
        public string sComentario { get; set; } //Comentario de la PA
        public string sUbicacion { get; set; }
        public string sRubro { get; set; }
 
        public double AdjudicadoNP { set; get; } //La cantidad que se le adjudico en la Nota de Pedido
        public string AdjudicadoMedidaNP { set; get; } //cantidad que lleva de avance para esta Nota de Pedido
        public double AvanceNP { set; get; } //cantidad que lleva de avance para esta Nota de Pedido
        public decimal AvanceActual { set; get; }
        public string _AvanceActual { set; get; }
        public float Cantidad { set; get; } //cantidad que carga de avance el JF
        public string _AdjudicadoNP
        {
            get
            {
                return (String.Format("{0:0.00}", AdjudicadoNP)).Replace(',', '.') + " " + AdjudicadoMedidaNP;
            }
        }
        public string _AvanceNP
        {
            get
            {
                return (String.Format("{0:0.00}", AvanceNP)).Replace(',', '.') + " " + AdjudicadoMedidaNP;
            }
        }


        
        
        public bool Finalizado { set; get; } //Si la actividad esta finalizada (ACTIVIDAD TOTAL)
        public bool FinalizadaActividadNP { set; get; } //SI la actividad esta finalizada en la NotaPedido

        public bool Trabajo { set; get; }
        public bool? TrabajoAyer { set; get; }

        public string FechaInicio { set; get; } //Fecha Estimada Inicio
        public string FechaFin { set; get; }  //Fecha Estimada Real
        public string FechaRealInicio { set; get; } // Fecha Real Inicio
        public string FechaRealFin { set; get; } // Fecha Real Fin
        public DateTime FechaCreacion { set; get; } // Fecha Creacion Parte Diario
        public DateTime FecEstInicio
        {
            get
            {
                DateTime fecha = new DateTime();
                bool todoOk = DateTime.TryParse(this.FechaInicio, out fecha);
                if (todoOk) return fecha;
                else return DateTime.MinValue;
            }
        }

        public DateTime FecEstFin
        {
            get
            {
                DateTime fecha = new DateTime();
                bool todoOk = DateTime.TryParse(this.FechaFin, out fecha);
                if (todoOk) return fecha;
                else return DateTime.MinValue;
            }
        }

        public string _ResumenFechas
        {
            get
            {
                return "<b>Est Inicio:</b>" + FecEstInicio.ToString("dd-MM-yyyy") + "<br><b>Est Fin:</b>" + FecEstFin.ToString("dd-MM-yyyy") + "<br><b>Real Inicio:</b>" + FechaRealInicio;
            }
        }

        public string _ResumenAvances
        {
            get
            {
                return "<b>Adjudicado:</b>" + _AdjudicadoNP + "<br><b>Avance:</b>" + _AvanceNP;
            }
        }

        public string ColorFondo { set; get; } // COLOR PARA PONER EN LA GRILLA B=Blanco, V=Verde, A=Amarillo,R=Rojo
        public string ColorBoton { set; get; } // COLOR PARA PONER en el boton de la grilla de parte diario actividades: BLanco: Sin carga, Verde con carga de actividad con cantidad

        public string Estado
        {
            get
            {
                string estado = ValoresUtiles.Estado_PPA_Programada;
                if (this.Finalizado) estado = ValoresUtiles.Estado_PPA_Finalizada;
                else
                {
                    if (this.FecEstFin <= DateTime.Today)
                        estado = ValoresUtiles.Estado_PPA_Vencida;
                    else if (this.FecEstInicio < DateTime.Today && this.FecEstFin > DateTime.Today)
                    {
                        if (this.AvanceNP == 0)
                            estado = ValoresUtiles.Estado_PPA_Atrasada;
                        else
                            estado = ValoresUtiles.Estado_PPA_EnCurso;
                    }
                }
                return estado;
            }
        }

        public bool Vencida
        {
            get
            {
                return this.Estado == ValoresUtiles.Estado_PPA_Vencida;
            }
        }
        public bool ProximaVencer
        {
            get
            {
                return this.Estado != ValoresUtiles.Estado_PPA_Finalizada &&
                    this.FecEstFin > DateTime.Now &&
                    this.FecEstFin <= DateTime.Now.AddDays(7);
            }
        }
        public bool AlDia
        {
            get
            {
                return this.Estado == ValoresUtiles.Estado_PPA_EnCurso ||
                    this.Estado == ValoresUtiles.Estado_PPA_Finalizada;
            }
        }
        public string EstadoAlternativo
        {
            get
            {
                return (this.Vencida ? ValoresUtiles.Estado_PPA_Vencida : string.Empty) + ", " +
                    (this.ProximaVencer ? ValoresUtiles.Estado_PPA_ProximaVencer : string.Empty) + ", " +
                    (this.AlDia ? ValoresUtiles.Estado_PPA_AlDia : string.Empty);
            }
        }


        public List<ItenCalidadParteDiario> ListaItems { set; get; }

    }
}
