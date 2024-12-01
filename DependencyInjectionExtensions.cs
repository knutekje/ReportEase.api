using ReportEase.api.Repositories;
using ReportEase.api.Services;

namespace ReportEase.api;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<DiscrepancyRepository>();
        services.AddTransient<TemperatureReadingRepository>();
        services.AddTransient<DailyTemperatureRecordRepository>();
        services.AddTransient<NotificationRepository>();
        services.AddTransient<FoodItemRepository>();
        services.AddTransient<FoodWasteReportRepository>();
        services.AddTransient<PhotoRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<DiscrepancyService>();
        services.AddTransient<DailyTemperatureRecordService>();
        services.AddTransient<NotificationService>();
        services.AddTransient<TemperatureReadingService>();
        services.AddTransient<FoodItemService>();
        services.AddTransient<PhotoService>();
        services.AddTransient<FoodWasteReportService>();

        return services;
    }
}