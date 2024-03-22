using System;
using System.Collections.Generic;

namespace WPF_ScoreStorage.Model
{
    public class SchoolYearDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int ExamYear { get; set; }
        public string Status { get; set; } = null!;
    }
}
