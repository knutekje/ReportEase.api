using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using ReportEase.api.Repositories;
using ReportEase.api.Services;
//using ReportEase.api.Utils;

var builder = WebApplication.CreateBuilder(args);

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbContext>();

// Register repositories
builder.Services.AddTransient<FoodItemRepository>();
builder.Services.AddTransient<FoodWasteReportRepository>();
builder.Services.AddTransient<PhotoRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowShit",
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
        });
});
// Register services
builder.Services.AddTransient<FoodItemService>();
builder.Services.AddTransient<PhotoService>();
builder.Services.AddTransient<FoodWasteReportService>();

// Add controllers
builder.Services.AddControllers();


/*builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<ComplexFormDataOperationFilter>();
});*/


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowShit");
// Add middleware
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();