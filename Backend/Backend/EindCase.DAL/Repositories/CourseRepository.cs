using EindCase.Domain.Interfaces;
using EindCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindCase.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AdministrationContext _context;
        public CourseRepository(AdministrationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> FindByCode(string code)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task Add(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task<(Course course, bool exists)> AddIfNotExists(Course course)
        {
            bool exists = await Get(course) != null;
            if (!exists)
            {
                await Add(course);
            }
            return (await Get(course), exists);
        }

        public async Task<Course> Get(Course course)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Code == course.Code);
        }
    }
}
