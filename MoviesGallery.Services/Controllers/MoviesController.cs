namespace MoviesGallery.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using MoviesGallery.Data;
    using MoviesGallery.Data.UoW;
    using MoviesGallery.Models;
    using MoviesGallery.Services.Models.MoviesModels;

    public class MoviesController : BaseApiController
    {
        public MoviesController()
            : base(new MoviesGalleryData(new MoviesGalleryDbContext()))
        {
        }

        // GET: api/Movies
        public IHttpActionResult GetMovies()
        {
            var movies = this.MoviesGalleryData.Movies.All().Select(ShortMovieDataModel.Create);
            return this.Ok(movies);
        }

        // GET: api/Movies/genreId={genreId}
        [Route("api/movies/genreId={genreId}")]
        public IHttpActionResult GetMoviesByGenreId(int genreId)
        {
            var movies =
                this.MoviesGalleryData.Movies.All()
                    .Where(m => m.Genre.Id == genreId)
                    .Select(ShortMovieDataModel.Create);
            return this.Ok(movies);
        }

        // GET: api/Movies/5
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = this.MoviesGalleryData.Movies.Find(id);
            if (movie == null)
            {
                return this.NotFound();
            }

            var result =
                this.MoviesGalleryData.Movies.All()
                    .Where(m => m.Id == movie.Id)
                    .Select(ShortMovieDataModel.Create)
                    .First();

            return this.Ok(result);
        }

        // PUT: api/Movies/5
        public IHttpActionResult PutMovie(int id, UpdateMovieInputModel movieModel)
        {
            if (movieModel == null)
            {
                return this.BadRequest("Model cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var movie = this.MoviesGalleryData.Movies.All().FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return this.NotFound();
            }

            movie.Title = movieModel.Title;
            movie.Length = movieModel.Length;
            movie.Ration = movieModel.Ration;
            movie.Country = movieModel.CountryName;
            movie.GenreId = movieModel.GenreId;
            
            this.MoviesGalleryData.Movies.Update(movie);
            this.MoviesGalleryData.SaveChanges();

            var result =
                this.MoviesGalleryData.Movies.All()
                    .Where(m => m.Id == movie.Id)
                    .Select(ShortMovieDataModel.Create)
                    .First();
            return this.Ok(result);
        }

        // POST: api/Movies
        [ResponseType(typeof(Movie))]
        public IHttpActionResult PostMovie(AddMovieInputModel movieModel)
        {
            if (movieModel == null)
            {
                return this.BadRequest("Model cannot be null");
            }
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (!this.MoviesGalleryData.Genres.All().Any(g => g.Id == movieModel.GenreId))
            {
                return this.BadRequest("Genre with such id does not exists");
            }

            if (movieModel.Ration > 10 || movieModel.Ration < 1)
            {
                return this.BadRequest("Ration must be in range [1..10]");
            }

            var movie = new Movie()
                        {
                            Title = movieModel.Title,
                            Length = movieModel.Length,
                            Ration = movieModel.Ration,
                            Country = movieModel.Country,
                            GenreId = movieModel.GenreId
                        };

            this.MoviesGalleryData.Movies.Add(movie);
            this.MoviesGalleryData.SaveChanges();
            var result = new { movie.Id, movie.Title };
            return this.CreatedAtRoute("DefaultApi", new { id = movie.Id }, result);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movie))]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = this.MoviesGalleryData.Movies.Find(id);
            if (movie == null)
            {
                return this.NotFound();
            }

            this.MoviesGalleryData.Movies.Delete(movie);
            this.MoviesGalleryData.SaveChanges();

            return this.Ok(movie);
        }
    }
}