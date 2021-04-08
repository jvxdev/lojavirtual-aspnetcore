using LojaVirtual.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.ShoppingKart
{
    public class CookieFrete
    {
        private string Key = "ShoppingKart.FreteValue";
        private Cookie.Cookie _cookie;


        public CookieFrete(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }


        public void Create(Frete item)
        {
            List<Frete> List;

            if (_cookie.Exist(Key))
            {
                List = Read();
                var itemLocation = List.SingleOrDefault(a => a.CEP == item.CEP);

                if (itemLocation == null)
                {
                    List.Add(item);
                }
                else
                {
                    itemLocation.codShoppingKart = item.codShoppingKart;
                    itemLocation.valuesList = item.valuesList;
                }
            }
            else
            {
                List = new List<Frete>();
                List.Add(item);
            }

            Save(List);
        }


        public List<Frete> Read()
        {
            if (_cookie.Exist(Key))
            {
                string Value = _cookie.Read(Key);
                return JsonConvert.DeserializeObject<List<Frete>>(Value);
            }
            else
            {
                return new List<Frete>();
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


        public void Update(Frete item)
        {
            var List = Read();
            var itemLocation = List.SingleOrDefault(a => a.CEP == item.CEP);

            if (itemLocation != null)
            {
                itemLocation.codShoppingKart = item.codShoppingKart;
                itemLocation.valuesList = item.valuesList;

                Save(List);
            }
        }


        public void Delete(Frete item)
        {
            var List = Read();
            var itemLocation = List.SingleOrDefault(a => a.CEP == item.CEP);

            if (itemLocation != null)
            {
                List.Remove(itemLocation);
                Save(List);
            }
        }


        public void DeleteAll()
        {
            _cookie.Delete(Key);
        }


        public void Save(List<Frete> List)
        {
            string Value = JsonConvert.SerializeObject(List);
            _cookie.Create(Key, Value);
        }
    }
}
