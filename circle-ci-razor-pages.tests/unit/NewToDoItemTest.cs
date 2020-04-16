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
using Microsoft.EntityFrameworkCore.Storage;

namespace circle_ci_razor_pages.tests
{
  public class NewTodoItemPage
  {
    private readonly DbContextOptions<DatabaseContext> _options;
    private readonly DatabaseContext _context;

    public NewTodoItemPage()
    {
      _options = new DbContextOptionsBuilder<DatabaseContext>()
        .UseInMemoryDatabase(databaseName: "inMemoryTestDatabase", new InMemoryDatabaseRoot())
        .Options;
      _context = new DatabaseContext(_options);
    }

    public void Dispose()
    {
      _context.Todo.RemoveRange();
      _context.Database.EnsureDeleted();
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
    public void Should_Not_Save_Invalid_Todo_Item()
    {
        NewToDoItemModel page = new NewToDoItemModel(_context);
        page.ToDoItem = null;
        page.OnPostAsync();
        Assert.Equal(0, _context.Todo.Count());
    }
  }
}
