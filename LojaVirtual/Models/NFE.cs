using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class NFE
    {
        [Url(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E004")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string NFE_Url { get; set; }
    }
}
