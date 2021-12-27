using System.Collections.Generic;
using Models.Api;
using Models.Api.Components;
using NUnit.Framework;
using Services;
using Season = Models.Api.Season;

namespace Tests
{
    [TestFixture]
    public class ApiServiceTests
    {
        /*
         * GetMovie
         */

        [Test]
        public void GetMovie_ShouldSucceed()
        {
            var success = ApiService.GetMovie(370172, out Movie response);
            Assert.IsInstanceOf<Movie>(response);
            Assert.IsTrue(success);
        }

        [Test]
        public void GetMovie_ShouldFailWithBadId()
        {
            var success = ApiService.GetMovie(1, out Movie response);
            Assert.IsNull(response);
            Assert.IsFalse(success);
        }

        /*
         * GetShow
         */

        [Test]
        public void GetShow_ShouldSucceed()
        {
            var success = ApiService.GetShow(94605, out Show response);
            Assert.IsInstanceOf<Show>(response);
            Assert.IsTrue(success);
        }

        [Test]
        public void GetShow_ShouldFailWithBadId()
        {
            var success = ApiService.GetShow(999999999, out Show response);
            Assert.IsNull(response);
            Assert.IsFalse(success);
        }

        /*
         * GetSeason
         */

        [Test]
        public void GetSeason_ShouldSucceed()
        {
            var success = ApiService.GetSeason(94605, 1, out Season response);
            Assert.IsInstanceOf<Season>(response);
            Assert.IsTrue(success);
        }

        [Test]
        public void GetSeason_ShouldFailWithBadSeason()
        {
            var success = ApiService.GetSeason(94605, 999, out Season response);
            Assert.IsNull(response);
            Assert.IsFalse(success);
        }

        /*
         * GetEpisode
         */

        [Test]
        public void GetEpisode_ShouldSucceed()
        {
            var success = ApiService.GetEpisode(94605, 1, 1, out Episode response);
            Assert.IsInstanceOf<Episode>(response);
            Assert.IsTrue(success);
        }

        [Test]
        public void GetEpisode_ShouldFailWithBadEpisode()
        {
            var success = ApiService.GetEpisode(94605, 1, 999, out Episode response);
            Assert.IsNull(response);
            Assert.IsFalse(success);
        }

        /*
         * GetProviders
         */

        [Test]
        public void GetProviders_ShouldSucceed()
        {
            var success = ApiService.GetProviders(ApiService.ShowBase, 94605, out Providers response);
            Assert.IsInstanceOf<Providers>(response);
            Assert.IsTrue(success);
        }

        [Test]
        public void GetProviders_ShouldFailWithBadId()
        {
            var success = ApiService.GetProviders(ApiService.MovieBase, 1, out Providers response);
            Assert.IsNull(response);
            Assert.IsFalse(success);
        }

        [Test]
        public void GetProviders_ShouldFailWithBadApiBase()
        {
            var success = ApiService.GetProviders(ApiService.ShowBase, 566525, out Providers response);
            Assert.IsNull(response);
            Assert.IsFalse(success);
        }

        /*
         * Search
         */

        [Test]
        public void Search_ShouldSucceed()
        {
            var success = ApiService.Search("stranger", out MultiSearch response, false);
            Assert.IsInstanceOf<MultiSearch>(response);
            Assert.IsTrue(success);
        }

        [Test]
        public void Search_ShouldFailWithBadApiBase()
        {
            var success = ApiService.Search("", out MultiSearch response, false);
            Assert.IsNull(response);
            Assert.IsFalse(success);
        }

        [Test]
        public void search_ShouldSucceedWithoutAdult_WhenInsertFalse()
        {
            bool adult = false;
            var success = ApiService.Search("organ", out MultiSearch response, false);
            foreach (MultiSearchResult item in response.results)
            {
                if (item.adult == true)
                {
                    adult = true;
                }
            }

            Assert.IsInstanceOf<MultiSearch>(response);
            Assert.IsTrue(success);
            Assert.IsFalse(adult);
        }

        [Test]
        public void Search_ShouldSucceedWithAdult_WhenInsertTrue()
        {
            bool adult = false;
            var success = ApiService.Search("organ", out MultiSearch response, true);
            foreach (MultiSearchResult item in response.results)
            {
                if (item.adult == true)
                {
                    adult = true;
                }
            }

            Assert.IsInstanceOf<MultiSearch>(response);
            Assert.IsTrue(success);
            Assert.IsTrue(adult);
        }

        /*
           Get<T>
         */

        [Test]
        public void Get_ShouldSucceed()
        {
            var urlSegments = new Dictionary<string, string>
            {
                {"id", 94605.ToString()},
                {"season", 1.ToString()},
                {"episode", 1.ToString()}
            };
            var urlParameters = new Dictionary<string, string>
                {{"append_to_response", "videos,images"}};

            var success = ApiService.Get<Episode>(ApiService.ShowBase, ApiService.GetShowEpsiode, urlSegments, urlParameters, out Episode response);

            Assert.IsInstanceOf<Episode>(response);
            Assert.IsTrue(success);
        }

        [Test]
        public void Get_ShouldFail_WithWrongInput()
        {
            var urlSegments = new Dictionary<string, string>
            {
                {"id", 94605.ToString()},
                {"season", 3.ToString()},
                {"episode", 999.ToString()}
            };
            var urlParameters = new Dictionary<string, string>
                {{"append_to_response", "videos,images"}};

            var success = ApiService.Get<Episode>(ApiService.ShowBase, ApiService.GetShowEpsiode, urlSegments, urlParameters, out Episode response);

            Assert.IsNull(response);
            Assert.IsFalse(success);
        }
    }
}