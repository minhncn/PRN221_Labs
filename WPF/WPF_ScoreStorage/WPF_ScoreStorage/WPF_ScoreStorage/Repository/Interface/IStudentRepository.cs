using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_ScoreStorage.Model;

namespace WPF_ScoreStorage.Repository
{
    public interface IStudentRepository
    {
        StudentDto GetStudentByStudentCodeAndExamYear(string studentCode, int examYear);
    }
}
