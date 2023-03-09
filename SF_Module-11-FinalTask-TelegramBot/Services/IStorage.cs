using SF_Module_11_FinalTask_TelegramBot.Models;

namespace SF_Module_11_FinalTask_TelegramBot.Services
{
    public interface IStorage
    {
        // Получение сессии
        Session GetSession(long chatId);
    }
}

