using System;
using System.Collections.Generic;

namespace WPF_Mark_Evaluation.Models;

public partial class SchoolYear
{
    public int Id { get; set; }

    public string? ExamYear { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
