using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Testserver
{
    public class Server : IDisposable
    {
        private HttpListener listener;
        private IResponseRepository responseRepository;

        public Server(IResponseRepository responseRepository, params string[] prefixes)
        {
            this.responseRepository = responseRepository;
            this.listener = new HttpListener();
            foreach (var prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
            this.listener.Start();
            Task.Run(() => Start());
        }

       
        public void Start()
        {
            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;

                var mockedResponse = this.responseRepository.GetResponse(request);

                var response = context.Response;
                response.StatusCode = (int) mockedResponse.HttpStatusCode;
                var buffer = Encoding.ASCII.GetBytes(mockedResponse.Message);

                response.ContentLength64 = buffer.Length;
                var output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }

        public void Dispose()
        {
            listener.Stop();
        }
    }
}