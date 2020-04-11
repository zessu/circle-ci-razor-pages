using Microsoft.EntityFrameworkCore;
using circle_ci_asp_net_razor_pages.Models;

namespace circle_ci_asp_net_razor_pages.Data
{

  public class DatabaseContext : DbContext
  {

    public virtual DbSet<Todo> Todo { get; set;  } 
  }
}
