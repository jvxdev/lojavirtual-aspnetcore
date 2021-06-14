using Coravel.Invocable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Manager.Scheduler.Invocable
{
    public class OrderPaymentSituationJob : IInvocable
    {
        public Task Invoke()
        {
            Debug.WriteLine("----- OrderPaymentSituationJob - Executado -----");

            return Task.CompletedTask;
        }
    }
}
