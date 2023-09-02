﻿using System;
using System.Collections.Generic;

namespace Api.Client.MarquetStore.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public ulong? IsAvailable { get; set; }

    public int Stock { get; set; }

    public string Pathlmage { get; set; } = null!;
}
