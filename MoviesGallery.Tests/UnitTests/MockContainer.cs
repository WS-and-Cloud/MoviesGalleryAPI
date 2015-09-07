namespace MoviesGallery.Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using MoviesGallery.Data.Repositories;
    using MoviesGallery.Models;

    public class MockContainer
    {
        public Mock<IRepository<Author>> AuthorRepositoryMock { get; set; }

        public Mock<IRepository<Movie>> MovieRepositoryMock { get; set; }

        public Mock<IRepository<Genre>> GenreRepositoryMock { get; set; }

        public Mock<IRepository<Review>> ReviewRepositoryMock { get; set; }

        public Mock<IRepository<ApplicationUser>> ApplicationUserRepositoryMock { get; set; }

        public void PrepareMocks()
        {
            this.SetupFakeUsers();
            this.SetupFakeAuthors();
            this.SetupFakeMovies();
            this.SetupFakeGenres();
            this.SetupFakeReviews();
        }

        private void SetupFakeReviews()
        {
            var fakeGenres = new List<Genre>()
                              {
                                  new Genre() {Id = 1, Name = "Author #1" },
                                  new Genre() {Id = 2, Name = "Author #2" }
                              };

            var fakeMovies = new List<Movie>()
            {
                new Movie() { Id = 1, Title = "Movie #1", Length = 2, Ration = 3, Country = "BG", GenreId = fakeGenres[0].Id },
                new Movie() { Id = 2, Title = "Movie #2", Length = 22, Ration = 3, Country = "BG", GenreId = fakeGenres[0].Id },
            };

            var fakeReviews = new List<Review>()
            {
                new Review() {Id = 1, Content = "Review #1", MovieId = fakeMovies[0].Id, DateOfCreation = DateTime.Now },
                new Review() {Id = 2, Content = "Review #2", MovieId = fakeMovies[1].Id, DateOfCreation = DateTime.Now },
            };

            this.ReviewRepositoryMock = new Mock<IRepository<Review>>();
            this.ReviewRepositoryMock.Setup(r => r.All()).Returns(fakeReviews.AsQueryable());
            this.ReviewRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    return fakeReviews.FirstOrDefault(u => u.Id == id);
                });
        }

        private void SetupFakeGenres()
        {
            var fakeGenres = new List<Genre>()
                              {
                                  new Genre() {Id = 1, Name = "Author #1" },
                                  new Genre() {Id = 2, Name = "Author #2" }
                              };
            this.GenreRepositoryMock = new Mock<IRepository<Genre>>();
            this.GenreRepositoryMock.Setup(r => r.All()).Returns(fakeGenres.AsQueryable());
            this.GenreRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    return fakeGenres.FirstOrDefault(u => u.Id == id);
                });
        }

        private void SetupFakeMovies()
        {
            var genres = new List<Genre>()
            {
                new Genre() { Id = 1, Name = "Horror"}
            };

            var fakeMovies = new List<Movie>()
            {
                new Movie() {Id = 1, Title = "Movie #1", Length = 2, Ration = 3, Country = "BG", GenreId = genres[0].Id },
                new Movie() {Id = 2, Title = "Movie #2", Length = 22, Ration = 3, Country = "BG", GenreId = genres[0].Id },
            };
            this.MovieRepositoryMock = new Mock<IRepository<Movie>>();
            this.MovieRepositoryMock.Setup(r => r.All()).Returns(fakeMovies.AsQueryable());
            this.MovieRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    return fakeMovies.FirstOrDefault(u => u.Id == id);
                });
        }

        private void SetupFakeAuthors()
        {
            var fakeAuthors = new List<Author>()
                              {
                                  new Author() { Id = 1, Name = "Author #1", DateOfBirth = new DateTime(2000, 10, 10)},
                                  new Author() { Id = 2, Name = "Author #2", DateOfBirth = new DateTime(2000, 10, 10)}
                              };
            this.AuthorRepositoryMock = new Mock<IRepository<Author>>();
            this.AuthorRepositoryMock.Setup(r => r.All()).Returns(fakeAuthors.AsQueryable());
            this.AuthorRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    return fakeAuthors.FirstOrDefault(u => u.Id == id);
                });
        }

        private void SetupFakeUsers()
        {
            var fakeUsers = new List<ApplicationUser>()
                            {
                                new ApplicationUser() { Id = "123", UserName = "Pesho#1" },
                                new ApplicationUser() { Id = "232", UserName = "Pesho#2" },
                                new ApplicationUser() { Id = "323", UserName = "Pesho#3" },
                            };
            this.ApplicationUserRepositoryMock = new Mock<IRepository<ApplicationUser>>();
            this.ApplicationUserRepositoryMock.Setup(r => r.All()).Returns(fakeUsers.AsQueryable());
            this.ApplicationUserRepositoryMock.Setup(r => r.Find(It.IsAny<int>())).Returns(
                (string id) =>
                {
                    return fakeUsers.FirstOrDefault(u => u.Id == id);
                });
        }
    }
}