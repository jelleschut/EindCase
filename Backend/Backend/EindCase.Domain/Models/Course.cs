using System;
using System.Collections.Generic;
using System.Text;

namespace EindCase.Domain.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public int LengthInDays { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }

    }
}
