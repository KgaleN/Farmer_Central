using System;
using System.Collections.Generic;

namespace FarmCentral__task_2_.Models;

public partial class Farmer
{
    public string FarmerId { get; set; } = null!;
    public string? Email { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Address { get; set; }

    public string? Tel { get; set; }

    public string? Active { get; set; }

    public string? EmpId { get; set; }

    public virtual Employee? Emp { get; set; }

    public virtual ICollection<Produce> Produces { get; set; } = new List<Produce>();
}
