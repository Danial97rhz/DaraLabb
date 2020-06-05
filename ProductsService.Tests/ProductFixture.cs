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
        public object JsonSerialize { get; private set; }

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
                        Price = 0,
                        ImgUrl = "https://testpicture.com",
                        InStock = false,
                        CategoryId = Guid.Parse("2560a3f2-bd7d-42d2-89d2-946c9dab43b1")
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

        public async void Dispose()
        {
            using (var client = new TestClientProvider().Client)
            {
                var deleteResponse = await client.DeleteAsync($"/api/product/delete?id={product.Id}");
            }
        }
    }
}
