using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Security
{
    public class StringMD5
    {
        public static string MD5Hash(string text)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(text));

                return Encoding.ASCII.GetString(result);
            }
        }
    }
}
