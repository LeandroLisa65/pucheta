#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Login/Registro.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c327359acee05f6e07f3404e5847a90c43146e3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Login_Registro), @"mvc.1.0.view", @"/Views/Login/Registro.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c327359acee05f6e07f3404e5847a90c43146e3", @"/Views/Login/Registro.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Login_Registro : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LoginViewModels>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Login/Registro.cshtml"
  
    ViewData["Title"] = "Registro";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral(@"    <div class=""login-box"">
        <div class=""login-logo"">
            <a href=""#""><b>IOT</b>ecnologias</a>
        </div>
        <!-- /.login-logo -->
        <div class=""card"">
            <div class=""card-body login-card-body"">
                <div class=""row"">
                    <div class=""col-md-12"">
                        
                            <div class=""form-group"">
                                <label class=""control-label"">Usuario</label>
                                <input id=""Usuario"" class=""form-control"" />

                            </div>
                            <div class=""form-group"">
                                <label class=""control-label"">Nombre</label>
                                <input id=""Nombre"" class=""form-control"" />

                            </div>
                            <div class=""form-group"">
                                <label class=""control-label"">Apellido</label>
                                <input id=""Apellido""");
            WriteLiteral(@" class=""form-control"" />

                            </div>
                            <div class=""form-group"">
                                <label class=""control-label"">Clave</label>
                                <input id=""Clave"" class=""form-control"" />

                            </div>
                            <div class=""form-group"">
                                <label class=""control-label"">Email</label>
                                <input id=""Email"" class=""form-control"" />

                            </div>


                            <div class=""form-group"">
                                <input type=""submit"" value=""Create"" class=""btn btn-primary"" />
                            </div>
                        
                    </div>
                </div>
                <div class=""row"">
                    <div class=""col-12"">
                        <p class=""mb-0"">
                            Regresar <a href=""/Login"" class=""text-center""> Login desde ");
            WriteLiteral("aquí.</a>\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LoginViewModels> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591