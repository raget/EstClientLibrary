using System.Net.Http;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace EstClientLibrary.Tests
{
    public partial class EstClientTests
    {
        private static HttpClientHandler IgnoringServerCertificateHandler
        {
            get
            {
                var handler = new HttpClientHandler();
                // Do not ignore server certificate errors in production.
                // This is only for testing.
                handler.ServerCertificateCustomValidationCallback += (_, _, _, _) => true;
                return handler;
            }
        }

        private void SetupEstServerCACertificates(string expectedCACerts)
        {
            _server.Given(
                    Request.Create()
                        .UsingGet()
                        .WithPath("/.well-known/est/cacerts"))
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBody(expectedCACerts));
        }
    }
}