using System;
using System.Threading;
using System.Threading.Tasks;
using circle_ci_asp_net_razor_pages.Data;
using Xunit;
using Moq;
using circle_ci_asp_net_razor_pages.Pages;
using circle_ci_asp_net_razor_pages.Models;
using circle_ci_razor_pages.tests.Fixtures;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace circle_ci_razor_pages.tests
{
  public class NewTodoItemPage
  {
    private DbContextOptions<DatabaseContext> _options;
    private DatabaseContext _context;

    public NewTodoItemPage()
    {
      _options = new DbContextOptionsBuilder<DatabaseContext>()
        .UseInMemoryDatabase(databaseName: "inMemoryTestDatabase")
        .Options;
      _context = new DatabaseContext(_options);
      _context.Todo.RemoveRange(); // remove all data from database
    }

    public void Dispose()
    {
      _context.Dispose();
    }
    
    [Fact]
    public void Should_Save_Valid_Todo_item()
    {
      // arrange
        NewToDoItemModel page = new NewToDoItemModel(_context);
        page.ToDoItem = ToDoItemFixture.ToDoItemTestFixture();
        page.OnPostAsync();
        Assert.Equal(1, _context.Todo.Count());
    }

    [Fact]
    public void Should_Not_Save_Invalid_Todo_item_Implementation_swap()
    {
        NewToDoItemModel page = new NewToDoItemModel(_context);
        page.ToDoItem = null;
        page.OnPostAsync();
        Assert.Equal(0, _context.Todo.Count());
    }
  }
}
