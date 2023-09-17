using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TESTAPP
{
    /// <summary>
    /// Класс управляющий подключениями к Базе Данных 
    /// </summary>
    public class DataBaseConnector
    {
        SqlConnection connection = new SqlConnection("Server = localhost\\SQLEXPRESS;" +
                                                    "Database=shoping;" +
                                                    "Trusted_Connection=True;");
        /// <summary>
        /// Открывает подключение к БД
        /// </summary>
        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            Console.WriteLine("Подключение открыто .../");
            //Console.WriteLine("Свойства подключения:");
            //Console.WriteLine($"\tСтрока подключения: {connection.ConnectionString}");
            //Console.WriteLine($"\tБаза данных: {connection.Database}");
            //Console.WriteLine($"\tСервер: {connection.DataSource}");
            //Console.WriteLine($"\tВерсия сервера: {connection.ServerVersion}");
            //Console.WriteLine($"\tСостояние: {connection.State}");
            //Console.WriteLine($"\tWorkstationld: {connection.WorkstationId}");
        }

       /// <summary>
       /// Закрывает подключение к БД
       /// </summary>
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            Console.WriteLine("Подключение закрыто ...\\");
        }
        
        /// <summary>
        /// Возвращает текущее подключения.
        /// </summary>
        /// <returns>SqlConnetion instance</returns>
        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
