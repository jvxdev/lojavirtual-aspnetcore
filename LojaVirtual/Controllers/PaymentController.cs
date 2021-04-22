﻿using AutoMapper;
using LojaVirtual.Controllers.Base;
using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Manager.Shipping;
using LojaVirtual.Libraries.Manager.Frete;
using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Manager.Payment;
using PagarMe;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaVirtual.Models.ViewModel.Payment;

namespace LojaVirtual.Controllers
{
    public class PaymentController : BaseController
    {
        private Cookie _cookie;
        private ManagePagarMe _managePagarMe;


        public PaymentController(ManagePagarMe managePagarMe, ClientLogin clientLogin, IDeliveryAddressRepository deliveryAddressRepository, IProductRepository productRepository, CookieShoppingKart cookieShoppingKart, CookieFrete cookieValorPrazoFrete, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage, Cookie cookie) : base(clientLogin, deliveryAddressRepository, productRepository, cookieShoppingKart, cookieValorPrazoFrete, mapper, wsCorreios, calculatePackage)
        {
            _cookie = cookie;
            _managePagarMe = managePagarMe;
        }


        [HttpGet]
        [ClientAuthorization]
        public IActionResult Index()
        {
            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);

            if (tipoFreteSelected != null)
            {
                List<ProductItem> productKartItemFull = ReadProductDB();

                ValorPrazoFrete frete = GetFrete();

                ViewBag.Frete = frete;
                ViewBag.Products = productKartItemFull;
                ViewBag.Installments = CalculateInstallment(productKartItemFull);

                return View("Index");
            }

            TempData["MSG_E"] = Message.MSG_E010;
            return RedirectToAction("DeliveryAddress", "ShoppingKart");
        }


        [HttpPost]
        [ClientAuthorization]
        public IActionResult Index([FromForm] IndexViewModel indexViewModel)
        {
            if (ModelState.IsValid)
            {
                DeliveryAddress deliveryAddress = GetAddress();
                ValorPrazoFrete frete = GetFrete();
                List<ProductItem> products = ReadProductDB();

                Installment installment = LocateInstallment(products, indexViewModel.Installment.Number);

                try
                {
                    dynamic pagarMeReturn = _managePagarMe.GerarPagCartaoCredito(indexViewModel.CreditCard, installment, deliveryAddress, frete, products);
                    
                    return new ContentResult() { Content = "Tudo certo! " + pagarMeReturn.TransactionId };
                }
                catch (PagarMeException e)
                {
                    StringBuilder sb = new StringBuilder();

                    if (e.Error.Errors.Count() > 0)
                    {
                        sb.Append("Erro no pagamento: ");

                        foreach(var erro in e.Error.Errors)
                        {
                            sb.Append(erro.Message + "<br />");
                        }
                    }
                    TempData["MSG_E"] = sb.ToString();

                    return Index();
                }
            }
            else
            {
                return Index();
            }
        }


        public IActionResult BoletoBancario()
        {
            DeliveryAddress deliveryAddress = GetAddress();
            ValorPrazoFrete frete = GetFrete();
            List<ProductItem> products = ReadProductDB();

            var totalPurchaseValue = GetTotalPurchaseValue(products);

            Boleto boleto = _managePagarMe.GerarBoleto(totalPurchaseValue);

            if (boleto.Erro != null)
            {
                TempData["MSG_E"] = boleto.Erro;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ContentResult() { Content = "Tudo certo! Código do boleto: " + boleto.TransactionId };
                //return View("OrderSuccess");
            }
        }


        private DeliveryAddress GetAddress()
        {
            DeliveryAddress deliveryAddress = null;

            var deliveryAddressId = int.Parse(_cookie.Read("ShoppingKart.DeliveryAddress", false).Replace("-end", ""));


            if (deliveryAddressId == 0)
            {
                Client client = _clientLogin.getClient();

                deliveryAddress = new DeliveryAddress();
                deliveryAddress.AddressName = "Endereço do cliente";
                deliveryAddress.Id = 0;
                deliveryAddress.CEP = client.CEP;
                deliveryAddress.State = client.State;
                deliveryAddress.City = client.City;
                deliveryAddress.Neighborhood = client.Neighborhood;
                deliveryAddress.Street = client.Street;
                deliveryAddress.HouseNumber = client.HouseNumber;
            }
            else
            {
                var Address = _deliveryAddressRepository.Read(deliveryAddressId);
            }

            return deliveryAddress;
        }


        private ValorPrazoFrete GetFrete()
        {
            var deliveryAddress = GetAddress();
            int cep = int.Parse(Mask.Delete(deliveryAddress.CEP));

            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);

            var shoppingKartHash = HashGenerator(_cookieShoppingKart.Read());

            Frete frete = _cookieFrete.Read().Where(a => a.CEP == cep && a.codShoppingKart == shoppingKartHash).FirstOrDefault();

            if (frete != null)
            {
                return frete.valuesList.Where(a => a.TipoFrete == tipoFreteSelected).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }


        private decimal GetTotalPurchaseValue(List<ProductItem> products)
        {
            ValorPrazoFrete frete = GetFrete();

            decimal total = Convert.ToDecimal(frete.Valor);

            foreach(var product in products)
            {
                total += product.Price;
            }

            return total;
        }


        private List<SelectListItem> CalculateInstallment(List<ProductItem> products)
        {
            var total = GetTotalPurchaseValue(products);
            var installment = _managePagarMe.CalcularPagamentoParcelado(total);

            return installment.Select(a => new SelectListItem(
                String.Format(
                    "{0}x {1} {2} - Valor total: {3}",
                    a.Number,
                    a.ValuePerInstallment.ToString("C"),
                    (a.Fees) ? "c/ juros" : "s/ juros",
                    a.Value.ToString("C")),
                    a.Number.ToString())).ToList();
        }


        private Installment LocateInstallment(List<ProductItem> products, int number)
        {
            return _managePagarMe.CalcularPagamentoParcelado(GetTotalPurchaseValue(products)).Where(a => a.Number == number).First();
        }
    }
}