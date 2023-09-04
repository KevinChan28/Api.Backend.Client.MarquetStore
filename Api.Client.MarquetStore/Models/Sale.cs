using System;
using System.Collections.Generic;

namespace Api.Client.MarquetStore.Models;

public partial class Sale
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public decimal Total { get; set; }

    public int UserId { get; set; }

    public bool? IsDelivered { get; set; }
}
