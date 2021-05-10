using LojaVirtual.Libraries.Lang;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Models
{
    public class CreditCard
    {
        [Display(Name = "Número do cartão")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [CreditCard(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E004")]
        [MinLength(15, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E003")]
        public string CardNumber { get; set; }


        [Display(Name = "Nome impresso no cartão")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string CardHolderName { get; set; }


        [Display(Name = "Mês do vencimento")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string CardExpirationDateMM { get; set; }


        [Display(Name = "Ano do vencimento")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string CardExpirationDateYY { get; set; }


        [Display(Name = "CVC/CVV")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        public string CardCvv { get; set; }
    }
}
