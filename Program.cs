using System;
using System.Net;
using System.Net.Http;
using Moq;

namespace Testserver
{
    class Program
    {
        static void Main(string[] args)
        {


            var responseRepo = new Mock<IResponseRepository>();
            responseRepo.Setup(r =>r.GetResponse(It.Is<HttpListenerRequest>(rq => rq.Url.AbsolutePath.Contains("api/v1/search"))))
                .Returns(new MockedResponse { HttpStatusCode = HttpStatusCode.OK, Message = "Blah blah"});

            Console.WriteLine("Hello World!");
            using (new Server(responseRepo.Object, "http://localhost:6000/"))
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(new Uri("http://localhost:6000/api/v1/search")).Result;
                    var responseString = response.Content.ReadAsStringAsync().Result;

                    if (responseString.Contains("Blah"))
                    {
                        Console.WriteLine(":)");
                    }
                    else
                    {
                        Console.WriteLine(":(");
                    }
                }
                
            }
            Console.WriteLine("done");
            Console.ReadKey();
            

        }
    }
}
