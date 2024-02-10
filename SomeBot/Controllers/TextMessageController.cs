using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace SomeBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Подсчёт символов", $"Symb"),
                        InlineKeyboardButton.WithCallbackData($"Сумма чисел", "Sum")
                    });

                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Выберите необходимую функцию. </b> {Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
            }
        }
    }
}
