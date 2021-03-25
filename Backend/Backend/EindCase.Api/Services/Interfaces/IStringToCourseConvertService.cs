using EindCase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EindCase.Api.Services.Interfaces
{
    public interface IStringToCourseConvertService
    {
        List<CourseInstance> Convert(string inputString);
        List<CourseInstance> ConvertToCourse(List<string> stringObjects);
        List<string> SplitToObjects(string inputString);
    }
}
