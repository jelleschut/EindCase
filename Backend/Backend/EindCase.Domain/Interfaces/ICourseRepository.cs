using EindCase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EindCase.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task Add(Course course);
        Task<(Course course, bool exists)> AddIfNotExists(Course course);
        Task<Course> FindByCode(string code);
        Task<Course> Get(Course course);
        Task<IEnumerable<Course>> GetAll();
    }
}
