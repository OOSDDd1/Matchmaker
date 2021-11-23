using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace MovieMatcher
{
    public class Database
    {
        private string _sqlBuilder = MainWindow.Config["db-string"];

        // Example method
        public string GetName()
        {
            using (SqlConnection connection = new(_sqlBuilder))
            {
                // Create query
                string sql = "SELECT name FROM Inventory";
                using (SqlCommand command = new(sql, connection))
                {
                    //Open connection
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Read result
                        string result = "";
                        while (reader.Read()) result += reader.GetString(0) + "\n";

                        return result;
                    }
                }
            }
        }
    }
}