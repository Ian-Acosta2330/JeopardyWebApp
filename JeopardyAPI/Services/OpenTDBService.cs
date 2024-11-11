using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JeopardyAPI.Services
{
    public class OpenTDBService
    {
        private readonly HttpClient _httpClient;

        public OpenTDBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://opentdb.com/");
        }

        // Retrieves a list of available categories
        public async Task<IEnumerable<Category>?> GetCategoriesAsync()
        {
            var url = "api_category.php";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CategoryResponse>(content);
                return result?.TriviaCategories;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return null;
            }
        }

        // Retrieves trivia questions by category and limits to max 5 questions
        public async Task<IEnumerable<TriviaQuestion>?> GetQuestionsByCategoryAsync(int categoryId, int amount = 5)
        {
            amount = Math.Min(amount, 5); // Limit to 5 questions per category
            var url = $"api.php?amount={amount}&category={categoryId}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TriviaResponse>(content);
                return result?.Results;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return null;
            }
        }
    }

    // Model for the API response containing categories
    public class CategoryResponse
    {
        [JsonPropertyName("trivia_categories")]
        public List<Category> TriviaCategories { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    // Model for the trivia response from OpenTDB
    public class TriviaResponse
    {
        [JsonPropertyName("results")]
        public List<TriviaQuestion> Results { get; set; }
    }

    public class TriviaQuestion
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("correct_answer")]
        public string Answer { get; set; }

        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("incorrect_answers")]
        public List<string> IncorrectAnswers { get; set; }
    }
}
