using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageApi.FileProcessors
{
    public class PreviewProcessor: BaseImageProcessor
    {
        public async Task CreatePreview(IEnumerable<IFormFile> files)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length <= 0) continue;
                await using var imageStream =  formFile.OpenReadStream();
                var image =  Image.Load(imageStream,  out Format);
                image.Mutate(i => i.Resize(100, 100)); 
                await image.SaveAsync($"{Guid.NewGuid()}.{Format.Name}");
            }
        }

        public PreviewProcessor(IWebHostEnvironment environment) : base(environment)
        {
        }
    }
}