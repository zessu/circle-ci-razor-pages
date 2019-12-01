using System.ComponentModel.DataAnnotations;
using System;

namespace circle_ci_asp_net_razor_pages.Models
{
  public class Todo
  {
    public Guid Id { get; set; }
    [MinLength(10)]
    [Required]
    public string description { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; }
    [DataType(DataType.Date)]
    public DateTime UpdatedAt { get; set; }

  }
}
