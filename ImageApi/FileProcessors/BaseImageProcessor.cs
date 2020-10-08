using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp.Formats;

namespace ImageApi.FileProcessors
{
    public class BaseImageProcessor
    {
        protected IWebHostEnvironment Environment;
        protected  IImageFormat Format;
        public BaseImageProcessor(IWebHostEnvironment environment)
        {
            Environment = environment;
        }
    }
}