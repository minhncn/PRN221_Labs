using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_ScoreStorage.Entities;
using WPF_ScoreStorage.Model;

namespace WPF_ScoreStorage.Dao
{
    public class StudentDbContext : ScoreStorageDbContext
    {
        private static StudentDbContext instance = null;
        private static readonly object instanceLock = new object();
        private StudentDbContext()
        {
            InitalizeContext();
        }

        protected virtual void InitalizeContext()
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public static StudentDbContext Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance = new StudentDbContext();
                    return instance;
                }
            }
        }

        public StudentDto GetStudentByStudentCodeAndExamYear(string studentCode, int examYear)
        {
            var student = this.Students.Include(s => s.SchoolYear)
                .Where(s => s.StudentCode == studentCode && s.SchoolYear.ExamYear == examYear)
                .FirstOrDefault();
            var studentDto = new StudentDto() 
            { 
                Id = student.Id,
                StudentCode = student.StudentCode,
                SchoolYearId = student.SchoolYearId,
                Math = student.Math,
                Literature = student.Literature,
                Physics = student.Physics,
                Biology = student.Biology,
                English = student.English,
                Chemistry = student.Chemistry,
                History = student.History,
                Geography = student.Geography,
                Civic = student.Civic,
                ProvinceId = student.ProvinceId,
            };
            return studentDto;
        }

    }
}
