using Microsoft.Data.SqlClient;
﻿using System;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using MovieMatcher.Models.Database;

namespace MovieMatcher
{
    public static class Database
    {
        private static string _sqlBuilder = MainWindow.Config["db-string"];
        
        // Voorbeeld method
        public static string GetName()
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                //make query
                string sql = "SELECT username FROM MatchMaker.Matchmaker.[user]";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Open connectie
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Lees result
                        string result = "";
                        while (reader.Read()) result += reader.GetString(0) + "\n";
                        return result;
                    }
                }
            }
        }

        //Checken of Wachtwoord correcet is
        public static Boolean GetUserInfo(string username)
        {
            using (SqlConnection connection = new(_sqlBuilder))
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
                                UserInfo.BirthYear = reader.GetDateTime(4);
                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
        }

        public static string CreateUser(string userName, string password, string email, string age)
        {
            try
            {
                using SqlConnection connection = new(_sqlBuilder);
            
                string sql = @$"INSERT INTO MatchMaker.Matchmaker.[user] (username, email, password, birth_date) VALUES ('{userName}', '{email}', '{password}', '{age}')";
            
                using SqlCommand command = new(sql, connection);
                connection.Open();
            
                using SqlDataReader reader = command.ExecuteReader();
            
                return "Your account has been registered!";
            }
            catch (SqlException ex)
            {
                return ex.Number switch
                {
                    241 => "Invalid date.",
                    2601 => "E-mail address or username is already in use.",
                    _ => "Something went wrong on our end. Please try again later."
                };
            }
        }
    }
}