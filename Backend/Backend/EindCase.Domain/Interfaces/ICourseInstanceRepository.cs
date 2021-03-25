using EindCase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EindCase.Domain.Interfaces
{
    public interface ICourseInstanceRepository
    {
        Task Add(CourseInstance courseInstance);
        Task<(CourseInstance courseInstance, bool exists)> AddIfNotExists(CourseInstance courseInstance);
        Task<IEnumerable<CourseInstance>> GetAll();
        Task<CourseInstance> Get(CourseInstance courseInstance);
    }
}
