#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2939a8a1f87c8c32355cc2065bfe367c89557812"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Menu_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Menu/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2939a8a1f87c8c32355cc2065bfe367c89557812", @"/Views/Shared/Components/Menu/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_Components_Menu_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Gestion.EF.ItemMenuBarra>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("gAvatar"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-circle elevation-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("User Image"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"sidebar \">\r\n    <div class=\"user-panel mt-3 pb-3 mb-3 d-flex\">\r\n        <div class=\"image\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "2939a8a1f87c8c32355cc2065bfe367c895578124599", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 175, "~/dist/avatar/", 175, 14, true);
#nullable restore
#line 6 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
AddHtmlAttributeValue("", 189, Model.Avatar, 189, 13, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div class=\"info\">\r\n            <a href=\"/Gestion/Perfil\" class=\"d-block\">");
#nullable restore
#line 9 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
                                                 Write(Model.NombreApellido);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>           \r\n            <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 422, "\"", 446, 1);
#nullable restore
#line 10 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
WriteAttributeValue("", 430, Model.IdUsuario, 430, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"IdUsuario\" />\r\n        </div>\r\n    </div>\r\n\r\n    <div id=\"menu\" style=\"min-height:300px;\">\r\n        <nav class=\"mt-2\">\r\n            <ul class=\"nav nav-pills nav-sidebar flex-column\" data-widget=\"treeview\" role=\"menu\" data-accordion=\"false\">\r\n\r\n");
#nullable restore
#line 18 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
                 foreach (var item in Model.Menu)
                {

                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
                     if (item.menu_h.Count > 0)
                    {


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <li");
            BeginWriteAttribute("class", " class=\'", 869, "\'", 916, 3);
            WriteAttributeValue("", 877, "nav-item", 877, 8, true);
            WriteAttributeValue(" ", 885, "has-treeview", 886, 13, true);
#nullable restore
#line 24 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
WriteAttributeValue(" ", 898, item.menu.Activo, 899, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <a href=\'#\' class=\'nav-link\'>\r\n                                <i");
            BeginWriteAttribute("class", " class=\'", 1013, "\'", 1036, 1);
#nullable restore
#line 26 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
WriteAttributeValue("", 1021, item.menu.Icon, 1021, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></i>\r\n                                <p> ");
#nullable restore
#line 27 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
                               Write(item.menu.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\'right fas fa-angle-left\'></i></p>\r\n                            </a>\r\n                            <ul class=\'nav nav-treeview\'>\r\n\r\n");
#nullable restore
#line 31 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
                                 foreach (var itemH in item.menu_h)
                                {


#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <li class=\'nav-item\'>\r\n                                        <a");
            BeginWriteAttribute("href", " href=\'", 1445, "\'", 1463, 1);
#nullable restore
#line 35 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
WriteAttributeValue("", 1452, itemH.Path, 1452, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\'", 1464, "\'", 1494, 2);
            WriteAttributeValue("", 1472, "nav-link", 1472, 8, true);
#nullable restore
#line 35 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
WriteAttributeValue(" ", 1480, itemH.Activo, 1481, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <i");
            BeginWriteAttribute("class", " class=\'", 1544, "\'", 1563, 1);
#nullable restore
#line 36 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
WriteAttributeValue("", 1552, itemH.Icon, 1552, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></i>\r\n                                            <p>");
#nullable restore
#line 37 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
                                          Write(itemH.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                        </a>\r\n                                    </li>\r\n");
#nullable restore
#line 40 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"

                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </ul>\r\n\r\n                        </li>\r\n");
#nullable restore
#line 46 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"

                    }
                    else
                    {


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <li class=\'nav-item\'>\r\n                            <a");
            BeginWriteAttribute("href", " href=\'", 1986, "\'", 2008, 1);
#nullable restore
#line 52 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
WriteAttributeValue("", 1993, item.menu.Path, 1993, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\'", 2009, "\'", 2043, 2);
            WriteAttributeValue("", 2017, "nav-link", 2017, 8, true);
#nullable restore
#line 52 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
WriteAttributeValue(" ", 2025, item.menu.Activo, 2026, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                <i");
            BeginWriteAttribute("class", " class=\'", 2081, "\'", 2104, 1);
#nullable restore
#line 53 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
WriteAttributeValue("", 2089, item.menu.Icon, 2089, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></i>\r\n                                <p>");
#nullable restore
#line 54 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
                              Write(item.menu.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </a>\r\n                        </li>\r\n");
#nullable restore
#line 57 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"

                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
                     

                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                

            </ul>
        </nav>

    </div>
</div>
<div class=""tramaBackMenu"">
   
    <div style=""width:100%; position: relative; text-align:center;"">
        <br /><br /><br /><br /><br />
        <div style=""font-size:12px; color:#090248"" hidden> IP : ");
#nullable restore
#line 74 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Shared/Components/Menu/Default.cshtml"
                                                           Write(Model.IP);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Gestion.EF.ItemMenuBarra> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
