using System;
using Xunit;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using circle_ci_razor_pages.tests.Utilities;
using System.Collections.Generic;
using AngleSharp.Dom;
using circle_ci_asp_net_razor_pages.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace circle_ci_razor_pages.tests
{
    public class ViewToDoItemsTest :
        IClassFixture<CustomWebApplicationFactory<circle_ci_asp_net_razor_pages.Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<circle_ci_asp_net_razor_pages.Startup> _factory;

        public ViewToDoItemsTest(CustomWebApplicationFactory<circle_ci_asp_net_razor_pages.Startup> Factory)
        {
            _factory = Factory;
            _httpClient = _factory.WithWebHostBuilder(builder => { builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices
                        .GetRequiredService<DatabaseContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<IndexPageTests>>();

                    try
                    {
                        DatabaseInit.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding " +
                                            "the database with test messages. Error: {Message}",
                            ex.Message);
                    }
                }
            }); }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false, // dont redirect, instead status code to test
            });
        }

        [Fact]
        public async Task ShouldReturn_CorrectResponse_OnPageLoad()
        {
            // Arrange 
            HttpResponseMessage ViewToDoItemsPage = await _httpClient.GetAsync("/all");
            Assert.Equal(HttpStatusCode.OK, ViewToDoItemsPage.StatusCode);
        }


        [Fact]
        public async Task ShouldLoadItemsIntoPage()
        {
            // Arrange 
            var ViewToDoItemsPage = await _httpClient.GetAsync("/all");
            ViewToDoItemsPage.EnsureSuccessStatusCode();
            var doc = await HtmlHelpers.GetDocumentAsync(ViewToDoItemsPage); // create Browsing Context

            Dictionary<string, string> formData = new Dictionary<string, string>
            {
                {"name", "itemname"},
                {"description", "itemdescription"}
            };
            
            // Act
            var response = await _httpClient.SendAsync(
                (IHtmlFormElement)doc.QuerySelector("form[id='addItem']"),
                (IHtmlButtonElement)doc.QuerySelector("button[id='addItemButton']"),
                formData
            );

            var postContent = await HtmlHelpers.GetDocumentAsync(response);

            // Assert
            Assert.Equal(1, doc.Forms.Length);
            Assert.Equal(HttpStatusCode.OK, ViewToDoItemsPage.StatusCode);
            Console.WriteLine(response);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
