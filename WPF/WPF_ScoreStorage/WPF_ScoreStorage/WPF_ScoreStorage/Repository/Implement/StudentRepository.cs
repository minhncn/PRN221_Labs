using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_ScoreStorage.Dao;
using WPF_ScoreStorage.Model;

namespace WPF_ScoreStorage.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public StudentDto GetStudentByStudentCodeAndExamYear(string studentCode, int examYear)
            => StudentDbContext.Instance.GetStudentByStudentCodeAndExamYear(studentCode, examYear);
    }
}
