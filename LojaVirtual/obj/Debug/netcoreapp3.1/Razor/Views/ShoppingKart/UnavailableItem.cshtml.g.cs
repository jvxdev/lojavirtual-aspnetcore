#pragma checksum "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\UnavailableItem.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "868410f6bb2f6564694f4b571687cc00363a26ac"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ShoppingKart_UnavailableItem), @"mvc.1.0.view", @"/Views/ShoppingKart/UnavailableItem.cshtml")]
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
using LojaVirtual.Libraries.Login;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\_ViewImports.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\_ViewImports.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"868410f6bb2f6564694f4b571687cc00363a26ac", @"/Views/ShoppingKart/UnavailableItem.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fbc64115c7c98cb933ab86d6bb1171733dcc29dd", @"/Views/_ViewImports.cshtml")]
    public class Views_ShoppingKart_UnavailableItem : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\emotionalboys\Documents\GitHub\lojavirtual-aspnetcore\LojaVirtual\Views\ShoppingKart\UnavailableItem.cshtml"
   
    ViewData["Title"] = "Item não existe!";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <h1 class=\"display-2 text-center my-5\">Este item não existe!</h1>\r\n    <p class=\"text-center font-weight-light\"><strong>Poxa...</strong> O produto selecionado não está disponível!</p>\r\n</div>");
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
