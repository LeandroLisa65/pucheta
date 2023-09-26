#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1f2f983820d04131079c8ee7467797d3d37cc91b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Estadisticas__EstadisticasIncidentes), @"mvc.1.0.view", @"/Views/Estadisticas/_EstadisticasIncidentes.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1f2f983820d04131079c8ee7467797d3d37cc91b", @"/Views/Estadisticas/_EstadisticasIncidentes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Estadisticas__EstadisticasIncidentes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<object>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""card"">
    <div class=""card-header"">
        <h3 class=""card-title"">
            <strong>Incidentes</strong>
        </h3>
    </div>
    <div class=""card-body"">
        <div class=""form-group"">
            <div class=""row"" style=""margin-bottom:0.5em"">
                <div class=""col-md-8"">
                    <select id=""slcProyectos_EstInc"" class=""form-control""></select>
                </div>
                <div class=""col-md-4"">
                    <button type=""button"" class=""btn btn-default float-right""
                            onclick=""recargar_EstInc()"">
                        <i class=""fas fa-search""></i>
                        Buscar
                    </button>
                </div>
            </div>
            <div class=""row"" style=""margin-bottom:0.5em"">
                <div class=""col-md-4"">
                    <input id=""inpFecDesde_EstInc""
                           type=""date""
                           class=""form-control"" />
                <");
            WriteLiteral(@"/div>
                <div class=""col-md-4"">
                    <input id=""inpFecHasta_EstInc""
                           type=""date""
                           class=""form-control"" />
                </div>
                <div class=""col-md-4"">
                    <button class=""btn btn-default float-right""
                            onclick=""verDetalle_EstInc()"">
                        <i class=""fas fa-eye""></i>
                        Ver Detalle
                    </button>
                </div>
            </div>
            <div class=""row"">
                <div id=""divConsolidado_EstInc"" class=""col-md-12""></div>
            </div>
        </div>
    </div>
</div>

<script>

        var data = JSON.parse('");
#nullable restore
#line 51 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml"
                          Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
        var oConsolidado = data.oEstInc.oConsolidado;
        var lDetalles = data.oEstInc.lDetalles;
        hdrTituloModal.innerHTML = data.oEstInc.titulo;
        cargarSelect(""slcProyectos_EstInc"", data.lProyectos, 'id', 'nombre');
        inpFecDesde_EstInc.value = data.fecDesde;
        inpFecHasta_EstInc.value = data.fecHasta;
        cargar_Consolidado_EstInc();

        function cargar_Consolidado_EstInc() {
            let table = ""<table class='table table-hover table-striped'><tbody>"";
            table += ""<tr>"";
            table += ""<td colspan='2'>Dias promedio de Cierre</td>"";
            table += ""<td>"" + getNumeroFormateado(oConsolidado.cantDiasPromedioCierre) + ""</td>"";
            table += ""</tr>"";
            table += ""<tr>"";
            table += ""<td colspan='2'>Mayor cantidad de días para Cierre</td>"";
            table += ""<td>"" + oConsolidado.cantDiasCierreMaxima + ""</td>"";
            table += ""</tr>"";
            table += ""<tr>"";
            table += ""<td ro");
            WriteLiteral("wspan=\'4\'>Días Promedio de demora para cada Estado</td>\";\r\n            table += \"<td>");
#nullable restore
#line 72 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml"
                     Write(Gestion.EF.ValoresUtiles.Estado_IncHist_Abierto);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\";\r\n            table += \"<td>\" + getNumeroFormateado(oConsolidado.cantDiasPromedioAbierto) + \"</td>\";\r\n            table += \"</tr>\";\r\n            table += \"<tr>\";\r\n            table += \"<td>");
#nullable restore
#line 76 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml"
                     Write(Gestion.EF.ValoresUtiles.Estado_IncHist_Enviado);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\";\r\n            table += \"<td>\" + getNumeroFormateado(oConsolidado.cantDiasPromedioEnviado) + \"</td>\";\r\n            table += \"</tr>\";\r\n            table += \"<tr>\";\r\n            table += \"<td>");
#nullable restore
#line 80 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml"
                     Write(Gestion.EF.ValoresUtiles.Estado_IncHist_Tratamiento);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\";\r\n            table += \"<td>\" + getNumeroFormateado(oConsolidado.cantDiasPromedioTratamiento) + \"</td>\";\r\n            table += \"</tr>\";\r\n            table += \"<tr>\";\r\n            table += \"<td>");
