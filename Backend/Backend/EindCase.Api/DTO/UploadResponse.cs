using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EindCase.Api.DTO
{
    public class UploadResponse
    {
        public int NewCourses { get; set; }
        public int NewInstances { get; set; }
        public int Duplicates { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}
