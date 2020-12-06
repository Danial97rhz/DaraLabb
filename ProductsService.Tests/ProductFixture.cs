using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductsService.Tests
{
    public class ProductFixture
    {
        public Product product { get; private set; }

        public ProductFixture()
        {
            product = Initialize().Result;
        }

        private async Task<Product> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(
                    new Product
                    {
                        Name = "TestProduct",
                        Price = 99M,
                        ImgUrl = "https://testpicture.com",
                        InStock = false
                    }
                    );

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/product/create", content);

                using (var responseStreame = await response.Content.ReadAsStreamAsync())
                {
                    var createdProduct = await JsonSerializer.DeserializeAsync<Product>(responseStreame,
                        new JsonSerializerOptions());
                    return createdProduct;
                }
            }
        }

        //public async void Dispose()
        //{
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        var deleteResponse = await client.DeleteAsync($"/api/product/delete?id={product.Id}");
        //    }
        //}
    }
}
