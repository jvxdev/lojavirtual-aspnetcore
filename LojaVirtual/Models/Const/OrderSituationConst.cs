namespace LojaVirtual.Models.Const
{
    public class OrderSituationConst
    {
        public const string PEDIDO_REALIZADO = "Pedido realizado";
        public const string PAGAMENTO_APROVADO = "Pagamento aprovado";
        public const string PAGAMENTO_REJEITADO = "Pagamento rejeitado";
        public const string PAGAMENTO_NAO_REALIZADO = "Pagamento não realizado (vencido)";
        public const string NF_EMITIDA = "Nota fiscal emitida";
        public const string EM_TRANSPORTE = "Em transporte";
        public const string ENTREGUE = "Entregue";
        public const string FINALIZADO = "Finalizado";
        public const string EM_CANCELAMENTO = "Em cancelamento";
        public const string EM_ANALISE = "Em análise";
        public const string CANCELAMENTO_APROVADO = "Cancelamento aprovado";
        public const string CANCELAMENTO_REJEITADO = "Cancelamento rejeitado";
        public const string ESTORNO = "Estorno";

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
