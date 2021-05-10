namespace LojaVirtual.Models.Const
{
    public class CorreiosConst
    {
        public const string SEDEX = "04014";
        public const string SEDEX10 = "04790";
        public const string PAC = "04510";

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
