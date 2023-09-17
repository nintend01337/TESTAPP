using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTAPP.Data;
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
            Cui.ClearScreen();
            FetchCustomers();
           
            Console.WriteLine("Добавление заказа:\n");
            Console.WriteLine("Код клиента:\n");
            var CustomerID = Console.ReadLine();
            Console.WriteLine("Имя заказа:\n");
            var OrderName = Console.ReadLine();
            Console.WriteLine("Сумма:\n");
            var OrderAmount = Console.ReadLine();
            string Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string Date = DateTime.Now.ToString("yyyy-MM-dd");
          
            string queryString = $"insert into Orders(CustomerID,OrderName,OrderDate,OrderAmount) values" +
                $"('{CustomerID}','{OrderName}', convert(datetime,'{Date}',120),'{OrderAmount}')";
            
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
            DataBaseConnector db = new DataBaseConnector();
            db.OpenConnection();
            string queryString = $"SELECT * FROM Customers";
            SqlCommand cmd = new SqlCommand(queryString, db.GetConnection());

            Cui.ClearScreen();
            Console.WriteLine("Получение списка клиентов:\n");
            Console.WriteLine("#ID\tИмя\tФамилия\temail\tтелефон");

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
            string queryString = $"SELECT O.OrderID, C.FirstName, C.LastName,O.OrderName,O.OrderDate,O.OrderAmount " +
                                 $"FROM Orders AS O INNER JOIN Customers AS C ON O.CustomerID = C.CustomerID;";
            SqlCommand cmd = new SqlCommand(queryString, db.GetConnection());
            Cui.ClearScreen();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //Console.WriteLine("{0,3},{1,15},{2,15},{3,32},{4,12}",$"{reader[0]}\t{reader[1]}\t{reader[2]}\t{reader[3]}\t{reader[4]}\t");
                    Console.WriteLine("{0,3}\t{1,15}\t{2,15}\t{3,32}\t{4,24}",reader[0],reader[1],reader[2],reader[3],reader[4]);
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
            Console.WriteLine("Получение списка заказов по ID клиента");
            Console.WriteLine("Введите Id клиента:");
            var clientID = Console.ReadLine();
            
            DataBaseConnector db = new DataBaseConnector();
            db.OpenConnection();
            string queryString = $"SELECT O.OrderID, C.FirstName, C.LastName,O.OrderName,O.OrderDate,O.OrderAmount " +
                                 $"FROM Orders AS O INNER JOIN Customers AS C ON O.CustomerID = C.CustomerID " +
                                 $"WHERE O.CustomerID = {clientID};";
            SqlCommand cmd = new SqlCommand(queryString, db.GetConnection());

            Cui.ClearScreen();
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
    /// Удаляет клиента по CustomerID
    /// </summary>
        private void RemoveCustomerById() 
        {
            Console.WriteLine("Удаление клиента по ID");
        }
        /// <summary>
        /// Удаляет заказа по OrderID
        /// </summary>
        private void RemoveOrderById() 
        {
            Console.WriteLine("Удаление заказа по ID");
        }
    }
}
