using LojaVirtual.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.ShoppingKart
{
    public class CookieValorPrazoFrete
    {
        private string Key = "ShoppingKart.ValorPrazoFrete";
        private Cookie.Cookie _cookie;


        public CookieValorPrazoFrete(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }


        public void Create(List<ValorPrazoFrete> list)
        {
            var jsonString = JsonConvert.SerializeObject(list);
            _cookie.Create(Key, jsonString);
        }


        public List<ValorPrazoFrete> Read()
        {
            if (_cookie.Exist(Key))
            {
                string Value = _cookie.Read(Key);
                return JsonConvert.DeserializeObject<List<ValorPrazoFrete>>(Value);
            }
            else
            {
                return null;
            }
        }


        public void DeleteAll()
        {
            _cookie.Delete(Key);
        }
    }
}
