using DaraLabb.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraLabb.Web.Components
{
    public class CategoryDropDown : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryDropDown(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.GetAll;
            return View(categories);
        }
    }
}
