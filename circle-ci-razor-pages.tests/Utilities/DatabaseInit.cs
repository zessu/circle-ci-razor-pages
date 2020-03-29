using circle_ci_asp_net_razor_pages.Data;
using circle_ci_asp_net_razor_pages.Models;
using System.Collections.Generic;

namespace circle_ci_razor_pages.tests.Utilities
{
    public class DatabaseInit
    {

        public static void InitializeDbForTests(DatabaseContext context)
        {
            context.Todo.AddRange(GetSeedingTodos());
            context.SaveChanges();
        }

        public static List<Todo> GetSeedingTodos()
        {
            return new List<Todo>()
            {
                new Todo() {name = "collect stamps", description = "stamp for sending mail"},
                new Todo() {name = "research xd", description = "design course research"},
                new Todo() {name = "Rational", description = "do article on rational thinking"}
            };
        }

        public static void PurgeDatabase(DatabaseContext context)
        {
            context.Todo.RemoveRange(context.Todo); // remove the whole table
            context.SaveChanges();
        }
    }
}