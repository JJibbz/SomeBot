using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using SomeBot.Services;

namespace SomeBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).Function = callbackQuery.Data;

            string functionText = callbackQuery.Data switch
            {
                "Symb" => "Подсчёт символов",
                "Sum" => "Сумма чисел",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"<b>Выбрана функция - {functionText}.{Environment.NewLine}</b>" + $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
        
}
