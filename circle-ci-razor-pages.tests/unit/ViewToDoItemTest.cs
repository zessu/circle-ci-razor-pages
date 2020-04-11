using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using circle_ci_asp_net_razor_pages.Pages;
using circle_ci_asp_net_razor_pages.Data;
using circle_ci_asp_net_razor_pages.Models;
using circle_ci_razor_pages.tests.Fixtures;
using System.Collections.Generic;
using System.Linq;

namespace circle_ci_razor_pages.tests
{
    public class ViewToDoItemPage
    {

        [Fact]
        public void Should_Load_Items_From_DB_on_Page_Load()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDatabase")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                context.Todo.AddRange(Fixtures.ToDoItemFixture.ToDoItemListFixture());
                context.SaveChanges();
                ViewToDoItemsModel viewTodoItemsModel = new ViewToDoItemsModel(context);
                viewTodoItemsModel.OnGet();
                Assert.NotNull(viewTodoItemsModel.ToDoItems);
            }
        }
    }
}