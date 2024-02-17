using Telegram.Bot;
using Telegram.Bot.Types;

namespace Module11Final.Controllers
{
    internal class UnsupportedMessagesController
    {
        private readonly ITelegramBotClient _telegramClient;

        public UnsupportedMessagesController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Я этот формат не хаваю, прекращай :D", cancellationToken: ct);
        }
    }
}
