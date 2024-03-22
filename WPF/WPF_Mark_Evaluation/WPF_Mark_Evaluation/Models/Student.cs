using System;
using System.Collections.Generic;

namespace WPF_Mark_Evaluation.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? StudentCode { get; set; }

    public int? SchoolYearId { get; set; }

    public string? Status { get; set; }

    public virtual SchoolYear? SchoolYear { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
