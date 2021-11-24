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
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                //Maak je query
                string sql = "SELECT name FROM MatchMaker.Matchmaker.[user]";
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

        //Voorbeeld method
        public string CheckPassword(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                //Maak je query
                string sql = @$"SELECT * FROM MatchMaker.Matchmaker.[user] WHERE name = '{username}'";
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
    }
}