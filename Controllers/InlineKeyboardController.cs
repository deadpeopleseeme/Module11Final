using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module11Final.Services;
using Telegram.Bot.Types.Enums;

namespace Module11Final.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).BotActionType = callbackQuery.Data;

            // Генерим информационное сообщение
            string languageText = callbackQuery.Data switch
            {
                "symbolsCount" => " Считаем количество символов во введенной строке",
                "twoNumbers" => " Сравниваем два числа (вводить через запятую)",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбранный режим бота - {languageText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Режим можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
