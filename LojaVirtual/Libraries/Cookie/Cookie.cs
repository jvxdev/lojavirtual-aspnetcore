using LojaVirtual.Libraries.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Cookie
{
    public class Cookie
    {
        private IHttpContextAccessor _context;
        private IConfiguration _configuration;


        public Cookie(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public void Create(string Key, string Value)
        {
            CookieOptions Options = new CookieOptions();
            Options.Expires = DateTime.Now.AddDays(7);

            var cryptValue = StringCipher.Encrypt(Value, _configuration.GetValue<string>("KeyCrypt"));

            _context.HttpContext.Response.Cookies.Append(Key, cryptValue, Options);
        }


        public string Read(string Key, bool Cript = true)
        {
            var Value = _context.HttpContext.Request.Cookies[Key];
            if (Cript)
            {
                Value = StringCipher.Decrypt(Value, _configuration.GetValue<string>("KeyCrypt"));
            }
            
            return Value;
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
