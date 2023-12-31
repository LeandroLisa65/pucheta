#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Calidad/CalidadObra.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "94fc797efe9bc71e1b9565c23648357b9194ff46"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Calidad_CalidadObra), @"mvc.1.0.view", @"/Views/Calidad/CalidadObra.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/_ViewImports.cshtml"
using Gestion.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/_ViewImports.cshtml"
using Gestion.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/_ViewImports.cshtml"
using Gestion.Web.ViewModels.Input;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"94fc797efe9bc71e1b9565c23648357b9194ff46", @"/Views/Calidad/CalidadObra.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Calidad_CalidadObra : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Calidad/CalidadObra.cshtml"
  
    ViewData["Title"] = "Calidad de la Obra";
    Layout = "~/Views/Shared/_LayoutGestion.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <div class=""card  card-rojo"">
            <div class=""card-header cabecera"">
                <h5 class=""m-0"">Gestión de calidad en la Obra</h5>
            </div>
            <div class=""card-body"">
                <div class=""row"">
                    <div class=""col-md-12"">
                        <div class=""row"">
                            <div class=""col-md-2"">
                                <label>Desde</label>
                                <input type=""date"" id=""inpFiltroFecDesde"" class=""form-control"" />
                            </div>
                            <div class=""col-md-2"">
                                <label>Hasta</label>
                                <input type=""date"" id=""inpFiltroFecHasta"" class=""form-control"" />
                            </div>
                            <button type=""button"" class=""btn btn-outline-dark float-right""
                                    style=""margin-left:1em""
    ");
            WriteLiteral(@"                                onclick=""cargaCalidadHistorialGrilla()"">
                                <i class=""fas fa-search""></i>
                                Buscar
                            </button>
                        </div>
                    </div>
                    <div class=""col-md-12"">
                        <div class=""row"">
                            <div class=""col-md-3"">
                                <div class=""form-group"">
                                    <label class=""control-label"">Proyecto:</label>
                                    <div id=""Conten_FiltroProyecto"">
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-3"">
                                <div class=""form-group"">
                                    <label class=""control-label"">Rubro:</label>
                                    <div id=""Conten_FiltroRubro"">
                  ");
            WriteLiteral(@"                  </div>
                                </div>
                            </div>
                            <div class=""col-md-3"">
                                <div class=""form-group"">
                                    <label class=""control-label"">Actividad:</label>
                                    <div id=""Conten_FiltroActividad"">
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-3"">
                                <div class=""form-group"">
                                    <label class=""control-label"">Resultado:</label>
                                    <div id=""Conten_FiltroResultado"">

                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div id=""conten_GrillaCalidadObra""></div>
                 ");
            WriteLiteral("   </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script>
    // JSON stringify serialization y limpieza
    JSON.stringify = JSON.stringify || function (obj) {
        var t = typeof (obj);
        if (t != ""object"" || obj === null) {
            // simple data type
            if (t == ""string"") obj = '""' + obj + '""';
            return String(obj);
        }
        else {
            // recurse array or object
            var n, v, json = [], arr = (obj && obj.constructor == Array);
            for (n in obj) {
                v = obj[n]; t = typeof (v);
                if (t == ""string"") v = '""' + v + '""';
                else if (t == ""object"" && v !== null) v = JSON.stringify(v);
                json.push((arr ? """" : '""' + n + '"":') + String(v));
            }
            var rawString = (arr ? ""["" : ""{"") + String(json) + (arr ? ""]"" : ""}"");
            return rawString;
        }
    };
    function escape(key, val) {
        if (typeof (val) != ""string"") return val;

        var replaced = val
        return replaced;");
                WriteLiteral("\r\n    }\r\n\r\n    JSON.stringifyEscaped = function (obj) {\r\n        return JSON.stringify(obj, escape);\r\n    }\r\n    //// FIN LIMPIEzA JSON\r\n\r\n    var data = JSON.parse(JSON.stringifyEscaped(");
#nullable restore
#line 108 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Calidad/CalidadObra.cshtml"
                                           Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"));

    //console.log(""Data"", data);
    //Cargo los combos de los Filtros
    cargarFiltrosFechas();
    cargaFiltroProyecto(data.lstProyectos);
    cargaFiltroRubro(data.lstRubros);
    cargaFiltroActividad(data.lstActividades);
    cargaFiltroResultado();
    cargaGrilla(data.lstItemCalidad);

    function cargaFiltroProyecto(data) {
        var d = '<select id=""ddlFiltroProyecto"" class=""form-control"" onchange=""Filtro();"" style=""font-size:12px"">';
        d = d + '<option value=""0"">Seleccione un Proyecto</option>';
        for (var i = 0; i < data.length; i++) {
            d = d + '<option value=""' + data[i] + '"">' + data[i] + '</option>';
        }
        d = d + '</select>';

        $('#Conten_FiltroProyecto').html(d);
    }

    function cargaFiltroRubro(data) {
        var d = '<select id=""ddlFiltroRubro"" class=""form-control"" onchange=""Filtro();"" style=""font-size:12px"">';
        d = d + '<option value=""0"">Seleccione un Rubro</option>';
        for (var i = 0; i < data.leng");
                WriteLiteral(@"th; i++) {
            d = d + '<option value=""' + data[i] + '"">' + data[i] + '</option>';
        }
        d = d + '</select>';

        $('#Conten_FiltroRubro').html(d);
    }

    function cargaFiltroActividad(data) {
        var d = '<select id=""ddlFiltroActividad"" class=""form-control"" onchange=""Filtro();"" style=""font-size:12px"">';
        d = d + '<option value=""0"">Seleccione una Actividad</option>';
        for (var i = 0; i < data.length; i++) {
            d = d + '<option value=""' + data[i] + '"">' + data[i] + '</option>';
        }
        d = d + '</select>';

        $('#Conten_FiltroActividad').html(d);
    }
    function cargaFiltroResultado() {
        var d = '<select id=""ddlFiltroResultado"" class=""form-control"" onchange=""Filtro();"" style=""font-size:12px"">';
        d = d + '   <option value=""0"" selected>Todos</option>';
        d = d + '    <option value=""Paso"">Paso Control</option>';
        d = d + '    <option value=""No Paso"">No Paso Control</option>';
        d = d ");
                WriteLiteral(@"+ '</select>';
        $('#Conten_FiltroResultado').html(d);

    }

    function cargarFiltrosFechas() {
        var inpFiltroFecDesde = document.getElementById('inpFiltroFecDesde');
        var inpFiltroFecHasta = document.getElementById('inpFiltroFecHasta');
        var fecActual = new Date();
        var mes = fecActual.getMonth() + 1;
        var dia = fecActual.getDate() + 1;
        var fechaHasta = fecActual.getFullYear() + ""-"" + (mes < 10 ? (""0"" + mes) : mes) + ""-"" + (dia < 10 ? (""0"" + dia) : dia);
        fecActual.setMonth(fecActual.getMonth() - 1);
        mes = fecActual.getMonth() + 1;
        var fechaDesde = fecActual.getFullYear() + ""-"" + (mes < 10 ? (""0"" + mes) : mes) + ""-"" + (dia < 10 ? (""0"" + dia) : dia);
        inpFiltroFecDesde.value = fechaDesde;
        inpFiltroFecHasta.value = fechaHasta;
    }
    function Filtro() {

        var selectedValueProyecto = ddlFiltroProyecto.options[ddlFiltroProyecto.selectedIndex].value;
        var selectedValueActividad = ddlFil");
                WriteLiteral(@"troActividad.options[ddlFiltroActividad.selectedIndex].value;
        var selectedValueRubro = ddlFiltroRubro.options[ddlFiltroRubro.selectedIndex].value;
        var selectedValueResultado = ddlFiltroResultado.options[ddlFiltroResultado.selectedIndex].value;

        var newData = data.lstItemCalidad;
        if (selectedValueProyecto != ""0"") {
            newData = newData.filter(s => s.sProyecto == selectedValueProyecto);
        }
        if (selectedValueActividad != ""0"") {
            newData = newData.filter(s => s.sActividad == selectedValueActividad);
        }
        if (selectedValueRubro != ""0"") {
            newData = newData.filter(s => s.sRubro == selectedValueRubro);
        }
        if (selectedValueResultado != ""0"") {
            if (selectedValueResultado == ""Paso"") {
                newData = newData.filter(s => s.valor == ""S"");
            }
            else {
                newData = newData.filter(s => s.valor == ""N"");
            }
        }
        cargaGrilla");
                WriteLiteral(@"(newData);
    }

    function VerIncidente(e) {

        var dataItem = this.dataItem($(e.currentTarget).closest(""tr""));
        var id = dataItem.idIncidente;  ///Es el Id de la Actividad
        var estadoFiltro = { Estado: '0', Visualizacion: '0', Situacion: '0', Proyecto: '0', Area: '0', IdIncidente: id };
        localStorage.setItem('ValoresFiltro', JSON.stringify(estadoFiltro));
        window.location.replace(""/incidentes/incidentesHistorial"");
    }

    function cargaGrilla(datos) {
        var colGrilla = [];
        colGrilla.push({ field: ""sFecha"", title: ""Fecha"", media: ""(min-width: 90px)"" });
        colGrilla.push({ field: ""sProyecto"", title: ""Proyecto"", media: ""(min-width: 150px)"" });
        colGrilla.push({ field: ""sUbicacion"", title: ""Ubicacion"", media: ""(min-width: 150px)"" });
        colGrilla.push({ field: ""sRubro"", title: ""Rubro"", media: ""(min-width: 4150px)"" });
        colGrilla.push({ field: ""sActividad"", title: ""Actividad"", media: ""(min-width: 150px)"" });
      ");
                WriteLiteral(@"  colGrilla.push({ field: ""sCalidadNombre"", title: ""Item Calidad"", media: ""(min-width: 150px)"" });
        colGrilla.push({ title: """", width: ""70px"", template: '# if (sCalidadItemCompleto) {#<button type=""button"" class=""btn btn-secondary"" data-toggle=""tooltip"" data-placement=""top"" title=""(#=sCalidadItemCompleto#)""><i class=""fa fa-info-circle"" aria-hidden=""true""></i></button>#} else {##}#', });
        colGrilla.push({ field: ""sValor"", title: ""Item Calidad"", width: ""1px""});
        colGrilla.push({ field: ""descripcion"", title: ""Descripcion"", media: ""(min-width: 450px)"" });
        colGrilla.push({ field: ""sIncidenteEstado"", title: ""Estado Inc."", media: ""(min-width: 450px)"" });
        colGrilla.push({ field: ""idIncidente"", title: """", width: ""1px"" });
        colGrilla.push({ command: { name: ""kedit"", text: """", click: VerIncidente }, title: ""Ver Incidente"", width: ""65px"", attributes: { style: ""text-align:center;"" } });

        $(""#conten_GrillaCalidadObra"").html(""<div id='grilla'></div>"");

        $");
                WriteLiteral(@"(""#grilla"").kendoGrid({
            filterable: true,
            sortable: true,
            filterable: true,
            groupable: true,
            reorderable: true,
            columnMenu: true,
            dataSource: {
                data: datos,
            },
            columns: colGrilla,
            dataBound: function () {
                dataView = this.dataSource.view();
                for (var i = 0; i < dataView.length; i++) {
                    if (dataView[i].sValor == ""No Paso"") {
                        var uid = dataView[i].uid;
                        $(""#grilla tbody"").find(""tr[data-uid="" + uid + ""]"").addClass(""K-bacgraundRow_R"");
                    }
                    if (dataView[i].idIncidente == ""0"" ) {
                        var uid = dataView[i].uid;
                        $(""#grilla tbody"").find(""tr[data-uid="" + uid + ""]"").find("".k-grid-kedit"").hide();
                    }
                }
            }
        });
    }

    function carga");
                WriteLiteral(@"CalidadHistorialGrilla() {
        var oFiltro = {
            FechaDesde: $('#inpFiltroFecDesde').val(),
            FechaHasta: $('#inpFiltroFecHasta').val()
        };
        $.post(""/Calidad/_CalidadObra"", oFiltro).done(
            function (data2) {
                data = data2;
                cargaFiltroProyecto(data.lstProyectos);
                cargaFiltroRubro(data.lstRubros);
                cargaFiltroActividad(data.lstActividades);
                cargaGrilla(data.lstItemCalidad);
        });
    }
    </script>

");
            }
            );
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
