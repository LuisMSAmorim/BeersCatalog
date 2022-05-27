using System.ComponentModel.DataAnnotations;

namespace BeersCatalog.BLL.Models;

public sealed class Beer
{
    [Key]
    public int Id { get; set; }
    [Required]
    [Display(Name = "Nome")]
    public string Name { get; set; }
    [Required]
    [Display(Name = "Preço")]
    public int Price { get; set; }
    [Required]
    public float ABV { get; set; }
    [Required]
    public int IBU { get; set; }
    [Required]
    public int StyleID { get; set; }
    [Display(Name = "Estilo")]
    public Style Style { get; set; }
}
