using System.ComponentModel.DataAnnotations;
using System;

namespace circle_ci_asp_net_razor_pages.Models
{
  public class Todo
  {
    public Guid Id { get; set; }
    [Required]
    [MinLength(10)]
    [MaxLength(200)]
    public string description { get; set; }
    [Required]
    [MinLength(5)]
    [MaxLength(30)]
    public string name { get; set; }
    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; }
    [DataType(DataType.Date)]
    public DateTime UpdatedAt { get; set; }
  }
}
