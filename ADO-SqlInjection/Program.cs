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
                string injection = string.Format("SELECT section_id FROM Student WHERE login = '{0}' AND last_name = '{1}';", login, lastName); // une requête concaténée comme celle-ci est vulnérable aux injections

                SqlCommand cmd = db.CreateCommand();
                cmd.CommandText = injection;

                db.Open();
                object returnvalue = cmd.ExecuteScalar();
                db.Close();

                Console.WriteLine(returnvalue.ToString());

                // méthode sécurisée avec paramètres SQL
                string noInjection = "SELECT section_id FROM Student WHERE login = @login AND last_name = @lName";

                SqlParameter paramLogin = new SqlParameter();
                paramLogin.ParameterName = "@login";
                paramLogin.Value = login;

                SqlParameter paramLName = new SqlParameter();
                paramLName.ParameterName = "@lName";
                paramLName.Value = lastName;

                cmd.CommandText = noInjection;

                db.Open();
                object returnvalue2 = cmd.ExecuteScalar();
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