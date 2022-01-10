using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Models.Database;
using Stores;

namespace Services
{
    public static class DatabaseService
    {
        private static string _sqlBuilder = ConfigService.Get["db-string"];

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
                                UserStore.id = reader.GetInt32(0);
                                UserStore.username = reader.GetString(1);
                                UserStore.email = reader.GetString(2);
                                UserStore.password = reader.GetString(3);
                                UserStore.birthYear = reader.GetDateTime(4);
                                UserStore.adult = reader.GetBoolean(5);
                                UserStore.provider = reader.GetBoolean(6);
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

        public static string UpdateUserGeneral(string userName, string email, string age, int id)
        {
            try
            {
                using SqlConnection connection = new(_sqlBuilder);

                string sql =
                    @$"UPDATE MatchMaker.Matchmaker.[user] SET username = '{userName}', email = '{email}', birth_date = '{age}' WHERE ID = {id}";

                using SqlCommand command = new(sql, connection);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();

                return "Your account has been updated!";
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

        public static string UpdateUserPassword(string password, int id)
        {
            try
            {
                using SqlConnection connection = new(_sqlBuilder);

                string sql =
                    @$"UPDATE MatchMaker.Matchmaker.[user] SET password = '{password}' WHERE ID = {id}";

                using SqlCommand command = new(sql, connection);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();

                return "Your password has been updated!";
            }
            catch (SqlException ex)
            {
                return ex.Number switch
                {
                    _ => "Something went wrong on our end. Please try again later."
                };
            }
        }

        public static string UpdateUserFilters(bool adult, bool otherProviders, int id)
        {
            try
            {
                using SqlConnection connection = new(_sqlBuilder);

                string sql =
                    @$"UPDATE MatchMaker.Matchmaker.[user] SET adult = '{adult}', providers = '{otherProviders}' WHERE ID = {id}";

                using SqlCommand command = new(sql, connection);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();

                return "Your filters have been updated!";
            }
            catch (SqlException ex)
            {
                return ex.Number switch
                {
                    _ => "Something went wrong on our end. Please try again later."
                };
            }
        }

        public static List<LikedContent> GetLikedContent(int userid)
        {
            using SqlConnection connection = new SqlConnection(_sqlBuilder);
            connection.Open();
            
            string sql = $"SELECT content_id, isShow FROM MatchMaker.Matchmaker.[content_review] WHERE liked = 'true' AND watched = 'true' AND user_id = {userid}";
            using SqlCommand command = new SqlCommand(sql, connection);

            using SqlDataReader reader = command.ExecuteReader();
            List<LikedContent> result = new List<LikedContent>();

            while (reader.Read())
            {
                var content = new LikedContent() { Content = reader.GetInt32(0), IsShow = reader.GetBoolean(1) };
                result.Add(content);
            }

            return result;
        }

        public static List<InterestedContent> GetInterestedContent(int userid)
        {
            using SqlConnection connection = new SqlConnection(_sqlBuilder);
            connection.Open();
            
            string sql = $"SELECT content_id, isShow FROM MatchMaker.Matchmaker.[content_review] WHERE liked = 'true' AND watched = 'false' AND user_id = {userid}";
            using SqlCommand command = new SqlCommand(sql, connection);
            
            using SqlDataReader reader = command.ExecuteReader();
            List<InterestedContent> result = new List<InterestedContent>();

            while (reader.Read())
            {
                var content = new InterestedContent() { Content = (int)reader.GetValue(0), IsShow = reader.GetBoolean(1) };
                result.Add(content);
            }

            return result;
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
                Console.WriteLine((string?)ex.Message);
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
                Console.WriteLine((string?)ex.Message);
            }
        }

        public static bool CheckIfReviewed(int id, int userId, bool isShow)
        {
            using SqlConnection connection = new SqlConnection(_sqlBuilder);
            connection.Open();
            
            string sql =
                @$"SELECT content_id FROM MatchMaker.Matchmaker.[content_review] WHERE content_id = '{id}' AND user_id = '{userId}' AND isShow = '{isShow}'";
            using SqlCommand command = new SqlCommand(sql, connection);
            
            using SqlDataReader reader = command.ExecuteReader();
            
            int? result = null;
            while (reader.Read()) result = reader.GetInt32(0);
            return result != null;
        }

        public static bool? CheckForLiked(int id, int userId, bool isShow)
        {
            using SqlConnection connection = new SqlConnection(_sqlBuilder);
            connection.Open();
            
            string sql =
                @$"SELECT liked FROM MatchMaker.Matchmaker.[content_review] WHERE content_id = '{id}' AND user_id = '{userId}' AND isShow = '{isShow}'";
            using SqlCommand command = new SqlCommand(sql, connection);
            
            using SqlDataReader reader = command.ExecuteReader();
            
            bool? result = null;
            while (reader.Read()) result = reader.GetBoolean(0);
            return result;
        }

        public static bool? CheckForWatched(int id, int userId, bool isShow)
        {
            using SqlConnection connection = new SqlConnection(_sqlBuilder);
            connection.Open();
            
            string sql =
                @$"SELECT watched FROM MatchMaker.Matchmaker.[content_review] WHERE content_id = '{id}' AND user_id = '{userId}' AND isShow = '{isShow}'";
            using SqlCommand command = new SqlCommand(sql, connection);
            
            using SqlDataReader reader = command.ExecuteReader();
            
            bool? result = null;
            while (reader.Read()) result = reader.GetBoolean(0);
            return result;
        }

        public static HashSet<int> GetReviewedMovies(int userId)
        {
            using SqlConnection connection = new(_sqlBuilder);
            connection.Open();
            
            string sql =
                @$"SELECT content_id FROM MatchMaker.Matchmaker.[content_review] WHERE user_id = '{userId}' AND isShow = 'false'";
            using SqlCommand command = new SqlCommand(sql, connection);
            
            using SqlDataReader reader = command.ExecuteReader();
            
            HashSet<int> reviewedMovies = new();
            if (!reader.HasRows) return reviewedMovies;
            while (reader.Read())
            {
                reviewedMovies.Add(reader.GetInt32(0));
            }

            return reviewedMovies;
        }

        public static List<Review> GetReviewedItems(int userId)
        {
            using SqlConnection connection = new(_sqlBuilder);
            connection.Open();
            
            string sql =
                @$"SELECT content_id, user_id, liked, watched, isShow, modifiedDate FROM MatchMaker.Matchmaker.[content_review] WHERE user_id = '{userId}' ORDER BY modifiedDate DESC";
            using SqlCommand command = new SqlCommand(sql, connection);
            
            using SqlDataReader reader = command.ExecuteReader();
            
            List<Review> reviewedItems = new();
            if (!reader.HasRows) return reviewedItems;
            while (reader.Read())
            {
                reviewedItems.Add(new Review(reader.GetInt32(0), reader.GetInt32(1), reader.GetBoolean(2),
                    reader.GetBoolean(3), reader.GetBoolean(4), reader.GetDateTime(5)));
            }

            return reviewedItems;
        }

        public static HashSet<int> GetInterestingAndLikedMovies()
        {
            using SqlConnection connection = new(_sqlBuilder);
            connection.Open();
            
            string sql =
                @$"SELECT content_id FROM MatchMaker.Matchmaker.[content_review] WHERE user_id = '{UserStore.id}' AND liked='true' AND isShow = 'false'";
            using SqlCommand command = new SqlCommand(sql, connection);
            
            using SqlDataReader reader = command.ExecuteReader();
            
            HashSet<int> reviewedMovies = new();
            if (!reader.HasRows) return reviewedMovies;
            while (reader.Read())
            {
                reviewedMovies.Add(reader.GetInt32(0));
            }

            return reviewedMovies;
        }

        public static List<Tuple<int, string>> GetWatchedCountGenres(int userid)
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                string sql =
                    "SELECT COUNT(id), name FROM MatchMaker.Matchmaker.[genre] G JOIN " +
                    "MatchMaker.Matchmaker.[content_review] CR ON G.content_id = CR.content_id WHERE watched = 'true' AND user_id = " +
                    userid + " GROUP BY name";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Tuple<int, string>> result = new List<Tuple<int, string>>();

                        while (reader.Read())
                        {
                            Tuple<int, string> content = new Tuple<int, string>((int)reader.GetValue(0), (string)reader.GetValue(1));
                            result.Add(content);
                        }

                        return result;
                    }
                }
            }
        }

        public static List<Tuple<int, string>> GetWatchedCountActors(int userid)
        {
            using (SqlConnection connection = new SqlConnection(_sqlBuilder))
            {
                string sql =
                    "SELECT COUNT(id), name FROM MatchMaker.Matchmaker.[actor] A JOIN " +
                    "MatchMaker.Matchmaker.[content_review] CR ON A.content_id = CR.content_id WHERE watched = 'true' AND user_id = " +
                    userid + " GROUP BY name";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Tuple<int, string>> result = new List<Tuple<int, string>>();

                        while (reader.Read())
                        {
                            Tuple<int, string> content = new Tuple<int, string>((int)reader.GetValue(0), (string)reader.GetValue(1));
                            result.Add(content);
                        }

                        return result;
                    }
                }
            }
        }

        #region Cache queries

        public static string GetCache(string cacheKey)
        {
            using SqlConnection connection = new(_sqlBuilder);
            connection.Open();

            using SqlCommand command = new(
                @$"SELECT json from MatchMaker.Matchmaker.[content] WHERE cache_key = @CacheKey",
                connection
            );
            command.Parameters.Add("@CacheKey", SqlDbType.VarChar).Value = cacheKey;

            using SqlDataReader reader = command.ExecuteReader();
            string json = string.Empty;
            while (reader.Read())
            {
                json = reader.GetString(0);
            }

            return json;
        }

        public static bool InsertCache(CacheInsert cacheInsert)
        {
            using SqlConnection connection = new(_sqlBuilder);
            connection.Open();

            // Inserting Movie or Show into Content
            using SqlCommand contentCommand = new(
                @$"INSERT INTO MatchMaker.Matchmaker.[content] (id,cache_key,title,overview,poster_path,backdrop_path,trailer_url,age,json,is_show) VALUES (@Id,@CacheKey,@Title,@Overview,@PosterPath,@BackdropPath,@TrailerUrl,@Age,@Json,@IsShow);",
                connection
            );
            contentCommand.Parameters.Add("@Id", SqlDbType.Int).Value = cacheInsert.Id;
            contentCommand.Parameters.Add("@CacheKey", SqlDbType.VarChar).Value = cacheInsert.CacheKey;
            contentCommand.Parameters.Add("@Title", SqlDbType.VarChar).Value = cacheInsert.Title ?? string.Empty;
            contentCommand.Parameters.Add("@Overview", SqlDbType.VarChar).Value = cacheInsert.Overview ?? string.Empty;
            contentCommand.Parameters.Add("@PosterPath", SqlDbType.VarChar).Value = cacheInsert.PosterPath ?? string.Empty;
            contentCommand.Parameters.Add("@BackdropPath", SqlDbType.VarChar).Value = cacheInsert.BackdropPath ?? string.Empty;
            contentCommand.Parameters.Add("@TrailerUrl", SqlDbType.VarChar).Value = cacheInsert.TrailerUrl ?? string.Empty;
            contentCommand.Parameters.Add("@Age", SqlDbType.Int).Value = cacheInsert.Age;
            contentCommand.Parameters.Add("@Json", SqlDbType.VarChar).Value = cacheInsert.Json ?? string.Empty;
            contentCommand.Parameters.Add("@IsShow", SqlDbType.Bit).Value = cacheInsert.IsShow;

            contentCommand.ExecuteNonQuery();

            // Inserting Actors
            using SqlCommand actorsCommand = new(
                $@"INSERT INTO MatchMaker.Matchmaker.[actor] (id,content_id,is_show,name,character_name) VALUES (@Id,@ContentId,@IsShow,@Name,@CharacterName)",
                connection
            );
            actorsCommand.Parameters.Add("@Id", SqlDbType.Int);
            actorsCommand.Parameters.Add("@ContentId", SqlDbType.Int);
            actorsCommand.Parameters.Add("@IsShow", SqlDbType.Bit);
            actorsCommand.Parameters.Add("@Name", SqlDbType.VarChar);
            actorsCommand.Parameters.Add("@CharacterName", SqlDbType.VarChar);

            foreach (var actor in cacheInsert.Actors)
            {
                actorsCommand.Parameters[0].Value = actor.id;
                actorsCommand.Parameters[1].Value = cacheInsert.Id;
                actorsCommand.Parameters[2].Value = cacheInsert.IsShow;
                actorsCommand.Parameters[3].Value = actor.name ?? string.Empty;
                actorsCommand.Parameters[4].Value = actor.character ?? string.Empty;
                actorsCommand.ExecuteNonQuery();
            }

            // Inserting Genres
            using SqlCommand genresCommand = new(
                $@"INSERT INTO MatchMaker.Matchmaker.[genre] (id,content_id,is_show,name) VALUES (@Id,@ContentId,@IsShow,@Name)",
                connection
            );
            genresCommand.Parameters.Add("@Id", SqlDbType.Int);
            genresCommand.Parameters.Add("@ContentId", SqlDbType.Int);
            genresCommand.Parameters.Add("@IsShow", SqlDbType.Bit);
            genresCommand.Parameters.Add("@Name", SqlDbType.VarChar);

            foreach (var genre in cacheInsert.Genres)
            {
                genresCommand.Parameters[0].Value = genre.id;
                genresCommand.Parameters[1].Value = cacheInsert.Id;
                genresCommand.Parameters[2].Value = cacheInsert.IsShow;
                genresCommand.Parameters[3].Value = genre.name ?? string.Empty;
                genresCommand.ExecuteNonQuery();
            }

            return true;
        }

        #endregion
    }
}