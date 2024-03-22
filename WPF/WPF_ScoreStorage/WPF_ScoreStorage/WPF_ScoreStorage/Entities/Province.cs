using System;
using System.Collections.Generic;

namespace WPF_ScoreStorage.Entities
{
    public partial class Province
    {
        public Province()
        {
            Students = new HashSet<Student>();
        }

        public string Id { get; set; } = null!;
        public string ProvinceCode { get; set; } = null!;
        public string? ProvinceName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
