using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DaraLabb.Web.Models;
using DaraLabb.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace DaraLabb.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly UserManager<User> _userManager;
        public CartController(IProductRepository productRepository, UserManager<User> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }
        public IActionResult List()
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            var vm = new CartViewModel();

            if (cart.Value != null)
            {
                var cartIds = cart.Value.Split(',');
                var products = _productRepository.GetAll();

                vm.Products = new List<CartItem>();

                foreach (string id in cartIds)
                {
                    var guid = Guid.Parse(id);

                    if (vm.Products.Count > 0 && vm.Products.Any(x => x.Product?.Id == guid))
                    {
                        int productIndex = vm.Products.FindIndex(x => x.Product.Id == guid);
                        vm.Products[productIndex].Quantity += 1;
                    }
                    else
                    {
                        var p = _productRepository.GetById(guid);
                        if (p != null)
                        {
                            vm.Products.Add(new CartItem() { Quantity = 1, Product = p });
                        }
                    }
                }
                vm.TotalPrice = vm.Products.Sum(x => x.Product.Price * x.Quantity);
            }

                return View(vm); 
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([Bind("TotalPrice,Products")]CartViewModel Cart)
        {
            OrderViewModel vm = new OrderViewModel();
            Order order = new Order();
            order.TotalPrice = Cart.TotalPrice;
            order.Date = DateTime.Now;
            order.UserId = Guid.Parse(_userManager.GetUserId(User));
            //order.OrderRows = vm.Products.Select(cartItem => new OrderRow(cartItem)).ToList();
            foreach (var cartItem in Cart.Products)
            {
                order.OrderRows.Add((OrderRow)cartItem);
            }
            vm.Order = order;
            vm.User = await _userManager.GetUserAsync(User);

            Response.Cookies.Delete("cart");

            return View("OrderSuccess", vm);
        }
    }
}