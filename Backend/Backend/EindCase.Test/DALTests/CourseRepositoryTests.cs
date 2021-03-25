using EindCase.DAL;
using EindCase.DAL.Repositories;
using EindCase.Domain.Interfaces;
using EindCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace EindCase.Test.DALTests
{

    public class CourseRepositoryTests
    {
        [Fact]
        public async Task EmptyDatabaseGetAllShouldReturnEmptyList()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(0);
            int expected = 0;

            //Act
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task OneRecordDatabaseGetAllShouldReturnListWithOneCourse()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(1);
            int expected = 1;

            //Act
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task ThreeRecordDatabaseGetAllShouldReturnListWithThreeCourses()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(3);
            int expected = 3;

            //Act
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task AddOneRecordToEmptyDatabaseGetAllShouldReturnListWithOneCourse()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture();
            int expected = 1;

            //Act
            await fixture.ExecuteAdd(new Course());
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task GetExistingCourseShouldReturnCourse()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(1);
            Course expected = new Course() { CourseId = 1, Code = "C1", LengthInDays = 5, Title = "Course1" };

            //Act
            var result = await fixture.ExecuteGet(new Course() { Code = "C1" });

            //Assert
            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task GetNonExistentCourseShouldReturnNull()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(1);
           
            //Act
            var result = await fixture.ExecuteGet(new Course() { Code = "C2" });

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddIfNotExistsShouldReturnCourseAndTrueIfExists()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(3);

            //Act
            var result = await fixture.ExecuteAddIfNotExists(new Course() { Code = "C1", LengthInDays = 5, Title = "Course1" });

            //Assert
            Assert.IsType<Course>(result.course);
            Assert.True(result.exists);
        }

        [Fact]
        public async Task AddIfNotExistsShouldReturnCourseAndFalseIfNotExists()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(0);

            //Act
            var result = await fixture.ExecuteAddIfNotExists(new Course() { Code = "C1", LengthInDays = 5, Title = "Course1" });

            //Assert
            Assert.IsType<Course>(result.course);
            Assert.False(result.exists);
        }

        [Fact]
        public async Task AddIfNotExistsShouldReturnExistingCourseWithIdIfExists()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(3);
            int expected = 1;

            //Act
            var result = await fixture.ExecuteAddIfNotExists(new Course() { Code = "C1", LengthInDays = 5, Title = "Course1" });

            //Assert
            Assert.Equal(expected, result.course.CourseId);
        }

        [Fact]
        public async Task AddIfNotExistsShouldReturnNewCourseWithIdIfExists()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(3);
            int expected = 4;

            //Act
            var result = await fixture.ExecuteAddIfNotExists(new Course() { Code = "C4", LengthInDays = 5, Title = "Course4" });

            //Assert
            Assert.Equal(expected, result.course.CourseId);
        }

        [Fact]
        public async Task AddIfNotExistsShouldAdd()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(3);
            int expected = 4;

            //Act
            await fixture.ExecuteAddIfNotExists(new Course() { Code = "C4", LengthInDays = 5, Title = "Course4" });
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task AddIfNotExistsShouldNotAdd()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(3);
            int expected = 3;

            //Act
            await fixture.ExecuteAddIfNotExists(new Course() { Code = "C3", LengthInDays = 5, Title = "Course3" });
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task AddIfNotExistsShouldNotAddIfCodeExists()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(3);
            int expected = 3;

            //Act
            await fixture.ExecuteAddIfNotExists(new Course() { Code = "C3", LengthInDays = 3, Title = "Course5" });
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task FindByCodeShouldReturnIfCodeExists()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithSpecificCourse(code: "C1");
            string expected = "C1";

            //Act
            var result = await fixture.ExecuteFindByCode("C1");

            //Assert
            Assert.Equal(expected, result.Code);
        }

        [Fact]
        public async Task FindByCodeShouldReturnNullIfCodeNotExists()
        {
            //Arrange
            var fixture = new CourseRepositoryFixture()
                .WithXCourses(5);

            //Act
            var result = await fixture.ExecuteFindByCode("C6");

            //Assert
            Assert.Null(result);
        }
    }

    internal class CourseRepositoryFixture
    {

        private readonly DbContextOptions _options;
        private readonly AdministrationContext _context;

        public CourseRepositoryFixture()
        {
            _options = new DbContextOptionsBuilder<AdministrationContext>()
                .UseInMemoryDatabase(databaseName: "AdminTestDB")
                .EnableSensitiveDataLogging()
                .Options;

            _context = new AdministrationContext(_options);
            _context.Database.EnsureDeleted();
        }

        public CourseRepositoryFixture WithXCourses(int x)
        {
            for (int i = 1; i <= x; i++)
            {
                _context.Courses.Add(new Course() { Code = "C" + i, LengthInDays = 5, Title = "Course" + i });
            }
            _context.SaveChanges();
            return this;
        }

        public CourseRepositoryFixture WithSpecificCourse(string code = "C1", int length = 5, string title = "Course1")
        {
            _context.Courses.Add(new Course() { Code = code, LengthInDays = length, Title = title });
            _context.SaveChanges();
            return this;
        }

        public async Task<IEnumerable<Course>> ExecuteGetAll()
        {
            var sut = new CourseRepository(_context);
            return await sut.GetAll();
        }

        public async Task ExecuteAdd(Course course)
        {
            var sut = new CourseRepository(_context);
            await sut.Add(course);
        }

        public async Task<Course> ExecuteGet(Course course)
        {
            var sut = new CourseRepository(_context);
            return await sut.Get(course);
        }

        public async Task<(Course course, bool exists)> ExecuteAddIfNotExists(Course course)
        {
            var sut = new CourseRepository(_context);
            return await sut.AddIfNotExists(course);
        }

        public async Task<Course> ExecuteFindByCode(string code)
        {
            var sut = new CourseRepository(_context);
            return await sut.FindByCode(code);
        }
    }
}
