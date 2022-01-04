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
            
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<Movie>(response);
                Assert.IsTrue(success);
            });
        }

        [Test]
        public void GetMovie_ShouldFailWithBadId()
        {
            var success = ApiService.GetMovie(1, out Movie response);
            
            Assert.Multiple(() =>
            {
                Assert.IsNull(response);
                Assert.IsFalse(success);
            });
        }

        /*
         * GetShow
         */

        [Test]
        public void GetShow_ShouldSucceed()
        {
            var success = ApiService.GetShow(94605, out Show response);
            
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<Show>(response);
                Assert.IsTrue(success);
            });
        }

        [Test]
        public void GetShow_ShouldFailWithBadId()
        {
            var success = ApiService.GetShow(999999999, out Show response);
            
            Assert.Multiple(() =>
            {
                Assert.IsNull(response);
                Assert.IsFalse(success);
            });
        }

        /*
         * GetSeason
         */

        [Test]
        public void GetSeason_ShouldSucceed()
        {
            var success = ApiService.GetSeason(94605, 1, out Season response);
            
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<Season>(response);
                Assert.IsTrue(success);
            });
        }

        [Test]
        public void GetSeason_ShouldFailWithBadSeason()
        {
            var success = ApiService.GetSeason(94605, 999, out Season response);
            
            Assert.Multiple(() =>
            {
                Assert.IsNull(response);
                Assert.IsFalse(success);
            });
        }

        /*
         * GetEpisode
         */

        [Test]
        public void GetEpisode_ShouldSucceed()
        {
            var success = ApiService.GetEpisode(94605, 1, 1, out Episode response);
            
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<Episode>(response);
                Assert.IsTrue(success);
            });
        }

        [Test]
        public void GetEpisode_ShouldFailWithBadEpisode()
        {
            var success = ApiService.GetEpisode(94605, 1, 999, out Episode response);
            
            Assert.Multiple(() =>
            {
                Assert.IsNull(response);
                Assert.IsFalse(success);
            });
        }

        /*
         * GetProviders
         */

        [Test]
        public void GetProviders_ShouldSucceed()
        {
            var success = ApiService.GetProviders(ApiService.ShowBase, 94605, out Providers response);
            
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<Providers>(response);
                Assert.IsTrue(success);
            });
        }

        [Test]
        public void GetProviders_ShouldFailWithBadId()
        {
            var success = ApiService.GetProviders(ApiService.MovieBase, 1, out Providers response);
            
            Assert.Multiple(() =>
            {
                Assert.IsNull(response);
                Assert.IsFalse(success);
            });
        }

        [Test]
        public void GetProviders_ShouldFailWithBadApiBase()
        {
            var success = ApiService.GetProviders(ApiService.ShowBase, 566525, out Providers response);
            
            Assert.Multiple(() =>
            {
                Assert.IsNull(response);
                Assert.IsFalse(success);
            });
        }

        /*
         * Search
         */

        [Test]
        public void Search_ShouldSucceed()
        {
            var success = ApiService.Search("stranger", out MultiSearch response, false);
            
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<MultiSearch>(response);
                Assert.IsTrue(success);
            });
        }

        [Test]
        public void Search_ShouldFailWithBadApiBase()
        {
            var success = ApiService.Search("", out MultiSearch response, false);
            
            Assert.Multiple(() =>
            {
                Assert.IsNull(response);
                Assert.IsFalse(success);
            });
        }

        [Test]
        public void search_ShouldSucceedWithoutAdult_WhenInsertFalse()
        {
            var success = ApiService.Search("organ", out MultiSearch response, false);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(success);
                Assert.IsInstanceOf<MultiSearch>(response);
            });
            
            var isAdult = false;
            foreach (MultiSearchResult item in response.results)
            {
                if (item.adult == true)
                {
                    isAdult = true;
                }
            }
            
            Assert.IsFalse(isAdult);
        }

        [Test]
        public void Search_ShouldSucceedWithAdult_WhenInsertTrue()
        {
            var success = ApiService.Search("the office", out MultiSearch response, true);
            
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<MultiSearch>(response);
                Assert.IsTrue(success);
            });
            
            bool isAdult = false;
            foreach (MultiSearchResult item in response.results)
            {
                if (item.adult == true)
                {
                    isAdult = true;
                }
            }

            Assert.IsTrue(isAdult);
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
                {{"append_to_response", "videos,images,content_ratings,credits"}};

            var success = ApiService.Get<Episode>(ApiService.ShowBase, ApiService.GetShowEpsiode, urlSegments, urlParameters, out Episode response);
            
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<Episode>(response);
                Assert.IsTrue(success);
            });
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
                {{"append_to_response", "videos,images,content_ratings,credits"}};

            var success = ApiService.Get<Episode>(ApiService.ShowBase, ApiService.GetShowEpsiode, urlSegments, urlParameters, out Episode response);

            Assert.Multiple(() =>
            {
                Assert.IsNull(response);
                Assert.IsFalse(success);
            });
        }
        
        /*
            Cache
         */
        [Test]
        public void ApiCalls_ShouldCacheResponseInMemory()
        {
            var id = 524434;
            var urlSegments = new Dictionary<string, string>
                { { "id", id.ToString() } };
            var urlParameters = new Dictionary<string, string>
                { { "append_to_response", "videos,images,release_dates,credits" } };
            
            var cacheKey = ApiService.GenerateCacheKey(ApiService.MovieBase, ApiService.GetDetails, urlSegments, urlParameters);
            
            ApiService.CacheGetFromMemory(cacheKey, out Movie emptyCacheReturn);
            var success = ApiService.GetMovie(id, out Movie getMovieReturn);
            ApiService.CacheGetFromMemory(cacheKey, out Movie filledCacheReturn);
            
            Assert.Multiple(() =>
            {
                Assert.IsNull(emptyCacheReturn);
                Assert.IsTrue(success);
                Assert.IsInstanceOf<Movie>(getMovieReturn);
                Assert.IsInstanceOf<Movie>(filledCacheReturn);
            });
        }
        
        [Test]
        public void ApiCalls_ShouldCacheResponseInDatabase()
        {
            var id = 524434;
            var urlSegments = new Dictionary<string, string>
                { { "id", id.ToString() } };
            var urlParameters = new Dictionary<string, string>
                { { "append_to_response", "videos,images,release_dates,credits" } };
            var cacheKey = ApiService.GenerateCacheKey(ApiService.MovieBase, ApiService.GetDetails, urlSegments, urlParameters);
            
            ApiService.CacheGetFromDatabase(cacheKey, out Movie cacheReturn);

            if (cacheReturn == null)
            {
                var success = ApiService.GetMovie(id, out Movie getMovieReturn);
                ApiService.CacheGetFromDatabase(cacheKey, out Movie filledCacheReturn);
                
                Assert.Multiple(() =>
                {
                    Assert.IsNull(cacheReturn);
                    Assert.IsTrue(success);
                    Assert.IsInstanceOf<Movie>(getMovieReturn);
                    Assert.IsInstanceOf<Movie>(filledCacheReturn);
                });
            }
            else
            {
                Assert.IsInstanceOf<Movie>(cacheReturn);
            }
        }
        
    }
}