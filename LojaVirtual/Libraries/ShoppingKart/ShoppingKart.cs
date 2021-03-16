using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.ShoppingKart
{
    public class ShoppingKart
    {
        private string Key = "ShoppingKart.Buy";
        private Cookie.Cookie _cookie;


        public ShoppingKart(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }


        public void Create(Item item)
        {
            List<Item> List;

            if (_cookie.Exist(Key))
            {
                List = Read();
                var itemLocation = List.SingleOrDefault(a => a.Id == item.Id);

                if (itemLocation != null)
                {
                    List.Add(item);
                }
                else
                {
                    itemLocation.Amount = itemLocation.Amount + 1;
                }
            }
            else
            {
                List = new List<Item>();
                List.Add(item);
            }

            Save(List);
        }


        public List<Item> Read()
        {
            if(_cookie.Exist(Key))
            {
                string Value = _cookie.Read(Key);
                return JsonConvert.DeserializeObject<List<Item>>(Value);
            }
            else
            {
                return new List<Item>();
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


        public void Update(Item item)
        {
            var List = Read();
            var itemLocation = List.SingleOrDefault(a => a.Id == item.Id);

            if (itemLocation != null)
            {
                itemLocation.Amount = item.Amount;
                Save(List);
            }
        }


        public void Delete(Item item)
        {
            var List = Read();
            var itemLocation = List.SingleOrDefault(a => a.Id == item.Id);

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


        public void Save(List<Item> List)
        {
            string Value = JsonConvert.SerializeObject(List);
            _cookie.Create(Key, Value);
        }
    }


    public class Item
    {
        public int? Id { get; set; }
        public int? Amount { get; set; }
    }
}
