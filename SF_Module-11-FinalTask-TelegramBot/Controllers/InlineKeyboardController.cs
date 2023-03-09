using System;
using System.Threading;
using System.Threading.Tasks;
using SF_Module_11_FinalTask_TelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SF_Module_11_FinalTask_TelegramBot.Controllers
{
    public class InlineKeyboardController
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
            _memoryStorage.GetSession(callbackQuery.From.Id).ButtonCode = callbackQuery.Data;

            // Генерим информационное сообщение
            string buttonCode = callbackQuery.Data switch
            {
                "length" => "подсчет символов.\nВведите любой текст...",
                "sum" => "подсчет суммы чисел.\nВведите числа через пробел (например: 1 2 3)",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбран режим - </b>{buttonCode}.{Environment.NewLine}",
                cancellationToken: ct, parseMode: ParseMode.Html);
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
        }
    }
}

