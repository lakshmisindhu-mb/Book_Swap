using System.Net;

namespace Book_Swap_API.Model
{
    public class Response
    {
        public HttpStatusCode statusCode { get; set; }
        public string statusMessage { get; set; }
    }
}
