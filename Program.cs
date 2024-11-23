using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using ReportEase.api.Repositories;
using ReportEase.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Configure MongoDB settings and context
/*builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));*/

builder.Services.AddSingleton<MongoDbContext>();

// Register repositories
builder.Services.AddTransient<FoodItemRepository>();
builder.Services.AddTransient<FoodWasteReportRepository>();

// Register services
builder.Services.AddTransient<FoodItemService>();
builder.Services.AddTransient<FoodWasteReportService>();

// Add controllers
builder.Services.AddControllers();

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add middleware
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();