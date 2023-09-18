using System;
using System.Collections.Generic;

namespace Api.Client.MarquetStore.Models;

public partial class Concept
{
    public int Id { get; set; }

    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public decimal Import { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
