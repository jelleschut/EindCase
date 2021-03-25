using EindCase.Api.Services;
using EindCase.Domain.Interfaces;
using EindCase.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EindCase.Test.ApiTests.Services
{
    public class CourseInsertServiceTests
    {
        [Fact]
        public async Task EmptyListShouldRetur0And0And0()
        {
            //Arrange
            var fixture = new CourseInsertFixture();
            (int, int, int) expected = (0, 0, 0);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OneUniqueEntry_ShouldReturn1And1And0()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXUniqueCourseInstancesWithUniqueCourses(1);
            (int, int, int) expected = (1, 1, 0);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task TenUniqueEntries_ShouldReturn10And10And0()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXUniqueCourseInstancesWithUniqueCourses(10);
            (int, int, int) expected = (10, 10, 0);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OneUniqueInstanceWithExistingCourse_ShouldReturn0And1And0()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXUniqueCourseInstancesWithExistingCourses(1);
            (int, int, int) expected = (0, 1, 0);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task TenUniqueInstancesWithExistingCourses_ShouldReturn0And10And0()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXUniqueCourseInstancesWithExistingCourses(10);
            (int, int, int) expected = (0, 10, 0);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OneExistingCourseInstance_ShouldRetur0And0And1()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXExistingCourseInstances(1);
            (int, int, int) expected = (0, 0, 1);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task TenExistingCourseInstances_ShouldRetur0And0And10()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXExistingCourseInstances(10);
            (int, int, int) expected = (0, 0, 10);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OneUniqueCourse_OneDuplicateCourse_ShouldReturn1And2And0()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXUniqueCourseInstancesWithUniqueCourses(1)
                .WithXUniqueCourseInstancesWithExistingCourses(1);
            (int, int, int) expected = (1, 2, 0);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OneUniqueCourse_OneExistingCourse_OneExistingInstance_ShouldReturn1And2And1()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXUniqueCourseInstancesWithUniqueCourses(1)
                .WithXUniqueCourseInstancesWithExistingCourses(1)
                .WithXExistingCourseInstances(1);
            (int, int, int) expected = (1, 2, 1);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OneExistingCourse_OneExistingInstance_ShouldReturn0And1And1()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXUniqueCourseInstancesWithExistingCourses(1)
                .WithXExistingCourseInstances(1);
            (int, int, int) expected = (0, 1, 1);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OneUniqueCourse_OneExistingInstance_ShouldReturn1And1And1()
        {
            //Arrange
            var fixture = new CourseInsertFixture()
                .WithXUniqueCourseInstancesWithUniqueCourses(1)
                .WithXExistingCourseInstances(1);
            (int, int, int) expected = (1, 1, 1);

            //Act
            var actual = await fixture.ExecuteInsertString();

            //Assert
            Assert.Equal(expected, actual);
        }

    }

    internal class CourseInsertFixture
    {
        private readonly Mock<ICourseRepository> _mockCourseRepo;
        private readonly Mock<ICourseInstanceRepository> _mockInstanceRepo;
        private readonly List<CourseInstance> _mockList;


        public CourseInsertFixture()
        {
            _mockCourseRepo = new Mock<ICourseRepository>();
            _mockInstanceRepo = new Mock<ICourseInstanceRepository>();
            _mockList = new List<CourseInstance>();

            _mockCourseRepo.Setup(c => c.AddIfNotExists(It.IsAny<Course>())).ReturnsAsync(
                (Course arg) =>
                {
                    return arg == null ? (new Course(), true) : (new Course(), false);
                });
        }

        public CourseInsertFixture WithXUniqueCourseInstancesWithUniqueCourses(int x)
        {
            Mock<CourseInstance> uniqueInstance = new Mock<CourseInstance>();
            uniqueInstance.Setup(i => i.Course).Returns(new Course());
            _mockInstanceRepo.Setup(c => c.AddIfNotExists(uniqueInstance.Object)).ReturnsAsync((new CourseInstance(), false));

            for (int i = 0; i < x; i++)
            {
                _mockList.Add(uniqueInstance.Object);
            }
            return this;
        }

        public CourseInsertFixture WithXUniqueCourseInstancesWithExistingCourses(int x)
        {
            Mock<CourseInstance> semiUniqueInstance = new Mock<CourseInstance>();
            semiUniqueInstance.Setup(i => i.Course).Returns((Course)null);
            _mockInstanceRepo.Setup(c => c.AddIfNotExists(semiUniqueInstance.Object)).ReturnsAsync((new CourseInstance(), false));


            for (int i = 0; i < x; i++)
            {
                _mockList.Add(semiUniqueInstance.Object);
            }
            return this;
        }

        public CourseInsertFixture WithXExistingCourseInstances(int x)
        {
            Mock<CourseInstance> existingInstance = new Mock<CourseInstance>();
            _mockInstanceRepo.Setup(c => c.AddIfNotExists(existingInstance.Object)).ReturnsAsync((new CourseInstance(), true));

            for (int i = 0; i < x; i++)
            {
                _mockList.Add(existingInstance.Object);
            }
            return this;
        }

        public async Task<(int, int, int)> ExecuteInsertString()
        {
            var sut = new CourseInsertService(_mockCourseRepo.Object, _mockInstanceRepo.Object);
            return await sut.InsertInstances(_mockList);
        }
    }
}
