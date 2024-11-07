using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

public class CluebaseService
{
    private readonly HttpClient _httpClient;

    public CluebaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Question>?> GetQuestionsAsync(int limit = 100, string orderBy = "category", string sort = "desc")
    {
        // Construct the request URL with query parameters
        var url = $"clues?limit={limit}&order_by={orderBy}&sort={sort}";

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<Question>>();
        }
        return null;
    }
}
