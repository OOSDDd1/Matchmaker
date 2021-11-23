using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace MovieMatcher
{
    public class Database
    {
        private string _sqlBuilder = MainWindow.Config["db-string"];

        //Voorbeeld method
        public string GetName()
        {
            using (SqlConnection connection = new(_sqlBuilder))
            {
                //Maak je query
                string sql = "SELECT name FROM Inventory";
                using (SqlCommand command = new(sql, connection))
                {
                    //Open connectie
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //Lees result
                        string result = "";
                        while (reader.Read()) result += reader.GetString(0) + "\n";

                        return result;
                    }
                }
            }
        }
    }
}