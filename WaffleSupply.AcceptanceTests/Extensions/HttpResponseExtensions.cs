using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WaffleSupply.AcceptanceTests.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<TBody> GetBodyAsync<TBody>(this HttpResponseMessage message)
        {
            var jsonBody = await message.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TBody>(jsonBody);
        }    
    }
}