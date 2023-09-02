using System;
using System.Collections.Generic;

namespace Api.Client.MarquetStore.Models;

public partial class Personalization
{
    public int Id { get; set; }

    public int ConceptId { get; set; }

    public int? IngredientId { get; set; }
}
