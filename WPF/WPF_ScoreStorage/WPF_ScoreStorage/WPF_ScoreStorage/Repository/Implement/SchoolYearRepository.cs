using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_ScoreStorage.Dao;
using WPF_ScoreStorage.Entities;
using WPF_ScoreStorage.Model;

namespace WPF_ScoreStorage.Repository
{
    public class SchoolYearRepository : ISchoolYearRepository
    {
        public SchoolYear GetSchoolYearByExamYear(int examYear)
            => SchoolYearDbContext.Instance.GetSchoolYearByExamYear(examYear);

        public SchoolYear GetDeactiveSchoolYearByExamYear(int examYear)
            => SchoolYearDbContext.Instance.GetDeactiveSchoolYearByExamYear(examYear);
        public void AddSchoolYear(SchoolYear schoolYear)
            => SchoolYearDbContext.Instance.AddSchoolYear(schoolYear);
    }
}
