using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Models.Entities
{
    /// <summary>
    /// Respresent a single course.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// The primary key of the course
        /// </summary>
        public int Id { get; set; }

        [StringLength(40, MinimumLength = 0)]
        [Display(Name = "(optional) The name of the course in the address bar. Auto-generated if not specified.")]
        [RegularExpression(@"^([\w\d-_])*$", ErrorMessage = "Choose a url name which contains only numbers, digits, hyphen and underbar, or choose a course name with more english chars.")]
        /// <summary>
        /// Respresnts the course in the url routing.
        /// </summary>
        public string UrlName { get; set; }
        [Required]
        /// <summary>
        /// Course display name
        /// </summary>
        public string DisplayName { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        /// <summary>
        /// Course description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Viewed counter
        /// </summary>
        public int Viewed { get; set; }

        /// <summary>
        /// Date created
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// Image URL source (positioned in img src)
        /// </summary>
        public string ImageSrc { get; set; }

        /// <summary>
        /// The learning units of the course
        /// </summary>
        public List<CourseUnit> Units { get; set; }
    }
}
