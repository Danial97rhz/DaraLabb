using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaraLabb.Web.Models;
using DaraLabb.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraLabb.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(string category)
        {
            ProductsListViewModel vm = new ProductsListViewModel();

            if (string.IsNullOrEmpty(category))
            {
                vm.products = _productRepository.GetAll();
                vm.Category = "All Products";
            }
            else
            {
                vm.products = _productRepository.GetAll().Where(x => x.Category.Name == category);
                vm.Category = category;
            }

            return View(vm);
        }

        [Authorize]
        public IActionResult AddToCart(Guid id)
        {
            var cart = Request.Cookies.SingleOrDefault(x => x.Key == "cart");
            var cartContent = "";
            if (cart.Value != null)
            {
                cartContent = cart.Value;
                cartContent += "," + id;
            }
            else cartContent += id;

            Response.Cookies.Append("cart", cartContent);

            return RedirectToAction("List", "Product");
        }
    }
}