#pragma checksum "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6aa424a019fb10bb7d6a6d483c414e1876675442"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Collaborator_Views_Product_Index), @"mvc.1.0.view", @"/Areas/Collaborator/Views/Product/Index.cshtml")]
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
#line 3 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using LojaVirtual.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using LojaVirtual.Models.Const;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6aa424a019fb10bb7d6a6d483c414e1876675442", @"/Areas/Collaborator/Views/Product/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f9694f54ed8c1dea27e98148d52653a17afc4d7e", @"/Areas/Collaborator/Views/_ViewImports.cshtml")]
    public class Areas_Collaborator_Views_Product_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PagedList<LojaVirtual.Models.Product>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Register", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success my-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Update", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
  
    ViewData["Title"] = "Produtos";
    var Search = Context.Request.Query["Search"];

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"mt-3 mb-2 display-4\">Produtos</h1>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6aa424a019fb10bb7d6a6d483c414e18766754426718", async() => {
                WriteLiteral("\r\n    <div>\r\n        <label for=\"Search\" class=\"form-label\"></label>\r\n        <input type=\"text\" for=\"Search\" name=\"Search\"");
                BeginWriteAttribute("value", " value=\"", 336, "\"", 351, 1);
#nullable restore
#line 13 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
WriteAttributeValue("", 344, Search, 344, 7, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" id=\"Search\" class=\"form-control\" placeholder=\"Digite o nome do produto.\">\r\n    </div>\r\n    <button type=\"submit\" class=\"btn btn-primary mt-3\">Buscar produto</button>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6aa424a019fb10bb7d6a6d483c414e18766754428880", async() => {
                WriteLiteral("Cadastrar novo produto");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 20 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
 if (TempData["MSG_S"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p class=\"alert alert-success\">");
#nullable restore
#line 22 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                              Write(TempData["MSG_S"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 23 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 25 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
 if (Model.Count > 0)
{


#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"table-responsive\">\r\n        <table class=\"table\">\r\n            <thead class=\"thead-dark\">\r\n                <tr>\r\n                    <th scope=\"col\">");
#nullable restore
#line 32 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.First().Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 33 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.First().Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 34 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.First().Category));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">");
#nullable restore
#line 35 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.First().Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th scope=\"col\">Ações</th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 40 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                 foreach (Product product in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <th scope=\"row\">");
#nullable restore
#line 43 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                               Write(product.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 44 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                   Write(product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 45 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                   Write(product.CategoryId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>R$ ");
#nullable restore
#line 46 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                      Write(product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6aa424a019fb10bb7d6a6d483c414e187667544214635", async() => {
                WriteLiteral("Editar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 48 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                                                 WriteLiteral(product.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6aa424a019fb10bb7d6a6d483c414e187667544216946", async() => {
                WriteLiteral("Remover");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 49 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                                                 WriteLiteral(product.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 52 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n");
#nullable restore
#line 57 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
Write(Html.PagedListPager(Model, Page => Url.Action("Index", new { Page = Page, Search = Search })));

#line default
#line hidden
#nullable disable
#nullable restore
#line 57 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
                                                                                              

}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>Nenhum produto cadastrado.</p>\r\n");
#nullable restore
#line 63 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Product\Index.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PagedList<LojaVirtual.Models.Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591
