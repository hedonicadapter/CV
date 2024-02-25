using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CV.Interfaces;

namespace CV.Services
{
    public class ItemClient : IItemClient
    {
        private readonly HttpClient _httpClient;

        public ItemClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(System.Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:5178");
        }

        public async Task<bool> DeleteItemAsync(int id, string itemType)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"api/{itemType}/{id}");
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}