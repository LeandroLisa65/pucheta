#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/_IncidentesArchivos.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "127466b74449d23f340d6f8aaa01ad9198f638ae"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ParteDiario__IncidentesArchivos), @"mvc.1.0.view", @"/Views/ParteDiario/_IncidentesArchivos.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"127466b74449d23f340d6f8aaa01ad9198f638ae", @"/Views/ParteDiario/_IncidentesArchivos.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ParteDiario__IncidentesArchivos : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Gestion.EF.Models.Archivos_Adjuntos_Relacion>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
    <div class=""row"">
        <div class=""col-md-4""><h6 class=""card-title"">Archivos Adjuntos</h6></div>
        <div class=""col-md-6""></div>
        <div class=""col-md-2"" ></div>
    </div>

    <br />
    <div id=""Lista"" class=""row""></div>
    

    <script>
        var data = JSON.parse('");
#nullable restore
#line 14 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/_IncidentesArchivos.cshtml"
                          Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
        console.log(data);

        var htmlD = """";
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {

                if (data[i].archivos_Adjuntos.extencion == ""PNG"" || data[i].archivos_Adjuntos.extencion == ""JPG"" || data[i].archivos_Adjuntos.extencion == ""JPEG"") {
                    htmlD = htmlD + ""<div class='col-md-3'><a href='"" + data[i].archivos_Adjuntos.url + data[i].archivos_Adjuntos.archivo + ""' target='_blank'><img src='"" + data[i].archivos_Adjuntos.url + data[i].archivos_Adjuntos.archivo + ""' style='width:100%; height:auto;'/> </a>"" + data[i].archivos_Adjuntos.extencion + ""<br>"" + data[i].archivos_Adjuntos.sUsuario + ""</div>"";
                } else {
                    htmlD = htmlD + ""<div class='col-md-3'><a href='"" + data[i].archivos_Adjuntos.url + data[i].archivos_Adjuntos.archivo + ""' target='_blank'>"" + data[i].archivos_Adjuntos.archivo + ""<br>"" + data[i].archivos_Adjuntos.extencion + ""<br>"" + data[i].archivos_Adjuntos.sUsuario + "" </a></div");
            WriteLiteral(">\";\r\n                }\r\n\r\n            }\r\n        } else {\r\n            htmlD = htmlD + \"<div class=\'col-md-12\'>No hay archivos cargados</div>\";\r\n        }\r\n\r\n        $(\"#Lista\").html(htmlD);\r\n\r\n    </script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Gestion.EF.Models.Archivos_Adjuntos_Relacion>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
