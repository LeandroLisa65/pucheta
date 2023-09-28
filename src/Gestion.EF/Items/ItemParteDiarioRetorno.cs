using System;
using System.Collections.Generic;
using System.Text;
using Gestion.EF.Models;

namespace Gestion.EF
{
    public class ItemParteDiarioRetorno
    {
        public int ParteDiario_ActividadesId { set; get; }  //id de la tabla partediario_actividades
        public int ParteDiario_Id { set; get; }  // Id del parte diario
        public int ParteDiario_PPA { set; get; } //Id del la Actividad del Proyecto PLanifiacion
        
        public int RubroId { set; get; }
        public string sRubro { set; get; }
        public int ActividadesId { set; get; }  //Id de la Actividad
        public string sActividades { set; get; }
        public int UbicacionId { set; get; }
        public string sUbiucacion { set; get; }
        public string sComentario { set; get; } //Comentario que se carga en la PLanificacion

        public string FechaIncio { set; get; } //Fecha Estimada Inicio
        public string FechaFin { set; get; }  //Fecha Estimada Real
        public string FechaRealInicio { set; get;} // Fecha Real Inicio
        public string FechaRealFin { set; get; } // Fecha Real Fin
        
        public float CantidadPPA { set; get; }  //Cantidad a Cubrir
        public string sUnidad { set; get; } //Unidad de la Actividad
        public float CantidadAcum { set; get; }  //Cantidad acumulada
        public float CantidadAyer { set; get; }
        public float CantidadHoy { set; get; }

        public float Avance { set; get; }  //Porcentaje de Avance
        public string sDetalle { set; get; } //Cometario de Planificacion_Proyecto_Actividades
        public int ProyectoId { set; get; }
        public string ProyectoNombre { set; get;}
        public string Fecha_Creacion { get; set; }
        public double DiasDiferencia { set; get;} // Dias que faltan para terminar una Actividad
        public bool Finalizado { set; get;}
        public string FechaInicioProyecto { set; get; }
        public int Duracion { set; get; }

        public double DiasFinalizaObra { set; get;}

        public double DiasDiferenciaActual { set; get;}

        public string ColorFondo { set; get; } // COLOR PARA PONER EN LA GRILLA B=Blanco, V=Verde, A=Amarillo,R=Rojo
        public string ColorBoton { set; get; } // COLOR PARA PONER en el boton de la grilla de parte diario actividades: BLanco: Sin carga, Verde con carga de actividad con cantidad

        public List<Actividades_Contratista> Actividades_Contratista { set; get; }

        public ParteDiario ParteDiario { set; get; }

        public DateTime FecEstInicio
        {
            get
            {
                DateTime fecha = new DateTime();
                bool todoOk = DateTime.TryParse(this.FechaIncio, out fecha);
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
                        if (this.Avance == 0)
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
    }

    public class Actividades_Contratista
    {

        public int id { set; get; } // id de la tabla partediario_actividades_contratistas
        public int ParteDiario_ActividadesId {set; get;}
        public int Planificacion_Proyecto_ActividadesId { set; get;}
        public int ContratistasId { get; set; }
        public string sContratistas { get; set; }
        public float Cantidad { set; get; }
        public DateTime Fecha { set; get; }
        public bool PermitoBorar { set; get; }
        public string TipoRegistro { set; get; }

        public bool isArchivoContratistas { set; get; }
    }
}
