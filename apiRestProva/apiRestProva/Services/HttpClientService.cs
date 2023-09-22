using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http;

namespace apiRestProva.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://localhost:7117/Article/");

            
        }

        public async Task<HttpResponseMessage> Get(string endpoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress+endpoint);
            return await Task.FromResult(response);
            
            
        }
    }
}