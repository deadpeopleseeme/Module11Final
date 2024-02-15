using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Module11Final;
using Module11Final.Controllers;
using Module11Final.Services;
using Telegram.Bot;

namespace Module11Final
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("6859652618:AAGxm5ru2FElItJGmi1zkwz9zIwq9OPiHI4"));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessagesController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<TextMessagesController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<IStorage, MemoryStorage>();
        }
    }
}