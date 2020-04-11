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

namespace circle_ci_razor_pages.tests
{
    public class UnitTest1
    {
        [Fact]
        public void NewToDoItemPageSavesValidItem()
        {
            // arrange
            var dbContext = new Mock<DatabaseContext>();
            var dbSet = new Mock<DbSet<Todo>>();
            dbContext.Setup(c => c.Todo).Returns(dbSet.Object);

            NewToDoItemModel page = new NewToDoItemModel(dbContext.Object);
            page.ToDoItem = ToDoItemFixture.ToDoItemTestFixture();
            
            // act
            page.OnPostAsync();

            // assert
            dbContext.Verify(m => m.Todo.AddAsync(It.IsAny<Todo>(), new CancellationToken()), Times.Once());
            dbContext.Verify(m => m.SaveChangesAsync(new CancellationToken()), Times.Once());
        }
        
        [Fact]
        public void NewToDoItemPageDoesNotSaveInvalidItem()
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