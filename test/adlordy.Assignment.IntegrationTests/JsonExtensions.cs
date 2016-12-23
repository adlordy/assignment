using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace adlordy.Assignment.IntegrationTests
{
    public static class JsonExtensions
    {
        public static async Task<T> AsJson<T>(this HttpContent content, JsonSerializer serializer)
        {
            using (var ms = new MemoryStream())
            {
                await content.CopyToAsync(ms);
                ms.Position = 0;
                using (var streamReader = new StreamReader(ms))
                using (var reader = new JsonTextReader(streamReader))
                    return serializer.Deserialize<T>(reader);
            }
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string uri, T data, JsonSerializer serializer)
        {
            using (var ms = new MemoryStream())
            using (var streamWriter = new StreamWriter(ms))
            using (var writer = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(writer, data);
                writer.Flush();
                return await client.PutAsync(uri, new StringContent(Encoding.UTF8.GetString(ms.ToArray()), Encoding.UTF8, "application/json"));
            }
        }
    }
}
