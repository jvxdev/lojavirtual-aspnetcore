using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class CancelData
    {
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string CancelReason { get; set; }


        public string PaymentForm { get; set; }


        public string Bank_code { get; set; }


        public string Agency { get; set; }


        public string Agency_dv { get; set; }


        public string Account { get; set; }


        public string Account_dv { get; set; }


        [MinLength(4, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        public string Legal_name { get; set; }


        public string CPF { get; set; }
    }
}