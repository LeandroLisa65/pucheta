using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion.EF
{
    public static class ValoresUtiles
    {
        public static string EmailTesting = "software_test@puchetaconstrucciones.com.ar";
        public static string EmailSeguimientoObra = "software@puchetaconstrucciones.com.ar";
        #region GENÉRICOS

        public static string Formato_yyyy_MM_dd = "yyyy-MM-dd";
        public static string Formato_dd_MM_yyyy = "dd-MM-yyyy";

        #region MENSAJES

        public static string Msj_Error = "Ocurrió un error! Si el problema persiste, contacte con el administrador.";
        public static string Msj_AccesoDenegado = "Acceso denegado.";

        #endregion

        #region PALABRAS CLAVES

        public static string OptTodos = "Todos";
        public static string OptTodas = "Todas";

        #endregion

        #endregion

        #region CRITICIDADES

        public static List<string> Get_lCriticidades_ParteDiarioIncidente() {
            return new List<string>() {
                Criticidad_PDI_Sin_Criticidad,
                Criticidad_PDI_Bloqueo_Parcial,
                Criticidad_PDI_Bloqueante
            };
        }
        public static string Criticidad_PDI_Sin_Criticidad = "Sin Criticidad";
        public static string Criticidad_PDI_Bloqueo_Parcial = "Bloqueo parcial";
        public static string Criticidad_PDI_Bloqueante = "Bloqueante";

        #endregion

        #region ROLES
        public static List<int> Get_lRolesAdmin()
        {
            return new List<int>() {
                IdRol_AdminFull,
                IdRol_Planificacion,
                IdRol_CoordinacionObra,
                IdRol_DireccionEmpresa,
                IdRol_CoordinacionEdificios
            };
        }

        public static int IdRol_AdminFull = 1;
        public static int IdRol_Planificacion = 5;
        public static int IdRol_JefeObra = 6;
        public static int IdRol_CoordinacionObra = 8;
        public static int IdRol_RecursosHumanos = 9;
        public static int IdRol_Administracion = 11;
        public static int IdRol_DireccionEmpresa = 13;
        public static int IdRol_CoordinacionEdificios = 18;
        public static int IdRol_SeguridadHigiene = 19;

        public static int IdIncidente_SeguridadHigiene_Inc_62 = 62;
        public static int IdIncidente_SeguridadHigiene_Nov = 63;
        public static int IdIncidente_SeguridadHigiene_Inc_64 = 64;
        public static int IdIncidente_AvisoImportante_Inc_55 = 55;

        #endregion

        #region USUARIOS

        public static string NombreUsuario_Personal = "personal@puchetaconstrucciones.com.ar";

        #endregion

        #region INCIDENTES

        public static List<string> Get_lRectypes_Incidente()
        {
            return new List<string>() {
                Rectype_Incidente_Novedad,
                Rectype_Incidente_IncidenteAccidente
            };
        }
        public static string Rectype_Incidente_Novedad = "Novedad";
        public static string Rectype_Incidente_IncidenteAccidente = "Incidente/Accidente";

        #region ESTADOS

        public static List<KeyValuePair<int, string>> Get_lEstados_IncidenteHistorial()
        {
            return new List<KeyValuePair<int, string>>() {
                new KeyValuePair<int, string>(IdEstado_IncHist_Abierto, Estado_IncHist_Abierto),
                new KeyValuePair<int, string>(IdEstado_IncHist_Tratamiento, Estado_IncHist_Tratamiento),
                new KeyValuePair<int, string>(IdEstado_IncHist_Enviado, Estado_IncHist_Enviado),
                new KeyValuePair<int, string>(IdEstado_IncHist_Validacion, Estado_IncHist_Validacion),
                new KeyValuePair<int, string>(IdEstado_IncHist_Cerrado, Estado_IncHist_Cerrado),
                new KeyValuePair<int, string>(IdEstado_IncHist_Cancelado, Estado_IncHist_Cancelado),
            };
        }

        public static int IdEstado_IncHist_Abierto = 1;
        public static string Estado_IncHist_Abierto = "Abierto";
        public static int IdEstado_IncHist_Tratamiento = 2;
        public static string Estado_IncHist_Tratamiento = "Tratamiento";
        public static int IdEstado_IncHist_Enviado = 3;
        public static string Estado_IncHist_Enviado = "Enviado";
        public static int IdEstado_IncHist_Validacion = 4;
        public static string Estado_IncHist_Validacion = "Validación";
        public static int IdEstado_IncHist_Cerrado = 50;
        public static string Estado_IncHist_Cerrado = "Cerrado";
        public static int IdEstado_IncHist_Cancelado = 99;
        public static string Estado_IncHist_Cancelado = "Cancelado";

        #endregion

        #endregion

        #region Planificacion Proyecto Actividades

        public static List<string> Get_lEstados_PPA()
        {
            return new List<string>() {
                Estado_PPA_EnCurso,
                Estado_PPA_Programada,
                Estado_PPA_Atrasada,
                Estado_PPA_Vencida,
                Estado_PPA_Finalizada
            };
        }
        public static string Estado_PPA_EnCurso = "En Curso";
        public static string Estado_PPA_Programada = "Programada";
        public static string Estado_PPA_Atrasada = "Atrasada";
        public static string Estado_PPA_Vencida = "Vencida";
        public static string Estado_PPA_Finalizada = "Finalizada";

        public static List<string> Get_lBusqueda_Filtros()
        {
            return new List<string>() {
                Busqueda_PPA_Rubro,
                Busqueda_PPA_Ubicacion,
                Busqueda_PPA_Cronologicamente
            };
        }
        public static string Busqueda_PPA_Rubro = "Por Rubro";
        public static string Busqueda_PPA_Ubicacion = "Por Ubicacion";
        public static string Busqueda_PPA_Cronologicamente = "Cronologicamente";

        public static List<string> Get_lEstadosAlternativos_PPA()
        {
            return new List<string>()
            {
                Estado_PPA_Vencida,
                Estado_PPA_ProximaVencer,
                Estado_PPA_AlDia
            };
        }
        public static string Estado_PPA_ProximaVencer = "Próxima a vencer";
        public static string Estado_PPA_AlDia = "Al día";

        #endregion

        #region Path donde se graban los Archivos Adjuntos

        public static string PathArchivos_Temporal = "archivos/temporal/";
        public static string PathArchivos_Novedades = "archivos/PINC/";
        public static string PathArchivos_Incidentes = "archivos/AIH/";

        #endregion

        #region PARTE DIARIO ACTIVIDAD CONTRATISTA

        public static string TipoRegistro_PDAC_Automatico = "Automatico";
        public static string TipoRegistro_PDAC_Manual = "Manual";

        #endregion

        #region PROY CERTIFICADOS

        public static List<string> Get_lEstados_PCPDAC()
        {
            return new List<string>() {
                Estado_PCPDAC_Abierto,
                Estado_PCPDAC_Cerrado
            };
        }
        public static string Estado_PCPDAC_Abierto = "Abierto";
        public static string Estado_PCPDAC_Cerrado = "Cerrado";

        #endregion

        #region NOTIFICACIONES

        public static string Estado_Ntf_SinLeer = "Sin Leer";
        public static string Estado_Ntf_Leida = "Leida";

        #endregion

        #region PROYECTOS

        public static string Estado_Proy_Ejecutado = "Ejecutado";

        public static List<string> Get_lTipos_Proyectos()
        {
            return new List<string>() {
                Tipo_Proy_Obra,
                Tipo_Proy_Edificio
            };
        }
        public static string Tipo_Proy_Obra = "Obra";
        public static string Tipo_Proy_Edificio = "Edificio";

        #endregion

        #region CONTRATISTAS

        public static int IdContratista_Pucheta = 1;

        #endregion

        #region PROCESOS AUTOMÁTICOS

        public static string Motivo_ProcAut_ReplanificaionActividades = "Replanificación de Actividades.";

        #endregion
    }
}
