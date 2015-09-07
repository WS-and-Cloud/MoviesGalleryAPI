namespace MoviesGallery.Services.Controllers
{
    using System.Web.Http;

    using MoviesGallery.Data;
    using MoviesGallery.Data.UoW;

    public class BaseApiController : ApiController
    {
        public BaseApiController() 
            : this (new MoviesGalleryData(new MoviesGalleryDbContext()))
        {
        }

        public BaseApiController(IMoviesGalleryData moviesGalleryData)
        {
            this.MoviesGalleryData = moviesGalleryData;
        }

        protected IMoviesGalleryData MoviesGalleryData { get; private set; }
    }
}
