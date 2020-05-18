//using DaraLabb.Web.Models;
//using DaraLabb.Web.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DaraLabb.Web.Components
//{
//    public class CartDropDown : ViewComponent
//    {
//        private readonly IProductRepository _productRepository;

//        public CartDropDown(IProductRepository productRepository)
//        {
//            _productRepository = productRepository;
//        }

//        public IViewComponentResult Invoke()
//        {
//            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
//            var cartIds = cart.Value.Split(',');
//            var products = _productRepository.GetAll();

//            var vm = new CartViewModel();
//            vm.Products = new List<CartItem>();

//            foreach (string id in cartIds)
//            {
//                var guid = Guid.Parse(id);

//                if (vm.Products.Count > 0 && vm.Products.Any(x => x.Product?.Id == guid))
//                {
//                    int productIndex = vm.Products.FindIndex(x => x.Product.Id == guid);
//                    vm.Products[productIndex].Quantity += 1;
//                }
//                else
//                {
//                    var p = _productRepository.GetById(guid);
//                    if (p != null)
//                    {
//                        vm.Products.Add(new CartItem() { Quantity = 1, Product = p });
//                    }
//                }
//            }
//            vm.TotalPrice = vm.Products.Sum(x => x.Product.Price * x.Quantity);

//            return View(vm);
//        }
//    }
//}
