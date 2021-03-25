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
    public class CourseInstanceApiTests
    {
        [Fact]
        public async Task GetCourseInstancesShouldCallGetAll()
        {
            //Arrange
            var fixture = new CourseInstanceApiFixture();

            //Act
            await fixture.ExecuteGetCourseInstances();

            //Assert
            fixture.AssertGetAllIsCalled();
        }

        [Fact]
        public async Task EmptyRepositoryShouldReturnEmptyList()
        {
            //Arrange
            var fixture = new CourseInstanceApiFixture()
                .WithEmptyRepository();
            int expected = 0;

            //Act
            var actual = await fixture.ExecuteGetCourseInstances();

            //Assert
            Assert.Equal(expected, actual.Count());
        }

        [Fact]
        public async Task TwoRecordsInRepoShouldReturnListWithTwoCourseInstances()
        {
            //Arrange
            var fixture = new CourseInstanceApiFixture()
                .WithXRecords(2);
            int expected = 2;

            //Act
            var result = await fixture.ExecuteGetCourseInstances();

            //Assert
            Assert.Equal(expected, result.Count());
        }
    }

    internal class CourseInstanceApiFixture
    {
        private readonly Mock<ICourseInstanceRepository> _mockRepository;

        public CourseInstanceApiFixture()
        {
            _mockRepository = new Mock<ICourseInstanceRepository>();
        }

        public CourseInstanceApiFixture WithEmptyRepository()
        {
            WithXRecords(0);
            return this;
        }

        public CourseInstanceApiFixture WithXRecords(int x)
        {
            List<CourseInstance> list = new List<CourseInstance>();
            for (int i = 0; i < x; i++)
            {
                list.Add(new CourseInstance());
            }

            _mockRepository.Setup(r => r.GetAll()).ReturnsAsync(list);
            return this;
        }

        public async Task<IEnumerable<CourseInstance>> ExecuteGetCourseInstances()
        {
            var sut = new CourseInstancesController(_mockRepository.Object);
            return await sut.GetCourseInstances();
        }

        public void AssertGetAllIsCalled()
        {
            _mockRepository.Verify(x => x.GetAll());
        }
    }
}
