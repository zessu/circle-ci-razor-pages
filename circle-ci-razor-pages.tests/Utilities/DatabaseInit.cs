using circle_ci_asp_net_razor_pages.Data;
using Microsoft.EntityFrameworkCore;
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
                new Todo() {name = "You're.", description = "mind"},
                new Todo() {name = "Would", description = "energy"},
                new Todo() {name = "Rational", description = "soul"}
            };
        }
        
        public static void PurgeDatabase(DatabaseContext context)
        {
            context.Todo.RemoveRange();
            context.SaveChanges();
        }

    }
}