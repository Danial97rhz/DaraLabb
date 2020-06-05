﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsService.Models;

namespace ProductsService.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult<Product> GetAll()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("id")]
        public ActionResult<Product> GetById(Guid id)
        {
            var product = _productRepository.GetById(id);
            return Ok(product);
        }

        [HttpGet]
        public ActionResult<Category> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        public ActionResult<Product> Create([FromBody] Product product)
        {
            var createdProduct = _productRepository.Create(product);
            return Ok(createdProduct);
        }

        [HttpDelete]
        public ActionResult<Product> Delete(Guid id)
        {
            var deletedProduct = _productRepository.Delete(id);
            return Ok(id);
        }
    }
}