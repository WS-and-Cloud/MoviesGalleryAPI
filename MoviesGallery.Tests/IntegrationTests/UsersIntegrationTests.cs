namespace MoviesGallery.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using EntityFramework.Extensions;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MoviesGallery.Data;
    using MoviesGallery.Models;
    using MoviesGallery.Services;

    using Owin;

    [TestClass]
    public class UsersIntegrationTests
    {
        private const string TestEmail = "pesho@aasdasd.bg";

        private const string TestPassword = "Aa#1234";

        private static TestServer httpTestServer;

        private static HttpClient httpClient;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext)
        {
            httpTestServer = TestServer.Create(
                appBuilder =>
                {
                    var config = new HttpConfiguration();
                    WebApiConfig.Register(config);
                    var startup = new Startup();

                    startup.Configuration(appBuilder);
                    appBuilder.UseWebApi(config);
                });

            httpClient = httpTestServer.HttpClient;
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            if (httpTestServer != null)
            {
                httpTestServer.Dispose();
            }
        }

        [TestMethod]
        public void GetUserByGuid_WithCorrectData_ShouldReturnUserAnd200Ok()
        {
            var ctx = new MoviesGalleryDbContext();
            var user = ctx.Users.FirstOrDefault();
            var endPoint = "/api/user/" + user.Id;
            var httpResponse = httpClient.GetAsync(endPoint).Result;
            var result = httpResponse.Content.ReadAsAsync<ApplicationUser>().Result;

            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.UserName, result.UserName);
        }

        [TestMethod]
        public void GetAllUsers_ShouldReturnUsersAnd200Ok()
        {
            var ctx = new MoviesGalleryDbContext();
            var usersCount = ctx.Users.Count();
            var endPoint = "/api/user";
            var httpResponse = httpClient.GetAsync(endPoint).Result;
            var result = httpResponse.Content.ReadAsAsync<List<ApplicationUser>>().Result;

            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.AreEqual(usersCount, result.Count);
        }

        [TestMethod]
        public void GetUsers_ByExistingGender_ShouldReturnUsersAnd200Ok()
        {
            var ctx = new MoviesGalleryDbContext();
            var userGender = ctx.Users.Select(u => u.Gender).FirstOrDefault();
            
            var endPoint = "/api/user?gender=" + userGender;
            var httpResponse = httpClient.GetAsync(endPoint).Result;
            var result = httpResponse.Content.ReadAsAsync<List<ApplicationUser>>().Result;

            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.AreEqual(userGender, result[0].Gender);
        }

        public void AdUser()
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email", TestEmail),
                new KeyValuePair<string, string>("Password", TestPassword),
                new KeyValuePair<string, string>("ConfirmPassword", TestPassword),
                new KeyValuePair<string, string>("Username", TestEmail),
                new KeyValuePair<string, string>("PersonalPage", "asianbeauties.com"),
                new KeyValuePair<string, string>("BirthDate", "2015-09-06T15:06:49.5296859+03:00"),
                new KeyValuePair<string, string>("Gender", 1.ToString()),
            });

            var httpResponse = httpClient.PostAsync("/api/account/register", data).Result;
        }
    }
}