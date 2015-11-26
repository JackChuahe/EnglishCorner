using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string connectionString;
            string queryString;

            connectionString = "Data Source=222.196.200.38/orcl.196.200.45;User ID=encor;PassWord=encor";

            queryString = "select * from allusers";

            OracleConnection myConnection = new OracleConnection(connectionString);

            OracleCommand myORACCommand = myConnection.CreateCommand();

            myORACCommand.CommandText = queryString;

            myConnection.Open();

            OracleDataReader myDataReader = myORACCommand.ExecuteReader();

            

            while(myDataReader.Read())
            {

                Console.WriteLine("email: " + myDataReader[0]);
                Console.WriteLine("url: " + (string)myDataReader[2]);
             }
            string userEmail = "601825672@qq.com";
             string str = "select * from allusers where useremail='" + userEmail + "'";
            Console.WriteLine(str );
            myDataReader.Close();

            myConnection.Close();  */

            string token = Environment.TickCount.ToString();
            Console.WriteLine(token);
        }
    }
}
