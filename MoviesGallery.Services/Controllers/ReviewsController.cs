namespace MoviesGallery.Services.Controllers
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Description;
    using MoviesGallery.Data;
    using MoviesGallery.Data.UoW;
    using MoviesGallery.Models;
    using MoviesGallery.Services.Models.UsersModels;

    public class ReviewsController : BaseApiController
    {
        public ReviewsController()
            : base(new MoviesGalleryData(new MoviesGalleryDbContext()))
        {
        }


        // GET: api/Reviews/movieId={movieId}
        [Route("api/reviews/movieId={movieId}")]
        public IHttpActionResult GetReviewByMovieId(int movieId)
        {
            var movie = this.MoviesGalleryData.Movies.Find(movieId);
            if (movie == null)
            {
                return this.NotFound();
            }

            var review =
                this.MoviesGalleryData.Reviews.All()
                    .Where(r => r.MovieId == movie.Id)
                    .Select(ShortReviewDataModel.Create);

            return this.Ok(review);
        }
        
        // POST: api/Reviews
        [ResponseType(typeof(Review))]
        public IHttpActionResult PostReview(AddReviewInputModel reviewModel)
        {
            if (reviewModel == null)
            {
                return this.BadRequest("Model cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            var movieId = this.MoviesGalleryData.Movies.All().FirstOrDefault(m => m.Id == reviewModel.MovieId);
            if (movieId == null)
            {
                return this.BadRequest("Movie with such id does not exists");
            }

            var review = new Review()
                         {
                             Content = reviewModel.Content,
                             MovieId = reviewModel.MovieId,
                             UserId = reviewModel.UserId,
                             DateOfCreation = reviewModel.DateOfCreation
                         };
            this.MoviesGalleryData.Reviews.Add(review);
            this.MoviesGalleryData.SaveChanges();

            return this.CreatedAtRoute("DefaultApi", new { id = review.Id }, new
                                                                             {
                                                                                 review.Id,
                                                                                 review.Content
                                                                             });
        }

        // DELETE: api/Reviews/5
        [ResponseType(typeof(Review))]
        public IHttpActionResult DeleteReview(int id)
        {
            Review review = this.MoviesGalleryData.Reviews.Find(id);
            if (review == null)
            {
                return this.NotFound();
            }

            this.MoviesGalleryData.Reviews.Delete(review);
            this.MoviesGalleryData.SaveChanges();

            return this.Ok(new
                           {
                               review.Id,
                               review.Content
                           });
        }
    }
}