﻿
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using adlordy.Assignment.Models;

namespace adlordy.Assignment.IntegrationTests
{
    public class DiffClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializer _serializer;
        private readonly string _url;
        private const string basePath = "/v1/diff/";

        public DiffClient(string url)
        {
            _url = url;
            _client = new HttpClient {Timeout = TimeSpan.FromSeconds(5)};
            _serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public async Task<DiffResult> GetAsync(string id)
        {
            var url = CreateGetUri(id);
            var response = await _client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound)
                return null;
            if (response.StatusCode == HttpStatusCode.OK)
                return await response.Content.AsJson<DiffResult>(_serializer);
            throw new HttpRequestException($"Unexpected response status code {response.StatusCode} from {url}");
        }

        public Task PutLeftAsync(string id, byte[] left)
        {
            var url = CreateLeftUri(id);
            return PutAsync(url, left);
        }

        public Task PutRightAsync(string id, byte[] right)
        {
            var url = CreateRightUri(id);
            return PutAsync(url, right);
        }

        private async Task PutAsync(string url, byte[] data)
        {
            var response = await _client.PutAsJsonAsync(url, new DiffModel { Data = data }, _serializer);
            if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.Created)
                throw new ArgumentException("Endpoint did not accept request. Status Code: " + response.StatusCode);
        }

        private string CreateGetUri(string id)
        {
            return $"{_url}{basePath}{id}";
        }

        private string CreateLeftUri(string id)
        {
            return $"{_url}{basePath}{id}/left";
        }

        private string CreateRightUri(string id)
        {
            return $"{_url}{basePath}{id}/right";
        }




    }
}
