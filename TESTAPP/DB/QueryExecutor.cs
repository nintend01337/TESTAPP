using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTAPP.DB
{
    /// <summary>
    /// Отправляет запросы в Базу данных
    /// </summary>
    public class QueryExecutor
    {
        DataBaseConnector dbConnector = new DataBaseConnector();
        public QueryExecutor() 
        {
            dbConnector.OpenConnection();
        }

        /// <summary>
        /// Выполняет запрос к бд, принимает на вход string
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Число измененных строк</returns>
        public int ExecuteNonQuery(string query) 
        {
            int rowsAffected = 0;
            SqlCommand cmd = new SqlCommand(query, dbConnector.GetConnection());
            rowsAffected = cmd.ExecuteNonQuery();
            dbConnector.CloseConnection();
            return rowsAffected;
        }

        /// <summary>
        /// Выполняет запрос к бд и возвращает данные в виде строки,принимает на вход string
        /// </summary>
        /// <param name="query"></param>
        /// <returns>string</returns>
        public string ExecuteQuery(string query) 
        {
            StringBuilder sb = new StringBuilder();
            SqlDataAdapter adapter = new SqlDataAdapter(query, dbConnector.GetConnection());
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            
            //Перебираем таблицы
            foreach(DataTable dt in ds.Tables) 
            { 
                //Получаем названия столбцов
                foreach(DataColumn column in dt.Columns) 
                {
                    sb.Append($"{column.ColumnName}\t");
                }
                sb.AppendLine();
                //Получаем значения ячеек
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                    { 
                        sb.Append($"{cell}\t"); 
                    }
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            
                dbConnector.CloseConnection();
            return sb.ToString();
        }
    }
}
