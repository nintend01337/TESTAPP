using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTAPP
{
    /// <summary>
    /// Utility Класс для работы с терминалом
    /// </summary>
    public static class Cui
    {
        public static void MainMenu()
        {
            Console.WriteLine("Выберите действие :\n" +
                "1. Добавить Нового Клиента\n" +
                "2. Просмотр Клиентов\n" +
                "3. Добавление заказа\n" +
                "4. Просмотр списка всех заказов\n" +
                "5. Просмотр заказов отдельного клиента");
        
        }

        public static void ClearScreen() 
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        } 
    }
}
