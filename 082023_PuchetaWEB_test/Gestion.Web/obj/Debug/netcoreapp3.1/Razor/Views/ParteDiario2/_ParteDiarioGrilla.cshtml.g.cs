#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario2/_ParteDiarioGrilla.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "64247d95fffdd50c98147289a8bb4652cc90c673"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ParteDiario2__ParteDiarioGrilla), @"mvc.1.0.view", @"/Views/ParteDiario2/_ParteDiarioGrilla.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"64247d95fffdd50c98147289a8bb4652cc90c673", @"/Views/ParteDiario2/_ParteDiarioGrilla.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ParteDiario2__ParteDiarioGrilla : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Res_PD>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-md-3"">
        <div class=""form-group"">
            <label class=""control-label"">Fecha Desde:</label>
            <input id=""FiltroFecDesde"" type=""date"" class=""form-control"" style=""width: 100%;"" contenteditable=""false"" />
        </div>
    </div>
    <div class=""col-md-3"">
        <div class=""form-group"">
            <label class=""control-label"">Fecha Hasta:</label>
            <input id=""FiltroFecHasta"" type=""date"" class=""form-control"" style=""width: 100%;"" contenteditable=""false"" />
        </div>
    </div>
    <div class=""col-md-3"">
        <div class=""form-group"">
            <label class=""control-label"">Proyecto</label>
            <input id=""ddlFiltroProyecto"" />
        </div>
    </div>
    <div style=""margin-top:25px;"" class=""col-md-3"">
        <div class=""row"">
            <div class=""col-md-6"">
                <button type=""button"" class=""btn btn-outline-dark float-right""
                        style=""margin-left:1em""
               ");
            WriteLiteral(@"         onclick=""cargaGrilla()"">
                    <i class=""fas fa-search""></i>
                    Buscar
                </button>
            </div>
            <div class=""col-md-6"">
            </div>
        </div>
    </div>
</div>
<br />
<div id=""GrillaKendo""></div>

<div class=""modal fade  bd-example-modal-xl""
     id=""ParteDiarioResumenModal""
     tabindex=""-1""
     role=""dialog""
     aria-labelledby=""myLargeModalLabel""
     aria-hidden=""true""
     data-backdrop=""static"">
    <div class=""modal-dialog modal-xl"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 id=""ModalTitulo"" class=""modal-title""></h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div id=""divMdlBody_ResumenPD"" class=""modal-body"">

            </div>

        </div>
    </div");
            WriteLiteral(">\r\n</div>\r\n\r\n\r\n\r\n<script>\r\n    var DataGlobal = [];\r\n    cargarFiltrosFechas();\r\n    cargaProyecto();\r\n    var data = JSON.parse(\'");
#nullable restore
#line 69 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario2/_ParteDiarioGrilla.cshtml"
                      Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');

    grilla(data);

    //#region FUNCIONES
    function cargarFiltrosFechas() {
        var FiltroFecDesde = document.getElementById('FiltroFecDesde');
        var FiltroFecHasta = document.getElementById('FiltroFecHasta');
        var fecActual = new Date();
        var mes = fecActual.getMonth() + 1;
        var fechaHasta = fecActual.getFullYear() + ""-"" + (mes < 10 ? (""0"" + mes) : mes) + ""-"" + fecActual.getDate();
        fecActual.setMonth(fecActual.getMonth() - 2);
        mes = fecActual.getMonth() + 1;
        var fechaDesde = fecActual.getFullYear() + ""-"" + (mes < 10 ? (""0"" + mes) : mes) + ""-"" + fecActual.getDate();
        FiltroFecDesde.value = fechaDesde;
        FiltroFecHasta.value = fechaHasta;
    }
    function cargaProyecto() {
        $.post(""/Proyecto/GetProyectosActivos"")
            .done(function (e) {
            if (!e.isError) {
                data2 = e.data;

                $(""#ddlFiltroProyecto"").kendoDropDownList({
                    filter: ""startsw");
            WriteLiteral(@"ith"",
                    dataTextField: ""nombre"",
                    dataValueField: ""id"",
                    optionLabel: ""Todos los Proyectos"",
                    value: 0,
                    dataSource: data2,
                    height: 400
                });
            }
            else {
                toastr.error(e.mensaje, { timeOut: 2000 });
            }
        });
    }
    function cargaGrilla() {
        var model = {
            fechaDesde: $('#FiltroFecDesde').val(),
            fechaHasta: $('#FiltroFecHasta').val(),
            proyecto: $(""#ddlFiltroProyecto"").val()
        }

        $.post(""/ParteDiario2/ParteDiarioGrillaFiltro"", model).done(function (e) {
            if (!e.isError) {
                grilla(e.data);
            }
        });

    }
    function grilla(datos) {

    $(""#GrillaKendo"").html(""<div id='grillaParteDiario'></div>"");

         var colGrilla = [];
        colGrilla.push({ field: ""id"", title: ""Id"", width: ""90px"", });
   ");
            WriteLiteral(@"     colGrilla.push({ field: ""proyectoNombre"", title: ""Proyecto"", media: ""(min-width: 150px)"" });
        colGrilla.push({ field: ""fecha_Creacion"", title: ""Fecha Parte Diario"", media: ""(min-width: 150px)"" });
        colGrilla.push({ command: { name: ""Ver"", text: ""Ver Informe"", click: VerDetalle }, title: ""Ver Informe"", width: ""120px"", });


    $(""#grillaParteDiario"").kendoGrid({
        filterable: true,
        sortable: true,
        groupable: true,
        reorderable: true,
        columnMenu: true,
        toolbar: [""excel""],
        excel: {
            fileName: ""ParteDiario.xlsx"",

            allPages: true,
            filterable: true
        },
        dataSource: {
            data: datos
        },
        columns: colGrilla
    });
    }

    function VerDetalle(e) {
        var d = this.dataItem($(e.currentTarget).closest(""tr""));
        var id = d.id;
        $('#ModalTitulo').html(""Resumen Parte Diario"");
        $.get(""");
#nullable restore
#line 156 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario2/_ParteDiarioGrilla.cshtml"
          Write(Url.Action("_ParteDiarioResumen","ParteDiario2"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/"" + id,
            function (data) {
                $('.modal-body').html(data);
                var windowHeight = $(window).height();
                var height = windowHeight / 5 * 4;
                $(""#divMdlBody_ResumenPD"").css({ 'overflow-y': 'scroll', 'height': (height + 'px') });

            })
        $('#ParteDiarioResumenModal').modal('show');
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Res_PD>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
