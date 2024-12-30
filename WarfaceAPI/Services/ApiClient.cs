using Newtonsoft.Json;

namespace WarfaceAPI.Services;

public class ApiClient(IHttpClientFactory httpClientFactory)
{
    public async Task<T?> SendApiRequestAsync<T>(string url)
    {
        var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
        
        return default;
    }
}