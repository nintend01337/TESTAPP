using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTAPP.Data;
using TESTAPP.DB;
using TESTAPP.Helpers;

namespace TESTAPP
{
    /// <summary>
    /// Главный класс приложения. 
    /// </summary>
    public class App
    {
        public App()
        {
            Start();
        }

        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        private void Start()
        {
            Cui.MainMenu();
            PullEvent();
        }

        /// <summary>
        /// Обработчик главного меню
        /// </summary>
        void PullEvent()
        {
            while (true)
            {
                Cui.ClearScreen();
                Cui.MainMenu();
                var input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "1":
                        AddCustomer();
                        break;

                    case "2":
                        FetchCustomers();
                        break;

                    case "3":
                        AddOrder();
                        break;

                    case "4":
                        FetchOrders();
                        break;

                    case "5":
                        FetchOrdersById();
                        break;

                    case "6":
                        RemoveCustomerById();
                        break;

                    case "7":
                        RemoveOrderById();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод!");
                        break;
                }
            }
        }

        /// <summary>
        /// Добавление пользователя в БД
        /// </summary>
        private void AddCustomer()
        {
            Console.WriteLine("Добавление Клиента :\n");
            Console.WriteLine("Имя:\n");
            var firstName = Console.ReadLine();
            Console.WriteLine("Фамилия:\n");
            var lastName = Console.ReadLine();
            Console.WriteLine("Имейл:\n");
            var email = Console.ReadLine();
            Console.WriteLine("Телефон:\n");
            var phoneNumber = Console.ReadLine();

            string queryString = $"insert into Customers(FirstName,LastName,Email,PhoneNumber) values" +
                                 $" ('{firstName}','{lastName}','{email}','{phoneNumber}')";

            //Если прошли валидацию выполняем запись в БД
            if (Validator.ValidateName(firstName) && Validator.ValidateName(lastName) &&
                   Validator.ValidateEmail(email) && Validator.ValidatePhone(phoneNumber))
            {

                QueryExecutor queryExecutor = new QueryExecutor();

                if (queryExecutor.ExecuteNonQuery(queryString) > 0)
                {
                    Console.WriteLine("Операция выполнена успешно!");
                }
            }
            else
            {
                Console.WriteLine("Неверные данные проверьте ввод!");
                Console.ReadKey();
                AddCustomer();
            }

        }

        /// <summary>
        /// Добавление заказа в БД
        /// </summary>
        private void AddOrder()
        {
            Cui.ClearScreen();
            FetchCustomers();

            Console.WriteLine("Добавление заказа:\n");
            Console.WriteLine("Код клиента:\n");
            var customerID = Console.ReadLine();
            Console.WriteLine("Имя заказа:\n");
            var orderName = Console.ReadLine();
            Console.WriteLine("Сумма:\n");
            var orderAmount = Console.ReadLine();

            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string queryString = $"insert into Orders(CustomerID,OrderName,OrderDate,OrderAmount) values" +
                                $"('{customerID}','{orderName}', convert(datetime,'{date}',120),'{orderAmount}')";

            if (Validator.ValidateDigits(customerID) && Validator.ValidateDecimal(orderAmount))
            {
                QueryExecutor queryExecutor = new QueryExecutor();

                if (queryExecutor.ExecuteNonQuery(queryString) > 0)
                {
                    Console.WriteLine("Операция выполнена успешно!");
                }
            }
        }

        /// <summary>
        /// Получение списка клиентов из БД 
        /// </summary>
        private void FetchCustomers()
        {
            string queryString = $"SELECT * FROM Customers";

            Cui.ClearScreen();
            Console.WriteLine("Получение списка клиентов:\n");
            QueryExecutor queryExecutor = new QueryExecutor();
            string result = queryExecutor.ExecuteQuery(queryString);
            Console.WriteLine(result);

            Console.ReadKey();
        }
        /// <summary>
        /// Получение списка заказов из БД
        /// </summary>
        private void FetchOrders()
        {
            Console.WriteLine("Получение списка заказов");
            string queryString = $"SELECT O.OrderID, C.FirstName, C.LastName,O.OrderName,O.OrderDate,O.OrderAmount " +
                                 $"FROM Orders AS O INNER JOIN Customers AS C ON O.CustomerID = C.CustomerID;";
            Cui.ClearScreen();
            QueryExecutor queryExecutor = new QueryExecutor();
            string result = queryExecutor.ExecuteQuery(queryString);
            Console.WriteLine(result);

            Console.WriteLine("Нажмите любую клавишу для возврата в главное меню.....");
            Console.ReadKey();
        }

        /// <summary>
        /// Получение заказов клиента по айди 
        /// </summary>
        void FetchOrdersById()
        {
            Console.WriteLine("Получение списка заказов по ID клиента");
            Console.WriteLine("Введите Id клиента:");
            var clientID = Console.ReadLine();
            if (Validator.ValidateDigits(clientID))
            {
                string queryString = $"SELECT O.OrderID, C.FirstName, C.LastName,O.OrderName,O.OrderDate,O.OrderAmount " +
                                     $"FROM Orders AS O INNER JOIN Customers AS C ON O.CustomerID = C.CustomerID " +
                                     $"WHERE O.CustomerID = {clientID};";
                Cui.ClearScreen();
                QueryExecutor queryExecutor = new QueryExecutor();
                string result = queryExecutor.ExecuteQuery(queryString);
                Console.WriteLine(result);


                Console.WriteLine("Нажмите любую клавишу для возврата в главное меню.....");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Введены неверный ID");
                FetchOrdersById();
            }
        }
        /// <summary>
        /// Удаляет клиента по CustomerID
        /// </summary>
        private void RemoveCustomerById()
        {
            FetchCustomers();
            Console.WriteLine("Удаление клиента по ID");
            Console.WriteLine("Введите Id клиента для удаления:");
            var clientID = Console.ReadLine();

            if (Validator.ValidateDigits(clientID))
            {
                string queryString = $"DELETE FROM Customers WHERE CustomerID = {clientID}";
                QueryExecutor queryExecutor = new QueryExecutor();

                if (queryExecutor.ExecuteNonQuery(queryString) > 0)
                {
                    Console.WriteLine("Операция выполнена успешно!");
                }
                Console.WriteLine("Нажмите любую клавишу для возврата в главное меню.....");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Введены неверный ID");
                RemoveCustomerById();
            }
        }

        /// <summary>
        /// Удаляет заказа по OrderID
        /// </summary>
        private void RemoveOrderById()
        {
            FetchOrders();
            Console.WriteLine("Удаление заказа по ID");
            Console.WriteLine("Введите Id заказа:");
            var orderID = Console.ReadLine();

            if (Validator.ValidateDigits(orderID))
            {
                string queryString = $"DELETE FROM Orders WHERE OrderID = {orderID}";
                QueryExecutor queryExecutor = new QueryExecutor();

                if (queryExecutor.ExecuteNonQuery(queryString) > 0)
                {
                    Console.WriteLine("Операция выполнена успешно!");
                }
                Console.WriteLine("Нажмите любую клавишу для возврата в главное меню.....");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Введены неверный ID");
                RemoveOrderById();
            }
        }
    }
}
