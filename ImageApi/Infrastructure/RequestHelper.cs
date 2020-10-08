using System.IO;
using System.Threading.Tasks;

namespace ImageApi.Infrastructure
{
    public static class RequestHelper
    {
        public static async Task<string> GetStringFromBody(Stream bodyAsStream)
        {
            using var reader = new StreamReader(bodyAsStream);
            var data = await reader.ReadToEndAsync();
            return data;
        }
    }
}