//using System.Collections.Generic;
//using MovieMatcher;
//using MovieMatcher.Models.Api;
//using NUnit.Framework;

//namespace Tests
//{
//    [TestFixture]
//    public class ApiTests
//    {
//        /*
//         * GetMovie(int id)
//         */

//        [Test]
//        public void GetMovie_ShouldReturnMovieWhenGivenId()
//        {
//            var result = Api.GetMovie(370172);
//            Assert.IsInstanceOf<Movie>(result);
//        }

//        [Test]
//        public void GetMovie_ShouldReturnMessageWithBadId()
//        {
//            var result = Api.GetMovie(1);
//            Assert.IsInstanceOf<Message>(result);
//        }

//        /*
//         * GetShow(int id)
//         */

//        [Test]
//        public void GetShow_ShouldReturnShowWhenGivenId()
//        {
//            var result = Api.GetShow(94605);
//            Assert.IsInstanceOf<Show>(result);
//        }

//        [Test]
//        public void GetShow_ShouldReturnMessageWithBadId()
//        {
//            var result = Api.GetShow(999999999);
//            Assert.IsInstanceOf<Message>(result);
//        }

//        /*
//         * GetSeason(int id, int season)
//         */

//        [Test]
//        public void GetSeason_ShouldReturnSeason()
//        {
//            var result = Api.GetSeason(94605, 1);
//            Assert.IsInstanceOf<Season>(result);
//        }

//        [Test]
//        public void GetSeason_ShouldReturnMessageWithBadSeason()
//        {
//            var result = Api.GetSeason(94605, 999);
//            Assert.IsInstanceOf<Message>(result);
//        }

//        /*
//         * GetEpisode(int id, int season, int episode)
//         */

//        [Test]
//        public void GetEpisode_ShouldReturnEpisode()
//        {
//            var result = Api.GetEpisode(94605, 1, 1);
//            Assert.IsInstanceOf<Episode>(result);
//        }

//        [Test]
//        public void GetEpisode_ShouldReturnMessageWithBadEpisode()
//        {
//            var result = Api.GetEpisode(94605, 1, 999);
//            Assert.IsInstanceOf<Message>(result);
//        }

//        /*
//         * GetProviders(string resourceBase, int id)
//         */

//        [Test]
//        public void GetProviders_ShouldReturnProviders()
//        {
//            var result = Api.GetProviders(Api.ShowBase, 94605);
//            Assert.IsInstanceOf<Providers>(result);
//        }

//        [Test]
//        public void GetProviders_ShouldReturnMessageWithBadId()
//        {
//            var result = Api.GetProviders(Api.MovieBase, 1);
//            Assert.IsInstanceOf<Message>(result);
//        }

//        [Test]
//        public void GetProviders_ShouldReturnMessageWithBadApiBase()
//        {
//            var result = Api.GetProviders(Api.ShowBase, 566525);
//            Assert.IsInstanceOf<Message>(result);
//        }

//        /*
//           Get<T>(
//                string resourceBase, 
//                string resource, 
//                Dictionary<string, string> urlSegments,
//                Dictionary<string, string> urlParameters
//            );
//         */

//        [Test]
//        public void Get_ShouldNotReturnMessage()
//        {
//            var urlSegments = new Dictionary<string, string>
//            {
//                {"id", 94605.ToString()},
//                {"season", 1.ToString()},
//                {"episode", 1.ToString()}
//            };
//            var urlParameters = new Dictionary<string, string>
//                {{"append_to_response", "videos,images"}};

//            var result = Api.Get<Episode>(Api.ShowBase, Api.GetShowEpsiode, urlSegments, urlParameters);

//            Assert.IsInstanceOf<Episode>(result);
//        }

//        [Test]
//        public void Get_ShouldReturnMessage()
//        {
//            var urlSegments = new Dictionary<string, string>
//            {
//                {"id", 94605.ToString()},
//                {"season", 3.ToString()},
//                {"episode", 999.ToString()}
//            };
//            var urlParameters = new Dictionary<string, string>
//                {{"append_to_response", "videos,images"}};

//            var result = Api.Get<Episode>(Api.ShowBase, Api.GetShowEpsiode, urlSegments, urlParameters);

//            Assert.IsInstanceOf<Message>(result);
//        }
//    }
//}