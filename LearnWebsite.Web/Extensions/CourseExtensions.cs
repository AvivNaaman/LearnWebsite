using LearnWebsite.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Extensions
{
    public static class CourseExtensions
    {
        /// <summary>
        /// Sorts the units and the pages inside a course by their defined order
        /// </summary>
        /// <param name="course">The course to order</param>
        /// <returns>The course</returns>
        public static Course SortUnitsAndPages(this Course course)
        {
            // order units
            course.Units = course.Units.OrderBy(u => u.InCourseOrder).ToList();
            // order pages inside units
            for (int i = 0; i < course.Units.Count; i++)
            {
                course.Units[i].Pages = course.Units[i].Pages.OrderBy(p => p.InUnitOrder).ToList();
            }
            return course;
        }
    }
}
