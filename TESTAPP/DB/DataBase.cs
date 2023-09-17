using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TESTAPP
{
    public class DataBase
    {
        //private string connectionString = 
        SqlConnection connection = new SqlConnection("Server = localhost\\SQLEXPRESS;" +
                                                    "Database=shoping;" +
                                                    "Trusted_Connection=True;");
            public void OpenConnection() 
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            Console.WriteLine("Подключение открыто .../");
//            Console.WriteLine("Свойства подключения:");
//            Console.WriteLine($"\tСтрока подключения: {connection.ConnectionString}");
//            Console.WriteLine($"\tБаза данных: {connection.Database}");
//            Console.WriteLine($"\tСервер: {connection.DataSource}");
////            Console.WriteLine($"\tВерсия сервера: {connection.ServerVersion}");
//            Console.WriteLine($"\tСостояние: {connection.State}");
//            Console.WriteLine($"\tWorkstationld: {connection.WorkstationId}");
        }

        public void CloseConnection() 
        { 
            if (connection.State == ConnectionState.Open) 
            {
                connection.Close();
            }
            Console.WriteLine("Подключение закрыто ...\\");
        }

        public SqlConnection GetConnection() 
        {
            return connection;
        }
    }
}
