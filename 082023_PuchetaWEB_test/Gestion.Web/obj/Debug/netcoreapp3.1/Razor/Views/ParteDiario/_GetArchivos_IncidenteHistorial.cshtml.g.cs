#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/_GetArchivos_IncidenteHistorial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2e2e6a7b65bb9ad00ee32dcded097a9a698899cc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ParteDiario__GetArchivos_IncidenteHistorial), @"mvc.1.0.view", @"/Views/ParteDiario/_GetArchivos_IncidenteHistorial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2e2e6a7b65bb9ad00ee32dcded097a9a698899cc", @"/Views/ParteDiario/_GetArchivos_IncidenteHistorial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ParteDiario__GetArchivos_IncidenteHistorial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Gestion.EF.Models.Archivos_Adjuntos_Relacion>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div style=\"padding:10px;\">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-4\"><h6 class=\"card-title\">Archivos Adjuntos</h6></div>\r\n    </div>\r\n    <br />\r\n    <div id=\"Lista\" class=\"row\"></div>\r\n</div>\r\n\r\n<script>\r\n\r\n        var data = JSON.parse(\'");
#nullable restore
#line 13 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/_GetArchivos_IncidenteHistorial.cshtml"
                          Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
        var htmlD = """";
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].archivos_Adjuntos.extencion == ""PNG"" ||
                    data[i].archivos_Adjuntos.extencion == ""JPG"" ||
                    data[i].archivos_Adjuntos.extencion == ""JPEG"") {
                    htmlD = htmlD + ""<div id='Img_"" + data[i].archivos_AdjuntosId +
                        ""' class='col-md-3'><a href='/"" + data[i].archivos_Adjuntos.url +
                        data[i].archivos_Adjuntos.archivo + ""' target='_blank'><img src='"" +
                        data[i].archivos_Adjuntos.url + data[i].archivos_Adjuntos.archivo +
                        ""' style='width:100%; height:auto;'/>"" + data[i].nombre + "" </a>""+
                        "" <img onclick='deleteFile("" + data[i].archivos_AdjuntosId +
                        "")' src='../dist/img/deleteImage.png' style='float:right; padding:5px; width:30px; height:auto;' /> </div>"";
                } else {
");
            WriteLiteral(@"                    htmlD = htmlD + ""<div id='Img_"" + data[i].archivos_AdjuntosId +
                        ""' class='col-md-3'><a href='/"" + data[i].archivos_Adjuntos.url +
                        data[i].archivos_Adjuntos.archivo + ""' target='_blank'>"" +
                        data[i].nombre +
                        "" </a> <img onclick='deleteFile("" + data[i].archivos_AdjuntosId +
                        "")' src='../dist/img/deleteImage.png' style='float:right; padding:5px; width:30px; height:auto;' /></div>"";
                }
            }
        }
        else {
            htmlD = htmlD + ""<div class='col-md-12'>No hay archivos cargados</div>"";
        }
        $(""#Lista"").html(htmlD);

        function deleteFile(id) {
            $.get(""");
#nullable restore
#line 43 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/_GetArchivos_IncidenteHistorial.cshtml"
              Write(Url.Action("ParteDiarioArchivosDelete", "ParteDiario"));

#line default
#line hidden
#nullable disable
            WriteLiteral("/\" + id, function (data) {\r\n                $(\'#Img_\' + id).hide();\r\n            })\r\n        }\r\n\r\n</script>\r\n\r\n");
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
