using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace MovieMatcher
{
    public class Database
    {
        private string _sqlBuilder = MainWindow.Config["db-string"];
        
        // Voorbeeld method
        public string GetName()
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                //Maak je query
                string sql = "SELECT username FROM MatchMaker.Matchmaker.[user]";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Open connectie
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Lees result
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

        //Voorbeeld method
        public string CheckPassword(string username, string password)
        {
            using (SqlConnection connection = new(_sqlBuilder))
            {
                //Maak je query
                string sql = @$"SELECT * FROM MatchMaker.Matchmaker.[user] WHERE name = '{username}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //Open connection
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Read result
                        string result = "";
                        while (reader.Read())
                        {
                            if (reader["password"].ToString().Equals(password))
                            {
                                result = "baggercode";
                            }
                        }

                        return result;
                    }
                }
            }
        }

        public string CreateUser(string userName, string password, string email, string age)
        {
            try
            {
                using SqlConnection connection = new(_sqlBuilder);
            
                string sql = @$"INSERT INTO MatchMaker.Matchmaker.[user] (username, email, password, birth_date) VALUES ('{userName}', '{email}', '{password}', '{age}')";
            
                using SqlCommand command = new(sql, connection);
                connection.Open();
            
                using SqlDataReader reader = command.ExecuteReader();
            
                return "Account created";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}