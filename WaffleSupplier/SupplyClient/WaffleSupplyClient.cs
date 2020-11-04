using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace WaffleSupplier.SupplyClient
{
    public class WaffleSupplyClient : IWaffleSupplyClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public WaffleSupplyClient(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _baseUrl = configuration["RestApiUrl"];
            _httpClient = httpClient;
        }
        
        public async Task AdjustWaffleSupply(int adjustSupplyAmount, CancellationToken cancellationToken = default)
        {
            var jsonBody = JsonConvert.SerializeObject(new
            {
                WaffleAdjustment = adjustSupplyAmount
            });

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/api/v1/waffle-supplies")
            {
                Content = new StringContent(jsonBody, Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if(response.IsSuccessStatusCode == false)
                throw new WaffleSupplyAdjustmentException();
        }

        public async Task<WaffleSupplyResponse> GetWaffleSupply(CancellationToken cancellationToken = default)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/v1/waffle-supplies");

            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if(response.IsSuccessStatusCode == false)
                throw new WaffleSupplyException();

            var jsonBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WaffleSupplyResponse>(jsonBody);
        }
    }
}