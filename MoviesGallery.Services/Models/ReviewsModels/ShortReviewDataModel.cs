namespace MoviesGallery.Services.Models.UsersModels
{
    using System;
    using System.Linq.Expressions;

    using MoviesGallery.Models;

    public class ShortReviewDataModel
    {
        public static Expression<Func<Review, ShortReviewDataModel>> Create
        {
            get
            {
                return
                    r =>
                    new ShortReviewDataModel()
                    {
                        Id = r.Id,
                        MovieId = r.MovieId,
                        UserId = r.UserId,
                        Content = r.Content,
                        DateOfCreation = r.DateOfCreation
                    };
            }
        } 

        public int Id { get; set; }

        public int MovieId { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}