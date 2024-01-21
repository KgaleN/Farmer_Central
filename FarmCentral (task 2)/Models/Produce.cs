using System;
using System.Collections.Generic;

namespace FarmCentral__task_2_.Models;

public partial class Produce
{
    public int ProduceId { get; set; }

    public string? ProduceName { get; set; }

    public string? Producetype { get; set; }

    public string? Grade { get; set; }

    public decimal? Amount { get; set; }

    public string? Unit { get; set; }

    public DateTime? PackedDate { get; set; }

    public int? ShelfLife { get; set; }

    public string? IsRotten { get; set; }

    public string? FarmrId { get; set; }

    public string? Status { get; set; }

    public virtual Farmer? Farmr { get; set; }
}
