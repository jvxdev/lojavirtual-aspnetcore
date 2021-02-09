using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LojaVirtual.Libraries.Lang;

namespace LojaVirtual.Models
{
    public class Contact
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E002")]
        public string Name { get; set; }


        [Required(ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }


        [Display(Name = "Mensagem")]
        [Required(ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(10, ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(1000, ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E003")]
        public string Menssage { get; set; }
    }
}
