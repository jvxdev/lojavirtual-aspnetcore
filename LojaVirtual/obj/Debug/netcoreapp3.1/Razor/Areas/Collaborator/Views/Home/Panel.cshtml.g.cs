#pragma checksum "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Home\Panel.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f9b67fb1659aae2572544a96b4d3b442fea20544"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Collaborator_Views_Home_Panel), @"mvc.1.0.view", @"/Areas/Collaborator/Views/Home/Panel.cshtml")]
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
#line 4 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using LojaVirtual.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using LojaVirtual.Models.ProductAggregator;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using LojaVirtual.Models.Const;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using LojaVirtual.Models.ViewModel.Order;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using LojaVirtual.Areas.Collaborator;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using LojaVirtual.Libraries.Json.Resolver;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\_ViewImports.cshtml"
using LojaVirtual.Libraries.Text;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f9b67fb1659aae2572544a96b4d3b442fea20544", @"/Areas/Collaborator/Views/Home/Panel.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b795bbd6a96fe6188b4a71b32d94f30d4aa6711a", @"/Areas/Collaborator/Views/_ViewImports.cshtml")]
    public class Areas_Collaborator_Views_Home_Panel : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Client", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark col-md-12"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Product", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "NewsletterCSV", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success col-md-12"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Order", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Home\Panel.cshtml"
  
    ViewData["Title"] = "Painel de controle";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1 class=""display-4 my-4"">Painel de controle</h1>

<h1 class=""my-4 font-weight-light h3"">Informações gerais</h1>

<div class=""row"">
    <div class=""col-md-4"">
        <div class=""card"">
            <h5 class=""card-header"">Clientes</h5>
            <div class=""card-body"">
                <p class=""card-text"">Quantidade total de clientes: <strong>");
#nullable restore
#line 14 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Home\Panel.cshtml"
                                                                      Write(ViewBag.Clients);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></p>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9b67fb1659aae2572544a96b4d3b442fea205448647", async() => {
                WriteLiteral("Clique aqui para ver todos clientes");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
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
            WriteLiteral(@"
            </div>
        </div>
    </div>

    <div class=""col-md-4"">
        <div class=""card"">
            <h5 class=""card-header"">Produtos</h5>
            <div class=""card-body"">
                <p class=""card-text"">Quantidade total de produtos: <strong>");
#nullable restore
#line 24 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Home\Panel.cshtml"
                                                                      Write(ViewBag.Products);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></p>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9b67fb1659aae2572544a96b4d3b442fea2054410749", async() => {
                WriteLiteral("Clique aqui para ver todos produtos");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
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
            WriteLiteral(@"
            </div>
        </div>
    </div>

    <div class=""col-md-4"">
        <div class=""card"">
            <h5 class=""card-header"">Newsletter</h5>
            <div class=""card-body"">
                <p class=""card-text"">Quantidade total de participantes: ");
#nullable restore
#line 34 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Home\Panel.cshtml"
                                                                   Write(ViewBag.Newsletter);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9b67fb1659aae2572544a96b4d3b442fea2054412841", async() => {
                WriteLiteral("Clique aqui para gerar um arquivo CSV");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
    </div>
</div>

<h1 class=""my-4 font-weight-light h3"">Vendas</h1>

<div class=""row"">
    <div class=""col-md-4"">
        <div class=""card"">
            <h5 class=""card-header"">Pedidos realizados</h5>
            <div class=""card-body"">
                <p class=""card-text"">Quantidade total de pedidos realizados: <strong>");
#nullable restore
#line 48 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Home\Panel.cshtml"
                                                                                Write(ViewBag.TotalOrders);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></p>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9b67fb1659aae2572544a96b4d3b442fea2054414940", async() => {
                WriteLiteral("Clique aqui para ver todos pedidos");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
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
            WriteLiteral(@"
            </div>
        </div>
    </div>

    <div class=""col-md-4"">
        <div class=""card"">
            <h5 class=""card-header"">Total vendido</h5>
            <div class=""card-body"">
                <p class=""card-text"">Valor total: <strong>R$ ");
#nullable restore
#line 58 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Home\Panel.cshtml"
                                                        Write(ViewBag.TotalValueOrders);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</strong></p>
            </div>
        </div>
    </div>

    <div class=""col-md-4"">
        <div class=""card"">
            <h5 class=""card-header"">Forma de pagamento</h5>
            <div class=""card-body"">
                <p class=""card-text"">Cartão de crédito: <strong>");
#nullable restore
#line 67 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Home\Panel.cshtml"
                                                           Write(ViewBag.TotalOrdersCreditCard);

#line default
#line hidden
#nullable disable
            WriteLiteral(". </strong>Boleto Bancário: <strong>");
#nullable restore
#line 67 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Collaborator\Views\Home\Panel.cshtml"
                                                                                                                             Write(ViewBag.TotalOrdersBoletoBancario);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
