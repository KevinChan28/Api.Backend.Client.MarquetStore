using System;
using System.Collections.Generic;

namespace Api.Client.MarquetStore.Models;

public partial class PaymentsMethod
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;
}
