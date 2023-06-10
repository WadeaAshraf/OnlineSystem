using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace E_CommercePortal.Models
{
    public class CallingAPI
    {
        private readonly HttpClient httpClient;
        private string Token;
        public CallingAPI(string baseurl,string token=null) {
            httpClient = new HttpClient { BaseAddress = new Uri(baseurl) };
            
            if (token!=null)
            {
                Token = token;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        public async Task<TResponse> GetObjectFromAPIAsync<TResponse>(string endpoint)
        {
            
            var response = await httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}: {response.Content}");
            }


        }
        public  async Task<List<TResponse>> GetDataFromAPIAsync<TResponse>(string endpoint)
        {
            
            var response = await httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TResponse>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}: {response.Content}");
            }

            
        }
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            
            var serializedData = JsonConvert.SerializeObject(data);
            var content = new StringContent(serializedData, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(endpoint, content);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}: {response.Content}");
            }
        }

    }
}
