using System.ComponentModel.DataAnnotations;

namespace BeersCatalog.BLL.Models;

public sealed class Style
{
    [Key]
    public int Id { get; set; }
    [Required]
    [Display(Name = "Nome")]
    public string Name { get; set; }
}
