namespace MoviesGallery.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using MoviesGallery.Data;
    using MoviesGallery.Data.UoW;
    using MoviesGallery.Models;
    using MoviesGallery.Services.Models.UsersModels;

    public class AuthorsController : BaseApiController
    {
        public AuthorsController()
            : base(new MoviesGalleryData(new MoviesGalleryDbContext()))
        {
        }

        public AuthorsController(IMoviesGalleryData moviesGalleryData)
            : base(moviesGalleryData)
        {
        }

        // GET: api/Authors/movieId={movieId}
        [Route("api/authors/movieId={movieId}")]
        public IHttpActionResult GetAuthorByMovieId(int movieId)
        {
            var movie = this.MoviesGalleryData.Movies.Find(movieId);
            if (movie == null)
            {
                return this.NotFound();
            }

            var author =
                this.MoviesGalleryData.Authors.All()
                    .Where(a => a.Movies.Any(m => m.Id == movieId))
                    .Select(ShortActorDataModel.Create);
            
            return this.Ok(author);
        }


        // GET: api/Authors/5
        public IHttpActionResult GetAuthorById(int id)
        {
            var t = this.MoviesGalleryData.Authors.All().ToList();
            Author author = this.MoviesGalleryData.Authors.All().FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return this.NotFound();
            }
            var result =
                this.MoviesGalleryData.Authors.All()
                    .Where(a => a.Id == author.Id)
                    .Select(ShortActorDataModel.Create)
                    .First();
            return this.Ok(result);
        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthor(int id, UpdateAuthorInputModel authorModel)
        {
            if (authorModel == null)
            {
                return this.BadRequest("Model cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var author = this.MoviesGalleryData.Authors.Find(id);
            if (author == null)
            {
                return this.NotFound();
            }

            if (authorModel.Name != null)
            {
                author.Name = authorModel.Name;
            }

            if (authorModel.DateOfBirth != null)
            {
                author.DateOfBirth = Convert.ToDateTime(authorModel.DateOfBirth);
            }

            if (authorModel.Biography != null)
            {
                author.Biography = authorModel.Biography;
            }


            if (authorModel.HomeTown != null)
            {
                author.HomeTown = authorModel.HomeTown;
            }

            this.MoviesGalleryData.Authors.Update(author);
            this.MoviesGalleryData.SaveChanges();

            return this.Ok(new { author.Id, author.Name, author.DateOfBirth });
        }

        // POST: api/Authors
        [ResponseType(typeof(Author))]
        public IHttpActionResult PostAuthor(AddAuthorInputModel authorModel)
        {
            if (authorModel == null)
            {
                return this.BadRequest("Model cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var author = new Author()
                         {
                             Name = authorModel.Name,
                             DateOfBirth = authorModel.DateOfBirth,
                             Biography = authorModel.Biography,
                             HomeTown = authorModel.HomeTown
                         };
            this.MoviesGalleryData.Authors.Add(author);
            this.MoviesGalleryData.SaveChanges();
            return this.Ok(new { author.Id, author.Name });
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            Author author = this.MoviesGalleryData.Authors.All().FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return this.NotFound();
            }

            this.MoviesGalleryData.Authors.Delete(author);
            this.MoviesGalleryData.SaveChanges();

            return this.Ok(new { author.Id, author.Name });
        }
    }
}