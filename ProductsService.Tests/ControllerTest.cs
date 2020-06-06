using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.Tests
{
    public class ControllerTest : IClassFixture<ProductFixture>
    {
        ProductFixture _fixture;

        public ControllerTest(ProductFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllProducts_Returns_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/product/getall");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task GetAllCategories_Returns_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/product/getallcategories");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetProductById_Returns_Product()
        {
            using (var client = new TestClientProvider().Client)
            {
                var productResponse = await client.GetAsync($"/api/product/getbyid/{_fixture.product.Id}");

                using (var responseStream = await productResponse.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.product.Id, product.Id);
                }

                //var productsResponse = await client.GetAsync("/api/product/getall");
                //IEnumerable<Product> products;
                //Guid testId;

                //using(var response = await productsResponse.Content.ReadAsStreamAsync())
                //{
                //    products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(response,
                //        new JsonSerializerOptions());
                //}

                //testId = products.ToList()[0].Id;


                //var productResponse = await client.GetAsync($"/api/product/getbyid?id={testId}");
                //Product product;
                //Guid responseId;

                //using (var response = await productsResponse.Content.ReadAsStreamAsync())
                //{
                //    product = await JsonSerializer.DeserializeAsync<Product>(response,
                //        new JsonSerializerOptions());
                //}

                //responseId = product.Id;
            }
        }
    }
}
