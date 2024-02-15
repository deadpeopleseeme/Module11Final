using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module11Final.Controllers
{
    internal class TextMessagesController
    {
        private readonly ITelegramBotClient _telegramClient;
        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Получено текстовое сообщение", cancellationToken: ct);
        }
        public TextMessagesController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }
    }
}
