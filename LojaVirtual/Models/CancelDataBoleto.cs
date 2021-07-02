using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class CancelDataBoleto
    {
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string CancelReason { get; set; }


        public string PaymentForm { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [MaxLength(3, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E003")]
        [MinLength(3, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        public string Bank_code { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string Agency { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string Agency_dv { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string Account { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string Account_dv { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [MinLength(4, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        public string Legal_name { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string CPF { get; set; }
    }
}