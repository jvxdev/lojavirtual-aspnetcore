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
        List<OrderSituationStatus> Timeline1 { get; set; }

        List<string> StatusTimeline1 = new List<string>() {
                OrderSituationConst.PEDIDO_REALIZADO,
                OrderSituationConst.PAGAMENTO_APROVADO,
                OrderSituationConst.NF_EMITIDA,
                OrderSituationConst.EM_TRANSPORTE,
                OrderSituationConst.ENTREGUE,
                OrderSituationConst.FINALIZADO
            };


        List<OrderSituationStatus> Timeline2 { get; set; }

        List<string> StatusTimeline2 = new List<string>() {
                OrderSituationConst.PAGAMENTO_NAO_REALIZADO
            };

        List<OrderSituationStatus> Timeline3 { get; set; }

        List<string> StatusTimeline3 = new List<string>() {
                OrderSituationConst.ESTORNO
            };


        public OrderSituationViewComponent()
        {
            Timeline1 = new List<OrderSituationStatus>();

            Timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.PEDIDO_REALIZADO, Concluded = false, Color = "complete" });
            Timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.PAGAMENTO_APROVADO, Concluded = false, Color = "complete" });
            Timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.NF_EMITIDA, Concluded = false, Color = "complete" });
            Timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.EM_TRANSPORTE, Concluded = false, Color = "complete" });
            Timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.ENTREGUE, Concluded = false, Color = "complete" });
            Timeline1.Add(new OrderSituationStatus() { Situation = OrderSituationConst.FINALIZADO, Concluded = false, Color = "complete" });

            Timeline2 = new List<OrderSituationStatus>();
            Timeline2.Add(new OrderSituationStatus() { Situation = OrderSituationConst.PEDIDO_REALIZADO, Concluded = false, Color = "complete" });
            Timeline2.Add(new OrderSituationStatus() { Situation = OrderSituationConst.PAGAMENTO_NAO_REALIZADO, Concluded = false, Color = "complete-red" });

            Timeline3 = new List<OrderSituationStatus>();
            Timeline3.Add(new OrderSituationStatus() { Situation = OrderSituationConst.PEDIDO_REALIZADO, Concluded = false, Color = "complete" });
            Timeline3.Add(new OrderSituationStatus() { Situation = OrderSituationConst.PAGAMENTO_APROVADO, Concluded = false, Color = "complete" });
            Timeline3.Add(new OrderSituationStatus() { Situation = OrderSituationConst.NF_EMITIDA, Concluded = false, Color = "complete" });
            Timeline3.Add(new OrderSituationStatus() { Situation = OrderSituationConst.ESTORNO, Concluded = false, Color = "complete-purple" });
        }


        public async Task<IViewComponentResult> InvokeAsync(Order order)
        {
            List<OrderSituationStatus> timeline = null;

            if (StatusTimeline1.Contains(order.Situation))
            {
                timeline = Timeline1;
            }

            if (StatusTimeline2.Contains(order.Situation))
            {
                timeline = Timeline2;
            }

            if (StatusTimeline3.Contains(order.Situation))
            {
                timeline = Timeline3;

                var nfe = order.OrderSituations.Where(a => a.Situation == OrderSituationConst.NF_EMITIDA).FirstOrDefault();

                if (nfe == null)
                {
                    timeline.Remove(timeline.FirstOrDefault(a => a.Situation == OrderSituationConst.NF_EMITIDA));
                }
            }

            if (timeline != null)
            {
                foreach (var orderSituation in order.OrderSituations)
                {
                    var orderSituationTimeline = timeline.Where(a => a.Situation == orderSituation.Situation).FirstOrDefault();

                    orderSituationTimeline.Date = orderSituation.Date;
                    orderSituationTimeline.Concluded = true;
                }
            }

            return View(timeline);
        }
    }
}