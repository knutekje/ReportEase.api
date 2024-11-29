using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;


using ReportEase.api.Repositories;
using ReportEase.api.Services;


var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext() 
    .WriteTo.Console() 
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Logs to a file
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddTransient<DiscrepancyRepository>();
builder.Services.AddTransient<TemperatureReadingRepository>();

builder.Services.AddTransient<FoodItemRepository>();
builder.Services.AddTransient<FoodWasteReportRepository>();
builder.Services.AddTransient<PhotoRepository>();
builder.Services.AddTransient<TemperatureReadingService>();
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

builder.Services.AddTransient<FoodItemService>();
builder.Services.AddTransient<PhotoService>();
builder.Services.AddTransient<FoodWasteReportService>();
builder.Services.AddTransient<DiscrepancyService>();
builder.Services.AddTransient<TemperatureReadingRepository>();



builder.Services.AddControllers();





var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowShit");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();