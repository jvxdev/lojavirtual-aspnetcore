using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.Models
{
    public class Collaborator
    {
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [MinLength(4, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        public string Name { get; set; }


        [Display(Name = "E-mail")]
        [UniqueEmail]
        public string Email { get; set; }


        [Display(Name = "Senha")]
        public string Password { get; set; }


        [Display(Name = "Confirmação de senha")]
        [NotMapped]
        public string PasswordConfirmation { get; set; }


        [Display(Name = "Cargo")]
        public string Position { get; set; }
    }
}
