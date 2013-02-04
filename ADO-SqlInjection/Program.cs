using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ADO_SqlInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectString = @"Data Source = ASPIRE-5560G\ZLSQL; Initial Catalog = DBSlides; Integrated Security = True";

            try
            {
                SqlConnection db = new SqlConnection();
                db.ConnectionString = connectString;

                string login = "glucas' --";
                string lastName = "Mais pas Lucas";
                string injection = string.Format("SELECT section_id FROM Student WHERE login = '{0}' AND last_name = '{1}';", login, lastName);

                SqlCommand cmd = db.CreateCommand();
                cmd.CommandText = injection;

                db.Open();
                object returnvalue = cmd.ExecuteScalar();
                db.Close();

                Console.WriteLine(returnvalue.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}