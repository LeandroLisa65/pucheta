#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/_Actividades_FotoGrilla.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2ca360812c4f9b63773286747a7b3b7a9cea432d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ParteDiario__Actividades_FotoGrilla), @"mvc.1.0.view", @"/Views/ParteDiario/_Actividades_FotoGrilla.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ca360812c4f9b63773286747a7b3b7a9cea432d", @"/Views/ParteDiario/_Actividades_FotoGrilla.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ParteDiario__Actividades_FotoGrilla : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Gestion.EF.Models.ParteDiario_Actividades_Fotos>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-md-4""> <h6 class=""card-title"">Listado de Fotos</h6></div>
    <div class=""col-md-6""></div>
    <div class=""col-md-2""><button class=""btn btn-primary"" id=""btn_Nuevo"" onclick=""Nuevo()""><i class=""far fa-edit""></i> Agregar  Foto</button></div>
</div>

<br />
<div id=""GrillaKendo""></div>


<script>

    function grilla(datos) {
        console.log(datos);
    $(""#GrillaKendo"").html(""<div id='grillaActividades_Foto'></div>"");

    $(""#grillaActividades_Foto"").kendoGrid({
        filterable: true,
        sortable: true,
        dataSource: {
            data: datos
        },
        columns: [

            { field: ""id"", title: ""Id"", width: ""60px"", media: ""(min-width: 850px)"" },
            { field: ""urL_Foto"", title: ""Fotos"", media: ""(min-width: 450px)"" },
          

            { command: { name: ""kedit"", text:"""", click: Editar }, title: ""Editar"", width: ""65px"" , attributes: { style: ""text-align:center;"" } }

        ]

    });
}

    var");
            WriteLiteral(" data = JSON.parse(\'");
#nullable restore
#line 38 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/_Actividades_FotoGrilla.cshtml"
                      Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n\r\n    grilla(data);\r\n\r\n\r\n</script>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Gestion.EF.Models.ParteDiario_Actividades_Fotos>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
