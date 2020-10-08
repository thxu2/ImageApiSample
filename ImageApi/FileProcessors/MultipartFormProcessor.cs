using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace ImageApi.FileProcessors
{
    public class MultipartFormProcessor : BaseImageProcessor
    {
        public MultipartFormProcessor(IWebHostEnvironment environment) : base(environment)
        {
        }

        public async Task Save(IEnumerable<IFormFile> files)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length <= 0) continue;
                await using var imageStream = formFile.OpenReadStream();
                var image = Image.Load(imageStream, out Format);
                await image.SaveAsync(Path.Combine(Environment.ContentRootPath,$"{Guid.NewGuid()}.{Format.Name}"));
            }
        }
    }
}