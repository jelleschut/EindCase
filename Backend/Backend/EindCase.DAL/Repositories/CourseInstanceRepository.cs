using EindCase.Domain.Interfaces;
using EindCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EindCase.DAL.Repositories
{
    public class CourseInstanceRepository : ICourseInstanceRepository
    {
        private readonly AdministrationContext _context;
        public CourseInstanceRepository(AdministrationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseInstance>> GetAll()
        {
            return await _context.CourseInstances.ToListAsync();
        }

        public async Task Add(CourseInstance courseInstance)
        {
            _context.CourseInstances.Add(courseInstance);
            await _context.SaveChangesAsync();
        }

        public async Task<(CourseInstance, bool)> AddIfNotExists(CourseInstance courseInstance)
        {
            bool exists = await Get(courseInstance) != null;
            if (!exists)
            {
                await Add(courseInstance);
            }
            return (await Get(courseInstance), exists);
        }

        public async Task<CourseInstance> Get(CourseInstance courseInstance)
        {
            return await _context.CourseInstances.FirstOrDefaultAsync(
                c => c.Course.Code == courseInstance.Course.Code
                && c.StartDate == courseInstance.StartDate);
        }
    }
}
