using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Models.Entities
{
    /// <summary>
    /// Respresent a learning unit (a batch of pages)
    /// </summary>
    public class CourseUnit
    {
        /// <summary>
        /// The unit's primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The primary key of the course, which holds the unit
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// The course which holds the unit
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// The friendly name of the unit
        /// </summary>
        public string DisplayName { get; set; }
        public List<CoursePage> Pages { get; internal set; }
    }
}
