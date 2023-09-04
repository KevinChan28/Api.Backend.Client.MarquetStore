using System;
using System.Collections.Generic;

namespace Api.Client.MarquetStore.Models;

public partial class Ingredient
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Stock { get; set; }

    public decimal Price { get; set; }

    public bool? IsAvailable { get; set; }

    public string? PathImage { get; set; }
}
