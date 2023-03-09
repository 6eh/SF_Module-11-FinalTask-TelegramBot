using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SF_Module_11_FinalTask_TelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace SF_Module_11_FinalTask_TelegramBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        private readonly IFunctions _functions;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, IFunctions functions)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _functions = functions;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");

            switch (message.Text)
            {
                case "/start":
                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Подсчет символов", $"length"),
                        InlineKeyboardButton.WithCallbackData($"Сложение чисел", $"sum")
                    });

                    // Передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                        $"<b>Выберите что-нибудь</b>",
                        cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:
                    string buttonCode = _memoryStorage.GetSession(message.Chat.Id).ButtonCode;
                    var result = _functions.Function(buttonCode, message.Text);
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
                    break;
            }            
        }
    }
}

