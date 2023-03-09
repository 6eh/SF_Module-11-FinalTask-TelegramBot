using System;

namespace SF_Module_11_FinalTask_TelegramBot.Services
{
	public class Functions : IFunctions
	{
        public string Function(string buttonCode, string text)
        {
            // Если пользователь выбрал подсчет символов
            if (buttonCode == "length")
                return $"В вашем тексте символов: {text.Length}";

            // Если пользователь выбрал суммирование чисел
            if (buttonCode == "sum")
            {
                int sum = 0;
                try
                {
                    string[] strings = text.Split(' ');

                    foreach (var s in strings)
                    {
                        sum += Convert.ToInt32(s);
                    }
                }
                catch (FormatException)
                {
                    return $"Ошибка: Нарушен формат ввода.{Environment.NewLine}Введите числа через пробел (например: 1 2 3)";
                }
                catch (Exception ex)
                {
                    return $"Ошибка: {ex.Message}";
                }
                return sum.ToString();
            }

            else return string.Empty;
        }
    }
}

