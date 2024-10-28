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
    var questionPool = new []
    {
        new { 
        Question = "What is the name of the first President of the USA? FirstName LastName",
        Answer = "george washington"
        },
        new {
        Question = "What is 2 + 5",
        Answer = "7"
        },
        new {
        Question = "What is the capital of Texas",
        Answer = "austin"
        },
        new {
        Question = "Who wrote Green Eggs and Ham",
        Answer = "dr. seuss"
        }
    };

    var randQuestionInd = new Random().Next(questionPool.Length);

    var question = new
    {
        chosenQuestion = questionPool[randQuestionInd].Question,
        chosenAnswer = questionPool[randQuestionInd].Answer
    };

    return Results.Ok(question);
})

.WithName("GetJeopardyQuestion");

app.Run();
