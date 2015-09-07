namespace MoviesGallery.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using MoviesGallery.Data.Migrations;
    using MoviesGallery.Models;

    public class MoviesGalleryDbContext : IdentityDbContext<ApplicationUser>
    {
        public MoviesGalleryDbContext()
            : base("MoviesGalleryConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesGalleryDbContext, Configuration>());
        }

        public virtual IDbSet<Movie> Movies { get; set; }

        public virtual IDbSet<Author> Authors { get; set; }

        public virtual IDbSet<Genre> Genres { get; set; }

        public virtual IDbSet<Review> Reviews { get; set; }

        public static MoviesGalleryDbContext Create()
        {
            return new MoviesGalleryDbContext();
        }
    }
}