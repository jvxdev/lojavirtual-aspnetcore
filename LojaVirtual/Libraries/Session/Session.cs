using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Session
{
    public class Session
    {
        private IHttpContextAccessor _context;

        public Session(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void Create(string Key, string Value)
        {
            _context.HttpContext.Session.SetString(Key, Value);
        }


        public string Read(string Key)
        {
            return _context.HttpContext.Session.GetString(Key);
        }

        
        public bool Exist(string Key)
        {
            if (_context.HttpContext.Session.GetString(Key) == null)
            {
                return false;
            }
                return true;
        }


        public void Update(string Key, string Value)
        {
            if(Exist(Key))
            {
                _context.HttpContext.Session.Remove(Key);
            }
            _context.HttpContext.Session.SetString(Key, Value);
        }


        public void Delete(string Key)
        {
            _context.HttpContext.Session.Remove(Key);
        }


        public void DeleteAll()
        {
            _context.HttpContext.Session.Clear();
        }
    }
}
