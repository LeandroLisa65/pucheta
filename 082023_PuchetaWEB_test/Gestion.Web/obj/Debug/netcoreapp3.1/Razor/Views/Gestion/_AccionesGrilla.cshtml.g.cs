#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Gestion/_AccionesGrilla.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dd3bfdde4a37317ec027a77a2b2ef47e698dd5b3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Gestion__AccionesGrilla), @"mvc.1.0.view", @"/Views/Gestion/_AccionesGrilla.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd3bfdde4a37317ec027a77a2b2ef47e698dd5b3", @"/Views/Gestion/_AccionesGrilla.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Gestion__AccionesGrilla : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Gestion.EF.Models.Acciones>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-md-6""> <h6 class=""card-title"">Listado de Acciones</h6></div>
    <div class=""col-md-6""><button class=""btn btn-primary"" id=""btn_Nuevo"" onclick=""Nuevo()""><i class=""far fa-edit""></i> Agregar Accion</button></div>
</div>

<br />
<div id=""GrillaKendo""></div>


<script>

    function grilla(datos) {

    $(""#GrillaKendo"").html(""<div id='grillaAcciones'></div>"");

    $(""#grillaAcciones"").kendoGrid({
        filterable: true,
        sortable: true,
        dataSource: {
            data: datos
        },
        columns: [

            { field: ""id"", title: ""Id"", width: ""60px"" },
            { field: ""detalle"", title: ""Detalle"" },            
            { field: ""menuDetalle"", title: ""Menu""},

            { title: ""editar"", template: '# if (editar) {#<img src=""../dist/img/checkbox.png"" />#} else {#<img src=""../dist/img/uncheckbox.png"" />#}#', width: ""80px"", attributes: { style: ""text-align:center;"" } },
            { title: ""borrar"", template: '# ");
            WriteLiteral(@"if (borrar) {#<img src=""../dist/img/checkbox.png"" />#} else {#<img src=""../dist/img/uncheckbox.png"" />#}#', width: ""80px"", attributes: { style: ""text-align:center;"" } },
            { title: ""ocultar"", template: '# if (ocultarZona1) {#<img src=""../dist/img/checkbox.png"" />#} else {#<img src=""../dist/img/uncheckbox.png"" />#}#', width: ""80px"", attributes: { style: ""text-align:center;"" } },
            { title: ""estado"", template: '# if (activo) {#<img src=""../dist/img/checkbox.png"" />#} else {#<img src=""../dist/img/uncheckbox.png"" />#}#', width: ""80px"", attributes: { style: ""text-align:center;"" } },

            { command: { name: ""kedit"", text:"""", click: Editar }, title: ""Editar"", width: ""65px"" , attributes: { style: ""text-align:center;"" } }

        ]

    });
}

    var data = JSON.parse('");
#nullable restore
#line 42 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Gestion/_AccionesGrilla.cshtml"
                      Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n\r\n    grilla(data);\r\n\r\n\r\n</script>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Gestion.EF.Models.Acciones>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
