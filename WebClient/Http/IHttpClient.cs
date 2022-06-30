using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Http
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string url);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item); // post data to the web api end point
    }
}
