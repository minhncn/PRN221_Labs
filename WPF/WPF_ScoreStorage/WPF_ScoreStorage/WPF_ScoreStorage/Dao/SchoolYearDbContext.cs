using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_ScoreStorage.Entities;
using WPF_ScoreStorage.Enums;
using WPF_ScoreStorage.Model;

namespace WPF_ScoreStorage.Dao
{
    public class SchoolYearDbContext : ScoreStorageDbContext
    {
        private static SchoolYearDbContext instance = null;
        private static readonly object instanceLock = new object();
        private SchoolYearDbContext()
        {
            InitalizeContext();
        }

        protected virtual void InitalizeContext()
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public static SchoolYearDbContext Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance = new SchoolYearDbContext();
                    return instance;
                }
            }
        }

        public SchoolYear GetSchoolYearByExamYear(int examYear)
        {
            var a = this.SchoolYears.ToList();
            return this.SchoolYears.FirstOrDefault(s => s.ExamYear == examYear && s.Status == SchoolYearStatus.Active.ToString());
        }

        public SchoolYear GetDeactiveSchoolYearByExamYear(int examYear)
        {
            var a = this.SchoolYears.ToList();
            return this.SchoolYears.FirstOrDefault(s => s.ExamYear == examYear && s.Status == SchoolYearStatus.Deactive.ToString());
        }

        public async void AddSchoolYear(SchoolYear schoolYear)
        {
            await this.SchoolYears.AddAsync(schoolYear);
            this.SaveChanges();
        }

    }
}
