using Telegram.Bot;
using Telegram.Bot.Types;
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
            string botMode = callbackQuery.Data switch
            {
                
                "symbolsCount" => " Считаем количество символов во введенной строке",
                "twoNumbers" => " Вычисляем сумму двух чисел (вводить через пробел)",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбранный режим бота - {botMode}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Режим можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
