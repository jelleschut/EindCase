using EindCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EindCase.DAL.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);

            builder.Property(c => c.CourseId)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Title)
                .HasMaxLength(300);

            builder.Property(c => c.Code)
                .HasMaxLength(10);
        }
    }
}
