using EindCase.Api.Controllers;
using EindCase.Domain.Interfaces;
using EindCase.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EindCase.Test.ApiTests
{
    public class CourseApiTests
    {
        [Fact]
        public async Task GetCoursesShouldCallGetAll()
        {
            //Arrange
            var fixture = new CourseApiFixture();

            //Act
            await fixture.ExecuteGetCourses();

            //Assert
            fixture.AssertGetAllIsCalled();
        }

        [Fact]
        public async Task EmptyRepositoryShouldReturnEmptyList()
        {
            //Arrange
            var fixture = new CourseApiFixture()
                .WithEmptyRepository();
            int expected = 0;

            //Act
            var actual = await fixture.ExecuteGetCourses();

            //Assert
            Assert.Equal(expected, actual.Count());
        }

        [Fact]
        public async Task TwoRecordsInRepoShouldReturnListWithTwoCourses()
        {
            //Arrange
            var fixture = new CourseApiFixture()
                .WithXRecords(2);
            int expected = 2;

            //Act
            var result = await fixture.ExecuteGetCourses();

            //Assert
            Assert.Equal(expected, result.Count());
        }
    }

    internal class CourseApiFixture
    {
        private readonly Mock<ICourseRepository> _mockRepository;

        public CourseApiFixture()
        {
            _mockRepository = new Mock<ICourseRepository>();
        }

        public CourseApiFixture WithEmptyRepository()
        {
            WithXRecords(0);
            return this;
        }

        public CourseApiFixture WithXRecords(int x)
        {
            List<Course> list = new List<Course>();
            for (int i = 0; i < x; i++)
            {
                list.Add(new Course());
            }

            _mockRepository.Setup(r => r.GetAll()).ReturnsAsync(list);
            return this;
        }

        public async Task<IEnumerable<Course>> ExecuteGetCourses()
        {
            var sut = new CoursesController(_mockRepository.Object);
            return await sut.GetCourses();
        }

        public void AssertGetAllIsCalled()
        {
            _mockRepository.Verify(x => x.GetAll());
        }
    }
}
