namespace LojaVirtual.Models.Const
{
    public class OrderSituationConst
    {
        public const string AGUARDANDO_PAGAMENTO = "04014";
        public const string PAGAMENTO_APROVADO = "04790";
        public const string PAGAMENTO_REJEITADO = "04510";
        public const string NF_EMITIDA = "04014";
        public const string EM_TRANSPORTE = "04790";
        public const string ENTREGUE = "04510";
        public const string FINALIZADO = "04510";
        public const string EM_CANCELAMENTO = "04510";
        public const string EM_ANALISE = "04510";
        public const string CANCELAMENTO_APROVADO = "04510";
        public const string CANCELAMENTO_REJEITADO = "04510";
        public const string ESTORNO = "04510";

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
