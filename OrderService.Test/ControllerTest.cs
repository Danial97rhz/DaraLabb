using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace OrderService.Test
{
    public class ControllerTest : IClassFixture<OrderFixture>
    {
        OrderFixture _fixture;

        public ControllerTest(OrderFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllOrders_Returns_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/order/getall");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetOrderById_Returns_Order()
        {
            using (var client = new TestClientProvider().Client)
            {
                var OrderResponse = await client.GetAsync($"/api/order/getbyid/{_fixture.order.Id}");

                using (var responseStream = await OrderResponse.Content.ReadAsStreamAsync())
                {
                    var order = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.order.Id, order.Id);
                }

            }
        }
    }
}
