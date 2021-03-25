using EindCase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EindCase.Api.Services.Interfaces
{
    public interface ICourseInsertService
    {
        Task<(int, int, int )> InsertInstances(List<CourseInstance> courseInstances);
    }
}
