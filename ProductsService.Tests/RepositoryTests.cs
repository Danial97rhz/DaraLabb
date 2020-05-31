using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProductsService.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void GetProductById_Returns_Product()
        {
            var productRepository = new ProductRepository();
            var product = productRepository.GetById(Guid.Empty);

            Assert.Equal(Guid.Empty, product.Id);
        }

        [Fact]
        public void GetAllProducts_Returns_AllProducts()
        {
            var productRepository = new ProductRepository();
            var product = productRepository.GetAll();

            
        }
    }
}
