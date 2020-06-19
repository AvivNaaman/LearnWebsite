using LearnWebsite.Web.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Areas.Admin.Models.ViewModels
{
    public class EditCourseViewModel : Course
    {
        /// <summary>
        /// The course's image upload field
        /// </summary>
        public IFormFile ImageUpload { get; set; }
        /// <summary>
        /// a 2-d int array, JSON serialized
        /// </summary>
        public string UnitsPagesOrder { get; set; }
    }
}
