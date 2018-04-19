using System;
using System.Net;

namespace Testserver
{
    public interface IResponseRepository
    {
        MockedResponse GetResponse(HttpListenerRequest request);
    }
}