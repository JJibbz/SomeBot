using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using SomeBot.Models;
using SomeBot.Services;
using SomeBot.Extensions;

namespace SomeBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            string flag = _memoryStorage.GetSession(message.Chat.Id).Function;
            switch (message.Text)
            {
                case "/start":
                    _memoryStorage.GetSession(message.Chat.Id).Function = null;
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Подсчёт символов", $"Symb"),
                        InlineKeyboardButton.WithCallbackData($"Сумма чисел", "Sum")
                    });
             
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Выберите необходимую функцию. </b> {Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    switch (flag)
                    {
                        case "Symb":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Длина сообщения: {MessageHandler.GetSymbAmt(message.Text)}", cancellationToken: ct);
                            break;
                        case "Sum":
                            try
                            {
                                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {MessageHandler.Sum(ToIntArr.GetIntArray(message.Text))}", cancellationToken: ct);
                                
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Проверьте ввод, для вычисления суммы необходимо ввести несколько чисел через пробел цифрами", cancellationToken: ct);
                            }
                            break;
                        default:
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Не выбрана функция.", cancellationToken: ct);
                            break;
                    }
                    break;
            }
        }
    }
}
