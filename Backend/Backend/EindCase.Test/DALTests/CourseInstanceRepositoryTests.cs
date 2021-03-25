using EindCase.DAL;
using EindCase.DAL.Repositories;
using EindCase.Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EindCase.Test.DALTests
{
    public class CourseInstanceRepositoryTests
    {

        [Fact]
        public async Task EmptyDatabaseGetAllShouldReturnEmptyList()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                .WithXCourseInstances(0);
            int expected = 0;

            //Act
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task OneRecordDatabaseGetAllShouldReturnListWithOneCourseInstance()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                .WithXCourseInstances(1);
            int expected = 1;

            //Act
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task ThreeRecordsDatabaseGetAllShouldReturnListWithThreeCourseInstances()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                .WithXCourseInstances(3);
            int expected = 3;

            //Act
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task AddOneRecordToEmptyDatabaseGetAllShouldReturnListWithOneCourseInstance()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture();
            int expected = 1;

            //Act
            await fixture.ExecuteAdd(new CourseInstance());
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task GetExistingCourseInstanceShouldReturnCourseInstance()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                .WithXCourseInstances(1);
            CourseInstance expected = new CourseInstance() { Course = new Course() { CourseId = 1, Code = "C1", LengthInDays = 5, Title = "Course1" }, StartDate = DateTime.Now.Date, CourseInstanceId = 1 };

            //Act
            var result = await fixture.ExecuteGet(new CourseInstance() { Course = new Course() { Code = "C1" }, StartDate = DateTime.Now.Date });

            //Assert
            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task GetCourseInstanceWithWrongDateShouldReturnNull()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                       .WithXCourseInstances(1);
            //Act
            var result = await fixture.ExecuteGet(new CourseInstance() { Course = new Course() { Code = "C1" }, StartDate = DateTime.Now.Date.AddDays(1) });


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCourseInstanceWithWrongCodeShouldReturnNull()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                       .WithXCourseInstances(1);
            //Act
            var result = await fixture.ExecuteGet(new CourseInstance() { Course = new Course() { Code = "C2" }, StartDate = DateTime.Now.Date });


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddIfNotExistsShouldReturnCourseInstanceAndTrueIfExists()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                .WithXCourseInstances(3);

            //Act
            var result = await fixture.ExecuteAddIfNotExists(new CourseInstance() { Course = new Course() { Code = "C1", LengthInDays = 5, Title = "Course1" }, StartDate = DateTime.Now.Date });

            //Assert
            Assert.IsType<CourseInstance>(result.courseInstance);
            Assert.True(result.exists);
        }

        [Fact]
        public async Task AddIfNotExistsShouldReturnCourseInstanceAndFalseIfNotExists()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture();

            //Act
            var result = await fixture.ExecuteAddIfNotExists(new CourseInstance() { Course = new Course(), StartDate = DateTime.Now.Date });

            //Assert
            Assert.IsType<CourseInstance>(result.courseInstance);
            Assert.False(result.exists);
        }

        [Fact]
        public async Task AddIfNotExistsShouldReturnExistingCourseInstanceWithIdIfExists()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                .WithXCourseInstances(3);
            int expected = 1;

            //Act
            var result = await fixture.ExecuteAddIfNotExists(new CourseInstance() { Course = new Course() { Code = "C1", LengthInDays = 5, Title = "Course1" }, StartDate = DateTime.Now.Date });

            //Assert
            Assert.Equal(expected, result.courseInstance.CourseInstanceId);
        }

        [Fact]
        public async Task AddIfNotExistsShouldReturnNewCourseInstanceWithIdIfExists()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                .WithXCourseInstances(3);
            int expected = 4;

            //Act
            var result = await fixture.ExecuteAddIfNotExists(new CourseInstance() { Course = new Course() { Code = "C4", LengthInDays = 5, Title = "Course4" }, StartDate = DateTime.Now.Date.AddDays(3) });

            //Assert
            Assert.Equal(expected, result.courseInstance.CourseInstanceId);
        }

        [Fact]
        public async Task AddIfNotExistsShouldAdd()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                .WithXCourseInstances(3);
            int expected = 4;

            //Act
            await fixture.ExecuteAddIfNotExists(new CourseInstance() { Course = new Course() { Code = "C4", LengthInDays = 5, Title = "Course4" }, StartDate = DateTime.Now.Date.AddDays(4) });
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task AddIfNotExistsShouldNotAddIfCourseInstanceExists()
        {
            //Arrange
            var fixture = new CourseInstanceRepositoryFixture()
                .WithXCourseInstances(3);
            int expected = 3;

            //Act
            await fixture.ExecuteAddIfNotExists(new CourseInstance() { Course = new Course() { Code = "C3", LengthInDays = 5, Title = "Course3" }, StartDate = DateTime.Now.Date.AddDays(2) });
            var result = await fixture.ExecuteGetAll();

            //Assert
            Assert.Equal(expected, result.Count());
        }
    }

    internal class CourseInstanceRepositoryFixture
    {

        private readonly DbContextOptions _options;
        private readonly AdministrationContext _context;

        public CourseInstanceRepositoryFixture()
        {
            _options = new DbContextOptionsBuilder<AdministrationContext>()
                .UseInMemoryDatabase(databaseName: "AdminTestDB")
                .Options;

            _context = new AdministrationContext(_options);
            _context.Database.EnsureDeleted();
        }

        public CourseInstanceRepositoryFixture WithXCourseInstances(int x)
        {
            for (int i = 1; i <= x; i++)
            {
                _context.CourseInstances.Add(new CourseInstance() { StartDate = DateTime.Now.Date.AddDays(i-1), Course = new Course() { Code = "C" + i, LengthInDays = 5, Title = "Course" + i } });
            }
            _context.SaveChanges();
            return this;
        }

        public async Task<IEnumerable<CourseInstance>> ExecuteGetAll()
        {
            var sut = new CourseInstanceRepository(_context);
            return await sut.GetAll();
        }

        public async Task ExecuteAdd(CourseInstance courseInstance)
        {
            var sut = new CourseInstanceRepository(_context);
            await sut.Add(courseInstance);
        }

        public async Task<CourseInstance> ExecuteGet(CourseInstance courseInstance)
        {
            var sut = new CourseInstanceRepository(_context);
            return await sut.Get(courseInstance);
        }

        public async Task<(CourseInstance courseInstance, bool exists)> ExecuteAddIfNotExists(CourseInstance courseInstance)
        {
            var sut = new CourseInstanceRepository(_context);
            return await sut.AddIfNotExists(courseInstance);
        }
    }
}
