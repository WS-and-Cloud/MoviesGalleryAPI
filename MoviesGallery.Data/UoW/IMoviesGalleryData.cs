namespace MoviesGallery.Data.UoW
{
    using MoviesGallery.Data.Repositories;
    using MoviesGallery.Models;

    public interface IMoviesGalleryData
    {
        IRepository<Movie> Movies { get; }

        IRepository<Author> Authors { get; }

        IRepository<Review> Reviews { get; }

        IRepository<Genre> Genres { get; }

        IRepository<ApplicationUser> ApplicationUsers { get; }

        int SaveChanges(); 
    }
}