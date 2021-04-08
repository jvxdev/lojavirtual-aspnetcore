﻿using AutoMapper;
using LojaVirtual.Libraries.Manager.Frete;
using LojaVirtual.Libraries.Manager.Shipping;
using LojaVirtual.Libraries.Security;
using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers.Base
{
    public class BaseController : Controller
    {
        protected IProductRepository _productRepository;
        protected CookieShoppingKart _cookieShoppingKart;
        protected CookieFrete _cookieFrete;
        protected IMapper _mapper;
        protected WSCorreiosCalcularFrete _wsCorreios;
        protected CalculatePackage _calculatePackage;


        public BaseController(IProductRepository productRepository, CookieShoppingKart cookieShoppingKart, CookieFrete cookieFrete, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage)
        {
            _productRepository = productRepository;
            _cookieShoppingKart = cookieShoppingKart;
            _cookieFrete = cookieFrete;
            _mapper = mapper;
            _wsCorreios = wsCorreios;
            _calculatePackage = calculatePackage;
        }


        protected List<ProductItem> ReadProductDB()
        {
            List<ProductItem> productKartItem = _cookieShoppingKart.Read();

            List<ProductItem> productKartItemFull = new List<ProductItem>();

            foreach (var item in productKartItem)
            {
                Product product = _productRepository.Read(item.Id);

                ProductItem productItem = _mapper.Map<ProductItem>(product);
                productItem.ItensKartAmount = item.ItensKartAmount;

                productKartItemFull.Add(productItem);
            }

            return productKartItemFull;
        }


        protected string HashGenerator(object obj)
        {
            return StringMD5.MD5Hash(JsonConvert.SerializeObject(obj));
        }
    }
}
