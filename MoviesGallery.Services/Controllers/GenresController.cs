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
    using MoviesGallery.Services.Models.GenresModels;

    public class GenresController : BaseApiController
    {
        public GenresController()
            : base(new MoviesGalleryData(new MoviesGalleryDbContext()))
        {
        }

        // GET: api/Genres
        public IHttpActionResult GetAllGenres()
        {
            var genres = this.MoviesGalleryData.Genres.All().Select(GenresViewModel.Create);

            return this.Ok(genres);
        }

        // GET: api/Genres/5
        [ResponseType(typeof(Genre))]
        public IHttpActionResult GetGenre(int id)
        {
            Genre genre = this.MoviesGalleryData.Genres.Find(id);
            if (genre == null)
            {
                return this.NotFound();
            }

            return this.Ok(new
                           {
                               genre.Id,
                               genre.Name
                           });
        }

        // PUT: api/Genres/5
        public IHttpActionResult PutGenre(int id, UpdateGenreInputModel genreModel)
        {
            if (genreModel == null)
            {
                return this.BadRequest("Model cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var genre = this.MoviesGalleryData.Genres.All().FirstOrDefault(g => g.Id == id);
            if (genre == null)
            {
                return this.NotFound();
            }

            genre.Name = genreModel.Name;
            this.MoviesGalleryData.Genres.Update(genre);
            this.MoviesGalleryData.SaveChanges();
            return this.Ok(new { id, genre.Name });
        }

        // POST: api/Genres
        public IHttpActionResult PostGenre(AddGenreInputModel genreModel)
        {
            if (genreModel == null)
            {
                return this.BadRequest("Model cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var genre = new Genre() { Name = genreModel.Name };
            this.MoviesGalleryData.Genres.Add(genre);
            this.MoviesGalleryData.SaveChanges();

            return this.CreatedAtRoute(
                "DefaultApi",
                new { id = genre.Id },
                new
                {
                    genre.Id,
                    genre.Name
                });
        }

        // DELETE: api/Genres/5
        public IHttpActionResult DeleteGenre(int id)
        {
            Genre genre = this.MoviesGalleryData.Genres.Find(id);
            if (genre == null)
            {
                return this.NotFound();
            }

            this.MoviesGalleryData.Genres.Delete(genre);
            this.MoviesGalleryData.SaveChanges();

            return this.Ok(new
                           {
                               genre.Id,
                               genre.Name
                           });
        }
    }
}