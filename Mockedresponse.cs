using System.Net;

namespace Testserver
{
    public class MockedResponse
    {
        public string Message { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}