using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using circle_ci_asp_net_razor_pages.Pages;
using circle_ci_asp_net_razor_pages.Data;
using circle_ci_asp_net_razor_pages.Models;
using circle_ci_razor_pages.tests.Fixtures;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace circle_ci_razor_pages.tests
{
    public class ViewToDoItemPage
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        public ViewToDoItemPage()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDatabase", new InMemoryDatabaseRoot())
                .Options;
            _context = new DatabaseContext(_options);
        }

        public void Dispose()
        {
            _context.Todo.RemoveRange(); // remove all data from database
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void Should_Load_Items_From_DB_on_Page_Load()
        {
                _context.Todo.AddRange(Fixtures.ToDoItemFixture.ToDoItemListFixture());
                _context.SaveChanges();
                ViewToDoItemsModel viewTodoItemsModel = new ViewToDoItemsModel(_context);
                viewTodoItemsModel.OnGet();
                Assert.NotNull(viewTodoItemsModel.ToDoItems);
        }
    }
}