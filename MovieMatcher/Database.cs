using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace MovieMatcher
{
    public class Database
    {


        private string _sqlBuilder = @"Data Source=127.0.0.1;Initial Catalog=TestDB;User ID=sa;Password=Welkom01!;TrustServerCertificate=true";

        //Voorbeeld method
        public string GetName()
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                //Maak je query
                string sql = "SELECT name FROM Inventory";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //Open connectie
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //Lees result
                        string result = "";
                        while (reader.Read())
                        {
                           result += reader.GetString(0) + "\n";
                        }

                        return result;
                    }
                }
            }
        }
    }
}