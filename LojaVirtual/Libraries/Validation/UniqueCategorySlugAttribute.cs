using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Libraries.Validation
{
    public class UniqueCategorySlugAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ICategoryRepository _categoryRepository = (ICategoryRepository)validationContext.GetService(typeof(ICategoryRepository));

            Category category = (Category)validationContext.ObjectInstance;

            if (category.Id == 0)
            {
                Category categoryDB = _categoryRepository.Read(category.Slug);

                if (categoryDB == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            else
            {
                Category categoryDB = _categoryRepository.Read(category.Slug);

                if (categoryDB == null)
                {
                    return ValidationResult.Success;
                }
                else if (categoryDB.Id == category.Id)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
        }
    }
}
