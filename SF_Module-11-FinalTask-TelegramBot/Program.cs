using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Telegram.Bot;
using SF_Module_11_FinalTask_TelegramBot.Controllers;
using SF_Module_11_FinalTask_TelegramBot.Services;

namespace SF_Module_11_FinalTask_TelegramBot
{
    class Program
    {
        public static async Task Main()
        {
            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build();// Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<TextMessageController>();
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<InlineKeyboardController>();
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("BOT_TOKEN"));
            // Регистрация сервиса сессии
            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddSingleton<IFunctions, Functions>();

            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }
    }
}

