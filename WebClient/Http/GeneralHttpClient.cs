using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Http
{
    public class GeneralHttpClient : IHttpClient
    {
        private static readonly HttpClient _client = new ();

        public async Task<string> GetStringAsync(string uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri); // HttpMethod is an helper class for retriving and comparing standard http methods and for creating new Http methods
            var response = await _client.SendAsync(requestMessage); // send an Http request as asynchronous operation to the web api end point
            return await response.Content.ReadAsStringAsync(); // read the response content as string
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T item)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json") // serialize the object to json and set it as content of the request message
            };
        
            //requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item)); // serialize the object to json and set it as content of the request message
            var response = await _client.SendAsync(requestMessage); // send an Http request as asynchronous operation to the web api end point
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
            return response;
        }
    }
}
