using System;
using System.Collections.Generic;
using System.Linq;
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
    public DatabaseContext _context { get; }

    public NewToDoItemModel(DatabaseContext context)
    {
      _context = context;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }
      await _context.Todo.AddAsync(ToDoItem);
      await _context.SaveChangesAsync();
      return RedirectToPage("ViewToDoItems");
    }
  }
}
