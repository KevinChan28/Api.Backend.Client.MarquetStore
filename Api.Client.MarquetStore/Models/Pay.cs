using System;
using System.Collections.Generic;

namespace Api.Client.MarquetStore.Models;

public partial class Pay
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public int SaleId { get; set; }

    public int PaymentsMethodId { get; set; }
}
