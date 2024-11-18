using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeopardyAPI.Services;

namespace JeopardyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly OpenTDBService _openTDBService;

        public TriviaController(OpenTDBService openTDBService)
        {
            _openTDBService = openTDBService;
        }

        /// <summary>
        /// Retrieves 5 questions from a random category.
        /// </summary>
        /// <param name="amount">The number of questions to retrieve (default is 5).</param>
        /// <returns>A list of trivia questions from a random category.</returns>
        [HttpGet("randomcategory/withquestions")]
        public async Task<IActionResult> GetQuestionsFromRandomCategory([FromQuery] int amount = 5)
        {
            // Step 1: Get all available categories
            var categories = await _openTDBService.GetCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found or API call failed.");
            }

            // Step 2: Select a random category
            var randomCategory = categories.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
            if (randomCategory == null)
            {
                return NotFound("Failed to select a random category.");
            }

            // Step 3: Fetch questions for the random category
            var questions = await _openTDBService.GetQuestionsByCategoryAsync(randomCategory.Id, amount);
            if (questions == null || !questions.Any())
            {
                return NotFound("No questions found for the selected random category.");
            }

            return Ok(new
            {
                Category = randomCategory.Name,
                Questions = questions
            });
        }

        /// <summary>
        /// Retrieves trivia questions from 6 random categories.
        /// </summary>
        /// <param name="amount">The number of questions to retrieve per category (default is 5).</param>
        /// <returns>A list of categories with their respective trivia questions.</returns>
        [HttpGet("randomcategories/withquestions")]
        public async Task<IActionResult> GetQuestionsFromMultipleRandomCategories([FromQuery] int amount = 5)
        {
            // Step 1: Get all available categories
            var categories = await _openTDBService.GetCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found or API call failed.");
            }

            // Step 2: Prepare a list to store questions per category
            var categoryQuestions = new List<object>();

            // Step 3: Fetch questions for 6 random categories, with delays between requests
            var randomCategories = categories.OrderBy(c => Guid.NewGuid()).Take(6).ToList();
            var tasks = new List<Task>();

            foreach (var randomCategory in randomCategories)
            {
                tasks.Add(FetchCategoryQuestionsWithPatience(randomCategory, amount, categoryQuestions));
                // Introduce a 2-second delay before starting the next category request
                await Task.Delay(5000); // 2-second delay between requests
            }

            // Wait for all the tasks to complete
            await Task.WhenAll(tasks);

            return Ok(categoryQuestions);
        }

        /// <summary>
        /// Helper method to fetch questions for a category with patience and retries.
        /// </summary>
        private async Task FetchCategoryQuestionsWithPatience(Category randomCategory, int amount, List<object> categoryQuestions)
        {
            int retryCount = 3;
            bool success = false;

            while (retryCount > 0 && !success)
            {
                try
                {
                    // Fetch questions for the current category
                    var questions = await _openTDBService.GetQuestionsByCategoryAsync(randomCategory.Id, amount);
                    if (questions != null && questions.Any())
                    {
                        categoryQuestions.Add(new
                        {
                            Category = randomCategory.Name,
                            Questions = questions
                        });
                        success = true; // Mark success if data is retrieved
                    }
                    else
                    {
                        throw new Exception("No questions found for the selected category.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    if (ex.Message.Contains("429"))
                    {
                        // If rate limit error (429), retry after delay
                        await Task.Delay(10000); // Delay for 10 seconds before retry
                    }
                    else
                    {
                        // Handle other types of exceptions
                        throw new Exception($"Request failed: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle other types of errors
                    Console.WriteLine($"Error fetching questions: {ex.Message}");
                    break;
                }
                finally
                {
                    retryCount--;
                }
            }
        }
    }
}
