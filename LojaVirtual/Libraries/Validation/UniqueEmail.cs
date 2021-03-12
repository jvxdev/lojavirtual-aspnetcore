using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Libraries.Validation
{
    public class UniqueEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string Email = (value as string).Trim();

            ICollaboratorRepository _collaboratorRepository = (ICollaboratorRepository)validationContext.GetService(typeof(ICollaboratorRepository));

            List<Collaborator> collaborators = _collaboratorRepository.GetCollaboratorEmail(Email);

            Collaborator objCollaborator = (Collaborator)validationContext.ObjectInstance;

            if (collaborators.Count > 1)
            {
                return new ValidationResult("Este e-mail já está cadastrado!");
            }

            if (collaborators.Count == 1 && objCollaborator.Id != collaborators[0].Id)
            {
                return new ValidationResult("Este e-mail já está cadastrado!");
            }

            return ValidationResult.Success;
        }
    }
}
