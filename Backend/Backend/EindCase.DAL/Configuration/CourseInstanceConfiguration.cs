using EindCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EindCase.DAL.Configuration
{
    public class CourseInstanceConfiguration : IEntityTypeConfiguration<CourseInstance>
    {
        public void Configure(EntityTypeBuilder<CourseInstance> builder)
        {
            builder.HasKey(c => c.CourseInstanceId);

            builder.Property(c => c.CourseInstanceId)
                .ValueGeneratedOnAdd();
        }
    }
}
