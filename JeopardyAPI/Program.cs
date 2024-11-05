using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


    var builder = WebApplication.CreateBuilder(args);
    builder.WebHost.UseUrls("http://localhost:5041");

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowAllOrigins");
    app.UseHttpsRedirection();

    app.MapGet("/jeopardy", () =>
    {
var geoPool = new[]
{
    // 100 points questions
    new { Question = "What is the capital of Texas?", Answer = "Austin", Hint = "Stone Cold Steve ___", Points = "100", answered=false},
    new { Question = "What is the capital of Australia?", Answer = "Canberra", Hint = "Not Sydney", Points = "100", answered=false},
    new { Question = "What is the capital of Canada?", Answer = "Ottawa", Hint = "Between Toronto and Montreal", Points = "100", answered=false },
    new { Question = "What is the capital of Brazil?", Answer = "Brasília", Hint = "Built in the 1960s", Points = "100", answered=false },
    new { Question = "What is the capital of Japan?", Answer = "Tokyo", Hint = "Home of the 2020 Summer Olympics", Points = "100", answered=false },
    new { Question = "What is the capital of Germany?", Answer = "Berlin", Hint = "Divided during the Cold War", Points = "100", answered=false },
    new { Question = "What is the capital of India?", Answer = "New Delhi", Hint = "Has a famous Presidential residence", Points = "100", answered=false },

    // 200 points questions
    new { Question = "What is the capital of California?", Answer = "Sacramento", Hint = "Also known as 'City of Trees'", Points = "200", answered=false },
    new { Question = "What is the capital of Russia?", Answer = "Moscow", Hint = "Home to the Kremlin", Points = "200", answered=false },
    new { Question = "What is the capital of Egypt?", Answer = "Cairo", Hint = "Near the Pyramids of Giza", Points = "200", answered=false },
    new { Question = "What is the capital of South Africa?", Answer = "Pretoria", Hint = "One of three capitals", Points = "200", answered=false },
    new { Question = "What is the capital of France?", Answer = "Paris", Hint = "City of Light", Points = "200", answered=false },
    new { Question = "What is the capital of Argentina?", Answer = "Buenos Aires", Hint = "Known for tango and football", Points = "200", answered=false },
    new { Question = "What is the capital of Spain?", Answer = "Madrid", Hint = "Home to Real Madrid", Points = "200", answered=false },

    // 300 points questions
    new { Question = "What is the capital of China?", Answer = "Beijing", Hint = "Known as 'The Forbidden City'", Points = "300", answered=false },
    new { Question = "What is the capital of Mexico?", Answer = "Mexico City", Hint = "One of the largest cities in the world", Points = "300", answered=false },
    new { Question = "What is the capital of South Korea?", Answer = "Seoul", Hint = "Technological hub", Points = "300", answered=false },
    new { Question = "What is the capital of Italy?", Answer = "Rome", Hint = "Ancient Empire's seat", Points = "300", answered=false },
    new { Question = "What is the capital of Turkey?", Answer = "Ankara", Hint = "Not Istanbul", Points = "300", answered=false },
    new { Question = "What is the capital of Sweden?", Answer = "Stockholm", Hint = "Home to the Nobel Prize", Points = "300", answered=false },
    new { Question = "What is the capital of Thailand?", Answer = "Bangkok", Hint = "City of temples and palaces", Points = "300", answered=false }
};

var mathPool = new[]
{
    // 100 points questions
    new { Question = "What is 2 + 5?", Answer = "7", Hint = "It ate 9", Points = "100", answered=false},
    new { Question = "What is 3 + 8?", Answer = "11", Hint = "Prime number", Points = "100", answered=false },
    new { Question = "What is 6 x 2?", Answer = "12", Hint = "Double of 6", Points = "100", answered=false },
    new { Question = "What is 10 - 4?", Answer = "6", Hint = "Half a dozen", Points = "100", answered=false },
    new { Question = "What is 9 + 1?", Answer = "10", Hint = "Base number in decimal", Points = "100", answered=false },
    new { Question = "What is 2 x 6?", Answer = "12", Hint = "Same as a dozen", Points = "100", answered=false },
    new { Question = "What is 5 + 5?", Answer = "10", Hint = "Fingers on both hands", Points = "100", answered=false },

    // 200 points questions
    new { Question = "What is 3 + 5?", Answer = "8", Hint = "Eaten by 7", Points = "200", answered=false },
    new { Question = "What is 12 ÷ 4?", Answer = "3", Hint = "Dividing into groups", Points = "200", answered=false },
    new { Question = "What is 15 - 7?", Answer = "8", Hint = "The answer is even", Points = "200", answered=false },
    new { Question = "What is 7 x 6?", Answer = "42", Hint = "Meaning of life, according to Hitchhiker's Guide", Points = "200", answered=false },
    new { Question = "What is 4 x 8?", Answer = "32", Hint = "Double of 16", Points = "200", answered=false },
    new { Question = "What is 16 ÷ 4?", Answer = "4", Hint = "Equal groups", Points = "200", answered=false },
    new { Question = "What is 20 ÷ 2?", Answer = "10", Hint = "Half of 20", Points = "200", answered=false },

    // 300 points questions
    new { Question = "What is 5 x 12?", Answer = "60", Hint = "Carry the one", Points = "300", answered=false },
    new { Question = "What is 18 ÷ 6?", Answer = "3", Hint = "Triad", Points = "300", answered=false },
    new { Question = "What is 144 ÷ 12?", Answer = "12", Hint = "Dozen", Points = "300", answered=false },
    new { Question = "What is the square root of 81?", Answer = "9", Hint = "3 squared", Points = "300", answered=false },
    new { Question = "What is 7 x 9?", Answer = "63", Hint = "Multiple of 9", Points = "300", answered=false },
    new { Question = "What is 6 x 8?", Answer = "48", Hint = "The same as 4 dozen", Points = "300", answered=false },
    new { Question = "What is the cube root of 27?", Answer = "3", Hint = "Cube", Points = "300", answered=false }
};

var historyPool = new[]
{
    // 100 points questions
    new { Question = "Who was the first President of the USA?", Answer = "George Washington", Hint = "Cherry Tree", Points = "100", answered=false },
    new { Question = "What year did the Titanic sink?", Answer = "1912", Hint = "The ship was called unsinkable", Points = "100", answered=false },
    new { Question = "Who discovered America?", Answer = "Christopher Columbus", Hint = "Sailed in 1492", Points = "100", answered=false },
    new { Question = "What year did World War I begin?", Answer = "1914", Hint = "The war to end all wars", Points = "100", answered=false },
    new { Question = "Who invented the telephone?", Answer = "Alexander Graham Bell", Hint = "Can you hear me now?", Points = "100", answered=false },
    new { Question = "What year did World War II end?", Answer = "1945", Hint = "V-E Day", Points = "100", answered=false },
    new { Question = "Who was the first man on the moon?", Answer = "Neil Armstrong", Hint = "One small step", Points = "100", answered=false },

    // 200 points questions
    new { Question = "Which president was assassinated by John Wilkes Booth?", Answer = "Abraham Lincoln", Hint = "Shot on 14th, Died 15th, 16th President", Points = "200", answered=false },
    new { Question = "What year did the Berlin Wall fall?", Answer = "1989", Hint = "End of the Cold War", Points = "200", answered=false },
    new { Question = "Who was the British Prime Minister during WWII?", Answer = "Winston Churchill", Hint = "We shall fight on the beaches", Points = "200", answered=false },
    new { Question = "Who wrote the Declaration of Independence?", Answer = "Thomas Jefferson", Hint = "3rd President of the USA", Points = "200", answered=false },
    new { Question = "Who led the Civil Rights Movement in the 1960s?", Answer = "Martin Luther King Jr.", Hint = "I have a dream", Points = "200", answered=false },
    new { Question = "What year was Pearl Harbor attacked?", Answer = "1941", Hint = "A date which will live in infamy", Points = "200", answered=false },
    new { Question = "Who was the 40th president of the USA?", Answer = "Ronald Reagan", Hint = "Former actor", Points = "200", answered=false },

    new { Question = "What year did the Roman Empire fall?", Answer = "476 AD", Hint = "The end of ancient rome", Points = "300", answered=false },
    new { Question = "Who led the Soviet Union during World War II?", Answer = "Joseph Stalin", Hint = "Dictator with bushy moustache", Points = "300", answered=false },
    new { Question = "Who was the first Emperor of China?", Answer = "Qin Shi Huang", Hint = "Known for the Great Wall", Points = "300", answered=false },
    new { Question = "Who was the first female pharaoh of Egypt?", Answer = "Hatshepsut", Hint = "Ruled over 20 years", Points = "300", answered=false },
    new { Question = "Who was the longest-reigning British monarch before Queen Elizabeth II?", Answer = "Queen Victoria", Hint = "Victorian era", Points = "300", answered=false },
    new { Question = "In what year did the Great Depression start?", Answer = "1929", Hint = "Known for the stock market crash", Points = "300", answered=false },
    new { Question = "What year did the civil war start?", Answer = "1861", Hint = "Started the same year as Lincoln's presidential term", Points = "300", answered=false },

};

        var questions = new
        {
            geography = geoPool,
            math = mathPool,
            history = historyPool
        };

        return Results.Ok(questions);
    })
    .WithName("GetJeopardyQuestions");

    app.Run();

