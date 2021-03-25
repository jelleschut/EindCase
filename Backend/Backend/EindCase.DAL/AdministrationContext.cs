using EindCase.DAL.Configuration;
using EindCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EindCase.DAL
{
    public class AdministrationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseInstance> CourseInstances { get; set; }

        public AdministrationContext()
        {

        }

        public AdministrationContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if(!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                var builder = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json");
                var configuration = builder.Build();

                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("AdministrationDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Course>(new CourseConfiguration());
            modelBuilder.ApplyConfiguration<CourseInstance>(new CourseInstanceConfiguration());
        }

    }
}
