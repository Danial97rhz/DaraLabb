using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DaraLabb.Web.Models;
using DaraLabb.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DaraLabb.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly string _productApiRoot;
        private readonly IConfiguration _config;

        public ProductController(IConfiguration config)
        {
            _config = config;
            _productApiRoot = _config.GetValue(typeof(string), "ProductApiRoot").ToString();
        }

        //private readonly IProductRepository _productRepository;
        //private readonly ICategoryRepository _categoryRepository;

        //public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        //{
        //    _productRepository = productRepository;
        //    _categoryRepository = categoryRepository;
        //}

        public async Task<ActionResult> List(string category)
        {
            var apikey = _config.GetValue<string>("ApiKeys:ProductApiKey");
            var products = new List<Product>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(_productApiRoot);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Add("ApiKey", apikey);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("getall");

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;

                    products = JsonConvert.DeserializeObject<List<Product>>(response);
                }

                ProductsListViewModel vm = new ProductsListViewModel();

                vm.products = products;

                //if (string.IsNullOrEmpty(category))
                //{
                //    vm.products = products;
                //}
                //else
                //{
                //    vm.products = products.Where(x => x.Category.Name == category);
                //}

                return View(vm);
            }
        }

        //public ViewResult List(string category)
        //{
        //    ProductsListViewModel vm = new ProductsListViewModel();

        //    if (string.IsNullOrEmpty(category))
        //    {
        //        vm.products = _productRepository.GetAll();
        //    }
        //    else
        //    {
        //        vm.products = _productRepository.GetAll().Where(x => x.Category.Name == category);
        //    }

        //    return View(vm);
        //}

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