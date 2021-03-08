using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{
    public class MenuViewComponent : ViewComponent
    {
        private ICategoryRepository _categoryRepository;


        public MenuViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listCategory = _categoryRepository.ReadAll().ToList();

            return View(listCategory);
        }
    }
}
