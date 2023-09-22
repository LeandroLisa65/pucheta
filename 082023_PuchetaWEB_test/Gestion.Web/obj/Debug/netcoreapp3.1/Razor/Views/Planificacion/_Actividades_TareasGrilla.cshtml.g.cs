#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Planificacion/_Actividades_TareasGrilla.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "00e6228ba6a0e2ceedfe0248a6afd914fbde6593"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Planificacion__Actividades_TareasGrilla), @"mvc.1.0.view", @"/Views/Planificacion/_Actividades_TareasGrilla.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00e6228ba6a0e2ceedfe0248a6afd914fbde6593", @"/Views/Planificacion/_Actividades_TareasGrilla.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Planificacion__Actividades_TareasGrilla : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Gestion.EF.Models.Planificacion_Actividades_Tareas>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
    <div class=""row"">
        <div class=""col-md-4""> <h6 class=""card-title"">Listado de Tareas</h6></div>
        <div class=""col-md-6""></div>
        <div class=""col-md-2"">
            <div class=""btn-master"" type=""button"" onclick=""Nuevo()""><i class=""far fa-edit""></i> Agregar  Tarea</div>
        </div>
    </div>

<br />
<div id=""GrillaKendo""></div>


<script>

    function grilla(datos) {
        console.log(datos);
    $(""#GrillaKendo"").html(""<div id='grillaActividades_Tareas'></div>"");

    $(""#grillaActividades_Tareas"").kendoGrid({
        filterable: true,
        sortable: true,
        dataSource: {
            data: datos
        },
        columns: [

            { field: ""id"", title: ""Id"", width: ""60px"", media: ""(min-width: 850px)"",hidden: true },
            { field: ""nombre"", title: ""Nombre"", media: ""(min-width: 450px)"" },
            { field: ""descripción"", title: ""Descripción"", media: ""(min-width: 450px)"" },
            { field: ""planificacion_Actividades.nombre"",");
            WriteLiteral(@" title: ""Actividades"", media: ""(min-width: 450px)"" },


            { command: { name: ""kedit"", text:"""", click: Editar }, title: ""Editar"", width: ""65px"" , attributes: { style: ""text-align:center;"" } }

        ]

    });
}

    var data = JSON.parse('");
#nullable restore
#line 42 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Planificacion/_Actividades_TareasGrilla.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Gestion.EF.Models.Planificacion_Actividades_Tareas>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591