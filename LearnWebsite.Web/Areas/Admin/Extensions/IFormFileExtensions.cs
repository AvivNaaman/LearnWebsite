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
        public static async Task<string> ScaleAndConvertToBase64(this IFormFile file, bool IncludeDataPrefix = true)
        {
            // if invalid extension, return empty string
            if (!AllowedImageExtensions.Any(extension => extension == Path.GetExtension(file.FileName))) {
                return string.Empty;
            }
            // check if scale required:
            using (MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                ms.Position = 0;
                using (Image img = Image.Load(ms))
                {
                    int width = img.Width;
                    // if too big, scale it:
                    if (width > MaxImageWidth)
                    {
                        width = MaxImageWidth;
                        img.Mutate(i => i.Resize(width, 0));
                    }
                    await ms.FlushAsync();
                    ms.Position = 0;
                    img.SaveAsJpeg(ms);
                    var b64 = Convert.ToBase64String(ms.ToArray());
                    if (IncludeDataPrefix)
                    {
                        b64 = "data:img/jpg;base64," + b64;
                    }
                    return b64;
                }
            }
        }
    }
}
