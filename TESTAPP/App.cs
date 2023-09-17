using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTAPP.Data;

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
            var FirstName = Console.ReadLine();
            Console.WriteLine("Фамилия:\n");
            var LastName = Console.ReadLine();
            Console.WriteLine("Имейл:\n");
            var Email = Console.ReadLine();
            Console.WriteLine("Телефон:\n");
            var PhoneNumber = Console.ReadLine();

            DataBaseConnector db = new DataBaseConnector();
            db.OpenConnection();

            string queryString = $"insert into Customers(FirstName,LastName,Email,PhoneNumber) values" +
                                 $" ('{FirstName}','{LastName}','{Email}','{PhoneNumber}')";

            SqlCommand cmd = new SqlCommand(queryString, db.GetConnection());

            if (cmd.ExecuteNonQuery() == 1)
            {
                Console.WriteLine("Операция выполнена успешно!");
            }
            db.CloseConnection();

        }

        /// <summary>
        /// Добавление заказа в БД
        /// </summary>
        private void AddOrder()
        {
            Console.WriteLine("Добавление заказа:\n");
            FetchCustomers();

            Console.WriteLine("Код клиента:\n");
            var CustomerID = Console.ReadLine();
            Console.WriteLine("Имя заказа:\n");
            var OrderName = Console.ReadLine();
            Console.WriteLine("Сумма:\n");
            var OrderAmount = Console.ReadLine();
            string Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string Date = DateTime.Now.ToString("yyyy-MM-dd");
          
            string queryString = $"insert into Orders(CustomerID,OrderName,OrderDate,OrderAmount) values" +
                $"('{CustomerID}','{OrderName}','{OrderAmount}','{Date}')";
            DataBaseConnector db = new DataBaseConnector();
            db.OpenConnection();

            SqlCommand cmd = new SqlCommand(queryString, db.GetConnection());
            // cmd.Parameters.Add(new SqlParameter("@Datevalue", System.Data.SqlDbType.DateTime)).Value = Date;

            if (cmd.ExecuteNonQuery() == 1)
            {
                Console.WriteLine("Операция выполнена успешно!");
            }
            db.CloseConnection();

        }

        /// <summary>
        /// Получение списка клиентов из БД 
        /// </summary>
        private void FetchCustomers()
        {
            Console.WriteLine("Получение списка клиентов:\n");
            DataBaseConnector db = new DataBaseConnector();
            db.OpenConnection();
            string queryString = $"SELECT * FROM Customers";
            SqlCommand cmd = new SqlCommand(queryString, db.GetConnection());

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}\t {reader[1]}\t {reader[2]}\t {reader[3]}\t {reader[4]}\t");
                }
            }
            //Console.WriteLine("Нажмите любую клавишу для возврата в главное меню.....");
            Console.ReadKey();
        }
        /// <summary>
        /// Получение списка заказов из БД
        /// </summary>
        private void FetchOrders()
        {
            Console.WriteLine("Получение списка заказов");
            DataBaseConnector db = new DataBaseConnector();
            db.OpenConnection();
            string queryString = $"SELECT * FROM Orders";
            SqlCommand cmd = new SqlCommand(queryString, db.GetConnection());

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}\t {reader[1]}\t {reader[2]}\t {reader[3]}\t {reader[4]}\t");
                }
            }
            Console.WriteLine("Нажмите любую клавишу для возврата в главное меню.....");
            Console.ReadKey();
        }

        /// <summary>
        /// Получение заказов клиента по айди 
        /// </summary>
        void FetchOrdersById()
        {
            Console.WriteLine();
        }
    }
}
