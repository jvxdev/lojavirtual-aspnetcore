using AutoMapper;
using LojaVirtual.Libraries.Manager.Shipping;
using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
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
        protected CookieValorPrazoFrete _cookieValorPrazoFrete;
        protected IMapper _mapper;
        protected WSCorreiosCalcularFrete _wsCorreios;
        protected CalculatePackage _calculatePackage;


        public BaseController(IProductRepository productRepository, CookieShoppingKart cookieShoppingKart, CookieValorPrazoFrete cookieValorPrazoFrete, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage)
        {
            _productRepository = productRepository;
            _cookieShoppingKart = cookieShoppingKart;
            _cookieValorPrazoFrete = cookieValorPrazoFrete;
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
    }
}
