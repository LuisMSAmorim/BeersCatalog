using System.ComponentModel.DataAnnotations;

namespace BeersCatalog.API.LoginModels.Config;

public sealed class UserDetails
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

}
