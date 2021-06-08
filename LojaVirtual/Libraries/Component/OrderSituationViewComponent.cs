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
        List<OrderSituationStatus> timeline1 { get; set; }

        public OrderSituationViewComponent()
        {
            timeline1 = new List<OrderSituationStatus>();

            timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.PEDIDO_REALIZADO, Concluded = false });
            timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.PAGAMENTO_APROVADO, Concluded = false });
            timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.NF_EMITIDA, Concluded = false });
            timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.EM_TRANSPORTE, Concluded = false });
            timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.ENTREGUE, Concluded = false });
            timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.FINALIZADO, Concluded = false });
        }


        public async Task<IViewComponentResult> InvokeAsync(Order order)
        {
            List<OrderSituationStatus> timeline = null;

            var listStatusTimeline1 = new List<string>()
            {
                OrderSituationConst.PEDIDO_REALIZADO,
                OrderSituationConst.PAGAMENTO_APROVADO,
                OrderSituationConst.NF_EMITIDA,
                OrderSituationConst.EM_TRANSPORTE,
                OrderSituationConst.ENTREGUE,
                OrderSituationConst.FINALIZADO
            };

            if (listStatusTimeline1.Contains(order.Situation))
            {
                timeline = timeline1;

                foreach (var orderSituation in order.OrderSituations)
                {
                    var orderSituationTimeline = timeline1.Where(a => a.Situation == orderSituation.Situation).FirstOrDefault();

                    orderSituationTimeline.Date = orderSituation.Date;
                    orderSituationTimeline.Concluded = true;
                }
            }

            return View(timeline);
        }
    }
}
