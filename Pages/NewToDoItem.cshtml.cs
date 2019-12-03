using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using circle_ci_asp_net_razor_pages.Models;

namespace circle_ci_asp_net_razor_pages.Pages
{
  public class NewToDoItemModel : PageModel
  {
    [BindProperty]
    public Todo ToDoItem { get; set; }
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }
      return RedirectToPage("ViewToDoItems");
    }
  }
}
