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
        [Fact]
        public void Should_Save_Valid_Todo_item()
        {
            // arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDatabase")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                context.Todo.RemoveRange(); // remove all items from database
                NewToDoItemModel page = new NewToDoItemModel(context);
                page.ToDoItem = ToDoItemFixture.ToDoItemTestFixture();
                page.OnPostAsync();
            }
            
            using (var context = new DatabaseContext(options))
            {
                Assert.Equal(1, context.Todo.Count());
            }
        }
        
        [Fact]
        public void Should_Not_Save_Invalid_Todo_item_Implementation_swap()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("inMemoryDatabase").Options;

            using (var context = new DatabaseContext(options))
            {
                NewToDoItemModel page = new NewToDoItemModel(context);
                context.Todo.RemoveRange(); // remove all data from database
                page.OnPostAsync();
                Assert.Equal(0, context.Todo.Count());
            }
        }
        
        [Fact]
        public void Should_Not_Save_Invalid_Todo_item()
        {
            // arrange
            var dbContext = new Mock<DatabaseContext>();
            var dbSet = new Mock<DbSet<Todo>>();
            dbContext.Setup(c => c.Todo).Returns(dbSet.Object);

            NewToDoItemModel page = new NewToDoItemModel(dbContext.Object);
            
            // act
            page.OnPostAsync();

            // assert
            dbContext.Verify(m => m.Todo.AddAsync(It.IsAny<Todo>(), new CancellationToken()), Times.Never());
            dbContext.Verify(m => m.SaveChangesAsync(new CancellationToken()), Times.Never);
        }
    }
}