using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TESTAPP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = new App();
            app.Start();
            Console.WriteLine("Программа завершила работу.");
            Console.Read();
        }
    }
}
