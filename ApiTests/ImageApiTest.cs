using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ImageApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace ApiTests
{
    public class ImageApiTest
    {
        private TestServer _server;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public async Task  UploadFileTest()
        {
            var formData = new MultipartFormDataContent();
            await using var fs = File.OpenRead(Path.Combine("TestsData","Cat03.jpg"));
            formData.Add(new StreamContent(fs), "file", "cat");
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/image")
            {
                Content = formData
            };
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        
        [Test]
        public async Task  PreviewTest()
        {
            var formData = new MultipartFormDataContent();
            await using var fs = File.OpenRead(Path.Combine("TestsData","Cat03.jpg"));
            formData.Add(new StreamContent(fs), "file", "cat");
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/image?type=preview")
            {
                Content = formData
            };
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
     
    }
}