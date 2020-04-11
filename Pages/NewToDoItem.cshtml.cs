using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using circle_ci_asp_net_razor_pages.Models;
using circle_ci_asp_net_razor_pages.Data;

namespace circle_ci_asp_net_razor_pages.Pages
{
  public class NewToDoItemModel : PageModel
  {
    [BindProperty]
    public Todo ToDoItem { get; set; }

    private DatabaseContext Context { get; }
 
    public NewToDoItemModel(DatabaseContext context)
    {
      Context = context;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (ToDoItem == null || !ModelState.IsValid)
      {
        return Page();
      }
      Debug.WriteLine(ToDoItem);
      await Context.Todo.AddAsync(ToDoItem);
      await Context.SaveChangesAsync();
      return RedirectToPage("ViewToDoItems");
    }
  }
}