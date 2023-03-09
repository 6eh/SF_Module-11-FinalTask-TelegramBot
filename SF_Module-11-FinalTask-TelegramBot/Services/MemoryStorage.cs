using System;
using SF_Module_11_FinalTask_TelegramBot.Models;
using System.Collections.Concurrent;

namespace SF_Module_11_FinalTask_TelegramBot.Services
{
    public class MemoryStorage : IStorage
    {
        // Хранилище сессий
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            // Возвращаем сессию по ключу, если она существует
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            // Создаем и возвращаем новую, если такой не было
            var newSession = new Session() { ButtonCode = "length" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}

