using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Collaborator
    {
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E005")]
        [MinLength(4, ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E002")]
        public string Name { get; set; }


        [Display(Name = "E-mail")]
        [Required(ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E005")]
        [MinLength(4, ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E002")]
        public string Email { get; set; }
        
        
        [Display(Name = "Senha")]
        [Required(ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E005")]
        [MinLength(4, ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E002")]
        public string Password { get; set; }

        [Display(Name = "Confirmação de senha")]
        [NotMapped]
        [Required(ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E005")]
        [Compare("Password", ErrorMessageResourceType = typeof(Menssage), ErrorMessageResourceName = "MSG_E006")]
        public string PasswordConfirmation { get; set;}


        [Display(Name = "Cargo")]
        public string Position { get; set; }
    }
}
