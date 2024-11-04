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
            new { Question = "What is the capital of Texas?", Answer = "Austin",Hint="Stone Cold Steve ___",Points="100"},
            new { Question = "What is the capital of California?", Answer = "Sacramento",Hint="Also known as \"City of Trees\"",Points="200"},
            new { Question = "What is the capital of China?", Answer = "Beijing",Hint="Known as \"The Forbidden City\"",Points="300"}
        };

        var mathPool = new[]
        {
            new { Question = "What is 2 + 5?", Answer = "7",Hint="It ate 9",Points="100"},
            new { Question = "What is 3 + 5?", Answer = "8",Hint="Eaten by 7",Points="200"},
            new { Question = "What is 5 x 12?", Answer = "60",Hint="Carry the one",Points="300"}
        };

        var historyPool = new[]
        {
            new { Question = "Who was the first President of the USA?",Answer = "George Washington",Hint="Cherry Tree",Points="100"},
            new { Question = "Which president was assassinated by John Wilkes Booth?",Answer = "Abraham Lincoln",Hint="Shot on 14th, Died 15th, 16th President",Points="200"},
            new { Question = "What year was the Wii released?",Answer = "2006",Hint="The year Italy won the FIFA World Cup",Points="300"}
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

