using System.Net;

namespace RKSoft.eShop.Api.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public dynamic?  Data { get; set; }
        public List<string>? Errors { get; set; }
    }
}
