namespace LojaVirtual.Models.Const
{
    public class PaymentMethodConst
    {
        public const string CreditCard = "Cartão de crédito";
        public const string Boleto = "Boleto bancário";

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
