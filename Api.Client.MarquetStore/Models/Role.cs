using System;
using System.Collections.Generic;

namespace Api.Client.MarquetStore.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
}
