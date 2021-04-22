﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using LojaVirtual.Models.ProductAggregator;

namespace LojaVirtual.Models.ViewModel.Components
{
    public class ProductListViewModel
    {
        public IPagedList<Product> productList { get; set; }


        public List<SelectListItem> ordinationList
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Ordem alfabética", "Oa"),
                    new SelectListItem("Menor preço", "Mep"),
                    new SelectListItem("Maior preço", "Map")
                };
            }
            private set { }
        }
    }
}