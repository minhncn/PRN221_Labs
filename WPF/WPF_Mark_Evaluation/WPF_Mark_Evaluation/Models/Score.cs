using System;
using System.Collections.Generic;

namespace WPF_Mark_Evaluation.Models;

public partial class Score
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? SubjectId { get; set; }

    public double? Score1 { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Subject? Subject { get; set; }
}
