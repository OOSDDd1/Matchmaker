using System;
using Microsoft.Data.SqlClient;
using MovieMatcher.Models.Database;

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
                //make query
                string sql = "SELECT username FROM MatchMaker.Matchmaker.[user]";
                using (SqlCommand command = new SqlCommand(sql, connection))
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

        //Checken of Wachtwoord correcet is
        public Boolean CheckPassword(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                string sql = @$"SELECT * FROM MatchMaker.Matchmaker.[user] WHERE username = '{username}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserInfo.Id = reader.GetInt32(0);
                                UserInfo.Username = reader.GetString(1);
                                UserInfo.Email = reader.GetString(2);
                                UserInfo.Password = reader.GetString(3);
                                UserInfo.BirthYear = reader.GetDateTime(4).ToString();
                            }
                            if (UserInfo.Password != null && PasswordHasher.Verify(password, UserInfo.Password))
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
        }
    }
}