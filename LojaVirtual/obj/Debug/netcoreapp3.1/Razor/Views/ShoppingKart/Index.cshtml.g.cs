#pragma checksum "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5612079df8044af5f9d04c4ceb73bfdd432d54fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ShoppingKart_Index), @"mvc.1.0.view", @"/Views/ShoppingKart/Index.cshtml")]
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
#line 4 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\_ViewImports.cshtml"
using LojaVirtual.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\_ViewImports.cshtml"
using LojaVirtual.Models.ProductAggregator;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\_ViewImports.cshtml"
using LojaVirtual.Models.ViewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\_ViewImports.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\_ViewImports.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5612079df8044af5f9d04c4ceb73bfdd432d54fa", @"/Views/ShoppingKart/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"251a12b0eaba32f86b89f4fbd97594e6d0dd7d56", @"/Views/_ViewImports.cshtml")]
    public class Views_ShoppingKart_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<LojaVirtual.Models.ProductAggregator.ProductItem>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/img-product.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-thumbnail img-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ShoppingKart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteItem", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-light btn-dark text-black"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
  
    ViewData["Title"] = "Carrinho de compras";

    decimal Subtotal = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<main role=""main"">
    <section class=""container"" id=""order"">
                <h1 class=""display-4 text-center my-5"">Carrinho de compras</h1>
                <div class=""alert alert-danger"" style=""display: none;"" role=""alert"">
                </div>   
                <div id=""code_cart"">
                    <div class=""card"">
                        <table class=""table table-hover shopping-cart-wrap"">
                            <thead class=""text-muted"">
                                <tr>
                                    <th scope=""col"">Nome do produto</th>
                                    <th scope=""col"" width=""120"">Quantidade</th>
                                    <th scope=""col"" width=""120"">Preço</th>
                                    <th scope=""col"" width=""200"" class=""text-right"">Ações</th>
                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 26 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td width=\"55%\">\r\n                                        <figure class=\"media\">\r\n                                            <div class=\"img-wrap\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "5612079df8044af5f9d04c4ceb73bfdd432d54fa7571", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</div>\r\n                                            <figcaption class=\"media-body\">\r\n                                                <h6 class=\"title text-truncate\">");
#nullable restore
#line 33 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
                                                                           Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h6>
                                                <dl class=""dlist-inline small"">
                                                    <dt>Size: </dt>
                                                    <dd>XXL</dd>
                                                </dl>
                                            </figcaption>
                                        </figure>
                                    </td>
                                    <td>
                                        <div>
                                            <div class=""input-group"">
                                                <input type=""hidden"" class=""input-product-id""");
            BeginWriteAttribute("value", " value=\"", 2339, "\"", 2355, 1);
#nullable restore
#line 44 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
WriteAttributeValue("", 2347, item.Id, 2347, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                                <input type=\"hidden\" class=\"input-product-stock\"");
            BeginWriteAttribute("value", " value=\"", 2457, "\"", 2477, 1);
#nullable restore
#line 45 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
WriteAttributeValue("", 2465, item.Amount, 2465, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                                <input type=\"hidden\" class=\"input-product-unitary-price\"");
            BeginWriteAttribute("value", " value=\"", 2587, "\"", 2606, 1);
#nullable restore
#line 46 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
WriteAttributeValue("", 2595, item.Price, 2595, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                                <div class=""input-group-append"">
                                                    <a href=""#"" class=""btn btn-secondary btn-less"">-</a>
                                                </div>
                                                <input type=""text"" style=""width: 30px;"" class=""form-control text-center input-product-amount-kart""");
            BeginWriteAttribute("value", " value=\"", 3002, "\"", 3031, 1);
#nullable restore
#line 50 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
WriteAttributeValue("", 3010, item.ItensKartAmount, 3010, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" readonly />
                                                <div class=""input-group-prepend"">
                                                    <a href=""#"" class=""btn btn-secondary btn-more"">+</a>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
");
#nullable restore
#line 57 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
                                       
                                        var subtotalItem = item.Price * item.ItensKartAmount;
                                        Subtotal = Subtotal + subtotalItem;
                                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td>\r\n                                        <div class=\"price-wrap\">\r\n                                            <var class=\"price\">");
#nullable restore
#line 63 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
                                                           Write((item.Price * item.ItensKartAmount).ToString("C"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</var>\r\n                                            <small class=\"text-muted\">unid/ (");
#nullable restore
#line 64 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
                                                                        Write(item.Price.ToString("C"));

#line default
#line hidden
#nullable disable
            WriteLiteral(")</small>\r\n                                        </div>\r\n                                    </td>\r\n                                    <td class=\"text-right d-none d-md-block\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5612079df8044af5f9d04c4ceb73bfdd432d54fa14101", async() => {
                WriteLiteral(" Remover");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 68 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
                                                                                                   WriteLiteral(item.Id);

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
            WriteLiteral("\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 71 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </tbody>
                        </table>
                    </div>
                </div>
    </section>

            <section class=""container my-5"">
                <div class=""row"">
                    <aside class=""col-md-6"">
                        <h4 class=""subtitle-doc"">
                            Frete
                        </h4>
                        <div id=""code_desc_simple"">
                            <div class=""box"">
                                <dl>
                                    <dt class=""mb-2"">CEP: </dt>
                                    <dd>
                                        <input type=""text"" name=""CEP""");
            BeginWriteAttribute("value", " value=\"", 5194, "\"", 5202, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-control\"/>\r\n                                    </dd>\r\n                                </dl>\r\n                                <dl>\r\n                                    <dd><input type=\"radio\" name=\"DeliveryCompany\"");
            BeginWriteAttribute("value", " value=\"", 5430, "\"", 5438, 0);
            EndWriteAttribute();
            WriteLiteral("/> <strong>SEDEX</strong> - R$ 389,99</dd>\r\n                                </dl>\r\n                                <dl>\r\n                                    <dd><input type=\"radio\" name=\"DeliveryCompany\"");
            BeginWriteAttribute("value", " value=\"", 5642, "\"", 5650, 0);
            EndWriteAttribute();
            WriteLiteral(@"/> <strong>CORREIOS</strong> - R$ 189,99</dd>
                                </dl>
                            </div>
                        </div>
                    </aside>
                    <aside class=""col-md-6"">
                        <h4 class=""subtitle-doc"">
                            Total a pagar
                        </h4>
                        <div id=""code_desc_right"">
                            <div class=""box"">
                                <dl class=""dlist-align"">
                                    <dt>Subtotal: </dt>
                                    <dd class=""text-right"">");
#nullable restore
#line 109 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\Index.cshtml"
                                                      Write(Subtotal.ToString("C"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</dd>
                                </dl>
                                <dl class=""dlist-align"">
                                    <dt>Frete:</dt>
                                    <dd class=""text-right"">R$ 789,99</dd>
                                </dl>
                                <dl class=""dlist-align"">
                                    <dt>Total:</dt>
                                    <dd class=""text-right h5 b"">R$ 989,99</dd>
                                </dl>
                            </div>
                        </div>
                    </aside>
                </div>
            </section>
</main>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<LojaVirtual.Models.ProductAggregator.ProductItem>> Html { get; private set; }
    }
}
#pragma warning restore 1591
