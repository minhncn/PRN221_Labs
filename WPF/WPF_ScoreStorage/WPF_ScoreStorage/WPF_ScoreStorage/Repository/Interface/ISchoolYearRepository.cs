using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_ScoreStorage.Entities;
using WPF_ScoreStorage.Model;

namespace WPF_ScoreStorage.Repository
{
    public interface ISchoolYearRepository
    {
        SchoolYear GetSchoolYearByExamYear(int examYear);
        SchoolYear GetDeactiveSchoolYearByExamYear(int examYear);
        void AddSchoolYear(SchoolYear schoolYear);
    }
}
