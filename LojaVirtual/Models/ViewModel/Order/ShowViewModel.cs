using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models.ViewModel.Order
{
    public class ShowViewModel
    {
        public Models.Order Order { get; set; }


        public NFE NFE { get; set; }

        
        public TrackingCod TrackingCod { get; set; }


        public CancelDataCreditCard CreditCard { get; set; }


        public CancelDataBoleto BoletoBancario { get; set; }


        public DevolutionData Devolution { get; set; }
    }
}
