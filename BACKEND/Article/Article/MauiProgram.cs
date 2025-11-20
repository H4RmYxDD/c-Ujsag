using Microsoft.Extensions.Logging;
using Windows.Services.Maps;
using ArticleServices;
using ArticleDataBase;

namespace ArticleMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            string connectionString =
                "Server=(localdb)\\mssqllocaldb;Database=Article;Trusted_Connection=True;MultipleActiveResultSets=true";

            builder.Services.AddDbContext<ArticleDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IService, Service>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
