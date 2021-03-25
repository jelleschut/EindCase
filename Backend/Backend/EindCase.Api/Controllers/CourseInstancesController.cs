using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EindCase.DAL;
using EindCase.Domain.Models;
using EindCase.Domain.Interfaces;

namespace EindCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInstancesController : ControllerBase
    {
        private readonly ICourseInstanceRepository _courseInstanceRepository;

        public CourseInstancesController(ICourseInstanceRepository courseInstanceRepository)
        {
            _courseInstanceRepository = courseInstanceRepository;
        }

        // GET: api/CourseInstances
        [HttpGet]
        public async Task<IEnumerable<CourseInstance>> GetCourseInstances()
        {
            var list = await _courseInstanceRepository.GetAll();
            list = list.OrderBy(c => c.StartDate);
            return list;
        }

        //// GET: api/CourseInstances/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<CourseInstance>> GetCourseInstance(int id)
        //{
        //    var courseInstance = await _context.CourseInstances.FindAsync(id);

        //    if (courseInstance == null)
        //    {
        //        return NotFound();
        //    }

        //    return courseInstance;
        //}

        //// PUT: api/CourseInstances/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCourseInstance(int id, CourseInstance courseInstance)
        //{
        //    if (id != courseInstance.CourseInstanceId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(courseInstance).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CourseInstanceExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/CourseInstances
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<CourseInstance>> PostCourseInstance(CourseInstance courseInstance)
        //{
        //    _context.CourseInstances.Add(courseInstance);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCourseInstance", new { id = courseInstance.CourseInstanceId }, courseInstance);
        //}

        //// DELETE: api/CourseInstances/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCourseInstance(int id)
        //{
        //    var courseInstance = await _context.CourseInstances.FindAsync(id);
        //    if (courseInstance == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CourseInstances.Remove(courseInstance);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CourseInstanceExists(int id)
        //{
        //    return _context.CourseInstances.Any(e => e.CourseInstanceId == id);
        //}
    }
}
