#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Planificacion/_Actividades_LogGrilla.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cb983c66aee1894684233897fd4bf7717fe4576d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Planificacion__Actividades_LogGrilla), @"mvc.1.0.view", @"/Views/Planificacion/_Actividades_LogGrilla.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cb983c66aee1894684233897fd4bf7717fe4576d", @"/Views/Planificacion/_Actividades_LogGrilla.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Planificacion__Actividades_LogGrilla : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Gestion.EF.Models.Planificacion_Proyecto_Actividades_Log>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-md-4""> <h6 class=""card-title"">Listado de Logistica</h6></div>
    <div class=""col-md-6""></div>
    <div class=""col-md-2""><button class=""btn btn-primary"" id=""btn_Nuevo"" onclick=""Nuevo()""><i class=""far fa-edit""></i> Agregar  Logistica</button></div>
</div>

<br />
<div id=""GrillaKendo""></div>


<script>

    function grilla(datos) {
    $(""#GrillaKendo"").html(""<div id='grillaActividades_Log'></div>"");

    $(""#grillaActividades_Log"").kendoGrid({
        filterable: true,
        sortable: true,
        dataSource: {
            data: datos
        },
        columns: [

            { field: ""id"", title: ""Id"", width: ""60px"", media: ""(min-width: 850px)"" },
            { field: ""version"", title: ""Version"", media: ""(min-width: 450px)"" },
            { field: ""fecha_Est_Incio"", title: ""Fecha Inicio"", media: ""(min-width: 450px)"" },
            { field: ""fecha_Est_Fin"", title: ""Fecha Fin"", media: ""(min-width: 450px)"" },


            

            ");
            WriteLiteral("{ command: { name: \"kedit\", text:\"\", click: Editar }, title: \"Editar\", width: \"65px\" , attributes: { style: \"text-align:center;\" } }\r\n\r\n        ]\r\n\r\n    });\r\n}\r\n\r\n    var data = JSON.parse(\'");
#nullable restore
#line 41 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Planificacion/_Actividades_LogGrilla.cshtml"
                      Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n\r\n    grilla(data);\r\n\r\n\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Gestion.EF.Models.Planificacion_Proyecto_Actividades_Log>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
