using Microsoft.EntityFrameworkCore;
using circle_ci_asp_net_razor_pages.Models;

namespace circle_ci_asp_net_razor_pages.Data
{

  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<Todo> Todo { get; }
  }
}
