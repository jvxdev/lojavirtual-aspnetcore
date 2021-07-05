namespace LojaVirtual.Models.Const
{
    public class OrderSituationConst
    {
        public const string PEDIDO_REALIZADO = "Pedido realizado";
        public const string PAGAMENTO_APROVADO = "Pagamento aprovado";
        public const string PAGAMENTO_REJEITADO = "Pagamento rejeitado";
        public const string PAGAMENTO_NAO_REALIZADO = "Pagamento não realizado (Vencido)";
        
        public const string NF_EMITIDA = "Nota fiscal emitida";
        public const string EM_TRANSPORTE = "Em transporte";
        public const string ENTREGUE = "Entregue";
        public const string FINALIZADO = "Finalizado";
        public const string ESTORNO = "Estorno";

        public const string DEVOLUCAO = "Devolução (Em transporte)";
        public const string DEVOLUCAO_ENTREGUE = "Devolução (Entregue)";
        public const string DEVOLUCAO_APROVADA = "Devolução aprovada";
        public const string DEVOLUCAO_REJEITADA = "Devolução rejeitada";

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
