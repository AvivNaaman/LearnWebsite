using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        /// <summary>
        /// The friendly name of the unit
        /// </summary>
        public string DisplayName { get; set; }
        public List<CoursePage> Pages { get; internal set; }
        public int InCourseOrder { get; set; }
        [StringLength(40, MinimumLength = 0)]
        [Display(Name = "(optional) The name of the course in the address bar. Auto-generated if not specified.")]
        [RegularExpression(@"^([\w\d-_])*$", ErrorMessage = "Choose a url name which contains only numbers, digits, hyphen and underbar, or choose a course name with more english chars.")]
        public string UrlName { get; set; }
    }
}
