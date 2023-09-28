using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion.Web.Models;
using Gestion.EF.Models;
using Gestion.EF;

namespace Gestion.Web.Models
{
    public class Res_PD_Resumen
    {

        //CAMBIAR TODO ESTO

        // PARTE DIARIO
        public int ParteDiarioId { set; get; }
        public int ProyectoId { set; get; }
        public string ProyectoNombre { set; get; }
        public string ParteDiario_Fecha { set; get; }

        public List<Proyecto_Ubicaciones> Proyecto_Ubicaciones { set; get; }

        public int Ubicaciones { set; get;}


        //Listado de Actividades cargadas en ese parte diario
        public List<ItemResumen_Actividades_Contratista> lstActContratistas { set; get; }

        //Listado de Asistencia
        public List<PDResumen_Asistencia_Contratista> lstAsistContratista { set; get; }

        //Listado de Incidentes
        public List<PDResumen_Incidentes> lstIncidentes { set; get; }

        public List<PDResumen_IncidentesHitorial> lstIncidentesHistorial { set; get; }
        public List<Calidad_Actividades_Valoracion> lstItemCalidadValoracion { set; get; }

        public List<string> lstDistinctContratistas { set; get; }
        public List<string> lstDistinctActividades { set; get; }
        public List<string> lstDistinctUbicacion { set; get; }
        public List<string> lEstados
        {
            get
            {
                return ValoresUtiles.Get_lEstadosAlternativos_PPA();
            }
        }

    }

    public class PDResumen_Asistencia_Contratista  
    {
        public int ContratistasId { get; set; }
        public string sContratistas { get; set; }
        public string Asignados { set; get; }
        public string Asig_Presentes { set; get; }
        public string Covid { set; get; }
        public string Comentario { set; get; }
        
    }
    public class PDResumen_Incidentes  
    {
        public int ContratistasId { get; set; }
        public string sContratistas { get; set; }

        public int IncidenteId { get; set; }
        public string sIncidente { get; set; }

        public int ParteDiario_IncidentesId { set; get; }

        public string sComentario { set; get; }

        public string sSolicitadoPor  { set; get; }

        public string sCriticidad { set; get; }

        public bool IsArchivos { set; get; }

    }


    public class PDResumen_IncidentesHitorial
    {
        public int Id { set; get; }
        public int ParteDiarioId { set; get; }
        public int IncidenteId { set; get; }
        public int ContratistaId { set; get; }
        public string Comentario { set; get; }
        public string SolicitadoPor { set; get; }
        public string sIncidente { set; get; }
        public string sContratista { set; get; }
        public int AreaId { set; get; }
        public string sAreaActual { set; get; }
        public int EstadoId { set; get; }
        public DateTime FechaCierre { set; get; }
        public string mUsuarioActual { set; get; }
        public long mIdUsuarioActual { set; get; }

    }
}
