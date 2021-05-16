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

        /// <summary>
        /// Gets current EST CA certificate(s) from the EST server.
        /// The EST client is assumed to perform this operation before performing other operations.
        /// </summary>
        /// <seealso cref="https://datatracker.ietf.org/doc/html/rfc7030#section-2.1"/>
        /// <returns>CA certificate(s) of the EST server</returns>
        public async Task<string> GetCACertificates()
        {
            using var response = await _client.GetAsync("cacerts").ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }

}