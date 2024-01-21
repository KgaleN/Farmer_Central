using System;
using System.Collections.Generic;

namespace FarmCentral__task_2_.Models;

public partial class Employee
{
    public string EmpId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Tel { get; set; }

    public virtual ICollection<Farmer> Farmers { get; set; } = new List<Farmer>();
}
