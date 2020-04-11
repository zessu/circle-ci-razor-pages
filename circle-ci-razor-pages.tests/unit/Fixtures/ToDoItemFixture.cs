using circle_ci_asp_net_razor_pages.Models;
using System;

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
    }
}