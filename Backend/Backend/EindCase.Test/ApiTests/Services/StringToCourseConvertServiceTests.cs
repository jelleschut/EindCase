using EindCase.Api.Services;
using EindCase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EindCase.Test.ApiTests.Services
{
    public class StringToCourseConvertServiceTests
    {
        [Fact]
        public void OneValidObjectShouldReturnListWithOneString()
        {
            //Arrange
            var fixture = new StringToCourseConvertFixture()
                .WithXValidEntries(1);
            int expected = 1;

            //Act
            var result = fixture.ExecuteSplitToObject();

            //Assert
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public void TenValidEntriesShouldReturnListWithTenStrings()
        {
            //Arrange
            var fixture = new StringToCourseConvertFixture()
                .WithXValidEntries(10);
            int expected = 10;

            //Act
            var result = fixture.ExecuteSplitToObject();

            //Assert
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public void OneValidObjectShouldReturnListWithOneCourseInstance()
        {
            //Arrange
            var fixture = new StringToCourseConvertFixture()
                .WithXValidObjects(1);
            int expected = 1;

            //Act
            var result = fixture.ExecuteConverToCourse();

            //Assert
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public void TenValidObjectsShouldReturnListWithTenCourseInstances()
        {
            //Arrange
            var fixture = new StringToCourseConvertFixture()
                .WithXValidObjects(10);
            int expected = 10;

            //Act
            var result = fixture.ExecuteConverToCourse();

            //Assert
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public void ValidEntriesShouldReturnListOfCourseInstances()
        {
            //Arrange
            var fixture = new StringToCourseConvertFixture()
                .WithXValidEntries(1);

            //Act
            var result = fixture.ExecuteConvert();

            //Assert
            Assert.IsType<List<CourseInstance>>(result);
        }

        [Fact]
        public void OneValidEntryShouldReturnListWithOneCourseInstance()
        {
            //Arrange
            var fixture = new StringToCourseConvertFixture()
                .WithXValidEntries(1);
            int expected = 1;

            //Act
            var result = fixture.ExecuteConvert();

            //Assert
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public void TenValidEntriesShouldReturnListWithTenCourseInstances()
        {
            //Arrange
            var fixture = new StringToCourseConvertFixture()
                .WithXValidEntries(10);
            int expected = 10;

            //Act
            var result = fixture.ExecuteConvert();

            //Assert
            Assert.Equal(expected, result.Count);
        }
    }

    internal class StringToCourseConvertFixture
    {
        private string _inputString;
        private readonly List<string> _objectStrings = new List<string>();
        private readonly string _validString;

        public StringToCourseConvertFixture()
        {
            _validString = @"Titel: C# Programmeren
                                Cursuscode: CNETIN
                                Duur: 5 dagen
                                Startdatum: 8 / 10 / 2018";
        }

        public StringToCourseConvertFixture WithXValidObjects(int x)
        {           
            for (int i = 0; i < x; i++)
            {
                _objectStrings.Add(_validString);
            }
          
            return this;
        }

        public StringToCourseConvertFixture WithXValidEntries(int x)
        {
            _inputString = _validString;

            for (int i = 0; i < x - 1; i++)
            {
                StringBuilder s = new StringBuilder(_inputString);
                s.Append("\r\n\r\n");
                s.Append(_validString);
                _inputString = s.ToString();
            }

            return this;
        }

        public List<string> ExecuteSplitToObject()
        {
            var sut = new StringToCourseConvertService();
            return sut.SplitToObjects(_inputString);
        }

        public List<CourseInstance> ExecuteConverToCourse()
        {
            var sut = new StringToCourseConvertService();
            return sut.ConvertToCourse(_objectStrings);
        }

        public List<CourseInstance> ExecuteConvert()
        {
            var sut = new StringToCourseConvertService();
            return sut.Convert(_inputString);
        }
    }
}
