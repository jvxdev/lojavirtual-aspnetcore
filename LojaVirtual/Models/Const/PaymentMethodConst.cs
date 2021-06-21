using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models.Const
{
    public class PaymentMethodConst
    {
        public const string CreditCard = "Cartão de Crédito";
        public const string Boleto = "Boleto Bancário";

        public static string GetName(string code)
        {
            foreach (var field in typeof(CorreiosConst).GetFields())
            {
                if ((string)field.GetValue(null) == code)
                    return field.Name.ToString();
            }
            return "";
        }
    }
}