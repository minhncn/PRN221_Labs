using System;
using System.Collections.Generic;

namespace WPF_Mark_Evaluation.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
