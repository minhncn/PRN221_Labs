using System;
using System.Collections.Generic;

namespace WPF_ScoreStorage.Entities
{
    public partial class Student
    {
        public string Id { get; set; } = null!;
        public string StudentCode { get; set; } = null!;
        public string? SchoolYearId { get; set; }
        public decimal? Math { get; set; }
        public decimal? Literature { get; set; }
        public decimal? Physics { get; set; }
        public decimal? Biology { get; set; }
        public decimal? English { get; set; }
        public decimal? Chemistry { get; set; }
        public decimal? History { get; set; }
        public decimal? Geography { get; set; }
        public decimal? Civic { get; set; }
        public string? ProvinceId { get; set; }

        public virtual Province? Province { get; set; }
        public virtual SchoolYear? SchoolYear { get; set; }
    }
}
