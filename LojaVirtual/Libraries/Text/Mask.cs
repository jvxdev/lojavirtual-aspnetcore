namespace LojaVirtual.Libraries.Text
{
    public class Mask
    {
        public static string Delete(string value)
        {
            return value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace("R$", "").Replace(",", "").Replace(" ", "");
        }


        public static int ConvertValuePagarMe(decimal value)
        {
            string stringValue = value.ToString("C");
            stringValue = Delete(stringValue);
            int intValue = int.Parse(stringValue);

            return intValue;
        }


        public static decimal ConvertPagarMeIntToDecimal(int value)
        {
            string valuePagarMeString = value.ToString();
            string valueDecimalString = valuePagarMeString.Substring(0, valuePagarMeString.Length - 2) + "," + valuePagarMeString.Substring(valuePagarMeString.Length - 2);

            var dec = decimal.Parse(valueDecimalString);

            return dec;
        }


        public static int ExtractCodOrder(string codOrder, out string transactionId)
        {
            string[] resultSplit = codOrder.Split("-");

            transactionId = resultSplit[1];

            return int.Parse(resultSplit[0]);
        }
    }
}
