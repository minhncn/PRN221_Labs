using System;
using System.Collections.Generic;

namespace WPF_ScoreStorage.Entities
{
    public partial class SchoolYear
    {
        public SchoolYear()
        {
            Students = new HashSet<Student>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int ExamYear { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}
