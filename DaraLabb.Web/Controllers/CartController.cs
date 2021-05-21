﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DaraLabb.Web.Models;
using DaraLabb.Web.Services;
using DaraLabb.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace DaraLabb.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly string _productApiRoot;
        private readonly string _orderApiRoot;
        private readonly IConfiguration _config;
        

        private readonly IProductRepository _productRepository;
        private readonly UserManager<User> _userManager;
        private readonly IOrderRepository _orderRepository;
        public CartController(IProductRepository productRepository, UserManager<User> userManager, IOrderRepository orderRepository, IConfiguration config)
        {
            _productRepository = productRepository;
            _userManager = userManager;
            _orderRepository = orderRepository;
            _config = config;
            _productApiRoot = _config.GetValue(typeof(string), "ProductApiRoot").ToString();
            _orderApiRoot = _config.GetValue(typeof(string), "OrderApiRoot").ToString();
        }
        public async Task<IActionResult> List()
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            var vm = new CartViewModel();
            var apikey = _config.GetValue<string>("ApiKeys:ProductApiKey");

            if (cart.Value != null)
            {
                var cartIds = cart.Value.Split(',');
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
                }

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
                        var p = products.Where(x => x.Id == guid).First();
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
            var apikey = _config.GetValue<string>("ApiKeys:OrderApiKey");

            foreach (var cartItem in Cart.Products)
            {
                order.OrderRows.Add((OrderRow)cartItem);
            }
            foreach (var item in order.OrderRows)
            {
                order.ItemCount += item.Quantity;
            }
            vm.Order = order;
            vm.User = await _userManager.GetUserAsync(User);

            order.Address = vm.User.StreetAddress + ", " + vm.User.ZipCode + ", " + vm.User.City + ", " + vm.User.State;

            //_orderRepository.addToOrderHirtory(order);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_orderApiRoot);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("ApiKey", apikey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PostAsync("create", content);
            }

            Response.Cookies.Delete("cart");

            return View("OrderSuccess", vm);
        }

        public async Task<IActionResult> OrderHistoryAsync()
        {
            //var orders = _orderRepository.GetAll();

            //return View(orders);

            var orders = new List<Order>();
            var apikey = _config.GetValue<string>("ApiKeys:OrderApiKey");

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(_orderApiRoot);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Add("ApiKey", apikey);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("getall");

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;

                    orders = JsonConvert.DeserializeObject<List<Order>>(response);
                }

                return View(orders);

            }

        }
    }
}