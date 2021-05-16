using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EstClientLibrary
{
    public class EstClient
    {
        private readonly HttpClient _client;
        public EstClient(string host, int port, HttpClientHandler handler = null)
        {
            var baseUri = new UriBuilder("https", host, port, ".well-known/est/");
            handler ??= new HttpClientHandler();
            _client = new HttpClient(handler)
            {
                BaseAddress = baseUri.Uri
            };
        }

        public async Task<string> GetCACertificates()
        {
            using var response = await _client.GetAsync("cacerts").ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }

}