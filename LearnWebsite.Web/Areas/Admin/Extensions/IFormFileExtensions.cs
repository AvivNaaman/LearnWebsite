using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Areas.Admin.Extensions
{
    public static class IFormFileExtensions
    {
        public static readonly List<string> AllowedImageExtensions = new List<string> { ".jpg", ".png", ".gif" };
        public const int MaxImageWidth = 1000;
        public static async Task<string> ScaleAndConvertToBase64(this IFormFile file)
        {
            // if invalid extension, return empty string
            if (!AllowedImageExtensions.Any(extension => extension == Path.GetExtension(file.Name)) {
                return string.Empty;
            }
            // check if scale required:
            using (MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                using (Image img = Image.Load(ms))
                {
                    int width = img.Width;
                    // if too big, scale it:
                    if (width > MaxImageWidth)
                    {
                        width = MaxImageWidth;
                        img.Mutate(i => i.Resize(width, 0));
                    }
                    img.SaveAsJpeg(ms);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}
