#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/Actividades_Foto.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6f6700b0f3227428346c2893c350620bc63bf1ef"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ParteDiario_Actividades_Foto), @"mvc.1.0.view", @"/Views/ParteDiario/Actividades_Foto.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6f6700b0f3227428346c2893c350620bc63bf1ef", @"/Views/ParteDiario/Actividades_Foto.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ParteDiario_Actividades_Foto : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/Actividades_Foto.cshtml"
  
    ViewData["Title"] = "Actividades_Foto";
    Layout = "~/Views/Shared/_LayoutGestion.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <div class=""card  card-rojo"">
            <div class=""card-header"">
                <h5 class=""m-0"">Gestión de Foto</h5>
            </div>
            <div id=""conten_Actividades_FotoGrilla"" class=""card-body"">

            </div>
        </div>
    </div>
</div>


<div class=""modal fade  bd-example-modal-lg""
     id=""Actividades_FotoModal""
     tabindex=""-1""
     role=""dialog""
     aria-labelledby=""myLargeModalLabel""
     aria-hidden=""true""
     data-backdrop=""static"">
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

        <");
            WriteLiteral("/div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n\r\n    <script>\r\n\r\n        cargaActividades_FotoGrilla();\r\n\r\n        function cargaActividades_FotoGrilla() {\r\n            var urlAction = \'");
#nullable restore
#line 52 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/Actividades_Foto.cshtml"
                        Write(Url.Action("_Actividades_FotoGrilla","ParteDiario"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
            $(""#conten_Actividades_FotoGrilla"").load(urlAction);
        }

        function Editar(e) {

            var dataItem = this.dataItem($(e.currentTarget).closest(""tr""));
            var id = dataItem.id;

            $('#ModalTitulo').html("" Editar Foto"");
                $.get(""");
#nullable restore
#line 62 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/Actividades_Foto.cshtml"
                  Write(Url.Action("_Actividades_FotoAbm","ParteDiario"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/"" + id,
                    function (data) { $('.modal-body').html(data); })
                $('#Actividades_FotoModal').modal('show');
            }

        function Nuevo() {
            $('#ModalTitulo').html("" Agregar Foto"");
            $.get(""");
#nullable restore
#line 69 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/Actividades_Foto.cshtml"
              Write(Url.Action("_Actividades_FotoAbm","ParteDiario"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/0"",
                function (data) { $('.modal-body').html(data); })
            $('#Actividades_FotoModal').modal('show');
        }

        $('#ParteDiarioModal').on('hidden.bs.modal', function (e) {
            $('.modal-body').html("""");
            cargaParteDiarioGrilla();
        })



    </script>

");
            }
            );
            WriteLiteral("\r\n");
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
