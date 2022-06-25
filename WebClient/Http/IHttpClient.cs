using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Http
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);
        Task<HttpResponseMessage> PostAsync<T>(string url, T item); // post data to the web api end point
    }
}
