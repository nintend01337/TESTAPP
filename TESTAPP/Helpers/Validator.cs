using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TESTAPP.Helpers
{
    /// <summary>
    ///Проверяет введеные данные на корректность
    /// </summary>
    public class Validator
    {
        Validator()
        {
            Console.WriteLine("Валидация данных ....");
        }
        /// <summary>
        /// Проверяет корректность ввода номера
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>bool</returns>
        public static bool ValidatePhone(string phoneNumber)
        {
            //шаблон тел номера начинается с + и след 11 цифр
            string phonePattern = @"^\+\d{11}$";
            return Validate(phoneNumber, phonePattern);
        }
        
        /// <summary>
        /// Проверяет кооректность ввода Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>bool</returns>
        public static bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Validate(email, emailPattern);
        }


        /// <summary>
        /// Произволит Валидацию Имени и Фамилии
        /// </summary>
        /// <param name="name"></param>
        /// <returns>bool</returns>
        public static bool ValidateName(string name)
        {
            string pattern = @"^[А-Я][а-я]*$";
            return Validate(name, pattern);
        }

        /// <summary>
        /// Производит валидацию введеного числа.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidateDigits(string input) 
        {
            string pattern = @"^\d[0-9]{1,}$";
            return Validate(input, pattern);
        }


        /// <summary>
        /// Общий метод валидации,используется внутри класса и не вызывается на прямую
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns>bool</returns>
        private static bool Validate(string input,string pattern) 
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
    }
}
