using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using WireMock.Server;

namespace EstClientLibrary.Tests
{
    public partial class EstClientTests
    {
        private WireMockServer _server;
        private const int MockServerPort = 9999;

        [SetUp]
        public void Setup()
        {
            _server = WireMockServer.Start(MockServerPort, true);
        }

        [TearDown]
        public void TearDown()
        {
            _server.Stop();
        }

        [Test]
        [TestCase("localhost", MockServerPort)]
        [TestCase("testrfc7030.com", 8443)]
        public async Task GetCertificationAuthorities(string hostname, int port)
        {
            var expectedCACerts = await File.ReadAllTextAsync("cacerts.p7").ConfigureAwait(false);
            SetupEstServerCACertificates(expectedCACerts);
            var estClient = new EstClient(hostname, port, IgnoringServerCertificateHandler);
            var cacerts = await estClient.GetCACertificates().ConfigureAwait(false);

            Assert.That(cacerts, Is.EqualTo(expectedCACerts));
        }
    }
}