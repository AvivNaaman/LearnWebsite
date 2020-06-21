using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Models.Entities
{
    /// <summary>
    /// Represents a single page of a course unit
    /// </summary>
    public class CoursePage
    {
        /// <summary>
        /// The primary key of the page
        /// </summary>
        public int Id { get; set; }
        [Required]
        [StringLength(70, MinimumLength = 2)]
        /// <summary>
        /// The title of the page
        /// </summary>
        public string Title { get; set; }
        [Display(Name = "Page content")]
        /// <summary>
        /// The content of the page, as HTML
        /// </summary>
        public string HtmlContent { get; set; }
        [Required]
        /// <summary>
        /// The primary key of the page's unit
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// The unit which contains the page
        /// </summary>
        public CourseUnit Unit { get; set; }
        public int InUnitOrder { get; set; }
        [StringLength(40, MinimumLength = 0)]
        [Display(Name = "(optional) The name of the course in the address bar. Auto-generated if not specified.")]
        [RegularExpression(@"^([\w\d-_])*$", ErrorMessage = "Choose a url name which contains only numbers, digits, hyphen and underbar, or choose a course name with more english chars.")]
        public string UrlName { get; set; }
    }
}
