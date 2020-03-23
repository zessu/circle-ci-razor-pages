using Xunit;
using circle_ci_asp_net_razor_pages;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using circle_ci_razor_pages.tests.Utilities;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp;
using circle_ci_asp_net_razor_pages.Models;

namespace circle_ci_razor_pages.tests
{
    public class NewToDoItemTest: 
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public NewToDoItemTest(CustomWebApplicationFactory<Startup> Factory)
        {
            _factory = Factory;
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false // dont redirect, instead status code to test
            });
        }

        [Fact]
        public async void ShouldReturn_CorrectResponse_OnPageLoad()
        {
            // Arrange 
            HttpResponseMessage NewToDoItemPage = await _httpClient.GetAsync("/new");
            Assert.Equal(HttpStatusCode.OK, NewToDoItemPage.StatusCode);
        }


        [Fact]
        public async void ShouldReturn_CorrectResponse_OnFormSubmit()
        {
            // Arrange 
            HttpResponseMessage NewToDoItemPage = await _httpClient.GetAsync("/new");
            NewToDoItemPage.EnsureSuccessStatusCode();
            IHtmlDocument doc = await HtmlHelpers.GetDocumentAsync(NewToDoItemPage); // create Browsing Context
            IHtmlFormElement form = doc.QuerySelector<IHtmlFormElement>("form[id='addItem']");

            Dictionary<string, string> submittedData = new Dictionary<string, string>();
            submittedData.Add("ToDoItem.name", "itemname");
            submittedData.Add("ToDoItem.description", "itemdescription");
            
            // Act
            await form.SubmitAsync(submittedData);

            // Assert
            Assert.Equal(HttpStatusCode.OK, NewToDoItemPage.StatusCode);
            // TODO check database to make sure we have one item
            // Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            // Assert.Equal("/new", response.Headers.Location.OriginalString);
        }
        
        public async void Should_ReturnError_OnInValidFormData() {}
        
    }
}