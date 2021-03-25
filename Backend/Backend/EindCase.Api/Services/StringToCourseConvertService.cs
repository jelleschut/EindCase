using EindCase.Api.Services.Interfaces;
using EindCase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EindCase.Api.Services
{
    public class StringToCourseConvertService : IStringToCourseConvertService
    {
        public List<CourseInstance> Convert(string inputString)
        {
            List<string> stringObjects = SplitToObjects(inputString);
            return ConvertToCourse(stringObjects);
        }

        //Public want anders niet testbaar
        public List<string> SplitToObjects(string inputString)
        {
            return inputString.Split("\r\n\r\n").ToList();
        }

        //Public want anders niet testbaar
        public List<CourseInstance> ConvertToCourse(List<string> stringObjects)
        {
            List<CourseInstance> courseInstanceList = new List<CourseInstance>();
            foreach (string s in stringObjects)
            {
                if (s.Length > 0)
                {
                    string[] properties = s.Split("\n");
                    string title = properties[0].Substring(7).Trim();
                    string code = properties[1].Substring(12).Trim();
                    int lengthInDays = int.Parse(properties[2].Trim().Substring(6, 1));
                    DateTime startDate = DateTime.Parse(properties[3].Trim().Substring(12));

                    Course course = new Course() { Title = title, Code = code, LengthInDays = lengthInDays };
                    courseInstanceList.Add(new CourseInstance() { StartDate = startDate, Course = course });
                }
            }
            return courseInstanceList;
        }
    }
}
