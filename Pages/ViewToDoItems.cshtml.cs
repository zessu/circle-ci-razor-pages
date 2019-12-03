using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using circle_ci_asp_net_razor_pages.Data;
using circle_ci_asp_net_razor_pages.Models;
using System.Linq;

namespace circle_ci_asp_net_razor_pages.Pages
{
  public class ViewToDoItemsModel : PageModel
  {

    public List<Todo> ToDoItems { get; set; }
    public DatabaseContext Context { get; }

    public ViewToDoItemsModel(DatabaseContext context)
    {
      Context = context;
    }
    public void OnGet()
    {
      ToDoItems = Context.Todo.ToList();
    }
  }
}
