using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;

namespace circle_ci_razor_pages.tests
{
    public class BasicTests 
        : IClassFixture<WebApplicationFactory<circle_ci_asp_net_razor_pages.Startup>>
    {
        private readonly WebApplicationFactory<circle_ci_asp_net_razor_pages.Startup> _factory;

        public BasicTests(WebApplicationFactory<circle_ci_asp_net_razor_pages.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/About")]
        [InlineData("/Privacy")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}