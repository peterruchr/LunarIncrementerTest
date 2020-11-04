using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace WaffleSupply.AcceptanceTests.ApiHelpers
{
    public class WaffleSupplyApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public WaffleSupplyApiHelper(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["RestApiUrl"];
        }

        public Task<HttpResponseMessage> AdjustWaffleSupply(int adjustmentAmount)
        {
            var jsonBody = JsonConvert.SerializeObject(new
            {
                WaffleAdjustment = adjustmentAmount
            });

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/api/v1/waffle-supplies")
            {
                Content = new StringContent(jsonBody, Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            return _httpClient.SendAsync(httpRequest);
        }

        public Task<HttpResponseMessage> GetWaffleSupply()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/v1/waffle-supplies");

            return _httpClient.SendAsync(httpRequest);
        } 
    }
}