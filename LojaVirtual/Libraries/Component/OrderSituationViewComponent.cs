using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{
    public class OrderSituationViewComponent : ViewComponent
    {
        List<OrderSituationStatus> timeline1 {
            get 
            {
                var list = new List<OrderSituationStatus>();

                list.Add(new OrderSituationStatus() { Situation = OrderSituationConst.AGUARDANDO_PAGAMENTO, Concluded = false });
                list.Add(new OrderSituationStatus() { Situation = OrderSituationConst.PAGAMENTO_APROVADO, Concluded = false });
                list.Add(new OrderSituationStatus() { Situation = OrderSituationConst.NF_EMITIDA, Concluded = false });
                list.Add(new OrderSituationStatus() { Situation = OrderSituationConst.EM_TRANSPORTE, Concluded = false });
                list.Add(new OrderSituationStatus() { Situation = OrderSituationConst.ENTREGUE, Concluded = false });
                list.Add(new OrderSituationStatus() { Situation = OrderSituationConst.FINALIZADO, Concluded = false });
                
                return list;
            } 
        }

        public OrderSituationViewComponent()
        {
        }


        public async Task<IViewComponentResult> InvokeAsync(Order order)
        {
            var listStatusTimeline1 = new List<string>()
            {
                OrderSituationConst.AGUARDANDO_PAGAMENTO,
                OrderSituationConst.PAGAMENTO_APROVADO,
                OrderSituationConst.NF_EMITIDA,
                OrderSituationConst.EM_TRANSPORTE,
                OrderSituationConst.ENTREGUE,
                OrderSituationConst.FINALIZADO
            };

            if (listStatusTimeline1.Contains(order.Situation))
            {
                foreach (var orderSituation in order.OrderSituations)
                {
                    var orderSituationTimeline = timeline1.Where(a => a.Situation == orderSituation.Situation).FirstOrDefault();

                    orderSituationTimeline.Date = orderSituation.Date;
                    orderSituationTimeline.Concluded = true;
                }
            }

            return View();
        }
    }
}
