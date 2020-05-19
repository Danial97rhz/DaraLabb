using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaraLabb.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaraLabb.Web.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        //private readonly IProductRepository _productRepository;
        //public CartController(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}

        //static int itemCounter = 0;
        [HttpGet]
        public IActionResult AddToCart(Guid id)
        {
            var cart = Request.Cookies.SingleOrDefault(x => x.Key == "cart");
            var cartContent = "";
            
            if (cart.Value != null)
            {
                /*itemCounter++*/;
                cartContent = cart.Value;
                cartContent += "," + id;
            }
            else { /*itemCounter++*/; cartContent += id; }

            Response.Cookies.Append("cart", cartContent);

            //TODO: how to count guid ??
            var itemCounter = cartContent.Replace(",", "").Count() / Guid.NewGuid().ToString().Count();


            return Ok(itemCounter);
        }

      
        [HttpGet]
        public int CartCount()
        {
            var cart = Request.Cookies.SingleOrDefault(x => x.Key == "cart");
            var cartContent = cart.Value;
            var itemCounter = 0;

            if (cartContent != null)
            {
                itemCounter = cartContent.Replace(",", "").Count() / Guid.NewGuid().ToString().Count();
            }

            return itemCounter;
        }

    }
}