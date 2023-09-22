#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario2/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2d849f187828cfc692a235e531469b726974b3b1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ParteDiario2_Index), @"mvc.1.0.view", @"/Views/ParteDiario2/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2d849f187828cfc692a235e531469b726974b3b1", @"/Views/ParteDiario2/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ParteDiario2_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario2/Index.cshtml"
  
    ViewData["Title"] = "ParteDiario";
    Layout = "~/Views/Shared/_LayoutGestion.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <div class=""card  card-rojo"">
            <div class=""card-header cabecera"">
                <button style=""float:right;"" class=""btn btn-secondary""
                        onclick=""CargarInformesActVenc()"">
                    <i class=""far fa-bell""></i>
                    Informes Actividades Vencidas
                </button>
                <button style=""float:right;"" class=""btn btn-primary"" id=""btn_Nuevo"" onclick=""CrearParte()"">
                    <i class=""fa fa-plus-circle""></i>
                    Crear Parte Diario
                </button>
                <h5 class=""m-0"">Gestión de Parte Diario</h5>
            </div>
            <div id=""conten_ParteDiarioGrilla"" class=""card-body""></div>
        </div>
    </div>
</div>

<div class=""modal fade  bd-example-modal-lg""
     id=""ParteDiario_ContratistasModal""
     tabindex=""-1""
     role=""dialog""
     aria-labelledby=""myLargeModalLabel""
     aria-hidden=""true""
     data-");
            WriteLiteral(@"backdrop=""static"">
    <div class=""modal-dialog  modal-lg"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 id=""ModalTitulo"" class=""modal-title""></h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">

            </div>
        </div>
    </div>
</div>

<div class=""modal"" id=""divMdl_InformesActVenc"">
    <div class=""modal-dialog modal-lg"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Informes de Actividades Vencidas</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">×</span>
                </button>
            </div>
            <div class=""modal-body"" style=""over");
            WriteLiteral(@"flow-y: scroll; height:30em"">
                <div class=""row"">
                    <div id=""divMdlBody_InformesActVenc"" class=""col-md-12""></div>
                </div>
            </div>
            <div class=""modal-footer"">

            </div>
        </div>
    </div>
</div>

");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n\r\n    <script>\r\n        cargaParteDiarioGrilla();\r\n\r\n        function cargaParteDiarioGrilla() {\r\n            var urlAction = \'");
#nullable restore
#line 75 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario2/Index.cshtml"
                        Write(Url.Action("_ParteDiarioGrilla","ParteDiario2"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n            $(\"#conten_ParteDiarioGrilla\").load(urlAction);\r\n        }\r\n\r\n        $(\'#ParteDiarioModal\').on(\'hidden.bs.modal\', function (e) {\r\n            $(\'.modal-body\').html(\"\");\r\n            cargaParteDiarioGrilla();\r\n        })\r\n\r\n");
                WriteLiteral("\r\n");
                WriteLiteral("\r\n\r\n\r\n        function CrearParte() {\r\n            window.location.replace(\"/ParteDiario2/ParteDiario\");\r\n        }\r\n\r\n        function CargarInformesActVenc() {\r\n            $.get(\"");
#nullable restore
#line 108 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario2/Index.cshtml"
              Write(Url.Action("_GetInformesActividadesVencidas","ParteDiario2"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\",\r\n                function (data) {\r\n                    $(\'#divMdlBody_InformesActVenc\').html(data);\r\n                    $(\'#divMdl_InformesActVenc\').modal(\'show\');\r\n                }\r\n            );\r\n        }\r\n\r\n\r\n    </script>\r\n\r\n");
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
