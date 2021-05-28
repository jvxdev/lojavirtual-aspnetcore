#pragma checksum "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d1720aca012a92b9eb403602edb9aad7dde5c85e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Client_Views_Order_Show), @"mvc.1.0.view", @"/Areas/Client/Views/Order/Show.cshtml")]
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
#line 3 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\_ViewImports.cshtml"
using LojaVirtual.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\_ViewImports.cshtml"
using LojaVirtual.Models.ProductAggregator;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\_ViewImports.cshtml"
using LojaVirtual.Models.Const;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\_ViewImports.cshtml"
using LojaVirtual.Areas.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\_ViewImports.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\_ViewImports.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\_ViewImports.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\_ViewImports.cshtml"
using LojaVirtual.Libraries.Text;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d1720aca012a92b9eb403602edb9aad7dde5c85e", @"/Areas/Client/Views/Order/Show.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2675bfcba42b87437ed6793dd505f4d1281eff42", @"/Areas/Client/Views/_ViewImports.cshtml")]
    public class Areas_Client_Views_Order_Show : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Order>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
  
    ViewData["Title"] = "Visualizar pedido";

    TransactionPagarMe transaction = JsonConvert.DeserializeObject<TransactionPagarMe>(Model.TransactionData);
    List<ProductItem> products = JsonConvert.DeserializeObject<List<ProductItem>>(Model.ProductsData);

    var birthDay = DateTime.Parse(transaction.Customer.Birthday);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container \">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-12 my-5\">\r\n");
#nullable restore
#line 15 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
             foreach (var orderSituation in Model.OrderSituations)
            {
                //orderSituation.Situation - orderSituation.Date
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <h3 class=\"display-4 text-center mb-5\">Informações do pedido</h3>\r\n            <table class=\"table table-bordered\">\r\n                <tr>\r\n                    <td><strong>Nome:</strong> ");
#nullable restore
#line 23 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                          Write(transaction.Customer.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><strong>Data de nascimento:</strong> ");
#nullable restore
#line 24 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                                        Write(birthDay.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <td><strong>Forma de pagamento:</strong> ");
#nullable restore
#line 27 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                                        Write(Model.PaymentForm);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><strong>N° nota fiscal:</strong> ");
#nullable restore
#line 28 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                                    Write(Model.NFE);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                </tr>
            </table>

            <h3 class=""display-4 text-center my-5"">Informações do endereço de entrega</h3>
            <table class=""table table-bordered"">
                <tr>
                    <td colspan=""6"" class=""text-center""><strong>");
#nullable restore
#line 35 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                                           Write(transaction.Shipping.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></td>\r\n                </tr>\r\n                <tr>\r\n                    <td><strong>CEP:</strong> ");
#nullable restore
#line 38 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                         Write(transaction.Shipping.Address.Zipcode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><strong>Estado:</strong> ");
#nullable restore
#line 39 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                            Write(transaction.Shipping.Address.State);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><strong>Cidade:</strong> ");
#nullable restore
#line 40 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                            Write(transaction.Shipping.Address.City);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><strong>Bairro:</strong> ");
#nullable restore
#line 41 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                            Write(transaction.Shipping.Address.Neighborhood);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><strong>Rua:</strong> ");
#nullable restore
#line 42 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                         Write(transaction.Shipping.Address.Street);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><strong>N° casa/apart:</strong> ");
#nullable restore
#line 43 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                                   Write(transaction.Shipping.Address.StreetNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <td><strong>Empresa de entrega:</strong> ");
#nullable restore
#line 46 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                                        Write(Model.FreteCompany);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><strong>Valor do frete:</strong> ");
#nullable restore
#line 47 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                                    Write(Mask.ConvertPagarMeIntToDecimal(transaction.Shipping.Fee).ToString("C"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><strong>Código de rastreamento:</strong> ");
#nullable restore
#line 48 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                                                            Write(Model.FreteTrackingCod);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                </tr>
            </table>

            <h3 class=""display-4 text-center my-5"">Produtos do pedido</h3>
            <table class=""table table-bordered"">
                <tr>
                    <th>Nome</th>
                    <th>Quantidade</th>
                    <th>Valor (unid)</th>
                    <th>Valor total</th>
                </tr>

");
#nullable restore
#line 61 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                 foreach (var product in products)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 64 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                       Write(product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 65 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                       Write(product.ItensKartAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 66 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                       Write(product.Price.ToString("C"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 67 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                        Write(product.Price * product.ItensKartAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral(".ToString(\"C\")</td>\r\n                    </tr>\r\n");
#nullable restore
#line 69 "C:\Users\jvXvj\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Areas\Client\Views\Order\Show.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Order> Html { get; private set; }
    }
}
#pragma warning restore 1591
