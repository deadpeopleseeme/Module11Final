using Microsoft.Extensions.Hosting;
using Module11Final.Controllers;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
//using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Module11Final.Controllers;
using Telegram.Bot.Polling;

namespace Module11Final
{
    internal class Bot : BackgroundService
    {
        // Клиент к Telegram Bot API
        private ITelegramBotClient _telegramClient;

        // Контроллеры различных видов сообщений
        private InlineKeyboardController _inlineKeyboardController;
        private TextMessagesController _textMessagesController;
        private UnsupportedMessagesController _unsupportedMessagesController;


        public Bot(
            ITelegramBotClient telegramClient,
            InlineKeyboardController inlineKeyboardController,
            TextMessagesController textMessagesController,
            UnsupportedMessagesController unsupportedMessagesController
            )
        {
            _telegramClient = telegramClient;
            _inlineKeyboardController = inlineKeyboardController;
            _textMessagesController = textMessagesController;
            _unsupportedMessagesController = unsupportedMessagesController;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                // В строке ниже выбираем, какие обновления хотим получать. В данном случае было задумано текст и нажатия на кнопки, но оно почему-то продолжало реагировать на всё
                new ReceiverOptions() { AllowedUpdates = [UpdateType.Message, UpdateType.CallbackQuery] }, 
                cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен.");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //  Обрабатываем нажатия на кнопки  из Telegram Bot API: https://core.telegram.org/bots/api#callbackquery
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyboardController.Handle(update.CallbackQuery, cancellationToken);
                return;
            }

            // Обрабатываем входящие сообщения из Telegram Bot API: https://core.telegram.org/bots/api#message
            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        await _textMessagesController.Handle(update.Message, cancellationToken);
                        return;
                    default:
                        await _unsupportedMessagesController.Handle(update.Message, cancellationToken);
                        return;
                }

                
                
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);
            return Task.CompletedTask;
        }
    }
}