using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Text
{
    public class Mask
    {
        public static string Delete(string value)
        {
            return value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "");
        }
    }
}
