using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Test
{
    public class OrderFixture
    {
        public Order order { get; private set; }

        public OrderFixture()
        {
            order = Initialize().Result;
        }

        private async Task<Order> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(
                    new Order
                    {
                        Date= DateTime.Now
                    }
                    );

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/order/create", content);

                using (var responseStreame = await response.Content.ReadAsStreamAsync())
                {
                    var createdOrder = await JsonSerializer.DeserializeAsync<Order>(responseStreame,
                        new JsonSerializerOptions());
                    return createdOrder;
                }
            }
        }

    }
}
