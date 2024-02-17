using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Module11Final.Services;

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
                            await _telegramClient.SendTextMessageAsync(message.From.Id, SymbolsCounter.SymbolsCount(message.Text), cancellationToken: ct);
                            return;

                        case "twoNumbers":
                            await _telegramClient.SendTextMessageAsync(message.From.Id, TwoNumbersCounter.TwoNumbersCount(message.Text), cancellationToken: ct);
                            return;
                    }
                    break;
            }
        }
    }
}
