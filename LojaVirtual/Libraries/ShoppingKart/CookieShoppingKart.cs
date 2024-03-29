﻿using LojaVirtual.Models.ProductAggregator;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Libraries.ShoppingKart
{
    public class CookieShoppingKart
    {
        private string Key = "ShoppingKart.Buy";
        private Cookie.Cookie _cookie;


        public CookieShoppingKart(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }


        public void Create(ProductItem productItem)
        {
            List<ProductItem> List;

            if (_cookie.Exist(Key))
            {
                List = Read();
                var itemLocation = List.SingleOrDefault(a => a.Id == productItem.Id);

                if (itemLocation == null)
                {
                    List.Add(productItem);
                }
                else
                {
                    itemLocation.ChosenUnits = itemLocation.ChosenUnits + 1;
                }
            }
            else
            {
                List = new List<ProductItem>();
                List.Add(productItem);
            }

            Save(List);
        }


        public List<ProductItem> Read()
        {
            if (_cookie.Exist(Key))
            {
                string Value = _cookie.Read(Key);
                return JsonConvert.DeserializeObject<List<ProductItem>>(Value);
            }
            else
            {
                return new List<ProductItem>();
            }
        }


        public bool Exist(string Key)
        {
            if (_cookie.Exist(Key))
            {
                return false;
            }

            return true;
        }


        public void Update(ProductItem productItem)
        {
            var List = Read();
            var itemLocation = List.SingleOrDefault(a => a.Id == productItem.Id);

            if (itemLocation != null)
            {
                itemLocation.ChosenUnits = productItem.ChosenUnits;
                Save(List);
            }
        }


        public void Delete(ProductItem item)
        {
            var List = Read();
            var ItemLocation = List.SingleOrDefault(a => a.Id == item.Id);

            if (ItemLocation != null)
            {
                List.Remove(ItemLocation);
                Save(List);
            }
        }


        public void DeleteAll()
        {
            _cookie.Delete(Key);
        }


        public void Save(List<ProductItem> List)
        {
            string Value = JsonConvert.SerializeObject(List);
            _cookie.Create(Key, Value);
        }
    }
}
