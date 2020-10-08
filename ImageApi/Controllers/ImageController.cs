using System;
using System.Threading.Tasks;
using ImageApi.Domain;
using ImageApi.FileProcessors;
using ImageApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly Base64Processor _base64Processor;
        private readonly MultipartFormProcessor _multipartFormProcessor;
        private readonly UrlFileProcessor _urlFileProcessor;
        private readonly PreviewProcessor _previewProcessor;

        public ImageController(Base64Processor base64Processor, MultipartFormProcessor multipartFormProcessor,
            UrlFileProcessor urlFileProcessor, PreviewProcessor previewProcessor)
        {
            _base64Processor = base64Processor;
            _multipartFormProcessor = multipartFormProcessor;
            _urlFileProcessor = urlFileProcessor;
            _previewProcessor = previewProcessor;
        }

        [HttpPost]
        public async Task<ActionResult> ProcessImage(
            [FromQuery] ImageType type)
        {
            switch (type)
            {
                case ImageType.Multipart:
                    var formData = Request.HttpContext.Request.Form.Files;
                    await _multipartFormProcessor.Save(formData);
                    break;
                case ImageType.Base64:
                    await _base64Processor.Save(await RequestHelper.GetStringFromBody(Request.Body));
                    break;
                case ImageType.Url:
                    await _urlFileProcessor.Save(await RequestHelper.GetStringFromBody(Request.Body));
                    break;
                case ImageType.Preview:
                    await _previewProcessor.CreatePreview(Request.HttpContext.Request.Form.Files);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return Ok();
        }
    }
}