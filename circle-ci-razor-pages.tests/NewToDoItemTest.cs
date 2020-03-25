using Xunit;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using circle_ci_razor_pages.tests.Utilities;
using System.Collections.Generic;
using AngleSharp.Dom;


namespace circle_ci_razor_pages.tests
{
    public class NewToDoItemTest :
        IClassFixture<CustomWebApplicationFactory<circle_ci_asp_net_razor_pages.Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<circle_ci_asp_net_razor_pages.Startup> _factory;

        public NewToDoItemTest(CustomWebApplicationFactory<circle_ci_asp_net_razor_pages.Startup> Factory)
        {
            _factory = Factory;
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false, // dont redirect, instead status code to test
            });
        }

        [Fact]
        public async Task ShouldReturn_CorrectResponse_OnPageLoad()
        {
            // Arrange 
            HttpResponseMessage NewToDoItemPage = await _httpClient.GetAsync("/new");
            Assert.Equal(HttpStatusCode.OK, NewToDoItemPage.StatusCode);
        }


        [Fact]
        public async Task ShouldReturn_CorrectResponse_OnFormSubmit()
        {
            // Arrange 
            var NewToDoItemPage = await _httpClient.GetAsync("/new");
            NewToDoItemPage.EnsureSuccessStatusCode();
            var doc = await HtmlHelpers.GetDocumentAsync(NewToDoItemPage); // create Browsing Context

            var form = doc.QuerySelector<IHtmlFormElement>("form[id='addItem']");
            var submitButton = doc.QuerySelector<IHtmlFormElement>("button[id='addItemButton']");

            Dictionary<string, string> formData = new Dictionary<string, string>
            {
                {"ToDoItem.name", "itemname"},
                {"ToDoItem.description", "itemdescription"}
            };

            // Act
            var response = await _httpClient.SendAsync(
              form,
              submitButton,
              formData
            );

            var res2 = await form.SubmitAsync(formData);
            var postContent = await HtmlHelpers.GetDocumentAsync(response);

            // Assert
            Assert.Equal(1, doc.Forms.Length);
            Assert.Equal(HttpStatusCode.OK, NewToDoItemPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/", response.Headers.Location.OriginalString);
        }

        // public async void Should_ReturnError_OnInValidFormData() { }

    }
}
