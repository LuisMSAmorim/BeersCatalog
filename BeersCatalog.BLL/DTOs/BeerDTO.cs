namespace BeersCatalog.BLL.DTOs;

public sealed class BeerDTO
{
    public string Name { get; set; }
    public int Price { get; set; }
    public float ABV { get; set; }
    public int IBU { get; set; }
    public int StyleId { get; set; }
}
