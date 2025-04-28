using Bot.Handlers;
using Bot.Interfaces;
using Bot.Services;
using Telegram.Bot;

namespace Bot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<ITelegramBotClient>(sp =>
                new TelegramBotClient(builder.Configuration["Telegram:BotToken"]!)
            );

            builder.Services.AddHttpClient<IBackendService, BackendService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["Backend:BaseUrl"]!)
            );
            builder.Services.AddScoped<ITelegramService, TelegramService>();
            builder.Services.AddScoped<CommandHandler>(sp =>
                new CommandHandler(
                    sp.GetRequiredService<ITelegramService>(),
                    builder.Configuration["WebApp:Url"]!
                )
            );
            builder.Services.AddScoped<MessageHandler>();
            builder.Services.AddScoped<UpdateHandler>();

            builder.Services.AddHostedService<BotHostedService>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();
            app.Urls.Add("http://0.0.0.0:8081");
            app.Run();
        }
    }
}
