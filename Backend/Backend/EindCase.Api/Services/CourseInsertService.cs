using EindCase.Api.Services.Interfaces;
using EindCase.Domain.Interfaces;
using EindCase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EindCase.Api.Services
{
    public class CourseInsertService : ICourseInsertService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseInstanceRepository _courseInstanceRepository;

        public CourseInsertService(ICourseRepository courseRepository, 
                                   ICourseInstanceRepository courseInstanceRepository)
        {
            _courseRepository = courseRepository;
            _courseInstanceRepository = courseInstanceRepository;
        }

        public async Task<(int, int, int)> InsertInstances(List<CourseInstance> courseInstances)
        {
            int newCourses = 0;
            int newCourseInstances = 0;

            foreach(CourseInstance c in courseInstances)
            {
                bool courseExists = false;
                bool courseInstanceExists = false;


                (c.Course, courseExists) = await _courseRepository.AddIfNotExists(c.Course);
                courseInstanceExists = (await _courseInstanceRepository.AddIfNotExists(c)).exists;

                if(!courseExists)
                {
                    newCourses++;
                }
                if(!courseInstanceExists)
                {
                    newCourseInstances++;
                }
            }

            return (newCourses, newCourseInstances, (courseInstances.Count - newCourseInstances));
        }
    }
}
