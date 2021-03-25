using System;
using System.Collections.Generic;
using System.Text;

namespace EindCase.Domain.Models
{
    public class CourseInstance
    {
        public int CourseInstanceId { get; set; }
        public DateTime StartDate { get; set; }
        public virtual Course Course { get; set; }
    }
}
