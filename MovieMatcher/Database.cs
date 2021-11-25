using System;
using Microsoft.Data.SqlClient;
using System.Text;
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
                string sql = "SELECT name FROM MatchMaker.Matchmaker.[user]";
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
                string sql = @$"SELECT * FROM MatchMaker.Matchmaker.[user] WHERE name = '{username}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserInfo userInfo = new UserInfo(); //zo moet het niet maar voor nu
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                
                                userInfo.id = reader.GetInt32(0);
                                userInfo.name = reader.GetString(1);
                                userInfo.email = reader.GetString(2);
                                userInfo.password = reader.GetString(3);
                                userInfo.birth_year = reader.GetDateTime(4).ToString();

                            }

                            if (userInfo.password.Equals(password))
                            {
                                return true;
                            }
                            else return false;
                        }
                        return false;
                    }
                }
            }
        }
    }
}