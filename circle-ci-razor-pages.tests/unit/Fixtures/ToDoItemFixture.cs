using circle_ci_asp_net_razor_pages.Models;
using System;
using System.Collections.Generic;
    
namespace circle_ci_razor_pages.tests.Fixtures
{
    public static class ToDoItemFixture
    {
        public static Todo ToDoItemTestFixture()
        {
            return new Todo
            {
                description = "sdsd",
                name = "sd",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
        
        public static List<Todo> ToDoItemListFixture()
        {
            return new List<Todo>
            {
                new Todo {
                    description = "sdsd", 
                    name = "sd",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Todo {
                    description = "another", 
                    name = "sks",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
            };
        }
    }
}