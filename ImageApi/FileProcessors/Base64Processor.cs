using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;

namespace ImageApi.FileProcessors
{
    public class Base64Processor : BaseImageProcessor
    {
        public async Task Save(string data)
        {
            var imageBytes = Convert.FromBase64String(data);
            var image = Image.Load(imageBytes,  out Format);
            await image.SaveAsync($"{Guid.NewGuid()}.{Format.Name}");
        }

        public Base64Processor(IWebHostEnvironment environment) : base(environment)
        {
        }
    }
}