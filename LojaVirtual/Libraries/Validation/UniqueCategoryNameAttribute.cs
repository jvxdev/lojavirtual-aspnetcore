using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Validation
{
    public class UniqueCategoryNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ICategoryRepository _categoryRepository = (ICategoryRepository)validationContext.GetService(typeof(ICategoryRepository));

            Category category = (Category) validationContext.ObjectInstance;

            if (category.Id == 0)
            {
                Category categoryDB = _categoryRepository.ReadCategoryName(category.Name);

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
                Category categoryDB = _categoryRepository.ReadCategoryName(category.Name);

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

            return base.IsValid(value, validationContext);
        }
    }
}
