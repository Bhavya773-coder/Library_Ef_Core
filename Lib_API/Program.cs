global using Lib_API.Models;

global using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<LibManagementContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<Author>, AuthorValidator>();
builder.Services.AddScoped<IValidator<Book>, BookValidator>();
builder.Services.AddScoped<IValidator<BookIssue>, BookIssueValidator>();
builder.Services.AddScoped<IValidator<StudentProfile>, StudentProfileValidator>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    // ... existing code ...

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; // This makes Swagger UI the default landing page
    });

    app.UseHttpsRedirection();
// ... existing code ...
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();