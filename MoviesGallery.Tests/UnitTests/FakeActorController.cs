namespace MoviesGallery.Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using MoviesGallery.Data.UoW;
    using MoviesGallery.Models;
    using MoviesGallery.Services.Controllers;
    using MoviesGallery.Services.Models.UsersModels;

    [TestClass]
    public class FakeActorController
    {
        private MockContainer mockContainer;

        [TestInitialize]
        public void InitTest()
        {
            this.mockContainer = new MockContainer();
            this.mockContainer.PrepareMocks();
        }

        [TestMethod]
        public void GetAuthorById_WithValidData_ShouldReturnAuthorAnd200Ok()
        {
            // Arrange
            var fakeAuthors = this.mockContainer.AuthorRepositoryMock.Object.All();
            var mockContext = new Mock<IMoviesGalleryData>();
            mockContext.Setup(r => r.Authors.All()).Returns(fakeAuthors.AsQueryable());
            var author = fakeAuthors.FirstOrDefault();
            // Act
            var controller = new AuthorsController(mockContext.Object);
            this.SetupController(controller);

            var httpResponse = controller.GetAuthorById(1).ExecuteAsync(CancellationToken.None).Result;

            var result = httpResponse.Content.ReadAsAsync<ShortActorDataModel>().Result;
            
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.AreEqual(author.Name, result.Name);
        }

        [TestMethod]
        public void GetAuthorById_WithInvalidData_ShouldReturn404NotFound()
        {
            // Arrange
            var fakeAuthors = this.mockContainer.AuthorRepositoryMock.Object.All();
            var mockContext = new Mock<IMoviesGalleryData>();
            mockContext.Setup(r => r.Authors.All()).Returns(fakeAuthors.AsQueryable());

            // Act
            var controller = new AuthorsController(mockContext.Object);
            this.SetupController(controller);

            var httpResponse = controller.GetAuthorById(111).ExecuteAsync(CancellationToken.None).Result;
            
            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }

        [TestMethod]
        public void AddAuthor_WithCorrectData_ShouldAddAuthorAndReturn201Created()
        {
            // Arrange
            var authors = new List<Author>();

            // Act
            var mockContext = new Mock<IMoviesGalleryData>();

            mockContext.Setup(r => r.Authors).Returns(this.mockContainer.AuthorRepositoryMock.Object);

            var controller = new AuthorsController(mockContext.Object);
            this.SetupController(controller);
            this.mockContainer.AuthorRepositoryMock.Setup(r => r.Add(It.IsAny<Author>()))
                .Callback((Author author) =>
                {
                    authors.Add(author);
                });
            var authorModel = new AddAuthorInputModel() { Name = "TestAuthor", DateOfBirth = new DateTime(2000, 1, 1) };

            var httpPostResponse = controller.PostAuthor(authorModel).ExecuteAsync(CancellationToken.None).Result;

            var result = httpPostResponse.Content.ReadAsAsync<ShortActorDataModel>().Result;

            // Assert
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.AreEqual(1, authors.Count);
            Assert.AreEqual(authorModel.Name, result.Name);
        }

        [TestMethod]
        public void AddAuthor_WithInvalidData_ShouldReturn400BadRequest()
        {
            // Arrange
            var authors = new List<Author>();

            // Act
            var mockContext = new Mock<IMoviesGalleryData>();

            mockContext.Setup(r => r.Authors).Returns(this.mockContainer.AuthorRepositoryMock.Object);

            var controller = new AuthorsController(mockContext.Object);
            this.SetupController(controller);
            this.mockContainer.AuthorRepositoryMock.Setup(r => r.Add(It.IsAny<Author>()))
                .Callback((Author author) =>
                {
                    authors.Add(author);
                });
            var httpPostResponse = controller.PostAuthor(null).ExecuteAsync(CancellationToken.None).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, httpPostResponse.StatusCode);
            Assert.AreEqual(0, authors.Count);
        }

        [TestMethod]
        public void DeleteAuthor_WithValidData_ShouldDeleteAuthorAndReturn200Ok()
        {
            // Arrange
            var fakeAuthors = this.mockContainer.AuthorRepositoryMock.Object.All();
            var mockContext = new Mock<IMoviesGalleryData>();
            mockContext.Setup(r => r.Authors.All()).Returns(fakeAuthors.AsQueryable());
            
            // Act
            var controller = new AuthorsController(mockContext.Object);
            this.SetupController(controller);
            var httpDeleteResponse = controller.DeleteAuthor(1).ExecuteAsync(CancellationToken.None).Result;
           
            // Assert
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.AreEqual(HttpStatusCode.OK, httpDeleteResponse.StatusCode);
        }

        [TestMethod]
        public void DeleteAuthor_WithInvalidData_ShouldReturn404NotFound()
        {

            // Arrange
            var fakeAuthors = this.mockContainer.AuthorRepositoryMock.Object.All();
            var mockContext = new Mock<IMoviesGalleryData>();
            mockContext.Setup(r => r.Authors.All()).Returns(fakeAuthors.AsQueryable());

            // Act
            var controller = new AuthorsController(mockContext.Object);
            this.SetupController(controller);
            var httpDeleteResponse = controller.DeleteAuthor(11111).ExecuteAsync(CancellationToken.None).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, httpDeleteResponse.StatusCode);
        }

        private void SetupController(BaseApiController controller)
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
        }
    }
}