#nullable restore
#line 84 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml"
                     Write(Gestion.EF.ValoresUtiles.Estado_IncHist_Validacion);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>"";
            table += ""<td>"" + getNumeroFormateado(oConsolidado.cantDiasPromedioValidacion) + ""</td>"";
            table += ""</tr>"";
            table += ""</tbody><table>"";
            divConsolidado_EstInc.innerHTML = table;
        }

        function verDetalle_EstInc() {
            console.log(lDetalles);
            $(""#divDetalle"").html(""<div id='divGrilla_EstInc'></div>"");
            var columnas = [
                { field: ""Id"", title: ""ID"" },
                {
                    title: """);
#nullable restore
#line 97 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml"
                       Write(Gestion.EF.ValoresUtiles.Estado_IncHist_Abierto);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    columns: [
                        { field: ""FecInicioAbierto"", title: ""Entró"" },
                        { field: ""FecFinAbierto"", title: ""Salió"" },
                        { field: ""CantDiasAbierto"", title: ""Cant. Días"" }
                    ]
                },
                {
                    title: """);
#nullable restore
#line 105 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml"
                       Write(Gestion.EF.ValoresUtiles.Estado_IncHist_Enviado);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    columns: [
                        { field: ""FecInicioEnviado"", title: ""Entró"" },
                        { field: ""FecFinEnviado"", title: ""Salió"" },
                        { field: ""CantDiasEnviado"", title: ""Cant. Días"" }
                    ]
                },
                {
                    title: """);
#nullable restore
#line 113 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml"
                       Write(Gestion.EF.ValoresUtiles.Estado_IncHist_Tratamiento);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    columns: [
                        { field: ""FecInicioTratamiento"", title: ""Entró"" },
                        { field: ""FecFinTratamiento"", title: ""Salió"" },
                        { field: ""CantDiasTratamiento"", title: ""Cant. Días"" }
                    ]
                },
                {
                    title: """);
#nullable restore
#line 121 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Estadisticas/_EstadisticasIncidentes.cshtml"
                       Write(Gestion.EF.ValoresUtiles.Estado_IncHist_Validacion);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    columns: [
                        { field: ""FecInicioValidacion"", title: ""Entró"" },
                        { field: ""FecFinValidacion"", title: ""Salió"" },
                        { field: ""CantDiasValidacion"", title: ""Cant. Días"" }
                    ]
                },
                { field: ""FecCierre"", title: ""Cerró"" },
                { field: ""CantDiasTotal"", title: ""Cant. Días Total"" },
            ];
            $(""#divGrilla_EstInc"").kendoGrid({
                dataSource: {
                    data: lDetalles
                },
                columns: columnas,
                dataBound: function () {
                }
            });
            $(""#divMdlDetalle"").modal(""toggle"");
        }

        function recargar_EstInc() {
            const oFiltro = {};
            oFiltro.proyectoId = slcProyectos_EstInc.value;
            oFiltro.fecDesde = inpFecDesde_EstInc.value;
            oFiltro.fecHasta = inpFecHasta_EstInc.value;
            $.");
            WriteLiteral(@"post(""/Estadisticas/RecargarEstIncidentes"", oFiltro)
                .done(
                    function (res) {
                        if (res.isError) {
                            toastr.error(res.data, { timeOut: 2000 });
                        } else {
                            hdrTituloModal.innerHTML = res.oEstInc.titulo;
                            oConsolidado = res.oEstInc.oConsolidado;
                            lDetalles = res.oEstInc.lDetalles;
                            cargar_Consolidado_EstInc();
                        }
                    }
                );
        }

</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<object> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591