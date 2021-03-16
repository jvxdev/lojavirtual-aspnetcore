using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Cookie
{
    public class Cookie
    {
        private IHttpContextAccessor _context;

        public Cookie(IHttpContextAccessor context)
        {
            _context = context;
        }


        public void Create(string Key, string Value)
        {
            CookieOptions Options = new CookieOptions();
            Options.Expires = DateTime.Now.AddDays(7);

            _context.HttpContext.Response.Cookies.Append(Key, Value, Options);
        }


        public string Read(string Key)
        {
            return _context.HttpContext.Request.Cookies[Key];
        }


        public bool Exist(string Key)
        {
            if (_context.HttpContext.Request.Cookies[Key] == null)
            {
                return false;
            }

            return true;
        }


        public void Update(string Key, string Value)
        {
            if (Exist(Key))
            {
                Delete(Key);
            }

            Create(Key, Value);
        }


        public void Delete(string Key)
        {
                _context.HttpContext.Response.Cookies.Delete(Key);
        }


        public void DeleteAll()
        {
            var cookieList = _context.HttpContext.Request.Cookies.ToList();
            foreach (var cookie in cookieList)
            {
                Delete(cookie.Key);
            }
        }
    }
}
