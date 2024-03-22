using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace WPF_ScoreStorage.Entities
{
    public partial class ScoreStorageContext : DbContext
    {
        public ScoreStorageContext()
        {
        }

        public ScoreStorageContext(DbContextOptions<ScoreStorageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<SchoolYear> SchoolYears { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();
                var strConn = config["ConnectionStrings:MyConnectionString"];
                optionsBuilder.UseSqlServer(strConn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.Id).HasMaxLength(255);

                entity.Property(e => e.ProvinceCode).HasMaxLength(255);

                entity.Property(e => e.ProvinceName).HasMaxLength(255);
            });

            modelBuilder.Entity<SchoolYear>(entity =>
            {
                entity.ToTable("SchoolYear");

                entity.Property(e => e.Id).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id).HasMaxLength(255);

                entity.Property(e => e.Biology).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Chemistry).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Civic).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.English).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Geography).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.History).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Literature).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Math).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Physics).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.ProvinceId).HasMaxLength(255);

                entity.Property(e => e.SchoolYearId).HasMaxLength(255);

                entity.Property(e => e.StudentCode).HasMaxLength(50);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Student_Province");

                entity.HasOne(d => d.SchoolYear)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolYearId)
                    .HasConstraintName("FK__Student__SchoolY__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
