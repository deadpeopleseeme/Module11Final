using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace Module11Final.Controllers
{
    internal class TextMessagesController
    {
        
        private readonly ITelegramBotClient _telegramClient;

        public TextMessagesController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Считаем символы" , $"symbolsCount"),
                        InlineKeyboardButton.WithCallbackData($" Два числа" , $"twoNumbers")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Блаблабла это бот.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Офигенный такой бот из модуля 11.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "ТУТ ТИПА ДЕФОЛТНЫЙ КЕЙС.", cancellationToken: ct);
                    break;
            }
        }
    }

}
