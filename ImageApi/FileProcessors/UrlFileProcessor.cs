using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;

namespace ImageApi.FileProcessors
{
    public class UrlFileProcessor : BaseImageProcessor
    {
        public async Task Save(string url)
        {
            using WebClient webClient = new WebClient();
            var imageBytes = webClient.DownloadData(url);
            var image = Image.Load(imageBytes,  out Format);
            await image.SaveAsync($"{Guid.NewGuid()}.{Format.Name}");
        }

        public UrlFileProcessor(IWebHostEnvironment environment) : base(environment)
        {
        }
    }
}