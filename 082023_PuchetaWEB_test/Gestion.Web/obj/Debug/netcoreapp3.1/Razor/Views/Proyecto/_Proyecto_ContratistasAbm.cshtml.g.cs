#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Proyecto/_Proyecto_ContratistasAbm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2081531eea82270da286ec20a4232826c3b7e053"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Proyecto__Proyecto_ContratistasAbm), @"mvc.1.0.view", @"/Views/Proyecto/_Proyecto_ContratistasAbm.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2081531eea82270da286ec20a4232826c3b7e053", @"/Views/Proyecto/_Proyecto_ContratistasAbm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Proyecto__Proyecto_ContratistasAbm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Gestion.Web.Models.ItemListaContratista>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n    <div style=\"padding:10px;\">\r\n        <div class=\"row\">\r\n            <div class=\"col-md-12 \">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2081531eea82270da286ec20a4232826c3b7e0533850", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 7 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Proyecto/_Proyecto_ContratistasAbm.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
               
                    <div class=""form-group"">
                        <label class=""control-label"">Contratistas</label>
                        <div id=""Conten_Contratistas""></div>
                    </div>
               
            </div>
        </div>

        <div class=""row"">
            <div class=""col-md-2"">
                <div class=""btn-graba"" type=""button"" onclick=""GrabarProyecto_Contratistas()""><i class=""fas fa-check""></i> Grabar</div>
            </div>
            <div class=""col-md-2"">
                <div class=""btn-cerrar"" type=""button"" data-dismiss=""modal""><i class=""far fa-times-circle""></i> Cerrar</div>
            </div>
            <div class=""col-md-8""></div>
        </div>

        <br />

        <div class=""row"">
            <div class=""col-md-12"">
                <div id=""grilla_Contratistas""></div>
            </div>
        </div>

        <br />
    </div>


    <script>


        var data = JSON.parse('");
#nullable restore
#line 42 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Proyecto/_Proyecto_ContratistasAbm.cshtml"
                          Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
        console.log(data);

    grillaContratistas(data.lista);

    cargaContratistas();

    function GrabarProyecto_Contratistas() {

        var valid = validaDatos('ProyectoContratistaModal');
        if (!valid.isError) {

            var model = {
                ProyectoId: $('#Id').val(),
                ContratistaId: $('#Contratista').val(),
            };


            $.post(""/Proyecto/Proyecto_ContratistasGraba"", model).done(function (e) {

                if (!e.isError) {
                    toastr.success(e.data, { timeOut: 2000 });
                    grillaContratistas(e.data1);

                } else {
                    toastr.error(e.data, { timeOut: 2000 });
                }

            });


        } else {
            toastr.error(valid.mensaje, { timeOut: 2000 });
        }
    }


     function grillaContratistas(datos) {

         console.log(datos);

        $(""#grilla_Contratistas"").html(""<div id='contratistas'></div>"");

      ");
            WriteLiteral(@"  $(""#contratistas"").kendoGrid({
            dataSource: {
                data: datos
            },
            columns: [
                { field: ""id"", title: ""Id"" },
                { field: ""sContratistas"", title: ""Contratista"" },
                { command: { name: ""btn-borrar"", text: ""Borrar"", click: GetBorrarPContratista }, title: """", width: ""75px"",attributes: { style: ""text-align:center;"" } }
            ]

        });
    }




     function cargaContratistas() {

        $.get(""");
#nullable restore
#line 103 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Proyecto/_Proyecto_ContratistasAbm.cshtml"
          Write(Url.Action("GetContratistas", "Proyecto"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            function (data) {

                if (!data.isError) {
                    var d = '<select id=""Contratista"" class=""form-control"">';
                    d = d + '<option value=""0"">Seleccione un Contratista</option>';
                    for (var i = 0; i < data.data.length; i++) {
                        d = d + '<option value=""' + data.data[i].id + '"">' + data.data[i].nombre + '</option>';
                    }
                    d = d + '</select>';

                    $('#Conten_Contratistas').html(d);
                }

            }
        );
    }


        
    function GetBorrarPContratista(e) {
         var dataItem = this.dataItem($(e.currentTarget).closest(""tr""));
         console.log('dataContratista',dataItem);
         var id = dataItem.id;
           $.post(""/Proyecto/PContratistaBorra/"" + id).done(function (e) {

                    if (!e.isError) {
                        toastr.success(e.data, { timeOut: 2000 });
                        $('#Pro");
            WriteLiteral(@"yectoContratistaModal').modal('hide');
                        grillaContratistas(data.lista);
                        
                        

                    } else {
                        toastr.error(e.data, { timeOut: 2000 });
                    }

                });
    }




    </script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Gestion.Web.Models.ItemListaContratista> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
