#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/ObraAdic.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "098534542b3826ccd23298f2fa4aa505d3e4daa5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ParteDiario_ObraAdic), @"mvc.1.0.view", @"/Views/ParteDiario/ObraAdic.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"098534542b3826ccd23298f2fa4aa505d3e4daa5", @"/Views/ParteDiario/ObraAdic.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ParteDiario_ObraAdic : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/ObraAdic.cshtml"
  
    ViewData["Title"] = "ObraAdic";
    Layout = "~/Views/Shared/_LayoutGestion.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <div class=""card  card-rojo"">
            <div class=""card-header"">
                <h5 class=""m-0"">Gestión de Obra Adicional</h5>
            </div>
            <div id=""conten_ObraAdicGrilla"" class=""card-body"">

            </div>
        </div>
    </div>
</div>


<div class=""modal fade  bd-example-modal-lg""
     id=""ObraAdicModal""
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

        </div>");
            WriteLiteral("\n    </div>\r\n</div>\r\n\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n\r\n    <script>\r\n\r\n        cargaObraAdicGrilla();\r\n\r\n        function cargaObraAdicGrilla() {\r\n            var urlAction = \'");
#nullable restore
#line 52 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/ObraAdic.cshtml"
                        Write(Url.Action("_ObraAdicGrilla","ParteDiario"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
            $(""#conten_ObraAdicGrilla"").load(urlAction);
        }

        function Editar(e) {

            var dataItem = this.dataItem($(e.currentTarget).closest(""tr""));
            var id = dataItem.id;

            $('#ModalTitulo').html("" Editar Parte Diario"");
                $.get(""");
#nullable restore
#line 62 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/ObraAdic.cshtml"
                  Write(Url.Action("_ObraAdicAbm","ParteDiario"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/"" + id,
                    function (data) { $('.modal-body').html(data); })
                $('#ObraAdicModal').modal('show');
            }

        function Nuevo() {
            $('#ModalTitulo').html("" Agregar Parte Diario"");
            $.get(""");
#nullable restore
#line 69 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/ParteDiario/ObraAdic.cshtml"
              Write(Url.Action("_ObraAdicAbm","ParteDiario"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/0"",
                function (data) { $('.modal-body').html(data); })
            $('#ObraAdicModal').modal('show');
        }

        $('#ObraAdicModal').on('hidden.bs.modal', function (e) {
            $('.modal-body').html("""");
            cargaObraAdicGrilla();
        })



    </script>

");
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
