using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module11Final.Models;
using Telegram.Bot.Types.Enums;
using Module11Final.Services;
using System.Threading;


namespace Module11Final.Controllers
{
    internal class TextMessagesController
    {
        
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessagesController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>
                    {
                        new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Считаем символы" , $"symbolsCount"),
                        InlineKeyboardButton.WithCallbackData($" Два числа" , $"twoNumbers")
                    }
                    };

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Выберите режим бота.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Режим по умолчанию при запуске: 'считаем символы'.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:

                    // в зависимости от выбранного режима бота выполняем действия
                    switch (_memoryStorage.GetSession(message.Chat.Id).BotActionType)
                    {
                        case "symbolsCount":
                            await _telegramClient.SendTextMessageAsync(message.From.Id, $"Длина сообщения,  знаков: {message.Text.Length}", cancellationToken: ct);
                            return;

                        case "twoNumbers":
                            string[] numbers = message.Text.Split(' ');

                            // проверяем, разбивается ли строка на 2, и если да, то числа ли введены в этой строке. числа пусть будут int, в задаче иного не указано
                            if (numbers.Length != 2 || !int.TryParse(numbers[0], out int number1) || !int.TryParse(numbers[01], out int number2))
                            {
                                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Только 2 целых числа и только через пробел!", cancellationToken: ct);
                            }
                            else
                            {
                                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"{number1 + number2}", cancellationToken: ct);
                            }
                            return;
                    }
                    break;
            }
        }
    }

}
