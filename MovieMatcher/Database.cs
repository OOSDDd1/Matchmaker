using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using MovieMatcher.Models.Api;
using MovieMatcher.Models.Database;
using MovieMatcher.Models.Api.Components;

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
                        if (reader.HasRows)
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

                string sql =
                    @$"INSERT INTO MatchMaker.Matchmaker.[user] (username, email, password, birth_date) VALUES ('{userName}', '{email}', '{password}', '{age}')";

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

        public static List<dynamic> GetLikedContent(int userid)
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                string sql =
                    "SELECT content_id, isShow FROM MatchMaker.Matchmaker.[content_review] WHERE liked = 'true' AND watched = 'true' AND user_id = " +
                    userid;
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<dynamic> result = new List<dynamic>();

                        while (reader.Read())
                        {
                            if ((bool) reader.GetValue(1) == true)
                            {
                                var content = new {content = (int) reader.GetValue(0), isShow = 1};
                                result.Add(content);
                            }
                            else
                            {
                                var content = new {content = (int) reader.GetValue(0), isShow = 0};
                                result.Add(content);
                            }
                        }

                        return result;
                    }
                }
            }
        }

        public static List<dynamic> GetInterestedContent(int userid)
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                string sql =
                    "SELECT content_id, isShow FROM MatchMaker.Matchmaker.[content_review] WHERE liked = 'true' AND watched = 'false' AND user_id = " +
                    userid;
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<dynamic> result = new List<dynamic>();

                        while (reader.Read())
                        {
                            if ((bool) reader.GetValue(1) == true)
                            {
                                var content = new {content = (int) reader.GetValue(0), isShow = 1};
                                result.Add(content);
                            }
                            else
                            {
                                var content = new {content = (int) reader.GetValue(0), isShow = 0};
                                result.Add(content);
                            }
                        }

                        return result;
                    }
                }
            }
        }

        public static void InsertItem(int id, int userId, bool liked, bool watched, bool isShow)
        {
            try
            {
                using SqlConnection connection = new(_sqlBuilder);
                string sql =
                    $@"INSERT INTO MatchMaker.MatchMaker.[content_review] (content_id, user_id, liked, watched, isShow) VALUES('{id}','{userId}','{liked}','{watched}','{isShow}')";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ChangeItem(int id, int userId, bool liked, bool watched)
        {
            try
            {
                using SqlConnection connection = new(_sqlBuilder);
                string sql =
                    $@"UPDATE MatchMaker.MatchMaker.[content_review] SET liked = '{liked}', watched = '{watched}' WHERE content_id = '{id}' AND user_id = '{userId}'";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public static bool CheckIfReviewed(int id, int userId, bool isShow)
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                string sql =
                    @$"SELECT content_id FROM MatchMaker.Matchmaker.[content_review] WHERE content_id = '{id}' AND user_id = '{userId}' AND isShow = '{isShow}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int? result = null;
                        while (reader.Read()) result = reader.GetInt32(0);
                        return result != null;
                    }
                }
            }
        }

        public static bool? CheckForLiked(int id, int userId, bool isShow)
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                string sql =
                    @$"SELECT liked FROM MatchMaker.Matchmaker.[content_review] WHERE content_id = '{id}' AND user_id = '{userId}' AND isShow = '{isShow}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        bool? result = null;
                        while (reader.Read()) result = reader.GetBoolean(0);
                        return result;
                    }
                }
            }
        }

        public static bool? CheckForWatched(int id, int userId, bool isShow)
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                string sql =
                    @$"SELECT watched FROM MatchMaker.Matchmaker.[content_review] WHERE content_id = '{id}' AND user_id = '{userId}' AND isShow = '{isShow}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        bool? result = null;
                        while (reader.Read()) result = reader.GetBoolean(0);
                        return result;
                    }
                }
            }
        }

        public static HashSet<int> GetReviewedMovies(int userId)
        {
            using (SqlConnection connection = new(_sqlBuilder))
            {
                string sql =
                    @$"SELECT content_id FROM MatchMaker.Matchmaker.[content_review] WHERE user_id = '{userId}' AND isShow = 'false'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        HashSet<int> reviewedMovies = new();
                        if (!reader.HasRows) return reviewedMovies;
                        while (reader.Read())
                        {
                            reviewedMovies.Add(reader.GetInt32(0));
                        }

                        return reviewedMovies;
                    }
                }
            }
        }

        public static List<DataBaseItem> GetReviewedItems(int userId)
        {
            using (SqlConnection connection = new(_sqlBuilder))
            {
                string sql =
                    @$"SELECT content_id, user_id, liked, watched, isShow, modifiedDate FROM MatchMaker.Matchmaker.[content_review] WHERE user_id = '{userId}' ORDER BY modifiedDate DESC";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<DataBaseItem> reviewedItems = new();
                        if (!reader.HasRows) return reviewedItems;
                        while (reader.Read())
                        {
                            reviewedItems.Add(new DataBaseItem(reader.GetInt32(0), reader.GetInt32(1), reader.GetBoolean(2), reader.GetBoolean(3), reader.GetBoolean(4), reader.GetDateTime(5) ));
                        }

                        return reviewedItems;
                    }
                }
            }
        }

        public static HashSet<int> GetInterestingAndLikedMovies()
        {
            using (SqlConnection connection = new(_sqlBuilder))
            {
                string sql =
                    @$"SELECT content_id FROM MatchMaker.Matchmaker.[content_review] WHERE user_id = '{UserInfo.Id}' AND liked='true' AND isShow = 'false'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        HashSet<int> reviewedMovies = new();
                        if (!reader.HasRows) return reviewedMovies;
                        while (reader.Read())
                        {
                            reviewedMovies.Add(reader.GetInt32(0));
                        }

                        return reviewedMovies;
                    }
                }
            }
        }
    }
}