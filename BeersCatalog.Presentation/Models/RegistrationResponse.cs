﻿namespace BeersCatalog.Presentation.Models;

public class RegistrationResponse
{
    #nullable disable
    public string Message { get; set; }
    #nullable enable
    public Object? Errors { get; set; }
}
