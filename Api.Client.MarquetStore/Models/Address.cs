using System;
using System.Collections.Generic;

namespace Api.Client.MarquetStore.Models;

public partial class Address
{
    public int Id { get; set; }

    public string ZipCode { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Neighborhood { get; set; } = null!;

    public string InteriorNumber { get; set; } = null!;

    public string OutdoorNumber { get; set; } = null!;

    public int UserId { get; set; }
}
