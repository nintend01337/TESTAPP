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
    internal class App
    {
        public App()
        {
            Initialize();
        }

        private void Initialize() 
        {

        }

        //Точка входа в приложение
        public void Start()
        {
            Cui terminal = new Cui();
            terminal.MainMenu();
            PullEvent();
        }        
        //Обработчик главного меню
        void PullEvent() 
        {
            while (true) 
            {
                Cui terminal = new Cui();
                terminal.ClearScreen();
                terminal.MainMenu();
                var input = Console.ReadLine().ToLower();


                switch(input)
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
    
        //Добавление пользователя в БД
        void AddCustomer() 
        {
           Console.WriteLine("Добавление Клиента :\n");
            //Customer customer = new Customer();
            Console.WriteLine("Имя:\n");
            var FirstName = Console.ReadLine();
            Console.WriteLine("Фамилия:\n");
            var LastName = Console.ReadLine();
            Console.WriteLine("Имейл:\n");
            var Email = Console.ReadLine();
            Console.WriteLine("Телефон:\n");
            var PhoneNumber = Console.ReadLine();
            
            DataBase db = new DataBase();
            db.OpenConnection();

            string queryString = $"insert into Customers(FirstName,LastName,Email,PhoneNumber) values" +
                                 $" ('{FirstName}','{LastName}','{Email}','{PhoneNumber}')";
            
            SqlCommand cmd = new SqlCommand(queryString,db.GetConnection());
            
            if (cmd.ExecuteNonQuery() == 1) 
            {
                Console.WriteLine("Операция выполнена успешно!");    
            }
            db.CloseConnection();
            
        }
        
        //Добавление заказа в БД
        void AddOrder() 
        {
            Console.WriteLine("Добавление заказа:\n");
            FetchCustomers();

            Console.WriteLine("Код клиента:\n");
            var CustomerID = Console.ReadLine();
            Console.WriteLine("Имя заказа:\n");
            var OrderName = Console.ReadLine();
            Console.WriteLine("Сумма:\n");
            var OrderAmount = Console.ReadLine();
            var Date = DateTime.Now;
            //string sqlDate


            string queryString = $"insert into Orders(CustomerID,OrderName,OrderDate,OrderAmount) values" +
                $"('{CustomerID}','{OrderName}','{OrderAmount}','{Date}')";
            DataBase db = new DataBase();
            db.OpenConnection();
           
            SqlCommand cmd = new SqlCommand(queryString, db.GetConnection());

            if (cmd.ExecuteNonQuery() == 1)
            {
                Console.WriteLine("Операция выполнена успешно!");
            }
            db.CloseConnection();

        }

        //Получение списка клиентов из БД 
        void FetchCustomers() 
        {
            Console.WriteLine("Получение списка клиентов:\n");
            DataBase db = new DataBase();
            db.OpenConnection();
            string queryString = $"SELECT * FROM Customers";
            SqlCommand cmd = new SqlCommand(queryString,db.GetConnection());
            
            using(SqlDataReader reader = cmd.ExecuteReader()) 
            {
                while (reader.Read()) 
                {
                    Console.WriteLine($"{reader[0]}\t {reader[1]}\t {reader[2]}\t {reader[3]}\t {reader[4]}\t");
                }    
            }
            //Console.WriteLine("Нажмите любую клавишу для возврата в главное меню.....");
            //Console.ReadKey();
        }
        //Получение списка заказов из БД
        void FetchOrders() 
        {
            Console.WriteLine("Получение списка заказов");
            DataBase db = new DataBase();
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
        
        //Получение заказов клиента по айди 
        void FetchOrdersById() 
        {
            Console.WriteLine();
        }
    }
}
