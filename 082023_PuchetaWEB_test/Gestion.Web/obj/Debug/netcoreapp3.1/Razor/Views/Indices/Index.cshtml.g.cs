#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Indices/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c3b5eabe9e4958807c1acefdfbd082ad76331a6d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Indices_Index), @"mvc.1.0.view", @"/Views/Indices/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3b5eabe9e4958807c1acefdfbd082ad76331a6d", @"/Views/Indices/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Indices_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Indices/Index.cshtml"
  
    ViewData["Title"] = "Indices de Ajustes";
    Layout = "~/Views/Shared/_LayoutGestion.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <div class=""card  card-rojo"">
            <div class=""card-header cabecera"">
                <h5 class=""m-0"">Gestión de Indices de Ajuste</h5>
            </div>
            <div id=""conten_IndicesGrilla"" class=""card-body"">

            </div>
        </div>
    </div>
</div>

<div class=""modal fade  bd-example-modal-lg""
     id=""IndicesModal""
     tabindex=""-1""
     role=""dialog""
     aria-labelledby=""myLargeModalLabel""
     aria-hidden=""true""
     data-backdrop=""static"">
    <div class=""modal-dialog  modal-lg"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header cabecera"">
                <h5 id=""ModalTitulo"" class=""modal-title""></h5>
                <button type=""button"" style=""color:#ffffff"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
");
            WriteLiteral("\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n\r\n    <script>\r\n\r\n        cargaIndicesGrilla();\r\n\r\n        function cargaIndicesGrilla() {\r\n            var urlAction = \'");
#nullable restore
#line 49 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Indices/Index.cshtml"
                        Write(Url.Action("_IndicesGrilla","Indices"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
            $(""#conten_IndicesGrilla"").load(urlAction);
        }

        function Editar(e) {

            var dataItem = this.dataItem($(e.currentTarget).closest(""tr""));
            var id = dataItem.id;
            console.log(""Indice"", dataItem);
            $('#ModalTitulo').html("" Editar Indices"");
                $.get(""");
#nullable restore
#line 59 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Indices/Index.cshtml"
                  Write(Url.Action("_IndicesAbm","Indices"));

#line default
#line hidden
#nullable disable
                WriteLiteral("/\" + id,\r\n                    function (data) { $(\'.modal-body\').html(data); })\r\n                $(\'#IndicesModal\').modal(\'show\');\r\n            }\r\n\r\n        function Nuevo() {\r\n            $(\'#ModalTitulo\').html(\"Agregar Indice\");\r\n            $.get(\"");
#nullable restore
#line 66 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Indices/Index.cshtml"
              Write(Url.Action("_IndicesAbm","Indices"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/0"",
                function (data) { $('.modal-body').html(data); })
            $('#IndicesModal').modal('show');
        }

        $('#IndicesModal').on('hidden.bs.modal', function (e) {
            $('.modal-body').html("""");
            cargaIndicesGrilla();
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
