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
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Не удалось подключиться к базе данных" +
                                        $" проверьте подключение а также убедитесь что БД существует.\n{e.Message}");
                } 
            }
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
